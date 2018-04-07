using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HzLibConnection.Sql;
using HzLibCorporativo.Config;
using HzlibWEB;
using HzLibGeneral.Util;
using HzWebManutencao;
using HzLibCorporativo.Geral;

namespace HzWebNumerador.Account
{
    public partial class Login : System.Web.UI.Page
    {
        #region Functions
        /// <summary>
        /// Valida um usuário do Manto.
        /// </summary>
        /// <param name="id">Chave primária do usuário na tabela usuaro do Manto.</param>
        private void checkUserManto(string chavemanto)
        {
            try
            {
                Session["Login"] = null;
                string p = ViewState["page"] != null ? ViewState["page"].ToString() : "~/Account/Login.aspx";

                using (DataTable table = Global.CheckUserManto(chavemanto))
                {
                    ViewState["table"] = table;

                    if (table != null || table.Rows.Count > 0)
                    {
                        txtEmail.Text = table.Rows[0]["cmpDcEmail"].ToString().Trim();
                        Objetos.LoadCombo(cmbContratante, table, "cmpNoContratante", "cmpCoContratante", "cmpNoContratante", true);
                        cmbContratante_SelectedIndexChanged(cmbContratante, EventArgs.Empty);
                        this.showContratante(true);
                        if (cmbLocal.Items.Count == 1)
                            btnConect_Click(btnConect, EventArgs.Empty);
                    }
                }

                if (ViewState["table"] == null || ((DataTable)ViewState["table"]).Rows.Count < 1)
                    Global.ShowError(Global.Title, "Usuário inválido!");
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Exibe/Inibe os dados da empresa.
        /// </summary>
        /// <param name="b"></param>
        private void showContratante(bool b)
        {
            lblContratante.Visible = b;
            cmbContratante.Visible = b;
            lblEmpresa.Visible = b;
            cmbEmpresa.Visible = b;
            lblLocal.Visible = b;
            cmbLocal.Visible = b;
            btnConect.Visible = b;
            rfdEmpresa.Enabled = b;

            rfdEmail.Enabled = !b;
            rfdSenha.Enabled = !b;
        }

        /// <summary>
        /// Carrega os dados do login para o objeto de login.
        /// </summary>
        /// <param name="table">Tabela com os dados do login do usuário.</param>
        /// <param name="cmpnoempresa">Nome da empresa.</param>
        /// <param name="cmpcoempresa">Chave primária da tabela da empresa.</param>
        private void loadLogin(DataTable table, string cmpnoempresa, string cmpcoempresa, string cmpcolocal)
        {
            try
            {
                HzLogin login = new HzLogin();
                if (table != null)
                {
                    login.cmpCoContratante = table.Rows[0]["cmpCoContratante"].ToString();
                    login.cmpCoPerfil = table.Rows[0]["cmpCoPerfil"].ToString();
                    login.cmpCoUsuario = table.Rows[0]["cmpCoUsuario"].ToString();
                    login.cmpDcEmail = table.Rows[0]["cmpDcEmail"].ToString();
                    login.cmpDcPerfil = table.Rows[0]["cmpDcPerfil"].ToString();
                    login.cmpNoUsuario = table.Rows[0]["cmpNoUsuario"].ToString();
                    login.cmpNuPerfil = table.Rows[0]["cmpNuPerfil"].ToString();
                    login.cmpNoEmpresa = cmpnoempresa == "" ? table.Rows[0]["cmpNoEmpresa"].ToString() : cmpnoempresa;
                    login.cmpCoEmpresa = cmpcoempresa == "" ? table.Rows[0]["cmpCoEmpresa"].ToString() : cmpcoempresa;
                    login.cmpCoLocal = cmpcolocal == "" ? table.Rows[0]["cmpCoLocal"].ToString() : cmpcolocal;
                    login.cmpNoLocal = cmpcolocal != "" ? table.Rows[0]["cmpNoLocal"].ToString() : "";
                    Session["Login"] = login;

                    tblLogAtividade tbl = new tblLogAtividade();
                    tbl.cmpCoContratante = login.cmpCoContratante;
                    tbl.cmpDcLogAtividade = "Login do usuário " + login.cmpNoUsuario;
                    tbl.cmpNoTabela = "tblLogAtividade";
                    tbl.cmpNoUsuario = login.cmpNoUsuario; ;
                    tbl.cmpTpLogAtividade = "I";
                    tbl.Save(Global.GetConnection());
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }
        #endregion

        #region Events

        /// <summary>
        /// Evento de clique do botão.
        /// Conecta o usuário.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConect_Click(object sender, EventArgs e)
        {
            try
            {
                string p;
                DataTable table = (DataTable)ViewState["table"];

                if (table.Rows[0]["cmpCoPerfil"].ToString() == "7") // Operador Escritório ORION
                    p = ViewState["page"] != null ? ViewState["page"].ToString() : "~/ATE/webATE_ServicosTI.aspx";
                else
                    p = ViewState["page"] != null ? ViewState["page"].ToString() : "~/ATE/webATE_OS.aspx";

                this.loadLogin((DataTable)ViewState["table"], ((ListItem)cmbEmpresa.SelectedItem).Text, ((ListItem)cmbEmpresa.SelectedItem).Value, ((ListItem)cmbLocal.SelectedItem).Value);

                Response.Redirect(p, false);
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Evento de clique do botão.
        /// Pesquisa os dados do usuário.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Session["Login"] = null;
                string p = ViewState["page"] != null ? ViewState["page"].ToString() : "~/Account/Login.aspx";

                using (DataTable table = Global.CheckUserContratante(txtEmail.Text, txtPassword.Text))
                {
                    ViewState["table"] = table;

                    if (table != null || table.Rows.Count > 0)
                    {
                        Objetos.LoadCombo(cmbContratante, table, "cmpNoContratante", "cmpCoContratante", "cmpNoContratante", true);
                        cmbContratante_SelectedIndexChanged(cmbContratante, EventArgs.Empty);
                        this.showContratante(true);
                        if (cmbLocal.Items.Count == 1)
                            btnConect_Click(btnConect, EventArgs.Empty);
                    }
                }

                if (ViewState["table"] == null || ((DataTable)ViewState["table"]).Rows.Count < 1)
                    Global.ShowError(Global.Title, "Usuário inválido!");
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Evento de seleciona ro combobox.
        /// Exibe as empresas dos contratantes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbContratante_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoContratante";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((ListItem)cmbContratante.SelectedItem).Value;
                ls.Add(lc);

                using (DataTable table = tblEmpresa.Get(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbEmpresa, table, "cmpNoEmpresa", "cmpCoEmpresa", "cmpNoEmpresa", true);
                    cmbEmpresa_SelectedIndexChanged(cmbEmpresa, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }


        /// <summary>
        /// Evento de seleciona ro combobox.
        /// Verifica a empresa desejada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoEmpresa";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((ListItem)cmbEmpresa.SelectedItem).Value;
                ls.Add(lc);

                using (DataTable table = tblLocal.Get(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbLocal, table, "cmpNoLocal", "cmpCoLocal", "cmpNoLocal", true);
                    cmbLocal_SelectedIndexChanged(cmbLocal, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Evento de seleciona ro combobox.
        /// Verifica a empresa desejada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbLocal.Items.Count > 0)
                {
                    using (DataTable table = tblUsuarioEmpresa.RetornaDadosLoginUsuario(Global.GetConnection(), ((ListItem)cmbLocal.SelectedItem).Value, ((DataTable)ViewState["table"]).Rows[0]["cmpCoUsuario"].ToString()))
                    {
                        ViewState["table"] = table;

                        if (table == null || table.Rows.Count < 1)
                            throw new Exception("Usuário inválido!");
                    }
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Evento ao carregar a página.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["login"] = null;
                Session["menu"] = null;
                this.showContratante(false);

                // Endereço dos relatórios gerados
                Global.UrlRel();

                if (Request.QueryString != null && Request.QueryString.Count > 0)
                {
                    ViewState["page"] = Request.QueryString["page"];

                    if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")
                        this.checkUserManto(Request.QueryString["id"]);
                }
            }
        }
        #endregion
    }
}