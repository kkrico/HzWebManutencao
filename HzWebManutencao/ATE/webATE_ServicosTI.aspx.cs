using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HzLibConnection.Sql;
using HzLibCorporativo.Funcional;
using HzLibManutencao;
using HzLibGeneral.Util;
using HzlibWEB;

using sharpPDF;
using sharpPDF.Enumerators;
using sharpPDF.Tables;

using HzWebManutencao.Classes.Relatorios;
using Apresentacao.Controles;

namespace HzWebManutencao.ATE
{
    public partial class webATE_ServicosTI : System.Web.UI.Page
    {
        #region variables
        static string prevPage = string.Empty;
        #endregion

        #region Functions

        /// <summary>
        /// Carrega dados da OS.
        /// </summary>
        private void loadOS()
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpIdOS";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ViewState["cmpIdOS"].ToString();
                ls.Add(lc);

                using (DataTable table = tblOS.Get(Global.GetConnection(), ls))
                {
                    cmbLocal.SelectedIndex = Objetos.RetornaIndiceCombo(cmbLocal, long.Parse(loadObraOS(table.Rows[0]["cmpDcLocal"].ToString())));
                    cmbLocal_SelectedIndexChanged(cmbLocal, EventArgs.Empty);
                    cmbOrigemOS.SelectedIndex = Objetos.RetornaIndiceCombo(cmbOrigemOS, long.Parse(table.Rows[0]["cmpCoOrigemOS"].ToString()));
                    cmbFormaSolicitacao.SelectedIndex = Objetos.RetornaIndiceCombo(cmbFormaSolicitacao, long.Parse(table.Rows[0]["cmpIdFormaSolicitacao"].ToString()));
                    cmbTipoAtividade.SelectedIndex = Objetos.RetornaIndiceCombo(cmbTipoAtividade, long.Parse(table.Rows[0]["cmpCoTipoAtividade"].ToString()));
                    cmbTipoAtiviade_SelectedIndexChanged(cmbTipoAtividade, EventArgs.Empty);
                    cmbSolicitacao.SelectedIndex = Objetos.RetornaIndiceCombo(cmbSolicitacao, long.Parse(table.Rows[0]["cmpCoDescricaoSolicitacao"].ToString()));

                    txtSolicitante.Text     = table.Rows[0]["cmpNoSolicitante"].ToString().Trim();
                    txtTelefone.Text        = table.Rows[0]["cmpNuTelefone"].ToString().Trim();
                    txtEmail.Text           = table.Rows[0]["cmpEeEmail"].ToString().Trim();
                    txtObservacoes.Text     = table.Rows[0]["cmpDcObservacoes"].ToString().Trim();
                    ViewState["cmpIdOS"]    = table.Rows[0]["cmpIdOs"].ToString();

                    if ((pnlConclusao.Visible = table.Rows[0]["cmpStOS"].ToString() == "S")) // OS Concluída
                    {
                        txtDtInicio.Text    = (DateTime.Parse(table.Rows[0]["cmpDtInicioAtendimento"].ToString()).ToString());
                        txtConclusao.Text   = (DateTime.Parse(table.Rows[0]["cmpDtConclusaoAtendimento"].ToString()).ToString());
                        txtAtestado.Text    = table.Rows[0]["cmpNoAtestador"].ToString();
                        txtConcluido.Text   = table.Rows[0]["cmpNoExecutor"].ToString();
                        txtObs.Text         = table.Rows[0]["cmpDcObservacaoConclusao"].ToString();
                        if (table.Rows[0]["cmpInSatisfacaoCliente"].ToString() != "")
                            lblSatisfacao.Text = table.Rows[0]["cmpInSatisfacaoCliente"].ToString() == "True" ? "SIM" : "NÃO";
                    }

                    if ((table.Rows[0]["cmpStOS"].ToString() == "C" || table.Rows[0]["cmpStOS"].ToString() == "R")) // OS Cancelada ou Não Aprovada
                    {
                        pnlJustificativa.Visible = true;
                        TxtJustificativa.Text = table.Rows[0]["cmpDcObservacaoConclusao"].ToString();
                    }

                    controlaSituacaoOS(table.Rows[0]["cmpStOS"].ToString());

                    lblNumeroOS.Text = "Ordem de Serviço Número ==> " + table.Rows[0]["cmpNuOS"].ToString();

                    txtDtAbertura.Text = table.Rows[0]["cmpDtAbertura"].ToString();
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Carrega as combos de tipo, atividade e forma de solicitação.
        /// </summary>
        private void load()
        {
            try
            {
                using (DataTable table = tblFormaSolicitacao.Get(Global.GetConnection()))
                {
                    Objetos.LoadCombo(cmbFormaSolicitacao, table, "cmpDcFormaSolicitacao", "cmpIdFormaSolicitacao", "cmpDcFormaSolicitacao", "--- Selecione uma Forma de Solicitação ---", true);
                }

                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoContratante";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoContratante;
                ls.Add(lc);

                using (DataTable table = tblOrigemOS.Get(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbOrigemOS, table, "cmpDcOrigemOS", "cmpCoOrigemOS", "cmpDcOrigemOS", "--- Selecione um Tipo ---", true);
                }

                ls = new ListCampos();
                lc = new ListCampo();
                lc.NomeCampo = "cmpDcTipoAtividade";
                lc.TipoCampo = TipoCampo.String;
                lc.ValorCampo = "SERVIÇOS DE TI";
                ls.Add(lc);

                using (DataTable table = tblTipoAtividade.Get(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbTipoAtividade, table, "cmpDcTipoAtividade", "cmpCoTipoAtividade", "cmpDcTipoAtividade", "--- Selecione um Tipo de Atividade ---", true);
                    cmbTipoAtiviade_SelectedIndexChanged(cmbTipoAtividade, EventArgs.Empty);
                }

                txtEmail.Text = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpDcEmail;

            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Carrega combo de obras do banco HzCorporativo.
        /// </summary>
        private void loadObraSolicitante()
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoLocal";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoLocal;
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "cmpCoUsuario";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoUsuario;
                ls.Add(lc);

                using (DataTable table = tblObraUsuario.Get(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbLocal, table, "cmpNoObra", "cmpCoObraGrupoLista", "cmpNoObra", true);
                    if (Session["login"] != null && ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista != null)
                        cmbLocal.SelectedIndex = Objetos.RetornaIndiceCombo(cmbLocal, int.Parse(((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista));
                    cmbLocal_SelectedIndexChanged(cmbLocal, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Carrega combo de obras do banco HzCorporativo.
        /// </summary>
        private string loadObra()
        {
            ListCampos ls   = new ListCampos();
            ListCampo lc    = new ListCampo();
            lc.NomeCampo    = "cmpNoObra";
            lc.TipoCampo    = TipoCampo.String;
            lc.ValorCampo   = "SERVIÇOS DE TI";
            ls.Add(lc);

            DataTable table = tblObraGrupoLista.Get(Global.GetConnection(), ls);

            return table.Rows[0]["cmpCoObraGrupoLista"].ToString();
        }

        /// <summary>
        /// Carrega a combo da tabela tblObraPavimento do banco HzCorporativo.
        /// </summary>
        private string loadObraPavimento()
        {
            ListCampos ls   = new ListCampos();
            ListCampo lc    = new ListCampo();
            lc.NomeCampo    = "cmpCoObraGrupoLista";
            lc.TipoCampo    = TipoCampo.Numero;
            lc.ValorCampo   = loadObra();
            ls.Add(lc);

            DataTable table = tblObraPavimento.Get(Global.GetConnection(), ls);
            if (table.Rows.Count > 0)
                ViewState["cmpInLogoObra"] = table.Rows[0]["cmpInLogoObra"].ToString() == "True" ? "1" : "0";

            return table.Rows[0]["cmpCoObraPavimento"].ToString();
        }

        /// <summary>
        /// Carrega combo de obras do banco HzCorporativo.
        /// </summary>
        private string loadObraOS(string Local)
        {
            ListCampos ls = new ListCampos();
            ListCampo lc  = new ListCampo();
            lc.NomeCampo  = "cmpNoObra";
            lc.TipoCampo  = TipoCampo.String;
            lc.ValorCampo = Local;
            ls.Add(lc);

            DataTable table = tblObraGrupoLista.Get(Global.GetConnection(), ls);

            return table.Rows[0]["cmpCoObraGrupoLista"].ToString();
        }

        /// <summary>
        /// Controla o estado dos botões.
        /// </summary>
        private void controlaPerfilUsuario()
        {
            switch (((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNuPerfil.ToString())
            {
                case "1": // Administrador
                    btnConcluir.Visible         = true;
                    btnExecucao.Visible         = true;
                    btnEnviarAprovacao.Visible  = true;
                    break;
            }
        }

        /// <summary>
        /// Grava dados da OS na tblOS.
        /// </summary>
        private bool GravarOS()
        {
            tblOS table = null;
            bool retval = true;

            try
            {
                if (retval)
                {
                    table = new tblOS();
                    table.cmpIdOS                       = ViewState["cmpIdOS"].ToString();
                    table.cmpCoDescricaoSolicitacao     = cmbSolicitacao.SelectedValue;
                    table.cmpCoObra                     = loadObra();
                    table.cmpCoObraPavimento            = loadObraPavimento();
                    table.cmpCoOrigemOS                 = cmbOrigemOS.SelectedValue;
                    table.cmpDcLocal                    = cmbLocal.SelectedItem.ToString().TrimEnd();
                    table.cmpNoSetor                    = "";
                    table.cmpIdFormaSolicitacao         = cmbFormaSolicitacao.SelectedValue;
                    table.cmpDcObservacoes              = txtObservacoes.Text.TrimEnd();
                    table.cmpNoSolicitante              = txtSolicitante.Text.TrimEnd();
                    table.cmpEeEmail                    = txtEmail.Text.TrimEnd();
                    table.cmpNuTelefone                 = txtTelefone.Text.TrimEnd();
                    table.cmpNoUsuario                  = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();
                    table.cmpIdPreventivaConfirmacao    = ViewState["cmpIdPreventivaAgenda"].ToString();
                    table.cmpNuDemandaCliente           = "";

                    using (DataTable tbl = table.GravarOS(Global.GetConnection()))
                    {
                        if (tbl != null && tbl.Rows.Count > 0)
                        {
                            lblNumeroOS.Text = "Ordem de Serviço Número ==> " + tbl.Rows[0][0].ToString();
                            ViewState["cmpIdOS"] = tbl.Rows[0]["cmpIdOS"];
                            txtDtAbertura.Text = tbl.Rows[0]["cmpDtAbertura"].ToString();
                            controlaSituacaoOS(tbl.Rows[0]["cmpStOS"].ToString());
                        }
                        else
                            retval = false;
                    }
                }
                return retval;
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
                return false;
            }
        }

        /// <summary>
        /// Controla o estado dos botões.
        /// </summary>
        private void controlaSituacaoOS(String estado)
        {
            lblDtAbertura.Visible = true;
            txtDtAbertura.Visible = true;

            switch (estado)
            {
                case "S": // Os Concluída
                    lblStatus.Text = "Situação: Concluída.";
                    btnSave.Enabled             = false;
                    btnCancelar.Enabled         = false;
                    btnImprimirOS.Enabled       = true;
                    btnConcluir.Enabled         = true;
                    btnEnviarAprovacao.Enabled  = false;
                    btnExecucao.Enabled         = false;
                    break;
                case "G": // Em Aprovação
                    lblStatus.Text = "Situação: Em Aprovação.";
                    btnSave.Enabled = false;
                    btnExcluir.Enabled = false;
                    btnCancelar.Enabled = false;
                    btnImprimirOS.Enabled = true;
                    break;
                case "P": // Aprovada
                    lblStatus.Text = "Situação: Aprovada.";
                    btnSave.Enabled = false;
                    btnImprimirOS.Enabled = true;
                    break;
                case "D": // Excluída
                    lblStatus.Text = "Situação: Excluída.";
                    btnSave.Enabled = false;
                    btnExcluir.Enabled = false;
                    btnCancelar.Enabled = false;
                    btnImprimirOS.Enabled = true;
                    btnConcluir.Enabled = false;
                    btnEnviarAprovacao.Enabled = false;
                    btnExecucao.Enabled = false;
                    break;
                case "C": // Cancelada
                    lblStatus.Text = "Situação: Cancelada.";
                    btnSave.Enabled = false;
                    btnExcluir.Enabled = false;
                    btnCancelar.Enabled = false;
                    btnImprimirOS.Enabled = true;
                    btnConcluir.Enabled = false;
                    btnEnviarAprovacao.Enabled = false;
                    btnExecucao.Enabled = false;
                    break;
                case "R": // Não Aprovada
                    lblStatus.Text = "Situação: Não Aprovada.";
                    btnSave.Enabled = false;
                    btnExcluir.Enabled = false;
                    btnCancelar.Enabled = false;
                    btnImprimirOS.Enabled = true;
                    btnConcluir.Enabled = false;
                    btnEnviarAprovacao.Enabled = false;
                    btnExecucao.Enabled = false;
                    break;
                case "N": // Em Aberto
                    lblStatus.Text = "Situação: Em aberto.";
                    btnSave.Enabled = true;
                    btnExcluir.Enabled = true;
                    btnCancelar.Enabled = true;
                    btnImprimirOS.Enabled = true;
                    break;
                case "E": // Em Execução
                    lblStatus.Text = "Situação: Em Execução.";
                    btnSave.Enabled         = false;
                    btnExcluir.Enabled      = true;
                    btnCancelar.Enabled     = true;
                    btnImprimirOS.Enabled   = true;
                    break;
                default:
                    lblStatus.Text = "Novo Atendimento";
                    lblDtAbertura.Visible   = false;
                    txtDtAbertura.Visible   = false;
                    lblNumeroOS.Text        = "";
                    btnSave.Enabled         = true;
                    btnExcluir.Enabled      = false;
                    btnCancelar.Enabled     = false;
                    btnImprimirOS.Enabled   = false;
                    btnConcluir.Enabled     = true;
                    btnEnviarAprovacao.Enabled  = true;
                    btnExecucao.Enabled         = true;
                    break;
            }
            controlaPerfilUsuario();
        }
        #endregion

        #region Events
        /// <summary>
        /// Evento ao selecionar um tipo de atividade.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbTipoAtiviade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoTipoAtividade";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = cmbTipoAtividade.SelectedValue;
                ls.Add(lc);

                using (DataTable table = tblDescricaoSolicitacao.Get(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbSolicitacao, table, "cmpDcDescricaoSolicitacao", "cmpCoDescricaoSolicitacao", "cmpDcDescricaoSolicitacao", "--- Selecione uma Descrição ---", true);
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Evento ao selecionar uma obra.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista = cmbLocal.SelectedValue;
            btnReset_Click(sender, e);
        }

        /// <summary>
        /// Grava uma ordem de serviço.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.GravarOS())
                CaixaMensagem.Mostar(Mensagem.Tipo.Sucesso, "Dados cadastrados com sucesso.");
        }

        /// <summary>
        /// Evento ao carregar a página.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();

                ViewState["cmpIdPreventivaAgenda"] = "0";

                this.loadObraSolicitante();
                loadObraPavimento();
                NameValueCollection n = Request.QueryString;

                if (n.HasKeys())
                {
                    switch (n.GetKey(0))
                    {
                        case "id":
                            if (n.Count > 1)
                                if (n.GetKey(1) == "Concluido")
                                    CaixaMensagem.Mostar(Mensagem.Tipo.Sucesso, "Ordem de serviço concluída com sucesso.");
                            ViewState["cmpIdOS"] = Request.QueryString["id"];
                            this.load();
                            this.loadOS();
                            break;
                    }
                }
                else
                {
                    btnReset_Click(sender, e);
                }
            }
        }

        /// <summary>
        /// Prepara a página para um novo registro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ViewState["cmpIdPreventivaAgenda"] = "0";
            ViewState["cmpIdOS"]        = 0;
            txtSolicitante.Text         = "";
            txtEmail.Text               = "";
            txtTelefone.Text            = "";
            txtObservacoes.Text         = "";
            pnlConclusao.Visible        = false;
            pnlJustificativa.Visible    = false;
            controlaSituacaoOS("");
            this.load();
        }

        /// <summary>
        /// Direciona para a página de pesquisa.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPesquiar_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("webATE_OSPesquisa.aspx");
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Exclui a O.S.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/ATE/webATE_OSJustificativa.aspx?id=" + ViewState["cmpIdOS"] + "&Sit=E" + "&Tp=TI", false);
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }
 
        /// <summary>
        /// Grava cancelamento da O.S
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/ATE/webATE_OSJustificativa.aspx?id=" + ViewState["cmpIdOS"] + "&Sit=C" + "&Tp=TI", false);
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// imprimir O.S
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImprimirOS_Click(object sender, EventArgs e)
        {
            if (ViewState["cmpIdOS"].ToString() == "0")
                this.GravarOS();

            ImprimirOs Imprimir = new ImprimirOs();
            pdfDocument myDoc = new pdfDocument("Horizon", "Orion");
            Imprimir.myDoc = myDoc;
            Imprimir.myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);
            Imprimir.cmpCoObra = loadObra();
            Imprimir.cmpIdOS   = ViewState["cmpIdOS"].ToString();
            Imprimir.NomeObra  = "Serviços de TI";
            Imprimir.EnderecoLogoOrion = Server.MapPath("~/Imagens/logo_Orion.jpg");

            if (ViewState["cmpInLogoObra"].ToString() == "1")
                Imprimir.EnderecoLogoObra = Server.MapPath("~/Imagens/logo_IPEN.bmp");
            else
                Imprimir.EnderecoLogoObra = "";

            Imprimir.ImprimeOrdemServico();

            string filename = "pdf" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf";
            string nomepdf = Server.MapPath("~/Relatorios/" + filename);
            myDoc.createPDF(nomepdf);

            Response.Write("<script>window.open('" + Global.UrlRelatorio + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');</script>");

            // Impressão ambiente desenvolvimento local
            //Response.Write("<script language='javascript'>"
            //                + "window.open('" + @"http://localhost:54215/Relatorios/" + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105')"
            //                + "</script>");

            // Impressão ambiente produção ambiente interno orion
            //Response.Write("<script language='javascript'>"
            //                  + "window.open('" + @"http://172.10.10.2/HzWebManutencao_Desenv/Relatorios/" + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105')"
            //                  + "</script>");
        }

        /// <summary>
        /// Direciona para a página de conclusão da O.S.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["cmpIdOS"].ToString() == "0")
                    this.GravarOS();

                Response.Redirect("~/ATE/webATE_OSConclusao.aspx?id=" + ViewState["cmpIdOS"] + "&Tp=TI", false);
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Grava envio da O.S para aprovação.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEnviarAprovacao_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["cmpIdOS"].ToString() == "0")
                    this.GravarOS();

                tblOS table = null;
                table = new tblOS();
                table.cmpIdOS = ViewState["cmpIdOS"].ToString();
                table.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;

                if (table.GravarEnviarAprovacaoOS(Global.GetConnection()))
                    CaixaMensagem.Mostar(Mensagem.Tipo.Aviso, "Ordem de serviço enviada para aprovação.");
                controlaSituacaoOS("G");
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Grava os dados da O.S em execução
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExecucao_Click(object sender, EventArgs e)
        {
            try
            {
                tblOS table = null;
                table = new tblOS();
                table.cmpIdOS = ViewState["cmpIdOS"].ToString();
                table.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;

                if (table.GravarExecucaoOS(Global.GetConnection()))
                    CaixaMensagem.Mostar(Mensagem.Tipo.Aviso, "Ordem de serviço em execução.");

                controlaSituacaoOS("E");
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        #endregion
    }
}