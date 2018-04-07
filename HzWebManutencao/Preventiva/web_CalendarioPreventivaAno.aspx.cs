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
    public partial class web_CalendarioPreventivaAno : System.Web.UI.Page
    {
        DataTable dtCalendario;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack == false)

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
                
                lblAno.Text = monthName + "/" + DateTime.Now.Year;
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
            dt.DefaultView.RowFilter = "cmpCoPeriodicidade <> 1 and cmpCoPeriodicidade<>2 and cmpCoPeriodicidade<> 3";
            grvPeriodicidade.DataSource = dt.DefaultView;
            grvPeriodicidade.DataBind();

            string Periodicidade = "";
            
            for (int i = 0; i < dt.DefaultView.Count; i++)
            {
                if (Periodicidade != "") { Periodicidade += ","; }
                Periodicidade += dt.DefaultView[i]["cmpCoPeriodicidade"].ToString();
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
            DateTime dataProx = data.AddYears(-1);
            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dataProx.Month);
            lblAno.Text = dataProx.Year.ToString();
            Session["data"] = dataProx;
            CarregarDados();
           }

            protected void btnproximo_Click(object sender, EventArgs e)
        {
            //string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.AddMonths(1));
            DateTime data = (DateTime)Session["data"];
            DateTime dataProx = data.AddYears(1);
            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dataProx.Month);
            lblAno.Text =  dataProx.Year.ToString();
            Session["data"] = dataProx;
            CarregarDados();
        }
            private void CarregarDados()
        {

            DateTime data = (DateTime)Session["data"];
            dtCalendario = tblPreventivaAgenda.RetornaCalendarioPreventivasAno(Global.GetConnection(), data.ToShortDateString(), 
            int.Parse(cmbObra.SelectedValue.ToString()), Session["coTipoAtividade"].ToString());

            grvCalendarioAno.DataSource = dtCalendario;
            grvCalendarioAno.DataBind();

        }

            protected void grvCalendarioAno_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hpCol1 = (e.Row.FindControl("hpCol1") as HyperLink);
                Label lblCol1 = (e.Row.FindControl("lblCol1") as Label);

                hpCol1.Text = "";
                string array = dtCalendario.Rows[e.Row.RowIndex]["Col1"].ToString();
                if (String.IsNullOrEmpty(array) == false)
                {
                    string[] dados = array.Split(';');
                    if (dados.Length > 0)
                    {
                        hpCol1.Text = dados[0];
                    }
                    if (dados.Length > 1)
                    {
                        lblCol1.Text = Linhas(array);
                        if (dados[1].Trim() == "")
                        {
                            e.Row.Cells[0].BackColor = Color.LightGray;
                        }
                    }
                }

                HyperLink hpCol2 = (e.Row.FindControl("hpCol2") as HyperLink);
                Label lblCol2 = (e.Row.FindControl("lblCol2") as Label);
                hpCol2.Text = "";
                array = dtCalendario.Rows[e.Row.RowIndex]["Col2"].ToString();
                if (String.IsNullOrEmpty(array) == false)
                {
                    string[] dados = array.Split(';');
                    if (dados.Length > 0)
                    {
                        hpCol2.Text = dados[0];
                    }
                    if (dados.Length > 1)
                    {
                        lblCol2.Text = Linhas(array);
                        if (dados[1].Trim() == "")
                        {
                            e.Row.Cells[1].BackColor = Color.LightGray;
                        }
                    }
                }

                 HyperLink hpCol3 = (e.Row.FindControl("hpCol3") as HyperLink);
                Label lblCol3 = (e.Row.FindControl("lblCol3") as Label);
                hpCol3.Text = "";
                array = dtCalendario.Rows[e.Row.RowIndex]["Col3"].ToString();
                if (String.IsNullOrEmpty(array) == false)
                {
                    string[] dados = array.Split(';');
                    if (dados.Length > 0)
                    {
                        hpCol3.Text = dados[0];
                    }
                    if (dados.Length > 1)
                    {
                        lblCol3.Text = Linhas(array);
                        if (dados[1].Trim() == "")
                        {
                            e.Row.Cells[2].BackColor = Color.LightGray;
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
                if (i > 0) { linha = linha + " <br /> "; }

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
            nomeRel = rel.CalendarioPreventivaANO(int.Parse(cmbObra.SelectedValue.ToString()), data.ToShortDateString(), Session["coTipoAtividade"].ToString());
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

            protected void btnmes_Click(object sender, EventArgs e)
            {
                Response.Redirect("web_CalendarioPreventiva.aspx");
            }

            protected void grvAtividades_SelectedIndexChanged(object sender, EventArgs e)
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
