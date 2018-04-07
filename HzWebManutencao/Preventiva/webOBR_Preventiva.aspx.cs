using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibConnection.Sql;
using System.Data;
using System.Configuration;

using sharpPDF;
using sharpPDF.Enumerators;
using sharpPDF.Tables;

using HzLibManutencao;
using HzlibWEB;
using HzLibCorporativo.Funcional;
using HzLibGeneral.Util;
using HzWebManutencao.Classes.Relatorios;
using Apresentacao.Controles;

namespace HzWebManutencao.Preventiva
{
    public partial class webOBR_Preventiva : System.Web.UI.Page
    {
        #region
        private string filename;
        private int IdEquipamentoObra;
        #endregion


        #region Functions

        //protected static void Redirect(string url, string target, string windowFeatures)
        //{
        //    "frmErro.aspx?id=" + ConfigurationManager.AppSettings["RelPdf"] + filename, "_blank", ""

        //    HttpContext.Current.RewritePath("~/frmErro.aspx?file=" + ConfigurationManager.AppSettings["RelPdf"] + filename, "_blank", "");

        //    HttpContext context = HttpContext.Current;
        //    if ((String.IsNullOrEmpty(target) || target.Equals("_self", StringComparison.OrdinalIgnoreCase)) && String.IsNullOrEmpty(windowFeatures))
        //    {
        //        context.Response.Redirect(url);
        //    }
        //    else
        //    {
        //        var page = (Page)context.Handler;
        //        if (page == null)
        //            throw new InvalidOperationException("Cannot redirect to new window outside Page context.");

        //        url = page.ResolveClientUrl(url);
        //        string script = !String.IsNullOrEmpty(windowFeatures) ? @"window.open(""{0}"", ""{1}"", ""{2}"");" : @"window.open(""{0}"", ""{1}"");";
        //        script = String.Format(script, url, target, windowFeatures);
        //        ScriptManager.RegisterStartupScript(page, typeof(Page), "Redirect", script, true);
        //    }
        //}

        /// <summary>
        /// Carrega as atividades das obras de acordo com a periodicidade e o 
        /// grupo de atividade.
        /// </summary>
        public void loadObraAtividades()
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoObraGrupoLista";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((ListItem)cmbObra.SelectedItem).Value;
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "cmpCoPeriodicidade";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((ListItem)cmbPeriodicidade.SelectedItem).Value;
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "cmpCoTipoAtividade";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((ListItem)cmbTipoAtividade.SelectedItem).Value;
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "cmpTpPreventiva";
                lc.TipoCampo = TipoCampo.String;
                lc.ValorCampo = ((ListItem)rdbType.SelectedItem).Value;
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "cmpCoGrupoAtividade";
                lc.TipoCampo = TipoCampo.String;
                lc.ValorCampo = ((ListItem)cmbGrupoAtividade.SelectedItem).Value;
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "[cmpCoPreventivaEspelho]";
                lc.TipoCampo = TipoCampo.Null;
                lc.ValorCampo =null;
                ls.Add(lc);

                using (DataTable table = tblPreventiva.GetAll(Global.GetConnection(), ls))
                {
                    if (table != null && table.Rows.Count > 0)
                    {
                        Objetos.LoadCombo(lstAtividadeObra, table, "cmpDcItemAtividadePreventiva", "cmpCoPreventivaAtividade", "cmpDcItemAtividadePreventiva", true);
                        lstAtividadeObra.SelectedIndex = lstAtividadeObra.Items.IndexOf(lstAtividadeObra.Items.FindByValue(table.Rows[0]["cmpCoPreventivaAtividade"].ToString()));
                        lstAtividadeObra_SelectedIndexChanged(lstAtividadeObra, EventArgs.Empty);

                        tblPreventivaEquipamentocs PrevEquip = new tblPreventivaEquipamentocs();
                        DataTable dt= PrevEquip.GetPreventivaEquipamento(Global.GetConnection(), int.Parse(ViewState["cmpCoPreventiva"].ToString()),int.Parse(cmbGrupoAtividade.SelectedItem.Value));
                        if (dt.Rows.Count > 0)
                        {
                            txtEquipamento.Text = dt.Rows[0]["CmpDcEquipamento"].ToString() + "|" + dt.Rows[0]["cmpDcLocalEquipamento"].ToString();
                            ViewState["IdEquipamentoObra"] = dt.Rows[0]["CmpDcEquipamento"].ToString();
                        }
                        else
                        {
                            //ViewState["IdEquipamentoObra"] = null;
                           // txtEquipamento.Text = null;
                        }
                    }
                }
                using (DataTable table = tblPreventiva.GetAllNotIN(Global.GetConnection(), ((ListItem)cmbObra.SelectedItem).Value, ((ListItem)cmbPeriodicidade.SelectedItem).Value, ((ListItem)rdbType.SelectedItem).Value, ((ListItem)cmbGrupoAtividade.SelectedItem).Value))
                {
                    if (table != null && table.Rows.Count > 0)
                    {
                        Objetos.LoadCombo(lstItemAtividade, table, "cmpDcItemAtividade", "cmpCoItemAtividade", "cmpDcItemAtividade", true);
                        lstItemAtividade.SelectedIndex = lstItemAtividade.Items.IndexOf(lstItemAtividade.Items.FindByValue(table.Rows[0]["cmpCoItemAtividade"].ToString()));
                        lstItemAtividade_SelectedIndexChanged(lstItemAtividade, EventArgs.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        private void limpa()
        {
            ViewState["IdEquipamentoObra"] = null;
            ViewState["cmpCoPreventiva"] = "0";
            cmbPeriodicidade.Focus();
            lstAtividadeObra.Items.Clear();
            lstItemAtividade.Items.Clear();
            txtDescriptionItemObra.Text = "";
            txtDescriptionItemRef.Text = "";
            txtEquipamento.Text = null; ;
            txtEquipamento.Text = null;
            //cmbEquipamentos.Items.Clear();
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

                using (DataTable table = tblPeriodicidade.Get(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbPeriodicidade, table, "cmpDcPeriodicidade", "cmpCoPeriodicidade", "cmpDcPeriodicidade", "--- Selecione ---", true);
                    cmbPeriodicidade_SelectedIndexChanged(cmbPeriodicidade, EventArgs.Empty);
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
        private void loadTipoAtividade()
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
                lc.NomeCampo = "cmpTpGrupoAtividade";
                lc.TipoCampo = TipoCampo.String;
                lc.ValorCampo = rdbType.SelectedValue.ToString();
                ls.Add(lc);

                using (DataTable table = tblTipoAtividade.RetornaTipoAtividade(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbTipoAtividade, table, "cmpDcTipoAtividade", "cmpCoTipoAtividade", "cmpDcTipoAtividade", "--- Selecione um tipo de atividade ---", true);
                    cmbTipoAtividade.SelectedIndex = 1;
                    cmbTipoAtividade_SelectedIndexChanged(cmbTipoAtividade, null);
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
        private void loadGrupo(string cmpcotipoatividade, string cmptpgrupoatividade)
        {
            try
            {
                using (DataTable table = tblGrupoAtividade.get(Global.GetConnection(), cmpcotipoatividade, cmptpgrupoatividade))
                {
                    Objetos.LoadCombo(cmbGrupoAtividade, table, "cmpDcGrupoAtividade", "cmpCoGrupoAtividade", "cmpDcGrupoAtividade", "--- Selecione um grupo de atividade ---", true);
                    cmbGrupoAtividade_SelectedIndexChanged(cmbGrupoAtividade, EventArgs.Empty);
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

        private bool MontaPopUp()
        {
            bool Ret = true;
            string nomepdf;
            pdfDocument myDoc;

            using (DataTable table = tblPreventiva.RetornaPlanoManutencao(Global.GetConnection(), cmbObra.SelectedValue))
            {
                if (table != null && table.Rows.Count > 0)
                {
                    filename = "PlanoManutencao" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf";
                    nomepdf = Server.MapPath("~/Relatorios/" + filename);

                    myDoc = ImprimirPlanoManutencao2.ImprimePlano(table, cmbObra.SelectedItem.ToString());
                    myDoc.createPDF(nomepdf);

                    btnImprimir.Visible = true;

                    btnImprimir.Attributes.Add("onclick", "window.open('" + Global.UrlRelatorio + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');");
                }
                else
                {
                    Ret = false;
                    btnImprimir.Visible = false;
                }
            }
            return Ret;
        }

        #endregion

        #region Events
        /// <summary>
        /// Evento de clique do combobox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                limpa();
                ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista = cmbObra.SelectedValue;
                MontaPopUp();
                cmbPeriodicidade_SelectedIndexChanged(sender, e);
                LoadEquipamentosObra();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Evento de clique do combobox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void cmbPeriodicidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               limpa();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Evento de clique do combobox.
        /// Selciona um tipo de atividade.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbTipoAtividade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                limpa();
                this.loadGrupo(((ListItem)cmbTipoAtividade.SelectedItem).Value, ((ListItem)rdbType.SelectedItem).Value);
                cmbGrupoAtividade.Focus();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Evento de clique do combobox.
        /// Carrega os itens do grupo de atividade.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbGrupoAtividade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                limpa();
                if (cmbGrupoAtividade.SelectedIndex != 0)
                {
                    this.loadObraAtividades();
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Evento de clique do botão.
        /// Adiciona uma atividade da obra.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddAtividade_Click(object sender, EventArgs e)
        {
            tblPreventivaAtividade table = new tblPreventivaAtividade();
            try
            {
                table.cmpCoPreventivaAtividade  = "0";
                table.cmpCoObraGrupoLista       = ((ListItem)cmbObra.SelectedItem).Value;
                table.cmpCoPreventiva           = ViewState["cmpCoPreventiva"] == null ? "0" : ViewState["cmpCoPreventiva"].ToString();
                table.cmpCoPeriodicidade        = ((ListItem)cmbPeriodicidade.SelectedItem).Value;
                table.cmpCoTipoAtividade        = ((ListItem)cmbTipoAtividade.SelectedItem).Value;
                table.cmpCoItemAtividade        = ((ListItem)lstItemAtividade.SelectedItem).Value;
                table.cmpTpPreventiva           = ((ListItem)rdbType.SelectedItem).Value;
                table.cmpDcItemAtividadePreventiva = ((ListItem)lstItemAtividade.SelectedItem).Text.TrimEnd();
                table.cmpNoUsuario                 = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();
                if (!(ViewState["cmpCoPreventiva"] = table.Save(Global.GetConnection())).Equals("0"))
                {
                    this.loadObraAtividades();
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Evento de clique do botão.
        /// Remove uma atividade da obra.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRemoveAtividade_Click(object sender, EventArgs e)
        {
            try
            {
                if (tblPreventivaAtividade.Delete(Global.GetConnection(), ((ListItem)lstAtividadeObra.SelectedItem).Value, ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario))
                    limpa();
                    this.loadObraAtividades();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Evento de clique do botão.
        /// Carrega os dados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lstAtividadeObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoPreventivaAtividade";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((ListItem)lstAtividadeObra.SelectedItem).Value;
                ls.Add(lc);

                using (DataTable table = tblPreventiva.GetAll(Global.GetConnection(), ls))
                {
                    ViewState["cmpCoPreventiva"] = table.Rows[0]["cmpCoPreventiva"].ToString();
                    txtDescriptionItemObra.Text = table.Rows[0]["cmpDcItemAtividadePreventiva"].ToString();
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        protected void lstItemAtividade_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDescriptionItemRef.Text = lstItemAtividade.SelectedItem.Text;
        }

        /// <summary>
        /// Evento de clique do radiobuttonlist.
        /// Altera o tipo da preventiva.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rdbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                limpa();
                loadTipoAtividade();
                loadGrupo(((ListItem)cmbTipoAtividade.SelectedItem).Value, ((ListItem)rdbType.SelectedItem).Value);
                cmbGrupoAtividade.Focus();
                LoadEquipamentosObra();
                if (rdbType.SelectedItem.Text == "Equipamento")
                {
                    btnAddEquipamento.Enabled = true;
                }
                else
                {
                    btnAddEquipamento.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }
        private void LoadEquipamentosObra()
        {


           
        }
        protected void btnGravar_Click(object sender, EventArgs e)
        {
            tblPreventivaAtividade table = new tblPreventivaAtividade();
            try
            {
                table.cmpCoPreventivaAtividade      = ((ListItem)lstAtividadeObra.SelectedItem).Value;
                table.cmpDcItemAtividadePreventiva  =  txtDescriptionItemObra.Text;
                table.cmpNoUsuario                  = ((HzLogin)Session["login"]).cmpNoUsuario;
                
                if (table.UpDate(Global.GetConnection()))
                    this.loadObraAtividades();
                if (ViewState["IdEquipamentoObra"] != null)
                {
                    tblPreventivaEquipamentocs PrevEquip = new tblPreventivaEquipamentocs();
                    //((ListItem)cmbGrupoAtividade.SelectedItem).Value;
                    int grupoAtividade=int.Parse(cmbGrupoAtividade.SelectedItem.Value.ToString());
                    PrevEquip.IncluirPreventivaEquipamentos(Global.GetConnection(), int.Parse(ViewState["cmpCoPreventiva"].ToString()), int.Parse(ViewState["IdEquipamentoObra"].ToString()), grupoAtividade);
                }

            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }

        }

        /// <summary>
        /// Evento ao carregrar a página.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    ViewState.Add("IdEquipamentoObra",null);
                    this.load();
                    this.loadObra();
                    limpa();
                    MontaPopUp();

                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddEquipamento_Click(object sender, EventArgs e)
        {
            dgvEquipamento.DataSource = PesquisaEquipamento();
                dgvEquipamento.DataBind();
                this.ModalPopupExtender1.Show();
        }
        private DataTable PesquisaEquipamento()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpCoObraGrupoLista";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = cmbObra.Text;
            ls.Add(lc);

            DataTable dtEquipamentoObra = tblEquipamentoObra.RetornarEquipamentoObra(Global.GetConnection(), ls);
            return dtEquipamentoObra;
        }
        protected void dgvEquipamento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dgvEquipamento_PageIndexChanged(object sender, EventArgs e)
        {
            //int IdEquipamentoObra;
            //IdEquipamentoObra = int.Parse(dgvEquipamento.Rows[dgvEquipamento.SelectedIndex].Cells[3].Text);
        }

        protected void btnCancelarEquipamento_Click(object sender, EventArgs e)
        {
            this.ModalPopupExtender1.Hide();
        }

        protected void dgvEquipamento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvEquipamento.PageIndex = e.NewPageIndex;
            dgvEquipamento.DataSource = PesquisaEquipamento();
            dgvEquipamento.DataBind();
            this.ModalPopupExtender1.Show();
        }

        protected void dgvEquipamento_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Select")
            {
                int linha = int.Parse(e.CommandArgument.ToString());
                IdEquipamentoObra = int.Parse(dgvEquipamento.DataKeys[linha].Value.ToString());
                //txtEquipamento.Text = dgvEquipamento.Rows[linha].Cells[1].Text + " | " + dgvEquipamento.Rows[linha].Cells[2].Text;
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpIdEquipamentoObra";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = IdEquipamentoObra.ToString();
                ls.Add(lc);

                DataTable dtEquipObra = tblEquipamentoObra.Get(Global.GetConnection(), ls);
                if (dtEquipObra.Rows.Count > 0)
                {
                    txtEquipamento.Text = dtEquipObra.Rows[0]["cmpDcEquipamentoObra"].ToString() + " | " + dtEquipObra.Rows[0]["cmpDcLocalEquipamento"].ToString();
                }
                ViewState["IdEquipamentoObra"] = IdEquipamentoObra;
            }
        }

        //protected void btnImprimir_Click(object sender, EventArgs e)
        //{
        //    if (!MontaPopUp())
        //        CaixaMensagem.Mostar(Mensagem.Tipo.Erro, "Obra sem cadastro de manutenção preventiva!");
        //    else
        //    {
        //        //Redirect("frmErro.aspx?id=" + ConfigurationManager.AppSettings["RelPdf"] + filename, "_blank", "");

        //        //Redirect(filename);

        //        //Response.Clear();
        //        //Response.ContentType = "application/pdf";
        //        //Response.AddHeader("Content-disposition", "filename=" + ConfigurationManager.AppSettings["RelPdf"] + filename);
        //        //Response.TransmitFile(ConfigurationManager.AppSettings["RelPdf"] + filename);
        //        //Response.End();
        //        //Response.Flush();
        //        //Response.Clear();

        //        //Response.Clear();
        //        //Response.ContentType = "application/pdf";
        //        //Response.AddHeader("Content-disposition", "filename="  + ConfigurationManager.AppSettings["WordDoc"] + filename);
        //        //Response.WriteFile(ConfigurationManager.AppSettings["RelPdf"] + filename);
        //        //Response.Flush();
        //        //Response.Close();
        //        //Response.End();
        //    }
        //}

        #endregion


    }
}