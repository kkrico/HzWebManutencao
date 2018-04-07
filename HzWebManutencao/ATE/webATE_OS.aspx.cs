using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
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
using HzLibCorporativo.Config;

namespace HzWebManutencao.ATE
{
    public partial class webATE_OS : System.Web.UI.Page
    {
        #region variables
        static string prevPage  = string.Empty;
        #endregion
        private string cmpCoEquipamentoObra;
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
                    cmbObra.SelectedIndex = Objetos.RetornaIndiceCombo(cmbObra, long.Parse(table.Rows[0]["cmpCoObraGrupoLista"].ToString()));
                    cmbObra_SelectedIndexChanged(cmbObra, EventArgs.Empty);

                    if (table.Rows[0]["cmpIdPreventivaAgenda"].ToString() != "")
                    {
                         for (int i = cmbOrigemOS.Items.Count-1; i >= 0; i--)
                         {
                             if (cmbOrigemOS.Items[i].Text != "PREVENTIVA")
                                 cmbOrigemOS.Items.Remove(cmbOrigemOS.Items[i]);
                         }
                         lblNuPreventiva.Visible = true;
                         lblNuPreventiva.Text = "Preventiva Número ==> " + table.Rows[0]["cmpNuPreventivaAgenda"].ToString();
                    }
                    cmbOrigemOS.SelectedIndex = Objetos.RetornaIndiceCombo(cmbOrigemOS, long.Parse(table.Rows[0]["cmpCoOrigemOS"].ToString()));

                    cmbFormaSolicitacao.SelectedIndex = Objetos.RetornaIndiceCombo(cmbFormaSolicitacao, long.Parse(table.Rows[0]["cmpIdFormaSolicitacao"].ToString()));

                    cmbTipoAtividade.SelectedIndex = Objetos.RetornaIndiceCombo(cmbTipoAtividade, long.Parse(table.Rows[0]["cmpCoTipoAtividade"].ToString()));
                    cmbTipoAtiviade_SelectedIndexChanged(cmbTipoAtividade, EventArgs.Empty);

                    cmbSolicitacao.SelectedIndex = Objetos.RetornaIndiceCombo(cmbSolicitacao, long.Parse(table.Rows[0]["cmpCoDescricaoSolicitacao"].ToString()));

                    cmbObraPavimento.SelectedIndex = Objetos.RetornaIndiceCombo(cmbObraPavimento, long.Parse(table.Rows[0]["cmpCoObraPavimento"].ToString()));
                                        
                    txtLocal.Text           = table.Rows[0]["cmpDcLocal"].ToString().Trim();
                    txtSetor.Text           = table.Rows[0]["cmpNoSetor"].ToString().Trim();
                    txtSolicitante.Text     = table.Rows[0]["cmpNoSolicitante"].ToString().Trim();
                    txtTelefone.Text        = table.Rows[0]["cmpNuTelefone"].ToString().Trim();
                    txtEmail.Text           = table.Rows[0]["cmpEeEmail"].ToString().Trim();
                    txtObservacoes.Text     = table.Rows[0]["cmpDcObservacoes"].ToString().Trim();
                    txtNuDemanda.Text       = table.Rows[0]["cmpNuDemandaCliente"].ToString().Trim();
                    ViewState["cmpIdOS"]    = table.Rows[0]["cmpIdOs"].ToString();
                    //txtEquipamentoObra.Text=
                    imgAndamento.Visible = true;
                    lbOSAndamento.Visible = true;
                    grdOSAndamento.DataSource = loadOSAndamento();
                    grdOSAndamento.DataBind();

                    switch (table.Rows[0]["cmpStOS"].ToString())
                    {
                        case "S": // Concluída
                                lblConclusao.Visible = true;
                                ImgConclusao.Visible = true;
                                txtDtInicio.Text    = (DateTime.Parse(table.Rows[0]["cmpDtInicioAtendimento"].ToString()).ToString()).Substring(0,16);
                                txtConclusao.Text   = (DateTime.Parse(table.Rows[0]["cmpDtConclusaoAtendimento"].ToString()).ToString()).Substring(0,16);
                                txtAtestado.Text    = table.Rows[0]["cmpNoAtestador"].ToString();
                                txtConcluido.Text   = table.Rows[0]["cmpNoExecutor"].ToString();
                                txtObs.Text         = table.Rows[0]["cmpDcObservacaoConclusao"].ToString();
                                if (table.Rows[0]["cmpInSatisfacaoCliente"].ToString() != "")
                                    lblSatisfacaoServico.Text = table.Rows[0]["cmpInSatisfacaoCliente"].ToString() == "True" ? "SIM" : "NÃO";
                                break;
                        case "C" : // Cancelada
                                lblJustifica.Visible = true;
                                ImgJustifica.Visible = true;
                                lblJustifica.Text = "Justificativa do Cancelamento da Ordem de Serviço";
                                TxtJustificativa.Text = table.Rows[0]["cmpDcObservacaoConclusao"].ToString();
                                break;
                        case "R" : // Reprovação Parcial
                                lblJustifica.Visible = true;
                                ImgJustifica.Visible = true;
                                lblJustifica.Text = "Justificativa da Reprovação Parcial da Ordem de Serviço";
                                TxtJustificativa.Text = table.Rows[0]["cmpDcObservacaoConclusao"].ToString();
                                break;
                        case "D": // Exclusão
                                lblJustifica.Visible = true;
                                ImgJustifica.Visible = true;
                                lblJustifica.Text = "Justificativa da Exclusão da Ordem de Serviço";
                                TxtJustificativa.Text = table.Rows[0]["cmpDcObservacaoConclusao"].ToString();
                                break;
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
        /// Carrega dados do Andamento da OS.
        /// </summary>
        private DataTable loadOSAndamento()
        {
            return tblOSAndamento.RetornarOSAndamentos(Global.GetConnection(), ViewState["cmpIdOS"].ToString());
        }

        /// <summary>
        /// Verifica se a OS tem material vinculado.
        /// </summary>
        private bool VerificaMaterial()
        {
            bool retval = false;

            try
            {
                ListCampos ls   = new ListCampos();
                ListCampo lc    = new ListCampo();
                lc.NomeCampo    = "cmpIdOs";
                lc.TipoCampo    = TipoCampo.Numero;
                lc.ValorCampo   = ViewState["cmpIdOS"].ToString();
                ls.Add(lc);

                using(DataTable table = tblOSMaterial.Get(Global.GetConnection(), ls))
                {
                    if (table != null && table.Rows.Count > 0)
                        retval = true;
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }

            return retval;
        }

        /// <summary>
        /// Carrega as combos de tipo, atividade e forma de solicitação.
        /// </summary>
        private void load()
        {
            try
            {
                ListCampos ls   = new ListCampos();
                ListCampo lc    = new ListCampo();
                lc.NomeCampo    = "cmpCoContratante";
                lc.TipoCampo    = TipoCampo.Numero;
                lc.ValorCampo   = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoContratante;
                ls.Add(lc);

                using (DataTable table = tblTipoAtividade.Get(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbTipoAtividade, table, "cmpDcTipoAtividade", "cmpCoTipoAtividade", "cmpDcTipoAtividade", "--- Selecione um Tipo de Atividade ---", true);
                    cmbTipoAtiviade_SelectedIndexChanged(cmbTipoAtividade, EventArgs.Empty);
                }

                using (DataTable table = tblFormaSolicitacao.Get(Global.GetConnection()))
                {
                    Objetos.LoadCombo(cmbFormaSolicitacao, table, "cmpDcFormaSolicitacao", "cmpIdFormaSolicitacao", "cmpDcFormaSolicitacao", "--- Selecione uma Forma de Solicitação ---", true);
                }

                if (ViewState["cmpIdPreventivaAgenda"].ToString() != "0")
                {
                    ls = new ListCampos();
                    lc = new ListCampo();
                    lc.NomeCampo = "cmpDcOrigemOS";
                    lc.TipoCampo = TipoCampo.String;
                    lc.ValorCampo = "PREVENTIVA";
                    ls.Add(lc);
                }
                using (DataTable table = tblOrigemOS.Get(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbOrigemOS, table, "cmpDcOrigemOS", "cmpCoOrigemOS", "cmpDcOrigemOS", "--- Selecione um Tipo ---", true);
                }

            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
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
                if (ViewState["cmpIdOS"].ToString() == "0" && txtNuDemanda.Text != "")
                {
                    DataTable tb = tblOS.VerificaNumeroDemanda(Global.GetConnection(), cmbObra.SelectedValue, txtNuDemanda.Text);
                    if (tb.Rows[0]["Mensagem"].ToString() != "")
                    {
                        CaixaMensagem.Mostar(Mensagem.Tipo.Erro, tb.Rows[0]["Mensagem"].ToString());
                        retval = false;
                    }
                }

                if (retval)
                {
                    table = new tblOS();
                    table.cmpIdOS                    = ViewState["cmpIdOS"].ToString();
                    table.cmpCoDescricaoSolicitacao  = cmbSolicitacao.SelectedValue;
                    table.cmpCoObra                  = cmbObra.SelectedValue;
                    table.cmpCoObraPavimento         = cmbObraPavimento.SelectedValue;
                    table.cmpCoOrigemOS              = cmbOrigemOS.SelectedValue;
                    table.cmpDcLocal                 = txtLocal.Text;
                    table.cmpNoSetor                 = txtSetor.Text;
                    table.cmpIdFormaSolicitacao      = cmbFormaSolicitacao.SelectedValue;
                    table.cmpDcObservacoes           = txtObservacoes.Text;
                    table.cmpNoSolicitante           = txtSolicitante.Text;
                    table.cmpEeEmail                 = txtEmail.Text;
                    table.cmpNuTelefone              = txtTelefone.Text;
                    table.cmpNoUsuario               = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;
                    table.cmpNuDemandaCliente        = txtNuDemanda.Text.TrimStart('0'); 
                    table.cmpIdPreventivaConfirmacao = ViewState["cmpIdPreventivaAgenda"].ToString();
                    table.cmpCoUsuario = cmbTecnico.SelectedValue.ToString();
                    
                    table.cmpIdEquipamentoObra = ViewState["cmpIdEquipamentoObra"].ToString();

                    using (DataTable tbl = table.GravarOS(Global.GetConnection()))
                    {
                        if (tbl != null && tbl.Rows.Count > 0)
                        {
                            lblNumeroOS.Visible     = true;
                            lblNumeroOS.Text        = "Ordem de Serviço Número ==> " + tbl.Rows[0][0].ToString();
                            txtDtAbertura.Text      = tbl.Rows[0]["cmpDtAbertura"].ToString();
                            controlaSituacaoOS(tbl.Rows[0]["cmpStOS"].ToString());
                            
                            grdOSAndamento.DataSource = loadOSAndamento();
                            grdOSAndamento.DataBind();

                            table.cmpDtAbertura         = txtDtAbertura.Text;
                            table.cmpNuOS               = tbl.Rows[0][0].ToString();
                            table.cmpNuObra             = ViewState["cmpNuObra"].ToString();

                            // Verifica se o cliente recebe arquivo anexo em email quando abre ordem de serviço.
                            if (ViewState["cmpEdArqAnexoCliente"].ToString() != "" && ViewState["cmpIdOS"].ToString() == "0")
                            {
                                table.cmpNoArquivoEnviar = ViewState["NomeArquivo"].ToString().Replace("_1.txt", "_2.txt");
                                table.cmpEdArqAnexoCliente = ViewState["cmpEdArqAnexoCliente"].ToString();
                                table.cmpEeCliente = ViewState["cmpEeCliente"].ToString();

                                table.GerarArqProtocoloCEF("2");
                                table.EnviarEmailCliente(Global.GetConnection());

                                // Move o arquivo de pasta recebido para a pasta de admitidos.
                                ViewState["NomeArquivo"].ToString().Replace("_2.txt", "_1.txt");
                                File.Move(Path.Combine(ViewState["cmpEdArqRecebidoCliente"].ToString(),  ViewState["NomeArquivo"].ToString()), 
                                          Path.Combine(ViewState["cmpEdArqAdmitidoCliente"].ToString(),  ViewState["NomeArquivo"].ToString()));
                            }

                            ViewState["cmpIdOS"] = tbl.Rows[0]["cmpIdOS"];

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
        /// Carrega combo de obras do banco HzCorporativo.
        /// </summary>
        private void loadObra()
        {
            try
            {
                ListCampos ls   = new ListCampos();
                ListCampo lc    = new ListCampo();

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
                    Objetos.LoadCombo(cmbObra, table, "cmpNoObra", "cmpCoObraGrupoLista", "cmpNoObra", true);

                    if (Session["login"] != null && ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista != null)
                        cmbObra.SelectedIndex = Objetos.RetornaIndiceCombo(cmbObra, int.Parse(((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista));
                    cmbObra_SelectedIndexChanged(cmbObra, EventArgs.Empty);
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
        private void loadObraSelecionada(string CoObraGrupoLista)
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();

            lc.NomeCampo = "cmpCoObraGrupoLista";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = CoObraGrupoLista;
            ls.Add(lc);

            using (DataTable table = tblObraGrupoLista.Get(Global.GetConnection(), ls))
            {
                if (ViewState["OSCef"].ToString() != "0")
                {
                    Objetos.LoadCombo(cmbObra, table, "cmpNoObra", "cmpCoObraGrupoLista", "cmpNoObra", true);
                    cmbObra.SelectedIndex = Objetos.RetornaIndiceCombo(cmbObra, int.Parse(Session["cmpCoObraGrupoLista"].ToString()));
                    cmbObra_SelectedIndexChanged(cmbObra, EventArgs.Empty);
                }
                ViewState["cmpEeCliente"]            = table.Rows[0]["cmpEeCliente"].ToString();
                ViewState["cmpEdArqAnexoCliente"]    = table.Rows[0]["cmpEdArqAnexoCliente"].ToString();
                ViewState["cmpEdArqRecebidoCliente"] = table.Rows[0]["cmpEdArqRecebidoCliente"].ToString();
                ViewState["cmpEdArqAdmitidoCliente"] = table.Rows[0]["cmpEdArqAdmitidoCliente"].ToString();

                ViewState["cmpNuObra"]               = table.Rows[0]["cmpNuObra"].ToString();

                if (ViewState["cmpEeCliente"].ToString() != "")
                {
                    Global.PathArquivos(@ViewState["cmpEdArqAnexoCliente"].ToString());
                    Global.PathArquivos(@ViewState["cmpEdArqRecebidoCliente"].ToString());
                    Global.PathArquivos(@ViewState["cmpEdArqAdmitidoCliente"].ToString());
                }
            }
        }


        /// <summary>
        /// Carrega a combo da tabela tblObraPavimento do banco HzCorporativo.
        /// </summary>
        private void loadObraPavimento()
        {
            try
            {
                ListCampos ls   = new ListCampos();
                ListCampo lc    = new ListCampo();
                lc.NomeCampo    = "cmpCoObraGrupoLista";
                lc.TipoCampo    = TipoCampo.Numero;
                lc.ValorCampo   = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista;
                ls.Add(lc);

                using (DataTable table = tblObraPavimento.Get(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbObraPavimento, table, "cmpDcPavimento", "cmpCoObraPavimento", "cmpNuOrdenacao", "--- Selecione o Pavimento ---", true);
                    if (table.Rows.Count > 0)
                        ViewState["cmpInLogoObra"] = table.Rows[0]["cmpInLogoObra"].ToString() == "True" ? "1" : "0"; 
                }


                ls = new ListCampos();
                lc = new ListCampo();
                lc.NomeCampo = "cmpCoPerfil";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = "12";
                ls.Add(lc);

                DataTable dtTecnicos = tblUsuario.Get(Global.GetConnection(), ls);
                Objetos.LoadCombo(cmbTecnico, dtTecnicos, "cmpNoUsuario", "cmpCoUsuario", "cmpNoUsuario", "--- Selecione o Usuario ---", true);
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Controla o estado dos botões.
        /// </summary>
        private void controlaPerfilUsuario()
        {
            switch (((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNuPerfil.ToString())
            {
                case "3": // Cliente
                        btnConcluir.Enabled  = false;
                        btnExecucao.Enabled  = false;
                break;
            }

        }

        /// <summary>
        /// Controla o estado dos botões.
        /// </summary>
        private void controlaSituacaoOS(String estado)
        {
            lblDtAbertura.Visible   = true;
            txtDtAbertura.Visible   = true;
            lblStatus.Visible       = true;

            switch (estado)
            {
                case "S": // Os Concluída
                        lblStatus.Text = "Situação: Concluída.";
                        btnSave.Enabled             = false;
                        btnConcluir.Enabled         = false;
                        btnEnviarAprovacao.Enabled  = false;
                        btnExecucao.Enabled         = false;
                        btnCancelar.Enabled         = false;
                        btnReabrirOS.Enabled        = true;
                        if (this.VerificaMaterial())
                        {
                            btnMaterial.Enabled = true;
                            btnMaterial.Text    = "Visualizar Material";
                        }
                        else
                        {
                            btnMaterial.Visible = false;
                        }
                        break;
                case "G": // Em Aprovação
                        lblStatus.Text              = "Situação: Em Aprovação.";
                        btnSave.Enabled             = false;
                        btnConcluir.Enabled         = false;
                        btnEnviarAprovacao.Enabled  = false;
                        btnExecucao.Enabled         = false;
                        btnExcluir.Enabled          = false;
                        btnCancelar.Enabled         = false;
                        btnReabrirOS.Enabled        = false;
                        btnMaterial.Enabled         = true;
                        break;
                case "P": // Aprovada
                        lblStatus.Text              = "Situação: Aprovada.";
                        btnSave.Enabled             = false;
                        btnConcluir.Enabled         = false;
                        btnEnviarAprovacao.Enabled  = false;
                        btnExecucao.Enabled         = true;
                        btnReabrirOS.Enabled        = true;
                        if (this.VerificaMaterial())
                        {
                            btnMaterial.Enabled = true;
                            btnMaterial.Text = "Visualizar Material";
                        }
                        else
                        {
                            btnMaterial.Visible = false;
                        }
                        break;
                case "D": // Excluída
                        lblStatus.Text = "Situação: Excluída.";
                        btnSave.Enabled             = false;
                        btnConcluir.Enabled         = false;
                        btnEnviarAprovacao.Enabled  = false;
                        btnExecucao.Enabled         = false;
                        btnExcluir.Enabled          = false;
                        btnCancelar.Enabled         = false;
                        btnReabrirOS.Enabled        = false;
                        btnMaterial.Enabled         = false;
                        break;
                case "C": // Cancelada
                        lblStatus.Text = "Situação: Cancelada.";
                        btnSave.Enabled             = false;
                        btnConcluir.Enabled         = false;
                        btnEnviarAprovacao.Enabled  = false;
                        btnExecucao.Enabled         = false;
                        btnExcluir.Enabled          = false;
                        btnCancelar.Enabled         = false;
                        btnReabrirOS.Enabled        = true;
                        if (this.VerificaMaterial())
                        {
                            btnMaterial.Enabled = true;
                            btnMaterial.Text = "Visualizar Material";
                        }
                        else
                        {
                            btnMaterial.Visible = false;
                        }
                        break;
                case "R": // Não Aprovada
                        lblStatus.Text = "Situação: Não Aprovada.";
                        btnSave.Enabled             = false;
                        btnConcluir.Enabled         = false;
                        btnEnviarAprovacao.Enabled  = false;
                        btnExecucao.Enabled         = false;
                        btnExcluir.Enabled          = false;
                        btnCancelar.Enabled         = false;
                        btnReabrirOS.Enabled        = true;
                        if (this.VerificaMaterial())
                        {
                            btnMaterial.Enabled = true;
                            btnMaterial.Text = "Visualizar Material";
                        }
                        else
                        {
                            btnMaterial.Visible = false;
                        }
                        break;
                case "N": // Em Aberto
                        lblStatus.Text = "Situação: Em aberto.";
                        btnSave.Enabled             = true;
                        btnConcluir.Enabled         = true;
                        btnEnviarAprovacao.Enabled  = true;
                        btnExecucao.Enabled         = false;
                        btnExcluir.Enabled          = true;
                        btnCancelar.Enabled         = true;
                        btnMaterial.Enabled         = true;
                        btnReabrirOS.Enabled        = false;
                        break;
                case "E": // Em Execução
                        lblStatus.Text = "Situação: Em Execução.";
                        btnSave.Enabled             = false;
                        btnConcluir.Enabled         = true;
                        btnEnviarAprovacao.Enabled  = false;
                        btnExecucao.Enabled         = false;
                        btnExcluir.Enabled          = true;
                        btnCancelar.Enabled         = true;
                        btnReabrirOS.Enabled        = true;
                        if (this.VerificaMaterial())
                        {
                            btnMaterial.Enabled = true;
                            btnMaterial.Text = "Visualizar Material";
                        }
                        else
                        {
                            btnMaterial.Visible = false;
                        }
                        break;
                case "B": // Raberta
                        lblStatus.Text = "Situação: Reaberta.";
                        btnSave.Enabled = true;
                        btnConcluir.Enabled = true;
                        btnEnviarAprovacao.Enabled = true;
                        btnExecucao.Enabled = false;
                        btnExcluir.Enabled = true;
                        btnCancelar.Enabled = true;
                        btnMaterial.Enabled = true;
                        btnReabrirOS.Enabled = false;
                        break;
                default:
                        lblStatus.Text          = "Novo Atendimento";
                        lblDtAbertura.Visible   = false;
                        txtDtAbertura.Visible   = false;
                        lblNumeroOS.Text        = "";
                        btnSave.Enabled         = true;
                        btnConcluir.Enabled     = true;
                        btnEnviarAprovacao.Enabled = true;
                        btnExcluir.Enabled      = false;
                        btnExecucao.Enabled     = false;
                        btnCancelar.Enabled     = false;
                        btnReabrirOS.Enabled    = false;
                        btnMaterial.Visible     = true;
                        btnMaterial.Text        = "Material";
                        btnMaterial.Enabled     = true;
                        break;
            }

            controlaPerfilUsuario();
        }
        #endregion

        #region Events
        /// <summary>
        /// Evento ao carregar a página.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.UrlReferrer != null) { prevPage = Request.UrlReferrer.ToString(); }

                ViewState["cmpIdPreventivaAgenda"] = "0";
                ViewState["OSCef"] = "0";
                ViewState["cmpIdEquipamentoObra"] = "0";

                lblConclusao.Visible = false;
                ImgConclusao.Visible = false;
                lblJustifica.Visible = false;
                ImgJustifica.Visible = false;
                imgAndamento.Visible = false;
                lbOSAndamento.Visible = false;

                #region Cartilha Classificação Ordem de Serviço
                string lblTitulo = "<b>CARTILHA DE CLASSIFICAÇÃO DE ORDEM DE SERVIÇO</b><br/><br/>";

                string lblmsgInterna = "<b>ORDEM DE SERVIÇO INTERNA</b> <br />" +
                            "<b>O Que é?</b> <br />" +
                            "É todo serviço de correção de um problema que é verificado pela própria equipe Orion " +
                            "e equipe de apoio da edificação (limpeza, brigada, segurança). <br />" +
                            "<b>Quem avalia?</b> <br />" +
                            "Por se tratar de um serviço em que o cliente não percebe o problema, <br />" +
                            "o responsável por avaliar o serviço é o encarregado.<br />" +
                            "<b>Exemplo:</b> <br /> Uma lâmpada queimada, verificada e trocada antes da solicitação do órgão.<br /><br />";

                string lblmsgExterna = "<b>ORDEM DE SERVIÇO EXTERNA</b> <br />" +
                            "<b>O Que é?</b> <br />" +
                            "É todo serviço de correção de um problema demandado pelo cliente (usuários da edificação, gestores ou fiscais).<br />" +
                            "<b>Quem avalia?</b> <br />" +
                            "A avaliação do serviço deve ser feita pelo usuário da edificação, gestor ou fiscal que solicitou o serviço. <br />" +
                            "<b>Exemplo: </b> <br /> Uma lâmpada queimada trocada a partir da solicitação de um funcionário do órgão.<br /><br />";

                string lblmsgPreventivas = "<b>ORDEM DE SERVIÇO PREVENTIVA</b> <br />" +
                            "<b>O Que é?</b> <br />" +
                            "É todo serviço de correção de um problema constatado a partir da rotina prevista no Plano de Manutenção Preventiva.<br />" +
                            "<b>Quem avalia?</b> <br />" +
                            "Por ser tratar de um serviço proveniente das rondas de manutenção, a avaliação pode ser de responsabilidade do encarregado ou do engenheiro.<br />" +
                            "<b>Exemplo: </b> <br /> Limpeza da caixa de gordura e esgoto.<br /><br />";

                string lblmsgEventual = "<b>ORDEM DE SERVIÇO EVENTUAL</b> <br />" +
                            "<b>O Que é?</b> <br />" +
                            "São serviços previstos no escopo contratual, porém com execução sob demanda do cliente.<br />" +
                            "<b>Quem avalia?</b> <br />" +
                            "Por ser tratar de um serviço não previsto no plano de manutenção, a avaliação é de responsabilidade do engenheiro.<br />" +
                            "<b>Exemplo: </b> <br /> Instalação de um novo ponto lógico.<br /><br />";

                string lblmsgForaEscopo = "<b>ORDEM DE SERVIÇO FORA DO ESCOPO</b> <br />" +
                            "<b>O Que é?</b> <br />" +
                            "São serviços não previstos no escopo contratual.<br />" +
                            "<b>Quem avalia?</b> <br />" +
                            "Por ser um serviço considerado como cortesia prestada pela empresa, as Ordens de Serviços Fora do Escopo devem ser assinadas pelo engenheiro e, <br /> " +
                            "posteriormente, depois de cadastrada no Horizon-Manutenção, encaminhadas ao escritório.<br />" +
                            "<b>Exemplo: </b> <br /> Serviços emergencial na residência oficial do órgão.<br /><br />";

                lblmsg.Text = lblTitulo + lblmsgInterna + lblmsgExterna + lblmsgPreventivas + lblmsgEventual + lblmsgForaEscopo;
                #endregion

                NameValueCollection n = Request.QueryString;

                if (n.HasKeys())
                {
                    switch (n.GetKey(0))
                    {
                        case "IdPreventivaConfirmacao":
                            ViewState["cmpIdPreventivaAgenda"] = n.Get(0); 
                            ViewState["cmpNuPreventivaAgenda"] = n.Get(1);
                            ViewState["cmpIdOS"]    = 0;
                            lblNumeroOS.Visible     = false;
                            lblStatus.Visible       = false;
                            lblNuPreventiva.Visible = true;
                            lblNuPreventiva.Text = "Preventiva Número ==> " + Request.QueryString["NuPreventiva"].ToString();
                            this.loadObra();
                            this.load();
                            break;
                        case "id":
                            this.loadObra();
                            if (n.Count > 1)
                                if (n.GetKey(1) == "Concluido")
                                    CaixaMensagem.Mostar(Mensagem.Tipo.Sucesso, "Ordem de serviço concluída com sucesso.");
                            ViewState["cmpIdOS"] = Request.QueryString["id"];
                            this.load();
                            this.loadOS();
                            break;
                        case "OSCef":
                            ViewState["OSCef"] = "1";
                            txtNuDemanda.Text   = Session["txtNuDemanda"].ToString();
                            txtLocal.Text       = Session["txtLocal"].ToString();
                            txtSolicitante.Text = Session["txtSolicitante"].ToString();
                            txtTelefone.Text    = Session["txtTelefone"].ToString();
                            txtObservacoes.Text = Session["txtObservacoes"].ToString();
                            ViewState["NomeArquivo"] = Session["NomeArquivo"].ToString();

                            Session.Remove("txtNuDemanda");
                            Session.Remove("txtLocal");
                            Session.Remove("txtSolicitante");
                            Session.Remove("txtTelefone");
                            Session.Remove("txtObservacoes");
                            Session.Remove("NomeArquivo");

                            this.loadObraSelecionada(Session["cmpCoObraGrupoLista"].ToString());
                            this.load();

                            ViewState["cmpIdOS"] = 0;
                            lblNuPreventiva.Visible = false;

                            controlaSituacaoOS("");

                            break;
                    }
                }
                else
                {
                    btnReset_Click(sender, e);
                    this.loadObra();
                }               
            }
        }

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
        protected void cmbObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista = cmbObra.SelectedValue;
            this.loadObraPavimento();

            if (ViewState["OSCef"].ToString() == "0")
                this.loadObraSelecionada(cmbObra.SelectedValue);

            if (ViewState["cmpIdPreventivaAgenda"].ToString() == "0" && ViewState["OSCef"].ToString() == "0")
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
        /// Prepara a página para um novo registro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ViewState["cmpIdPreventivaAgenda"] = "0";
            ViewState["cmpIdOS"]    = 0;
            lblNuPreventiva.Visible = false;
            txtLocal.Text           = "";
            txtSolicitante.Text     = "";
            txtEmail.Text           = "";
            txtTelefone.Text        = "";
            txtObservacoes.Text     = "";
            txtSetor.Text           = "";
            txtNuDemanda.Text       = "";
            lblConclusao.Visible    = false;
            ImgConclusao.Visible    = false;
            lblJustifica.Visible    = false;
            ImgJustifica.Visible    = false;
            imgAndamento.Visible    = false;
            lbOSAndamento.Visible   = false;

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
        /// Direciona para a página de conclusão da O.S.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConcluir_Click(object sender, EventArgs e)
        {
            try
            {
                string EdArqAnexo = "";
                //if (ViewState["cmpIdOS"].ToString() == "0")
                this.GravarOS();

                //Pega o endereço para envio de arquivo em anexo da conclusão da O.S para o cliente.
                EdArqAnexo = ViewState["cmpEdArqAnexoCliente"].ToString();

                Response.Redirect("~/ATE/webATE_OSConclusao.aspx?id=" + ViewState["cmpIdOS"] + "&Tp=OSConvencioal" + "&Anexo=" + EdArqAnexo, false);
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
        /// Exclui a O.S.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/ATE/webATE_OSJustificativa.aspx?id=" + ViewState["cmpIdOS"] + "&Sit=E", false);
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

        /// <summary>
        /// Grava cancelamento da O.S
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/ATE/webATE_OSJustificativa.aspx?id=" + ViewState["cmpIdOS"] + "&Sit=C", false);
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }          
        }

        /// <summary>
        /// Reabrir O.S com justificativa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReabrirOS_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/ATE/webATE_OSJustificativa.aspx?id=" + ViewState["cmpIdOS"] + "&Sit=B", false);
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }          
        }

        /// <summary>
        /// Seleciona material da O.S
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["cmpIdOS"].ToString() == "0")
                    this.GravarOS();

                Response.Redirect("~/ATE/webATE_OSMaterial.aspx?id=" + ViewState["cmpIdOS"] + "&obr=" + ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista, false);
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
            pdfDocument myDoc   = new pdfDocument("Horizon", "Orion");
            Imprimir.myDoc      = myDoc;
            Imprimir.myPage     = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);
            Imprimir.cmpCoObra  = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista;
            Imprimir.cmpIdOS    = ViewState["cmpIdOS"].ToString();
            Imprimir.NomeObra   = cmbObra.SelectedItem.ToString();
            Imprimir.EnderecoLogoOrion = Server.MapPath("~/Imagens/logo_Orion.jpg");

            if (ViewState["cmpInLogoObra"].ToString() == "1")
                Imprimir.EnderecoLogoObra = Server.MapPath("~/Imagens/logo_IPEN.bmp");
            else
                Imprimir.EnderecoLogoObra = "";

            Imprimir.ImprimeOrdemServico();

            string filename = "pdf" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf";
            string nomepdf = Server.MapPath("~/Relatorios/" + filename);
            myDoc.createPDF(nomepdf);

            Response.Write("<script language='javascript'>"
                                + "window.open('" + Global.UrlRelatorio + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105')"
                                + "</script>");
 
            // Impressão ambiente produção ambiente interno orion
            //Response.Write("<script language='javascript'>"
            //                  + "window.open('" + @"http://172.10.10.2/HzWebManutencao_Desenv/Relatorios/" + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105')"
            //                  + "</script>");

        }

        #endregion

        protected void btnEquipamento_Click(object sender, EventArgs e)
        {
            ModalPopupExtender5.Show();
        }

        protected void lnkEditar_Click(object sender, EventArgs e)
        {
            LinkButton lnkEditar = sender as LinkButton;

            GridViewRow gvrow = (GridViewRow)lnkEditar.NamingContainer;
            ViewState["cmpIdEquipamentoObra"] = lnkEditar.CommandArgument.ToString();
            txtEquipamentoObra.Text = gvrow.Cells[1].Text.ToString();
            //ViewState["cmpIdTipoEquipamento"] = gvrow.Cells[9].Text.ToString();

            ////loadEquipamentoObra();

            //ViewState["edicao"] = "Sim";

            //this.ModalPopupExtender2.Show();
        }
        protected void grdPesquisa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdPesquisa.PageIndex = e.NewPageIndex;
                grdPesquisa.DataSource = pesquisa();
                grdPesquisa.DataBind();
                ModalPopupExtender5.Show();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        private DataTable pesquisa()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc;

            lc = new ListCampo();
            lc.NomeCampo = "cmpCoObraGrupoLista";
            lc.TipoCampo = TipoCampo.Numero;
            lc.Sinal = SinalPesquisa.Igual;
            lc.ValorCampo = cmbObra.SelectedValue;
            ls.Add(lc);


            //if (cmbObraPavimento.SelectedValue != "0")
            //{
            //    lc = new ListCampo();
            //    lc.NomeCampo = "cmpCoObraPavimento";
            //    lc.TipoCampo = TipoCampo.Numero;
            //    lc.Sinal = SinalPesquisa.Igual;
            //    lc.ValorCampo = cmbObraPavimento.SelectedValue;
            //    ls.Add(lc);
            //}
            return tblEquipamentoObra.Get(Global.GetConnection(), ls);
        }

        protected void cmbTecnico_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmbObraPavimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdPesquisa.DataSource = pesquisa();
            grdPesquisa.DataBind();
        }
    }
}