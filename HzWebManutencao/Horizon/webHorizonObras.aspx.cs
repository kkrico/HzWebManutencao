using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HzLibCorporativo.Horizon;
using HzlibWEB;
using HzLibCorporativo.Funcional;

namespace HzWebManutencao.Horizon
{
    public partial class webHorizonObras : System.Web.UI.Page
    {
        wucCentroCusto userc;
        wucObra userObra;
        wucPavimento usecPavimento;
        wucEquipamento usecEquipamento;
        wucEquipamentosObra usecEquipamentoObra;

        private enum _Controles { Nenhum, CentroCusto, Area, Equipamento, Pavimento, Obra , EquipamentoObra};
        private _Controles Controles;
        private bool Carregar = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Page.Session.Add("Controle", Controles);
                Page.Session.Add("Carregar", Carregar);
                Load();
            }
            Carregar = (bool)Page.Session["Carregar"];
        }
        private void Load()
        {
           int coUsuario = int.Parse(((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoUsuario);
           DataTable table = tblCentroCusto.GetObraGrupo(Global.GetConnection(),coUsuario);
           Objetos.LoadCombo(cmbCentroCusto, table, "cmpDcObraGrupo", "cmpCoObraGrupo", "cmpDcObraGrupo", true);
           if (table.Rows.Count > 0)
           {
               cmbCentroCusto.SelectedIndex = 0;
               int centroCusto = int.Parse(cmbCentroCusto.SelectedValue.ToString());
               DataTable tbObra = tblObra.RetornarGrupoObra(Global.GetConnection(), centroCusto);
               CarregarObras(tbObra);
           }
        }

        protected void cmbCentroCusto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int centroCusto= int.Parse(cmbCentroCusto.SelectedValue.ToString());
            DataTable table = tblObra.RetornarGrupoObra(Global.GetConnection(), centroCusto);
            CarregarObras(table);
        }
        private void CarregarObras(DataTable Obras)
        {
            trvObras.Nodes.Clear();
            for (int icenCus = 0; icenCus < Obras.Rows.Count; icenCus++)
            {
                TreeNode tnCenCus = new TreeNode(Obras.Rows[icenCus]["cmpNoObra"].ToString(), Obras.Rows[icenCus]["cmpCoObra"].ToString());
                tnCenCus.Expanded = false;
                tnCenCus.ToolTip = "Obra";
                CarregarPavimentos(tnCenCus, Obras.Rows[icenCus]["cmpCoObra"].ToString());
                trvObras.Nodes.Add(tnCenCus);
            }
        }
        private void CarregarPavimentos(TreeNode nod, string cmpCoObra)
        {
            DataTable dtPavimentos = tblObraPavimento.ObraPavimentos(Global.GetConnection(), cmpCoObra.ToString());
            for (int i = 0; i < dtPavimentos.Rows.Count; i++)
            {
                TreeNode tnPav = new TreeNode(dtPavimentos.Rows[i]["cmpDcPavimento"].ToString(), dtPavimentos.Rows[i]["cmpCoObraPavimento"].ToString());
                CarregarAtividadesPavimento(tnPav, dtPavimentos.Rows[i]["cmpCoObraPavimento"].ToString());
                tnPav.Expanded = false;
                tnPav.ToolTip = "Pavimento";
                nod.ChildNodes.Add(tnPav);
            }

        }
        private void CarregarAtividadesPavimento(TreeNode nod, string cmpCoObraPavimento)
        {
            DataTable dtPavimentos = tblObraPavimento.AtividadesPavimentos(Global.GetConnection(), cmpCoObraPavimento);
            for (int i = 0; i < dtPavimentos.Rows.Count; i++)
            {
                TreeNode tnPav = new TreeNode(dtPavimentos.Rows[i]["cmpDcTipoAtividade"].ToString(), dtPavimentos.Rows[i]["cmpCoTipoAtividade"].ToString());
                CarregarSubgrupos(tnPav, cmpCoObraPavimento, dtPavimentos.Rows[i]["cmpCoTipoAtividade"].ToString());
                tnPav.Expanded = false;
                tnPav.ToolTip = "AtvPavimento";
                nod.ChildNodes.Add(tnPav);
            }
        }

        private void CarregarSubgrupos(TreeNode nod, string cmpCoObraPavimento, string cmpCoTipoAtividade)
        {
            DataTable dtPavimentos = tblObraPavimento.EquipamentosPavimentos(Global.GetConnection(), cmpCoObraPavimento, cmpCoTipoAtividade);
            for (int i = 0; i < dtPavimentos.Rows.Count; i++)
            {
                TreeNode tnPav = new TreeNode(dtPavimentos.Rows[i]["CmpDcEquipamento"].ToString(), dtPavimentos.Rows[i]["cmpIdEquipamento"].ToString());
                tnPav.Expanded = false;
                tnPav.ToolTip = "EquipPavimento";
                nod.ChildNodes.Add(tnPav);
            }
        }
        protected void btnNovo_Click(object sender, EventArgs e)
        {
            CarregarControles("CentroCusto");
        }
        private void CarregarControles(string strNode)
        {
            Page.Session["Carregar"] = true;
            switch (strNode)
            {
                case "CentroCusto":
                    Controles = _Controles.CentroCusto;
                    Page.Session["Controle"] = Controles;
                    userc = (wucCentroCusto)LoadControl("wucCentroCusto.ascx");
                    divComponentes.Controls.Clear();
                    divComponentes.Controls.Add(userc);
                    break;
                case "Obra":
                    Controles = _Controles.Obra;
                    Page.Session["Controle"] = Controles;
                    userObra = (wucObra)LoadControl("wucObra.ascx");
                    userObra.cmpCoObra = trvObras.SelectedNode.Value;
                    divComponentes.Controls.Clear();
                    divComponentes.Controls.Add(userObra);
                    break;
                case "Pavimento":
                    Controles = _Controles.Pavimento;
                    Page.Session["Controle"] = Controles;
                    usecPavimento = (wucPavimento)LoadControl("wucPavimento.ascx");
                    usecPavimento.cmpCoPavimento = trvObras.SelectedNode.Value;
                    divComponentes.Controls.Clear();
                    divComponentes.Controls.Add(usecPavimento);
                    break;
                case "EquipPavimento":
                    Controles = _Controles.EquipamentoObra;
                    Page.Session["Controle"] = Controles;
                    usecEquipamentoObra = (wucEquipamentosObra)LoadControl("wucEquipamentosObra.ascx");
                    string[] valores = trvObras.SelectedNode.ValuePath.Split('/');
                    
                    usecEquipamentoObra.cmpIdEquipamento = int.Parse(valores[3]);
                    usecEquipamentoObra.cmpCoObraPavimento = int.Parse(valores[1]);
                    usecEquipamentoObra.cmpCoGrupoAtividade = int.Parse(valores[2]);
                    usecEquipamentoObra.PesquisarEquipamentos();
                    divComponentes.Controls.Clear();
                    divComponentes.Controls.Add(usecEquipamentoObra);
                    break;
                default:
                    break;
            }

        }

        protected void trvObras_SelectedNodeChanged(object sender, EventArgs e)
        {
            string strNode = trvObras.SelectedNode.ToolTip;
            CarregarControles(strNode);
        }
    }
}