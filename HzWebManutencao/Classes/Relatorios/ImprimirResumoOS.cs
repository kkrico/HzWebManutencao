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

namespace HzWebManutencao
{
    public class TotalOS
    {
        public int Eventual     { get; set; }
        public int ForaEscopo   { get; set; }
        public int Externa      { get; set; }
        public int Preventiva   { get; set; }
        public int Interno      { get; set; }

        public TotalOS()
        {
            this.Eventual   = 0;
            this.ForaEscopo = 0;
            this.Externa    = 0;
            this.Preventiva = 0;
            this.Interno    = 0;
        }

        public void Adicionar(string cmporigemos)
        {
            switch (cmporigemos.Trim().ToLower())
            {
                case "eventuais":
                    this.Eventual++;
                    break;
                case "interno":
                    this.Interno++;
                    break;
                case "preventiva":
                    this.Preventiva++;
                    break;
                case "externo":
                    this.Externa++;
                    break;
                case "fora do escopo":
                    this.ForaEscopo++;
                    break;
            }
        }
    }

    public class ImprimirResumoOS
    {
        #region Variables
        /// <summary>
        /// Código da Ordem de Serviço
        /// </summary>
        //public string cmpIdOS { get; set; }
        /// <summary>
        /// Código da Obra
        /// </summary>
        //public string cmpCoObra { get; set; }
        /// <summary>
        /// Documento PDF
        /// </summary>
        //public pdfDocument myDoc { get; set; }

        #endregion

        #region Base Functions

        protected static pdfTable CabRelatorio(pdfDocument myDoc, pdfPage myPage, int top, string DadosObra, string DataInicial, string DataFinal)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;

            // Cabeçalho Relatório                
            myPage.addText("Atendimentos Realizados  -  Período : " + DataInicial + " a " + DataFinal, leftMargin, getTop(myPage, 30),
                            myDoc.getFontReference(strFont), 15, pdfColor.Black);

            pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 11, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont), 11, pdfColor.Black, pdfColor.White));
            top += 20;

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);

            int cl = 0;
            myTable.tableHeader.rowHeight = 30;
            myTable.tableHeader.addColumn(550, predefinedAlignment.csLeft);
            myTable.tableHeader[cl].addText("Obra: " + DadosObra.ToString());
            myPage.addTable(myTable);

            top += 30;
            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);

            cl = 0;
            myTable.tableHeader.rowHeight = 10;
            myTable.tableHeader.addColumn(450, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Descrição da Solicitação");

            myTable.tableHeader.addColumn(100, predefinedAlignment.csCenter);
            myTable.tableHeader[cl++].addText("Data da Conclusão");
            top = +15;

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

        public static pdfDocument ImprimeResumoOS(DataTable table, string DataInicial, string DataFinal)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;
            int pg = 1;
            int top = 40;
            TotalOS totalos = new TotalOS();
            //Tamanho da lin em pixel
            int nTamLinPixel = 19; 

            // Variaveis de controle para quebra de linha de campos
            int QtdCaracterLinha = 120;
            int Tamanho;
            int QtdLin;
            int StrInicio;
            int StrFim;
            int cl; 

            // Adiciona Página
            pdfDocument myDoc   = new pdfDocument("Horizon", "Orion");
            pdfPage myPage      = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);

            string DadosObra = (table.Rows[0]["cmpNoObra"].ToString().Trim() + (table.Rows[0]["cmpNuContrato"] != null ? " -> Contrato nº " + table.Rows[0]["cmpNuContrato"].ToString() : ""));
            pdfTable myTable = CabRelatorio(myDoc, myPage, top, DadosObra, DataInicial, DataFinal);

            // Imprimir linhas de detalhes
            pdfTableRow myRow = myTable.createRow();

            foreach (DataRow row in table.Rows)
            {
                totalos.Adicionar(row["cmpDcOrigemOS"].ToString());
                cl = 0;
                row["cmpDcObservacoes"] = TiraCaractEspecial(row["cmpDcObservacoes"].ToString());

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
                            myRow = myTable.createRow();
                            myRow.rowStyle = myTable.rowStyle;
                            myRow[cl++].addText(row["cmpDcObservacoes"].ToString().Substring(StrInicio, StrFim).Trim());
                            myRow[cl++].addText(row["cmpDtConclusaoAtendimento"].ToString());
                        }
                        else
                        {
                            cl = 0;
                            myRow = myTable.createRow();
                            myRow.rowStyle = myTable.rowStyle;
                            myRow[cl++].addText(row["cmpDcObservacoes"].ToString().Substring(StrInicio, StrFim).Trim());
                            myRow[cl++].addText("");
                        }
                        myTable.addRow(myRow);
                        StrInicio += QtdCaracterLinha;
                        top += nTamLinPixel;
                    }
                }
                else
                {
                    myRow = myTable.createRow();
                    myRow.rowStyle = myTable.rowStyle;
                    myRow[cl++].addText(row["cmpDcDescricaoSolicitacao"].ToString().TrimEnd());
                    myRow[cl++].addText(row["cmpDtConclusaoAtendimento"].ToString());
                    myTable.addRow(myRow);
                    top += nTamLinPixel;
                }
                if (top > 980)
                {
                    myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);
                    myPage.addTable(myTable);

                    // Adiciona uma página
                    myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);
                    top = 40;
                    myTable = CabRelatorio(myDoc, myPage, top, DadosObra, DataInicial, DataFinal);
                    top =+ 15;
                }
            }

            if (top < 980)
            {
                myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);
                myPage.addTable(myTable);

                // Adiciona uma página
                myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);
                top = 40;
                myTable = CabRelatorio(myDoc, myPage, top, DadosObra, DataInicial, DataFinal);
                top =+ 120;
            }

            // Imprimir Resumo dos atendimentos                
            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                      new pdfTableStyle(myDoc.getFontReference(strFont), 12, pdfColor.Black, new pdfColor("FFFFFF")),
                      new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);

            cl = 0;
            myTable.tableHeader.rowHeight = 30;
            myTable.tableHeader.addColumn(300, predefinedAlignment.csCenter);
            myTable.tableHeader[cl++].addText("RESUMO DAS ATIVIDADES REALIZADAS");
            myPage.addTable(myTable);

            top += 30;

            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                        new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                        new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);

            cl = 0;
            myTable.tableHeader.rowHeight = 12;
            myTable.tableHeader.addColumn(200, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Tipo de Atendimento");

            myTable.tableHeader.addColumn(100, predefinedAlignment.csRight);
            myTable.tableHeader[cl++].addText("Quantidade");

            cl = 0;
            myRow = myTable.createRow();
            myRow.rowStyle = myTable.rowStyle;
            myRow[cl++].addText("Eventuais");
            myRow[cl++].addText(totalos.Eventual.ToString());
            myTable.addRow(myRow);

            cl = 0;
            myRow = myTable.createRow();
            myRow.rowStyle = myTable.rowStyle;
            myRow[cl++].addText("Interno");
            myRow[cl++].addText(totalos.Interno.ToString());
            myTable.addRow(myRow);

            cl = 0;
            myRow = myTable.createRow();
            myRow.rowStyle = myTable.rowStyle;
            myRow[cl++].addText("Preventiva");
            myRow[cl++].addText(totalos.Preventiva.ToString());
            myTable.addRow(myRow);

            cl = 0;
            myRow = myTable.createRow();
            myRow.rowStyle = myTable.rowStyle;
            myRow[cl++].addText("Externo");
            myRow[cl++].addText(totalos.Externa.ToString());
            myTable.addRow(myRow);

            cl = 0;
            myRow = myTable.createRow();
            myRow.rowStyle = myTable.rowStyle;
            myRow[cl++].addText("Fora de Escopo");
            myRow[cl++].addText(totalos.ForaEscopo.ToString());
            myTable.addRow(myRow);

            myPage.addTable(myTable);

            top += 150;

            // Imprimir indíce da eficiência da manutenção                
            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                      new pdfTableStyle(myDoc.getFontReference(strFont), 12, pdfColor.Black, new pdfColor("FFFFFF")),
                      new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);

            cl = 0;
            myTable.tableHeader.rowHeight = 30;
            myTable.tableHeader.addColumn(200, predefinedAlignment.csCenter);
            myTable.tableHeader[cl++].addText("EFICIÊNCIA DA MANUTENÇÃO");
            myPage.addTable(myTable);

            top += 30;

            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                        new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                        new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);

            float Soma1 = totalos.Preventiva + totalos.Interno;
            float Soma2 = totalos.Preventiva + totalos.Interno + totalos.Externa;

            float IEM = (Soma1 / Soma2) * 100;

            cl = 0;
            myTable.tableHeader.rowHeight = 12;
            myTable.tableHeader.addColumn(200, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Resultado ==> " + string.Format("{0:0.0000}%", IEM));
                //string.Format("{0:N2}%", IEM.ToString()));

            myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);
            myPage.addTable(myTable);
 
            return myDoc;
        }
           
        
        #endregion

    }
}