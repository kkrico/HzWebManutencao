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
using HzLibCorporativo.Config;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.IO;
using System.Reflection;
using HzLibCorporativo.Geral;

namespace HzWebManutencao.Financeiro
{
    /// <summary>
    /// Classe com o Gridview e o nome da obra.
    /// </summary>
    public class GridExcel
    {
        public GridView Grid { get; set; }
        public Label Name { get; set; }

        public GridExcel(GridView grid, Label name)
        {
            this.Grid = grid;
            this.Name = name;
        }
    }

    public partial class webFIN_Lancamento : System.Web.UI.Page
    {
        #region Variables
        private float[] sumMonth = null;
        private float[] sumYear = null;
        private float[] arrAVG = null;
        private float[] sumEnt = null;
        private float[] sumPend = null;
        private float[] arrAdm = null;
        private float Result = 0f;

        List<GridExcel> lstGrid = new List<GridExcel>();
        #endregion

        #region Functions
        /// <summary>
        /// Formata a planilha excel para a exportação do grid.
        /// </summary>
        /// <param name="grid">Objeto do grid para exportação.</param>
        /// <returns></returns>
        private GridView PrepareForExport(GridView grid)
        {
            try
            {
                //Change the Header Row back to white color
                grid.HeaderRow.Style.Add("background-color", "#FFFFFF");
                //Apply style to Individual Cells
                for (int k = 0; k < grid.HeaderRow.Cells.Count; k++)
                    grid.HeaderRow.Cells[k].Style.Add("background-color", "gray");

                for (int i = 0; i < grid.Rows.Count; i++)
                {
                    GridViewRow row = grid.Rows[i];
                    //Change Color back to white
                    row.BackColor = System.Drawing.Color.White;
                    //Apply text style to each Row
                    row.Attributes.Add("class", "currency");
                    //Apply style to Individual Cells of Alternating Row
                    if (i % 2 != 0)
                    {
                        for (int j = 0; j < grid.Rows[i].Cells.Count; j++)
                            row.Cells[j].Style.Add("background-color", "#C2D69B");
                    }
                }

                return grid;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Cria a planilha excel de acordo com o gridview.
        /// </summary>
        /// <param name="lst">Objeto GridExcel com o GridView e o objeto com o nome da obra.</param>
        private void createExcel(List<GridExcel> lst)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                Table tb = new Table();
                foreach (GridExcel g in lst)
                {
                    TableRow tr1 = new TableRow();
                    TableCell cell1 = new TableCell();
                    cell1.Controls.Add(g.Name);
                    cell1.Controls.Add(this.PrepareForExport(g.Grid));
                    tr1.Controls.Add(cell1);
                    tb.Rows.Add(tr1);
                }
                tb.RenderControl(hw);
                //style to format numbers to string 
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End(); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private Label createLabel(string item, double top, string left)
        {
            Label lbl = new Label();
            lbl.Text = item;
            lbl.Style[HtmlTextWriterStyle.Position] = "absolute";
            lbl.Style[HtmlTextWriterStyle.Top] = top.ToString() + "px";
            lbl.Style[HtmlTextWriterStyle.Left] = left;

            return lbl;
        }

        private GridView setupGrid(double top)
        {
            GridView grd = new GridView();
            grd.RowDataBound += new GridViewRowEventHandler(grdObraFinanceiro_RowDataBound);
            grd.AutoGenerateColumns = false;
            //grd.ShowFooter = true;
            grd.FooterStyle.HorizontalAlign = HorizontalAlign.Right;
            grd.AlternatingRowStyle.BackColor = HzUtilClasses.Util.Color.convertColorHTML("#C0C0C0");
            grd.Font.Size = FontUnit.Smaller;
            grd.Width = 990;

            grd.Style[HtmlTextWriterStyle.Position] = "absolute";
            grd.Style[HtmlTextWriterStyle.Top] = top.ToString() + "px";
            grd.Style[HtmlTextWriterStyle.Left] = "1px";

            grd.Columns.Add(this.createBoundField("cmpDcCodigoConta", "Código", HorizontalAlign.Left, 30, ""));
            grd.Columns.Add(this.createBoundField("cmpDcTipoConta", "Conta", HorizontalAlign.Left, 100, ""));
            grd.Columns.Add(this.createBoundField("cmpVlExpectativa", "Expectativa", HorizontalAlign.Right, 70, "{0:N}"));

            return grd;
        }

        /// <summary>
        /// Cria o grid de pesquisa.
        /// </summary>
        private void creategrid()
        {
            double top = 250;
            DataTable tbl = null;
            try
            {
                if (chkExcel.Checked && (((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNuPerfil == "1"))
                    lstObraGrupo.Items[0].Selected = true;
                foreach (ListItem item in lstObraGrupo.Items)
                {
                    if (item.Selected)
                    {
                        Label lbl = this.createLabel("Obra: " + item.Text, top, "1px");
                        Label lblResult = this.createLabel("", top, "800px");
                        top += 20;

                        sumMonth = null;
                        sumYear = null;
                        arrAVG = null;
                        sumEnt = null;
                        arrAdm = null;
                        Result = 0;

                        GridView grd = this.setupGrid(top);

                        DateTime date = DateTime.Parse("01/" + cmbMesInicial.SelectedValue + "/" + txtAnoInicial.Text);
                        DateTime dateend = DateTime.Parse("01/" + cmbMesFinal.SelectedValue + "/" + txtAnoFinal.Text);
                        DataTable table = null;
                        if (!chkGeneral.Checked)
                            table = tblObraFinanceiroDetalhe.GetPIVOT(Global.GetConnection(), item.Value, date.ToString("dd/MM/yyyy"), dateend.ToString("dd/MM/yyyy"));
                        else
                        {
                            ListCampos ls = new ListCampos();
                            ListCampo lc = new ListCampo();
                            lc.NomeCampo = "cmpCoObraSienge";
                            lc.TipoCampo = TipoCampo.Numero;
                            lc.ValorCampo = item.Value;
                            ls.Add(lc);

                            tbl = tblObraSienge.Get(Global.GetConnection(), ls);
                            table = tblObraFinanceiroDetalhe.GetGrupoPIVOT(Global.GetConnection(), tbl.Rows[0]["cmpCoObraGrupoLista"].ToString(), "01/" + cmbMesInicial.SelectedValue + "/" + txtAnoInicial.Text,
                                "01/" + cmbMesFinal.SelectedValue + "/" + txtAnoFinal.Text);
                        }
                        if (table != null)
                        {
                            //acrescenta uma coluna para o cálculo da média
                            //table.Columns.Add(new DataColumn("cmpVlTotal", typeof(float)));
                            //table.AcceptChanges();
                            ////acrescenta uma linha para a porcentagem do lucro
                            DataRow row = table.NewRow();
                            table.Rows.Add(row);
                            table.AcceptChanges();

                            row = table.NewRow();
                            table.Rows.Add(row);
                            table.AcceptChanges();

                            row = table.NewRow();
                            table.Rows.Add(row);
                            table.AcceptChanges();

                            row = table.NewRow();
                            table.Rows.Add(row);
                            table.AcceptChanges();

                            ListCampos ls = new ListCampos();
                            ListCampo lc = new ListCampo();
                            lc.NomeCampo = "tosp.cmpCoObraSienge";
                            lc.TipoCampo = TipoCampo.Numero;
                            lc.ValorCampo = item.Value;
                            ls.Add(lc);

                            lc = new ListCampo();
                            lc.NomeCampo = "cmpDtLancamento";
                            lc.TipoCampo = TipoCampo.Data;
                            lc.Sinal = SinalPesquisa.MaiorIgual;
                            lc.ValorCampo = date.ToString("dd/MM/yyyy");
                            ls.Add(lc);

                            lc = new ListCampo();
                            lc.NomeCampo = "cmpDtLancamento";
                            lc.TipoCampo = TipoCampo.Data;
                            lc.Sinal = SinalPesquisa.MenorIgual;
                            lc.ValorCampo = dateend.ToString("dd/MM/yyyy");
                            ls.Add(lc);

                            lc = new ListCampo();
                            lc.NomeCampo = "cmpInStatus";
                            lc.TipoCampo = TipoCampo.String;
                            lc.ValorCampo = "N";
                            ls.Add(lc);

                            using (DataTable tb = tblObraSiengePendencia.Get(Global.GetConnection(), ls))
                            {
                                foreach (DataRow r in tb.Rows)
                                {
                                    string c = DateTime.Parse(r["cmpDtLancamento"].ToString()).Day.ToString("00") + "/" + 
                                        DateTime.Parse(r["cmpDtLancamento"].ToString()).Month.ToString("00") + "/" +
                                        DateTime.Parse(r["cmpDtLancamento"].ToString()).Year.ToString("0000");
                                    table.Rows[table.Rows.Count - 1][c] = r["cmpVlLancado"];
                                    table.Rows[table.Rows.Count - 1]["cmpDcTipoConta"] = "Receita Pendentes";
                                }
                            }
                            int months = Math.Abs(((dateend.Year - date.Year) * 12) + dateend.Month - date.Month);
                            BoundField bnd = null;
                            for (int i = 1; i <= months + 1; ++i)
                            {
                                bnd = this.createBoundField("01/" + date.Month.ToString("00") + "/" + date.Year.ToString("0000"), date.Month.ToString() + "/" + date.Year.ToString(),
                                    HorizontalAlign.Right, 70, "{0:N}");
                                grd.Columns.Add(bnd);

                                date = date.AddMonths(1);
                            }

                            bnd = this.createBoundField("cmpVlMedia", "Média (R$)", HorizontalAlign.Right, 70, "{0:N}");
                            grd.Columns.Add(bnd);

                            bnd = this.createBoundField("cmpVlTotal", "Total (R$)", HorizontalAlign.Right, 70, "{0:N}");
                            grd.Columns.Add(bnd);

                            grd.DataSource = table;
                            grd.DataBind();

                            //lblResult.Text = "Resultado (R$): " + String.Format("{0:N2}", Result);

                            this.divstyle.Controls.Add(lbl);
                            this.divstyle.Controls.Add(lblResult);
                            this.divstyle.Controls.Add(grd);
                            top += 300;

                            if (chkExcel.Checked)
                                lstGrid.Add(new GridExcel(grd, lbl));
                        }
                    }
                }
                this.divstyle.Style[HtmlTextWriterStyle.Height] = top.ToString() + "px";
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        //private void plot(double top, string title, string xtitle, string ytitle, string legendtext, string labeltext, SeriesChartType type, DataTable table, string xname, string yname, string graphname)
        private void plot()
        {
            Chart chart = new Chart();
            try
            {
                chart.BorderlineColor = HzUtilClasses.Util.Color.convertColorHTML("#000000");
                chart.BorderlineDashStyle = ChartDashStyle.Solid;
                chart.BorderlineWidth = 2;
                chart.Width = 600;

                chart.ChartAreas.Add(new ChartArea("chartresult"));
                chart.ChartAreas.Add(new ChartArea("chartperc"));
                
                chart.Series.Add(new Series("entradas"));
                chart.Series.Add(new Series("saidas"));
                chart.Series.Add(new Series("metamensal"));
                chart.Series.Add(new Series("metaatingida"));
                chart.Titles.Add("Relatório Financeiro");

                chart.Legends.Add(new Legend("result"));
                chart.Legends.Add(new Legend("perc"));

                //chart.Titles["title1"].Text = title;
                chart.Series["entradas"].ChartType = SeriesChartType.Column;
                chart.Series["entradas"].ChartArea = "chartresult";
                chart.Series["entradas"].Legend = "result";
                chart.Series["entradas"].LegendText = "Entradas";

                chart.Series["saidas"].ChartType = SeriesChartType.Column;
                chart.Series["saidas"].ChartArea = "chartresult";
                chart.Series["saidas"].Legend = "result";
                chart.Series["saidas"].LegendText = "Saídas";

                chart.Series["metamensal"].ChartType = SeriesChartType.Line;
                chart.Series["metamensal"].ChartArea = "chartperc";
                chart.Series["metamensal"].Legend = "perc";
                chart.Series["metamensal"].LegendText = "Meta Mensal";

                chart.Series["metaatingida"].ChartType = SeriesChartType.Line;
                chart.Series["metaatingida"].ChartArea = "chartperc";
                chart.Series["metaatingida"].Legend = "perc";
                chart.Series["metaatingida"].LegendText = "Meta Atingida";

                chart.ChartAreas["chartresult"].AxisX.Title = "mês";
                chart.ChartAreas["chartresult"].AxisY.Title = "reais";
                chart.ChartAreas["chartresult"].BorderColor = HzUtilClasses.Util.Color.convertColorHTML("#FFFFFF");
                chart.ChartAreas["chartresult"].BorderDashStyle = ChartDashStyle.Solid;
                chart.ChartAreas["chartresult"].BorderWidth = 2;

                chart.ChartAreas["chartperc"].AxisX.Title = "mês";
                chart.ChartAreas["chartperc"].AxisY.Title = "%";
                chart.ChartAreas["chartperc"].BorderColor = HzUtilClasses.Util.Color.convertColorHTML("#FFFFFF");
                chart.ChartAreas["chartperc"].BorderDashStyle = ChartDashStyle.Solid;
                chart.ChartAreas["chartperc"].BorderWidth = 2;

                using (DataTable table = tblObraFinanceiroDetalhe.GetChart(Global.GetConnection(), lstObraGrupo.SelectedValue,
                    "01/" + cmbMesInicial.SelectedValue + "/" + txtAnoInicial.Text, "01/" + cmbMesFinal.SelectedValue + "/" + txtAnoFinal.Text))
                {
                    foreach (DataRow r in table.Rows)
                    {
                        string ano = r["cmpNuMes"].ToString() + "/" + r["cmpNuAno"].ToString();
                        if (r["cmpDcTipoEntrada"].ToString().Trim() == "E")
                        {
                            chart.Series["entradas"].Points.AddXY(ano, r["cmpVlSoma"].ToString().Replace(",", "."));
                        }
                        else
                            chart.Series["saidas"].Points.AddXY(ano, r["cmpVlSoma"].ToString().Replace(",", "."));
                    }
                }

                using (DataTable table = tblObraSiengeMeta.Get(Global.GetConnection(), lstObraGrupo.SelectedValue,
                    "01/" + cmbMesInicial.SelectedValue + "/" + txtAnoInicial.Text, "01/" + cmbMesFinal.SelectedValue + "/" + txtAnoFinal.Text))
                {
                    foreach (DataRow r in table.Rows)
                    {
                        string ano = DateTime.Parse(r["cmpDtLancamento"].ToString()).Month.ToString() + "/" + DateTime.Parse(r["cmpDtLancamento"].ToString()).Year.ToString();
                        chart.Series["metamensal"].Points.AddXY(ano, r["cmpVlMetaMensal"].ToString().Replace(",", "."));
                        chart.Series["metaatingida"].Points.AddXY(ano, r["cmpVlMetaAtingida"].ToString().Replace(",", "."));
                    }
                }
                //chart.SaveImage(Server.MapPath(graphname), ChartImageFormat.Jpeg);

                int top = 600;
                chart.Style[HtmlTextWriterStyle.Position] = "absolute";
                chart.Style[HtmlTextWriterStyle.Top] = top.ToString() + "px";
                chart.Style[HtmlTextWriterStyle.Left] = "550px";
                this.Controls.Add(chart);

                //HyperLink hl = new HyperLink();
                //hl.NavigateUrl = "~/Financeiro/" + graphname;
                //hl.Text = "~/Financeiro/" + graphname;
                //hl.Style[HtmlTextWriterStyle.Position] = "absolute";
                //hl.Style[HtmlTextWriterStyle.Top] = (top += chart.Height.Value).ToString() + "px";
                //hl.Style[HtmlTextWriterStyle.Left] = "550px";
                //this.Controls.Add(hl);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Carrega as combos de tipo, atividade e forma de solicitação.
        /// </summary>
        private void load()
        {
            try
            {
                ListCampos ls = new ListCampos();
                //ListCampo lc = new ListCampo();
                //lc.NomeCampo = "cmpCoLocal";
                //lc.TipoCampo = TipoCampo.Numero;
                //lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoLocal;
                //ls.Add(lc);

                //lc = new ListCampo();
                //lc.NomeCampo = "cmpCoUsuario";
                //lc.TipoCampo = TipoCampo.Numero;
                //lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoUsuario;
                //ls.Add(lc);

                using (DataTable table = tblObraSienge.Get(Global.GetConnection(), ls))
                {
                    if (((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNuPerfil == "1")
                        Objetos.LoadCombo(lstObraGrupo, table, "cmpDcObraSienge", "cmpCoObraSienge", "cmpDcObraSienge", "--- TODOS ---", true);
                    else
                        Objetos.LoadCombo(lstObraGrupo, table, "cmpDcObraSienge", "cmpCoObraSienge", "cmpDcObraSienge", true);
                }

                using (DataTable table = tblMes.Get(Global.GetConnection(), new ListCampos()))
                {
                    Objetos.LoadCombo(cmbMesInicial, table, "cmpDcMes", "cmpCoMes", "cmpIdMes", true);
                    cmbMesInicial.SelectedIndex = Objetos.RetornaIndiceCombo(cmbMesInicial, 1);
                    Objetos.LoadCombo(cmbMesFinal, table, "cmpDcMes", "cmpCoMes", "cmpIdMes", true);
                    cmbMesFinal.SelectedIndex = Objetos.RetornaIndiceCombo(cmbMesFinal, DateTime.Now.Month);
                }

                txtAnoInicial.Text = DateTime.Now.Year.ToString();
                txtAnoFinal.Text = DateTime.Now.Year.ToString();
                grdTipoConta.Visible = false;
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Carrega as combos de tipo, atividade e forma de solicitação.
        /// </summary>
        private void loadData()
        {
            try
            {
                grdTipoConta.Visible = false;
                btnSave.Visible = false;
                cmbMesInicial.Enabled = true;
                txtAnoInicial.Enabled = true;
                lblMesInicial.Enabled = true;
                lblContrato.Visible = false;
                txtVlContrato.Visible = false;
                lblAdm.Visible = false;
                txtVlAdm.Visible = false;
                lblMetaMensal.Visible = false;
                txtMetaMensal.Visible = false;
                lblFinanceiro.Visible = false;
                lblRealizado.Visible = false;
                lblPendencia.Visible = false;
                grdPendencia.Visible = false;
                chkRealizado.Visible = false;

                lblTotal.Text = "";
                ViewState["cmpCoObraFinanceiro"] = "0";
                this.creategrid();
                grdTipoConta.Visible = false;
                if (chkExcel.Checked)
                    this.createExcel(lstGrid);

                plot();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// Evento ao carregar a página.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.load();

                if (Request.QueryString != null && Request.QueryString.Count > 0)
                {
                    cmbMesInicial.SelectedIndex = Objetos.RetornaIndiceCombo(cmbMesInicial, Request.QueryString["mes"]);
                    lstObraGrupo.SelectedIndex = Objetos.RetornaIndiceCombo(lstObraGrupo, int.Parse(Request.QueryString["cmpcoobragrupo"]));
                    txtAnoInicial.Text = Request.QueryString["ano"];
                    btnReset_Click(sender, e);
                }
                //else
                //    this.loadData();
            }
        }

        private string getCoMes(string mes)
        {
            string retval = "0";
            mes = mes.ToLower().Trim();

            if (mes == "janeiro")
                retval = "1";
            else if (mes == "fevereiro")
                retval = "2";
            else if (mes == "marco" || mes == "março")
                retval = "3";
            else if (mes == "abril")
                retval = "4";
            else if (mes == "maio")
                retval = "5";
            else if (mes == "junho")
                retval = "6";
            else if (mes == "julho")
                retval = "7";
            else if (mes == "agosto")
                retval = "8";
            else if (mes == "setembro")
                retval = "9";
            else if (mes == "outubro")
                retval = "10";
            else if (mes == "novembro")
                retval = "11";
            else if (mes == "dezembro")
                retval = "12";

            return retval;
        }
        /// <summary>
        /// Evento de clique do botão.
        /// Armazena os dados no banco de dados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            tblObraFinanceiro table = new tblObraFinanceiro();
            tblObraFinanceiroDetalhe tableDetalhe = new tblObraFinanceiroDetalhe();
            tblObraSiengePendencia tablePendnecia = new tblObraSiengePendencia();
            tblObraSiengeMeta tableMeta = new tblObraSiengeMeta();
            float somaE = 0;
            float somaS = 0;
            string tipoentrada = "E";
            try
            {
                table.cmpCoObraSienge = lstObraGrupo.SelectedValue;
                table.cmpMesLancamento = cmbMesFinal.SelectedValue;
                table.cmpAnoLancamento = txtAnoFinal.Text;
                table.cmpVlAdm = txtVlAdm.Text;
                table.cmpVlContrato = txtVlContrato.Text;
                table.cmpVlMeta = txtMetaMensal.Text;
                table.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;
                tableDetalhe.cmpCoObraFinanceiro = table.Save(Global.GetConnection(), false);

                tableDetalhe.cmpMesLancamento = cmbMesFinal.SelectedValue;
                tableDetalhe.cmpAnoLancamento = txtAnoFinal.Text;
                tableDetalhe.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;
                foreach (GridViewRow r in grdTipoConta.Rows)
                {
                    TextBox txt = (TextBox)r.FindControl("txtValue");
                    TextBox txtcod = (TextBox)r.FindControl("txtCode");
                    TextBox txtObraSiengeConta = (TextBox)r.FindControl("txtObraSiengeConta");
                    tipoentrada = txtObraSiengeConta.ToolTip;
                    CheckBox chksum = (CheckBox)r.FindControl("chkSum");
                    tableDetalhe.cmpCoObraSiengeConta = txtObraSiengeConta.Text;
                    if (txt != null)
                    {
                        string conta = r.Cells[0].Text;
                        tableDetalhe.cmpVlLancamento = txt.Text == "" ? txtVlContrato.Text : txt.Text.Replace(",", ".");
                        tableDetalhe.Save(Global.GetConnection(), chksum.Checked);

                        if (tipoentrada == "E")
                            somaE += float.Parse(tableDetalhe.cmpVlLancamento.Replace(".", ","));
                        else if (tipoentrada == "S")
                            somaS += float.Parse(tableDetalhe.cmpVlLancamento.Replace(".", ","));
                    }
                }

                tablePendnecia.cmpCoObraSienge = lstObraGrupo.SelectedValue;
                tablePendnecia.cmpDtLancamento = "01/" + cmbMesFinal.SelectedValue + "/" + txtAnoFinal.Text;
                tablePendnecia.cmpVlLancado = txtVlContrato.Text;
                tablePendnecia.cmpInStatus = chkRealizado.Checked ? "S" : "N";
                tablePendnecia.Save(Global.GetConnection());

                tableMeta.CmpCoObraSienge = lstObraGrupo.SelectedValue;
                tableMeta.CmpNuMes = cmbMesFinal.SelectedValue;
                tableMeta.cmpNuAno = txtAnoFinal.Text;
                tableMeta.cmpVlMetaMensal = txtMetaMensal.Text;
                tableMeta.cmpVlMetaAtingida = somaE > 0 ? (((somaE - somaS - somaE * float.Parse(txtVlAdm.Text) / 100) / somaE) * 100).ToString() : "0";
                tableMeta.Save(Global.GetConnection());

                foreach (GridViewRow r in grdPendencia.Rows)
                {
                    CheckBox chk = (CheckBox)r.FindControl("chkPagamento");
                    if (chk.Checked)
                    {
                        tblObraSiengePendencia tblP = new tblObraSiengePendencia();
                        tblP.cmpCoObraSienge = lstObraGrupo.SelectedValue;
                        tblP.cmpDtLancamento = "01/" + getCoMes(r.Cells[0].Text) + "/" + r.Cells[1].Text;
                        tblP.cmpVlLancado = txtVlContrato.Text;
                        tblP.cmpInStatus = "S";
                        tblP.Save(Global.GetConnection());
                    }
                }

                this.loadData();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Evento de clique do botão.
        /// Habilita a gravação de um novo registro ou 
        /// alteração de um existente.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdTipoConta.Visible)
                {
                    grdTipoConta.Visible = false;
                    cmbMesInicial.Enabled = true;
                    txtAnoInicial.Enabled = true;
                    lblMesInicial.Enabled = true;
                    lblFinanceiro.Visible = false;
                    lblPendencia.Visible = false;
                    btnSave.Visible = false;
                    grdPendencia.Visible = false;
                    lblRealizado.Visible = false;
                    chkRealizado.Visible = false;

                    return;
                }

                grdTipoConta.Visible = true;
                btnSave.Visible = true;
                cmbMesInicial.Enabled = false;
                txtAnoInicial.Enabled = false;
                lblMesInicial.Enabled = false;
                lblContrato.Visible = true;
                txtVlContrato.Visible = true;
                lblMetaMensal.Visible = true;
                txtMetaMensal.Visible = true;
                lblAdm.Visible = true;
                txtVlAdm.Visible = true;
                lblFinanceiro.Visible = true;
                lblPendencia.Visible = true;
                grdPendencia.Visible = true;
                lblRealizado.Visible = true;
                chkRealizado.Visible = true;

                lblFinanceiro.Text = "Financeiro atual de " + cmbMesFinal.SelectedValue + "/" + txtAnoFinal.Text;

                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "HzCorporativo..tblObraSiengeConta.cmpCoObraSienge";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = lstObraGrupo.SelectedValue;
                ls.Add(lc);

                using (DataTable table = tblObraSienge.GetObraSiengeContas(Global.GetConnection(), ls))
                {
                    txtVlContrato.Text = table.Rows[0]["cmpVlContrato"].ToString().Trim();
                    txtMetaMensal.Text = table.Rows[0]["cmpVlMeta"].ToString().Trim();
                    txtVlAdm.Text = table.Rows[0]["cmpVlAdm"].ToString().Trim();

                    grdTipoConta.DataSource = table;
                    grdTipoConta.DataBind();

                    foreach (GridViewRow r in grdTipoConta.Rows)
                    {
                        TextBox txtObraSiengeConta = (TextBox)r.FindControl("txtObraSiengeConta");
                        foreach (DataRow dr in table.Rows)
                        {
                            if (dr["cmpDcCodigoConta"].ToString().Trim() == r.Cells[0].Text.Trim())
                            {
                                txtObraSiengeConta.Text = dr["cmpCoObraSiengeConta"].ToString();
                                txtObraSiengeConta.ToolTip = dr["cmpDcTipoEntrada"].ToString();
                                break;
                            }
                        }
                    }
                }

                if (cmbMesInicial.SelectedValue != "" && txtAnoInicial.Text != "")
                {
                    DataTable tbl = null;

                    ls = new ListCampos();
                    lc = new ListCampo();
                    lc.NomeCampo = "tosp.cmpCoObraSienge";
                    lc.TipoCampo = TipoCampo.Numero;
                    lc.ValorCampo = lstObraGrupo.SelectedValue;
                    ls.Add(lc);

                    lc = new ListCampo();
                    lc.NomeCampo = "cmpInStatus";
                    lc.TipoCampo = TipoCampo.String;
                    lc.ValorCampo = "N";
                    ls.Add(lc);

                    tbl = tblObraSiengePendencia.Get(Global.GetConnection(), ls);
                    grdPendencia.DataSource = tbl;
                    grdPendencia.DataBind();

                    ls = new ListCampos();
                    lc = new ListCampo();
                    lc.NomeCampo = "tosp.cmpCoObraSienge";
                    lc.TipoCampo = TipoCampo.Numero;
                    lc.ValorCampo = lstObraGrupo.SelectedValue;
                    ls.Add(lc);

                    lc = new ListCampo();
                    lc.NomeCampo = "cmpDtLancamento";
                    lc.TipoCampo = TipoCampo.Data;
                    lc.ValorCampo = "01/" + cmbMesFinal.SelectedValue + "/" + txtAnoFinal.Text;
                    ls.Add(lc);

                    tbl = tblObraSiengePendencia.Get(Global.GetConnection(), ls);
                    if (tbl != null && tbl.Rows.Count > 0)
                        chkRealizado.Checked = tbl.Rows[0]["cmpInStatus"].ToString().Trim() == "S";
                    else
                        chkRealizado.Checked = false;

                    ls = new ListCampos();
                    lc = new ListCampo();
                    lc.NomeCampo = "gobra.cmpCoObraSienge";
                    lc.TipoCampo = TipoCampo.Numero;
                    lc.ValorCampo = lstObraGrupo.SelectedValue;
                    ls.Add(lc);

                    lc = new ListCampo();
                    lc.NomeCampo = "cmpNuMes";
                    lc.TipoCampo = TipoCampo.Numero;
                    lc.ValorCampo = cmbMesFinal.SelectedValue;
                    ls.Add(lc);

                    lc = new ListCampo();
                    lc.NomeCampo = "cmpNuAno";
                    lc.TipoCampo = TipoCampo.Numero;
                    lc.ValorCampo = txtAnoFinal.Text;
                    ls.Add(lc);

                    using (DataTable table = tblObraFinanceiroDetalhe.Get(Global.GetConnection(), ls))
                    {
                        foreach (GridViewRow r in grdTipoConta.Rows)
                        {
                            TextBox txt = (TextBox)r.FindControl("txtValue");
                            TextBox txtcod = (TextBox)r.FindControl("txtCode");
                            Label lbl = (Label)r.FindControl("lblValue");
                            foreach (DataRow dr in table.Rows)
                            {
                                txtVlContrato.Text = dr["cmpVlContrato"].ToString();
                                txtVlAdm.Text = dr["cmpVlAdm"].ToString();
                                txtMetaMensal.Text = dr["cmpVlMeta"].ToString();

                                if (dr["cmpDcCodigoConta"].ToString().Trim() == r.Cells[0].Text.Trim())
                                {
                                    txt.Text = dr["cmpVlLancamento"].ToString();
                                    lbl.Text = dr["cmpVlLancamento"].ToString();
                                    txtcod.Text = dr["cmpCoObraFinanceiro"].ToString();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        private BoundField createBoundField(string datafield, string headertext, HorizontalAlign ha, Unit width, string formatstring)
        {
            BoundField bnd = new BoundField();
            bnd.DataField = datafield;
            bnd.HeaderText = headertext;
            bnd.ItemStyle.HorizontalAlign = ha;
            bnd.ItemStyle.Width = width;
            bnd.DataFormatString = formatstring;
            return bnd;
        }
        /// <summary>
        /// Evento de clique do botão.
        /// Pesquisa os dados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.loadData();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Evento de clique do listbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lstObraGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearch_Click(sender, e);
        }

        /// <summary>
        /// Evento de vinculação dos dados da tabela com o gridview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdObraFinanceiro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int month = 0;
            float vlexpectativa = 0f;
            float vllancamento = 0f;
            float vllancado = 0f;
            float vladm = 0f;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // O número de colunas do pivot table é a quantidade de meses + o tipo da conta, tipo da entrada
                    // e o código da conta. Por este motivo, subtrai-se 3 do total de colunas.
                    int columns = ((System.Data.DataRowView)e.Row.DataItem).Row.Table.Columns.Count - 4;
                    
                    object[] rowview = ((DataRowView)e.Row.DataItem).Row.ItemArray;
                    if (sumMonth == null)
                        sumMonth = new float[columns];
                    if (sumYear == null)
                        sumYear = new float[columns];
                    if (sumEnt == null)
                        sumEnt = new float[columns];
                    if (sumPend == null)
                        sumPend = new float[columns];
                    if (arrAdm == null)
                        arrAdm = new float[columns];
                    if (arrAVG == null)
                        arrAVG = new float[((System.Data.DataRowView)(e.Row.DataItem)).Row.Table.Rows.Count + 1];
                    month = 0;
                    //for (int i = 3; i <= rowview.Length - 1; ++i)
                    float sumrow = 0;
                    float.TryParse(rowview[2].ToString(), out vlexpectativa);
                    float.TryParse(rowview[4].ToString(), out vladm);
                    for (int i = 5; i < rowview.Length; ++i)
                    {
                        float.TryParse(rowview[i].ToString(), out vllancamento);
                        if (rowview.Length > 10)
                            float.TryParse(rowview[11].ToString(), out vllancado);
                        if (rowview[3].ToString() == "E")
                        {
                            sumPend[i - 5] += vllancado;
                            sumEnt[i - 5] += vllancamento;
                            arrAdm[i - 5] = (float)(sumEnt[i - 5] * vladm / 100);
                        }
                        else
                            sumMonth[i - 5] += vllancamento;
                        sumrow += vllancamento;
                    }
                    DateTime date = DateTime.Parse("01/" + cmbMesInicial.SelectedValue + "/" + txtAnoInicial.Text);
                    DateTime dateend = DateTime.Parse("01/" + cmbMesFinal.SelectedValue + "/" + txtAnoFinal.Text);
                    TimeSpan ts = dateend.Subtract(date);
                    double months = Math.Round(ts.Days / (365.25 / 12)) + 1;

                    e.Row.Cells[e.Row.Cells.Count - 2].Text = String.Format("{0:N2}", sumrow / months);
                    e.Row.Cells[e.Row.Cells.Count - 1].Text = String.Format("{0:N2}", sumrow);

                    if (e.Row.RowIndex == ((System.Data.DataRowView)(e.Row.DataItem)).Row.Table.Rows.Count - 4)
                    {
                        sumrow = 0;
                        e.Row.Cells[0].Text = "4";
                        e.Row.Cells[1].Text = "Resultado Bruto";
                        for (int i = 5; i < rowview.Length; ++i)
                        {
                            float d = (float)(sumEnt[i - 5] - sumMonth[i - 5]);
                            if (d < 0)
                                e.Row.Cells[i - 2].ForeColor = HzUtilClasses.Util.Color.convertColorHTML("#FF0000");
                            e.Row.Cells[i - 2].Text = String.Format("{0:N2}", d);
                            sumrow += d;
                        }
                        e.Row.Cells[e.Row.Cells.Count - 2].Text = String.Format("{0:N2}", sumrow / months);
                        e.Row.Cells[e.Row.Cells.Count - 1].Text = String.Format("{0:N2}", sumrow);
                    }

                    if (e.Row.RowIndex == ((System.Data.DataRowView)(e.Row.DataItem)).Row.Table.Rows.Count - 3)
                    {
                        sumrow = 0;
                        if (vladm < 1)
                            vladm = 5.5f;
                        e.Row.Cells[0].Text = "5";
                        e.Row.Cells[1].Text = "Adm (" + String.Format("{0:N2}", vladm) + "%)";
                        for (int i = 5; i < rowview.Length; ++i)
                        {
                            e.Row.Cells[i - 2].ForeColor = HzUtilClasses.Util.Color.convertColorHTML("#FF0000");
                            e.Row.Cells[i - 2].Text = String.Format("{0:N2}", arrAdm[i - 5]);
                            sumrow += arrAdm[i - 5];
                        }
                        e.Row.Cells[e.Row.Cells.Count - 2].Text = String.Format("{0:N2}", sumrow / months);
                        e.Row.Cells[e.Row.Cells.Count - 1].Text = String.Format("{0:N2}", sumrow);
                    }

                    if (e.Row.RowIndex == ((System.Data.DataRowView)(e.Row.DataItem)).Row.Table.Rows.Count - 2)
                    {
                        float d = 0f;
                        float p = 0f;
                        float percrow = 0;
                        sumrow = 0;
                        e.Row.Cells[0].Text = "6";
                        e.Row.Cells[1].Text = "Resultado Líquido";
                        for (int i = 5; i < rowview.Length; ++i)
                        {
                            d = (float)((-1) * sumMonth[i - 5] - arrAdm[i - 5] + sumEnt[i - 5]);
                            if (sumEnt[i - 5] > 0)
                                p = d / sumEnt[i - 5] * 100;
                            else
                                p = 0;
                            if (d < 0)
                                e.Row.Cells[i - 2].ForeColor = HzUtilClasses.Util.Color.convertColorHTML("#FF0000");
                            e.Row.Cells[i - 2].Text = String.Format("{0:N2}", d) + " (" +
                                String.Format("{0:N2}", p) + "%)";
                            sumrow += d;
                            percrow += p; 
                        }
                        e.Row.Cells[e.Row.Cells.Count - 2].Text = String.Format("{0:N2}", sumrow / months) + " (" +
                                String.Format("{0:N2}", percrow / months) + "%)";
                        e.Row.Cells[e.Row.Cells.Count - 1].Text = String.Format("{0:N2}", sumrow);
                    }

                    if (e.Row.RowIndex == ((System.Data.DataRowView)(e.Row.DataItem)).Row.Table.Rows.Count - 1)
                    {
                        e.Row.Cells[0].Text = "7";
                        e.Row.Cells[1].Text = "Faturas Pendentes";

                        for (int i = 5; i < rowview.Length; ++i)
                        {
                            e.Row.Cells[e.Row.Cells.Count - 1].Text = String.Format("{0:N2}", sumPend[i - 5]);
                        }
                    }
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[1].Text = "%";
                    Result = 0;
                    for (int i = 0; i < sumYear.Length - 1; ++i)
                    {
                        Result += sumMonth[i];
                        if (sumYear[i] > 1 && Math.Abs((sumMonth[i] / sumYear[i]) * 100) <= 100)
                        {
                            e.Row.Cells[i + 2].Text = String.Format("{0:N2}", Math.Abs((sumMonth[i] / sumYear[i]) * 100));
                            if (sumMonth[i] / sumYear[i] < sumMonth[0] / sumYear[0])
                                e.Row.Cells[i + 2].ForeColor = HzUtilClasses.Util.Color.convertColorHTML("#FF0000");
                        }
                        else
                        {
                            e.Row.Cells[i + 2].Text = "####";
                            e.Row.Cells[i + 2].ForeColor = HzUtilClasses.Util.Color.convertColorHTML("#FF0000");
                        }
                    }
                    e.Row.Cells[e.Row.Cells.Count - 1].Text = arrAVG[0] > 0 ? String.Format("{0:N2}", (arrAVG[arrAVG.Length - 1] * 100 / arrAVG[0])) : "####";
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }
        #endregion

        protected void cmbMesFinal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdTipoConta.Visible)
                btnReset_Click(sender, e);
        }
    }
}