using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HzLibManutencao;
using HzLibConnection.Sql;
using HzLibCorporativo.Funcional;

namespace HzWebManutencao.CentroCusto
{
    public partial class webSistemasPrediais : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Pesquisar();
            }
        }
        private void Pesquisar()
        {
            string CoObraGrupoLista;
            if (Session["CoObraGrupoLista"] != null)
            {
                CoObraGrupoLista = tblObraGrupoLista.RetornaCoObraGrupoLista(Global.GetConnection(), Session["cmpCoObra"].ToString());
                Session["CoObraGrupoLista"] = CoObraGrupoLista;
            }
            else
            {
                CoObraGrupoLista = tblObraGrupoLista.RetornaCoObraGrupoLista(Global.GetConnection(), Session["cmpCoObra"].ToString());
                Session.Add("CoObraGrupoLista", CoObraGrupoLista);
            }


            DataTable dt = tblObraTipoAtividade.GETTipoAtividade(Global.GetConnection(), CoObraGrupoLista);
            grdSistPredial.DataSource = dt;
            grdSistPredial.DataBind();
        }
        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            

            DataTable dt = tblObraTipoAtividade.AtividadeSemGrupo(Global.GetConnection(), Session["CoObraGrupoLista"].ToString());

            //gvdSelecSistemasPrediais.DataSource = dt;
            //gvdSelecSistemasPrediais.DataBind();

            dgvSistPredial.DataSource = dt;
            dgvSistPredial.DataBind();

            modSistPredial.Show();
        }

        protected void btnPavimentos_Click(object sender, EventArgs e)
        {
            Response.Redirect("webPavimentos.aspx");
        }

        protected void grdSistPredial_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvdSelecSistemasPrediais_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                tblObraTipoAtividade tblObraTipo = new tblObraTipoAtividade();
                tblObraTipo.cmpCoObraGrupoLista = int.Parse(Session["CoObraGrupoLista"].ToString());
                tblObraTipo.cmpCoTipoAtividade = int.Parse(e.CommandArgument.ToString());
                tblObraTipo.Save(Global.GetConnection());
                Pesquisar();
            }
        }

        protected void dgvSistPredial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                tblObraTipoAtividade tblObraTipo = new tblObraTipoAtividade();
                tblObraTipo.cmpCoObraGrupoLista = int.Parse(Session["CoObraGrupoLista"].ToString());
                tblObraTipo.cmpCoTipoAtividade = int.Parse(e.CommandArgument.ToString());
                tblObraTipo.Save(Global.GetConnection());

                Pesquisar();
            }

        }

        protected void dgvSistPredial_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}