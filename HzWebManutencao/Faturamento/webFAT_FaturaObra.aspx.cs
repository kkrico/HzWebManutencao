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

namespace HzWebManutencao.Faturamento
{
    public partial class webFAT_FaturaObra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                loadObra();
            }
        }
        private void loadObra()
        {
            try
            {
                //ListCampos ls = new ListCampos();
                //ListCampo lc = new ListCampo();
                //lc.NomeCampo = "cmpCoLocal";
                //lc.TipoCampo = TipoCampo.Numero;
                //lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoLocal;
                //ls.Add(lc);

                //lc = new ListCampo();
                //lc.NomeCampo = "cmpCoUsuario";
                //lc.TipoCampo = TipoCampo.Numero;
                //lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoUsuario;
                //ls.Add(lc);

                //HzLibCorporativo.Faturamento.tblObraFatura.Get

                using (DataTable table = HzLibCorporativo.Faturamento.tblObraFatura.Get(Global.GetConnection()))
                {
                    Objetos.LoadCombo(cmbObra, table, "cmpNoObraFatura", "cmpCoObra", "cmpNoObraFatura", true);
                    //if (Session["login"] != null && ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista != null)
                    //    cmbObra.SelectedIndex = Objetos.RetornaIndiceCombo(cmbObra, int.Parse(((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista));
                    //cmbObra_SelectedIndexChanged(cmbObra, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            HzLibCorporativo.Faturamento.tblObraFatura obra = new HzLibCorporativo.Faturamento.tblObraFatura();
            obra.Get(Global.GetConnection(), int.Parse(cmbObra.SelectedValue.ToString()));
            obra.cmpNoObraFatura = txtNomeObra.Text;
            obra.cmpNoOrgao = txtNomeOrgao.Text;
            obra.cmpNoPrimeiroGestor = txtPrimDest.Text;
            obra.cmpNoSegundoGestor = txtSegDest.Text;
            obra.Update(Global.GetConnection());

        }

        protected void cmbObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpCoObra";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = cmbObra.SelectedValue.ToString();
            ls.Add(lc);

           DataTable dtObraFatura= HzLibCorporativo.Faturamento.tblObraFatura.Get(Global.GetConnection(), ls);
           txtNomeObra.Text = "";
           txtNomeOrgao.Text = "";
           txtPrimDest.Text = "";
           txtSegDest.Text = "";

           if (dtObraFatura.Rows.Count == 1)
           {
               txtNomeObra.Text = dtObraFatura.Rows[0]["cmpNoObraFatura"].ToString();
               txtNomeOrgao.Text = dtObraFatura.Rows[0]["cmpNoOrgao"].ToString();
               txtPrimDest.Text = dtObraFatura.Rows[0]["cmpNoPrimeiroGestor"].ToString();
               txtSegDest.Text = dtObraFatura.Rows[0]["cmpNoSegundoGestor"].ToString();
           }

        }

        protected void txtNomeOrgao_TextChanged(object sender, EventArgs e)
        {

        }
    }
}