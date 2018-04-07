using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using HzLibConnection.Sql;
using HzLibManutencao;
using HzlibWEB;
using Apresentacao.Controles;

namespace HzWebManutencao.Tabelas_Apoio
{
    public partial class webTAB_ExibirManutencaoEquipamento : System.Web.UI.Page
    {
        #region variables
        //static string prevPage = string.Empty;
        #endregion
 
        #region Função
        /// <summary>
        /// Carrega campos da página.
        /// </summary>
        private void loadEquipamento()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpIdEquipamentoObra";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = ViewState["idEquipObra"].ToString();
            ls.Add(lc);

            using (DataTable table = tblEquipamentoObra.Get(Global.GetConnection(), ls))
            {
                txtNomeObra.Text        = table.Rows[0]["cmpNoObra"].ToString();
                txtDcEquipamento.Text   = table.Rows[0]["cmpDcEquipamentoObra"].ToString();
                txtCodEquipamento.Text  = table.Rows[0]["CodEquipamento"].ToString();
                txtPavimento.Text       = table.Rows[0]["cmpDcPavimento"].ToString();
                txtLocalizacao.Text     = table.Rows[0]["cmpDcLocalEquipamento"].ToString();

                //txtDtManutencaoInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //txtDtManutencaoFinal.Text   = DateTime.Now.ToString("dd/MM/yyyy");

                txtNomeObra.Enabled         = false;
                txtDcEquipamento.Enabled    = false;
                txtCodEquipamento.Enabled   = false;
                txtPavimento.Enabled        = false;
                txtLocalizacao.Enabled      = false;
            }
        }

        /// <summary>
        /// Carrega lista de manutenções do equipamento da Obra.
        /// </summary>
        private DataTable loadManutencoes()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc  = new ListCampo();
            lc.NomeCampo  = "cmpIdEquipamentoObra";
            lc.TipoCampo  = TipoCampo.Numero;
            lc.ValorCampo = ViewState["idEquipObra"].ToString();
            ls.Add(lc);

            if (txtDtManutencaoInicial.Text != "")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpDtManutencaoEquipamento";
                lc.TipoCampo = TipoCampo.Data;
                lc.Sinal = SinalPesquisa.MaiorIgual;
                lc.ValorCampo = txtDtManutencaoInicial.Text + " 00:00:00";
                ls.Add(lc);
            }

            if (txtDtManutencaoFinal.Text != "")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpDtManutencaoEquipamento";
                lc.TipoCampo = TipoCampo.Data;
                lc.Sinal = SinalPesquisa.MenorIgual;
                lc.ValorCampo = txtDtManutencaoFinal.Text + " 23:59:59";
                ls.Add(lc);
            }

            return tblManutencaoEquipamento.Get(Global.GetConnection(), ls);
        }
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //prevPage = Request.UrlReferrer.ToString();

                if (Request.QueryString != null && Request.QueryString.Count > 0)
                {
                    ViewState["idEquipObra"] = Request.QueryString["idEquipObra"];
                    loadEquipamento();
                    grdPesquisa.DataSource = loadManutencoes();
                    grdPesquisa.DataBind();
                }
            }
        }

        protected void btnPesquisa_Click(object sender, EventArgs e)
        {
            grdPesquisa.DataSource = loadManutencoes();
            grdPesquisa.DataBind();
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            //Response.Redirect(prevPage);
            Response.Redirect("~/Tabelas Apoio/webTAB_CadastroEquipamentoObra.aspx", false);
        }

        protected void grdPesquisa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdPesquisa.PageIndex = e.NewPageIndex;
                grdPesquisa.DataSource = loadManutencoes();
                grdPesquisa.DataBind();
           }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        protected void grdPesquisa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower().Trim())
            {
                case "lnk":
                    Response.Redirect("~/ATE/webATE_OS.aspx?id=" + e.CommandArgument.ToString(), false);
                    break;
            }
        }

        #endregion

      }
}