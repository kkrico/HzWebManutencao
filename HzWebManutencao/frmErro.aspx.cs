using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace HzWebManutencao
{
    public partial class frmErro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString != null && Request.QueryString.Count > 0)
                {
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-disposition", "filename=" + Request.QueryString["file"]);
                    Response.TransmitFile(ConfigurationManager.AppSettings["RelPdf"] + Request.QueryString["id"]);
                    Response.End();
                    Response.Flush();
                    Response.Clear();
                }
            }
        }
    }
}