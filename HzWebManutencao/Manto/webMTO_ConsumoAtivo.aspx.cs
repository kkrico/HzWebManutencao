using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HzLibConnection.Sql;
using HzLibCorporativo.Funcional;
using HzLibCorporativo.Config;

namespace HzWebManutencao.Manto
{
    public partial class webMTO_ConsumoAtivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpCoUsuario";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoUsuario;
            ls.Add(lc);

            using (DataTable table = tblUsuario.Get(Global.GetConnection(), ls))
            {
                if (table != null && table.Rows.Count > 0)
                {
                    string filename = "webMNT_Geral.aspx?tipo=ativo" + "&co_usuario=" + table.Rows[0]["chavemanto"].ToString();
                    Response.Write("<script language='javascript'>"
                          + "window.open('" + @"http://www.manto.com.br/MANTO20/" + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105')"
                          + "</script>");

                    //Response.Write("<script language='javascript'>"
                    //      + "window.open('" + @"http://ww.manto.com.br/MANTO20/" + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105')"
                    //      + "</script>");
                }
            }

        }
    }
}