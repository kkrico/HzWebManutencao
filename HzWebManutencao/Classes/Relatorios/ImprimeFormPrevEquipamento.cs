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
    public class ImprimeFormPrevEquipamento
    {
        #region Variables
        /// <summary>
        /// Nome da Obra
        /// </summary>
        public string cmpNoObra { get; set; }
        /// <summary>
        /// Descrição do Tipo de Atividade
        /// </summary>
        public string cmpDcTipoAtividade { get; set; }
        /// <summary>
        /// Descrição da Periodicidade da Preventiva
        /// </summary>
        public string cmpDcPeriodicidade { get; set; }

        #endregion

        public virtual int CabRelatorio(pdfDocument myDoc, pdfPage myPage, int top)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;

            myPage.addText("Formulário de Manutenção Preventiva de Equipamento", 200, getTop(myPage, top),
                            myDoc.getFontReference(strFont), 12, pdfColor.Black);
            top += 15;
            myPage.addText("OBRA: " + cmpNoObra, leftMargin, getTop(myPage, top),
                            myDoc.getFontReference(strFont), 10, pdfColor.Black);
            top += 15;
            myPage.addText("TIPO ATIVIDADE: " + cmpDcTipoAtividade + "                                                  " + "PERIODICIDADE : " + cmpDcPeriodicidade, leftMargin, getTop(myPage, top),
                            myDoc.getFontReference(strFont), 10, pdfColor.Black);
            top += 20;
            myPage.addText("Dados do Equipamento", 230, getTop(myPage, top),myDoc.getFontReference(strFont), 12, pdfColor.Black);

            return top += 15;
        }

        public virtual int SubCabRelatorio(pdfDocument myDoc, pdfPage myPage, int top, DataRow r)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;
            //Tamanho da lin em pixel
            int nTamLinPixel = 19;

            pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                                new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                                new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);

            //// Create table's header
            int cl = 0;
            myTable.tableHeader.rowHeight = 10;
            myTable.tableHeader.addColumn(150, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Descrição do Equipamento");

            myTable.tableHeader.addColumn(400, predefinedAlignment.csLeft);
            myTable.tableHeader[cl].columnAlign = predefinedAlignment.csLeft;
            myTable.tableHeader[cl++].addText(r["cmpDcEquipamentoObra"].ToString());

            top += nTamLinPixel;

            myPage.addTable(myTable);

            myTable =   new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                        new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                        new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);

            //// Create table's header
            cl = 0;
            myTable.tableHeader.rowHeight = 10;
            myTable.tableHeader.addColumn(150, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Código do Equipamento");

            myTable.tableHeader.addColumn(125, predefinedAlignment.csLeft);
            myTable.tableHeader[cl].columnAlign = predefinedAlignment.csLeft;
            myTable.tableHeader[cl++].addText(r["CodEquipamento"].ToString().TrimEnd());

            myTable.tableHeader.rowHeight = 10;
            myTable.tableHeader.addColumn(115, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Tipo do Equipamento");

            myTable.tableHeader.addColumn(160, predefinedAlignment.csLeft);
            myTable.tableHeader[cl].columnAlign = predefinedAlignment.csLeft;
            myTable.tableHeader[cl++].addText(r["cmpDcTipoAtividade"].ToString().TrimEnd());

            top += nTamLinPixel;
            // Create Table Row - 1ª linha
            pdfTableRow myRow = myTable.createRow();
            myRow = myTable.createRow();
            cl = 0;
            myRow[cl].columnAlign = predefinedAlignment.csLeft;
            myRow[cl++].addText("Capacidade do Equipamento");
            myRow[cl++].addText(r["DcCapacidade"].ToString().TrimEnd());

            myRow[cl].columnAlign = predefinedAlignment.csLeft;
            myRow[cl++].addText("Marca / Modelo");
            myRow[cl++].addText(r["cmpDcMarcaModeloEquipamento"].ToString().TrimEnd());
            myTable.addRow(myRow);
            top += nTamLinPixel;

            // Create Table Row - 2ª linha
            myRow = myTable.createRow();
            cl = 0;
            myRow[cl].columnAlign = predefinedAlignment.csLeft;
            myRow[cl++].addText("Número de Série");
            myRow[cl++].addText(r["cmpNuSerieEquipamento"].ToString().TrimEnd());

            myRow[cl].columnAlign = predefinedAlignment.csLeft;
            myRow[cl++].addText("Número do Patrimônio");
            myRow[cl++].addText(r["cmpNuPatrimonio"].ToString().TrimEnd());
            myTable.addRow(myRow);
            top += nTamLinPixel;

            // Create Table Row - 3ª linha
            myRow = myTable.createRow();
            cl = 0;
            myRow[cl].columnAlign = predefinedAlignment.csLeft;
            myRow[cl++].addText("Pavimento");
            myRow[cl++].addText(r["cmpDcPavimento"].ToString().TrimEnd());

            myRow[cl].columnAlign = predefinedAlignment.csLeft;
            myRow[cl++].addText("Localização");
            myRow[cl++].addText(r["cmpDcLocalEquipamento"].ToString().TrimEnd());
            myTable.addRow(myRow);
            top += nTamLinPixel;

            myPage.addTable(myTable);

            return top;
        }

        protected static pdfTable ItemAtividade(pdfDocument myDoc, pdfPage myPage, int top, DataRow r)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;

            pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                   new pdfTableStyle(myDoc.getFontReference(strFont), 12, pdfColor.Black, new pdfColor("FFFFFF")),
                   new pdfTableStyle(myDoc.getFontReference(strFont), 12, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);
            int cl = 0;

            myTable.tableHeader.addColumn(550, predefinedAlignment.csCenter);
            myTable.tableHeader.rowHeight = 20;
            myTable.tableHeader[cl++].addText("Itens da Manutenção");

            myPage.addTable(myTable);

            top += 20;

            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                   new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                   new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);
            cl = 0;

            myTable.tableHeader.addColumn(20, predefinedAlignment.csLeft);
            myTable.tableHeader.rowHeight = 10;
            myTable.tableHeader[cl++].addText("");

            myTable.tableHeader.addColumn(530, predefinedAlignment.csLeft);
            myTable.tableHeader.rowHeight = 10;
            myTable.tableHeader[cl++].addText(TiraCaractEspecial(r["cmpDcItemAtividadePreventiva"].ToString().TrimEnd()));

            return myTable;
        }

        protected static pdfTable ConclusaoRelatorio(pdfDocument myDoc, pdfPage myPage, int top)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;

            pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));
            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);
            int cl = 0;
            myTable.tableHeader.rowHeight = 15;
            myTable.tableHeader.addColumn(550, predefinedAlignment.csCenter);
            myTable.tableHeader[cl].addText("Responsável pela Execução dos Serviços");
            myPage.addTable(myTable);

            top += 15;
            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                      new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                      new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));
            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);
            cl = 0;
            myTable.tableHeader.rowHeight = 15;
            myTable.tableHeader.addColumn(275, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Nome do Técnico:");
            myTable.tableHeader.rowHeight = 15;
            myTable.tableHeader.addColumn(275, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Data: _____________________________, ____/____/" + DateTime.Now.Year);
            myPage.addTable(myTable);

            top += 15;
            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                      new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                      new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));
            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);
            cl = 0;
            myTable.tableHeader.rowHeight = 15;
            myTable.tableHeader.addColumn(275, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Hora de Entrada: ________________");
            myTable.tableHeader.rowHeight = 15;
            myTable.tableHeader.addColumn(275, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Hora da Saída: ______________");
            myPage.addTable(myTable);

            top += 30;
            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                      new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                      new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));
            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);
            cl = 0;
            myTable.tableHeader.rowHeight = 15;
            myTable.tableHeader.addColumn(550, predefinedAlignment.csCenter);
            myTable.tableHeader[cl].addText("Conclusão / Aceite do Cliente");
            myPage.addTable(myTable);

            top += 15;
            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                      new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                      new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);
            cl = 0;
            myTable.tableHeader.rowHeight = 15;
            myTable.tableHeader.addColumn(550, predefinedAlignment.csLeft);
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

        public virtual pdfDocument ImprimeAtividades(DataTable table)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;
            int pg = 1;
            int top = 20;
            int RegItemAtv;
            //Tamanho da lin em pixel
            int nTamLinPixel = 19;

            // Variaveis de controle para quebra de linha de campos
            //int QtdCaracterLinha = 50;
            //int Tamanho;
            //int QtdLin;
            //int StrInicio;
            //int StrFim;
            int cl;

            // Adiciona Página
            pdfDocument myDoc   = new pdfDocument("Horizon", "Orion");
            pdfPage myPage      = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);
            
            ImprimeFormPrevEquipamento Cab = new ImprimeFormPrevEquipamento();
            Cab.cmpNoObra           = table.Rows[0]["cmpNoObra"].ToString().TrimEnd();
            Cab.cmpDcTipoAtividade  = table.Rows[0]["cmpDcTipoAtividade"].ToString().TrimEnd();
            Cab.cmpDcPeriodicidade  = table.Rows[0]["cmpDcPeriodicidade"].ToString().TrimEnd();

            top = Cab.CabRelatorio(myDoc, myPage, top);
            top = Cab.SubCabRelatorio(myDoc, myPage, top, table.Rows[0]);
            pdfTable myTable = ItemAtividade(myDoc, myPage, top, table.Rows[0]);
            top += 40;
            RegItemAtv = 0;

            int IdEquipamento = int.Parse(table.Rows[0]["cmpIdEquipamentoObra"].ToString()); 

            // Imprimir linhas de detalhes
            pdfTableRow myRow = myTable.createRow();

            foreach (DataRow row in table.Rows)
            {
                if (IdEquipamento != int.Parse(row["cmpIdEquipamentoObra"].ToString()))
                {
                    IdEquipamento = int.Parse(row["cmpIdEquipamentoObra"].ToString());

                    myPage.addTable(myTable);

                    myTable = ConclusaoRelatorio(myDoc, myPage, top);

                    myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);
                    myPage.addTable(myTable);

                    // Adiciona uma página
                    myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);
                    pg = 1;
                    top = 30;
                    top = Cab.CabRelatorio(myDoc, myPage, top);
                    top = Cab.SubCabRelatorio(myDoc, myPage, top, row);
                    myTable = ItemAtividade(myDoc, myPage, top, row);
                    top += 40;
                 }
                else
                {
                    if (RegItemAtv == 0)
                        RegItemAtv = 1;
                    else
                    {
                        cl = 0;
                        myRow = myTable.createRow();
                        myRow.rowStyle = myTable.rowStyle;
                        myRow[cl++].addText("");
                        myRow[cl++].addText(row["cmpDcItemAtividadePreventiva"].ToString().TrimEnd());
                        myTable.addRow(myRow);
                        top += nTamLinPixel;
                    }
                }
                if (top > 1000)
                {
                    myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);
                    myPage.addTable(myTable);

                    // Adiciona uma página
                    myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);

                    top = 30;
                    top = Cab.CabRelatorio(myDoc, myPage, top);
                    top = Cab.SubCabRelatorio(myDoc, myPage, top, row);
                    myTable = ItemAtividade(myDoc, myPage, top, row);
                    top += 40;
                }
            }

            if (top < 1000)
            {
                myPage.addTable(myTable);

                myTable = ConclusaoRelatorio(myDoc, myPage, top);
                myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);
                myPage.addTable(myTable);
            }
            return myDoc;
        }
    }
}