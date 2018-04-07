using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibCorporativo.Horizon;
using System.Data;
using HzLibCorporativo.Funcional;
using HzLibManutencao;
using HzLibConnection.Sql;

namespace HzWebManutencao.Horizon
{
    public partial class webHorizon : System.Web.UI.Page
    {
        wucCentroCusto userc;
        wucObra userObra;
        wucPavimento usecPavimento;
        wucEquipamento usecEquipamento;

        private enum _Controles {Nenhum, CentroCusto, Area, Equipamento,Pavimento,Obra };
        private _Controles Controles;
        private bool Carregar = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Page.Session.Add("Controle", Controles);
                Page.Session.Add("Carregar", Carregar);
                CarregarDados();
                
            }
            Controles = (_Controles)Page.Session["Controle"];
            Carregar = (bool)Page.Session["Carregar"];
            if (Carregar == true)
            {
                //CarregarDados();
                //Page.Session["Carregar"] = false;
            }
                switch (Controles)
                {
                    case _Controles.CentroCusto:
                                userc = (wucCentroCusto)LoadControl("wucCentroCusto.ascx");
                                if (cmbCentroCusto.SelectedIndex == -1)
                                {
                                    userc.cmpCoCentroCusto = "0";
                                }
                                else { userc.cmpCoCentroCusto = cmbCentroCusto.SelectedValue; }
                                divComponentes.Controls.Add(userc);
                        break;
                    case _Controles.Obra:
                                userObra = (wucObra)LoadControl("wucObra.ascx");
                                userObra.cmpCoObra = TreeView1.SelectedNode.Value;
                                divComponentes.Controls.Add(userObra);
                        break;
                    case _Controles.Area:
                        break;
                    case _Controles.Pavimento:
                                usecPavimento = (wucPavimento)LoadControl("wucPavimento.ascx");
                                usecPavimento.cmpCoPavimento = TreeView1.SelectedNode.Value;
                                divComponentes.Controls.Add(usecPavimento);
                        break;
                    case _Controles.Equipamento:
                        usecEquipamento = (wucEquipamento)LoadControl("wucEquipamento.ascx");
                        usecEquipamento.cmpIdEquipamento = TreeView1.SelectedNode.Value;
                        divComponentes.Controls.Add(usecEquipamento);
                        break;
                    default:
                        break;
                }

            //}
        }

        private void CarregarControles()
        {
            string strNode = TreeView1.SelectedNode.ToolTip;
            switch (strNode)
            {
                case "CentroCusto":

                    break;
                case "Obra":
                    Controles = _Controles.Obra;
                    Page.Session["Controle"] = Controles;
                    userObra = (wucObra)LoadControl("wucObra.ascx");
                    userObra.cmpCoObra = TreeView1.SelectedNode.Value;
                    divComponentes.Controls.Clear();
                    divComponentes.Controls.Add(userObra);
                    break;
                case "Pavimento":
                    Controles = _Controles.Pavimento;
                    Page.Session["Controle"] = Controles;
                    usecPavimento = (wucPavimento)LoadControl("wucPavimento.ascx");
                    usecPavimento.cmpCoPavimento = TreeView1.SelectedNode.Value;
                    divComponentes.Controls.Clear();
                    divComponentes.Controls.Add(usecPavimento);
                    break;
                case "Equipamento":
                    Controles = _Controles.Equipamento;
                    Page.Session["Controle"] = Controles;
                    usecEquipamento = (wucEquipamento)LoadControl("wucEquipamento.ascx");
                    usecEquipamento.cmpIdEquipamento = TreeView1.SelectedNode.Value;
                    divComponentes.Controls.Clear();
                    divComponentes.Controls.Add(usecPavimento);
                    break;
                default:
                    break;
            }

        }
        private void CarregarDados()
        {
            //tblCentroCustocs cust = new tblCentroCustocs();
            //DataTable dt = tblCentroCustocs.GetObraGrupo(Global.GetConnection(),);
            //cmbCentroCusto.Items.Clear();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    cmbCentroCusto.Items.Add(new ListItem(dt.Rows[i]["cmpNoCentroCusto"].ToString(),dt.Rows[i]["cmpCoCentroCusto"].ToString()));
            //}
        }
        protected void bntNovoCentroCusto_Click(object sender, EventArgs e)
        {

        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Page.Session["Carregar"] = true;
            Controles = _Controles.CentroCusto;
            Page.Session["Controle"] = Controles;
            userc = (wucCentroCusto)LoadControl("wucCentroCusto.ascx");
            userc.cmpCoCentroCusto = "0";
            divComponentes.Controls.Add(userc);
        }

        protected void cmbCentroCusto_SelectedIndexChanged(object sender, EventArgs e)
        {
            //divComponentes.Controls.Clear();

            CarregarTreeView(cmbCentroCusto.SelectedValue.ToString());
            Page.Session["Carregar"] = true;
            Controles = _Controles.CentroCusto;
            Page.Session["Controle"] = Controles;
            userc = (wucCentroCusto)LoadControl("wucCentroCusto.ascx");
            userc.cmpCoCentroCusto = cmbCentroCusto.SelectedValue.ToString();
            divComponentes.Controls.Add(userc);
        }
        private void CarregarTreeView(string cmpCoCentroCusto )
        {
            TreeView1.Nodes.Clear();
           DataTable dtCentroCusto= tblCentroCustoObra.ObrasCentroCusto(Global.GetConnection(),cmpCoCentroCusto);
           for (int icenCus = 0; icenCus < dtCentroCusto.Rows.Count; icenCus++)
           {
               TreeNode tnCenCus = new TreeNode(dtCentroCusto.Rows[icenCus]["cmpNoObra"].ToString(), dtCentroCusto.Rows[icenCus]["cmpCoObra"].ToString());
               tnCenCus.ToolTip = "Obra";
               //TreeNode tnCenCus = new TreeNode("<span onclick=showmenuie5(event);>" + dtCentroCusto.Rows[icenCus]["cmpNoObra"].ToString() + " </span>");

               DataTable dtPavimentos = tblObraPavimento.ObraPavimentos(Global.GetConnection(), dtCentroCusto.Rows[icenCus]["cmpCoObra"].ToString());
               for (int iPav = 0; iPav < dtPavimentos.Rows.Count; iPav++)
               {
                   TreeNode tnPav = new TreeNode(dtPavimentos.Rows[iPav]["cmpDcPavimento"].ToString(), dtPavimentos.Rows[iPav]["cmpCoObraPavimento"].ToString());
                   tnPav.ToolTip = "Pavimento";
                   //DataTable dtArea = tblAreaPavimento.RetornaAreaPavimentos(Global.GetConnection(), dtPavimentos.Rows[iPav]["cmpCoObraPavimento"].ToString());

                   //for (int iArea = 0; iArea < dtArea.Rows.Count; iArea++)
                   //{
                   //    TreeNode tnArea = new TreeNode(dtArea.Rows[iArea]["cmpNoArea"].ToString(), dtArea.Rows[iArea]["cmpIdArea"].ToString());
                   //    tnPav.ChildNodes.Add(tnArea);
                   //}
                   
                   ListCampos lstCampos=new ListCampos();
                   ListCampo lstCampo=new ListCampo();
                   lstCampo.NomeCampo="cmpCoObraPavimento";
                   lstCampo.ValorCampo=dtPavimentos.Rows[iPav]["cmpCoObraPavimento"].ToString();
                   lstCampo.TipoCampo = TipoCampo.String;
                   lstCampos.Add(lstCampo);
                   DataTable dtEquip = tblEquipamentoObra.Get(Global.GetConnection(), lstCampos);
                   for (int iEquip = 0; iEquip < dtEquip.Rows.Count; iEquip++)
                   {
                       TreeNode tnEquip = new TreeNode(dtEquip.Rows[iEquip]["cmpDcEquipamentoObra"].ToString(), dtEquip.Rows[iEquip]["cmpIdEquipamentoObra"].ToString());
                       tnEquip.ToolTip = "Equipamento";
                       tnPav.ChildNodes.Add(tnEquip);
                   }
                   tnCenCus.ChildNodes.Add(tnPav);
               }

               TreeView1.Nodes.Add(tnCenCus);
           }
            
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            string strNode = TreeView1.SelectedNode.ToolTip;
            switch (strNode)
            {
                case "CentroCusto":

                    break;
                case    "Obra":
                        Controles = _Controles.Obra;
                        Page.Session["Controle"] = Controles;
                        userObra = (wucObra)LoadControl("wucObra.ascx");
                        userObra.cmpCoObra = TreeView1.SelectedNode.Value;
                        divComponentes.Controls.Clear();
                        divComponentes.Controls.Add(userObra);
                    break;
                case "Pavimento":
                    Controles = _Controles.Pavimento;
                    Page.Session["Controle"] = Controles;
                    usecPavimento = (wucPavimento)LoadControl("wucPavimento.ascx");
                    usecPavimento.cmpCoObraPavimento = TreeView1.SelectedNode.Value;
                    usecPavimento.cmpDcPavimento = TreeView1.SelectedNode.Text;
                    divComponentes.Controls.Clear();
                    divComponentes.Controls.Add(usecPavimento);
                    break;
                case "Equipamento":
                    Controles = _Controles.Equipamento;
                    Page.Session["Controle"] = Controles;
                    usecEquipamento = (wucEquipamento)LoadControl("wucEquipamento.ascx");
                    usecEquipamento.cmpIdEquipamento = TreeView1.SelectedNode.Value;
                    divComponentes.Controls.Clear();
                    divComponentes.Controls.Add(usecEquipamento);
                    break;
                default:
                    break;
            }
        }
    }
}