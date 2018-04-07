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

namespace HzWebManutencao.ATE
{
    public partial class webATE_OSPesquisa : System.Web.UI.Page
    {
        #region Functions

        private void loadEnviados()
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

        
         #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.load();
                
                
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
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

                if (chkConcluida.Checked)
                {
                    lc = new ListCampo();
                    lc.NomeCampo = "cmpStOS";
                    lc.TipoCampo = TipoCampo.String;
                    lc.Sinal = SinalPesquisa.Igual;
                    lc.ValorCampo = "S";
                    ls.Add(lc);
                }

                if (chkEmAprovacao.Checked)
                {
                    lc = new ListCampo();
                    lc.NomeCampo = "cmpStOS";
                    lc.TipoCampo = TipoCampo.String;
                    lc.Sinal = SinalPesquisa.Igual;
                    lc.ValorCampo = "G";
                    ls.Add(lc);
                }

                using (DataTable table = tblOS.Get(Global.GetConnection(), ls))
                {
                    if (table != null && table.Rows.Count > 0)
                    {
                        grdOS.DataSource = table;
                        grdOS.DataBind();

                    }
                    else
                    {
                        Global.ShowMensager(Global.Title, "Nenhum registro encontrado!");
                    }
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }
        #endregion

        protected void grdOS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToLower().Trim())
                {
                    case "lnk":
                        Response.Redirect("webATE_OS.aspx?id=" + e.CommandArgument.ToString());
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
                btnPesquisar_Click(sender, EventArgs.Empty);
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
      
    }
}