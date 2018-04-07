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
using HzLibManutencao;

namespace HzWebManutencao.Preventiva
{
    public partial class webPREAgendaManutencao : System.Web.UI.Page
    {
        DataTable dtPesquisa = new DataTable();
        DataTable dtPesquisaDia = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                ViewState["Data"] = DateTime.Now;
                Calendar1.SelectedDate = DateTime.Now;
                loadObra();
                
                
            }
            if (cmbObra.Items.Count > 0) { pesquisaOsPreventiva(); }
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

        protected void cmbObra_SelectedIndexChanged(object sender, EventArgs e)
        {

            pesquisaOsPreventiva();

            //grdOS.DataSource = table;
            //grdOS.DataBind();

            //if (table != null && table.Rows.Count > 0)
            //{
            //    ViewState["cmpIdOS"] = table.Rows[0]["cmpIdOS"].ToString();
            //    btnAprove.Visible = rdbState.SelectedValue == "G" ? true : false;
            //    ViewState["cmpInLogoObra"] = table.Rows[0]["cmpInLogoObra"].ToString() == "True" ? "1" : "0";
            //}
        }
        private void  pesquisaOsPreventiva()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();

            Calendar1.SelectedDate = (DateTime)ViewState["Data"];

            tblOS tbos = new tblOS();
            dtPesquisa= tbos.OS_Preventivas_Mes(Global.GetConnection(), cmbObra.SelectedValue, Calendar1.SelectedDate);
            
        }
        private void pesquisaOsPreventivaDia()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();

            Calendar1.SelectedDate = (DateTime)ViewState["Data"];

            tblOS tbos = new tblOS();
            dtPesquisaDia = tbos.OS_Preventivas_Dia(Global.GetConnection(), cmbObra.SelectedValue, Calendar1.SelectedDate);

        }
        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            int contPreventivas = 0;
            int contOS = 0;

            dtPesquisa.DefaultView.RowFilter = "cmpDtExecucaoPreventivaAgenda ='" + e.Day.Date.ToShortDateString() + "' and  TipoPedido='Preventiva'";
            //if (dtPesquisa.DefaultView.Count > 0)
            //{
            //    e.Cell.Controls.Add(new LiteralControl("<br /><span class='style1' color:#0066FF;>Preventiva"));
            //}
            for (int i = 0; i < dtPesquisa.Rows.Count; i++)
            {
                DateTime dtime = DateTime.Parse(dtPesquisa.Rows[i]["cmpDtExecucaoPreventivaAgenda"].ToString());
                string tipoAtend = dtPesquisa.Rows[i]["TipoPedido"].ToString();
                if (dtime.Year == e.Day.Date.Year && dtime.Month == e.Day.Date.Month && dtime.Day == e.Day.Date.Day && tipoAtend=="OS")
                {
                    //e.Cell.Controls.Add(new LiteralControl("<br /><span class='style1'>OS"));
                    contOS++;
                    //break;
                }
                if (dtime.Year == e.Day.Date.Year && dtime.Month == e.Day.Date.Month && dtime.Day == e.Day.Date.Day && tipoAtend == "Preventiva")
                {
                    //e.Cell.Controls.Add(new LiteralControl("<br /><span class='style1'>Preventiva"));
                    contPreventivas++;
                    //break;
                }
            }

            if (contPreventivas > 0)
            {
                e.Cell.Controls.Add(new LiteralControl("<br /><span style='color:#009933;'>(" + contPreventivas.ToString() + ")"));
            }
            if (contOS > 0)
            {
                e.Cell.Controls.Add(new LiteralControl("<br /><span style='color:#0066FF;'>(" + contOS.ToString() + ")"));
            }
            e.Cell.Controls.Add(new LiteralControl("<br />  <br/> "));
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            ViewState["Data"] = Calendar1.SelectedDate;
            pesquisaOsPreventivaDia();
            grvAtividades.DataSource = dtPesquisaDia;
            grvAtividades.DataBind();
        }

    }
}