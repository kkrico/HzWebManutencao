using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibManutencao;
using System.Data;
using HzLibConnection.Sql;
using HzLibCorporativo.Funcional;
using HzlibWEB;
using System.Globalization;
using System.Drawing;

namespace HzWebManutencao.Tabelas_Apoio
{
    public partial class web_CalendarioPreventiva : System.Web.UI.Page
    {
        DataTable dtCalendario;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {


                DateTime dataAtual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                Session.Add("data", dataAtual);
                loadObra();

                cmbObra.SelectedIndex = 0;
                CarregarAtividades();
                CarregarPeriodicidade();
                CarregarDados();
                Session.Add("coTipoAtividade", "");
                Session.Add("coAtividade", "");
                Session.Add("mes", DateTime.Now.Month);
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
                lblmes_ano.Text = monthName + "/" + DateTime.Now.Year;
                CarregarAtividades();

            }
        }

        private void CarregarPeriodicidade()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpCoObraGrupoLista";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = cmbObra.SelectedValue.ToString();
            ls.Add(lc);

            DataTable dt = tblPeriodicidade.RetornaPeriodicidadeObra(Global.GetConnection(), ls);
            grvPeriodicidade.DataSource = dt;
            grvPeriodicidade.DataBind();

            string Periodicidade = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Periodicidade != "") { Periodicidade += ","; }
                Periodicidade += dt.Rows[i]["cmpCoPeriodicidade"].ToString();
            }
            Periodicidade += ",0";
            Session["CoPeriodicidade"] = Periodicidade;
        }


        private void CarregarAtividades()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpCoObraGrupoLista";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = cmbObra.SelectedValue.ToString();
            ls.Add(lc);

            DataTable dt = tblTipoAtividade.RetornaTipoAtividadeObra(Global.GetConnection(), ls);
            grvAtividades.DataSource = dt;
            grvAtividades.DataBind();

            string tipoAtividade = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (tipoAtividade != "") { tipoAtividade += ","; }
                tipoAtividade += dt.Rows[i]["cmpCoTipoAtividade"].ToString();
            }
            tipoAtividade += ",0";
            Session["coTipoAtividade"] = tipoAtividade;
        }

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
                    Objetos.LoadCombo(cmbObra, table, "cmpNoObra", "cmpCoObraGrupoLista", "cmpNoObra", true);
                    //if (Session["login"] != null && ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista != null)
                    //    cmbObra.SelectedIndex = Objetos.RetornaIndiceCombo(cmbObra, int.Parse(((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista));
                    //cmbObra_SelectedIndexChanged(cmbObra, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnPesquisa_Click(object sender, EventArgs e)
        {

        }

        protected void btnretornar_Click(object sender, EventArgs e)
        {
            DateTime data = (DateTime)Session["data"];
            DateTime dataProx = data.AddMonths(-1);
            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dataProx.Month);
            lblmes_ano.Text = monthName + "/" + dataProx.Year;
            Session["data"] = dataProx;
            CarregarDados();
        }

        protected void btnproximo_Click(object sender, EventArgs e)
        {
            //string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.AddMonths(1));
            DateTime data = (DateTime)Session["data"];
            DateTime dataProx = data.AddMonths(1);
            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dataProx.Month);
            lblmes_ano.Text = monthName + "/" + dataProx.Year;
            Session["data"] = dataProx;
            CarregarDados();
        }
        private void CarregarDados()
        {

            DateTime data = (DateTime)Session["data"];
            dtCalendario = tblPreventivaAgenda.RetornaCalendarioPreventivas(Global.GetConnection(), data.ToShortDateString(), int.Parse(cmbObra.SelectedValue.ToString()), Session["coTipoAtividade"].ToString(), Session["coPeriodicidade"].ToString());
            grvCalendario.DataSource = dtCalendario;
            grvCalendario.DataBind();

        }


        protected void grvCalendario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hpDomingo = (e.Row.FindControl("hpDomingo") as HyperLink);
                Label lblDomingo = (e.Row.FindControl("lblDomingo") as Label);

                hpDomingo.Text = "";
                string array = dtCalendario.Rows[e.Row.RowIndex]["Domingo"].ToString();
                if (String.IsNullOrEmpty(array) == false)
                {
                    string[] dados = array.Split(';');
                    if (dados.Length > 0)
                    {
                        hpDomingo.Text = dados[0];
                    }
                    if (dados.Length > 1)
                    {
                        lblDomingo.Text = Linhas(array);
                        if (dados[1].Trim() == "")
                        {
                            e.Row.Cells[0].BackColor = Color.LightGray;
                        }
                    }
                }

                HyperLink hpSegunda = (e.Row.FindControl("hpSegunda") as HyperLink);
                Label lblSegunda = (e.Row.FindControl("lblSegunda") as Label);
                hpSegunda.Text = "";
                array = dtCalendario.Rows[e.Row.RowIndex]["Segunda"].ToString();
                if (String.IsNullOrEmpty(array) == false)
                {
                    string[] dados = array.Split(';');
                    if (dados.Length > 0)
                    {
                        hpSegunda.Text = dados[0];
                    }
                    if (dados.Length > 1)
                    {
                        lblSegunda.Text = Linhas(array);
                        if (dados[1].Trim() == "")
                        {
                            e.Row.Cells[1].BackColor = Color.LightGray;
                        }
                    }
                }

                HyperLink hpTerca = (e.Row.FindControl("hpTerca") as HyperLink);
                Label lblTerca = (e.Row.FindControl("lblTerca") as Label);
                hpTerca.Text = "";
                array = dtCalendario.Rows[e.Row.RowIndex]["Terca"].ToString();
                if (String.IsNullOrEmpty(array) == false)
                {
                    string[] dados = array.Split(';');
                    if (dados.Length > 0)
                    {
                        hpTerca.Text = dados[0];
                    }
                    if (dados.Length > 1)
                    {
                        lblTerca.Text = Linhas(array);
                        if (dados[1].Trim() == "")
                        {
                            e.Row.Cells[2].BackColor = Color.LightGray;
                        }
                    }
                }

                HyperLink hpQuarta = (e.Row.FindControl("hpQuarta") as HyperLink);
                Label lblQuarta = (e.Row.FindControl("lblQuarta") as Label);
                hpQuarta.Text = "";
                array = dtCalendario.Rows[e.Row.RowIndex]["Quarta"].ToString();
                if (String.IsNullOrEmpty(array) == false)
                {
                    string[] dados = array.Split(';');
                    if (dados.Length > 0)
                    {
                        hpQuarta.Text = dados[0];
                    }
                    if (dados.Length > 1)
                    {
                        lblQuarta.Text = Linhas(array);
                        if (dados[1].Trim() == "")
                        {
                            e.Row.Cells[3].BackColor = Color.LightGray;
                        }
                    }
                }

                HyperLink hpQuinta = (e.Row.FindControl("hpQuinta") as HyperLink);
                Label lblQuinta = (e.Row.FindControl("lblQuinta") as Label);
                hpQuinta.Text = "";
                array = dtCalendario.Rows[e.Row.RowIndex]["Quinta"].ToString();
                if (String.IsNullOrEmpty(array) == false)
                {
                    string[] dados = array.Split(';');
                    if (dados.Length > 0)
                    {
                        hpQuinta.Text = dados[0];
                    }
                    if (dados.Length > 1)
                    {
                        lblQuinta.Text = Linhas(array);
                        if (dados[1].Trim() == "")
                        {
                            e.Row.Cells[4].BackColor = Color.LightGray;
                        }
                    }
                }

                HyperLink hpSexta = (e.Row.FindControl("hpSexta") as HyperLink);
                Label lblSexta = (e.Row.FindControl("lblSexta") as Label);
                hpSexta.Text = "";
                array = dtCalendario.Rows[e.Row.RowIndex]["Sexta"].ToString();
                if (String.IsNullOrEmpty(array) == false)
                {
                    string[] dados = array.Split(';');
                    if (dados.Length > 0)
                    {
                        hpSexta.Text = dados[0];
                    }
                    if (dados.Length > 1)
                    {
                        lblSexta.Text = Linhas(array);
                        if (dados[1].Trim() == "")
                        {
                            e.Row.Cells[5].BackColor = Color.LightGray;
                        }
                    }
                }

                HyperLink hpSabado = (e.Row.FindControl("hpSabado") as HyperLink);
                Label lblSabado = (e.Row.FindControl("lblSabado") as Label);
                hpSabado.Text = "";
                array = dtCalendario.Rows[e.Row.RowIndex]["Sabado"].ToString();
                if (String.IsNullOrEmpty(array) == false)
                {
                    string[] dados = array.Split(';');
                    if (dados.Length > 0)
                    {
                        hpSabado.Text = dados[0];
                    }
                    if (dados.Length > 1)
                    {
                        lblSabado.Text = Linhas(array);
                        if (dados[1].Trim() == "")
                        {
                            e.Row.Cells[6].BackColor = Color.LightGray;
                        }
                    }
                }
            }
        }
        private string Linhas(string array)
        {
            string linha = "";
            string[] arrays = array.Split(';');
            for (int i = 1; i < arrays.Length; i++)
            {
                if (i > 1) { linha = linha + " <br /> "; }

                if (i < 4)
                {
                    linha = linha + arrays[i];
                }
                else
                {
                    linha = linha + "(...)";
                    break;
                }

            }
            return linha;
        }

        protected void cmbObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarAtividades();
            CarregarDados();
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            string nomeRel = "";
            DateTime data = (DateTime)Session["data"];
            RelatoriosReportServer.clRelatorios rel = new RelatoriosReportServer.clRelatorios();
            nomeRel = rel.CalendarioPreventiva(int.Parse(cmbObra.SelectedValue.ToString()), data.ToShortDateString(), Session["coTipoAtividade"].ToString(),Session["coPeriodicidade"].ToString());
            this.Response.Write("<SCRIPT language=javascript>window.open('../" + nomeRel + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');</SCRIPT>");
        }

        protected void btnfiltro_Click(object sender, EventArgs e)
        {
            ModalPopupExtender2.Show();
            CarregarAtividades();
        }

        protected void BtnOk_Click(object sender, EventArgs e)
        {
            string coTipoAtividade = "";

            for (int i = 0; i < grvAtividades.Rows.Count; i++)
            {
                //ckAtividade
                CheckBox ck = (CheckBox)grvAtividades.Rows[i].FindControl("ckAtividade");
                if (ck.Checked == true)
                {
                    if (coTipoAtividade != "") { coTipoAtividade += ","; }
                    coTipoAtividade += ck.ToolTip;
                }

            }
            coTipoAtividade += ",0";
            Session["coTipoAtividade"] = coTipoAtividade;

            string coPeriodicidade = "";

            for (int i = 0; i < grvPeriodicidade.Rows.Count; i++)
            {
                //ckAtividade
                CheckBox ck = (CheckBox)grvPeriodicidade.Rows[i].FindControl("ckPeriodicidade");
                if (ck.Checked == true)
                {
                    if (coPeriodicidade != "") { coPeriodicidade += ","; }
                    coPeriodicidade += ck.ToolTip;
                }

            }
            coPeriodicidade += ",0";
            Session["coPeriodicidade"] = coPeriodicidade;

            CarregarDados();
        }
        private void Periodicidade()
        {
            string coPeriodicidade = "";

            for (int i = 0; i < grvPeriodicidade.Rows.Count; i++)
            {
                //ckPeriodicidade
                CheckBox ck = (CheckBox)grvPeriodicidade.Rows[i].FindControl("ckPeriodicidade");
                if (ck.Checked == true)
                {
                    if (coPeriodicidade != "") { coPeriodicidade += ","; }
                    coPeriodicidade += ck.ToolTip;
                }

            }
            coPeriodicidade += ",0";
            Session["coPeriodicidade"] = coPeriodicidade;
            CarregarDados();


        }



        protected void grvPeriodicidade_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAno_Click(object sender, EventArgs e)
        {
            Response.Redirect("web_CalendarioPreventivaAno.aspx");
        }

        protected void grvAtividades_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grvAtividades_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvAtividades.PageIndex = e.NewPageIndex;
            CarregarAtividades();
            ModalPopupExtender2.Show();
            
        }
    }
}



