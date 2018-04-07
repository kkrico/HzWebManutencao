using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzlibWEB;
using HzLibConnection.Sql;
using HzLibManutencao;
using Apresentacao.Controles;

namespace HzWebManutencao.Tabelas_Apoio
{
    public partial class webTAB_CadastroEquipamento : System.Web.UI.Page
    {
        #region Funções

        /// <summary>
        /// Carrega a combo de tipo de equipamento
        /// </summary>
        private void load()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();

            lc.NomeCampo = "cmpCoContratante";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoContratante;
            ls.Add(lc);

            lc = new ListCampo();
            lc.NomeCampo = "cmpTpGrupoAtividade";
            lc.TipoCampo = TipoCampo.StringIN;
            lc.Sinal = SinalPesquisa.IN;
            lc.ValorCampo = "'T','E'";
            ls.Add(lc);

            DataTable table = tblTipoAtividade.RetornaTipoAtividade(Global.GetConnection(), ls);
            Objetos.LoadCombo(cmbTipoEquipamento, table, "cmpDcTipoAtividade", "cmpCoTipoAtividade", "cmpDcTipoAtividade", "--- Selecione ---", true);
            cmbTipoEquipamento_SelectedIndexChanged(cmbTipoEquipamento, EventArgs.Empty);

            table.Reset();

            ls = new ListCampos(); 
            lc = new ListCampo();
            lc.NomeCampo = "cmpTpGrupoAtividade";
            lc.TipoCampo = TipoCampo.StringIN;
            lc.Sinal = SinalPesquisa.IN;
            lc.ValorCampo = "'T','E'";
            ls.Add(lc);

            table = tblGrupoAtividade.get(Global.GetConnection(), ls);
            Objetos.LoadCombo(cmbTipoEquip, table, "cmpDcGrupoAtividade", "cmpCoGrupoAtividade", "cmpDcGrupoAtividade", "--- Selecione ---", true);
        }

       /// <summary>
        /// Carrega dados do equipamento
        /// </summary>
        private void loadEquipamento()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();

            lc.NomeCampo = "cmpIdEquipamento";
            lc.TipoCampo = TipoCampo.Numero;
            lc.Sinal = SinalPesquisa.Igual;
            lc.ValorCampo = ViewState["cmpIdEquipamento"].ToString();
            ls.Add(lc);

            using (DataTable table = tblEquipamento.Get(Global.GetConnection(), ls))
            {
                this.TxtDescricao.Text = table.Rows[0]["CmpDcEquipamento"].ToString();
                this.cmbTipoEquip.SelectedIndex = Objetos.RetornaIndiceCombo(cmbTipoEquip, long.Parse(table.Rows[0]["cmpCoGrupoAtividade"].ToString()));
                this.cmbTipoEquip.Enabled = false;
            }
        }

        /// <summary>
        /// Pesquisa.
        /// </summary>
        private DataTable pesquisa()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc;

            if (txtDcEquipamentoPesq.Text != "")
            {
                lc = new ListCampo();
                lc.NomeCampo    = "CmpDcEquipamento";
                lc.TipoCampo    = TipoCampo.Like;
                lc.Sinal        = SinalPesquisa.Like;
                lc.Percent      = TipoPercent.InicioFim;
                lc.ValorCampo   = txtDcEquipamentoPesq.Text.ToString();
                ls.Add(lc);
            }

            if (cmbTipoEquipamento.SelectedValue != "0")
            {
                lc = new ListCampo();
                lc.NomeCampo    = "cmpCoTipoAtividade";
                lc.TipoCampo    = TipoCampo.Numero;
                lc.Sinal        = SinalPesquisa.Igual;
                lc.ValorCampo   = cmbTipoEquipamento.SelectedValue;
                ls.Add(lc);
            }

            return tblEquipamento.Get(Global.GetConnection(), ls);
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                load();
                TxtDescricao.Focus();
            }
        }

        protected void cmbTipoEquipamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPesquisa_Click(sender, e);
        }

        protected void txtDcEquipamento_TextChanged(object sender, System.EventArgs e)
        {
            btnPesquisa_Click(sender, e);
        }

        protected void btnPesquisa_Click(object sender, System.EventArgs e)
        {
            grdPesquisa.DataSource = pesquisa();
            grdPesquisa.DataBind();
        }

        protected void grdPesquisa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdPesquisa.PageIndex = e.NewPageIndex;
                grdPesquisa.DataSource = pesquisa();
                grdPesquisa.DataBind();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        protected void grdPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Cells[3].Visible = false;
                    break;
                case DataControlRowType.DataRow:
                    e.Row.Cells[3].Visible = false;
                    break;
                case DataControlRowType.Footer:
                    e.Row.Cells[3].Visible = false;
                    break;
            }
        }

        protected void lnkIncluir_Click(object sender, EventArgs e)
        {
            ViewState["cmpIdEquipamento"] = 0;

            this.TxtDescricao.Text = "";
            this.cmbTipoEquip.SelectedIndex = 0;
            this.cmbTipoEquip.Enabled = true;

            this.ModalPopupExtender1.Show();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.TxtDescricao.Text = "";
            this.cmbTipoEquip.SelectedIndex = 0;

            this.ModalPopupExtender1.Hide();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            tblEquipamento table = new tblEquipamento();

            table.cmpIdEquipamento      = ViewState["cmpIdEquipamento"].ToString();
            table.CmpDcEquipamento      = TxtDescricao.Text.ToUpper().TrimEnd();
            table.cmpCoGrupoAtividade   = cmbTipoEquip.SelectedValue;
            table.cmpNoUsuario          = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();

            if (table.Save(Global.GetConnection()))
            {
                btnCancel_Click(sender, e);
                txtDcEquipamentoPesq.Text = TxtDescricao.Text.TrimEnd();
                grdPesquisa.DataSource = pesquisa();
                grdPesquisa.DataBind();

                CaixaMensagem.Mostar(Mensagem.Tipo.Sucesso, "Equipamento cadastrado com sucesso!");
            }
            else
            {
                CaixaMensagem.Mostar(Mensagem.Tipo.Erro, "Equipamento já cadastrado.");
            }
        }

        protected void lnkEditar_Click(object sender, EventArgs e)
        {
            LinkButton lnkEditar = sender as LinkButton;

            GridViewRow gvrow = (GridViewRow)lnkEditar.NamingContainer;
            ViewState["cmpIdEquipamento"]       = lnkEditar.CommandArgument.ToString();
            //ViewState["cmpIdTipoEquipamento"]   = gvrow.Cells[3].Text.ToString();

            loadEquipamento();
            this.ModalPopupExtender1.Show();
        }

        protected void lnkExcluir_Click(object sender, EventArgs e)
        {
            LinkButton lnkExcluir = sender as LinkButton;

            GridViewRow gvrow = (GridViewRow)lnkExcluir.NamingContainer;
            ViewState["cmpIdEquipamento"] = lnkExcluir.CommandArgument.ToString();

            tblEquipamento table = new tblEquipamento();
            table.cmpIdEquipamento = ViewState["cmpIdEquipamento"].ToString();
            table.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;

            if (table.Delete(Global.GetConnection()))
            {
                CaixaMensagem.Mostar(Mensagem.Tipo.Sucesso, "Equipamento excluído com sucesso!!!");
                grdPesquisa.DataSource = pesquisa();
                grdPesquisa.DataBind();
            }
            else
            {
                CaixaMensagem.Mostar(Mensagem.Tipo.Erro, "Não é possível excluir o registro. Equipamento vinculado a uma obra!");
            }

        }

        protected void btnNovo_Click(object sender, System.EventArgs e)
        {
            lnkIncluir_Click(sender, e);
        }

   }
}