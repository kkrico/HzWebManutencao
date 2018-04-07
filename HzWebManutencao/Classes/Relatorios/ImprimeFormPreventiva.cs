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
    public class ImprimeFormPreventiva
    {
        protected static void CabRelatorio(pdfDocument myDoc, pdfPage myPage, int top, string NomeObra, string Atividade, string Periodicidade)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;

            myPage.addText("Formulário de Manutenção Preventiva", 200, getTop(myPage, top),
                            myDoc.getFontReference(strFont), 12, pdfColor.Black);
            top += 15;
            myPage.addText("OBRA: " + NomeObra.TrimEnd(), leftMargin, getTop(myPage, top),
                            myDoc.getFontReference(strFont), 10, pdfColor.Black);
            top += 15;
            myPage.addText("TIPO ATIVIDADE: " + Atividade.TrimEnd() + "                                    " + "PERIODICIDADE : " + Periodicidade.TrimEnd(), leftMargin, getTop(myPage, top),
                            myDoc.getFontReference(strFont), 10, pdfColor.Black);
        }

        protected static void SubCabRelatorio(pdfDocument myDoc, pdfPage myPage, int top, string CoObra, string cmpCoPreventiva)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;

            pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 7, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont), 6, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);
            int cl = 0;
            myTable.tableHeader.addColumn(200, predefinedAlignment.csCenter);
            myTable.tableHeader.rowHeight = 20;
            myTable.tableHeader[cl++].addText("Atividades da Manutenção Preventiva");
            myPage.addTable(myTable);

            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 7, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont), 5, pdfColor.Black, pdfColor.White));

            top = 70;
            myTable.coordX = 220;
            myTable.coordY = getTop(myPage, top);
            cl = 0;
            myTable.tableHeader.addColumn(352, predefinedAlignment.csCenter);
            myTable.tableHeader.rowHeight = 10;
            myTable.tableHeader[cl++].addText("Pavimentos");
            myPage.addTable(myTable);

            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 5, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont), 3, pdfColor.Black, pdfColor.White));

            top += 10;
            myTable.coordX = 220;
            myTable.coordY = getTop(myPage, top);
            cl = 0;
            myTable.tableHeader.rowHeight = 10;
            DataTable table = tblPreventivaAtividade.RetornaFormPreventivaPivot(Global.GetConnection(), cmpCoPreventiva, CoObra);
            for (int i = 2; i <= 23; ++i)
            {
                myTable.tableHeader.addColumn(16, predefinedAlignment.csCenter);
                if (i <= table.Columns.Count - 1)
                    myTable.tableHeader[cl++].addText(table.Columns[i].ColumnName.ToString());
                else
                    myTable.tableHeader[cl++].addText("");
            }

            myPage.addTable(myTable);
        }

       protected static void ImprimeGrupo(pdfDocument myDoc, pdfPage myPage, int top, string GrupoAtividade)
       {
            String strFont = "Helvetica";
            int leftMargin = 30;

            pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 7, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont), 5, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);
            int cl = 0;
            myTable.tableHeader.addColumn(542, predefinedAlignment.csLeft);
            myTable.tableHeader.rowHeight = 10;
            myTable.tableHeader[cl++].addText(GrupoAtividade);
            myPage.addTable(myTable);
       }

        protected static pdfTable ConclusaoRelatorio(pdfDocument myDoc, pdfPage myPage, int top)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;

            pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, pdfColor.White));
            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);
            int cl = 0;
            myTable.tableHeader.rowHeight = 20;
            myTable.tableHeader.addColumn(542, predefinedAlignment.csCenter);
            myTable.tableHeader[cl].addText("Responsável pela Execução dos Serviços");
            myPage.addTable(myTable);

            top += 15;
            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                      new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, new pdfColor("FFFFFF")),
                      new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, pdfColor.White));
            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);
            cl = 0;
            myTable.tableHeader.rowHeight = 15;
            myTable.tableHeader.addColumn(271, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Nome do Técnico:");
            myTable.tableHeader.rowHeight = 15;
            myTable.tableHeader.addColumn(271, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Data: ____________________________________, ____/____/" + DateTime.Now.Year);
            myPage.addTable(myTable);

            top += 15;
            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                      new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, new pdfColor("FFFFFF")),
                      new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, pdfColor.White));
            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);
            cl = 0;
            myTable.tableHeader.rowHeight = 15;
            myTable.tableHeader.addColumn(271, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Hora de Entrada: ________________");
            myTable.tableHeader.rowHeight = 15;
            myTable.tableHeader.addColumn(271, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Hora da Saída: ______________");
            myPage.addTable(myTable);

            top += 30;
            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                      new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, new pdfColor("FFFFFF")),
                      new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, pdfColor.White));
            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);
            cl = 0;
            myTable.tableHeader.rowHeight = 20;
            myTable.tableHeader.addColumn(542, predefinedAlignment.csCenter);
            myTable.tableHeader[cl].addText("Conclusão / Aceite do Cliente");
            myPage.addTable(myTable);

            top += 15;
            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                      new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, new pdfColor("FFFFFF")),
                      new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);
            cl = 0;
            myTable.tableHeader.rowHeight = 15;
            myTable.tableHeader.addColumn(542, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Nome do Cliente:                                                                                                    Data:");

            pdfTableRow myRow;

            cl = 0;
            myRow = myTable.createRow();
            myRow.rowStyle = myTable.rowStyle;
            myRow[cl].addText("Satisfeito com o serviço? _________ Sim    _________ Não");
            myTable.addRow(myRow);
            myRow = myTable.createRow();
            myRow[cl].addText("Comentários: ");
            myTable.addRow(myRow);
            myRow = myTable.createRow();
            myRow[cl].addText("");
            myTable.addRow(myRow);
            myRow = myTable.createRow();
            myRow[cl].addText("");
            myTable.addRow(myRow);
            myRow = myTable.createRow();
            myRow[cl].addText("");
            myTable.addRow(myRow);
            myRow = myTable.createRow();
            myRow[cl].addText("");
            myTable.addRow(myRow);
            myRow = myTable.createRow();
            myRow[cl].addText("");
            myTable.addRow(myRow);
            myRow = myTable.createRow();
            myRow[cl].addText("");
            myTable.addRow(myRow);
            myRow = myTable.createRow();
            myRow[cl].addText("");
            myTable.addRow(myRow);

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

        public static pdfDocument ImprimeAtividades(DataTable table, string NomeObra, string TipoAtividade, string Periodicidade)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;
            int pg = 1;
            int top = 20;
            //Tamanho da lin em pixel
            int nTamLinPixel = 19;

            // Variaveis de controle para quebra de linha de campos
            int QtdCaracterLinha = 50;
            int Tamanho;
            int QtdLin;
            int StrInicio;
            int StrFim;
            int cl;

            // Adiciona Página
            pdfDocument myDoc = new pdfDocument("Horizon", "Orion");
            pdfPage myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);

            CabRelatorio(myDoc, myPage, top, NomeObra, TipoAtividade, Periodicidade);
            top += 50;

            SubCabRelatorio(myDoc, myPage, top, table.Rows[0]["cmpCoObra"].ToString(), table.Rows[0]["cmpCoPreventiva"].ToString());
            top += 20;

            DataTable table2 = tblPreventivaAtividade.RetornaFormPreventivaPivot(Global.GetConnection(), table.Rows[0]["cmpCoPreventiva"].ToString(), table.Rows[0]["cmpCoObra"].ToString());

            pdfTable myTable;

            string GrupoAtividade = "";

            foreach (DataRow row in table2.Rows)
            {
                row["cmpDcGrupoAtividade"] = TiraCaractEspecial(row["cmpDcGrupoAtividade"].ToString().TrimEnd());

                if (row["cmpDcGrupoAtividade"].ToString() != GrupoAtividade)
                {
                    GrupoAtividade = row["cmpDcGrupoAtividade"].ToString();
                    ImprimeGrupo(myDoc, myPage, top, GrupoAtividade);
                    top += 10;
                }

                // Imprimir Itens do grupo atividade
                myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                                    new pdfTableStyle(myDoc.getFontReference(strFont), 5, pdfColor.Black, new pdfColor("FFFFFF")),
                                    new pdfTableStyle(myDoc.getFontReference(strFont), 5, pdfColor.Black, pdfColor.White));

                myTable.coordX = leftMargin;
                myTable.coordY = getTop(myPage, top);
                cl = 0;
                myTable.tableHeader.addColumn(190, predefinedAlignment.csLeft);
                myTable.tableHeader.rowHeight = 10;
                myTable.tableHeader[cl].addText(TiraCaractEspecial(row["cmpDcItemAtividadePreventiva"].ToString()));

                for (int i = 2; i <= 23; ++i)
                {
                    myTable.tableHeader.addColumn(16, predefinedAlignment.csCenter);
                    myTable.tableHeader[cl++].addText("");
                }

                myPage.addTable(myTable);
                top += 10;
            }

            top += 20;
            myTable = ConclusaoRelatorio(myDoc, myPage, top);

            myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);
            myPage.addTable(myTable);

            return myDoc;
        }

    }
}