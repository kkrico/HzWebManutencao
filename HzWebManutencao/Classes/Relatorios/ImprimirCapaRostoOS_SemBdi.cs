using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Linq;
using sharpPDF;
using sharpPDF.Enumerators;
using sharpPDF.Tables;
using sharpPDF.Collections;
using HzlibWEB;
using HzLibConnection.Sql;
using HzLibManutencao;
using HzLibCorporativo;

namespace HzWebManutencao
{
    public class FechamentoMes
    {
        #region Totaliza Material por Ordem de Serviço
        public class TotMaterial
        {
            public decimal Valor  { get; set; }

            public TotMaterial()
            {
                this.Valor = 0;
            }

            public void Adicionar(DataTable table)
            {
                DataTable tbl;
                foreach (DataRow row in table.Rows)
                {
                    tbl = tblOSMaterial.RetornarTotalMatOS(Global.GetConnection(), row["cmpIdOs"].ToString());

                    if (tbl.Rows[0]["TotalMaterial"].ToString() != "")
                    {
                        this.Valor += decimal.Parse(tbl.Rows[0]["TotalMaterial"].ToString());
                    }
                }
            }
        }
        #endregion

        #region Cabeçalhos do Relatório
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

            myTable.tableHeader.addColumn(200, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Detalhamento Serviço");

            myTable.tableHeader.addColumn(100, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("Local");

            myTable.tableHeader.addColumn(40, predefinedAlignment.csCenter);
            myTable.tableHeader[cl++].addText("Abertura");

            myTable.tableHeader.addColumn(40, predefinedAlignment.csCenter);
            myTable.tableHeader[cl++].addText("Conclusão");

            myTable.tableHeader.addColumn(50, predefinedAlignment.csRight);
            myTable.tableHeader[cl++].addText("Total Material");

            return myTable;
        }
        #endregion

        #region Funções gerais
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

        #region Monta Relatório
        public static pdfDocument ImprimeCapaRostoOS(DataTable table, string DataInicial, string DataFinal)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;
            int pg = 1;
            int top = 40;
            //Tamanho da lin em pixel
            int nTamLinPixel = 19;

            // Variaveis de controle para quebra de linha de campos
            int QtdCaracterLinha = 50;
            int Tamanho;
            int QtdLin;
            int StrInicio;
            int StrFim;
            int cl;

            DataTable DtTotMatOs;
            decimal TotMatOs = 0;

            TotMaterial totalMat = new TotMaterial();
            totalMat.Adicionar(table);

            decimal ValorBdiLucro   = totalMat.Valor * decimal.Parse(table.Rows[0]["cmpPeBDILucro"].ToString())/100;
            decimal ValorBdiTributo = totalMat.Valor * decimal.Parse(table.Rows[0]["cmpPeBDITributos"].ToString())/100;

            decimal TotalMaterialLucroTributo = totalMat.Valor + ValorBdiLucro + ValorBdiTributo;

            // Adiciona Página
            pdfDocument myDoc = new pdfDocument("Horizon", "Orion");
            pdfPage myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);
            
            string DadosObra = (table.Rows[0]["cmpNoObra"].ToString().Trim() + (table.Rows[0]["cmpNuContrato"] != null ? " -> Contrato nº " + table.Rows[0]["cmpNuContrato"].ToString() : ""));
            CabRelatorio(myDoc, myPage, top, DadosObra, DataInicial, DataFinal);
            top += 30;

            #region Resultado Financeiro
            //Imprime Total Geral de Material
            pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));
            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);

            cl = 0;
            myTable.tableHeader.rowHeight = 10;
            myTable.tableHeader.addColumn(400, predefinedAlignment.csRight);
            myTable.tableHeader[cl++].addText("Total do Material R$ ");

            myTable.tableHeader.addColumn(150, predefinedAlignment.csRight);
            myTable.tableHeader[cl++].addText(totalMat.Valor.ToString("0,0.00"));
            top += 20;

            // Create Table Row
            pdfTableRow myRow = myTable.createRow();
            cl = 0;
            myRow = myTable.createRow();
            myRow[cl].columnAlign = predefinedAlignment.csRight;
            myRow[cl++].addText("Lucro + Despesas Administrativas (" + table.Rows[0]["cmpPeBDILucro"].ToString() + "%)");
            myRow[cl++].addText(ValorBdiLucro.ToString("0,0.00"));
            myTable.addRow(myRow);

            top += 20;
            cl = 0;
            myRow = myTable.createRow();
            myRow[cl].columnAlign = predefinedAlignment.csRight;
            myRow[cl++].addText("Tributos (" + table.Rows[0]["cmpPeBDITributos"].ToString() + "%)");
            myRow[cl++].addText(ValorBdiTributo.ToString("0,0.00"));
            myTable.addRow(myRow);

            top += 20;
            cl = 0;
            myRow = myTable.createRow();
            myRow[cl].columnAlign = predefinedAlignment.csRight;
            myRow[cl++].addText("Total (Material + Lucro + Tributos)");
            myRow[cl++].addText(TotalMaterialLucroTributo.ToString("0,0.00"));
            myTable.addRow(myRow);

            myPage.addTable(myTable);
            #endregion

            top += 10;
            myTable = SubCabRelatorio(myDoc, myPage, top);
            top += 10;

            // Imprimir linhas de detalhes
            myRow = myTable.createRow();

            foreach (DataRow row in table.Rows)
            {
                DtTotMatOs = tblOSMaterial.RetornarTotalMatOS(Global.GetConnection(), row["cmpIdOs"].ToString());
                TotMatOs = DtTotMatOs.Rows[0]["TotalMaterial"].ToString() != "" ? decimal.Parse(DtTotMatOs.Rows[0]["TotalMaterial"].ToString()) : 0;

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
                            myRow[cl++].addText(row["cmpDcLocal"].ToString());
                            myRow[cl++].addText(row["cmpDtAbertura"].ToString().Substring(0, 10));
                            myRow[cl++].addText(row["cmpDtConclusaoAtendimento"].ToString() == "" ? "" : row["cmpDtConclusaoAtendimento"].ToString().Substring(0, 10));
                            myRow[cl++].addText(TotMatOs.ToString("0,0.00"));
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
                    myRow[cl++].addText(TotMatOs.ToString("0,0.00"));
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
                //Imprime Total Geral de Material
                myRow = myTable.createRow();
                myRow.rowStyle = myTable.rowStyle;
                cl = 0;
                myRow[cl++].addText("");
                myRow[cl++].addText("");
                myRow[cl++].addText("");
                myRow[cl++].addText("");
                myRow[cl++].addText("");
                myRow[cl++].addText("Total ==>");
                myRow[cl++].addText(totalMat.Valor.ToString("0,0.00"));
                myTable.addRow(myRow);

                myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);
                myPage.addTable(myTable);
            }

            return myDoc;
        }

        #endregion

    }
}