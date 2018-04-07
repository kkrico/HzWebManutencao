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
    public class ImprimirMaterialGrupoObraPeriodo
    {
        #region Variables
        /// <summary>
        /// Documento PDF
        /// </summary>
        public pdfDocument myDoc { get; set; }
        /// <summary>
        /// Página PDF
        /// </summary>
        public pdfPage myPage { get; set; }
        /// <summary>
        /// Nome da Obra
        /// </summary>
        public string NomeObra { get; set; }
        /// <summary>
        /// Data inicial do consumo do material
        /// </summary>
        public string DtInicial { get; set; }
        /// <summary>
        /// Data final do consumo do material
        /// </summary>
        public string DtFinal { get; set; }
        /// <summary>
        /// Caminho da imagem da logo da Obra
        /// </summary>
        public string EnderecoLogoObra { get; set; }
        /// <summary>
        /// Caminho da imagem da logo da Orion
        /// </summary>
        public string EnderecoLogoOrion { get; set; }

        string TipoMaterial;

        String strFont = "Helvetica";
        int leftMargin = 30;
        int pg = 1;
        int top = 40;
        //Tamanho da lin em pixel
        int nTamLinPixel = 12;

        // Variaveis de controle para quebra de linha de campos
        int QtdCaracterLinha = 80;
        int Tamanho;
        int QtdLin;
        int StrInicio;
        int StrFim;
        int cl;
        #endregion

        #region Base Functions

        protected void CabRelatorio()
        {
            String strFont = "Helvetica";
            int leftMargin = 30;

            //Bitmap image1 = (Bitmap)Image.FromFile("~/Imagens/logo_IPEN.bmp", true);

            //myDoc.addImageReference(EnderecoLogoOrion, "logo1");
            //myPage.addImage(myDoc.getImageReference("logo1"), leftMargin, getTop(myPage, top));

            //if (EnderecoLogoObra != "")
            //{
            //    myDoc.addImageReference(EnderecoLogoObra, "logo2");
            //    myPage.addImage(myDoc.getImageReference("logo2"), 530, getTop(myPage, top));
            //}

            //top += 10;
            myPage.addText("LISTA DE MATERIAIS UTILIZADOS AGRUPADOS", 200, getTop(myPage, top), myDoc.getFontReference(strFont), 12, pdfColor.Black);
            top += 20;
            myPage.addText("PERÍODO: " + DtInicial + " a " + DtFinal, 230, getTop(myPage, top), myDoc.getFontReference(strFont), 12, pdfColor.Black);
            //top += 20;
            //myPage.addText("OBRA: " + NomeObra.ToString().TrimEnd(), 30, getTop(myPage, top), myDoc.getFontReference(strFont), 12, pdfColor.Black);

            top += 10;
            myPage.addText("__________________________________________________________________________________", leftMargin, getTop(myPage, top),
                            myDoc.getFontReference(strFont), 12, pdfColor.Black);

            top += 20;
        }

        protected void SubCabRelatorio()
        {
            String strFont = "Helvetica";
            int leftMargin = 30;

            pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 9, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);
            int cl = 0;
            myTable.tableHeader.addColumn(550, predefinedAlignment.csLeft);
            myTable.tableHeader.rowHeight = 20;
            myTable.tableHeader[cl++].addText(TipoMaterial);
            myPage.addTable(myTable);

            top += 24;
        }

         protected pdfTable ItensMaterial()
        {
            String strFont = "Helvetica";
            int leftMargin = 30;

            pdfTable myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2,
                               new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, new pdfColor("FFFFFF")),
                               new pdfTableStyle(myDoc.getFontReference(strFont), 7, pdfColor.Black, pdfColor.White));

            myTable.coordX = leftMargin;
            myTable.coordY = getTop(myPage, top);

            int cl = 0;
            myTable.tableHeader.rowHeight = 20;

            myTable.tableHeader.addColumn(40, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("ITEM");

            myTable.tableHeader.addColumn(310, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("MATERIAL");

            myTable.tableHeader.addColumn(30, predefinedAlignment.csLeft);
            myTable.tableHeader[cl++].addText("UNID.");

            myTable.tableHeader.addColumn(50, predefinedAlignment.csRight);
            myTable.tableHeader[cl++].addText("PREÇO");

            myTable.tableHeader.addColumn(50, predefinedAlignment.csRight);
            myTable.tableHeader[cl++].addText("QTD.UTIL.");

            myTable.tableHeader.addColumn(70, predefinedAlignment.csRight);
            myTable.tableHeader[cl++].addText("TOT.MATERIAL");

            top += 20;

            return myTable;
        }

        protected static int getTop(pdfPage myPage, int nTop)
        {
            return myPage.height - nTop;
        }

        protected pdfTable QuebraPagina(pdfTable myTable)
        {
            myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);
            myPage.addTable(myTable);

            // Adiciona uma página
            myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);
            top = 40;
            CabRelatorio();
            SubCabRelatorio();
            myTable = ItensMaterial();
            return myTable;
        }

        protected static string TiraCaractEspecial(string Campo)
        {
            Campo = Campo.Replace("\r", string.Empty);
            Campo = Campo.Replace("\n", string.Empty);

            return Campo;
        }

        #endregion

        public void ImprimeMaterial(DataTable table)
        {
            pdfTable myTable;
            pdfTableRow myRow;

            TipoMaterial = table.Rows[0]["cmpDcTipoMaterial"].ToString();

            CabRelatorio();

            SubCabRelatorio();

            myTable = ItensMaterial();

            foreach (DataRow row in table.Rows)
            {
                if (row["cmpDcTipoMaterial"].ToString() != TipoMaterial)
                {
                    TipoMaterial = row["cmpDcTipoMaterial"].ToString();
                    if ((top + 52) > 746) // 52 é a soma do cabeçalho + subcabeçalho
                    {
                        myTable = QuebraPagina(myTable);
                    }
                    else
                    {
                        myPage.addTable(myTable);
                        SubCabRelatorio();
                        myTable = ItensMaterial();
                    }
                }

                row["cmpDcMaterialGrupo"] = TiraCaractEspecial(row["cmpDcMaterialGrupo"].ToString());

                Tamanho = row["cmpDcMaterialGrupo"].ToString().Length;

                QtdLin = (Tamanho % QtdCaracterLinha == 0 ? Tamanho / QtdCaracterLinha : (Tamanho / QtdCaracterLinha) + 1);
                StrInicio = 0;

                if ((top + (QtdLin * nTamLinPixel)) > 770)
                {
                    myTable = QuebraPagina(myTable);
                }

                cl = 0;
                myRow = myTable.createRow();
                myRow.rowStyle = myTable.rowStyle;

                myRow[cl++].addText(row["cmpDcItem"].ToString());

                for (int Linha = 1; Linha <= QtdLin; Linha++)
                {
                    StrFim = Linha == QtdLin ? Tamanho - StrInicio : QtdCaracterLinha;
                    if (Linha == 1)
                    {
                        myRow[cl++].addText(row["cmpDcMaterialGrupo"].ToString().Substring(StrInicio, StrFim).Trim());
                        myRow[cl++].addText(row["cmpDcUnidade"].ToString());
                        myRow[cl++].addText(decimal.Parse(row["cmpVlPrecoUnitario"].ToString()).ToString("0,0.00"));
                        myRow[cl++].addText(row["TotQtMaterialUtilizado"].ToString() == "" ? "" : row["TotQtMaterialUtilizado"].ToString());
                        myRow[cl++].addText(decimal.Parse(row["TotVlMaterialUtilizado"].ToString()).ToString("0,0.00"));
                    }
                    else
                    {
                        cl = 0;
                        myRow = myTable.createRow();
                        myRow.rowStyle = myTable.rowStyle;
                        myRow[cl++].addText("");
                        myRow[cl++].addText(row["cmpDcMaterialGrupo"].ToString().Substring(StrInicio, StrFim).Trim());
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

            if (top < 770)
            {
                myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);
                myPage.addTable(myTable);
            }

            return;
        }
    }
}