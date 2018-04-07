using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibCorporativo.Horizon;
using System.Data;
using HzLibCorporativo.Funcional;

namespace HzWebManutencao.Horizon
{
    public partial class wucCentroCusto : System.Web.UI.UserControl
    {
        public string cmpCoCentroCusto = "0";
        public string cmpNoCentroCusto;

        protected void Page_Load(object sender, EventArgs e)
        {
             if (IsPostBack == true)
            {
                //if (cmpCoCentroCusto != "0")
                //{
                    CarregarDados();
                //}
            }
        }
        private void CarregarDados()
        {
            DataTable dt = tblObra.Get(Global.GetConnection());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lstObras.Items.Add(new ListItem(dt.Rows[i]["cmpNoObra"].ToString(), dt.Rows[i]["cmpCoObra"].ToString()));
            }

            lstObrasCentroCusto.Items.Clear();
            if (cmpCoCentroCusto != "0")
            {
                DataTable dtCentro = tblCentroCustoObra.ObrasCentroCusto(Global.GetConnection(), cmpCoCentroCusto);
                for (int i = 0; i < dtCentro.Rows.Count; i++)
                {
                    lstObrasCentroCusto.Items.Add(new ListItem(dtCentro.Rows[i]["cmpNoObra"].ToString(), dt.Rows[i]["cmpCoObra"].ToString()));
                }
                for (int i = 0; i < lstObras.Items.Count; i++)
                {
                    dtCentro.DefaultView.RowFilter = "cmpCoObra=" + lstObras.Items[i].Value;
                    if (dtCentro.DefaultView.Count == 1)
                    {
                        lstObras.Items.Remove(lstObras.Items[i]);
                    }
                }
            }
            

            tblCentroCusto cust = new tblCentroCusto();
            cust.Get(Global.GetConnection(), cmpCoCentroCusto);
            txtDescricaoCentroCusto.Text = cust.cmpNoCentroCusto;

        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            tblCentroCusto cust = new tblCentroCusto();
            cust.cmpCoCentroCusto = cmpCoCentroCusto;
            cust.cmpNoCentroCusto = txtDescricaoCentroCusto.Text;
            cust.Save(Global.GetConnection());
            Page.Session["Carregar"]=false;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnAdciona_Click(object sender, EventArgs e)
        {
            tblCentroCustoObra CentroCusto = new tblCentroCustoObra();
            CentroCusto.cmpCoCentroCusto = cmpCoCentroCusto;
            CentroCusto.cmpCoObra = lstObras.SelectedValue.ToString();
            CentroCusto.Save(Global.GetConnection());

            if (lstObrasCentroCusto.SelectedIndex > -1) { lstObrasCentroCusto.SelectedItem.Selected = false; }
            lstObrasCentroCusto.Items.Add(lstObras.SelectedItem);
            lstObras.Items.Remove(lstObras.SelectedItem);
            lstObras.SelectedIndex = 0;
        }

        protected void btnExclui_Click(object sender, EventArgs e)
        {
            if (lstObrasCentroCusto.SelectedIndex > -1)
            {
                tblCentroCustoObra.Delete(Global.GetConnection(), cmpCoCentroCusto, lstObrasCentroCusto.SelectedItem.Value);
                lstObrasCentroCusto.Items.Clear();
                lstObras.Items.Clear();
                CarregarDados();
            }
        }
    }
}