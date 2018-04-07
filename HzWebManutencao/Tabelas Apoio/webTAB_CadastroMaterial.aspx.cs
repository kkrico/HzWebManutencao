using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzlibWEB;
using HzLibConnection.Sql;
using HzLibManutencao;

namespace HzWebManutencao.Configuracao
{
    public partial class webCON_CadastroMaterial : System.Web.UI.Page
    {
        #region Funções

                /// <summary>
        /// Carrega as combos de tipo, atividade e forma de solicitação.
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

                using (DataTable table = tblUnidade.Get(Global.GetConnection()))
                {
                    Objetos.LoadCombo(cmbUnidade, table, "cmpDcUnidade", "cmpCoUnidade", "cmpDcUnidade", "--- Selecione ---", true);
                }

                using (DataTable table = tblTipoMaterial.Get(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbTipo, table, "cmpDescricaoMaterial", "cmpCoTipoMaterial", "cmpDescricaoMaterial", "--- Selecione ---", true);
                }

            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }
        private bool PesquisaMaterial()
        {
            try
            {
                using (DataTable table = tblMaterial.PesquisaMaterial(Global.GetConnection(), txtItemMaterial.Text))
                {
                    if (table != null && table.Rows.Count > 0)
                    {
                        Objetos.LoadCombo(LstMaterial, table, "cmpDcMaterial", "cmpCoMaterial", "cmpDcMaterial", true);
                        LstMaterial.SelectedIndex = Objetos.RetornaIndiceCombo(LstMaterial, long.Parse(table.Rows[0]["cmpCoMaterial"].ToString()));
                        LstMaterial_SelectedIndexChanged(LstMaterial, EventArgs.Empty);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
                return false;
            }
        }

        private void CarregaDadosMaterial()
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoMaterial";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ViewState["cmpCoMaterial"].ToString();
                ls.Add(lc);

                using (DataTable table = tblMaterial.Get(Global.GetConnection(), ls))
                {
                    txtItMaterial.Enabled = false;
                    txtItMaterial.Text = table.Rows[0]["cmpDcItemMaterial"].ToString();
                    txtDcMaterial.Text = table.Rows[0]["cmpDcMaterial"].ToString();
                    txtPrecoMaterial.Text = table.Rows[0]["cmpVlPrecoUnitario"].ToString();
                    cmbUnidade.SelectedIndex = Objetos.RetornaIndiceCombo(cmbUnidade, long.Parse(table.Rows[0]["cmpCoUnidade"].ToString()));
                    cmbTipo.SelectedIndex = Objetos.RetornaIndiceCombo(cmbTipo, long.Parse(table.Rows[0]["cmpCoTipoMaterial"].ToString()));
                }

            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                load();
                txtItemMaterial.Focus();
            }
        }

        protected void LstMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["cmpCoMaterial"] = LstMaterial.SelectedValue;
            CarregaDadosMaterial();
        }

        protected void btnPesqMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtItemMaterial.Text.Length >= 3)
                {
                     if (!PesquisaMaterial())
                        Global.ShowMensager(Global.Title, "Material não encontrado.");
                }
                else
                {
                    Global.ShowMensager(Global.Title, "Informe no mínimo 3 digitos para pesquisa!");
                    txtItemMaterial.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Prepara a página para um novo registro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNovo_Click(object sender, EventArgs e)
        {
            ViewState["cmpCoMaterial"] = 0;
            txtItMaterial.Enabled = true;
            txtItMaterial.Text = "";
            txtDcMaterial.Text = "";
            txtPrecoMaterial.Text = "";
            cmbUnidade.SelectedIndex = 0;
            cmbTipo.SelectedIndex = 0;
        }

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            tblMaterial table = null;

            try
            {
                table = new tblMaterial();
                table.cmpCoMaterial         = ViewState["cmpCoMaterial"].ToString();
                table.cmpCoTipoMaterial     = cmbTipo.SelectedValue;
                table.cmpCoUnidade          = cmbUnidade.SelectedValue;
                table.cmpDcItemMaterial     = txtItMaterial.Text;
                table.cmpDcMaterial         = txtDcMaterial.Text;
                table.cmpVlPrecoUnitario    = txtPrecoMaterial.Text.Replace(",",".");
                table.cmpNoUsuario          = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;

                if (table.GravarMaterial(Global.GetConnection()))
                {
                    txtItMaterial.Enabled = false;
                    PesquisaMaterial();
                    LstMaterial.SelectedIndex = int.Parse(ViewState["cmpCoMaterial"].ToString());
                    Global.ShowMensager(Global.Title, "Material cadastrado.");
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, "Material já cadastrado.");
            }
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            tblMaterial table = null;

            try
            {
                table = new tblMaterial();
                table.cmpCoMaterial = ViewState["cmpCoMaterial"].ToString();
                table.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;

                if (table.ExcluirMaterial(Global.GetConnection()))
                {
                    txtItMaterial.Enabled = false;
                    PesquisaMaterial();
                    Global.ShowMensager(Global.Title, "Material excluído.");
                }
            }
            catch (Exception ex)
            {
                Global.ShowMensager(Global.Title, "Material não excluído. Erro de integridade.");
                //Global.ShowError(Global.Title, ex);
            }

        }
    }
}