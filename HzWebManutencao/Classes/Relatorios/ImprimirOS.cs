using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using HzLibConnection.Sql;
using HzLibCorporativo.Funcional;
using HzlibWEB;
using HzLibManutencao;
using sharpPDF;
using sharpPDF.Enumerators;
using sharpPDF.Tables;

namespace HzWebManutencao.Classes.Relatorios
{
    public class ImprimirOs
    {
        #region Variables
        /// <summary>
        /// Código da Ordem de Serviço
        /// </summary>
        public string cmpIdOS { get; set; }
        /// <summary>
        /// Código da Obra
        /// </summary>
        public string cmpCoObra { get; set; }
        /// <summary>
        /// Caminho da imagem da logo da Obra
        /// </summary>
        public string EnderecoLogoObra { get; set; }
        /// <summary>
        /// Caminho da imagem da logo da Orion
        /// </summary>
        public string EnderecoLogoOrion { get; set; }
        /// <summary>
        /// Nome da Obra
        /// </summary>
        public string NomeObra { get; set; }
        /// <summary>
        /// Documento PDF
        /// </summary>
        public pdfDocument myDoc { get; set; }
        /// <summary>
        /// Página PDF
        /// </summary>
        public pdfPage myPage { get; set; }

        String strFont = "Helvetica";
        int leftMargin = 30;
        string nNuOs;
        string nNuDemanda;
        string DadosObra;

        int pg = 1;
        int top = 40;
        int nTamLinPixel = 19; //Tamanho da lin em pixel
        // Variaveis de controle para quebra de linha de campos
        int QtdCaracterLinha;
        int Tamanho;
        int QtdLin;
        int StrInicio;
        int StrFim;
        int cl;

        float PercentualBdiLucro;
        float PercentualBdiTributo;

        #endregion
  
        protected string TiraCaractEspecial(string Campo)
        {
            Campo = Campo.Replace("\r", string.Empty);
            Campo = Campo.Replace("\n", string.Empty);
            return Campo;
        }

        protected static int getTop(pdfPage myPage, int nTop)
        {
            return myPage.height - nTop;
        }

        #region Cabeçalho
        protected void CabRelatorio(pdfDocument myDoc, pdfPage myPage, int top, string nNuOs, string nNuDemanda)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;

            //myDoc.addImageReference(EnderecoLogoOrion, "logo1");
            //myPage.addImage(myDoc.getImageReference("logo1"), leftMargin, getTop(myPage, top));

            //if (EnderecoLogoObra != "")
            //{
            //    myDoc.addImageReference(EnderecoLogoObra, "logo2");
            //    myPage.addImage(myDoc.getImageReference("logo2"), 530, getTop(myPage, top));
            //}

            //top += 10;

            myPage.addText("ORDEM DE SERVIÇO", 235, getTop(myPage, 30), myDoc.getFontReference(strFont), 12, pdfColor.Black);

            pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont), 9, pdfColor.Black, pdfColor.White));
            top += 20;

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);

            int cl = 0;
            myTable.tableHeader.rowHeight = 20;
            myTable.tableHeader.addColumn(570, predefinedAlignment.csLeft);
            myTable.tableHeader[cl].addText("Obra: " + DadosObra);
            myPage.addTable(myTable);

            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont),  9, pdfColor.Black, pdfColor.White));
            top += 20;
            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);

            cl = 0;
            myTable.tableHeader.rowHeight = 20;
            myTable.tableHeader.addColumn(285, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Nº ORDEM DE SERVIÇO: " + nNuOs.ToString());
            top += 20;

            myTable.tableHeader.rowHeight = 20;
            myTable.tableHeader.addColumn(285, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Nº DEMANDA CLIENTE: " + nNuDemanda.ToString());
            myPage.addTable(myTable);
        }
        #endregion

        #region Cabeçalho Material Sem Valor
        protected pdfTable CabMaterialSemValor()
        {
            String strFont = "Helvetica";
            int leftMargin = 30;

            pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont), 7, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);

            int cl = 0;
            myTable.tableHeader.rowHeight = 10;
            myTable.tableHeader.addColumn(30, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Item");

            myTable.tableHeader.rowHeight = 10;
            myTable.tableHeader.addColumn(460, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Descrição");

            myTable.tableHeader.addColumn(30, predefinedAlignment.csCenter);
            myTable.tableHeader[cl].columnAlign = predefinedAlignment.csCenter;
            myTable.tableHeader[cl++].addText("Unid");

            myTable.tableHeader.addColumn(50, predefinedAlignment.csRight);
            myTable.tableHeader[cl].columnAlign = predefinedAlignment.csRight;
            myTable.tableHeader[cl++].addText("Qtd.");

            return myTable;
        }
        #endregion

        #region ImprimeOSSemValorMaterial
        private void ImprimeOsSemValorMaterial(DataTable tblMat)
        {
            pdfTable myTable = CabMaterialSemValor();
            top += 15;
            QtdCaracterLinha = 120;

            foreach (DataRow row in tblMat.Rows)
            {
                // Create Table Row
                int cl = 0;
                pdfTableRow myRow = myTable.createRow();
                myRow.rowStyle = myTable.rowStyle;
                myRow[cl].columnAlign = predefinedAlignment.csLeft;
                myRow[cl++].addText(row["cmpDcItem"].ToString().Trim());

                row["cmpDcMaterialObraGrupo"] = TiraCaractEspecial(row["cmpDcMaterialObraGrupo"].ToString());

                Tamanho = row["cmpDcMaterialObraGrupo"].ToString().Length;
                QtdLin = (Tamanho % QtdCaracterLinha == 0 ? Tamanho / QtdCaracterLinha : (Tamanho / QtdCaracterLinha) + 1);
                StrInicio = 0;

                for (int Linha = 1; Linha <= QtdLin; Linha++)
                {
                    StrFim = Linha == QtdLin ? Tamanho - StrInicio : QtdCaracterLinha;
                    if (Linha == 1)
                    {
                        myRow[cl++].addText(row["cmpDcMaterialObraGrupo"].ToString().Substring(StrInicio, StrFim).Trim());
                        myRow[cl++].addText(row["cmpDcUnidade"].ToString().Trim());
                        myRow[cl++].addText(decimal.Parse(row["cmpQtMaterial"].ToString()).ToString("0,0.00"));
                        myTable.addRow(myRow);
                        StrInicio += QtdCaracterLinha;
                        top += 11;
                    }
                    else
                    {
                        cl = 0;
                        myRow = myTable.createRow();
                        myRow.rowStyle = myTable.rowStyle;
                        myRow[cl].columnAlign = predefinedAlignment.csLeft;
                        myRow[cl++].addText("");
                        myRow[cl++].addText(row["cmpDcMaterialObraGrupo"].ToString().Substring(StrInicio, StrFim).Trim());
                        myRow[cl++].addText("");
                        myRow[cl++].addText("");
                        myTable.addRow(myRow);
                        StrInicio += QtdCaracterLinha;
                        top += 11;
                    }

                    if (top > 730)
                    {
                        myPage.addTable(myTable);

                        myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                                    new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                                    new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));
                        myTable.coordX = leftMargin;
                        myTable.coordY = getTop(myPage, top); ;

                        top += 152;
                        myTable.coordX = leftMargin;
                        myTable.coordY = getTop(myPage, top); ;

                        myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);

                        // Add a page
                        myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);

                        // Cabeçalho
                        top = 40;
                        CabRelatorio(myDoc, myPage, top, nNuOs, nNuDemanda);
                        top += 70;

                        // Header Text                
                        //myPage.addText("Ordem de Serviço", leftMargin, getTop(myPage, 30),
                        //                myDoc.getFontReference(strFont), 15, pdfColor.Black);
                        //top = 40;
                        //myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                        //            new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                        //            new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

                        //myTable.coordX = leftMargin;
                        //myTable.coordY = getTop(myPage, top);
                        
                        ////// Create table's header
                        //cl = 0;
                        //myTable.tableHeader.rowHeight = 10;
                        //myTable.tableHeader.addColumn(150, predefinedAlignment.csLeft);
                        //myTable.tableHeader[cl++].addText("Nº O.S.");

                        //myTable.tableHeader.addColumn(420, predefinedAlignment.csLeft);
                        //myTable.tableHeader[cl].columnAlign = predefinedAlignment.csLeft;
                        //myTable.tableHeader[cl++].addText(nNuOs.ToString());
                        //myPage.addTable(myTable);

                        //top += nTamLinPixel;

                        myTable = CabMaterialSemValor();
                    }
                }
            }

            myPage.addTable(myTable);
        }
        #endregion

        #region Cabeçalho Material
        protected pdfTable CabMaterial()
        {
           String strFont = "Helvetica";
           int leftMargin = 30;

           pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                              new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, new pdfColor("FFFFFF")),
                              new pdfTableStyle(myDoc.getFontReference(strFont), 7, pdfColor.Black, pdfColor.White));

           myTable.coordX = leftMargin;
           myTable.coordY = getTop(myPage, top);

           int cl = 0;
           myTable.tableHeader.rowHeight = 10;
           myTable.tableHeader.addColumn(30, predefinedAlignment.csLeft);
           myTable.tableHeader[cl++].addText("Item");

           myTable.tableHeader.rowHeight = 10;
           myTable.tableHeader.addColumn(320, predefinedAlignment.csLeft);
           myTable.tableHeader[cl++].addText("Descrição");

           myTable.tableHeader.addColumn(30, predefinedAlignment.csCenter);
           myTable.tableHeader[cl].columnAlign = predefinedAlignment.csCenter;
           myTable.tableHeader[cl++].addText("Unid");

           myTable.tableHeader.addColumn(50, predefinedAlignment.csRight);
           myTable.tableHeader[cl].columnAlign = predefinedAlignment.csRight;
           myTable.tableHeader[cl++].addText("Qtd.");

           myTable.tableHeader.addColumn(50, predefinedAlignment.csRight);
           myTable.tableHeader[cl].columnAlign = predefinedAlignment.csRight;
           myTable.tableHeader[cl++].addText("Preço");

           myTable.tableHeader.addColumn(90, predefinedAlignment.csRight);
           myTable.tableHeader[cl].columnAlign = predefinedAlignment.csRight;
           myTable.tableHeader[cl++].addText("Sub-Total");

           return myTable;
       }
        #endregion

        #region Cabeçalho Material Com Valor
        private void ImprimeOsComValorMaterial(DataTable tblMat)
        {
           pdfTable myTable = CabMaterial();
           pdfTableRow myRow;
           top += 15;
           QtdCaracterLinha = 73;

           float total = 0f;

            foreach (DataRow row in tblMat.Rows)
            {
                // Create Table Row
                cl = 0;
                myRow = myTable.createRow();
                myRow.rowStyle = myTable.rowStyle;
                myRow[cl].columnAlign = predefinedAlignment.csLeft;
                myRow[cl++].addText(row["cmpDcItem"].ToString().Trim());

                row["cmpDcMaterialObraGrupo"] = TiraCaractEspecial(row["cmpDcMaterialObraGrupo"].ToString());

                Tamanho = row["cmpDcMaterialObraGrupo"].ToString().Length;
                QtdLin = (Tamanho % QtdCaracterLinha == 0 ? Tamanho / QtdCaracterLinha : (Tamanho / QtdCaracterLinha) + 1);
                StrInicio = 0;

                for (int Linha = 1; Linha <= QtdLin; Linha++)
                {
                    StrFim = Linha == QtdLin ? Tamanho - StrInicio : QtdCaracterLinha;
                    if (Linha == 1)
                    {
                        myRow[cl++].addText(row["cmpDcMaterialObraGrupo"].ToString().Substring(StrInicio, StrFim).Trim());
                        myRow[cl++].addText(row["cmpDcUnidade"].ToString().Trim());
                        myRow[cl++].addText(decimal.Parse(row["cmpQtMaterial"].ToString()).ToString("0,0.00"));
                        myRow[cl++].addText(decimal.Parse(row["cmpVlMaterial"].ToString()).ToString("0,0.00"));
                        myRow[cl++].addText(decimal.Parse(row["cmpVlSubTotal"].ToString()).ToString("0,0.00"));
                        total += float.Parse(row["cmpVlSubTotal"].ToString());
                        myTable.addRow(myRow);
                        StrInicio += QtdCaracterLinha;
                        top += 11;
                    }
                    else
                    {
                        cl = 0;
                        myRow = myTable.createRow();
                        myRow.rowStyle = myTable.rowStyle;
                        myRow[cl].columnAlign = predefinedAlignment.csLeft;
                        myRow[cl++].addText("");
                        myRow[cl++].addText(row["cmpDcMaterialObraGrupo"].ToString().Substring(StrInicio, StrFim).Trim());
                        myRow[cl++].addText("");
                        myRow[cl++].addText("");
                        myRow[cl++].addText("");
                        myRow[cl++].addText("");
                        myTable.addRow(myRow);
                        StrInicio += QtdCaracterLinha;
                        top += 11;
                    }

                    if (top > 730)
                    {
                        myPage.addTable(myTable);

                        myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                                    new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                                    new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));
                        myTable.coordX = leftMargin;
                        myTable.coordY = getTop(myPage, top);

                        //// Create table's header
                        cl = 0;
                        myTable.tableHeader.rowHeight = 10;
                        myTable.tableHeader.addColumn(570, predefinedAlignment.csRight);
                        myTable.tableHeader[cl++].addText("Subtotal: " + total.ToString("0,0.00"));
                        myPage.addTable(myTable);

                        //top += 152;
                        //myTable.coordX = leftMargin;
                        //myTable.coordY = getTop(myPage, top);

                        myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);

                        // Add a page
                        myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);
                        // Cabeçalho
                        top = 40;
                        CabRelatorio(myDoc, myPage, top, nNuOs, nNuDemanda);
                        top += 70;
                        myTable = CabMaterial();
                    }
                }
            }

            myPage.addTable(myTable);
           
            top += 13;

            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                        new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                        new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);

            cl = 0;
            myTable.tableHeader.rowHeight = 10;
            myTable.tableHeader.addColumn(480, predefinedAlignment.csRight);
            myTable.tableHeader[cl++].addText("Total do Material R$ ");

            myTable.tableHeader.addColumn(90, predefinedAlignment.csRight);
            myTable.tableHeader[cl++].addText(total.ToString("0,0.00"));
            top += 20;

            float ValorBdiLucro = (total * PercentualBdiLucro) / 100;
            float ValorBdiTributo = (total * PercentualBdiTributo) / 100;

            float TotalMaterialLucroTributo = total + ValorBdiLucro + ValorBdiTributo;

            // Create Table Row
            myRow = myTable.createRow();
            cl = 0;
            myRow = myTable.createRow();
            myRow[cl].columnAlign = predefinedAlignment.csRight;
            myRow[cl++].addText("Lucro + Despesas Administrativas (" + PercentualBdiLucro.ToString() + "%)");
            myRow[cl++].addText(ValorBdiLucro.ToString("0,0.00"));
            myTable.addRow(myRow);

            top += 20;
            cl = 0;
            myRow = myTable.createRow();
            myRow[cl].columnAlign = predefinedAlignment.csRight;
            myRow[cl++].addText("Tributos (" + PercentualBdiTributo.ToString() + "%)");
            myRow[cl++].addText(ValorBdiTributo.ToString("0,0.00"));
            myTable.addRow(myRow);

            top += 20;
            cl = 0;
            myRow = myTable.createRow();
            myRow[cl].columnAlign = predefinedAlignment.csRight;
            myRow[cl++].addText("Total (Material + Lucro + Tributos)");
            myRow[cl++].addText(TotalMaterialLucroTributo.ToString("0,0.00"));
            myTable.addRow(myRow);
            myPage.addTable(myTable);

            if (top > 680)
            {
                myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);
                // Add a page
                myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);
                // Cabeçalho
                top = 40;
                CabRelatorio(myDoc, myPage, top, nNuOs, nNuDemanda);
                top += 70;
            }
        }
        #endregion

        #region Imprime Ordem de Serviço
        public void ImprimeOrdemServico()
        {
           top  = 40;
           pg   = 1;
                      
            using (DataTable table = Global.GetConnection().loadDataTable("select * from HzManutencao..vwATE_OS where cmpCoObraGrupoLista = " + cmpCoObra + " And cmpIdOs = " + cmpIdOS))
            {
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow r = table.Rows[0];
                    nNuOs = r["cmpNuOS"].ToString();
                    nNuDemanda = r["cmpNuDemandaCliente"].ToString();

                    DadosObra = r["cmpNoObra"].ToString().Trim();  // +(r["cmpNuContrato"] != null ? " -> Contrato nº " + r["cmpNuContrato"].ToString() : "");

                    CabRelatorio(myDoc, myPage, top, nNuOs, nNuDemanda);
                    top += 70;

                    if (bool.Parse(r["cmpInValorMaterial"].ToString()))
                    {
                        PercentualBdiLucro    = float.Parse(r["cmpPeBDILucro"].ToString());
                        PercentualBdiTributo  = float.Parse(r["cmpPeBDITributos"].ToString());
                    }

                    #region Antes de Iniciar o Serviço
                    pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                                       new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                                       new pdfTableStyle(myDoc.getFontReference(strFont), 9, pdfColor.Black, pdfColor.White));
                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);
                    int cl = 0;
                    myTable.tableHeader.rowHeight = 20;
                    myTable.tableHeader.addColumn(570, predefinedAlignment.csCenter);
                    myTable.tableHeader[cl].addText("Antes de Iniciar o Serviço");
                    myPage.addTable(myTable);

                    top += 20;

                    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                                        new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                                        new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);

                    //// Create table's header
                    cl = 0;
                    myTable.tableHeader.rowHeight = 10;
                    myTable.tableHeader.addColumn(110, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl++].addText("Solicitado em");
                    myTable.tableHeader.addColumn(180, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl].columnAlign = predefinedAlignment.csLeft;
                    myTable.tableHeader[cl++].addText(r["cmpDtAbertura"].ToString());
                    myTable.tableHeader.addColumn(100, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl].columnAlign = predefinedAlignment.csLeft;
                    myTable.tableHeader[cl++].addText("Tipo da O.S");
                    myTable.tableHeader.addColumn(180, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl].columnAlign = predefinedAlignment.csLeft;
                    myTable.tableHeader[cl++].addText(r["cmpDcOrigemOS"].ToString());

                    top += nTamLinPixel;

                    // Create Table Row
                    pdfTableRow myRow = myTable.createRow();
                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Forma Solicitação");
                    myRow[cl++].addText(r["cmpDcFormaSolicitacao"].ToString().Trim());
                    myRow[cl++].addText("Setor");
                    myRow[cl++].addText(r["cmpNoSetor"].ToString().Trim());
                    myTable.addRow(myRow);

                    top += nTamLinPixel - 5;

                    myPage.addTable(myTable);

                    //Local e pavimento de atendimento
                    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                              new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                              new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);

                    //// Create table's header
                    cl = 0;
                    myTable.tableHeader.rowHeight = 10;
                    myTable.tableHeader.addColumn(110, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl++].addText("Local");

                    myTable.tableHeader.addColumn(460, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl].columnAlign = predefinedAlignment.csLeft;
                    myTable.tableHeader[cl++].addText(r["cmpDcLocal"].ToString().TrimEnd());

                    top += nTamLinPixel;

                    // Create Table Row
                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Pavimento");
                    myRow[cl++].addText(r["cmpDcPavimento"].ToString().Trim());
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    //Nome do solicitante
                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl++].addText("Solicitante");
                    myRow[cl++].addText(r["cmpNoSolicitante"].ToString().Trim());
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    //Telefone do solicitante
                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl++].addText("Telefone/Ramal");
                    myRow[cl++].addText(r["cmpNuTelefone"].ToString().Trim());
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    //Email para contato
                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl++].addText("Email para Contato");
                    myRow[cl++].addText(r["cmpEeEmail"].ToString().Trim());
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    //Tipo de serviço
                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl++].addText("Tipo do Serviço");
                    myRow[cl++].addText(r["cmpDcTipoAtividade"].ToString().Trim());
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    //Solicitação do serviço
                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl++].addText("Solicitação");
                    myRow[cl++].addText(r["cmpDcDescricaoSolicitacao"].ToString().Trim());
                    myTable.addRow(myRow);

                    myPage.addTable(myTable);

                    top += nTamLinPixel - 27;

                    //Detalhamento da Solicitação
                    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                                new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                                new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);

                    //// Create table's header
                    cl = 0;
                    myTable.tableHeader.rowHeight = 10;
                    myTable.tableHeader.addColumn(570, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl++].addText("Detalhamento da Solicitação");

                    top += nTamLinPixel;

                    r["cmpDcObservacoes"] = TiraCaractEspecial(r["cmpDcObservacoes"].ToString().TrimEnd());
                    Tamanho = r["cmpDcObservacoes"].ToString().Length;
                    QtdLin = (Tamanho % 115 == 0 ? Tamanho / 115 : (Tamanho / 115) + 1);
                    StrInicio = 0;

                    for (int Linha = 1; Linha <= QtdLin; Linha++)
                    {
                        cl = 0;
                        myRow = myTable.createRow();
                        myRow[cl].columnAlign = predefinedAlignment.csLeft;

                        StrFim = Linha == QtdLin ? Tamanho - StrInicio : 115;
                        myRow[cl++].addText(r["cmpDcObservacoes"].ToString().Substring(StrInicio, StrFim).Trim());
                        myTable.addRow(myRow);
                        StrInicio += 115;
                    }
                    top += nTamLinPixel * QtdLin - 15;
                    myPage.addTable(myTable);
                    #endregion

                    #region Execução do Serviço
                    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                                       new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                                       new pdfTableStyle(myDoc.getFontReference(strFont), 9, pdfColor.Black, pdfColor.White));
                    top += 5;
                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);
                    cl = 0;
                    myTable.tableHeader.rowHeight = 20;
                    myTable.tableHeader.addColumn(570, predefinedAlignment.csCenter);
                    myTable.tableHeader[cl].addText("Execução do Serviço ");
                    myPage.addTable(myTable);

                    top += nTamLinPixel;

                    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                                new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                                new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);

                    cl = 0;
                    myTable.tableHeader.rowHeight = 10;
                    myTable.tableHeader.addColumn(70, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl++].addText("Início em");
                    myTable.tableHeader.addColumn(100, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl++].addText(r["cmpDtInicioAtendimento"].ToString() != "" ? DateTime.Parse(r["cmpDtInicioAtendimento"].ToString()).ToString("dd/MM/yyyy HH:mm") : "");
                    myTable.tableHeader.addColumn(70, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl++].addText("Término em");
                    myTable.tableHeader.addColumn(100, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl++].addText(r["cmpDtConclusaoAtendimento"].ToString() != "" ? DateTime.Parse(r["cmpDtConclusaoAtendimento"].ToString()).ToString("dd/MM/yyyy HH:mm") : "");
                    myTable.tableHeader.addColumn(70, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl++].addText("Gestor");
                    myTable.tableHeader.addColumn(160, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl++].addText(r["cmpNoAtestador"].ToString().Trim());
                    myPage.addTable(myTable);

                    top += nTamLinPixel - 5;

                    //Executantes do serviço
                    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                              new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                              new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);

                    //// Create table's header
                    cl = 0;
                    myTable.tableHeader.rowHeight = 10;
                    myTable.tableHeader.addColumn(70, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl++].addText("Executante(s)");
                    myTable.tableHeader.addColumn(500, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl++].addText(r["cmpNoExecutor"].ToString().TrimEnd());
                    myPage.addTable(myTable);

                    top += nTamLinPixel;

                    //Detalhamento da Serviço Executado
                    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                                new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                                new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);

                    //// Create table's header
                    cl = 0;
                    myTable.tableHeader.rowHeight = 10;
                    myTable.tableHeader.addColumn(570, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl++].addText("Detalhamento do Serviço Executado");

                    top += nTamLinPixel;

                    r["cmpDcObservacaoConclusao"] = TiraCaractEspecial(r["cmpDcObservacaoConclusao"].ToString());
                    Tamanho = r["cmpDcObservacaoConclusao"].ToString().Length;
                    QtdLin = (Tamanho % 120 == 0 ? Tamanho / 120 : (Tamanho / 120) + 1);
                    StrInicio = 0;
                    for (int Linha = 1; Linha <= QtdLin; Linha++)
                    {
                        cl = 0;
                        myRow = myTable.createRow();
                        StrFim = Linha == QtdLin ? Tamanho - StrInicio : 120;
                        //if (QtdLin > 1)
                        //{
                        //    myRow[cl++].addText(r["cmpDcObservacaoConclusao"].ToString().Substring(StrInicio, StrFim).Trim());
                        //}
                        //else
                        //{
                            string observacao = r["cmpDcObservacaoConclusao"].ToString().Substring(StrInicio, StrFim).Trim();
                            //observacao=observacao.Replace(@"\"," ");
                            observacao = observacao.Replace("\t", string.Empty);

                            myRow[cl++].addText(observacao);
                        //}
                        myTable.addRow(myRow);
                        StrInicio += 120;
                    }
                    top += nTamLinPixel * QtdLin - 1;
                    myPage.addTable(myTable);
                    #endregion

                    #region Lista de Material/Serviço Utilizado
                    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                                new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                                new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));
                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);

                    //// Create table's header
                    cl = 0;
                    myTable.tableHeader.rowHeight = 10;
                    myTable.tableHeader.addColumn(570, predefinedAlignment.csCenter);
                    myTable.tableHeader[cl++].addText("Material / Serviço Utilizado");

                    myPage.addTable(myTable);

                    top += nTamLinPixel;

                    string sql = "select * from HzManutencao..vwATE_OSMaterial where cmpCoObraGrupoLista = " + cmpCoObra + " And cmpIdOS = " + cmpIdOS;
                    using (DataTable tblMat = Global.GetConnection().loadDataTable(sql))
                    if (tblMat != null && tblMat.Rows.Count > 0)
                    {
                        if (bool.Parse(r["cmpInValorMaterial"].ToString()))
                            ImprimeOsComValorMaterial(tblMat);
                        else
                            ImprimeOsSemValorMaterial(tblMat);
                    }

                    #endregion 

                    top += 10;

                    //Aceite do Cliente
                    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                                       new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                                       new pdfTableStyle(myDoc.getFontReference(strFont), 9, pdfColor.Black, pdfColor.White));

                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);
                    cl = 0;
                    myTable.tableHeader.rowHeight = 20;
                    myTable.tableHeader.addColumn(570, predefinedAlignment.csCenter);
                    myTable.tableHeader[cl].addText("Aceite do Cliente");
                    myPage.addTable(myTable);

                    top += 20;

                    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                                new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                                new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));
                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);
                    cl = 0;
                    myRow = myTable.createRow();
                    myTable.tableHeader.rowHeight = 10;
                    myTable.tableHeader.addColumn(570, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl++].addText("Satisfeito com o serviço: " + "__Sim         __Não");
                    myPage.addTable(myTable);

                    top += 20;

                    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                                new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                                new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);

                    cl = 0;
                    myTable.tableHeader.rowHeight = 40;
                    myTable.tableHeader.rowVerticalAlign = predefinedVerticalAlignment.csBottom;
                    myTable.tableHeader.addColumn(285, predefinedAlignment.csCenter);
                    myTable.tableHeader[cl].columnVerticalAlign = predefinedVerticalAlignment.csBottom;
                    myTable.tableHeader[cl++].addText("Assinatura: GrupoOrion");

                    myTable.tableHeader.addColumn(285, predefinedAlignment.csCenter);
                    myTable.tableHeader[cl].columnVerticalAlign = predefinedVerticalAlignment.csBottom;
                    myTable.tableHeader[cl].addText("Assinatura: Cliente");

                    myPage.addTable(myTable);

                    myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);

                }
            }
        }
        #endregion
    }
}