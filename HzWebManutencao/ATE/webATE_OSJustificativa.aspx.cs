using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibManutencao;
using HzLibConnection.Sql;

namespace HzWebManutencao.ATE
{
    public partial class webATE_OSAprovacao : System.Web.UI.Page
    {
        private List<STOS> lst { get; set; }

        #region Function
        public void loadOS()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpIdOS";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = ViewState["cmpIdOS"].ToString();
            ls.Add(lc);

            using (DataTable table = tblOS.Get(Global.GetConnection(), ls))
            {
                txtObra.Text = table.Rows[0]["cmpNoObra"].ToString();
                txtObra.ReadOnly = true;
                
                switch (ViewState["cmpStOS"].ToString())
                {
                    case "R":
                        lblAcao.Text = "Justificar a Reprovação da Ordem de Serviço Número ==> " + table.Rows[0]["cmpNuOS"].ToString();
                        break;
                    case "C":
                        lblAcao.Text = "Justificar o Cancelamento da Ordem de Serviço Número ==> " + table.Rows[0]["cmpNuOS"].ToString();
                        break;
                    case "E":
                        lblAcao.Text = "Justificar a Exclusão da Ordem de Serviço Número ==> " + table.Rows[0]["cmpNuOS"].ToString();
                        break;
                    case "B":
                        lblAcao.Text = "Justificar a Reabertura da Ordem de Serviço Número ==> " + table.Rows[0]["cmpNuOS"].ToString();
                        break;
                }
            }

        }
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString != null && Request.QueryString.Count > 0)
                {
                    ViewState["cmpIdOS"] = Request.QueryString["id"];
                    ViewState["cmpStOS"] = Request.QueryString["Sit"];
                    loadOS();
                }
            }
        }

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ATE/webATE_OS.aspx?id=" + ViewState["cmpIdOS"].ToString(), false);
        }

        protected void btnExecutar_Click(object sender, EventArgs e)
        {
            try
            {
                tblOS tbl = null;
                tbl = new tblOS();
                tbl.cmpIdOS = ViewState["cmpIdOS"].ToString();
                tbl.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;
                tbl.cmpDcObservacaoConclusao = txtJustificativa.Text;

                switch (ViewState["cmpStOS"].ToString())
                {
                    case "R":
                        List<STOS> l = ((List<STOS>)Session["STOS"]).FindAll(delegate(STOS st) { return st.state == "R"; });
                        string str = "";
                        foreach (STOS st in l)
                        {
                            tbl.cmpCoObra = st.cmpCoObra;
                            str += st.cmpIdOS + ",";
                        }
                        tbl.GravarRejeicaoOS(Global.GetConnection(), str);
                        break;
                    case "E":
                        if (tbl.ExcluirOS(Global.GetConnection()))
                            Global.ShowMensager(Global.Title, "Ordem de serviço excluída.");
                        break;
                    case "C":
                        if (tbl.GravarCancelamentoOS(Global.GetConnection()))
                            Global.ShowMensager(Global.Title, "Ordem de serviço cancelada.");
                        break;
                    case "B":
                        if (tbl.ReabrirOS(Global.GetConnection()))
                            Global.ShowMensager(Global.Title, "Ordem de serviço reaberta.");
                        break;
                }
                Response.Redirect("~/ATE/webATE_OS.aspx?id=" + ViewState["cmpIdOS"].ToString(), false);
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }          

        }
    }
}