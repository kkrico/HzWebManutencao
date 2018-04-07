using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibConnection.Sql;
using System.Data;
using HzLibCorporativo.Funcional;
using HzlibWEB;
using HzLibManutencao;
using sharpPDF;
using sharpPDF.Enumerators;
using sharpPDF.Tables;

namespace HzWebManutencao.Classes.Relatorios
{
    public class ImprimirAgendaPreventiva
    {
        #region Variables
        /// <summary>
        /// Caminho da imagem da logo da Obra
        /// </summary>
        public string EnderecoLogoObra { get; set; }
        /// <summary>
        /// Caminho da imagem da logo da Orion
        /// </summary>
        public string EnderecoLogoOrion { get; set; }
        #endregion

        #region Base Functions
        protected void CabRelatorio(pdfDocument myDoc, pdfPage myPage, int top, string DadosObra, string DtInicial, string DtFinal)
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

            //top += 20;

            myPage.addText("AGENDA MANUTENÇÃO PREVENTIVA", 235, getTop(myPage, top),
                            myDoc.getFontReference(strFont), 12, pdfColor.Black);

            pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, pdfColor.White));
            top += 10;

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);

            int cl = 0;
            myTable.tableHeader.rowHeight = 20;
            myTable.tableHeader.addColumn(350, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Obra: " + DadosObra.ToString().TrimEnd());

            myTable.tableHeader.addColumn(200, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Período: " + DtInicial + " a " + DtFinal);

            myPage.addTable(myTable);
        }

        protected static void SubCabRelatorio(pdfDocument myDoc, pdfPage myPage, int top, DataRow row)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;

            pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 9, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);

            int cl = 0;
            myTable.tableHeader.rowHeight = 20;

            myTable.tableHeader.addColumn(110, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("ATIVIDADE");

            myTable.tableHeader.addColumn(60, predefinedAlignment.csCenter);
            myTable.tableHeader[cl++].addText("DATA");

            myTable.tableHeader.addColumn(80, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Nº PREVENTIVA");

            myTable.tableHeader.addColumn(90, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("PERIODICIDADE");

            myTable.tableHeader.addColumn(100, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("SITUAÇÃO");

            myTable.tableHeader.addColumn(110, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("TIPO PREVENTIVA");

            top += 20;
            cl = 0;
            pdfTableRow myRow = myTable.createRow();
            myRow.rowStyle    = myTable.rowStyle;

            myRow[cl++].addText(row["cmpDcTipoAtividade"].ToString().TrimEnd());
            myRow[cl++].addText(row["cmpDtReprogramacaoPreventivaAgenda"].ToString().Substring(0, 10));
            myRow[cl++].addText(row["cmpNuPreventivaAgenda"].ToString().TrimEnd());
            myRow[cl++].addText(row["cmpDcPeriodicidade"].ToString().TrimEnd());
            myRow[cl++].addText(row["EstadoManutencaoPreventiva"].ToString().TrimEnd());
            myRow[cl++].addText(row["TPPREVENTIVA"].ToString().TrimEnd());

            myTable.addRow(myRow);

            myPage.addTable(myTable);
        }

        protected static void ImprimeGrupo(pdfDocument myDoc, pdfPage myPage, int top, string GrupoAtividade)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;

            pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 9, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);
            int cl = 0;
            myTable.tableHeader.addColumn(550, predefinedAlignment.csLeft);
            myTable.tableHeader.rowHeight = 20;
            myTable.tableHeader[cl++].addText("GRUPO DE ATIVIDADE: " + GrupoAtividade);
            myPage.addTable(myTable);
        }

        protected static void ItensAtividade(pdfDocument myDoc, pdfPage myPage, int top)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;

            pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 9, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);

            int cl = 0;
            myTable.tableHeader.rowHeight = 20;
            myTable.tableHeader.addColumn(550, predefinedAlignment.csCenter);
            myTable.tableHeader[cl++].addText("ITENS DE ATIVIDADE");

            myPage.addTable(myTable);
        }

        protected static int getTop(pdfPage myPage, int nTop)
        {
            return myPage.height - nTop;
        }

        protected static string TiraCaractEspecial(string Campo)
        {
            Campo = Campo.Replace("\r", string.Empty);
            Campo = Campo.Replace("\n", string.Empty);
            return Campo;
        }

        #endregion

        public pdfDocument ImprimeAgenda(DataTable table, string NoObra, string DtInicial, string DtFinal)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;
            int pg = 1;
            int top = 40;
            //Tamanho da lin em pixel
            int nTamLinPixel = 12;

            // Variaveis de controle para quebra de linha de campos
            int QtdCaracterLinha = 145;
            int Tamanho;
            int QtdLin;
            int StrInicio;
            int StrFim;
            int cl;

            // Adiciona Página
            pdfDocument myDoc   = new pdfDocument("Horizon", "Orion");
            pdfPage     myPage  = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);
            pdfTable    myTable;
            pdfTableRow myRow;

            DataTable table2;
            DataColumn column;

            string DadosObra = NoObra;
            int qtdReg;
            foreach (DataRow row in table.Rows)
            {
                top = 40;
                CabRelatorio(myDoc, myPage, top, DadosObra, DtInicial, DtFinal);
                top += 55;

                SubCabRelatorio(myDoc, myPage, top, row);
                top += 32;

                column = table.Columns["cmpCoPreventiva"];
                
                table2 = tblPreventivaAtividade.Get(Global.GetConnection(), row[column].ToString());
                
                string GrupoAtividade = "";
                qtdReg = 0;

                foreach (DataRow row2 in table2.Rows)
                {
                    row2["cmpDcGrupoAtividade"] = TiraCaractEspecial(row2["cmpDcGrupoAtividade"].ToString());

                    if (row2["cmpDcGrupoAtividade"].ToString() != GrupoAtividade)
                    {
                        qtdReg = 0;
                        GrupoAtividade = row2["cmpDcGrupoAtividade"].ToString();
                        ImprimeGrupo(myDoc, myPage, top, GrupoAtividade);
                        top += 20;

                        ItensAtividade(myDoc, myPage, top);
                        top += 20;
                    }

                    cl = 0;
                    row2["cmpDcItemAtividadePreventiva"] = TiraCaractEspecial(row2["cmpDcItemAtividadePreventiva"].ToString());
                    Tamanho = row2["cmpDcItemAtividadePreventiva"].ToString().Length;

                    if (Tamanho > 0)
                    {
                        QtdLin = (Tamanho % QtdCaracterLinha == 0 ? Tamanho / QtdCaracterLinha : (Tamanho / QtdCaracterLinha) + 1);
                        StrInicio = 0;

                        cl = 0;
                        for (int Linha = 1; Linha <= QtdLin; Linha++)
                        {
                            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                                               new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, new pdfColor("FFFFFF")),
                                               new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, pdfColor.White));

                            myTable.coordX = leftMargin;
                            myTable.coordY = getTop(myPage, top);
                            myTable.tableHeader.rowHeight = 10;
                            myTable.tableHeader.addColumn(550, predefinedAlignment.csLeft);

                            StrFim = Linha == QtdLin ? Tamanho - StrInicio : QtdCaracterLinha;
                            
                            myTable.tableHeader[cl].addText(row2["cmpDcItemAtividadePreventiva"].ToString().Substring(StrInicio, StrFim).Trim());

                            myPage.addTable(myTable);

                            StrInicio += QtdCaracterLinha;
                            top += nTamLinPixel;
                            qtdReg++;

                            if (top > 700)
                            {
                                myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);

                                // Adiciona uma página
                                myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);

                                top = 40;
                                CabRelatorio(myDoc, myPage, top, DadosObra, DtInicial, DtFinal);
                                top += 30;
                                if (qtdReg <= table2.Rows.Count)
                                {
                                    SubCabRelatorio(myDoc, myPage, top, row);
                                    top += 32;
                                    ImprimeGrupo(myDoc, myPage, top, GrupoAtividade);
                                    top += 20;
                                    ItensAtividade(myDoc, myPage, top);
                                    top += 20;
                                }
                            }
                        }
                    }
                }
                myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);
                myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);
            }

            //if (top < 700)
            //{
            //    myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);
            //}
            return myDoc;
        }
    }
}