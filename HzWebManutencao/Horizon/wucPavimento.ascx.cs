using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HzLibConnection.Sql;
using HzLibManutencao;

namespace HzWebManutencao.Horizon
{
    public partial class wucPavimento : System.Web.UI.UserControl
    {
        public string cmpCoObraPavimento;
        public string cmpCoPavimento;
        public string cmpDcPavimento;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true)
            {
                CarregarDados();
            }
        }
        private void CarregarDados()
        {
            ListCampos lstCampos = new ListCampos();
            ListCampo lstCampo = new ListCampo();
            lstCampo.NomeCampo = "cmpCoObraPavimento";
            lstCampo.ValorCampo = cmpCoObraPavimento;
            lstCampo.TipoCampo = TipoCampo.String;
            lstCampos.Add(lstCampo);
            DataTable dtEquip = tblEquipamentoObra.Get(Global.GetConnection(), lstCampos);
            grvEquipamentos.DataSource = dtEquip;
            grvEquipamentos.DataBind();

            txtPavimento.Text = cmpDcPavimento;
        }
    }
}