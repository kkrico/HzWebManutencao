using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibConnection.Sql;
using System.Data;
using HzLibManutencao;
using HzlibWEB;

namespace HzWebManutencao.Tabelas_Apoio.Preventiva
{
    public partial class webTAB_GrupoAtividade : System.Web.UI.Page
    {
        #region Functions
        /// <summary>
        /// Carrega as listboxes.
        /// </summary>
        private void load()
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoContratante";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoContratante;
                ls.Add(lc);

                using (DataTable table = tblTipoAtividade.Get(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbTipoAtividade, table, "cmpDcTipoAtividade", "cmpCoTipoAtividade", "cmpDcTipoAtividade", true);
                    cmbTipoAtividade_SelectedIndexChanged(cmbTipoAtividade, null);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Carrega as listboxes.
        /// </summary>
        private void loadGrupo(string cmpcotipoatividade)
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoTipoAtividade";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = cmpcotipoatividade;
                ls.Add(lc);

                using (DataTable table = tblGrupoAtividade.get(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(lstGrupoAtividade, table, "cmpDcGrupoAtividade", "cmpCoGrupoAtividade", "cmpDcGrupoAtividade", true);
                    lstGrupoAtividade_SelectedIndexChanged(lstGrupoAtividade, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// Evento de clique do botão.
        /// Habilita o cadastro de um registro novo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            ViewState["cmpCoGrupoAtividade"] = 0;
            txtGrupoAtividade.Text = "";
            txtGrupoAtividade.Focus();
        }

        /// <summary>
        /// Evento do clique do botão.
        /// Armazena o registro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            tblGrupoAtividade table = null;
            try
            {
                table = new tblGrupoAtividade();
                table.cmpCoGrupoAtividade = ViewState["cmpCoGrupoAtividade"].ToString();
                table.cmpCoTipoAtividade = ((ListItem)cmbTipoAtividade.SelectedItem).Value;
                table.cmpDcGrupoAtividade = txtGrupoAtividade.Text.ToUpper();
                table.cmpTpGrupoAtividade = ((ListItem)rdbTpGrupoAtividade.SelectedItem).Value;
                if (table.Save(Global.GetConnection()))
                {
                    this.loadGrupo(((ListItem)cmbTipoAtividade.SelectedItem).Value);
                    btnNew_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Evento do clique do botão.
        /// Exclui um registro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (tblGrupoAtividade.delete(Global.GetConnection(), ViewState["cmpCoGrupoAtividade"].ToString()))
                {
                    this.loadGrupo(((ListItem)cmbTipoAtividade.SelectedItem).Value);
                    btnNew_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Evento de clique do commbox.
        /// Selciona um tipo de atividade.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbTipoAtividade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnNew_Click(sender, e);
                this.loadGrupo(((ListItem)cmbTipoAtividade.SelectedItem).Value);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Evento de clique do listbox.
        /// Carrega os dados do grupo de atividade.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lstGrupoAtividade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoGrupoAtividade";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((ListItem)lstGrupoAtividade.SelectedItem).Value;
                ls.Add(lc);

                using (DataTable table = tblGrupoAtividade.get(Global.GetConnection(), ls))
                {
                    if (table != null && table.Rows.Count > 0)
                    {
                        ViewState["cmpCoGrupoAtividade"] = table.Rows[0]["cmpCoGrupoAtividade"].ToString();
                        rdbTpGrupoAtividade.SelectedIndex = Objetos.RetornaIndiceRadio(rdbTpGrupoAtividade, table.Rows[0]["cmpTpGrupoAtividade"].ToString(), true);
                        txtGrupoAtividade.Text = table.Rows[0]["cmpDcGrupoAtividade"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
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
                this.load();
        }
        #endregion

     }
}