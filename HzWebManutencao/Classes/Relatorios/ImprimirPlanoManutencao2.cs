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
    public class ImprimirPlanoManutencao2
    {
        #region Base Functions
        protected static void CabRelatorio(pdfDocument myDoc, pdfPage myPage, int top, string DadosObra)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;

            myPage.addText("PLANO MANUTENÇÃO PREVENTIVA", leftMargin, getTop(myPage, 30),
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
            myTable.tableHeader.addColumn(80, predefinedAlignment.csCenter);
            myTable.tableHeader[cl++].addText("Atividade");

            myTable.tableHeader.addColumn(140, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Grupo Atividade");

            myTable.tableHeader.addColumn(50, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Periodicidade");

            myTable.tableHeader.addColumn(300, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Itens de Atividade");

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
            return Campo;
        }

        #endregion

        public static pdfDocument ImprimePlano(DataTable table, string NoObra)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;
            int pg = 1;
            int top = 40;
            //Tamanho da lin em pixel
            int nTamLinPixel = 19;

            // Variaveis de controle para quebra de linha de campos
            int QtdCaracterLinha = 100;
            int Tamanho;
            int QtdLin;
            int StrInicio;
            int StrFim;
            int cl;

            // Adiciona Página
            pdfDocument myDoc = new pdfDocument("Horizon", "Orion");
            pdfPage myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);

            string DadosObra = NoObra;
            CabRelatorio(myDoc, myPage, top, DadosObra);
            top += 30;

            pdfTable myTable = SubCabRelatorio(myDoc, myPage, top);
            top += 10;

            string TipoAtividade = "";
            string GrupoAtividade = "";
            string Periodicidade = "";

            // Imprimir linhas de detalhes
            pdfTableRow myRow = myTable.createRow();

            try
            {

                foreach (DataRow row in table.Rows)
                {
                    cl = 0;
                    myRow = myTable.createRow();
                    myRow.rowStyle = myTable.rowStyle;

                    if (TipoAtividade != row["cmpCoTipoAtividade"].ToString())
                    {
                        row["cmpDcTipoAtividade"] = TiraCaractEspecial(row["cmpDcTipoAtividade"].ToString());
                        myRow[cl++].addText(row["cmpDcTipoAtividade"].ToString().TrimEnd());
                        TipoAtividade = row["cmpCoTipoAtividade"].ToString();
                    }
                    else
                        myRow[cl++].addText("");

                    if (GrupoAtividade != row["cmpCoGrupoAtividade"].ToString())
                    {
                        row["cmpDcGrupoAtividade"] = TiraCaractEspecial(row["cmpDcGrupoAtividade"].ToString());
                        myRow[cl++].addText(row["cmpDcGrupoAtividade"].ToString().TrimEnd());
                        GrupoAtividade = row["cmpCoGrupoAtividade"].ToString();
                    }
                    else
                        myRow[cl++].addText("");

                    if (Periodicidade != row["cmpCoPeriodicidade"].ToString())
                    {
                        myRow[cl++].addText(row["cmpDcPeriodicidade"].ToString());
                        Periodicidade = row["cmpCoPeriodicidade"].ToString();
                    }
                    else
                        myRow[cl++].addText("");

                    row["cmpDcItemAtividadePreventiva"] = TiraCaractEspecial(row["cmpDcItemAtividadePreventiva"].ToString());

                    Tamanho = row["cmpDcItemAtividadePreventiva"].ToString().Length;

                    if (Tamanho > 0)
                    {
                        QtdLin = (Tamanho % QtdCaracterLinha == 0 ? Tamanho / QtdCaracterLinha : (Tamanho / QtdCaracterLinha) + 1);
                        StrInicio = 0;

                        for (int Linha = 1; Linha <= QtdLin; Linha++)
                        {
                            StrFim = Linha == QtdLin ? Tamanho - StrInicio : QtdCaracterLinha;
                            if (Linha == 1)
                            {
                                myRow[cl++].addText(row["cmpDcItemAtividadePreventiva"].ToString().Substring(StrInicio, StrFim).Trim());
                            }
                            else
                            {
                                cl = 0;
                                myRow = myTable.createRow();
                                myRow.rowStyle = myTable.rowStyle;
                                myRow[cl++].addText("");
                                myRow[cl++].addText("");
                                myRow[cl++].addText("");
                                myRow[cl++].addText(row["cmpDcItemAtividadePreventiva"].ToString().Substring(StrInicio, StrFim).Trim());
                            }
                            myTable.addRow(myRow);
                            StrInicio += QtdCaracterLinha;
                            top += nTamLinPixel;
                        }
                    }
                    //else
                    //{
                    //    myRow[cl++].addText(row["cmpDcDescricaoSolicitacao"].ToString().TrimEnd());
                    //    myRow[cl++].addText(row["cmpDcLocal"].ToString());
                    //    myRow[cl++].addText(row["cmpDtAbertura"].ToString().Substring(0, 10));
                    //    myRow[cl++].addText(row["cmpDtConclusaoAtendimento"].ToString() == "" ? "" : row["cmpDtConclusaoAtendimento"].ToString().Substring(0, 10));
                    //    myTable.addRow(myRow);
                    //    top += nTamLinPixel;
                    //}

                    if (top > 1250)
                    {
                        myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);
                        myPage.addTable(myTable);

                        // Adiciona uma página
                        myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);
                        top = 30;
                        CabRelatorio(myDoc, myPage, top, DadosObra);
                        top += 30;
                        myTable = SubCabRelatorio(myDoc, myPage, top);
                        top = +10;
                    }
                }

                if (top < 1250)
                {
                    myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);
                    myPage.addTable(myTable);
                }

            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }

            return myDoc;

        }
    }
}