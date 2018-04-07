using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibCorporativo.Config;
using HzLibConnection.Sql;
using System.Data;
using HzLibCorporativo.Funcional;

namespace HzWebManutencao.Horizon
{
    public partial class wucObra : System.Web.UI.UserControl
    {
        public string cmpCoObra;
        public string cmpNoObra;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true)
            {

                CarregarDados();

            }
        }
        public void CarregarDados()
        {
            lstPavimentos.Items.Clear();
            lstPavimentosObra.Items.Clear();

            ListCampos lstCampos=new ListCampos();
            ListCampo lstCampo=new ListCampo();
            //lstCampo.NomeCampo="cmpNuOrdenacao";
            //lstCampo.Ordem=TipoOrdem.Ascendente;
            //lstCampos.Add(lstCampo);
            DataTable dtPavimento= tblPavimento.Get(Global.GetConnection(), lstCampos);
            for (int i = 0; i < dtPavimento.Rows.Count; i++)
            {
                lstPavimentos.Items.Add(new ListItem(dtPavimento.Rows[i]["cmpDcPavimento"].ToString(), dtPavimento.Rows[i]["cmpCoPavimento"].ToString()));
            }

            DataTable dtObraPaviemnto = tblObraPavimento.ObraPavimentos(Global.GetConnection(), cmpCoObra);
            for (int i = 0; i < dtObraPaviemnto.Rows.Count; i++)
            {
                lstPavimentosObra.Items.Add(new ListItem(dtObraPaviemnto.Rows[i]["cmpDcPavimento"].ToString(), dtObraPaviemnto.Rows[i]["cmpCoPavimento"].ToString()));
            }

            for (int i = 0; i < lstPavimentos.Items.Count; i++)
            {
                dtObraPaviemnto.DefaultView.RowFilter = "cmpCoPavimento=" + lstPavimentos.Items[i].Value;
                if (dtObraPaviemnto.DefaultView.Count == 1)
                {
                    lstPavimentos.Items.Remove(lstPavimentos.Items[i]);
                }
            }
            
            lstCampos=new ListCampos();
            lstCampo=new ListCampo();
            lstCampo.NomeCampo = "cmpCoObra";
            lstCampo.TipoCampo = TipoCampo.String;
            lstCampo.ValorCampo = cmpCoObra;
            lstCampos.Add(lstCampo);

            DataTable dtObra= tblObra.Get(Global.GetConnection(), lstCampos);

            if (dtObra.Rows.Count > 0)
            {
                txtDescricaoObra.Text = dtObra.Rows[0]["cmpNoObra"].ToString();
            }
        }
        protected void btnAdciona_Click(object sender, EventArgs e)
        {
            if (lstPavimentos.SelectedIndex > -1)
            {
                tblObraPavimento obraPav = new tblObraPavimento();
                obraPav.cmpCoObra = cmpCoObra;
                obraPav.cmpCoPavimento = lstPavimentos.SelectedValue;
                obraPav.Save(Global.GetConnection());

                if (lstPavimentosObra.SelectedIndex > -1) { lstPavimentosObra.SelectedItem.Selected = false; }
                lstPavimentosObra.Items.Add(lstPavimentos.SelectedItem);
                lstPavimentos.Items.Remove(lstPavimentos.SelectedItem);
                lstPavimentos.SelectedIndex = 0;
            }
        }

        protected void btnExclui_Click(object sender, EventArgs e)
        {
            if (lstPavimentosObra.SelectedIndex > -1)
            {
                tblObraPavimento obraPav = new tblObraPavimento();
                obraPav.cmpCoObra = cmpCoObra;
                obraPav.cmpCoPavimento = lstPavimentosObra.SelectedValue;
                obraPav.Delete(Global.GetConnection());

                lstPavimentosObra.Items.Clear();
                lstPavimentos.Items.Clear();
                CarregarDados();
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

        }
    }
}