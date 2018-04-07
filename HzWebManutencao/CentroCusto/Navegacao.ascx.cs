using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HzWebManutencao.CentroCusto
{
    public partial class Navegacao : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hypCentroCusto.Enabled = false;
            hypObras.Enabled = false;
            hypSistemasPrediais.Enabled = false;
            hypPavimentos.Enabled = false;
            hypAtivos.Enabled = false;

            if (Session["Navegacao"] != null)
            {
                List<string> lstNavegar = (List<string>) Session["Navegacao"];
                if (lstNavegar.Count == 1)
                {
                    hypCentroCusto.Enabled = true;
                    lblDescricao.Text = lstNavegar[0];
                }
                if (lstNavegar.Count == 2)
                {
                    hypCentroCusto.Enabled = true;
                    hypObras.Enabled = true;
                    lblDescricao.Text = lstNavegar[0] + " >> " + lstNavegar[1];
                }
                if (lstNavegar.Count == 3)
                {
                    hypCentroCusto.Enabled = true;
                    hypObras.Enabled = true;
                    hypPavimentos.Enabled = true;
                    lblDescricao.Text = lstNavegar[0] + " >> " + lstNavegar[1] + " >> " + lstNavegar[2];
                }
            }

        }
        
    }
}