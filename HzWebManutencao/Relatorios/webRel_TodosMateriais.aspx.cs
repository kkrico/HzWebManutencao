using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using sharpPDF;
using sharpPDF.Enumerators;
using sharpPDF.Tables;
using sharpPDF.Elements;

using HzlibWEB;
using HzLibConnection.Sql;
using HzLibManutencao;
using HzLibCorporativo.Funcional;
using HzLibGeneral.Util;
using HzWebManutencao.Classes.Relatorios;
using Apresentacao.Controles;

namespace HzWebManutencao.Relatorios
{
    public partial class webRel_TodosMateriais : System.Web.UI.Page
    {
  

        #region Funções
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

        /// <summary>
        /// Retorna materiais da obra (tblObraGrupoMaterial).
        /// </summary>
        private DataTable RetornaMaterialObra()
        {
            return tblObraGrupoMaterial.RetornarMaterialGrupoObra(Global.GetConnection(), ViewState["cmpCoObraGrupo"].ToString());
        }

        /// <summary>
        /// Retorna materiais da obra (tblObraGrupoMaterial).
        /// </summary>
        private DataTable RetornaMaterialPeriodo()
        {
            txtDataInicial.Text = txtDataInicial.Text + " 00:00:00";
            txtDataFinal.Text = txtDataFinal.Text + " 23:59:59";

            return tblObraGrupoMaterial.RetornarMaterialObraPeriodo(Global.GetConnection(), ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista, txtDataInicial.Text, txtDataFinal.Text, rdbData.SelectedValue.ToString());
        }

        /// <summary>
        /// Retorna materiais da obra (tblObraGrupoMaterial).
        /// </summary>
        private DataTable RetornaMaterialGrupoObraPeriodo()
        {
            txtDataInicial.Text = txtDataInicial.Text + " 00:00:00";
            txtDataFinal.Text = txtDataFinal.Text + " 23:59:59";

            return tblObraGrupoMaterial.RetornarMaterialGrupoObraPeriodo(Global.GetConnection(), ViewState["cmpCoObraGrupo"].ToString(), txtDataInicial.Text, txtDataFinal.Text, rdbData.SelectedValue.ToString());
        }

        private void ControleExibicao(bool periodo)
        {
            txtDataInicial.Visible = periodo;
            txtDataFinal.Visible = periodo;
            lblPeriodo.Visible = periodo;
            lblDtOs.Visible = periodo;
            rdbData.Visible = periodo;
            Label1.Visible = periodo;
            btnImprimir.Visible = false;
        }

        private bool ValidaPeriodo()
        {
            bool Ret = true;

            if (txtDataInicial.Text == "")
            {
                Ret = false;
                txtDataInicial.Focus();
                CaixaMensagem.Mostar(Mensagem.Tipo.Erro, "Informe a data inicial!");
            }
            else if (txtDataFinal.Text == "")
            {
                Ret = false;
                txtDataFinal.Focus();
                CaixaMensagem.Mostar(Mensagem.Tipo.Erro, "Informe a data final!");
            }
            return Ret;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                loadObra();
                btnImprimir.Visible = false;
                ControleExibicao(false);
            }
        }

        protected void btnGerarRel_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);

            string filename;
            string nomepdf;

            DataTable table = new DataTable();

            if (ViewState["tipoMaterial"] !=null && ViewState["tipoMaterial"].ToString() == "LGMT")
            {
                table = RetornaMaterialObra();
                if (table != null && table.Rows.Count > 0)
                {
                    ImprimirMaterialObra Imprimir = new ImprimirMaterialObra();
                    pdfDocument myDoc = new pdfDocument("Horizon", "Orion");
                    filename = "MaterialObra" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf";
                    nomepdf = Server.MapPath("~/Relatorios/" + filename);

                    Imprimir.myDoc = myDoc;
                    Imprimir.myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);
                    Imprimir.NomeObra = cmbObra.SelectedItem.ToString();
                    Imprimir.EnderecoLogoOrion = Server.MapPath("~/Imagens/logo_Orion.jpg");
                    if (ViewState["cmpInLogoObra"].ToString() == "True")
                        Imprimir.EnderecoLogoObra = Server.MapPath("~/Imagens/logo_IPEN.bmp");
                    else
                        Imprimir.EnderecoLogoObra = "";

                    Imprimir.ImprimeMaterial(table);

                    myDoc.createPDF(nomepdf);

                    btnImprimir.Visible = true;
                    btnImprimir.Attributes.Add("onclick", "window.open('" + Global.UrlRelatorio + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');");
                }
                else
                    CaixaMensagem.Mostar(Mensagem.Tipo.Aviso, "Não há consumo de material para o período!");
            }
            else if (ViewState["tipoMaterial"] != null && ViewState["tipoMaterial"].ToString() == "LMUO")
            {
                if (ValidaPeriodo())
                {
                    table = RetornaMaterialPeriodo();
                    if (table != null && table.Rows.Count > 0)
                    {
                        ImprimirMaterialObraPeriodo Imprimir = new ImprimirMaterialObraPeriodo();
                        pdfDocument myDoc = new pdfDocument("Horizon", "Orion");
                        filename = "MaterialObra" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf";
                        nomepdf = Server.MapPath("~/Relatorios/" + filename);

                        Imprimir.myDoc = myDoc;
                        Imprimir.myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);
                        Imprimir.NomeObra = cmbObra.SelectedItem.ToString();
                        Imprimir.EnderecoLogoOrion = Server.MapPath("~/Imagens/logo_Orion.jpg");
                        if (ViewState["cmpInLogoObra"].ToString() == "True")
                            Imprimir.EnderecoLogoObra = Server.MapPath("~/Imagens/logo_IPEN.bmp");
                        else
                            Imprimir.EnderecoLogoObra = "";

                        Imprimir.DtInicial = txtDataInicial.Text.Substring(0, 10);
                        Imprimir.DtFinal = txtDataFinal.Text.Substring(0, 10);

                        Imprimir.ImprimeMaterial(table);

                        myDoc.createPDF(nomepdf);

                        btnImprimir.Visible = true;
                        btnImprimir.Attributes.Add("onclick", "window.open('" + Global.UrlRelatorio + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');");
                    }
                    else
                        CaixaMensagem.Mostar(Mensagem.Tipo.Aviso, "Não há consumo de material para o período!");
                }
            }
            else if (ViewState["tipoMaterial"] != null && ViewState["tipoMaterial"].ToString() == "LMUA")
            {
                if (ValidaPeriodo())
                {
                    table = RetornaMaterialGrupoObraPeriodo();
                    if (table != null && table.Rows.Count > 0)
                    {
                        ImprimirMaterialGrupoObraPeriodo Imprimir = new ImprimirMaterialGrupoObraPeriodo();
                        pdfDocument myDoc = new pdfDocument("Horizon", "Orion");
                        filename = "MaterialObra" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString() + ".pdf";
                        nomepdf = Server.MapPath("~/Relatorios/" + filename);

                        Imprimir.myDoc = myDoc;
                        Imprimir.myPage = myDoc.addPage(predefinedPageSize.csSharpPDFFormat);
                        Imprimir.NomeObra = cmbObra.SelectedItem.ToString();
                        Imprimir.EnderecoLogoOrion = Server.MapPath("~/Imagens/logo_Orion.jpg");
                        if (ViewState["cmpInLogoObra"].ToString() == "True")
                            Imprimir.EnderecoLogoObra = Server.MapPath("~/Imagens/logo_IPEN.bmp");
                        else
                            Imprimir.EnderecoLogoObra = "";
                        Imprimir.DtInicial = txtDataInicial.Text.Substring(0, 10);
                        Imprimir.DtFinal = txtDataFinal.Text.Substring(0, 10);

                        Imprimir.ImprimeMaterial(table);

                        myDoc.createPDF(nomepdf);

                        btnImprimir.Visible = true;
                        btnImprimir.Attributes.Add("onclick", "window.open('" + Global.UrlRelatorio + filename + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');");
                    }
                    else
                        CaixaMensagem.Mostar(Mensagem.Tipo.Aviso, "Não há consumo de material para o período!");
                }
            }
        }

        /// <summary>
        /// Evento ao selecionar obra.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista = cmbObra.SelectedValue;

                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoObraGrupoLista";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista;
                ls.Add(lc);

                using (DataTable table = tblObraUsuario.Get(Global.GetConnection(), ls))
                {
                    ViewState["cmpCoObraGrupo"] = table.Rows[0]["cmpCoObraGrupo"].ToString();
                    ViewState["cmpInLogoObra"] = table.Rows[0]["cmpInLogoObra"].ToString();
                    btnImprimir.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        protected void rdbMateriais_SelectedIndexChanged(object sender, EventArgs e)
        {
            //rdbMateriais.ClearSelection();
            ViewState["tipoMaterial"] = rdbMateriais.SelectedValue;

            switch (rdbMateriais.SelectedValue)
            {
                case "LGMT": // Lista Geral de Materiais
                    ControleExibicao(false);
                    break;
                case "LMUO": // Lista de Materiais Utilizados por Obra 
                    //ViewState["filename"] = ViewState["co_ObraManto"].ToString() + "&mesinicial=" + cmbDtInicial.SelectedValue + "&anoinicial=" + txtAnoInicial.Text + "&mesfinal=" + cmbDtFinal.SelectedValue + "&anofinal=" + TxtAnoFinal.Text + "&tipo_graficonet=" + rdbHidra.SelectedValue;
                    ControleExibicao(true);
                    break;
                case "LMUA": // Lista de Materiais Utilizados Agrupados por Obra 
                    //ViewState["filename"] = ViewState["co_ObraManto"].ToString() + "&mesinicial=" + cmbDtInicial.SelectedValue + "&anoinicial=" + txtAnoInicial.Text + "&mesfinal=" + cmbDtFinal.SelectedValue + "&anofinal=" + TxtAnoFinal.Text + "&tipo_graficonet=" + rdbHidra.SelectedValue;
                    ControleExibicao(true);
                    break;

            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {

        }
   
    }
}