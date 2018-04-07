using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibConnection.Sql;
using System.Data;
using HzLibCorporativo.Funcional;
using HzLibCorporativo.Geral;
using HzlibWEB;
using HzLibManutencao;
using sharpPDF;
using sharpPDF.Enumerators;
using sharpPDF.Tables;
using Apresentacao.Controles;

namespace HzWebManutencao.Preventiva
{
    public partial class webPRE_FormularioPreventiva : System.Web.UI.Page
    {
        #region Functions
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

                lc = new ListCampo();
                lc.NomeCampo = "cmpCoObraGrupoLista";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = cmbObra.SelectedValue.ToString();
                ls.Add(lc);

                using (DataTable table = tblTipoAtividade.RetornaTipoAtividadeObra(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbTipoAtividade, table, "cmpDcTipoAtividade", "cmpCoTipoAtividade", "cmpDcTipoAtividade", "--- Selecione todas ---", true);
                }

                using (DataTable table = tblPeriodicidade.RetornaPeriodicidadeObra(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbPeriodicidade, table, "cmpDcPeriodicidade", "cmpCoPeriodicidade", "cmpDcPeriodicidade", "--- Selecione todas ---", true);
                }

            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Carrega as listboxes.
        /// </summary>
        private void loadPeriodicidadeTpAtividade()
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoContratante";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoContratante;
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "cmpCoObraGrupoLista";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = cmbObra.SelectedValue.ToString();
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "cmpCoTipoAtividade";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = cmbTipoAtividade.SelectedValue.ToString();
                ls.Add(lc);

                using (DataTable table = tblPeriodicidade.RetornaPeriodicidadeTipoAtividade(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbPeriodicidade, table, "cmpDcPeriodicidade", "cmpCoPeriodicidade", "cmpDcPeriodicidade", "--- Selecione todas ---", true);
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
            string TpEquipamento = rdbType.SelectedValue != "T" ? rdbType.SelectedValue : "0";
//            return tblPreventivaAgenda.RetornaFormPreventiva(Global.GetConnection(), cmbObra.SelectedValue.ToString(), cmbPeriodicidade.SelectedValue.ToString(), cmbTipoAtividade.SelectedValue.ToString(), TpEquipamento, "1");
            return tblPreventivaAgenda.RetornaFormPreventiva(Global.GetConnection(), cmbObra.SelectedValue.ToString(), cmbPeriodicidade.SelectedValue.ToString(), cmbTipoAtividade.SelectedValue.ToString(), TpEquipamento, DateTime.Now.Month.ToString());
        }

        private void limpa()
        {
            cmbPeriodicidade.Items.Clear();
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.loadObra();
                this.load();
            }
        }

        protected void cmbObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista = cmbObra.SelectedValue;
            this.load();
            cmbTipoAtividade.Focus();
            btnPesquisar_Click(sender, e);

        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            DataTable tbl = pesquisaPreventiva();

            if (tbl != null && tbl.Rows.Count > 0)
            {
                grdFormPreventiva.DataSource = tbl;
                grdFormPreventiva.DataBind();
            }
            else
            {
                CaixaMensagem.Mostar(Mensagem.Tipo.Aviso,"Pesquisa não encontrada!");
            }
        }

        protected void grdFormPreventiva_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdFormPreventiva.PageIndex = e.NewPageIndex;
                grdFormPreventiva.DataSource = pesquisaPreventiva();
                grdFormPreventiva.DataBind();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }

        }

        protected void grdFormPreventiva_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    break;
                case DataControlRowType.DataRow:
                    ImageButton ImagePavimento = ((ImageButton)e.Row.Cells[4].FindControl("ImgPavimento"));
                    ImageButton ImageEquipamento = ((ImageButton)e.Row.Cells[4].FindControl("ImgEquipamento"));
                    ImagePavimento.Visible = false;
                    ImageEquipamento.Visible = false;
                    //*********** Tipo de Preventiva (Pavimento / Equipamento ************
                    if (e.Row.Cells[2].Text == "Pavimento")
                    {
                        ImagePavimento.Visible = true;
                    }
                    else
                    {
                        ImageEquipamento.Visible = true;
                    }
                    break;
                case DataControlRowType.Footer:
                    break;
            }
        }

        protected void grdFormPreventiva_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string filename;
                string nomepdf; 
                pdfDocument myDoc;

                switch (e.CommandName.ToLower().Trim())
                {
                    case "btn1":
                        using (DataTable table = Global.GetConnection().loadDataTable("select * from HzManutencao..vwPRE_FormularioPreventiva where cmpCoPreventiva = " + e.CommandArgument.ToString()))
                        {
                            filename = "FormPreventiva" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf";
                            nomepdf  = Server.MapPath("~/Relatorios/" + filename);

                            myDoc = ImprimeFormPreventiva.ImprimeAtividades(table, cmbObra.SelectedItem.ToString(), cmbTipoAtividade.SelectedItem.ToString(), cmbPeriodicidade.SelectedItem.ToString());
                            myDoc.createPDF(nomepdf);

                            // Impressão ambiente desenvolvimento local
                            //Response.Write("<script language='javascript'>"
                            //              + "window.open('" + @"http://localhost:54215/Relatorios/" + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105')"
                            //              + "</script>");

                            // Impressão ambiente produção ambiente externo orion
                            Response.Write("<script language='javascript'>"
                                              + "window.open('" + @"http://201.39.115.18/HzWEBManutencao/Relatorios/" + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105')"
                                              + "</script>");

                        }
                        break;
                    case "btn2":
                        using (DataTable table = Global.GetConnection().loadDataTable("select * from HzManutencao..vwPRE_FormularioPreventivaEquipamento where cmpCoPreventiva = " + e.CommandArgument.ToString() + " ORDER BY CMPIDEQUIPAMENTOOBRA"))
                        {
                            if (table != null && table.Rows.Count > 0)
                            {
                                ImprimeFormPrevEquipamento Impressao = new ImprimeFormPrevEquipamento();
                                myDoc = Impressao.ImprimeAtividades(table);
                                filename = "FormPreventivaEquipamento" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf";
                                nomepdf = Server.MapPath("~/Relatorios/" + filename);
                                myDoc.createPDF(nomepdf);

                                // Impressão ambiente desenvolvimento local
                                //Response.Write("<script language='javascript'>"
                                //              + "window.open('" + @"http://localhost:54215/Relatorios/" + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105')"
                                //              + "</script>");

                                // Impressão ambiente produção ambiente externo orion
                                Response.Write("<script language='javascript'>"
                                                  + "window.open('" + @"http://201.39.115.18/HzWEBManutencao/Relatorios/" + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105')"
                                                  + "</script>");
                            }
                            else
                            {
                                //CaixaMensagem.Mostar(Mensagem.Tipo.Aviso, "Preventiva sem equipamento cadastrado!");
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Preventiva sem equipamento cadastrado!')", true);
                            }
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        protected void cmbTipoAtividade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                limpa();
                this.loadPeriodicidadeTpAtividade();
                cmbPeriodicidade.Focus();
                btnPesquisar_Click(sender, e);
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }

        }

        protected void cmbPeriodicidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        protected void rdbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        protected void grdFormPreventiva_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}