using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using HzLibCorporativo.Funcional;
using HzlibWEB;
using HzLibManutencao;
using sharpPDF;
using sharpPDF.Enumerators;
using sharpPDF.Tables;

namespace HzWebManutencao.Relatorios
{
    public class ImprimeOSPeriodo
    {
        protected int getTop(pdfPage myPage, int nTop)
        {
            return myPage.height - nTop;
        }

        public void ImprimeOS(object Grid)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;
            int topMargin = 30;
            int bottomMargin = 30;
            int lineHeight = 20;
            int linesPerPage;

            pdfDocument myDoc = new pdfDocument("Horizon", "Orion");
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

            //pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
            //                                new pdfTableStyle(myDoc.getFontReference(strFont),
            //                                10, pdfColor.Black, new pdfColor("d9d1b3")),
            //                                new pdfTableStyle(myDoc.getFontReference(strFont),
            //                                10, pdfColor.Black, pdfColor.White),
            //                                new pdfTableStyle(myDoc.getFontReference(strFont),
            //                                10, pdfColor.Black, pdfColor.White));

            int q = 0;
            int pg = 1;
            int top = 40;
            using (DataTable table = Global.GetConnection().loadDataTable("select * from HzManutencao..vwATE_OS where cmpIdOS = " + cmpidos))
            {
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow r = table.Rows[0];

                    pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2, new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
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

                    // Create Table Row
                    pdfTableRow myRow = myTable.createRow();
                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Data Solicitação:");
                    myRow[cl++].addText(r["cmpDtAbertura"].ToString().Trim());
                    myTable.addRow(myRow);

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Obra:");
                    myRow[cl++].addText(r["cmpNoObra"].ToString().Trim() + (r["cmpNuContrato"] != null ? " -> Contrato nº " + r["cmpNuContrato"].ToString() : ""));
                    myTable.addRow(myRow);

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Soliciante:");
                    myRow[cl++].addText(r["cmpNoSolicitante"].ToString().Trim());
                    myTable.addRow(myRow);

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Local:");
                    myRow[cl++].addText(r["cmpDcLocal"].ToString().Trim());
                    myTable.addRow(myRow);

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Setor:");
                    myRow[cl++].addText(r["cmpNoSetor"].ToString().Trim());
                    myTable.addRow(myRow);

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Descrição do Serviço:");
                    myRow[cl++].addText(r["cmpDcDescricaoSolicitacao"].ToString().Trim());
                    myTable.addRow(myRow);

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow.rowHeight = 50;
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Conclusão/Aceite do Solicitante:");
                    myRow[cl++].addText("");
                    myTable.addRow(myRow);

                    myPage.addTable(myTable);

                    top = 215;
                    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2, new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                                            new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);

                    //// Create table's header
                    cl = 0;
                    myTable.tableHeader.rowHeight = 10;
                    myTable.tableHeader.addColumn(150, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl++].addText("Data Início");

                    myTable.tableHeader.addColumn(420, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl].columnAlign = predefinedAlignment.csLeft;
                    myTable.tableHeader[cl++].addText(r["cmpDtInicioAtendimento"].ToString() != "" ? DateTime.Parse(r["cmpDtInicioAtendimento"].ToString()).ToString("dd/MM/yyyy HH:mm") : "");

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Data Conclusão:");
                    myRow[cl++].addText(r["cmpDtConclusaoAtendimento"].ToString() != "" ? DateTime.Parse(r["cmpDtConclusaoAtendimento"].ToString()).ToString("dd/MM/yyyy HH:mm") : "");
                    myTable.addRow(myRow);

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow.rowHeight = 50;
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Verificação do Serviço:");
                    myRow[cl++].addText(r["cmpDcObservacaoConclusao"].ToString().Trim());
                    myTable.addRow(myRow);

                    myPage.addTable(myTable);

                    top += 70;
                    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2, new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                        new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);

                    //// Create table's header
                    cl = 0;
                    myTable.tableHeader.rowHeight = 10;
                    myTable.tableHeader.addColumn(570, predefinedAlignment.csLeft);
                    //IFormatProvider fp = new CultureInfo("pt-BR", true);
                    myTable.tableHeader[cl++].addText("Material Utilizado");
                    myPage.addTable(myTable);

                    top += 18;

                    string sql = "select * from HzManutencao..vwATE_OSMaterial where cmpIdOS = " + r["cmpIdOS"].ToString();
                    using (DataTable tblMat = Global.GetConnection().loadDataTable(sql))
                    {
                        if (tblMat != null && tblMat.Rows.Count > 0)
                        {
                            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2, new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, new pdfColor("FFFFFF")),
                                new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, pdfColor.White));

                            myTable.coordX = leftMargin;
                            //top = 210;
                            myTable.coordY = getTop(myPage, top);

                            //// Create table's header
                            cl = 0;
                            myTable.tableHeader.rowHeight = 10;
                            myTable.tableHeader.addColumn(350, predefinedAlignment.csLeft);
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
                            top += 15;

                            float total = 0f;
                            foreach (DataRow row in tblMat.Rows)
                            {
                                // Create Table Row
                                myRow = myTable.createRow();
                                cl = 0;
                                myRow = myTable.createRow();
                                myRow[cl].columnAlign = predefinedAlignment.csLeft;
                                myRow[cl++].addText(row["DcMaterial"].ToString().Trim());
                                myRow[cl++].addText(row["cmpDcUnidade"].ToString().Trim());
                                myRow[cl++].addText(decimal.Parse(row["cmpQtMaterial"].ToString()).ToString("0,0.00"));
                                myRow[cl++].addText(decimal.Parse(row["cmpVlMaterial"].ToString()).ToString("0,0.00"));
                                myRow[cl++].addText(decimal.Parse(row["cmpVlSubTotal"].ToString()).ToString("0,0.00"));
                                total += float.Parse(row["cmpVlSubTotal"].ToString());
                                myTable.addRow(myRow);
                                top += 12;
                            }
                            myPage.addTable(myTable);
                            top += 3;

                            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2, new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                                new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

                            myTable.coordX = leftMargin;
                            myTable.coordY = getTop(myPage, top);

                            //// Create table's header
                            cl = 0;
                            myTable.tableHeader.rowHeight = 10;
                            myTable.tableHeader.addColumn(570, predefinedAlignment.csRight);
                            //IFormatProvider fp = new CultureInfo("pt-BR", true);
                            myTable.tableHeader[cl++].addText("Total: " + total.ToString("0,0.00"));
                            top += 17;
                            myPage.addTable(myTable);
                        }
                    }

                    top = 410;

                    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2, new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                        new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);

                    cl = 0;
                    myRow = myTable.createRow();
                    myTable.tableHeader.rowHeight = 10;
                    myTable.tableHeader.addColumn(570, predefinedAlignment.csLeft);
                    //IFormatProvider fp = new CultureInfo("pt-BR", true);
                    myTable.tableHeader[cl++].addText("Data: " + DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString());

                    top += 15;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow.rowHeight = 10;
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Satisfeito com o serviço: " + "__Sim         __Não");
                    myTable.addRow(myRow);
                    top += 15;

                    myPage.addTable(myTable);

                    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2, new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
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

                    myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 5, pdfColor.Black);
                    //string nomepdf = "c:\\Horizon\\Ocr\\Relatorios\\" + "Relatorio_" + (txtPlacaRelatorio.Text != "" ? txtPlacaRelatorio.Text : "Todas") + "_" + retornaDataRelatorio(txtDataInicial.Text) + "_" + retornaDataRelatorio(txtDataFinal.Text) + ".pdf";
                    string filename = "pdf" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf";
                    string nomepdf = Server.MapPath(filename);
                    myDoc.createPDF(nomepdf);
                    Response.Redirect(filename, false);
                }
            }
            ++q;

            //if (q == 30)
            //{
            //    myPage.addText("Pág. " + pg++, leftMargin, 10,
            //                    myDoc.getFontReference(strFont), 5, pdfColor.Black);
            //    myPage.addTable(myTable);
            //    myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);
            //    q = 0;
            //    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 5,
            //                                    new pdfTableStyle(myDoc.getFontReference(strFont),
            //                                    10, pdfColor.Black, new pdfColor("d9d1b3")),
            //                                    new pdfTableStyle(myDoc.getFontReference(strFont),
            //                                    10, pdfColor.Black, pdfColor.White),
            //                                    new pdfTableStyle(myDoc.getFontReference(strFont),
            //                                    10, pdfColor.Black, pdfColor.White));
            //    myTable.coordX = leftMargin;
            //    myTable.coordY = getTop(myPage, 100);

            //    // Create table's header
            //    myTable.tableHeader.rowHeight = 10;
            //    c = 0;
            //    myTable.tableHeader.addColumn(100, predefinedAlignment.csCenter);
            //    myTable.tableHeader[c++].addText("Placa");
            //    if (rdbAcesso.IsChecked.Value)
            //    {
            //        myTable.tableHeader.addColumn(200, predefinedAlignment.csLeft);
            //        myTable.tableHeader[c++].addText("Nome");
            //    }
            //    myTable.tableHeader.addColumn(150, predefinedAlignment.csCenter);
            //    myTable.tableHeader[c++].addText("Data");
            //    if (rdbAcesso.IsChecked.Value)
            //    {
            //        myTable.tableHeader.addColumn(100, predefinedAlignment.csCenter);
            //        myTable.tableHeader[c].addText("Cancela");
            //    }

            //    myPage.addText("Procuradoria Geral da República", leftMargin, getTop(myPage, 30),
            //                    myDoc.getFontReference(strFont), 20, pdfColor.Black);

            //    // Body Text
            //    myPage.addText("Relatório dos Acessos à Garagem", leftMargin, getTop(myPage, 50),
            //                   myDoc.getFontReference(strFont), 10, pdfColor.Black);
            //    myPage.addText("Relatório emitido em: " + DateTime.Now.ToShortDateString(), leftMargin, getTop(myPage, 70),
            //                   myDoc.getFontReference(strFont), 10, pdfColor.Black);
            //}
        }

    }
}