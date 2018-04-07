using System;
using System.Collections.Generic;
using System.Linq;
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


namespace HzWebManutencao
{
    public class ImprimirOS
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
        /// Documento PDF
        /// </summary>
        public pdfDocument myDoc { get; set; }

        #endregion

        #region Base Functions

        protected string TiraCaractEspecial(string Campo)
        {
            Campo = Campo.Replace("\r", string.Empty);
            Campo = Campo.Replace("\n", string.Empty);
            return Campo;
        }

        protected int getTop(pdfPage myPage, int nTop)
        {
            return myPage.height - nTop;
        }

        protected pdfTable CabMaterial(pdfPage myPage, int top)
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

        public void ImprimeOS()
        {
            String strFont = "Helvetica";
            int leftMargin = 30;
            string nNuOs;
            int pg = 1;
            int top = 40;
            int nTamLinPixel = 19; //Tamanho da lin em pixel
            // Variaveis de controle para quebra de linha de campos
            int QtdCaracterLinha;
            int Tamanho;
            int QtdLin;
            int StrInicio;
            int StrFim;

            //pdfDocument myDoc = new pdfDocument("Horizon", "Orion");
            // Add a page
            pdfPage myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);

            // Load an image (I was able to load JPEG, GIF, TIFF, and BMP formats)
            //myDoc.addImageReference(Server.MapPath("~/Assets/Images/Sample.gif"), "ex");
            //myPage.addImage(myDoc.getImageReference("ex"), leftMargin, getTop(myPage, 120));

            // Header Text                
            myPage.addText("Ordem de Serviço", leftMargin, getTop(myPage, 30),
                            myDoc.getFontReference(strFont), 15, pdfColor.Black);

            // Body Text
            //myPage.addText("Divisão de Administração Predial, Obras e Instalações", leftMargin, getTop(myPage, 50),
            //               myDoc.getFontReference(strFont), 10, pdfColor.Black);
            //myPage.addText("Relatório emitido em: " + DateTime.Now.ToShortDateString(), leftMargin, getTop(myPage, 70),
            //               myDoc.getFontReference(strFont), 10, pdfColor.Black);

            using (DataTable table = Global.GetConnection().loadDataTable("select * from HzManutencao..vwATE_OS where cmpCoObraGrupoLista = " + cmpCoObra + " And cmpIdOs = " + cmpIdOS))
            {
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow r = table.Rows[0];

                    pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                                       new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                                       new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);

                    //// Create table's header
                    int cl = 0;
                    myTable.tableHeader.rowHeight = 10;
                    myTable.tableHeader.addColumn(150, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl++].addText("Nº O.S.");

                    myTable.tableHeader.addColumn(420, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl].columnAlign = predefinedAlignment.csLeft;
                    myTable.tableHeader[cl++].addText(r["cmpNuOS"].ToString());
                    nNuOs = r["cmpNuOS"].ToString();

                    top += nTamLinPixel;

                    // Create Table Row
                    pdfTableRow myRow = myTable.createRow();
                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Data Solicitação:");
                    myRow[cl++].addText(r["cmpDtAbertura"].ToString().Trim());
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Obra:");
                    myRow[cl++].addText(r["cmpNoObra"].ToString().Trim() + (r["cmpNuContrato"] != null ? " -> Contrato nº " + r["cmpNuContrato"].ToString() : ""));
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Soliciante:");
                    myRow[cl++].addText(r["cmpNoSolicitante"].ToString().Trim());
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Pavimento:");
                    myRow[cl++].addText(r["cmpDcPavimento"].ToString().Trim());
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Local / Sala:");
                    myRow[cl++].addText(r["cmpDcLocal"].ToString().Trim());
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Setor:");
                    myRow[cl++].addText(r["cmpNoSetor"].ToString().Trim());
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Descrição do Serviço:");
                    myRow[cl++].addText(r["cmpDcDescricaoSolicitacao"].ToString().Trim());
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Detalhamento do Serviço:");

                    r["cmpDcObservacoes"] = TiraCaractEspecial(r["cmpDcObservacoes"].ToString());

                    Tamanho = r["cmpDcObservacoes"].ToString().Length;
                    QtdLin = (Tamanho % 69 == 0 ? Tamanho / 69 : (Tamanho / 69) + 1);
                    StrInicio = 0;

                    for (int Linha = 1; Linha <= QtdLin; Linha++)
                    {
                        if (Linha > 1)
                        {
                            cl = 0;
                            myRow = myTable.createRow();
                            myRow[cl].columnAlign = predefinedAlignment.csLeft;
                            myRow[cl++].addText("");
                        }
                        StrFim = Linha == QtdLin ? Tamanho - StrInicio : 69;
                        myRow[cl++].addText(r["cmpDcObservacoes"].ToString().Substring(StrInicio, StrFim).Trim());
                        myTable.addRow(myRow);
                        StrInicio += 69;
                    }

                    top += nTamLinPixel * QtdLin - 1;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow.rowHeight = 50;
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Conclusão/Aceite do Solicitante:");
                    myRow[cl++].addText("");
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    myPage.addTable(myTable);

                    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                              new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                              new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);

                    //// Create table's header
                    cl = 0;
                    myTable.tableHeader.rowHeight = 10;
                    myTable.tableHeader.addColumn(150, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl++].addText("Data Início");

                    top += nTamLinPixel;

                    myTable.tableHeader.addColumn(420, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl].columnAlign = predefinedAlignment.csLeft;
                    myTable.tableHeader[cl++].addText(r["cmpDtInicioAtendimento"].ToString() != "" ? DateTime.Parse(r["cmpDtInicioAtendimento"].ToString()).ToString("dd/MM/yyyy HH:mm") : "");

                    top += nTamLinPixel;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Data Conclusão:");
                    myRow[cl++].addText(r["cmpDtConclusaoAtendimento"].ToString() != "" ? DateTime.Parse(r["cmpDtConclusaoAtendimento"].ToString()).ToString("dd/MM/yyyy HH:mm") : "");
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow.rowHeight = 50;
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Verificação do Serviço:");
                    myRow[cl++].addText(TiraCaractEspecial(r["cmpDcObservacaoConclusao"].ToString().Trim()));
                    myTable.addRow(myRow);

                    myPage.addTable(myTable);

                    top += nTamLinPixel;

                    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                              new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                              new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));
                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);

                    //// Create table's header
                    cl = 0;
                    myTable.tableHeader.rowHeight = 10;
                    myTable.tableHeader.addColumn(570, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl++].addText("Material / Serviço Utilizado");

                    myPage.addTable(myTable);

                    top += nTamLinPixel;

                    string sql = "select * from HzManutencao..vwATE_OSMaterial where cmpCoObraGrupoLista = " + cmpCoObra + " And cmpIdOS = " + r["cmpIdOS"].ToString();
                    using (DataTable tblMat = Global.GetConnection().loadDataTable(sql))
                    {
                        if (tblMat != null && tblMat.Rows.Count > 0)
                        {
                            //myTable = CabMaterial(myDoc, myPage, top);
                            myTable = CabMaterial(myPage, top);
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

                                        top += 152;
                                        myTable.coordX = leftMargin;
                                        myTable.coordY = getTop(myPage, top);

                                        myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);

                                        // Add a page
                                        myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);

                                        // Header Text                
                                        myPage.addText("Ordem de Serviço", leftMargin, getTop(myPage, 30),
                                                        myDoc.getFontReference(strFont), 15, pdfColor.Black);
                                        top = 40;
                                        myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                                                  new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                                                  new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

                                        myTable.coordX = leftMargin;
                                        myTable.coordY = getTop(myPage, top);

                                        //// Create table's header
                                        cl = 0;
                                        myTable.tableHeader.rowHeight = 10;
                                        myTable.tableHeader.addColumn(150, predefinedAlignment.csLeft);
                                        myTable.tableHeader[cl++].addText("Nº O.S.");

                                        myTable.tableHeader.addColumn(420, predefinedAlignment.csLeft);
                                        myTable.tableHeader[cl].columnAlign = predefinedAlignment.csLeft;
                                        myTable.tableHeader[cl++].addText(nNuOs.ToString());
                                        myPage.addTable(myTable);

                                        top += nTamLinPixel;

                                        myTable = CabMaterial(myPage, top);
                                    }
                                }
                            }

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
                            myTable.tableHeader[cl++].addText("Total: " + total.ToString("0,0.00"));
                            myPage.addTable(myTable);
                            top += 17;
                        }
                    }

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

                    top += 15;

                    myPage.addTable(myTable);

                    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                              new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                              new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);

                    cl = 0;
                    myTable.tableHeader.rowHeight = 80;
                    myTable.tableHeader.rowVerticalAlign = predefinedVerticalAlignment.csBottom;
                    myTable.tableHeader.addColumn(285, predefinedAlignment.csCenter);
                    myTable.tableHeader[cl].columnVerticalAlign = predefinedVerticalAlignment.csBottom;
                    myTable.tableHeader[cl++].addText("De acordo Empresa");

                    myTable.tableHeader.addColumn(285, predefinedAlignment.csCenter);
                    myTable.tableHeader[cl].columnVerticalAlign = predefinedVerticalAlignment.csBottom;
                    myTable.tableHeader[cl].addText("De acordo Gestor");

                    myPage.addTable(myTable);

                    myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);

                }
            }
        }
 
        #endregion
    }
}