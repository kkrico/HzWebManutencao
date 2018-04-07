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
    public class ImprimirPlanoManutencao
    {

        #region Cabeçalhos do Relatório
        protected static void CabRelatorio(pdfDocument myDoc, pdfPage myPage, int top, string DadosObra)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;

            myPage.addText("Plano de Manutenção Preventiva", leftMargin, getTop(myPage, 30), myDoc.getFontReference(strFont), 12, pdfColor.Black);

            pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 11, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont), 11, pdfColor.Black, pdfColor.White));
            top += 10;

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);

            int cl = 0;
            myTable.tableHeader.rowHeight = 25;
            myTable.tableHeader.addColumn(550, predefinedAlignment.csLeft);
            myTable.tableHeader[cl].addText("Obra: " + DadosObra.ToString());

            myPage.addTable(myTable);
        }

        protected static pdfTable SubCabRelatorio(pdfDocument myDoc, pdfPage myPage, int top, string Atividade, string GrupoAtv, string Periodicidade)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;

            pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);

            // Create Table Row
            pdfTableRow myRow;

            int cl = 0;

            //// Create table's header
            myTable.tableHeader.rowHeight = 20;
            myTable.tableHeader.addColumn(100, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Atividade");
            myTable.tableHeader.addColumn(450, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText(Atividade.TrimEnd());
            myRow = myTable.createRow();
            myRow.rowStyle = myTable.rowStyle;
            top += 20;
            cl = 0;
            myRow = myTable.createRow();
            myRow.rowStyle = myTable.rowStyle;
            myRow[cl].columnAlign = predefinedAlignment.csLeft;
            myRow[cl++].addText("Grupo Atividade");
            myRow[cl++].addText(TiraCaractEspecial(GrupoAtv.TrimEnd()));
            myTable.addRow(myRow);
            top += 20;
            cl = 0;
            myRow = myTable.createRow();
            myRow.rowStyle = myTable.rowStyle;
            myRow[cl].columnAlign = predefinedAlignment.csLeft;
            myRow[cl++].addText("Periodicidade");
            myRow[cl++].addText(Periodicidade.TrimEnd());
            myTable.addRow(myRow);
            top += 10;
            myPage.addTable(myTable);

            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                                new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                                new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));
            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);
            cl = 0;
            myTable.tableHeader.rowHeight = 20;
            myTable.tableHeader.addColumn(550, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("ITENS DE ATIVIDADE");

            return myTable;
        }
        #endregion

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

        public static pdfDocument ImprimePlano(DataTable table, string NoObra)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;
            int pg = 1;
            int top = 40;

            //Tamanho da lin em pixel
            int nTamLinPixel = 20;

            // Variaveis de controle para quebra de linha de campos
            int QtdCaracterLinha = 115;
            int Tamanho;
            int QtdLin;
            int StrInicio;
            int StrFim;
            int cl;

            // Adiciona Página
            pdfDocument myDoc = new pdfDocument("Horizon", "Orion");
            pdfPage myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);

            CabRelatorio(myDoc, myPage, top, NoObra);
            top += 40;

            // Create Table
            pdfTable myTable;

            // Create Table Row
            pdfTableRow myRow;

            string TipoAtividade  = table.Rows[0]["cmpCoTipoAtividade"].ToString();
            string GrupoAtividade = table.Rows[0]["cmpCoGrupoAtividade"].ToString();
            string Periodicidade  = table.Rows[0]["cmpCoPeriodicidade"].ToString();

            myTable = SubCabRelatorio(myDoc, myPage, top, table.Rows[0]["cmpDcTipoAtividade"].ToString(), table.Rows[0]["cmpDcGrupoAtividade"].ToString(), table.Rows[0]["cmpDcPeriodicidade"].ToString());
            top += 90;

            int i = 1;

            foreach (DataRow row in table.Rows)
            {
                cl = 0;
                if (TipoAtividade  != row["cmpCoTipoAtividade"].ToString()  ||
                    GrupoAtividade != row["cmpCoGrupoAtividade"].ToString() ||
                    Periodicidade  != row["cmpCoPeriodicidade"].ToString())
                {
                    TipoAtividade   = row["cmpCoTipoAtividade"].ToString();
                    GrupoAtividade  = row["cmpCoGrupoAtividade"].ToString();
                    Periodicidade   = row["cmpCoPeriodicidade"].ToString();
                    
                    myPage.addTable(myTable);

                    //if (pg == 2 && i == 0)
                    //    break;

                    //top += 15;
                    
                    myTable = SubCabRelatorio(myDoc, myPage, top, row["cmpDcTipoAtividade"].ToString(), row["cmpDcGrupoAtividade"].ToString(), row["cmpDcPeriodicidade"].ToString());
                    top += 90 ;
                    cl = 0;
                    i++;
                }

                myRow = myTable.createRow();
                myRow.rowStyle = myTable.rowStyle;

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
                            myRow[cl++].addText(row["cmpDcItemAtividadePreventiva"].ToString().Substring(StrInicio, StrFim).Trim());
                        }
                        myTable.addRow(myRow);
                        StrInicio += QtdCaracterLinha;
                        top += nTamLinPixel;
                        //if (top % 3 == 0)
                        //    top -= 3;
                    }
                }

                if (top > 750)       //(top > 1230)
                {
                    myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);
                    myPage.addTable(myTable);

                    // Adiciona uma página
                    myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);
                    top = 40;
                    //CabRelatorio(myDoc, myPage, top, NoObra);
                    //top += 40;

                    myTable = SubCabRelatorio(myDoc, myPage, top, row["cmpDcTipoAtividade"].ToString(), row["cmpDcGrupoAtividade"].ToString(), row["cmpDcPeriodicidade"].ToString());
                    top = +90;

                    if (pg == 2)
                        i = 0;
                }
                //if (pg == 3)
                //    break;
            }

            return myDoc;
        }

    }
}