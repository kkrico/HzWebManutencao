using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibConnection.Sql;
using System.Data;
using HzLibCorporativo.Funcional;
using HzlibWEB;
using HzLibManutencao;
using sharpPDF;
using sharpPDF.Enumerators;
using sharpPDF.Tables;
using HzWebManutencao.Classes.Relatorios;
using RelatoriosReportServer;

namespace HzWebManutencao.ATE
{
    public class STOS
    {
        public string cmpIdOS { get; set; }
        public string state { get; set; }
        public string cmpCoObra { get; set; }

        public STOS(string cmpidos, string state, string cmpcobora)
        {
            this.cmpIdOS = cmpidos;
            this.state = state;
            this.cmpCoObra = cmpcobora;
        }
    }

    public partial class webATE_OSPesquisa : System.Web.UI.Page
    {
        #region Variables
        static string filename;
        #endregion


        #region Functions

        /// <summary>
        /// Carrega as combos.
        /// </summary>
        private void load()
        {
            try
            {
                txtDataInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");

                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoContratante";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoContratante;
                ls.Add(lc);

                using (DataTable table = tblOrigemOS.Get(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbOrigemOS, table, "cmpDcOrigemOS", "cmpCoOrigemOS", "cmpDcOrigemOS", "TODAS", true);
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
        private string loadObraTI()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpNoObra";
            lc.TipoCampo = TipoCampo.String;
            lc.ValorCampo = "SERVIÇOS DE TI";
            ls.Add(lc);

            DataTable table = tblObraGrupoLista.Get(Global.GetConnection(), ls);

            return table.Rows[0]["cmpCoObraGrupoLista"].ToString();
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
        /// Controla o estado dos botões.
        /// </summary>
        private void controlaPerfilUsuario()
        {
            switch (((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNuPerfil.ToString())
            {
                case "3": // Cliente
                    chkServicosTI.Visible = false;
                    break;
            }

        }

        /// <summary>
        /// Pesquisa OS.
        /// </summary>
        private DataTable pesquisaOs()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();

            if (chkGrupoObra.Checked)
            {
                DataTable table = tblObraGrupoLista.RetornaGrupoObra(Global.GetConnection(), cmbObra.SelectedValue.ToString());

                if (table != null && table.Rows.Count > 0)
                {
                    lc.NomeCampo = "cmpCoObraGrupo";
                    lc.TipoCampo = TipoCampo.Numero;
                    lc.Sinal = SinalPesquisa.Igual;
                    lc.ValorCampo = table.Rows[0]["cmpcoobragrupo"].ToString();
                    ls.Add(lc);
                }
            }
            else
            {
                lc.NomeCampo = "cmpCoObraGrupoLista";
                lc.TipoCampo = TipoCampo.String;
                lc.Sinal = SinalPesquisa.Igual;
                lc.ValorCampo = chkServicosTI.Checked == true ? loadObraTI() : cmbObra.SelectedValue;
                ls.Add(lc);
            }

            if (chkServicosTI.Checked == true)
            {
                lc = new ListCampo();
                lc.NomeCampo    = "cmpDcLocal";
                lc.TipoCampo    = TipoCampo.String;
                lc.Sinal        = SinalPesquisa.Igual;
                lc.ValorCampo   = cmbObra.SelectedItem.ToString();
                ls.Add(lc);
            }

            if (txtNumeroDemanda.Text != "")
            {
                lc = new ListCampo();
                lc.NomeCampo    = "cmpNuDemandaCliente";
                lc.TipoCampo    = TipoCampo.String;
                lc.Sinal        = SinalPesquisa.Igual;
                lc.ValorCampo   = txtNumeroDemanda.Text;
                ls.Add(lc);
            }
            else
            {
                lc = new ListCampo();
                lc.NomeCampo    = rdbData.SelectedValue.ToString() == "CL" ? "cmpDtConclusaoAtendimento" : "cmpDtAbertura";
                lc.TipoCampo    = TipoCampo.Data;
                lc.Sinal        = SinalPesquisa.MaiorIgual;
                lc.ValorCampo   = txtDataInicial.Text + " 00:00:00";
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo    = rdbData.SelectedValue.ToString() == "CL" ? "cmpDtConclusaoAtendimento" : "cmpDtAbertura";
                lc.TipoCampo    = TipoCampo.Data;
                lc.Sinal        = SinalPesquisa.MenorIgual;
                lc.ValorCampo   = txtDataFinal.Text + " 23:59:59";
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo    = "cmpStOS";
                lc.TipoCampo    = TipoCampo.String;
                lc.Sinal        = SinalPesquisa.Diferente;
                lc.ValorCampo   = "D";
                ls.Add(lc);

                if (cmbOrigemOS.SelectedValue != "0")
                {
                    lc = new ListCampo();
                    lc.NomeCampo    = "cmpCoOrigemOS";
                    lc.TipoCampo    = TipoCampo.Numero;
                    lc.Sinal        = SinalPesquisa.Igual;
                    lc.ValorCampo   = cmbOrigemOS.SelectedValue;
                    ls.Add(lc);
                }

                if (rdbState.SelectedValue != "T")
                {
                    lc = new ListCampo();
                    lc.NomeCampo    = "cmpStOS";
                    lc.TipoCampo    = TipoCampo.String;
                    lc.Sinal        = SinalPesquisa.Igual;
                    lc.ValorCampo   = rdbState.SelectedValue;
                    ls.Add(lc);
                }
            }

            return tblOS.Get(Global.GetConnection(), ls);
        }

        private void ImprimirTodasOS()
        {
            DataTable table = pesquisaOs();

            if (table != null && table.Rows.Count > 0)
            {
                ImprimirOs Imprimir = new ImprimirOs();
                pdfDocument myDoc   = new pdfDocument("Horizon", "Orion");
                Imprimir.myDoc      = myDoc;
                Imprimir.cmpCoObra  = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista;
                Imprimir.NomeObra   = cmbObra.SelectedItem.ToString();
                Imprimir.EnderecoLogoOrion = Server.MapPath("~/Imagens/logo_Orion.jpg");

                if (ViewState["cmpInLogoObra"].ToString() == "True")
                    Imprimir.EnderecoLogoObra = Server.MapPath("~/Imagens/logo_IPEN.bmp");
                else
                    Imprimir.EnderecoLogoObra = "";

                foreach (DataRow lin in table.Rows)
                {
                    Imprimir.cmpIdOS = lin["cmpIdOs"].ToString();
                    Imprimir.myPage  = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);
                    Imprimir.ImprimeOrdemServico();
                }

                //filename = "pdf" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf";
                string nomepdf = Server.MapPath("~/Relatorios/" + filename);
                myDoc.createPDF(nomepdf);

                Response.Write("<script>window.open('" + Global.UrlRelatorio + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');</script>");

                // Impressão ambiente produção ambiente interno orion
                //Response.Write("<script language='javascript'>"
                //                  + "window.open('" + @"http://172.10.10.2/HzWebManutencao_Desenv/Relatorios/" + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105')"
                //                  + "</script>");

            }
        }

        private void ImprimirResumoOS()
        {
            DataTable table = pesquisaOs();

            if (table != null && table.Rows.Count > 0)
            {
                //filename = "pdf" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf";
                string nomepdf = Server.MapPath("~/Relatorios/" + filename);

                pdfDocument myDoc = HzWebManutencao.ImprimirResumoOS.ImprimeResumoOS(table, txtDataInicial.Text.ToString(), txtDataFinal.Text.ToString());
                myDoc.createPDF(nomepdf);

                Response.Write("<script>window.open('" + Global.UrlRelatorio + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');</script>");

                // Impressão ambiente produção ambiente interno orion
                //Response.Write("<script language='javascript'>"
                //                  + "window.open('" + @"http://172.10.10.2/HzWebManutencao_Desenv/Relatorios/" + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105')"
                //                  + "</script>");

            }
        }

        private void ImprimirFechamentoMesMaterial()
        {
            DataTable table = pesquisaOs();

            if (table != null && table.Rows.Count > 0)
            {
                //filename = "FechaMesMaterial" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf";
                string nomepdf = Server.MapPath("~/Relatorios/" + filename);

                pdfDocument myDoc = FechamentoMes.ImprimeCapaRostoOS(table, txtDataInicial.Text.ToString(), txtDataFinal.Text.ToString());
                myDoc.createPDF(nomepdf);

                Response.Write("<script>window.open('" + Global.UrlRelatorio + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');</script>");

                // Impressão ambiente produção ambiente interno orion
                //Response.Write("<script language='javascript'>"
                //                  + "window.open('" + @"http://172.10.10.2/HzWebManutencao_Desenv/Relatorios/" + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105')"
                //                  + "</script>");
            }
        }

        private void ImprimirFechamentoMes()
        {
            DataTable table = pesquisaOs();

            if (table != null && table.Rows.Count > 0)
            {
                //filename = "FechaMes" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf";
                string nomepdf = Server.MapPath("~/Relatorios/" + filename);

                pdfDocument myDoc = HzWebManutencao.ImprimirFechamentoMes.ImprimeFechamento(table, txtDataInicial.Text.ToString(), txtDataFinal.Text.ToString());
                myDoc.createPDF(nomepdf);

                Response.Write("<script>window.open('" + Global.UrlRelatorio + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');</script>");

                // Impressão ambiente produção ambiente interno orion
                //Response.Write("<script language='javascript'>"
                //                  + "window.open('" + @"http://172.10.10.2/HzWebManutencao_Desenv/Relatorios/" + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105')"
                //                  + "</script>");

            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.load();
                this.loadObra();
                this.controlaPerfilUsuario();
            }
            filename = "pdf" + DateTime.Now.Day.ToString() +
                               DateTime.Now.Month.ToString() +
                               DateTime.Now.Year.ToString() +
                               DateTime.Now.Millisecond.ToString() + ".pdf";
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            DataTable table = pesquisaOs();
            grdOS.DataSource = table;
            grdOS.DataBind();

            if (table != null && table.Rows.Count > 0)
            {
                ViewState["cmpIdOS"] = table.Rows[0]["cmpIdOS"].ToString();
                btnAprove.Visible = rdbState.SelectedValue == "G" ? true : false;
                ViewState["cmpInLogoObra"] = table.Rows[0]["cmpInLogoObra"].ToString() == "True" ? "1" : "0"; 
            }
        }

        protected void grdOS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            TableCellCollection col = e.Row.Cells;
            if (col.Count > 1)
                col[0].Visible = rdbState.SelectedValue == "G";

            //switch (e.Row.RowType)
            //{
            //    case DataControlRowType.DataRow:
            //        e.Row.Cells[8].Attributes.Add("onclick", "window.open('" + Global.UrlRelatorio + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');");
            //        break;
            //}
        }

        protected void grdOS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] ParImpressao = e.CommandArgument.ToString().Split(new char[] { '$' });

            try
            {
                string p;
                switch (e.CommandName.ToLower().Trim())
                {
                    case "lnk":
                            if (cmbObra.SelectedItem.ToString().TrimEnd() == "SERVIÇOS DE TI" || chkServicosTI.Checked == true)
                                p = "webATE_ServicosTI.aspx?id=" + e.CommandArgument.ToString();
                            else
                                p = "webATE_OS.aspx?id=" + e.CommandArgument.ToString();
                            Response.Redirect(p, false);
                            break;

                    case "btn":
                            ImprimirOs Imprimir = new ImprimirOs();
                            pdfDocument myDoc   = new pdfDocument("Horizon", "Orion");
                            Imprimir.myDoc      = myDoc;
                            Imprimir.myPage     = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);
                            Imprimir.cmpCoObra  = ParImpressao[0].ToString();
                            Imprimir.cmpIdOS    = ParImpressao[1].ToString();
                            Imprimir.NomeObra   = cmbObra.SelectedItem.ToString();
                            Imprimir.EnderecoLogoOrion = Server.MapPath("~/Imagens/logo_Orion.jpg");

                            if (ViewState["cmpInLogoObra"].ToString() == "1")
                                Imprimir.EnderecoLogoObra = Server.MapPath("~/Imagens/logo_IPEN.bmp");
                            else
                                Imprimir.EnderecoLogoObra = "";

                            Imprimir.ImprimeOrdemServico();
                            
                            //filename = "pdf" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf";
                            //string nomepdf = Server.MapPath("~/Relatorios/" + filename);

                            myDoc.createPDF(Server.MapPath("~/Relatorios/" + filename));

                            Response.Write("<script>window.open('" + Global.UrlRelatorio + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');</script>");

                            break;
                    case "log":
                            DataTable dtLog = tblLogOS.LogOS(Global.GetConnection(), e.CommandArgument.ToString().Trim());
                            grdLogs.DataSource = dtLog;
                            grdLogs.DataBind();
                            ModalPopupExtender2.Show();

                        break;
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        protected void grdOS_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdOS.PageIndex = e.NewPageIndex;
                grdOS.DataSource = pesquisaOs();
                grdOS.DataBind();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        protected void chkConcluida_CheckedChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        protected void cmbObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista = cmbObra.SelectedValue;
            if (cmbObra.SelectedItem.Text == "SERVIÇOS DE TI")
                chkServicosTI.Enabled = false;
            else
                chkServicosTI.Enabled = true;

            btnPesquisar_Click(sender, e);
        }

        protected void cmbOrigemOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        protected void chkEmAprovacao_CheckedChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        protected void rdbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        protected void cmbHeaderAprovar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedvalue = ((DropDownList)sender).SelectedValue;

            foreach (GridViewRow r in grdOS.Rows)
            {
                DropDownList drop = (DropDownList)r.FindControl("cmbAprovar");
                if (drop != null)
                {
                    drop.SelectedIndex = selectedvalue == "A" ? 1 : 2;

                }

            }
        }

        protected void btnAprove_Click(object sender, EventArgs e)
        {
            List<STOS> lst = new List<STOS>();
            ViewState["reprovar"] = false;
            try
            {
                foreach (GridViewRow r in grdOS.Rows)
                {
                    DropDownList drop = (DropDownList)r.FindControl("cmbAprovar");
                    if (drop != null)
                    {
                        if (drop.SelectedValue == "A")
                            lst.Add(new STOS(((LinkButton)r.FindControl("lnk")).CommandArgument, "A", cmbObra.SelectedValue));
                        else if (drop.SelectedValue == "R")
                        {
                            ViewState["reprovar"] = true;
                            lst.Add(new STOS(((LinkButton)r.FindControl("lnk")).CommandArgument, "R", cmbObra.SelectedValue));
                        }
                    }
                }

                if (lst.Count < 1)
                    throw new Exception("Selecione uma ordem de serviço!");

                List<STOS> l = lst.FindAll(delegate(STOS st) { return st.state == "A"; });
                //aprovar
                //executar procedure de aprovacao
                tblOS tbl = new tblOS();
                tbl.cmpCoObra = cmbObra.SelectedValue;
                tbl.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;
                string str = "";
                foreach (STOS st in l)
                    str += st.cmpIdOS + ",";
                tbl.GravarAprovacaoOS(Global.GetConnection(), str);

                l = lst.FindAll(delegate(STOS st) { return st.state == "R"; });
                //rejeitar
                if ((bool)ViewState["reprovar"])
                {
                    foreach (STOS st in l)
                        str += st.cmpIdOS + ",";


                    Session["STOS"] = lst;
                    Response.Redirect("~/ATE/webATE_OSJustificativa.aspx?id=" + ViewState["cmpIdOS"] + "&Sit=R", false);
                }
                else
                    btnPesquisar_Click(sender, e);
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex.Message);
            }
        }

        protected void cmbRelatorios_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedvalue = ((DropDownList)sender).SelectedValue;

            switch (cmbRelatorios.SelectedValue.ToString())
            {
                case "1":
                    ImprimirTodasOS();
                    break;
                case "2":
                    ImprimirResumoOS();
                    break;
                case "3":
                    ImprimirFechamentoMesMaterial();
                    break;
                case "4":
                    ImprimirFechamentoMes();
                    break;
                case "5":
                    GerarRelatorioMesSemMaterial();
                    break;
            }
        }
        private void GerarRelatorioMesSemMaterial()
        {
            clRelatorios clRel = new clRelatorios();
            string nomeRel= clRel.FechamentoMesSemMaterial(cmbObra.SelectedValue.ToString(), txtDataInicial.Text, txtDataFinal.Text, "D");
            this.Response.Write("<SCRIPT language=javascript>window.open('../" + nomeRel + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');</SCRIPT>");
        }
        protected void rdbData_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        protected void chkServicosTI_CheckedChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        protected void chkGrupoObra_CheckedChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }
  
        #endregion

        protected void btnFecharLog_Click(object sender, EventArgs e)
        {
            ModalPopupExtender2.Hide();
        }
        
    }  
}