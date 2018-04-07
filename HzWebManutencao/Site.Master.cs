using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HzLibConnection.Sql;
using HzLibCorporativo.Funcional;
using HzlibWEB;
using HzLibGeneral.Util;

namespace HzWebManutencao
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        #region Functions
        private void loadMenu()
        {
            try
            {
                if (Session["menu"] == null)
                {
                    List<MenuItem> menu = new List<MenuItem>();
                    string sql =    "select * from vwREL_PerfilMenu" + 
                                    " where cmpIdSistema = 1 and cmpNuPerfil = " + ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNuPerfil +
                                    " order by cmpCoMenuPai, cmpNuOrdemApresentacao, cmpNuPerfil, cmpNuOrdem ";
                    DataTable table = Global.GetConnection().loadDataTable(sql);
                    foreach (DataRow r in table.Rows)
                    {
                        if (r["cmpCoMenuPai"].ToString() == "")
                            menu.Add(new MenuItem(r["cmpDcMenu"].ToString().Trim(), r["cmpCoMenu"].ToString(), "", r["cmpDcArquivo"].ToString().Trim()));
                        else
                        {
                            MenuItem m = menu.Find(delegate(MenuItem mn) { return mn.Value.ToString() == r["cmpCoMenuPai"].ToString(); });
                            if (m != null)
                                m.ChildItems.Add(new MenuItem(r["cmpDcMenu"].ToString().Trim(), r["cmpCoMenu"].ToString(), "", r["cmpDcArquivo"].ToString().Trim()));
                        }
                    }
                    Session["menu"] = menu;
                }
                this.NavigationMenu.Items.Clear();
                for (int i = 0; i < ((List<MenuItem>)Session["menu"]).Count; ++i)
                    this.NavigationMenu.Items.Add(((List<MenuItem>)Session["menu"])[i]);
            }
            catch
            {
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.loadMenu();

            if (!Page.IsPostBack)
            {
                if (Session["login"] == null)
                    Response.Redirect("~/Account/Login.aspx?page=" + HttpContext.Current.Request.FilePath, false);

                //else
                //{
                    //ListCampos ls = new ListCampos();
                    //ListCampo lc = new ListCampo();
                    //lc.NomeCampo = "cmpCoPerfil";
                    //lc.TipoCampo = TipoCampo.Numero;
                    //lc.ValorCampo = ((HzLogin)Session["login"]).cmpCoPerfil;
                    //ls.Add(lc);

                    //lc = new ListCampo();
                    //lc.NomeCampo = "cmpDcArquivo";
                    //lc.TipoCampo = TipoCampo.Like;
                    //lc.Sinal = SinalPesquisa.Like;
                    //lc.Percent = TipoPercent.InicioFim;
                    //lc.ValorCampo = HttpContext.Current.Request.FilePath;
                    //ls.Add(lc);
                    //string sql = "select * from vwREL_PerfilMenu" +
                    //                " where cmpIdSistema = 1 and cmpNuPerfil = " + ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNuPerfil +
                    //                " and cmpDcArquivo like '%" + HttpContext.Current.Request.FilePath + "%'";
                    //using (DataTable table = Global.GetConnection().loadDataTable(sql))
                    //{
                    //    if (table == null || table.Rows.Count < 1)
                    //        Response.Redirect("~/ATE/webATE_OS.aspx");
                    //}
                //}
            }
        }
     
    }
}
