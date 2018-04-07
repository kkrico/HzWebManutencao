using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibConnection.Sql;
using System.Data;
using HzLibCorporativo.Funcional;
using HzlibWEB;
using HzLibManutencao;

namespace HzWebManutencao.ATE
{
    public partial class webATE_OSPesquisaNOS : System.Web.UI.Page
    {
        #region Functions

        /// <summary>
        /// Carrega as combos.
        /// </summary>
        private void loadObra()
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoLocal";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoLocal;
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "cmpCoUsuario";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoUsuario;
                ls.Add(lc);

                using (DataTable table = tblObraUsuario.Get(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbObra, table, "cmpNoObra", "cmpCoObraGrupoLista", "cmpNoObra", true);
                    if (Session["login"] != null && ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista != null)
                        cmbObra.SelectedIndex = Objetos.RetornaIndiceCombo(cmbObra, int.Parse(((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista));
                    cmbObra_SelectedIndexChanged(cmbObra, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               this.loadObra();
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoObraGrupoLista";
                lc.TipoCampo = TipoCampo.Numero;
                lc.Sinal = SinalPesquisa.Igual;
                lc.ValorCampo = cmbObra.SelectedValue;
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "cmpNuOS";
                lc.TipoCampo = TipoCampo.String;
                lc.Sinal = SinalPesquisa.Igual;
                lc.ValorCampo = txtNuOS.Text;
                ls.Add(lc);

                using (DataTable table = tblOS.Get(Global.GetConnection(), ls))
                {
                    if (table != null && table.Rows.Count > 0)
                    {
                        Response.Redirect("~/ATE/webATE_OS.aspx?id=" + table.Rows[0]["cmpIdOS"], false);
                    }
                    else
                    {
                        Global.ShowMensager(Global.Title,"Número de ordem de serviço não existe para a obra selecionada!");
                    }
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }
        #endregion

        protected void cmbObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista = cmbObra.SelectedValue;
        }

       
    }
}