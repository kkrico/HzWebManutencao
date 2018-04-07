using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using sharpPDF;
using sharpPDF.Enumerators;
using sharpPDF.Tables;
using System.Data;
using HzLibConnection.Data;
using System.Globalization;

namespace HzWebManutencao
{
    public partial class webPDF : System.Web.UI.Page
    {
        protected int getTop(pdfPage myPage, int nTop)
        {
            return myPage.height - nTop;
        }

        /// <summary>
        /// Formata a data para o padrão americano.
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private string retornaData(string d)
        {
            string retval = d;
            if (d != "" && d.Length == 10)
                retval = d.Substring(3, 2) + "/" + d.Substring(0, 2) + "/" + d.Substring(6, 4);
            return retval;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            HzConexao c = new HzConexao(@"192.168.200.170\desenv", "sa", "rona3007", "HzManutencao", "System.Data.SqlClient");

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
            using (DataTable table = c.loadDataTable("select * from HzManutencao..vwATE_OS where cmpIdOS = 1"))
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
                    myRow[cl++].addText("Data Início:");
                    myRow[cl++].addText(r["cmpDtInicioAtendimento"].ToString() != "" ? DateTime.Parse(r["cmpDtInicioAtendimento"].ToString()).ToString("dd/MM/yyyy HH:mm") : "");
                    myTable.addRow(myRow);

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Data Conclusão:");
                    myRow[cl++].addText(r["cmpDtConclusaoAtendimento"].ToString() != "" ? DateTime.Parse(r["cmpDtConclusaoAtendimento"].ToString()).ToString("dd/MM/yyyy HH:mm") : "");
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
                    myRow[cl++].addText("Verificação do Serviço:");
                    myRow[cl++].addText(r["cmpDcObservacaoConclusao"].ToString().Trim());
                    myTable.addRow(myRow);
                    top += 10 * 9;

                    myPage.addTable(myTable);

                    top += 100;
                    myPage.addText("Material Utilizado", leftMargin, this.getTop(myPage, top), myDoc.getFontReference(strFont), 10, pdfColor.Black);
                    top += 12;

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


                    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2, new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                        new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);

                    //// Create table's header
                    cl = 0;
                    myTable.tableHeader.rowHeight = 30;
                    myTable.tableHeader.rowVerticalAlign = predefinedVerticalAlignment.csTop;
                    myTable.tableHeader.addColumn(570, predefinedAlignment.csLeft);
                    myTable.tableHeader[cl].columnVerticalAlign = predefinedVerticalAlignment.csTop;
                    myTable.tableHeader[cl++].addText("Conclusão/Aceite do Solicitante");
                    top += 30;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow.rowHeight = 10;
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Data: " + DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString());
                    myTable.addRow(myRow);
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
                    myTable.tableHeader.rowHeight = 50;
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
                    string nomepdf = Server.MapPath("pdf" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf");
                    myDoc.createPDF(nomepdf);
                    System.Diagnostics.Process.Start(nomepdf);
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