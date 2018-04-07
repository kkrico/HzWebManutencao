using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HzLibManutencao;

namespace HzWebManutencao.App_Horizon
{
    public partial class Preventivas_App : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = tblAppPreventiva.Get(Global.GetConnection());
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}