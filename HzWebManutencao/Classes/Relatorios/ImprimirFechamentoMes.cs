using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Linq;
using HzLibConnection.Sql;
using HzlibWEB;
using HzLibManutencao;
using sharpPDF;
using sharpPDF.Enumerators;
using sharpPDF.Tables;
using sharpPDF.Collections;

namespace HzWebManutencao
{
    public class ImprimirFechamentoMes
    {
        #region Base Functions
        protected static void CabRelatorio(pdfDocument myDoc, pdfPage myPage, int top, string DadosObra, string DataInicial, string DataFinal)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;

            myPage.addText("Fechamento do Mês - Atendimentos Realizados no Período de : " + DataInicial + " a " + DataFinal, leftMargin, getTop(myPage, 30),
                            myDoc.getFontReference(strFont), 12, pdfColor.Black);

            pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 9, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, pdfColor.White));
            top += 5;

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);

            int cl = 0;
            myTable.tableHeader.rowHeight = 20;
            myTable.tableHeader.addColumn(550, predefinedAlignment.csLeft);
            myTable.tableHeader[cl].addText("Obra: " + DadosObra.ToString());

            myPage.addTable(myTable);
        }

        protected static pdfTable SubCabRelatorio(pdfDocument myDoc, pdfPage myPage, int top)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;

            pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 7, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont), 6, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);

            int cl = 0;
            myTable.tableHeader.rowHeight = 10;
            myTable.tableHeader.addColumn(40, predefinedAlignment.csCenter);
            myTable.tableHeader[cl++].addText("Nº O.S");

            myTable.tableHeader.addColumn(80, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Solicitante");

            myTable.tableHeader.addColumn(250, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Detalhamento Serviço");

            myTable.tableHeader.addColumn(100, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Local");

            myTable.tableHeader.addColumn(40, predefinedAlignment.csCenter);
            myTable.tableHeader[cl++].addText("Abertura");

            myTable.tableHeader.addColumn(40, predefinedAlignment.csCenter);
            myTable.tableHeader[cl++].addText("Conclusão");

            return myTable;
        }

        protected static int getTop(pdfPage myPage, int nTop)
        {
            return myPage.height - nTop;
        }

        protected static string TiraCaractEspecial(string Campo)
        {
            Campo = Campo.Replace("\r", string.Empty);
            Campo = Campo.Replace("\n", string.Empty);
            Campo = Campo.Replace("²", string.Empty);

            return Campo;
        }

        #endregion

        public static pdfDocument ImprimeFechamento(DataTable table, string DataInicial, string DataFinal)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;
            int pg = 1;
            int top = 40;
            //Tamanho da lin em pixel
            int nTamLinPixel = 19;

            // Variaveis de controle para quebra de linha de campos
            int QtdCaracterLinha = 80;
            int Tamanho;
            int QtdLin;
            int StrInicio;
            int StrFim;
            int cl;

            // Adiciona Página
            pdfDocument myDoc = new pdfDocument("Horizon", "Orion");
            pdfPage myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);

            string DadosObra = (table.Rows[0]["cmpNoObra"].ToString().Trim() + (table.Rows[0]["cmpNuContrato"] != null ? " -> Contrato nº " + table.Rows[0]["cmpNuContrato"].ToString() : ""));
            CabRelatorio(myDoc, myPage, top, DadosObra, DataInicial, DataFinal);
            top += 30;

            pdfTable myTable = SubCabRelatorio(myDoc, myPage, top);
            top += 10;

            // Imprimir linhas de detalhes
            pdfTableRow myRow = myTable.createRow();

            int cont = 0;
            foreach (DataRow row in table.Rows)
            {
                cl = 0;
                myRow = myTable.createRow();
                myRow.rowStyle = myTable.rowStyle;

                myRow[cl++].addText(row["cmpNuOs"].ToString());
                myRow[cl++].addText(row["cmpNoSolicitante"].ToString().TrimEnd());

                row["cmpDcObservacoes"] = TiraCaractEspecial(row["cmpDcObservacoes"].ToString().ToUpper());

                Tamanho = row["cmpDcObservacoes"].ToString().Length;

                if (Tamanho > 0)
                {
                    QtdLin = (Tamanho % QtdCaracterLinha == 0 ? Tamanho / QtdCaracterLinha : (Tamanho / QtdCaracterLinha) + 1);
                    StrInicio = 0;

                    for (int Linha = 1; Linha <= QtdLin; Linha++)
                    {
                        StrFim = Linha == QtdLin ? Tamanho - StrInicio : QtdCaracterLinha;
                        if (Linha == 1)
                        {
                            myRow[cl++].addText(row["cmpDcObservacoes"].ToString().Substring(StrInicio, StrFim).Trim());
                            if (row["cmpDcLocal"] != null)
                            {
                                cont++;
                                //System.Diagnostics.Debug.WriteLine(cont.ToString());
                                myRow[cl++].addText(TiraCaractEspecial(row["cmpDcLocal"].ToString()));
                            }
                            else { myRow[cl++].addText(" "); }
                            myRow[cl++].addText(row["cmpDtAbertura"].ToString().Substring(0, 10));
                            myRow[cl++].addText(row["cmpDtConclusaoAtendimento"].ToString() == "" ? "" : row["cmpDtConclusaoAtendimento"].ToString().Substring(0, 10));
                        }
                        else
                        {
                            cl = 0;
                            myRow = myTable.createRow();
                            myRow.rowStyle = myTable.rowStyle;
                            myRow[cl++].addText("");
                            myRow[cl++].addText("");
                            myRow[cl++].addText(row["cmpDcObservacoes"].ToString().Substring(StrInicio, StrFim).Trim());
                            myRow[cl++].addText("");
                            myRow[cl++].addText("");
                            myRow[cl++].addText("");
                        }
                        myTable.addRow(myRow);
                        StrInicio += QtdCaracterLinha;
                        top += nTamLinPixel;
                    }
                }
                else
                {
                    myRow[cl++].addText(row["cmpDcDescricaoSolicitacao"].ToString().TrimEnd());
                    myRow[cl++].addText(row["cmpDcLocal"].ToString());
                    myRow[cl++].addText(row["cmpDtAbertura"].ToString().Substring(0, 10));
                    myRow[cl++].addText(row["cmpDtConclusaoAtendimento"].ToString() == "" ? "" : row["cmpDtConclusaoAtendimento"].ToString().Substring(0, 10));
                    myTable.addRow(myRow);
                    top += nTamLinPixel;
                }

                if (top > 1230)
                {
                    myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);
                    myPage.addTable(myTable);

                    // Adiciona uma página
                    myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);
                    top = 30;
                    CabRelatorio(myDoc, myPage, top, DadosObra, DataInicial, DataFinal);
                    top += 30;
                    myTable = SubCabRelatorio(myDoc, myPage, top);
                    top = +10;
                }
            }

            if (top < 1230)
            {
                myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);
                myPage.addTable(myTable);
            }

            return myDoc;
        }

    }
}