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
    public partial class webFin_Relatorio : System.Web.UI.Page
    {
        #region Functions
        /// <summary>
        /// Carrega as combos de tipo, atividade e forma de solicitação.
        /// </summary>
        private void load()
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
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        private BoundField createBoundField(string datafield, string headertext, HorizontalAlign ha, Unit width)
        {
            BoundField bnd = new BoundField();
            bnd.DataField = datafield;
            bnd.HeaderText = headertext;
            bnd.ItemStyle.HorizontalAlign = ha;
            bnd.ItemStyle.Width = width;

            return bnd;
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
                foreach (ListItem item in lstObraGrupo.Items)
                {
                    if (item.Selected)
                    {
                        Label lbl = new Label();
                        lbl.Text = "Obra: " + item.Text;
                        lbl.Style[HtmlTextWriterStyle.Position] = "absolute";
                        lbl.Style[HtmlTextWriterStyle.Top] = top.ToString() + "px";
                        lbl.Style[HtmlTextWriterStyle.Left] = "1px";

                        Label lblResult = new Label();
                        lblResult.Text = "";
                        lblResult.Style[HtmlTextWriterStyle.Position] = "absolute";
                        lblResult.Style[HtmlTextWriterStyle.Top] = top.ToString() + "px";
                        lblResult.Style[HtmlTextWriterStyle.Left] = "800px";
                        top += 20;

                        GridView grd = new GridView();
//                        grd.RowDataBound += new GridViewRowEventHandler(grdObraFinanceiro_RowDataBound);
                        grd.AutoGenerateColumns = false;
                        //grd.ShowFooter = true;
                        grd.FooterStyle.HorizontalAlign = HorizontalAlign.Right;
                        grd.AlternatingRowStyle.BackColor = HzUtilClasses.Util.Color.convertColorHTML("#C0C0C0");
                        grd.Font.Size = FontUnit.Smaller;
                        grd.Width = 990;

                        grd.Style[HtmlTextWriterStyle.Position] = "absolute";
                        grd.Style[HtmlTextWriterStyle.Top] = top.ToString() + "px";
                        grd.Style[HtmlTextWriterStyle.Left] = "1px";

                        grd.Columns.Add(this.createBoundField("cmpDcCodigoConta", "Código", HorizontalAlign.Left, 30));
                        grd.Columns.Add(this.createBoundField("cmpDcTipoConta", "Conta", HorizontalAlign.Left, 100));
                        grd.Columns.Add(this.createBoundField("cmpVlExpectativa", "Expectativa", HorizontalAlign.Right, 70));

                        //DateTime date = DateTime.Parse("01/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString("0000"));
                        //DateTime dateend = date.AddMonths(-11);
                        DateTime date = DateTime.Parse("01/" + cmbMesInicial.SelectedValue + "/" + txtAnoInicial.Text);
                        DateTime dateend = DateTime.Parse("01/" + cmbMesFinal.SelectedValue + "/" + txtAnoFinal.Text);
                        
                        ListCampos ls = new ListCampos();
                        ListCampo lc = new ListCampo();
                        lc.NomeCampo = "cmpCoObraSienge";
                        lc.TipoCampo = TipoCampo.Numero;
                        lc.ValorCampo = item.Value;
                        ls.Add(lc);

                        lc = new ListCampo();
                        lc.NomeCampo = "cmpInStatus";
                        lc.TipoCampo = TipoCampo.String;
                        lc.ValorCampo = "S";
                        ls.Add(lc);

                        DataTable table = tblObraSiengePendencia.Get(Global.GetConnection(), ls);
                        grd.DataSource = table;
                        grd.DataBind();

                        this.divstyle.Controls.Add(lbl);
                        this.divstyle.Controls.Add(lblResult);
                        this.divstyle.Controls.Add(grd);
                        top += 300;
                    }
                }
                this.divstyle.Style[HtmlTextWriterStyle.Height] = top.ToString() + "px";
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// Evento de clique do listbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lstObraGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearch_Click(sender, e);
        }

        protected void cmbMesFinal_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Evento de clique do botão de pesquisa.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            double top = 250;
            ListCampos ls = new ListCampos();
            ListCampo lc = null;
            try
            {

                if (lstObraGrupo.SelectedIndex > 0)
                {
                    lc = new ListCampo();
                    lc.NomeCampo = "tosp.cmpCoObraSienge";
                    lc.TipoCampo = TipoCampo.Numero;
                    lc.ValorCampo = lstObraGrupo.SelectedValue;
                    ls.Add(lc);
                }

                lc = new ListCampo();
                lc.NomeCampo = "cmpDtLancamento";
                lc.TipoCampo = TipoCampo.Data;
                lc.Sinal = SinalPesquisa.MaiorIgual;
                lc.ValorCampo = "01/" + cmbMesInicial.SelectedValue + "/" + txtAnoInicial.Text;
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "cmpDtLancamento";
                lc.TipoCampo = TipoCampo.Data;
                lc.Sinal = SinalPesquisa.MenorIgual;
                lc.ValorCampo = "01/" + cmbMesFinal.SelectedValue + "/" + txtAnoFinal.Text;
                ls.Add(lc);

                DataTable table = tblObraSiengePendencia.Get(Global.GetConnection(), ls);

                GridView grd = new GridView();
                //                        grd.RowDataBound += new GridViewRowEventHandler(grdObraFinanceiro_RowDataBound);
                grd.AutoGenerateColumns = false;
                //grd.ShowFooter = true;
                grd.FooterStyle.HorizontalAlign = HorizontalAlign.Right;
                grd.AlternatingRowStyle.BackColor = HzUtilClasses.Util.Color.convertColorHTML("#C0C0C0");
                grd.Font.Size = FontUnit.Smaller;
                grd.Width = 990;

                grd.Style[HtmlTextWriterStyle.Position] = "absolute";
                grd.Style[HtmlTextWriterStyle.Top] = top.ToString() + "px";
                grd.Style[HtmlTextWriterStyle.Left] = "1px";

                grd.Columns.Add(this.createBoundField("cmpDcObraSienge", "Obra", HorizontalAlign.Left, 30));
                grd.Columns.Add(this.createBoundField("cmpVlLancado", "Valor (R$)", HorizontalAlign.Right, 70));

                grd.DataSource = table;
                grd.DataBind();

                this.divstyle.Controls.Add(grd);
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

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
                }
            }
        }
        #endregion

    }
}