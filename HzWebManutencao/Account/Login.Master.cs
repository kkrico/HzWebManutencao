using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Configuration;

namespace HzWebNumerador.Account
{
    public partial class Login1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string version = System.Reflection.Assembly.GetExecutingAssembly()
                                           .GetName()
                                           .Version
                                           .ToString();
            var connectionString = ConfigurationManager.ConnectionStrings["Horizon"].ConnectionString;
            string BD = "N";
            if(connectionString.IndexOf("prod;")>-1){BD="P";}
            if (connectionString.IndexOf("desenv;") > -1) { BD = "D"; }
            lblVersao.Text = "Versão:" + version + " " + BD;
        }

        protected void lblVersao_Load(object sender, EventArgs e)
        {

        }
    }
}