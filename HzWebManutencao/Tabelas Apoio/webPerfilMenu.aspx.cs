using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibCorporativo.Config;

namespace HzWebManutencao.Tabelas_Apoio
{
    public partial class webPerfilMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            CarregarMenu();
        }
        private void CarregarMenu()
        {
            tblMenu clMenu = new tblMenu();
            clMenu.listaTblMenus(Global.GetConnection(), 0);
            for (int n1 = 0; n1 < clMenu.lstTblMenu.Count; n1++)
            {
                TreeNode tn1 = new TreeNode();
                tn1.Text = clMenu.lstTblMenu[n1].cmpDcMenu;
                tn1.Value = clMenu.lstTblMenu[n1].cmpCoMenu;
                for (int n2 = 0; n2 < clMenu.lstTblMenu[n1].lstTblMenu.Count; n2++)
                {
                    TreeNode tn2 = new TreeNode();
                    tn2.Text = clMenu.lstTblMenu[n1].lstTblMenu[n2].cmpDcMenu;
                    tn2.Value = clMenu.lstTblMenu[n1].lstTblMenu[n2].cmpCoMenu;
                    tn1.ChildNodes.Add(tn2);
                }
                TreeView1.Nodes.Add(tn1);
            }
        }
    }
}