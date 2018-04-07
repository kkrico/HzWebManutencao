using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using sharpPDF;
using sharpPDF.Enumerators;
using sharpPDF.Tables;

using HzLibCorporativo.Funcional;
using HzLibConnection.Sql;
using HzlibWEB;
using HzLibManutencao;
using Apresentacao.Controles;

using HzWebManutencao.Classes.Relatorios;

namespace HzWebManutencao.Preventiva
{
    public partial class webPRE_PreventivaPesquisa : System.Web.UI.Page
    {
        #region Functions

        private string LimiteConfirmacao(string CoPeriodicidade, string DtReprogramacao)
        {
            DateTime Dtlimite = DateTime.Parse(DtReprogramacao);

            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpCoPeriodicidade";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = CoPeriodicidade;
            ls.Add(lc);

            using (DataTable table = tblPeriodicidade.Get(Global.GetConnection(), ls))
            {
                int NumeroDias = int.Parse(table.Rows[0]["cmpNuDiasConfirmacaoExecucao"].ToString());

                for (int i = 1; i <= NumeroDias; i++)
                {
                    Dtlimite = Dtlimite.AddDays(1);
                    Dtlimite = Dtlimite.DayOfWeek == DayOfWeek.Saturday ? Dtlimite.AddDays(2) : Dtlimite;
                }
            }

            return Dtlimite.ToString("dd/M/yyyy");
        }

        private string LimiteReprogramacao(string DcPeriodicidade,string DtExecucao )
        {
            DateTime Dtlimite       = DateTime.Parse(DtExecucao);
            DateTime PrimeiroDiaMes = Convert.ToDateTime("1/" + Dtlimite.ToString("M/yyyy"));
            DateTime UltimoDiaMes   = PrimeiroDiaMes.AddMonths(1).AddDays(-1);
            UltimoDiaMes = UltimoDiaMes.DayOfWeek == DayOfWeek.Saturday ? UltimoDiaMes.AddDays(-1): UltimoDiaMes;
            UltimoDiaMes = UltimoDiaMes.DayOfWeek == DayOfWeek.Sunday ? UltimoDiaMes.AddDays(-2): UltimoDiaMes;

            switch (DcPeriodicidade.ToUpper())
            {
                case "SEMANAL":
                    Dtlimite = Dtlimite.AddDays(4);
                    Dtlimite = Dtlimite.DayOfWeek == DayOfWeek.Saturday ? Dtlimite.AddDays(-1) : Dtlimite;
                    Dtlimite = Dtlimite.DayOfWeek == DayOfWeek.Sunday ? Dtlimite.AddDays(-2) : Dtlimite;
                    break;
                case "QUINZENAL":
                    if (Dtlimite.Day <= 6)
                    {
                        Dtlimite = Dtlimite.AddDays(14);
                    }
                    else
                    {
                        Dtlimite = UltimoDiaMes;
                    }
                    break;
                case "MENSAL":
                    Dtlimite = UltimoDiaMes;
                    break;
                case "BIMESTRAL":
                    UltimoDiaMes = PrimeiroDiaMes.AddMonths(2).AddDays(-1);
                    Dtlimite = UltimoDiaMes.DayOfWeek == DayOfWeek.Saturday ? UltimoDiaMes.AddDays(-1) : UltimoDiaMes;
                    Dtlimite = UltimoDiaMes.DayOfWeek == DayOfWeek.Sunday ? UltimoDiaMes.AddDays(-2) : UltimoDiaMes;
                    break;
                case "TRIMESTRAL":
                    UltimoDiaMes = PrimeiroDiaMes.AddMonths(3).AddDays(-1);
                    Dtlimite = UltimoDiaMes.DayOfWeek == DayOfWeek.Saturday ? UltimoDiaMes.AddDays(-1) : UltimoDiaMes;
                    Dtlimite = UltimoDiaMes.DayOfWeek == DayOfWeek.Sunday ? UltimoDiaMes.AddDays(-2) : UltimoDiaMes;
                    break;
                case "SEMESTRAL":
                    UltimoDiaMes = PrimeiroDiaMes.AddMonths(6).AddDays(-1);
                    Dtlimite = UltimoDiaMes.DayOfWeek == DayOfWeek.Saturday ? UltimoDiaMes.AddDays(-1) : UltimoDiaMes;
                    Dtlimite = UltimoDiaMes.DayOfWeek == DayOfWeek.Sunday ? UltimoDiaMes.AddDays(-2) : UltimoDiaMes;
                    break;
                case "ANUAL":
                    UltimoDiaMes = PrimeiroDiaMes.AddMonths(12).AddDays(-1);
                    Dtlimite = UltimoDiaMes.DayOfWeek == DayOfWeek.Saturday ? UltimoDiaMes.AddDays(-1) : UltimoDiaMes;
                    Dtlimite = UltimoDiaMes.DayOfWeek == DayOfWeek.Sunday ? UltimoDiaMes.AddDays(-2) : UltimoDiaMes;
                    break;
            }
          
            return Dtlimite.ToString("dd/M/yyyy");
        }

        /// <summary>
        /// Validar confirmação da agenda preventiva.
        /// </summary>
        private bool ValidarConfirmacao()
        {
            bool ret = true;
            string msg = "";

            if (TxtDtConfirmacao.Text == "")
                msg += "Data de confirmação da preventiva em branco! <br />";
            else if (DateTime.Compare(DateTime.Parse(TxtDtConfirmacao.Text.ToString()), DateTime.Parse(txtDtAgenda.Text.ToString())) < 0)
                msg += "Data confirmação menor que data agendada! <br />";
            else if (DateTime.Compare(DateTime.Parse(TxtDtConfirmacao.Text.ToString()), DateTime.Parse(txtLimiteConfirma.Text.ToString())) > 0)
                msg += "Data confirmação maior que data limite para confirmação! <br />";
            if (TxtGestor.Text == "")
                msg += "Nome do gestor em branco! <br />";
            if (txtFuncionario.Text == "")
                msg += "Nome do funcionario em branco! <br />";
            if (msg != "")
            {
                CaixaMensagem.Mostar(Mensagem.Tipo.Erro, msg);
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Validar reprogramação da agenda preventiva.
        /// </summary>
        private bool ValidarReprogramacao()
        {
            bool ret = true;
            string msg = "";

            if (TxtDtReprogramar.Text == "")
                msg += "Data de reprogramação da preventiva em branco! <br />";
            else if (DateTime.Compare(DateTime.Parse(TxtDtReprogramar.Text.ToString()), DateTime.Parse(txtDtExecucao.Text.ToString())) < 0)
                msg += "Data reprogramação menor que data início da execução! <br />";
            else if (DateTime.Compare(DateTime.Parse(TxtDtReprogramar.Text.ToString()), DateTime.Parse(txtLimiteRepro.Text.ToString())) > 0)
                msg += "Data reprogramação maior que data limite para reprogramação! <br />";
            if (msg != "")
            {
                CaixaMensagem.Mostar(Mensagem.Tipo.Erro, msg);
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Carrega as listboxes.
        /// </summary>
        private void load()
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoContratante";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoContratante;
                ls.Add(lc);

                using (DataTable table = tblTipoAtividade.Get(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbTipoAtividade, table, "cmpDcTipoAtividade", "cmpCoTipoAtividade", "cmpDcTipoAtividade", "--- Selecione a atividade ---", true);
                }

                using (DataTable tbl = tblPeriodicidade.Get(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbPeriodicidade, tbl, "cmpDcPeriodicidade", "cmpCoPeriodicidade", "cmpDcPeriodicidade", "--- Selecione ---", true);
                }

            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Carrega as combos.
        /// </summary>
        private void loadObra()
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
                    ViewState["cmpInLogoObra"] = table.Rows[0]["cmpInLogoObra"].ToString() == "True" ? "1" : "0"; 

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
        /// Pesquisa Preventiva Agenda.
        /// </summary>
        private DataTable pesquisaPreventiva()
        {
            string CoObra          = cmbObra.SelectedValue.ToString();
            string DtInicial       = txtDataInicial.Text.ToString();
            string DtFinal         = txtDataFinal.Text.ToString();
            string TipoPrev        = rdbType.SelectedValue != "T" ? rdbType.SelectedValue : "0";
            string CoPeriodicidade = cmbPeriodicidade.SelectedValue.ToString();
            string CoTpAtividade   = cmbTipoAtividade.SelectedValue.ToString();
            string CoTpSituacao    = cmbSituacao.SelectedValue.ToString();

            return tblPreventivaAgenda.RetornaAgendaPreventiva(Global.GetConnection(), CoObra, DtInicial, DtFinal, TipoPrev,CoPeriodicidade, CoTpAtividade, CoTpSituacao);
        }

        private void MontaPopUp()
        {
            string filename;
            string nomepdf;
            pdfDocument myDoc;

            using (DataTable table = pesquisaPreventiva())
            {
                if (table != null && table.Rows.Count > 0)
                {
                    ImprimirAgendaPreventiva ImprimirAgendaPreventiva = new ImprimirAgendaPreventiva();
                    ImprimirAgendaPreventiva.EnderecoLogoOrion = Server.MapPath("~/Imagens/logo_Orion.jpg");

                    if (ViewState["cmpInLogoObra"].ToString() == "1")
                        ImprimirAgendaPreventiva.EnderecoLogoObra = Server.MapPath("~/Imagens/logo_IPEN.bmp");
                    else
                        ImprimirAgendaPreventiva.EnderecoLogoObra = "";

                    filename = "AgendaPreventiva" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf";
                    nomepdf = Server.MapPath("~/Relatorios/" + filename);

                    myDoc = ImprimirAgendaPreventiva.ImprimeAgenda(table, cmbObra.SelectedItem.ToString(), txtDataInicial.Text.ToString(), txtDataFinal.Text.ToString());
                    myDoc.createPDF(nomepdf);

                    btnImprimir.Visible = true;
                    btnImprimir.Attributes.Add("onclick", "window.open('" + Global.UrlRelatorio + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');");
                }
                else
                    btnImprimir.Visible = false;
            }
        }

        #endregion

        #region events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.load();
                this.loadObra();
            }
            //else
            //{
            //    Control c = Objetos.GetPostBackControl(this.Page);
            //    if (c != null)
            //    {
            //        switch (c.ID)
            //        {
            //            case "btnPesquisar":
            //                btnPesquisar.CausesValidation = true;
            //                break;
            //            case "btnUpdate":
            //                btnUpdate.CausesValidation = true;
            //                break;
            //            case "btnSaveRepro":
            //                btnSaveRepro.CausesValidation = true;
            //                break;
            //            case "lnkConfirmar":

            //                btnSaveRepro.CausesValidation = false;
            //                btnUpdate.CausesValidation = true;

            //                //TxtDtReprogramar.CausesValidation   = false;
            //                //txtLimiteRepro.CausesValidation     = false;

            //                //txtDtAgenda.CausesValidation        = true;
            //                //TxtDtConfirmacao.CausesValidation   = true;
            //                //TxtGestor.CausesValidation          = true;
            //                //txtFuncionario.CausesValidation     = true;
            //                //rdbState.CausesValidation           = true;

            //                break;
            //            case "lnkReprogramar":
            //                btnSaveRepro.CausesValidation = true;
            //                btnUpdate.CausesValidation = false;

            //                //txtDtAgenda.CausesValidation        = false;
            //                //TxtDtConfirmacao.CausesValidation   = false;
            //                //TxtGestor.CausesValidation          = false;
            //                //txtFuncionario.CausesValidation     = false;
            //                //rdbState.CausesValidation           = false;

            //                //TxtDtReprogramar.CausesValidation   = true;
            //                //txtLimiteRepro.CausesValidation     = true;

            //                break;
            //        }                    
            //    }
            //}
        }

        protected void cmbObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista = cmbObra.SelectedValue;
            if (txtDataInicial.Text == "")
            {
                txtDataInicial.Text = DateTime.Now.ToShortDateString();
                txtDataFinal.Text   = DateTime.Now.ToShortDateString();
            }
            btnPesquisar_Click(sender, e);
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            DataTable table = pesquisaPreventiva();
            grdPreventivaPesq.DataSource = table;
            grdPreventivaPesq.DataBind();
            MontaPopUp();
         }

        protected void rdbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
            MontaPopUp();
        }

        protected void grdPreventivaPesq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdPreventivaPesq.PageIndex = e.NewPageIndex;
                grdPreventivaPesq.DataSource = pesquisaPreventiva();
                grdPreventivaPesq.DataBind();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        protected void txtData_TextChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        protected void txtDataFinal_TextChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        protected void grdPreventivaPesq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[9].Visible = false;
                    e.Row.Cells[10].Visible = false;
                    e.Row.Cells[11].Visible = false;
                    e.Row.Cells[13].Visible = false;
                    e.Row.Cells[14].Visible = false;
                    e.Row.Cells[15].Visible = false;
                    break;
                case DataControlRowType.DataRow:
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[9].Visible = false;
                    e.Row.Cells[10].Visible = false;
                    e.Row.Cells[11].Visible = false;
                    e.Row.Cells[13].Visible = false;
                    e.Row.Cells[14].Visible = false;
                    e.Row.Cells[15].Visible = false;

                    if (e.Row.Cells[6].Text.Equals("EQUIPAMENTO"))
                    {
                        if (e.Row.Cells[11].Text.Equals("False"))
                        {
                            if (e.Row.Cells[14].Text.Equals("&nbsp;"))
                            {
                                e.Row.Cells[8].FindControl("LinkButton1").Visible = false;
                                e.Row.Cells[8].FindControl("LinkButton2").Visible = false;
                                e.Row.Cells[8].FindControl("LinkButton3").Visible = true;
                            }
                            else if (e.Row.Cells[9].Text.Equals("&nbsp;"))
                            {
                                e.Row.Cells[8].FindControl("LinkButton1").Visible = false;
                                e.Row.Cells[8].FindControl("LinkButton2").Visible = true;
                                e.Row.Cells[8].FindControl("LinkButton3").Visible = false;
                            }
                            else
                            {
                                e.Row.Cells[8].FindControl("LinkButton1").Visible = true;
                                e.Row.Cells[8].FindControl("LinkButton2").Visible = false;
                                e.Row.Cells[8].FindControl("LinkButton3").Visible = false;
                            }
                        }
                        else
                            e.Row.Cells[8].Text = "Não";
                    }
                    else
                    {
                        if (e.Row.Cells[11].Text.Equals("False"))
                        {
                            if (e.Row.Cells[9].Text.Equals("&nbsp;"))
                            {
                                e.Row.Cells[8].FindControl("LinkButton1").Visible = false;
                                e.Row.Cells[8].FindControl("LinkButton2").Visible = true;
                                e.Row.Cells[8].FindControl("LinkButton3").Visible = false;
                            }
                            else
                            {
                                e.Row.Cells[8].FindControl("LinkButton1").Visible = true;
                                e.Row.Cells[8].FindControl("LinkButton2").Visible = false;
                                e.Row.Cells[8].FindControl("LinkButton3").Visible = false;
                            }
                        }
                    }

                    LinkButton lnkReprogramar = ((LinkButton)e.Row.Cells[12].FindControl("lnkReprogramar"));
                    //*********** Periodicidade Diária ************
                    if (e.Row.Cells[3].Text == "1") 
                    {
                        lnkReprogramar.Visible = e.Row.Cells[3].Text == "1" ? false : true; 
                        e.Row.Cells[12].Text = "Não Reprograma";
                    }
                    if (e.Row.Cells[7].Text.Equals("Expirada"))
                    {
                        // e.Row.BackColor = System.Drawing.Color.Cyan; 
                        e.Row.Cells[7].Font.Bold.ToString();
                        e.Row.Cells[7].ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[12].Text = "";
                        e.Row.Cells[0].Text = "";
                    }
                    if (e.Row.Cells[7].Text.Equals("Em Aberto"))
                    {
                        // e.Row.BackColor = System.Drawing.Color.Cyan; 
                        e.Row.Cells[7].ForeColor = System.Drawing.Color.Green;
                    }
                    if (e.Row.Cells[7].Text.Equals("Confirmada"))
                    {
                        // e.Row.BackColor = System.Drawing.Color.Cyan; 
                        e.Row.Cells[7].ForeColor = System.Drawing.Color.CornflowerBlue;
                        lnkReprogramar.Visible = e.Row.Cells[3].Text == "1" ? false : true;
                        e.Row.Cells[12].Text = "Não Reprograma";
                    }
                    break;
                case DataControlRowType.Footer:
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[8].Visible = false;
                    break;
            }
        }

        protected void grdPreventivaPesq_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] ParImpressao = e.CommandArgument.ToString().Split(new char[] { '$' });

            switch (e.CommandName.ToLower().Trim())
            {
                case "linkbutton1":
                    Response.Redirect("~/ATE/webATE_OS.aspx?id=" + e.CommandArgument.ToString(), false);
                    break;
                case "linkbutton2":
                    Response.Redirect("~/ATE/webATE_OS.aspx?IdPreventivaConfirmacao=" + ParImpressao[0].ToString() + "&NuPreventiva=" + ParImpressao[1].ToString(), false);
                    break;
                case "linkbutton3":
                    Response.Redirect("~/Preventiva/webPRE_Equipamento.aspx?CoAtividade=" + ParImpressao[0].ToString() + "&NuPreventiva=" + ParImpressao[1].ToString() + "&NoObra=" + cmbObra.SelectedItem + "&IdPreventivaConfirmacao=" + ParImpressao[2].ToString(), false);
                    break;
            }
        }

        protected void btnCancel1_Click(object sender, EventArgs e)
        {
            TxtDtReprogramar.Text   = null;
            TxtDtConfirmacao.Text   = "";
            TxtGestor.Text          = "";
            txtFuncionario.Text     = "";
            txtObservacao.Text      = "";
            this.ModalPopupExtender1.Hide();
            btnPesquisar_Click(sender, e);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidarConfirmacao())
            {
                tblPreventivaConfirmacao table = new tblPreventivaConfirmacao();
                try
                {
                    table.cmpIdPreventivaConfirmacao            = ViewState["cmpIdPreventivaConfirmacao"].ToString();
                    table.cmpIdPreventivaAgenda                 = ViewState["cmpIdPreventivaAgenda"].ToString();
                    table.cmpDtConfirmacaoPreventiva            = TxtDtConfirmacao.Text.ToString();
                    table.cmpNoGestorPreventivaConfirmacao      = TxtGestor.Text.TrimEnd();
                    table.cmpNoFuncionarioPreventivaConfirmacao = txtFuncionario.Text.TrimEnd();
                    table.cmpInOcorrenciaPreventiva             = rdbState.SelectedValue;
                    table.cmpDcObservacaoPreventivaConfirmacao  = txtObservacao.Text.TrimEnd();
                    table.cmpNoUsuario                          = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;

                    using (DataTable tbl = table.GravarPreventivaConfirmacao(Global.GetConnection()))
                    {
                        if (tbl != null && tbl.Rows.Count > 0)
                        {
                            TxtDtConfirmacao.Text   = "";
                            TxtGestor.Text          = "";
                            txtFuncionario.Text     = "";
                            txtObservacao.Text      = "";

                            this.ModalPopupExtender1.Hide();

                            if (rdbState.SelectedValue == "0")
                            {
                                // Preventiva de Equipamento
                                if (ViewState["cmpTpPreventiva"].ToString() == "EQUIPAMENTO")
                                {
                                    Response.Redirect("~/Preventiva/webPRE_Equipamento.aspx?CoAtividade=" + ViewState["cmpCoTipoAtividade"] + "&NuPreventiva=" + tbl.Rows[0][0].ToString() + "&NoObra=" + cmbObra.SelectedItem + "&IdPreventivaConfirmacao=" + tbl.Rows[0][1].ToString(), false);
                                }
                                else
                                {
                                    Response.Redirect("~/ATE/webATE_OS.aspx?IdPreventivaConfirmacao=" + tbl.Rows[0][1].ToString() + "&NuPreventiva=" + tbl.Rows[0][0].ToString(), false);
                                }
                            }
                            grdPreventivaPesq.DataSource = pesquisaPreventiva();
                            grdPreventivaPesq.DataBind();
                            CaixaMensagem.Mostar(Mensagem.Tipo.Sucesso, "Preventiva confirmada com sucesso.");
                        }
                    }
                    TxtDtReprogramar.Text = null;
                }
                catch (Exception ex)
                {
                    Global.ShowError(Global.Title, ex);
                }
            }
            else
            {
                btnPesquisar_Click(sender, e);
            }
        }

        protected void lnkEditarPreventiva_Click(object sender, EventArgs e)
        {
            LinkButton lnkConfirmar = sender as LinkButton;
            DataKey dk = grdPreventivaPesq.SelectedDataKey;


            ViewState["cmpIdPreventivaAgenda"] = lnkConfirmar.CommandArgument.ToString();
            int row=int.Parse(lnkConfirmar.CommandArgument.ToString());
            //Response.Redirect("webPRE_EditarPreventiva.aspx?idPreventivaAgenda=" + grdPreventivaPesq.DataKeys[row].Values[0].ToString());
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open(webPRE_EditarPreventiva.aspx?idPreventivaAgenda=" + grdPreventivaPesq.DataKeys[row].Values[0].ToString() + "');", true);
            //Response.Write( "<script> window.open(webPRE_EditarPreventiva.aspx?idPreventivaAgenda=" + grdPreventivaPesq.DataKeys[row].Values[0].ToString() + "</script>");
            //   Response.End();
            Response.Write("<script>window.open('webPRE_EditarPreventiva.aspx?idPreventivaAgenda=" +  grdPreventivaPesq.DataKeys[row].Values[0].ToString() + "&cmpCoObraGrupolista=" + cmbObra.SelectedValue + "','_blank');</script>");
        }
        protected void lnkConfirmar_Click(object sender, EventArgs e)
        {
            LinkButton lnkConfirmar = sender as LinkButton;

            GridViewRow gvrow = (GridViewRow)lnkConfirmar.NamingContainer;
            ViewState["cmpIdPreventivaAgenda"]  = lnkConfirmar.CommandArgument.ToString();
            ViewState["cmpTpPreventiva"]        = gvrow.Cells[6].Text;
            ViewState["cmpCoTipoAtividade"]     = gvrow.Cells[13].Text;

            lblTipoAtividade.Text       = gvrow.Cells[5].Text;
            lblTipoPreventiva.Text      = "Tipo Preventiva: " + gvrow.Cells[6].Text;
            txtDtAgenda.Text            = gvrow.Cells[15].Text.ToString();  //txtData.Text.ToString();
            txtDtAgenda.Enabled         = false;
            txtLimiteConfirma.Text      = LimiteConfirmacao(gvrow.Cells[3].Text, gvrow.Cells[15].Text.ToString());
            txtLimiteConfirma.Enabled   = false;

            if (gvrow.Cells[7].Text != "Em Aberto")
            {
                ViewState["cmpIdPreventivaConfirmacao"] = gvrow.Cells[10].Text;
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpIdPreventivaConfirmacao";
                lc.TipoCampo = TipoCampo.Numero;
                lc.Sinal = SinalPesquisa.Igual;
                lc.ValorCampo = gvrow.Cells[10].Text;
                ls.Add(lc);
                using (DataTable table = tblPreventivaConfirmacao.Get(Global.GetConnection(), ls))
                {
                    this.txtFuncionario.Text        = table.Rows[0]["cmpNoFuncionarioPreventivaConfirmacao"].ToString();
                    this.TxtGestor.Text             = table.Rows[0]["cmpNoGestorPreventivaConfirmacao"].ToString();
                    this.txtObservacao.Text         = table.Rows[0]["cmpDcObservacaoPreventivaConfirmacao"].ToString();
                    this.rdbState.Items[1].Selected = bool.Parse(table.Rows[0]["cmpInOcorrenciaPreventiva"].ToString());
                    this.TxtDtConfirmacao.Text      = (DateTime.Parse(table.Rows[0]["cmpDtConfirmacaoPreventiva"].ToString()).ToString());

                    this.TxtDtConfirmacao.Enabled = false;
                }
            }
            else
            {
                ViewState["cmpIdPreventivaConfirmacao"] = 0;
                this.btnUpdate.Enabled          = true;
                this.TxtDtConfirmacao.Enabled   = true;
                this.txtFuncionario.Enabled     = true;
                this.TxtGestor.Enabled          = true;
                this.txtObservacao.Enabled      = true;
                this.rdbState.Enabled           = true;
                this.TxtDtConfirmacao.Focus();
            }
            this.ModalPopupExtender1.Show();
        }

        protected void lnkReprogramar_Click(object sender, EventArgs e)
        {
            LinkButton lnkReprogramar = sender as LinkButton;

            GridViewRow gvrow = (GridViewRow)lnkReprogramar.NamingContainer;
            ViewState["cmpIdPreventivaAgenda"] = lnkReprogramar.CommandArgument.ToString();

            lblTipoAtividade.Text = gvrow.Cells[4].Text;
            lblTipoPreventiva.Text = "Tipo Preventiva: " + gvrow.Cells[6].Text;

            ListCampos ls   = new ListCampos();
            ListCampo lc    = new ListCampo();
            lc.NomeCampo    = "cmpIdPreventivaAgenda";
            lc.TipoCampo    = TipoCampo.Numero;
            lc.Sinal        = SinalPesquisa.Igual;
            lc.ValorCampo   = ViewState["cmpIdPreventivaAgenda"].ToString();
            ls.Add(lc);

            using (DataTable table = tblPreventivaAgenda.Get(Global.GetConnection(), ls))
            {
                this.lblObra.Text           = cmbObra.SelectedItem.ToString();
                this.lblAtividade.Text      = table.Rows[0]["cmpDcTipoAtividade"].ToString();
                this.lblPeriodicidade.Text  = table.Rows[0]["cmpDcPeriodicidade"].ToString();
                this.txtDtExecucao.Text     = (DateTime.Parse(table.Rows[0]["cmpDtExecucaoPreventivaAgenda"].ToString()).ToString());
                this.txtLimiteRepro.Text    = LimiteReprogramacao(lblPeriodicidade.Text.ToString(),txtDtExecucao.Text.ToString());

                this.txtDtExecucao.Enabled = false;
                this.txtLimiteRepro.Enabled = false;

                if (gvrow.Cells[7].Text != "Em Aberto")
                {
                    this.TxtDtReprogramar.Text = (DateTime.Parse(table.Rows[0]["cmpDtReprogramacaoPreventivaAgenda"].ToString()).ToString());
                    this.btnSaveRepro.Enabled = false;
                }
                else
                {
                    this.TxtDtReprogramar.Enabled = true;
                    this.TxtDtReprogramar.Text = "";
                    this.TxtDtReprogramar.Focus();
                    this.btnSaveRepro.Enabled = true;
                }
            }
            this.ModalPopupExtender2.Show();
        }

        protected void btnCancelRepro_Click(object sender, EventArgs e)
        {
            this.TxtDtConfirmacao.Text  = null;
            this.txtFuncionario.Text    = null;
            this.TxtGestor.Text         = null;
            this.txtObservacao.Text     = null;
            this.rdbState.ClearSelection();

            this.ModalPopupExtender2.Hide();
            btnPesquisar_Click(sender, e);
        }

        protected void btnSaveRepro_Click(object sender, EventArgs e)
        {
            if (ValidarReprogramacao())
            {
                try
                {
                    tblPreventivaAgenda table       = new tblPreventivaAgenda();
                    table.cmpIdPreventivaAgenda     = ViewState["cmpIdPreventivaAgenda"].ToString();
                    table.cmpDtReprogramacaoPreventivaAgenda = this.TxtDtReprogramar.Text.ToString();
                    table.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;

                    if ((table.Save(Global.GetConnection())))
                    {
                        grdPreventivaPesq.DataSource = pesquisaPreventiva();
                        grdPreventivaPesq.DataBind();
                        this.ModalPopupExtender2.Hide();
                        CaixaMensagem.Mostar(Mensagem.Tipo.Sucesso, "Preventiva reprogramada com sucesso.");
                    }
                    this.TxtDtConfirmacao.Text = null;
                    this.txtFuncionario.Text = null;
                    this.TxtGestor.Text = null;
                    this.txtObservacao.Text = null;
                    this.rdbState.ClearSelection();
                }
                catch (Exception ex)
                {
                    Global.ShowError(Global.Title, ex);
                }
            }
            else
            {
                btnPesquisar_Click(sender, e);
            }
        }

        protected void cmbPeriodicidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        protected void cmbTipoAtividade_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        protected void cmbSituacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }
        
        #endregion

        protected void btnResumoPreventiva_Click(object sender, EventArgs e)
        {
            //myDoc = ImprimirAgendaPreventiva.ImprimeAgenda(table, cmbObra.SelectedItem.ToString(), txtDataInicial.Text.ToString(), txtDataFinal.Text.ToString());

            RelatoriosReportServer.clRelatorios rel = new RelatoriosReportServer.clRelatorios();
            string nomeRel= rel.ManutencaoPreventiva(int.Parse(cmbObra.SelectedItem.Value.ToString()), DateTime.Parse(txtDataInicial.Text), DateTime.Parse(txtDataFinal.Text));

            this.Response.Write("<SCRIPT language=javascript>window.open('../" + nomeRel + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');</SCRIPT>");
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {

        }

     }

}