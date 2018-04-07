using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibConnection.Sql;
using HzLibCorporativo.Funcional;
using HzLibCorporativo.Config;
using HzLibManutencao;
using HzlibWEB;
using HzLibGeneral.Util;

namespace HzWebManutencao.Configuracao
{
    public partial class webCON_UsuCadastro : System.Web.UI.Page
    {
        #region functions
        /// <summary>
        /// Carrega os dados de obra e perfil.
        /// </summary>
        public void load()
        {
            try
            {
                using (DataTable table = tblPerfil.Get(Global.GetConnection()))
                {
                    Objetos.LoadCombo(cmbPerfil, table, "cmpDcPerfil", "cmpCoPerfil", "cmpDcPerfil", "--- Selecione um Perfil ---", true);
                }

                using (DataTable table = tblObra.RetornarObraNOTINUsuario(Global.GetConnection(), ViewState["cmpCoUsuario"].ToString(), ((HzLogin)Session["login"]).cmpCoLocal))
                {
                    Objetos.LoadCombo(lstObra, table, "cmpNoObra", "cmpCoObra", "cmpNoObra", true);
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Carrega a combo com as obras do usuário.
        /// </summary>
        public void loadObraUsuario()
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoUsuario";
                lc.TipoCampo = TipoCampo.Numero;
                lc.Sinal = SinalPesquisa.Igual;
                lc.ValorCampo = ViewState["cmpCoUsuario"].ToString();
                ls.Add(lc);

                using (DataTable table = tblObraUsuario.Get(Global.GetConnection(), ls))
                {
                    if (table != null && table.Rows.Count > 0)
                    {
                        Objetos.LoadCombo(lstObrVinculada, table, "cmpNoObra", "cmpCoObraUsuario", "cmpNoObra", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Carrega os dados do usuário.
        /// </summary>
        public void loadUsuario()
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoUsuario";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ViewState["cmpCoUsuario"].ToString();
                ls.Add(lc);

                using (DataTable table = tblUsuario.Get(Global.GetConnection(), ls))
                {
                    if (table != null && table.Rows.Count > 0)
                    {
                        cmbPerfil.SelectedIndex = Objetos.RetornaIndiceCombo(cmbPerfil, long.Parse(table.Rows[0]["cmpCoPerfil"].ToString()));
                        txtUsuario.Text = table.Rows[0]["cmpNoUsuario"].ToString();
                        txtEmail.Text = table.Rows[0]["cmpDcEmail"].ToString();
                        txtPwd.Attributes.Add("value", table.Rows[0]["cmpDcSenha"].ToString());
                        chkAtivo.Checked = (bool.Parse(table.Rows[0]["cmpInAtivo"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }

        }

        /// <summary>
        /// Carregas as combos e dados do usuário.
        /// </summary>
        private void loadAll()
        {
            this.load();
            this.loadObraUsuario();
            this.loadUsuario();
        }

        /// <summary>
        /// Grava dados do Usuario na HzCorporativo..tblUsuario.
        /// </summary>
        private bool Gravar()
        {
            tblUsuario table = null;
            bool retval = false;

            try
            {
                table = new tblUsuario();
                table.cmpCoUsuario      = ViewState["cmpCoUsuario"].ToString();
                table.cmpDcUsuario      = txtUsuario.Text;
                table.cmpCoPerfil       = cmbPerfil.SelectedValue;
                table.cmpDcEmail        = txtEmail.Text;
                table.cmpInAtivo        = chkAtivo.Checked.ToString();
                table.cmpDcSenha        = txtPwd.Text;
                
                using (DataTable tbl = table.Save(Global.GetConnection()))
                {
                    if (retval = (tbl != null && tbl.Rows.Count > 0))
                        ViewState["cmpCoUsuario"] = tbl.Rows[0]["cmpCoUsuario"];
                }

                return retval;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Evento de carregar a página.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString != null && Request.QueryString.Count > 0)
                    ViewState["cmpCoUsuario"] = Request.QueryString["id"];
                else
                    btnNovo_Click(sender, e);
                this.loadAll();
            }
        }

        /// <summary>
        /// Evento de clique do botão.
        /// Cadastra uma obra para o usuário.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnVincular_Click(object sender, EventArgs e)
        {
            tblObraUsuario table = new tblObraUsuario();
            bool ok = false;
            try
            {
                if (ViewState["cmpCoUsuario"].ToString() == "0")
                    this.Gravar();
                foreach (ListItem item in lstObra.Items)
                {
                    if (item.Selected)
                    {
                        table.cmpCoUsuario = ViewState["cmpCoUsuario"].ToString();
                        table.cmpCoObra = item.Value;
                        table.cmpNoUsuario = ((HzLogin)Session["login"]).cmpNoUsuario;
                        if (!(ok = table.Save(Global.GetConnection())))
                            break;
                    }
                }
                this.loadAll();
                if (!ok)
                    throw new Exception("Erro ao cadastrar a(s) obra(s) do usuário!");
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Evento de clique do botão.
        /// Exclui uma obra do usuário.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDesvincular_Click(object sender, EventArgs e)
        {
            bool ok = false;
            try
            {
                foreach (ListItem item in lstObrVinculada.Items)
                {
                    if (item.Selected)
                    {
                        if (!(ok = tblObraUsuario.Delete(Global.GetConnection(), item.Value, ((HzLogin)Session["Login"]).cmpNoUsuario)))
                            break;
                    }
                }
                this.loadAll();
                if (!ok)
                    throw new Exception("Erro ao excluir a(s) obra(s) do usuário!");
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Evento de clique do botão.
        /// Grava os dados do usuário.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Gravar();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Evento de clique do botão.
        /// Habilita o cadastro de um novo usuário.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNovo_Click(object sender, EventArgs e)
        {
            ViewState["cmpCoUsuario"] = 0;
            txtUsuario.Text = "";
            txtEmail.Text = "";
            txtPwd.Text = "";
            chkAtivo.Checked = true;
            lstObrVinculada.Items.Clear();
        }

        #endregion

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Server.Transfer("webCON_UsuPesquisa.aspx");
        }

        protected void btnRelatorioPerfil_Click(object sender, EventArgs e)
        {
            //string nomeRel;
            //RelatoriosReportServer.clRelatorios rel = new RelatoriosReportServer.clRelatorios();
            //nomeRel = rel.ManutencaoPreventivaNumPreventiva(txtNumPreventiva.Text, txtDataInicial.Text, txtDataFinal.Text);
            //this.Response.Write("<SCRIPT language=javascript>window.open('../" + nomeRel + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');</SCRIPT>");
        }

    }
}