using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibConnection.Sql;
using HzlibWEB;
using HzLibManutencao;
using HzLibCorporativo.Funcional;
using HzLibCorporativo.Config;

namespace HzWebManutencao.Configuracao
{
    public partial class webCON_UsuPesquisa : System.Web.UI.Page
    {
        #region Functions

        private void loadPerfil()
        {
            try
            {
                using (DataTable table = tblPerfil.Get(Global.GetConnection()))
                {
                    Objetos.LoadCombo(cmbPerfil, table, "cmpDcPerfil", "cmpCoPerfil", "cmpDcPerfil", "TODOS", true);
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Pesquisa Usuários.
        /// </summary>
        private DataTable pesquisaUsuarios()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc;

            if (cmbPerfil.SelectedValue != "0")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpCoPerfil";
                lc.TipoCampo = TipoCampo.Numero;
                lc.Sinal = SinalPesquisa.Igual;
                lc.ValorCampo = cmbPerfil.SelectedValue;
                ls.Add(lc);
            }

            if (txtNome.Text != "")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpNoUsuario";
                lc.TipoCampo = TipoCampo.Like;
                lc.Sinal = SinalPesquisa.Like;
                lc.Percent = TipoPercent.InicioFim;
                lc.ValorCampo = txtNome.Text;
                ls.Add(lc);
            }

            return tblUsuario.Get(Global.GetConnection(), ls);
        }

        #endregion

        #region events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.loadPerfil();
                btnPesquisar_Click(sender, e);
            }
        }

        protected void grdUsu_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToLower().Trim())
                {
                    case "lnk":
                        string p = "~/Configuracao/webCON_UsuCadastro.aspx?id=" + e.CommandArgument.ToString();
                        Response.Redirect(p, false);
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        protected void grdUsu_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdUsu.PageIndex = e.NewPageIndex;
                grdUsu.DataSource = pesquisaUsuarios();
                grdUsu.DataBind();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        protected void grdUsu_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow Row in grdUsu.Rows)
            {
                ((Label)Row.FindControl("lblStatus")).Text = ((Label)Row.FindControl("lblAtivo")).Text == "True" ? "Ativo" : "Inativo";
            }            
        }

        protected void cmbObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }        
        #endregion

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            grdUsu.DataSource = pesquisaUsuarios(); ;
            grdUsu.DataBind();
        }

        protected void txtNome_TextChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

    }
}