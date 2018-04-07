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
    public partial class webTAB_ItemAtividade : System.Web.UI.Page
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

                using (DataTable table = tblUnidade.Get(Global.GetConnection()))
                {
                    Objetos.LoadCombo(cmbUnidade, table, "cmpDcUnidade", "cmpCoUnidade", "cmpDcUnidade", true);
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
                    Objetos.LoadCombo(cmbGrupoAtividade, table, "cmpDcGrupoAtividade", "cmpCoGrupoAtividade", "cmpDcGrupoAtividade", true);
                    cmbGrupoAtividade_SelectedIndexChanged(cmbGrupoAtividade, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Carrega as listboxes.
        /// </summary>
        private void loadItem(string cmpcogrupoatividade)
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoGrupoAtividade";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = cmpcogrupoatividade;
                ls.Add(lc);

                using (DataTable table = tblItemAtividade.get(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(lstItemAtividade, table, "cmpDcItemAtividade", "cmpCoItemAtividade", "cmpDcItemAtividade", true);
                    lstItemAtividade.SelectedIndex = lstItemAtividade.Items.IndexOf(lstItemAtividade.Items.FindByValue(table.Rows[0]["cmpCoItemAtividade"].ToString()));
                    lstItemAtividade_SelectedIndexChanged(lstItemAtividade, EventArgs.Empty);
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
            ViewState["cmpCoItemAtividade"] = 0;
            txtItemAtividade.Text = "";
            txtItemAtividade.Focus();
        }

        /// <summary>
        /// Evento do clique do botão.
        /// Armazena o registro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            tblItemAtividade table = null;
            try
            {
                table = new tblItemAtividade();
                table.cmpCoItemAtividade = ViewState["cmpCoItemAtividade"].ToString();
                table.cmpCoGrupoAtividade = ((ListItem)cmbGrupoAtividade.SelectedItem).Value;
                table.cmpDcItemAtividade = txtItemAtividade.Text.ToUpper();
                if (rblValor.Checked == true)
                {
                    table.cmpCoUnidade =double.Parse(cmbUnidade.SelectedValue.ToString());
                    table.cmpValores = true;
                    table.cmpValorMin = int.Parse(txtValorMinimo.Text);
                    table.cmpValorMedio = int.Parse(txtValorMedio.Text);
                    table.cmpValorMaximo = int.Parse(txtValorMaximo.Text);

                }
                else
                {
                    table.cmpValores = false;
                    table.cmpValorMin = 0;
                    table.cmpValorMedio = 0;
                    table.cmpValorMaximo = 0;
                }
                
                if (table.Save(Global.GetConnection()))
                {
                    this.loadItem(((ListItem)cmbGrupoAtividade.SelectedItem).Value);
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
                if (tblItemAtividade.delete(Global.GetConnection(), ViewState["cmpCoItemAtividade"].ToString()))
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
        /// Evento ao carregar a página.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                this.load();
        }

        /// <summary>
        /// Evento de clique do combobox.
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
        /// Evento de clique do combobox.
        /// Carrega os itens do grupo de atividade.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbGrupoAtividade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.loadItem(((ListItem)cmbGrupoAtividade.SelectedItem).Value);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Evento de cilque do listbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lstItemAtividade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpcoitematividade";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((ListItem)lstItemAtividade.SelectedItem).Value;
                ls.Add(lc);

                using (DataTable table = tblItemAtividade.get(Global.GetConnection(), ls))
                {
                    if (table != null && table.Rows.Count > 0)
                    {
                        ViewState["cmpCoItemAtividade"] = table.Rows[0]["cmpCoItemAtividade"];
                        txtItemAtividade.Text = table.Rows[0]["cmpDcItemAtividade"].ToString().Trim();
                        pnlValores.Visible = false;
                        if (table.Rows[0]["cmpValores"] != null && table.Rows[0]["cmpValores"].ToString() == "True")
                        {
                            pnlValores.Visible = true;
                            if (table.Rows[0]["cmpValorMin"] != null) { txtValorMinimo.Text = table.Rows[0]["cmpValorMin"].ToString(); };
                            if (table.Rows[0]["cmpValorMax"] != null) { txtValorMaximo.Text = table.Rows[0]["cmpValorMax"].ToString(); };
                            if (table.Rows[0]["cmpValorMedio"] != null) { txtValorMedio.Text = table.Rows[0]["cmpValorMedio"].ToString(); };
                            if (table.Rows[0]["cmpCoUnidade"] != null)
                            {
                                cmbUnidade.SelectedValue = table.Rows[0]["cmpCoUnidade"].ToString();

                            }
                            else
                            {
                                cmbUnidade.SelectedIndex = 0;
                            }
                            
                            rblValor.Checked = true;
                            rblCheck.Checked = false;
                        }
                        else
                        {
                            rblValor.Checked = false;
                            rblCheck.Checked = true;
                            txtValorMinimo.Text = "";
                            txtValorMaximo.Text = "";
                            txtValorMedio.Text = "";
                        }

                    }
                }
                for (int i = 0; i < 60000; ++i)
                    ; ;
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        protected void rblCheck_CheckedChanged(object sender, EventArgs e)
        {
            pnlValores.Visible = false;
            rblValor.Checked = false;
        }

        protected void rblValor_CheckedChanged(object sender, EventArgs e)
        {
            pnlValores.Visible = true;
            rblCheck.Checked = false;
        }

        protected void cmbUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}