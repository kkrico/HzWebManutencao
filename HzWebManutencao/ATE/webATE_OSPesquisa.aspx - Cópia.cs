using System;
using System.Collections.Generic;
using System.Linq;
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

namespace HzWebManutencao.ATE
{
    public class STOS
    {
        public string cmpIdOS { get; set; }
        public string state { get; set; }
        public string cmpCoObra { get; set; }

        public STOS(string cmpidos, string state, string cmpcobora)
        {
            this.cmpIdOS = cmpidos;
            this.state = state;
            this.cmpCoObra = cmpcobora;
        }
    }

    public partial class webATE_OSPesquisa : System.Web.UI.Page
    {
        #region Functions
        
        /// <summary>
        /// Carrega as combos.
        /// </summary>
        private void load()
        {
            try
            {
                txtDataInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");

                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoContratante";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoContratante;
                ls.Add(lc);

                using (DataTable table = tblOrigemOS.Get(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbOrigemOS, table, "cmpDcOrigemOS", "cmpCoOrigemOS", "cmpDcOrigemOS", "TODAS", true);
                }

            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Carrega as combos.
        /// </summary>
        private void loadObra()
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoLocal";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoLocal;
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "cmpCoUsuario";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoUsuario;
                ls.Add(lc);

                using (DataTable table = tblObraUsuario.Get(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbObra, table, "cmpNoObra", "cmpCoObra", "cmpNoObra", true);
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Pesquisa OS.
        /// </summary>
        private DataTable pesquisaOs()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpCoObra";
            lc.TipoCampo = TipoCampo.Numero;
            lc.Sinal = SinalPesquisa.Igual;
            lc.ValorCampo = cmbObra.SelectedValue;
            ls.Add(lc);

            lc = new ListCampo();
            lc.NomeCampo = "cmpDtAbertura";
            lc.TipoCampo = TipoCampo.Data;
            lc.Sinal = SinalPesquisa.MaiorIgual;
            lc.ValorCampo = txtDataInicial.Text + " 00:00:00";
            ls.Add(lc);

            lc = new ListCampo();
            lc.NomeCampo = "cmpDtAbertura";
            lc.TipoCampo = TipoCampo.Data;
            lc.Sinal = SinalPesquisa.MenorIgual;
            lc.ValorCampo = txtDataFinal.Text + " 23:59:59";
            ls.Add(lc);

            lc = new ListCampo();
            lc.NomeCampo = "cmpStOS";
            lc.TipoCampo = TipoCampo.String;
            lc.Sinal = SinalPesquisa.Diferente;
            lc.ValorCampo = "D";
            ls.Add(lc);

            if (cmbOrigemOS.SelectedValue != "0")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpCoOrigemOS";
                lc.TipoCampo = TipoCampo.Numero;
                lc.Sinal = SinalPesquisa.Igual;
                lc.ValorCampo = cmbOrigemOS.SelectedValue;
                ls.Add(lc);
            }

            if (rdbState.SelectedValue != "T")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpStOS";
                lc.TipoCampo = TipoCampo.String;
                lc.Sinal = SinalPesquisa.Igual;
                lc.ValorCampo = rdbState.SelectedValue;
                ls.Add(lc);
            }

            return tblOS.Get(Global.GetConnection(), ls);
        }

        private string TiraCaractEspecial(string Campo)
        {
            Campo = Campo.Replace("\r", string.Empty);
            Campo = Campo.Replace("\n", string.Empty);
            return Campo;
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.load();
                this.loadObra();
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            DataTable table = pesquisaOs();
            grdOS.DataSource = table;
            grdOS.DataBind();

            if (table != null && table.Rows.Count > 0)
            {
                ViewState["cmpIdOS"] = table.Rows[0]["cmpIdOS"].ToString();
                btnAprove.Visible = rdbState.SelectedValue == "G" ? true : false;
                btnImprimirTodasOS.Visible = true;
            }
        }

        protected int getTop(pdfPage myPage, int nTop)
        {
            return myPage.height - nTop;
        }

        private void ImprimeOS(string cmpCoObra, string cmpidos)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;
            //int topMargin = 30;
            //int bottomMargin = 30;
            //int lineHeight = 20;
            //int linesPerPage;

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

            int pg = 1;
            int top = 40;
            int nTamLinPixel = 19; //Tamanho da lin em pixel
            // Variaveis de controle para quebra de linha de campos
            int QtdCaracterLinha;
            int Tamanho;
            int QtdLin;
            int StrInicio;
            int StrFim;

            using (DataTable table = Global.GetConnection().loadDataTable("select * from HzManutencao..vwATE_OS where cmpCoObra = " + cmpCoObra + " And cmpIdOs = " + cmpidos))
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

                    top += nTamLinPixel;

                    // Create Table Row
                    pdfTableRow myRow = myTable.createRow();
                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Data Solicitação:");
                    myRow[cl++].addText(r["cmpDtAbertura"].ToString().Trim());
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Obra:");
                    myRow[cl++].addText(r["cmpNoObra"].ToString().Trim() + (r["cmpNuContrato"] != null ? " -> Contrato nº " + r["cmpNuContrato"].ToString() : ""));
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Soliciante:");
                    myRow[cl++].addText(r["cmpNoSolicitante"].ToString().Trim());
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Local / Sala:");
                    myRow[cl++].addText(r["cmpDcLocal"].ToString().Trim());
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Setor:");
                    myRow[cl++].addText(r["cmpNoSetor"].ToString().Trim());
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Descrição do Serviço:");
                    myRow[cl++].addText(r["cmpDcDescricaoSolicitacao"].ToString().Trim());
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Detalhamento do Serviço:");

                    r["cmpDcObservacoes"] = TiraCaractEspecial(r["cmpDcObservacoes"].ToString());

                    Tamanho = r["cmpDcObservacoes"].ToString().Length;
                    QtdLin = (Tamanho % 69 == 0 ? Tamanho / 69 : (Tamanho / 69) + 1);
                    StrInicio = 0;

                    for (int Linha = 1; Linha <= QtdLin; Linha++)
                    {
                        if (Linha > 1)
                        {
                            cl = 0;
                            myRow = myTable.createRow();
                            myRow[cl].columnAlign = predefinedAlignment.csLeft;
                            myRow[cl++].addText("");
                        }
                        StrFim = Linha == QtdLin ? Tamanho - StrInicio : 69;
                        myRow[cl++].addText(r["cmpDcObservacoes"].ToString().Substring(StrInicio, StrFim).Trim());
                        myTable.addRow(myRow);
                        StrInicio += 69;
                    }

                    top += nTamLinPixel * QtdLin - 1;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow.rowHeight = 50;
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Conclusão/Aceite do Solicitante:");
                    myRow[cl++].addText("");
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    myPage.addTable(myTable);

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
                    myTable.tableHeader[cl++].addText("Material / Serviço Utilizado");
                    myPage.addTable(myTable);

                    top += 18;

                    string sql = "select * from HzManutencao..vwATE_OSMaterial where cmpIdOS = " + r["cmpIdOS"].ToString();
                    using (DataTable tblMat = Global.GetConnection().loadDataTable(sql))
                    {
                        if (tblMat != null && tblMat.Rows.Count > 0)
                        {
                            myTable =   new pdfTable(myDoc, 1, new pdfColor("000000"), 2, 
                                        new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, new pdfColor("FFFFFF")),
                                        new pdfTableStyle(myDoc.getFontReference(strFont), 7, pdfColor.Black, pdfColor.White));

                            myTable.coordX = leftMargin;
                            myTable.coordY = getTop(myPage, top);

                            //// Create table's header
                            cl = 0;
                            myTable.tableHeader.rowHeight = 10;
                            myTable.tableHeader.addColumn(30, predefinedAlignment.csLeft);
                            myTable.tableHeader[cl++].addText("Item");

                            myTable.tableHeader.rowHeight = 10;
                            myTable.tableHeader.addColumn(320, predefinedAlignment.csLeft);
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

                            QtdCaracterLinha = 73;
                            float total = 0f;

                            foreach (DataRow row in tblMat.Rows)
                            {
                                // Create Table Row
                                cl = 0;
                                myRow = myTable.createRow();
                                myRow.rowStyle = myTable.rowStyle;
                                myRow[cl].columnAlign = predefinedAlignment.csLeft;
                                myRow[cl++].addText(row["cmpDcItem"].ToString().Trim());

                                row["cmpDcMaterial"] = TiraCaractEspecial(row["cmpDcMaterial"].ToString());

                                Tamanho = row["cmpDcMaterial"].ToString().Length;
                                QtdLin = (Tamanho % QtdCaracterLinha == 0 ? Tamanho / QtdCaracterLinha : (Tamanho / QtdCaracterLinha) + 1);
                                StrInicio = 0;

                                for (int Linha = 1; Linha <= QtdLin; Linha++)
                                {
                                    StrFim = Linha == QtdLin ? Tamanho - StrInicio : QtdCaracterLinha;
                                    if (Linha == 1)
                                    {
                                        myRow[cl++].addText(row["cmpDcMaterial"].ToString().Substring(StrInicio, StrFim).Trim());
                                        myRow[cl++].addText(row["cmpDcUnidade"].ToString().Trim());
                                        myRow[cl++].addText(decimal.Parse(row["cmpQtMaterial"].ToString()).ToString("0,0.00"));
                                        myRow[cl++].addText(decimal.Parse(row["cmpVlMaterial"].ToString()).ToString("0,0.00"));
                                        myRow[cl++].addText(decimal.Parse(row["cmpVlSubTotal"].ToString()).ToString("0,0.00"));
                                        total += float.Parse(row["cmpVlSubTotal"].ToString());
                                        myTable.addRow(myRow);
                                        StrInicio += QtdCaracterLinha;
                                        top += 11;
                                    }
                                    else
                                    {
                                        cl = 0;
                                        myRow = myTable.createRow();
                                        myRow.rowStyle = myTable.rowStyle;
                                        myRow[cl].columnAlign = predefinedAlignment.csLeft;
                                        myRow[cl++].addText("");
                                        myRow[cl++].addText(row["cmpDcMaterial"].ToString().Substring(StrInicio, StrFim).Trim());
                                        myRow[cl++].addText("");
                                        myRow[cl++].addText("");
                                        myRow[cl++].addText("");
                                        myRow[cl++].addText("");
                                        myTable.addRow(myRow);
                                        StrInicio += QtdCaracterLinha;
                                        top += 11;
                                    }
                                }
                            }

                            myPage.addTable(myTable);
                            //top += 3;

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

                    cl = 0;
                    myRow = myTable.createRow();
                    myTable.tableHeader.rowHeight = 10;
                    myTable.tableHeader.addColumn(570, predefinedAlignment.csLeft);
                    //IFormatProvider fp = new CultureInfo("pt-BR", true);
                    myTable.tableHeader[cl++].addText("Satisfeito com o serviço: " + "__Sim         __Não");
                   
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

                    myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black); 
                    string filename = "pdf" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf";
                    string nomepdf = Server.MapPath("~/Relatorios/" + filename);
                    myDoc.createPDF(nomepdf);

                    //Response.Redirect("~/Relatorios/webRel_OS.aspx?NomeRel=" + filename, false);

                    // Impressão ambiente desenvolvimento local
                    Response.Write("<script language='javascript'>"
                                  + "window.open('" + @"http://localhost:51055/Relatorios/" + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105')"
                                  + "</script>");

                    // Impressão ambiente produção ambiente interno orion
                    //Response.Write("<script language='javascript'>"
                    //                  + "window.open('" + @"http://172.10.10.2/HzWEBManutencao/Relatorios/" + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105')"
                    //                  + "</script>");

                    // Impressão ambiente produção ambiente externo orion
                    //Response.Write("<script language='javascript'>"
                    //                  + "window.open('" + @"http://201.22.148.250/HzWEBManutencao/Relatorios/" + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105')"
                    //                  + "</script>");
                }
        }
        //++q;

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

        protected void ImprimeTodasOS(pdfDocument myDoc)
        {
            String strFont = "Helvetica";
            int leftMargin = 30;
            int pg = 1;

            DataTable table = pesquisaOs();

            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow lin in table.Rows)
                {
                    // Add a page
                    pdfPage myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);

                    // Header Text                
                    myPage.addText("Ordem de Serviço", leftMargin, getTop(myPage, 30),
                                    myDoc.getFontReference(strFont), 15, pdfColor.Black);

                    int top = 40;
                    //Tamanho da lin em pixel
                    int nTamLinPixel = 19; 
                    // Variaveis de controle para quebra de linha de campos
                    int QtdCaracterLinha;
                    int Tamanho;
                    int QtdLin;
                    int StrInicio;
                    int StrFim;

                    /*
                    * Imprimir Dados da Ordem de Serviço
                    */

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
                    myTable.tableHeader[cl++].addText(lin["cmpNuOS"].ToString());

                    top += nTamLinPixel;

                    // Create Table Row
                    pdfTableRow myRow = myTable.createRow();
                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Data Solicitação:");
                    myRow[cl++].addText(lin["cmpDtAbertura"].ToString().Trim());
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Obra:");
                    myRow[cl++].addText(lin["cmpNoObra"].ToString().Trim() + (lin["cmpNuContrato"] != null ? " -> Contrato nº " + lin["cmpNuContrato"].ToString() : ""));
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Soliciante:");
                    myRow[cl++].addText(lin["cmpNoSolicitante"].ToString().Trim());
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Local / Sala:");
                    myRow[cl++].addText(lin["cmpDcLocal"].ToString().Trim());
                    myTable.addRow(myRow);

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Setor:");
                    myRow[cl++].addText(lin["cmpNoSetor"].ToString().Trim());
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Descrição do Serviço:");
                    myRow[cl++].addText((lin["cmpDcDescricaoSolicitacao"].ToString().Trim()));
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Detalhamento do Serviço:");

                    lin["cmpDcObservacoes"] = TiraCaractEspecial(lin["cmpDcObservacoes"].ToString());

                    Tamanho = lin["cmpDcObservacoes"].ToString().Length;
                    QtdLin = (Tamanho % 69 == 0 ? Tamanho / 69 : (Tamanho / 69) + 1);
                    StrInicio = 0;

                    for (int Linha = 1; Linha <= QtdLin; Linha++)
                    {
                        if (Linha > 1)
                        {
                            cl = 0;
                            myRow = myTable.createRow();
                            myRow[cl].columnAlign = predefinedAlignment.csLeft;
                            myRow[cl++].addText("");
                        }
                        StrFim = Linha == QtdLin ? Tamanho - StrInicio : 69;
                        myRow[cl++].addText(lin["cmpDcObservacoes"].ToString().Substring(StrInicio, StrFim).Trim());
                        myTable.addRow(myRow);
                        StrInicio += 69;
                    }

                    top += nTamLinPixel * QtdLin - 1;

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow.rowHeight = 50;
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Conclusão/Aceite do Solicitante:");
                    myRow[cl++].addText("");
                    myTable.addRow(myRow);

                    top += nTamLinPixel;

                    myPage.addTable(myTable);

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
                    myTable.tableHeader[cl++].addText(lin["cmpDtInicioAtendimento"].ToString() != "" ? DateTime.Parse(lin["cmpDtInicioAtendimento"].ToString()).ToString("dd/MM/yyyy HH:mm") : "");

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Data Conclusão:");
                    myRow[cl++].addText(lin["cmpDtConclusaoAtendimento"].ToString() != "" ? DateTime.Parse(lin["cmpDtConclusaoAtendimento"].ToString()).ToString("dd/MM/yyyy HH:mm") : "");
                    myTable.addRow(myRow);

                    cl = 0;
                    myRow = myTable.createRow();
                    myRow.rowHeight = 50;
                    myRow[cl].columnAlign = predefinedAlignment.csLeft;
                    myRow[cl++].addText("Verificação do Serviço:");
                    myRow[cl++].addText(lin["cmpDcObservacaoConclusao"].ToString().Trim());
                    myTable.addRow(myRow);

                    myPage.addTable(myTable);

                    top += 90;

                    /*
                    * Imprimir Itens de Material da Ordem de Serviço
                    */
                    
                    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2, new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                        new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);

                    //// Create table's header
                    cl = 0;
                    myTable.tableHeader.rowHeight = 10;
                    myTable.tableHeader.addColumn(570, predefinedAlignment.csLeft);
                    //IFormatProvider fp = new CultureInfo("pt-BR", true);
                    myTable.tableHeader[cl++].addText("Material / Serviço Utilizado");
                    myPage.addTable(myTable);

                    top += 18;

                    string sql = "select * from HzManutencao..vwATE_OSMaterial where cmpIdOS = " + lin["cmpIdOS"].ToString();
                    using (DataTable tblMat = Global.GetConnection().loadDataTable(sql))
                    {
                        if (tblMat != null && tblMat.Rows.Count > 0)
                        {
                            myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2, new pdfTableStyle(myDoc.getFontReference(strFont), 8, pdfColor.Black, new pdfColor("FFFFFF")),
                                new pdfTableStyle(myDoc.getFontReference(strFont), 7, pdfColor.Black, pdfColor.White));

                            myTable.coordX = leftMargin;
                            myTable.coordY = getTop(myPage, top);

                            //// Create table's header
                            cl = 0;
                            myTable.tableHeader.rowHeight = 10;
                            myTable.tableHeader.addColumn(30, predefinedAlignment.csLeft);
                            myTable.tableHeader[cl++].addText("Item");

                            myTable.tableHeader.rowHeight = 10;
                            myTable.tableHeader.addColumn(320, predefinedAlignment.csLeft);
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

                            QtdCaracterLinha = 73;
                            float total = 0f;

                            foreach (DataRow row in tblMat.Rows)
                            {
                                // Create Table Row
                                cl = 0;
                                myRow = myTable.createRow();
                                myRow.rowStyle = myTable.rowStyle;
                                myRow[cl].columnAlign = predefinedAlignment.csLeft;
                                myRow[cl++].addText(row["cmpDcItem"].ToString().Trim());

                                row["cmpDcMaterial"] = TiraCaractEspecial(row["cmpDcMaterial"].ToString());

                                Tamanho = row["cmpDcMaterial"].ToString().Length;
                                QtdLin = (Tamanho % QtdCaracterLinha == 0 ? Tamanho / QtdCaracterLinha : (Tamanho / QtdCaracterLinha) + 1);
                                StrInicio = 0;
                                StrFim = 0;

                                for (int Linha = 1; Linha <= QtdLin; Linha++)
                                {
                                    StrFim = Linha == QtdLin ? Tamanho - StrInicio : QtdCaracterLinha;
                                    if (Linha == 1)
                                    {
                                        myRow[cl++].addText(row["cmpDcMaterial"].ToString().Substring(StrInicio, StrFim).TrimEnd());
                                        myRow[cl++].addText(row["cmpDcUnidade"].ToString().Trim());
                                        myRow[cl++].addText(decimal.Parse(row["cmpQtMaterial"].ToString()).ToString("0,0.00"));
                                        myRow[cl++].addText(decimal.Parse(row["cmpVlMaterial"].ToString()).ToString("0,0.00"));
                                        myRow[cl++].addText(decimal.Parse(row["cmpVlSubTotal"].ToString()).ToString("0,0.00"));
                                        total += float.Parse(row["cmpVlSubTotal"].ToString());
                                        myTable.addRow(myRow);
                                        StrInicio += QtdCaracterLinha;
                                        top += 11;
                                    }
                                    else
                                    {
                                        cl = 0;
                                        myRow = myTable.createRow();
                                        myRow.rowStyle = myTable.rowStyle;
                                        myRow[cl].columnAlign = predefinedAlignment.csLeft;
                                        myRow[cl++].addText("");
                                        myRow[cl++].addText(row["cmpDcMaterial"].ToString().Substring(StrInicio, StrFim).TrimEnd());
                                        myRow[cl++].addText("");
                                        myRow[cl++].addText("");
                                        myRow[cl++].addText("");
                                        myRow[cl++].addText("");
                                        myTable.addRow(myRow);
                                        StrInicio += QtdCaracterLinha;
                                        top += 11;
                                    }
                                }
                            }

                            myPage.addTable(myTable);
                            //top += 1;

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
                            myPage.addTable(myTable);
                            top += 17;
                        }
                    }

                    myTable = new pdfTable(myDoc, 1, new pdfColor("000000"), 2, new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, new pdfColor("FFFFFF")),
                        new pdfTableStyle(myDoc.getFontReference(strFont), 10, pdfColor.Black, pdfColor.White));

                    myTable.coordX = leftMargin;
                    myTable.coordY = getTop(myPage, top);

                    cl = 0;
                    myRow = myTable.createRow();
                    myTable.tableHeader.rowHeight = 10;
                    myTable.tableHeader.addColumn(570, predefinedAlignment.csLeft);
                    //IFormatProvider fp = new CultureInfo("pt-BR", true);
                    myTable.tableHeader[cl++].addText("Satisfeito com o serviço: " + "__Sim         __Não");

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

                    myPage.addText("Pág. " + pg++, leftMargin, 10, myDoc.getFontReference(strFont), 10, pdfColor.Black);
                }
        }
     }

    protected void grdOS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string[] ParImpressao = e.CommandArgument.ToString().Split(new char[] { '$' });

        try
        {
            switch (e.CommandName.ToLower().Trim())
            {
                case "lnk":
                    string p = "webATE_OS.aspx?id=" + e.CommandArgument.ToString();
                    Response.Redirect(p, false);
                    break;

                case "btn":
                    this.ImprimeOS(ParImpressao[0].ToString(), ParImpressao[1].ToString());
                    break;
            }
        }
        catch (Exception ex)
        {
            Global.ShowError(Global.Title, ex);
        }
    }

    protected void grdOS_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdOS.PageIndex = e.NewPageIndex;
            grdOS.DataSource = pesquisaOs();
            grdOS.DataBind();
        }
        catch (Exception ex)
        {
            Global.ShowError(Global.Title, ex);
        }
    }

    protected void chkConcluida_CheckedChanged(object sender, EventArgs e)
    {
        btnPesquisar_Click(sender, e);
    }

    protected void cmbObra_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnPesquisar_Click(sender, e);
    }

    protected void cmbOrigemOS_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnPesquisar_Click(sender, e);
    }

    protected void chkEmAprovacao_CheckedChanged(object sender, EventArgs e)
    {
        btnPesquisar_Click(sender, e);
    }

    protected void rdbState_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnPesquisar_Click(sender, e);
    }

    protected void cmbHeaderAprovar_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedvalue = ((DropDownList)sender).SelectedValue;

        foreach (GridViewRow r in grdOS.Rows)
        {
            DropDownList drop = (DropDownList)r.FindControl("cmbAprovar");
            if (drop != null)
                drop.SelectedIndex = selectedvalue == "A" ? 1 : 2;
        }
    }

    protected void btnAprove_Click(object sender, EventArgs e)
    {
        List<STOS> lst = new List<STOS>();
        ViewState["reprovar"] = false;
        try
        {
            foreach (GridViewRow r in grdOS.Rows)
            {
                DropDownList drop = (DropDownList)r.FindControl("cmbAprovar");
                if (drop != null)
                {
                    if (drop.SelectedValue == "A")
                        lst.Add(new STOS(((LinkButton)r.FindControl("lnk")).CommandArgument, "A", cmbObra.SelectedValue));
                    else if (drop.SelectedValue == "R")
                    {
                        ViewState["reprovar"] = true;
                        lst.Add(new STOS(((LinkButton)r.FindControl("lnk")).CommandArgument, "R", cmbObra.SelectedValue));
                    }
                }
            }

            if (lst.Count < 1)
                throw new Exception("Selecione uma ordem de serviço!");

            List<STOS> l = lst.FindAll(delegate(STOS st) { return st.state == "A"; });
            //aprovar
            //executar procedure de aprovacao
            tblOS tbl = new tblOS();
            tbl.cmpCoObra = cmbObra.SelectedValue;
            tbl.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;
            string str = "";
            foreach (STOS st in l)
                str += st.cmpIdOS + ",";
            tbl.GravarAprovacaoOS(Global.GetConnection(), str);

            //rejeitar
            if ((bool)ViewState["reprovar"])
            {
                Session["STOS"] = lst;
                Response.Redirect("~/ATE/webATE_OSJustificativa.aspx?id=" + ViewState["cmpIdOS"] + "&Sit=R", false);
            }
            else
                btnPesquisar_Click(sender, e);
        }
        catch (Exception ex)
        {
            Global.ShowError(Global.Title, ex.Message);
        }
    }

    protected void grdOS_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        TableCellCollection col = e.Row.Cells;
        if (col.Count > 1)
            col[0].Visible = rdbState.SelectedValue == "G";
    }

    protected void btnImprimirTodasOS_Click(object sender, EventArgs e)
    {
        pdfDocument myDoc = new pdfDocument("Horizon", "Orion");
        ImprimeTodasOS(myDoc);

        string filename = "pdf" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf";
        string nomepdf = Server.MapPath("~/Relatorios/" + filename);
        myDoc.createPDF(nomepdf);

        //Response.Redirect(filename, false);

        // Impressão ambiente desenvolvimento local
        Response.Write("<script language='javascript'>"
                      + "window.open('" + @"http://localhost:51055/Relatorios/" + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105')"
                      + "</script>");

        // Impressão ambiente produção ambiente interno orion
        //Response.Write("<script language='javascript'>"
        //                  + "window.open('" + @"http://172.10.10.2/HzWEBManutencao/Relatorios/" + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105')"
        //                  + "</script>");

        // Impressão ambiente produção ambiente externo orion
        //Response.Write("<script language='javascript'>"
        //                  + "window.open('" + @"http://201.22.148.250/HzWEBManutencao/Relatorios/" + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105')"
        //                  + "</script>");
    }

    #endregion

    }
}