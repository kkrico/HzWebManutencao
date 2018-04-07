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
    public partial class webTAB_TipoAtividade : System.Web.UI.Page
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
                    Objetos.LoadCombo(lstTipoAtividade, table, "cmpDcTipoAtividade", "cmpCoTipoAtividade", "cmpDcTipoAtividade", true);
                    lstTipoAtividade_SelectedIndexChanged(lstTipoAtividade, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Evento de clique do listbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lstTipoAtividade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoTipoAtividade";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((ListItem)lstTipoAtividade.SelectedItem).Value;
                ls.Add(lc);

                using (DataTable table = tblTipoAtividade.Get(Global.GetConnection(), ls))
                {
                    if (table != null && table.Rows.Count > 0)
                    {
                        ViewState["cmpCoTipoAtividade"] = table.Rows[0]["cmpCoTipoAtividade"].ToString();
                        txtTipoAtividade.Text = table.Rows[0]["cmpDcTipoAtividade"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Evento de clique do botão.
        /// Habilita o cadastro de um registro novo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            ViewState["cmpCoTipoAtividade"] = 0;
            txtTipoAtividade.Text = "";
            txtTipoAtividade.Focus();
        }

        /// <summary>
        /// Evento do clique do botão.
        /// Armazena o registro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            tblTipoAtividade table = null;
            try
            {
                table = new tblTipoAtividade();
                table.cmpCoContratante = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoContratante;
                table.cmpCoTipoAtividade = ViewState["cmpCoTipoAtividade"].ToString();
                table.cmpDcTipoAtividade = txtTipoAtividade.Text.ToUpper();
                if (table.Save(Global.GetConnection()))
                {
                    this.load();
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
                if (tblTipoAtividade.Delete(Global.GetConnection(), ViewState["cmpCoTipoAtividade"].ToString()))
                {
                    this.load();
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
        #endregion

    }
}