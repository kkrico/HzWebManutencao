using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibCorporativo.Horizon;
using System.Data;
using HzLibConnection.Sql;
using HzLibCorporativo.Funcional;
using System.Drawing;

namespace HzWebManutencao.CentroCusto
{
    public partial class webObra : System.Web.UI.Page
    {
        tblObra Obra = new tblObra();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Session.Add("Obra", null);
                gvdWebObra.DataSource= Pesquisar();
                gvdWebObra.DataBind();

                //DataTable dt = tblAreaNegocio.Get(Global.GetConnection());
                //cmbAreaNegocio.DataSource = dt;
                //cmbAreaNegocio.DataBind();

            }

        }
        private DataTable Pesquisar()
        {
           DataTable dt=  tblObraGrupo.GetObrasGrupo(Global.GetConnection(), Session["CoObraGrupo"].ToString());
            return dt;
        }
        private void CarregarGrid()
        {
          DataTable dt=  tblCentroCustoObra.ObrasCentroCusto(Global.GetConnection(), Session["CoCentroCusto"].ToString());
          gvdWebObra.DataSource = dt;
          gvdWebObra.DataBind();
        }
        protected void btnNovo_Click(object sender, EventArgs e)
        {

        }

        protected void dgvWebObra_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gdvWebObra_RowCommand(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

        }

        protected void Button5_Click(object sender, EventArgs e)
        {

        }

        protected void btnfecharSelectObra_Click(object sender, EventArgs e)
        {

        }

        protected void Button6_Click(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void btnNovo_Click1(object sender, EventArgs e)
        {
            txtDescricaoObra.Text = "";
            txtNumeroObra.Text = "";
            cmbAreaNegocio.SelectedIndex = 0;
            Session["Obra"] = null;
            ModObra.Show();
        }

        protected void btnNovo0_Click(object sender, EventArgs e)
        {
            gvdWebObra.DataSource = Pesquisar(); ;
            gvdWebObra.DataBind();
            modSelecObras.Show();
        }

        protected void btnSalvar_Click1(object sender, EventArgs e)
        {
            bool novo = false;

            if (Session["Obra"] == null)
            {
                Obra = new tblObra();
                Obra.cmpCoObra = "0";
                novo = true;
            }
            else
            {
                Obra =(tblObra) Session["Obra"];
            }
            //Obra.cmpNoObra = txtDescricaoObra.Text;
            //Obra.cmpNuObra = txtNumeroObra.Text;
            //Obra.cmpCoAreaNegocio = cmbAreaNegocio.SelectedValue;
            Obra.Save(Global.GetConnection());

            if (novo == true)
            {
                tblObraGrupoLista obraGrupo = new tblObraGrupoLista();
                obraGrupo.cmpcoObra = int.Parse(Obra.cmpCoObra);
                obraGrupo.cmpCoObraGrupo = int.Parse(Session["CoObraGrupo"].ToString());

                obraGrupo.Salvar(Global.GetConnection());
            }
            gvdWebObra.DataSource = Pesquisar();
            gvdWebObra.DataBind();
        }

        protected void btnFechar_Click(object sender, EventArgs e)
        {

        }

        protected void gvdWebObra_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnFecharSelecObras_Click(object sender, EventArgs e)
        {
            gvdWebObraSelect.DataSource = Pesquisar(); ;
            gvdWebObraSelect.DataBind();
            modSelecObras.Show();
            CarregarGrid();
        }

        protected void gvdWebObra_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Editar")
            {
                Obra = new tblObra(Global.GetConnection(), e.CommandArgument.ToString());
                txtNumeroObra.Text = Obra.cmpNuObra;
                txtDescricaoObra.Text = Obra.cmpNoObra;
                txtNumeroObra.Text = Obra.cmpNuObra;
                cmbAreaNegocio.ClearSelection();// esse comando é valido para a combo
                //if (string.IsNullOrEmpty(Obra.cmpCoAreaNegocio)==false)
                //{
                //    ListItem it=  cmbAreaNegocio.Items.FindByValue(Obra.cmpCoAreaNegocio);
                //    if (it != null)
                //    {

                //        it.Selected = true;
                //    }
                //}
                Session["Obra"] = Obra;
                ModObra.Show();
            }

            if (e.CommandName == "Select")
            {
                List<string> lstNavegacao;
                lstNavegacao = (List<string>)Session["Navegacao"];
                Obra = new tblObra(Global.GetConnection(), e.CommandArgument.ToString());
                lstNavegacao.Add(Obra.cmpNoObra);
                Session["Navegacao"] = lstNavegacao;
                if (Session["cmpCoObra"] == null) { Session.Add("cmpCoObra", e.CommandArgument); }
                Session["cmpCoObra"] = e.CommandArgument;
                Response.Redirect("webSistemasPrediais.aspx");

            }
        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            DataTable dt = tblObraGrupo.GetObrasSemGrupo(Global.GetConnection());
           gvdWebObraSelect.DataSource = dt;
           gvdWebObraSelect.DataBind();
            modSelecObras.Show();
        }

        protected void gvdWebObraSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvdWebObraSelect_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

                if (Session["cmpCoObra"] == null) { Session.Add("cmpCoObra", e.CommandArgument); }
                Session["cmpCoObra"] = e.CommandArgument;

                tblObraGrupoLista tbl = new tblObraGrupoLista();
                tbl.cmpcoObra = int.Parse(e.CommandArgument.ToString());
                tbl.cmpCoObraGrupo = int.Parse(Session["CoObraGrupo"].ToString());
                tbl.Salvar(Global.GetConnection());

                gvdWebObra.DataSource = Pesquisar();
                gvdWebObra.DataBind();
            }
        }
    }
}