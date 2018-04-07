using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HzLibCorporativo.Funcional;
using HzLibCorporativo.Config;

namespace HzWebManutencao.CentroCusto
{
    public partial class webPavimentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                if (Session["cmpCoObra"] != null)
                {
                    Pesquisar();
                }
            }
            else
            {
                //modSelecPavimentos.Show();
            }
        }
        private void Pesquisar()
        {
            DataTable dt = tblObraPavimento.ObraPavimentos(Global.GetConnection(), Session["cmpCoObra"].ToString());
            grdPavimentos.DataSource = dt;
            grdPavimentos.DataBind();
        }
        protected void btnNovo0_Click(object sender, EventArgs e)
        {
            modSelecPavimentos.Show();
        }

        protected void grdPavimentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                //Response.Redirect("webSistemasPrediais.aspx.aspx");

            }

            if (e.CommandName == "Select")
            {
                if (Session["CoObraPavimento"] == null) { Session.Add("CoObraPavimento", e.CommandArgument); }
                List<string> lstNavegacao = new List<string>();

                tblPavimento pav = new tblPavimento(Global.GetConnection(), int.Parse(e.CommandArgument.ToString()));

                lstNavegacao.Add(pav.cmpDcPavimento);
                Session["Navegacao"] = lstNavegacao;

                Session["CoObraPavimento"] = e.CommandArgument;
                Response.Redirect("webAtivoObra.aspx");

            }
        }

        protected void grdPavimentos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            DataTable dt = tblObraPavimento.PavimentoSemGrupo(Global.GetConnection(), Session["cmpCoObra"].ToString());
            grvPavimentosSelec.DataSource = dt;
            grvPavimentosSelec.DataBind();
            modSelecPavimentos.Show();
        }

         protected void grvPavimentosSelec_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                tblObraPavimento obrapav = new tblObraPavimento();
                obrapav.cmpCoObra = Session["cmpCoObra"].ToString();
                obrapav.cmpCoPavimento = e.CommandArgument.ToString();
                obrapav.Save(Global.GetConnection());
                Pesquisar();
            }

        }

         protected void LinkButton3_Click(object sender, EventArgs e)
        {

        }

         protected void btnNovo_Click(object sender, EventArgs e)
         {

         }

         protected void btnSalvarNovo_Click(object sender, EventArgs e)
         {
             tblPavimento pav = new tblPavimento();
             
         }
    }
}