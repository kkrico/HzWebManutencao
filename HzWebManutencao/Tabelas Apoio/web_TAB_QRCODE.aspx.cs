using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HzWebManutencao.Tabelas_Apoio
{
    public partial class web_TAB_QRCODE : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string valor = Request.QueryString[0];
            string filepath = Server.MapPath("~/QRCODE") + "\\" + valor + ".jpeg";
            imgQRCODE.ImageUrl = "~/QRCODE/" + valor + ".jpeg";
        }
    }
}