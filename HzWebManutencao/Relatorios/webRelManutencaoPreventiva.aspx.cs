using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibConnection.Sql;
using System.Data;
using HzlibWEB;
using HzLibCorporativo.Funcional;

namespace HzWebManutencao.Relatorios
{
    public partial class webRelManutencaoPreventiva : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }
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
        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            string nomeRel = "";
            if (rblNumPreventPeriodo.Checked == true)
            {
                string numPreventiva = txtNumPreventiva.Text;

                RelatoriosReportServer.clRelatorios rel = new RelatoriosReportServer.clRelatorios();
                nomeRel = rel.ManutencaoPreventivaNumPreventiva(txtNumPreventiva.Text, txtDataInicial.Text, txtDataFinal.Text);
            }
            if (rblObraPeriodo.Checked==true)
            {
                string numPreventiva = txtNumPreventiva.Text;

                RelatoriosReportServer.clRelatorios rel = new RelatoriosReportServer.clRelatorios();
                nomeRel = rel.ManutencaoPreventivaNumPreventivaObra(int.Parse(cmbObra.SelectedValue.ToString()), TxtdtInicialObra.Text, txtDtFinalObra.Text,cmbObra.SelectedItem.Text);
            }
    

            this.Response.Write("<SCRIPT language=javascript>window.open('../" + nomeRel + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');</SCRIPT>");
        }

        protected void cmbObra_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void rblNumPreventPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            rblObraPeriodo.Checked = false;
            MultiView1.ActiveViewIndex = 1;
        }

        protected void rblObraPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            rblNumPreventPeriodo.Checked = false;
            MultiView1.ActiveViewIndex = 0;
            cmbObra.Items.Clear();
            loadObra();
        }

    }
}