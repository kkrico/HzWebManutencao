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
    public partial class webTABCentrodeCusto : System.Web.UI.Page
    {
        private string cmpCoCentroCusto;
        private string cmpNoCentroCusto;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            tblCentroCusto cust = new tblCentroCusto();
            cust.cmpCoCentroCusto = cmpCoCentroCusto;
            cust.cmpNoCentroCusto = txtDescricaoCentroCusto.Text;
            cust.Save(Global.GetConnection());

        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            tblCentroCusto cust = new tblCentroCusto();
            cust.cmpCoCentroCusto = cmpCoCentroCusto;
            cust.cmpNoCentroCusto = txtDescricaoCentroCusto.Text;
            cust.Save(Global.GetConnection());
        }
    }
}