using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Globalization;
using System.Configuration;
using System.Web.UI.HtmlControls;

using HzLibCorporativo.Funcional;
using HzLibCorporativo.Faturamento;
using HzLibCorporativo.Geral;
using HzLibCorporativo.Config;
using HzClasses.Numeracao;
using HzClasses.Tabelas.Apoio;

using HzLibConnection.Sql;
using HzlibWEB;
using HzLibManutencao;
using Apresentacao.Controles;
using HzWebManutencao.Classes;
using HzLibGeneral.Util;

namespace HzWebManutencao.Faturamento
{
    public partial class webFAT_RecebeFatura : System.Web.UI.Page
    {
        #region Variables
        private decimal ValorTotalNota = 0;
        private decimal ValorTotalRecebido = 0;
        private decimal ValorTotalGlosado = 0;
        static string prevPage = string.Empty;
        #endregion

        #region Função
        /// <summary>
        /// Validar inclusão do material da O.S.
        /// </summary>
        private bool ValidarCampos()
        {
            bool ret = true;
            string msg = "";
            if (rdbRecebe.SelectedValue.ToString() == "RT" || rdbRecebe.SelectedValue.ToString() == "RP")
            {
                if (txtVlRecebido.Text == "")
                    msg += "Valor recebido em branco! <br />";
                if (txtDtRecebido.Text == "")
                    msg += "Data recebimento em branco! <br />";
            }

            if (rdbRecebe.SelectedValue.ToString() == "RG")
            {
                if (txtVlGlosado.Text == "")
                    msg += "Valor glosado em branco! <br />";
                if (txtDtRecebido.Text == "")
                    msg += "Data recebimento em branco! <br />";
            }

            //else
            //{
            //    if (txtJustificativa.Text == "")
            //        msg += "Justificativa em branco! <br />";
            //}

            if (msg != "")
            {
                MsgBox.ShowMessage(msg,"Erro");
                ret = false;
            }

            return ret;
        }

        private void PesquisaNota(int linha)
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();

            lc = new ListCampo();
            lc.NomeCampo  = "cmpIdFaturaObra";
            lc.TipoCampo  = TipoCampo.Numero;
            lc.ValorCampo = ViewState["idFaturaObra"].ToString();
            ls.Add(lc);

            lc = new ListCampo();
            lc.NomeCampo = "cmpInStautsNota";
            lc.TipoCampo = TipoCampo.String;
            lc.ValorCampo = "true";
            ls.Add(lc);

            DataTable table = tblFaturaCartaNota.GetRecebimento(Global.GetConnection(), ls);
            gvDadosNota.DataSource = table;
            gvDadosNota.DataBind();

            lblNomeObra.Text    = table.Rows[0]["cmpNoObraFatura"].ToString();
            lblTipoServico.Text = table.Rows[0]["cmpDcTipoServico"].ToString();

            ViewState["cmpNuMesFatura"]     = table.Rows[0]["cmpNuMesFatura"].ToString();
            ViewState["cmpNuAnoFatura"]     = table.Rows[0]["cmpNuAnoFatura"].ToString();
            ViewState["cmpDcTipoServico"]   = table.Rows[0]["cmpDcTipoServico"].ToString();
            ViewState["cmpIdTipoServico"]   = table.Rows[0]["cmpIdTipoServico"].ToString();
            ViewState["cmpIdFaturaCartaNota"] = table.Rows[0]["cmpIdFaturaCartaNota"].ToString();

            ValorTotalNota      = 0;
            ValorTotalRecebido  = 0;
            ValorTotalGlosado   = 0;
        }

        /// <summary>
        /// Carrega campos da página.
        /// </summary>
        private DataTable loadJustificativa(bool Todas)
        {
            ListCampos ls = new ListCampos();
            ListCampo lc  = new ListCampo();
            lc.NomeCampo  = "cmpDcFaturaEtapa";
            lc.TipoCampo  = TipoCampo.String;
            lc.ValorCampo = "Não Recebimento Fatura";
            ls.Add(lc);

            if (Todas)
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpIdFaturaObra";
                lc.TipoCampo = TipoCampo.String;
                lc.ValorCampo = ViewState["idFaturaObra"].ToString();
                ls.Add(lc);
            }
            else
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpIdFaturaObraEtapaJustifica";
                lc.TipoCampo = TipoCampo.String;
                lc.ValorCampo = ViewState["cmpIdFaturaObraEtapaJustifica"].ToString();
                ls.Add(lc);
            }

            return tblFaturaObraEtapaJustifica.Get(Global.GetConnection(), ls);

        }

        /// <summary>
        /// Carrega campos da página.
        /// </summary>
        private void loadRecebimento()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc  = new ListCampo();
            lc.NomeCampo  = "cmpIdFaturaCartaNota";
            lc.TipoCampo  = TipoCampo.Numero;
            lc.ValorCampo = ViewState["cmpIdFaturaCartaNota"].ToString();
            ls.Add(lc);

            using (DataTable table = tblFaturaCartaNota.GetRecebimento(Global.GetConnection(), ls))
            {
                if (table != null && table.Rows.Count > 0)
                {
                    lblNotaFiscal.Text = "Número da Nota Fiscal: " + table.Rows[0]["cmpNuNotaFiscal"].ToString();
                    rdbRecebe.SelectedValue = table.Rows[0]["cmpStRecebimentoNota"].ToString();
                    txtVlRecebido.Focus();

                    txtVlRecebido.Text = table.Rows[0]["cmpVlRecebidoNota"].ToString();
                    txtVlGlosado.Text  = table.Rows[0]["cmpVlGlosadoNota"].ToString();
                    txtDtRecebido.Text = table.Rows[0]["cmpDtRecebimentoNota"].ToString();

                    ViewState["cmpStRecebimentoNota"] = table.Rows[0]["cmpStRecebeNota"].ToString();
                }
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Operador Obra ou Engenheiro Obra
                if (((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNuPerfil.ToString() == "2" || ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNuPerfil.ToString() == "4")
                    btnGravar.Enabled = false;
                // Operador Escritório ou Administrador Sistema
                else if (((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNuPerfil.ToString() == "1" || ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNuPerfil.ToString() == "7")
                    btnGravar.Enabled = true;

                prevPage = Request.UrlReferrer.AbsolutePath;




                if (Request.QueryString != null && Request.QueryString.Count > 0)
                {
                    ViewState["idFaturaObra"] = Request.QueryString["idFaturaObra"];
                    PesquisaNota(0);
                    loadRecebimento();

                }
            }
            HzLogin login;
            login = (HzLogin)Session["Login"];
            tblMenu clMenu = new tblMenu();
            bool EditarPagina = clMenu.EditarMenu(Global.GetConnection(), int.Parse(login.cmpCoPerfil), 63);

            btnGravar.Enabled = EditarPagina;
            btnJustificativa.Enabled = EditarPagina;
            grdJustifica.Enabled = EditarPagina;
            txtVlRecebido.Enabled = EditarPagina;
            txtVlGlosado.Enabled = EditarPagina;
            txtDtRecebido.Enabled = EditarPagina;
            rdbRecebe.Enabled = EditarPagina;
         }

        #region Gride Notas
        protected void gvDadosNota_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Cells[0].Visible = false;
                    break;
                case DataControlRowType.DataRow:
                    e.Row.Cells[0].Visible = false;
                    e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
                    e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gvDadosNota, String.Concat("Select$", e.Row.RowIndex.ToString()));

                    Label lblValorNota = (Label)e.Row.FindControl("lblValorNota");
                    if (lblValorNota.Text != "")
                        ValorTotalNota += Decimal.Parse(lblValorNota.Text);

                    Label lblValorNotaRecebido = (Label)e.Row.FindControl("lblValorNotaRecebido");
                    if (lblValorNotaRecebido.Text != "")
                        ValorTotalRecebido += Decimal.Parse(lblValorNotaRecebido.Text);

                    Label lblValorNotaGlosado = (Label)e.Row.FindControl("lblValorNotaGlosado");
                    if (lblValorNotaGlosado.Text != "")
                        ValorTotalGlosado += Decimal.Parse(lblValorNotaGlosado.Text);
                    break;
                case DataControlRowType.Footer:
                    e.Row.Cells[0].Visible = false;
                    Label lblValorTotalNota = (Label)e.Row.FindControl("lblValorTotalNota");
                    if (ValorTotalNota.ToString() != "")
                        lblValorTotalNota.Text = ValorTotalNota.ToString("N2");

                    Label lblValorTotalRecebido = (Label)e.Row.FindControl("lblValorTotalNotaRecebido");
                    if (ValorTotalRecebido.ToString() != "")
                        lblValorTotalRecebido.Text = ValorTotalRecebido.ToString("N2");

                    Label lblValorTotalNotaGlosado = (Label)e.Row.FindControl("lblValorTotalNotaGlosado");
                    if (ValorTotalGlosado.ToString() != "")
                        lblValorTotalNotaGlosado.Text = ValorTotalGlosado.ToString("N2");
                    break;
            }
        }

        protected void gvDadosNota_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow Linha = gvDadosNota.SelectedRow;
            ViewState["cmpIdFaturaCartaNota"] = Linha.Cells[0].Text;
            loadRecebimento();
        }
        #endregion

        #region Recebimento
        protected void btnGravar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                tblFaturaCartaNota table = new tblFaturaCartaNota();
                try
                {
                    table.cmpIdFaturaObra       = ViewState["idFaturaObra"].ToString();
                    table.cmpIdFaturaCartaNota  = ViewState["cmpIdFaturaCartaNota"].ToString();
                    table.cmpVlRecebidoNota     = txtVlRecebido.Text.Replace(",", ".");
                    table.cmpVlGlosadoNota      = txtVlGlosado.Text.Replace(",", ".");
                    table.cmpDtRecebimentoNota  = txtDtRecebido.Text;
                    table.cmpStRecebimentoNota  = rdbRecebe.SelectedValue.ToString();
                    table.cmpNoUsuario          = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();

                    if (!(table.SaveRecebimento(Global.GetConnection())))
                        throw new Exception("Erro ao gravar dados do recebimento do fatura da obra!");
                    else
                        MsgBox.ShowMessage("Dados gravado com sucesso!", "Aviso");
                    PesquisaNota(0);
                }
                catch (Exception ex)
                {
                    MsgBox.ShowMessage(ex.ToString(), "Erro");
                }
            }
        }

        protected void rdbRecebe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbRecebe.SelectedValue.ToString() == "NR" || rdbRecebe.SelectedValue.ToString() == "RA" || rdbRecebe.SelectedValue.ToString() == "NI")
            {
                txtVlRecebido.Text = "";
                txtVlGlosado.Text = "";
                txtDtRecebido.Text = "";
                btnGravar_Click(sender, null);
            }
            else if (rdbRecebe.SelectedValue.ToString() == "RT" || rdbRecebe.SelectedValue.ToString() == "RP")
                txtVlRecebido.Focus();
            else
                txtVlGlosado.Focus();
        }
        
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage + "?NuMesFatura=" + ViewState["cmpNuMesFatura"].ToString()+ "&NuAnoFatura=" + ViewState["cmpNuAnoFatura"].ToString()+ "&IdTipoServico=" + ViewState["cmpIdTipoServico"].ToString(), false);
        }
        #endregion

        #region Justificativa
        protected void btnJustificativa_Click(object sender, EventArgs e)
        {
            grdJustifica.DataSource = loadJustificativa(true);
            grdJustifica.DataBind();

            if (grdJustifica.Rows.Count <= 0)
                btnNovaJustifica_Click(sender, null);

            ModalPopupExtender.Show();
        }

        protected void grdJustifica_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdJustifica.PageIndex = e.NewPageIndex;
                grdJustifica.DataSource = loadJustificativa(true);
                grdJustifica.DataBind();
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(ex.ToString(), "Erro");
            }
        }

        protected void grdJustifica_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ViewState["cmpIdFaturaObraEtapaJustifica"] = e.CommandArgument.ToString();
            DataTable tbl = loadJustificativa(false);

            switch (e.CommandName.Trim())
            {
                case "Editar":
                    txtJustificativa.Text = tbl.Rows[0]["cmpDcFaturaObraEtapaJustifica"].ToString();
                    ModalPopupExtender.Show();
                    txtJustificativa.Focus();
                    break;
                case "Excluir":
                    tblFaturaObraEtapaJustifica table = new tblFaturaObraEtapaJustifica();
                    table.cmpIdFaturaObraEtapaJustifica = ViewState["cmpIdFaturaObraEtapaJustifica"].ToString();
                    table.cmpNoUsuario                  = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;
                    if (table.Delete(Global.GetConnection()))
                    {
                        grdJustifica.DataSource = loadJustificativa(true);
                        grdJustifica.DataBind();
                        MsgBox.ShowMessage("Justificativa excluída com sucesso!", "Aviso");
                    }
                    ModalPopupExtender.Show();
                    break;
            }
        }

        protected void grdJustifica_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Cells[0].Visible = false;
                    break;
                case DataControlRowType.DataRow:
                    e.Row.Cells[0].Visible = false;
                    break;
                case DataControlRowType.Footer:
                    e.Row.Cells[0].Visible = false;
                    break;
            }
        }

        protected void btnNovaJustifica_Click(object sender, EventArgs e)
        {
            ViewState["cmpIdFaturaObraEtapaJustifica"] = "0";
            txtJustificativa.Text = "";
            txtJustificativa.Focus();
            ModalPopupExtender.Show();
        }

        protected void btnGravarJustifica_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                tblFaturaObraEtapaJustifica table = new tblFaturaObraEtapaJustifica();
                try
                {
                    table.cmpIdFaturaObraEtapaJustifica = ViewState["cmpIdFaturaObraEtapaJustifica"].ToString();
                    table.cmpIdFaturaObra               = ViewState["idFaturaObra"].ToString();
                    table.cmpDcFaturaObraEtapaJustifica = txtJustificativa.Text.TrimEnd();
                    table.cmpNoUsuario                  = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();

                    if (!(table.GravarJustificaNaoRecebimentoFatura(Global.GetConnection())))
                        throw new Exception("Erro ao gravar justificativa do não recebimento da fatura da obra!");
                    else
                        MsgBox.ShowMessage("Dados gravado com sucesso!", "Aviso");
                    btnJustificativa_Click(sender, null);
                    ModalPopupExtender.Show();
                }
                catch (Exception ex)
                {
                    MsgBox.ShowMessage(ex.ToString(), "Erro");
                }
            }
            else
                txtJustificativa.Focus();
        }

        protected void btnFechar_Click(object sender, EventArgs e)
        {
            ModalPopupExtender.Hide();
        }
        #endregion
  
    }
}