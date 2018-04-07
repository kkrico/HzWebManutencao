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

namespace HzWebManutencao.Preventiva
{
    public partial class webPRE_PreventivaSemConformidade : System.Web.UI.Page
    {
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
                    Objetos.LoadCombo(cmbObra, table, "cmpNoObra", "cmpCoObraGrupoLista", "cmpNoObra", true);
                    if (Session["login"] != null && ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista != null)
                        cmbObra.SelectedIndex = Objetos.RetornaIndiceCombo(cmbObra, int.Parse(((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista));
                    cmbObra_SelectedIndexChanged(cmbObra, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Pesquisa Preventiva Agenda.
        /// </summary>
        private DataTable pesquisaPreventiva()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpCoObraGrupoLista";
            lc.TipoCampo = TipoCampo.Numero;
            lc.Sinal = SinalPesquisa.Igual;
            lc.ValorCampo = cmbObra.SelectedValue;
            ls.Add(lc);

            lc = new ListCampo();
            lc.NomeCampo = "cmpDtConfirmacaoPreventiva";
            lc.TipoCampo = TipoCampo.Data;
            lc.Sinal = SinalPesquisa.MaiorIgual;
            lc.ValorCampo = txtDataInicial.Text + " 00:00:00";
            ls.Add(lc);

            lc = new ListCampo();
            lc.NomeCampo = "cmpDtConfirmacaoPreventiva";
            lc.TipoCampo = TipoCampo.Data;
            lc.Sinal = SinalPesquisa.MenorIgual;
            lc.ValorCampo = txtDataFinal.Text + " 23:59:59";
            ls.Add(lc);

            lc = new ListCampo();
            lc.NomeCampo = "cmpInOcorrenciaPreventiva";
            lc.TipoCampo = TipoCampo.Numero;
            lc.Sinal = SinalPesquisa.Igual;
            lc.ValorCampo = "0";
            ls.Add(lc);

            return tblPreventivaAgenda.Get(Global.GetConnection(), ls);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.loadObra();
            }
        }

        protected void cmbObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista = cmbObra.SelectedValue;
            if (txtDataInicial.Text == "")
                txtDataInicial.Text = DateTime.Now.ToShortDateString();
                txtDataFinal.Text = DateTime.Now.ToShortDateString();
            btnPesquisar_Click(sender, e);

        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            DataTable table = pesquisaPreventiva();
            grdPreventivaPesq.DataSource = table;
            grdPreventivaPesq.DataBind();

            //foreach (GridViewRow row in grdPreventivaPesq.Rows)
            //{
            //    //*********** Ordem de Serviço vinculada ************
            //    row.Cells[6].FindControl("LinkButton1").Visible = row.Cells[5].Text.Equals("") ? false : true;
            //    //*********** Ordem de Serviço não vinculada ************
            //    row.Cells[7].FindControl("LinkButton2").Visible = row.Cells[5].Text.Equals("&nbsp;") ? true : false;
            //}
        }

        protected void rdbOcorrencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        protected void txtDataFinal_TextChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        protected void txtData_TextChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        protected void grdPreventivaPesq_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] ParImpressao = e.CommandArgument.ToString().Split(new char[] { '$' });

            switch (e.CommandName.ToLower().Trim())
            {
                case "linkbutton1":
                    Response.Redirect("~/ATE/webATE_OS.aspx?id=" + e.CommandArgument.ToString(), false);
                    break;
                case "linkbutton2":
                    Response.Redirect("~/ATE/webATE_OS.aspx?idPreAgenda=" + ParImpressao[0].ToString() + "&NuPreventiva=" + ParImpressao[1].ToString(), false);
                    break;
            }
        }

        protected void grdPreventivaPesq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                    break;
                case DataControlRowType.DataRow:
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                    e.Row.Cells[8].FindControl("LinkButton1").Visible = e.Row.Cells[7].Text.Equals("&nbsp;") ? false : true;
                    e.Row.Cells[8].FindControl("LinkButton2").Visible = e.Row.Cells[7].Text.Equals("&nbsp;") ? true : false;
                    break;
                case DataControlRowType.Footer:
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                    break;
            }

        }

        protected void grdPreventivaPesq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdPreventivaPesq.PageIndex = e.NewPageIndex;
                grdPreventivaPesq.DataSource = pesquisaPreventiva();
                grdPreventivaPesq.DataBind();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

    }
}