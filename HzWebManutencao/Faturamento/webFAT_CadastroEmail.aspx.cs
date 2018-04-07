using System;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using HzLibCorporativo.Funcional;
using HzLibCorporativo.Faturamento;
using HzLibCorporativo.Geral;
using HzLibCorporativo.Config;

using HzLibConnection.Sql;
using HzlibWEB;
using Apresentacao.Controles;

namespace HzWebManutencao.Faturamento
{
    public partial class webFAT_CadastroEmail : System.Web.UI.Page
    {
        #region variables

        #endregion

        #region Functions
        /// <summary>
        /// Carrega as combos.
        /// </summary>
        private void load()
        {
            try
            {
                Objetos.LoadCombo(cmbTipoServico, tblTipoServicoObra.Get(Global.GetConnection()), "cmpDcTipoServico", "cmpIdTipoServico", "cmpDcTipoServico", "-- Selecione Área Negocio", true);
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        ///  Retorna obras que não estão vinculadas ao funcionário
        /// </summary>
        private bool CarregaObraNotFuncionario()
        {
            try
            {
                using (DataTable table = tblObraFatura.RetornaObraNotFuncionario(Global.GetConnection(), ViewState["cmpidFaturaEmail"].ToString(),cmbTipoServico.SelectedValue))
                {
                    if (table != null && table.Rows.Count > 0)
                    {
                        Objetos.LoadCombo(lstObras, table, "cmpNoObraFatura", "cmpIdObraFatura", "cmpNoObraFatura", true);
                        lstObras.SelectedIndex = 0;
                        return true;
                    }
                    else
                    {
                        lstObras.Items.Clear();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(ex.ToString(), "Erro");
                return false;
            }
        }

        /// <summary>
        ///   Retorna obras que estão vinculadas ao funcionário
        /// </summary>
        private bool CarregaObraFuncionario()
        {
            try
            {
                using (DataTable table = tblObraFatura.RetornaObraFuncionario(Global.GetConnection(), ViewState["cmpidFaturaEmail"].ToString()))
                {
                    if (table != null && table.Rows.Count > 0)
                    {
                        Objetos.LoadCombo(lstObraFunc, table, "cmpNoObraFatura", "cmpIdObraFatura", "cmpNoObraFatura", true);
                        lstObraFunc.SelectedIndex = 0;
                        return true;
                    }
                    else
                    {
                        lstObraFunc.Items.Clear();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(ex.ToString(), "Erro");
                return false;
            }
        }

        /// <summary>
        /// Pesquisa Dados.
        /// </summary>
        private DataTable pesquisa()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();

            //lc = new ListCampo();
            //lc.NomeCampo = "cmpInAtivo";
            //lc.TipoCampo = TipoCampo.Numero;
            //lc.ValorCampo = "1";
            //ls.Add(lc);

            if (rdbData.SelectedValue == "T")
            {
                lc = new ListCampo();
                lc.NomeCampo    = "cmpInEscritorio";
                lc.TipoCampo    = TipoCampo.StringIN;
                lc.Sinal        = SinalPesquisa.IN;
                lc.ValorCampo   = "0,1";
                ls.Add(lc);

            }
            else 
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpInEscritorio";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = rdbData.SelectedValue;
                ls.Add(lc);
            }

            if (rdbPerfilEmail.SelectedValue != "T")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpNuNivelEmail";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = rdbPerfilEmail.SelectedValue;
                ls.Add(lc);
            }

            return tblFaturaEmail.Get(Global.GetConnection(), ls);
        }

        /// <summary>
        /// Validar campos abertura faturamento
        /// </summary>
        private bool ValidarCampos()
        {
            bool ret = true;
            string msg = "";
            if (txtNomeFuncionario.Text == "")
                msg += "Nome do Funcionário em branco! <br />";
            if (txtDcEmail.Text == "")
                msg += "Endereço de email em branco! <br />";
            if (string.IsNullOrEmpty(rdbPerfil.SelectedValue))
                msg += "Perfil do email em branco! <br />";
            if (msg != "")
            {
                MsgBox.ShowMessage(msg, "Erro");
                ret = false;
            }

            return ret;
        }

        #endregion

        #region events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                load();
                btnPesquisar_Click(sender, null);
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            DataTable tbl = pesquisa();

            if (tbl.Rows.Count > 0)
            {
                double TamDiv = tbl.Rows.Count * 25;
                TamDiv += 75;

                this.divstyle.Style[HtmlTextWriterStyle.Height] = TamDiv.ToString() + "px";
                gvDados.DataSource = tbl;
                gvDados.DataBind();
            }
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            ViewState["cmpidFaturaEmail"] = 0;
            txtNomeFuncionario.Text     = "";
            txtDcEmail.Text             = "";
            rdbPerfil.SelectedIndex     = -1;

            btnDesvincular.Enabled  = false;
            btnVincular.Enabled     = false;

            CarregaObraNotFuncionario();
            lstObraFunc.Items.Clear();

            this.ModalPopupExtender2.Show();
        }

        protected void lnkEditar_Click(object sender, EventArgs e)
        {
            LinkButton lnkEditar = sender as LinkButton;

            GridViewRow gvrow = (GridViewRow)lnkEditar.NamingContainer;
            ViewState["cmpidFaturaEmail"] = lnkEditar.CommandArgument.ToString();

            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpidFaturaEmail";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = ViewState["cmpidFaturaEmail"].ToString();
            ls.Add(lc);

            using (DataTable table = tblFaturaEmail.Get(Global.GetConnection(), ls))
            {
                txtNomeFuncionario.Text     = table.Rows[0]["cmpNoFuncionario"].ToString();
                txtDcEmail.Text             = table.Rows[0]["cmpDcEmail"].ToString();
                rdbPerfil.SelectedValue     = table.Rows[0]["cmpNuNivelEmail"].ToString();
            }

            CarregaObraNotFuncionario();
            CarregaObraFuncionario();

            this.ModalPopupExtender2.Show();
        }

        /// <summary>
        /// Vincular obra ao funcionário
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnVincular_Click(object sender, EventArgs e)
        {
            bool ok = false;
            tblFaturaEmailObraFatura table = new tblFaturaEmailObraFatura();
            try
            {
                foreach (ListItem item in lstObras.Items)
                {
                    if (item.Selected)
                    {
                        table.cmpIdFaturaEmail = ViewState["cmpidFaturaEmail"].ToString();
                        table.cmpIdObraFatura = item.Value;
                        table.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;

                        if (!(ok = table.GravarFaturaEmailObraFuncionario(Global.GetConnection())))
                            throw new Exception("Erro ao cadastrar a(s) obra(s) do funcionário!");
                    }
                }

                CarregaObraNotFuncionario();
                CarregaObraFuncionario();
                this.ModalPopupExtender2.Show();
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(ex.ToString(), "Erro");
            }
        }

        /// <summary>
        /// Desvincular obra do funcionário
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDesvincular_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstObraFunc.Items.Count != 0)
                {
                    tblFaturaEmailObraFatura table = new tblFaturaEmailObraFatura();

                    foreach (ListItem item in lstObraFunc.Items)
                    {
                        if (item.Selected)
                        {
                            table.cmpIdFaturaEmail = ViewState["cmpidFaturaEmail"].ToString();
                            table.cmpIdObraFatura = item.Value.ToString();

                            table.ExcluirFaturaEmailObraFuncionario(Global.GetConnection());
                        }
                    }

                    CarregaObraNotFuncionario();
                    if (lstObraFunc.Items.Count == 1)
                        lstObraFunc.Items.Clear();
                    else
                        CarregaObraFuncionario();
                    this.ModalPopupExtender2.Show();
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(ex.ToString(), "Erro");
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                try
                {
                    tblFaturaEmail table = new tblFaturaEmail();
                    table.cmpIdFaturaEmail  = ViewState["cmpidFaturaEmail"].ToString();
                    table.cmpNoFuncionario  = txtNomeFuncionario.Text.ToString();
                    table.cmpDcEmail        = txtDcEmail.Text.ToString();
                    table.cmpNuNivelEmail   = rdbPerfil.SelectedValue.ToString();
                    table.cmpNoUsuario      = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();

                    table.Save(Global.GetConnection());
                    MsgBox.ShowMessage("Registro atualizado com sucesso!", "Aviso");
                    btnPesquisar_Click(sender, e);

                    btnDesvincular.Enabled = true;
                    btnVincular.Enabled = true;
                    this.ModalPopupExtender2.Show();
                }
                catch (Exception ex)
                {
                    Global.ShowError(Global.Title, ex);
                }
            }
        }

        protected void btnPesqAreaNegocio_Click(object sender, EventArgs e)
        {
            CarregaObraNotFuncionario();
            this.ModalPopupExtender2.Show();
        }

        protected void lnkExcluir_Click(object sender, EventArgs e)
        {
            LinkButton lnkExcluir = sender as LinkButton;

            GridViewRow gvrow = (GridViewRow)lnkExcluir.NamingContainer;
            ViewState["cmpidFaturaEmail"] = lnkExcluir.CommandArgument.ToString();

            tblFaturaEmail table    = new tblFaturaEmail();
            table.cmpIdFaturaEmail  = ViewState["cmpidFaturaEmail"].ToString();
            table.cmpNoUsuario      = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;

            if (table.Delete(Global.GetConnection()))
            {
                MsgBox.ShowMessage("Registro excluído com sucesso!", "Aviso");
                btnPesquisar_Click(sender, e);
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            this.ModalPopupExtender2.Hide();
        }

        protected void gvDados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                 case DataControlRowType.DataRow:
                    //if (e.Row.Cells[5].Text.Equals("&nbsp;"))
                    //{
                    //    e.Row.Cells[5].Font.Bold.ToString();
                    //    e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
                    //    e.Row.Cells[5].Text = "Escritório";
                    //}

                    switch (e.Row.Cells[4].Text.ToString())
                    {
                        case "1":
                            e.Row.Cells[4].Font.Bold.ToString();
                            e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                            e.Row.Cells[4].Text = "Diretor";
                            break;
                        case "2":
                            e.Row.Cells[4].Font.Bold.ToString();
                            e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                            e.Row.Cells[4].Text = "Gerente";
                            break;
                        case "3":
                            e.Row.Cells[4].Font.Bold.ToString();
                            e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                            e.Row.Cells[4].Text = "Coordenador";
                            break;
                        case "4":
                            e.Row.Cells[4].Text = "Engenheiro Responsável Obra";
                            break;
                        case "5":
                            e.Row.Cells[4].Font.Bold.ToString();
                            e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                            e.Row.Cells[4].Text = "Setor Faturamento";
                            break;
                        case "6":
                            e.Row.Cells[4].Text = "Assistente Administrativo";
                            break;

                    }
                    break;

                case DataControlRowType.Footer:
                    break;
            }

        }

        #endregion

 
    }
}