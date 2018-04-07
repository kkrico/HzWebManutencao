using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibManutencao;
using System.Data;

namespace HzWebManutencao.Preventiva
{
    public partial class webPRE_EditarPreventiva : System.Web.UI.Page
    {
        private string cmpDcGrupoAtividade="";
        private string cmpDcTipoAtividade="";
        private string cmpCoObraGrupoLista = "";
        private int intSubTotalIndex;
        private bool Grupo;
        private bool Tipo;
        private bool cabecalho;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                string valorPreventivaAgenda = Request.QueryString["idPreventivaAgenda"];
                string valorCoObraGrupoLista = Request.QueryString["cmpCoObraGrupoLista"];
                Session.Add("idPreventivaAgenda", valorPreventivaAgenda);
                Session.Add("cmpCoObraGrupoLista", valorCoObraGrupoLista);

                Load(int.Parse(valorPreventivaAgenda));
            }
        }
        private void Load(int idPreventivaAgenda)
        {



            //DataTable dtPreventiva = tblPreventivaAgenda.RetornaAgendaPreventivaValores(Global.GetConnection(), idPreventivaAgenda);
            //grdPreventiva.DataSource = dtPreventiva ;
            //grdPreventiva.DataBind();

            DataTable dtEquipamentoObra = HzLibManutencao.tblEquipamentoObra.RetornarEquipamentosAtividade(Global.GetConnection(), Session["cmpCoObraGrupoLista"].ToString(), DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString(), idPreventivaAgenda);
            grdPreventivaEquipamento.DataSource = dtEquipamentoObra;
            grdPreventivaEquipamento.DataBind();

        }
        
    
        

        protected void grdPreventiva_RowCreated(object sender, GridViewRowEventArgs e)
        {
            cabecalho = true;
            if (cabecalho == false)
            {
                if (string.IsNullOrEmpty(cmpDcGrupoAtividade) && DataBinder.Eval(e.Row.DataItem, "cmpDcGrupoAtividade") != null) {  Grupo = true; }
                if (string.IsNullOrEmpty(cmpDcTipoAtividade) && DataBinder.Eval(e.Row.DataItem, "cmpDcTipoAtividade") != null) {  Tipo = true; }

            }
            else
            {
                if (DataBinder.Eval(e.Row.DataItem, "cmpDcGrupoAtividade") != null) 
                {

                    if (cmpDcGrupoAtividade != DataBinder.Eval(e.Row.DataItem, "cmpDcGrupoAtividade").ToString().Trim()) { Grupo = true; }; 
                }
                if (DataBinder.Eval(e.Row.DataItem, "cmpDcTipoAtividade") != null) 
                {
                    if (cmpDcTipoAtividade != DataBinder.Eval(e.Row.DataItem, "cmpDcTipoAtividade").ToString().Trim()) { ; Tipo = true; } 
                }
            }

            if(Grupo==true)
            {
                GridView grdViewOrders = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "Grupo de Atividade : " + DataBinder.Eval(e.Row.DataItem, "cmpDcGrupoAtividade").ToString();
                cell.ColumnSpan = 6;
                cell.CssClass = "GroupHeaderStyle";
                cell.Font.Bold = true;
                row.Cells.Add(cell);
                grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
            }
            if (Tipo == true)
            {
                GridView grdViewOrders = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "Tipo de Atividade:" + DataBinder.Eval(e.Row.DataItem, "cmpDcTipoAtividade").ToString();
                cell.ColumnSpan = 6;
                cell.CssClass = "GroupHeaderStyle";
                cell.Font.Bold = true;
                row.Cells.Add(cell);
                grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
            }

            if (DataBinder.Eval(e.Row.DataItem, "cmpDcGrupoAtividade") != null) { cmpDcGrupoAtividade = DataBinder.Eval(e.Row.DataItem, "cmpDcGrupoAtividade").ToString().Trim(); Grupo = true; }
            if (DataBinder.Eval(e.Row.DataItem, "cmpDcTipoAtividade") != null) { cmpDcTipoAtividade = DataBinder.Eval(e.Row.DataItem, "cmpDcTipoAtividade").ToString().Trim(); Tipo = true; }

            Grupo = false;
            Tipo = false;
           
          
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

         
       
        protected void grdPreventivaEquipamento_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdPreventivaEquipamento_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void grdPreventivaEquipamento_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            DataTable dtPreventiva = tblPreventivaAgenda.RetornaAgendaPreventivaValores(Global.GetConnection(), int.Parse(Session["IdPreventivaAgenda"].ToString()),int.Parse(e.CommandArgument.ToString()));
            grdPreventiva.DataSource = dtPreventiva;
            grdPreventiva.DataBind();

        }

        protected void grdPreventivaEquipamento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPreventivaEquipamento.PageIndex = e.NewPageIndex;
            DataTable dtEquipamentoObra = HzLibManutencao.tblEquipamentoObra.RetornarEquipamentosAtividade(Global.GetConnection(), Session["cmpCoObraGrupoLista"].ToString(), DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString(), int.Parse(Session["IdPreventivaAgenda"].ToString()));
            grdPreventivaEquipamento.DataSource = dtEquipamentoObra;
            grdPreventivaEquipamento.DataBind();
        }


    }
}