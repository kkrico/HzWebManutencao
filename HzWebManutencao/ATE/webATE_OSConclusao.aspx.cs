using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using HzLibConnection.Sql;
using HzLibManutencao;
using HzLibCorporativo;
using HzLibCorporativo.Funcional;
using Apresentacao.Controles;

namespace HzWebManutencao.ATE
{
    public partial class webATE_OSConclusao : System.Web.UI.Page
    {
        [ScriptService]
        public class AutoComplete : System.Web.Services.WebService
        {
            [WebMethod]
            public String[] RetornaFuncionarioObra()
            {
                List<String> res = new List<String>();

                using (DataTable table = tblObraFuncionario.Get(Global.GetConnection(), "36"))
                {
                    foreach (DataRow row in table.Rows)
                    {
                        res.Add(table.Rows[0]["cmpNoFuncionario"].ToString() + ",");
                    }
                }

                HttpContext.Current.Cache["Funcionarios"] = res.ToArray();

                return res.ToArray();
            }

            //[WebMethod]
            //[System.Web.Script.Services.ScriptMethod]
            //public string[] GetCompletionList(string prefixText, int count, string contextKey)
            //{

            //    return " ";
            //}

        }
        
        #region Functions
        protected string TiraCaractEspecial(string Campo)
        {
            Campo = Campo.Replace("\r", string.Empty);
            Campo = Campo.Replace("\n", string.Empty);
            return Campo;
        }

        private bool GravarOS()
        {
            tblOS table = new tblOS();
            bool retval = true;

            try
            {
                table.cmpIdOS                   = ViewState["id"].ToString();
                table.cmpDtInicioAtendimento    = TxtInicioAtendimento.Text + " " + txtHoraIni.Text + ":00";
                table.cmpDtConclusaoAtendimento = TxtConclusaoAtendimento.Text + " " + txtHoraFim.Text + ":00";
                table.cmpNoAtestador            = TxtAtestador.Text;
                table.cmpNoExecutor             = TxtExecutor.Text;
                table.cmpDcObservacaoConclusao  = TiraCaractEspecial(txtObservacaoConclusao.Text);
                table.cmpInSatisfacaoCliente    = rbSatisfacaoCliente.SelectedValue;
                table.cmpNoUsuario              = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;
                table.cmpNuMatTecRespCEF        = txtMatriculaGestor.Text;

                retval = table.GravarConclusaoOS(Global.GetConnection());

                // Gera arquivo anexo, com dados da conclusão da O.S, e envia por email ao cliente.
                if (retval && ViewState["EdArqAnexo"].ToString() != "")
                {
                    table.cmpNuDemandaCliente   = ViewState["cmpNuDemandaCliente"].ToString();
                    table.cmpNuOS               = ViewState["cmpNuOs"].ToString();
                    table.cmpNuObra             = ViewState["cmpNuObra"].ToString();
                    table.cmpNoArquivoEnviar    = ViewState["cmpNuDemandaCliente"].ToString().PadLeft(20,'0') + "_3.txt";
                    table.cmpEdArqAnexoCliente  = ViewState["EdArqAnexo"].ToString();
                    table.cmpEeCliente          = ViewState["cmpEeCliente"].ToString();
                    table.cmpDtAbertura         = DateTime.Now.ToString();

                    table.GerarArqProtocoloCEF("3");
                    table.EnviarEmailCliente(Global.GetConnection());
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
                retval = false;
            }
            return retval;
        }

        private void load()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpIdOS";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = ViewState["id"].ToString();
            ls.Add(lc);

            using (DataTable table = tblOS.Get(Global.GetConnection(), ls))
            {
                lblAcao.Text = "Concluir a Ordem de Serviço Número ==> " + table.Rows[0]["cmpNuOS"].ToString();
                if(ViewState["tipo"].ToString() == "TI")
                    TxtObra.Text = table.Rows[0]["cmpDcLocal"].ToString();
                else
                    TxtObra.Text = table.Rows[0]["cmpNoObra"].ToString();

                ViewState["cmpNuDemandaCliente"]    = table.Rows[0]["cmpNuDemandaCliente"].ToString();
                ViewState["cmpNuOs"]                = table.Rows[0]["cmpNuOs"].ToString();
                ViewState["cmpNuObra"]              = table.Rows[0]["cmpNuObra"].ToString();
                ViewState["cmpEeCliente"]           = table.Rows[0]["cmpEeCliente"].ToString();

                //DynamicPopulate populate =  new DynamicPopulate();
                //populate.cmpCoObra = table.Rows[0]["cmpCoObra"].ToString();

                TxtObra.ReadOnly = true;

                if (table.Rows[0]["cmpDtInicioAtendimento"].ToString() != "")
                {
                    TxtInicioAtendimento.Text       = (table.Rows[0]["cmpDtInicioAtendimento"].ToString()).Substring(1, 10);
                    txtHoraIni.Text                 = (table.Rows[0]["cmpDtInicioAtendimento"].ToString()).Substring(11, 5);
                    TxtConclusaoAtendimento.Text    = (table.Rows[0]["cmpDtConclusaoAtendimento"].ToString() == "" ? "" : table.Rows[0]["cmpDtConclusaoAtendimento"].ToString().Substring(1, 10));
                    txtHoraFim.Text                 = (table.Rows[0]["cmpDtConclusaoAtendimento"].ToString() == "" ? "" : table.Rows[0]["cmpDtConclusaoAtendimento"].ToString().Substring(11, 5));
                    TxtAtestador.Text               = table.Rows[0]["cmpNoAtestador"].ToString();
                    TxtExecutor.Text                = table.Rows[0]["cmpNoExecutor"].ToString();
                    txtObservacaoConclusao.Text     = table.Rows[0]["cmpDcObservacaoConclusao"].ToString();
                    txtMatriculaGestor.Text         = table.Rows[0]["cmpNuMatTecRespCEF"].ToString();
                }
            }
        }

        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString != null && Request.QueryString.Count > 0)
                {
                    ViewState["id"]         = Request.QueryString["id"];
                    ViewState["tipo"]       = Request.QueryString["Tp"];
                    ViewState["EdArqAnexo"] = Request.QueryString["Anexo"];
                    this.load();
                    TxtInicioAtendimento.Focus();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.GravarOS())
                if (ViewState["tipo"].ToString() == "TI")
                    Response.Redirect("~/ATE/webATE_ServicosTI.aspx?id=" + ViewState["id"].ToString() + "&Concluido=Sim", false);
                else
                    Response.Redirect("~/ATE/webATE_OS.aspx?id=" + ViewState["id"].ToString() + "&Concluido=Sim", false);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            if (ViewState["tipo"].ToString() == "TI")
                Response.Redirect("~/ATE/webATE_ServicosTI.aspx?id=" + ViewState["id"].ToString(), false);
            else
                Response.Redirect("~/ATE/webATE_OS.aspx?id=" + ViewState["id"].ToString(), false);
        }
        #endregion


    }
}