using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibManutencao;

namespace HzWebManutencao.ATE
{
    public partial class webATE_OSAprovacao : System.Web.UI.Page
    {
        private List<STOS> lst { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        protected void btnExecutar_Click(object sender, EventArgs e)
        {
            List<STOS> l = ((List<STOS>)Session["STOS"]).FindAll(delegate(STOS st) { return st.state == "R";});
            string str = "";
            tblOS tbl = new tblOS();
            foreach (STOS st in l)
            {
                tbl.cmpCoObra = st.cmpCoObra;
                str += st.cmpIdOS + ",";
            }
            tbl.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;
            tbl.cmpDcObservacaoConclusao = txtJustificativa.Text;
            tbl.GravarRejeicaoOS(Global.GetConnection(), str);
        }

        protected void rdbAprovar_SelectedIndexChanged(object sender, EventArgs e)
        {
            RequiredFieldValidator1.Enabled = ((RadioButtonList)sender).SelectedValue == "R";
        }
    }
}