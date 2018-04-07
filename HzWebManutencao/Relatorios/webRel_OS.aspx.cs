using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HzWebManutencao.Relatorios
{
    public partial class webRel_OS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString != null && Request.QueryString.Count > 0)
                {
                   // ViewState["Relatorio"] = Request.QueryString["NomeRel"];
                    string nomepdf = Server.MapPath("~/Relatorios/" + Request.QueryString["NomeRel"]);
                    Response.Write(nomepdf);
                }
            }

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ATE/webATE_OSPesquisa.aspx", false);
        }
    }
}