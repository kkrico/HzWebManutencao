using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Globalization;
using System.Configuration;
using System.Web.UI.HtmlControls;

using HzLibCorporativo.Funcional;
using HzLibCorporativo.Faturamento;
using HzLibCorporativo.Geral;
using HzLibCorporativo.Config;
using HzClasses.Numeracao;
using HzClasses.Tabelas.Apoio;

using HzLibConnection.Sql;
using HzlibWEB;
using HzLibManutencao;
using Apresentacao.Controles;
using HzWebManutencao.Classes;
using Microsoft.VisualStudio.Tools.Applications;

namespace HzWebManutencao.Faturamento
{
    public partial class webFAT_FaturamentoObraDetalhe : System.Web.UI.Page
    {
        #region Função
        /// <summary>
        /// Validar campos do faturamento
        /// </summary>
        private bool ValidarCampos()
        {
            bool ret = true;
            string msg = "";
            if (txtNomeOrgao.Text == "")
                msg += "Nome do orgão em branco! <br />";
            if (txtDataInicial.Text == "")
                msg += "Data inicial do faturamento em branco! <br />";
            if (txtDataFinal.Text == "")
                msg += "Data final do faturamento em branco! <br />";
            if (txtNuNotaFiscal.Text == "")
                msg += "Número da nota fiscal em branco! <br />";
            if (txtEmissaoNota.Text == "")
                msg += "Data da emissão da nota fiscal em branco! <br />";
            if (txtValorNota.Text == "")
                msg += "Valor da nota fiscal em branco! <br />";
            //if (cmbTipoServico.SelectedIndex == 0)
            //    msg += "Tipo de serviço em branco! <br />";
            if (txtEmailEng.Text == "")
                msg += "Endereço de email engenheiro em branco! <br />";

            if (msg != "")
            {
                MsgBox.ShowMessage(msg, "Erro");
                ret = false;
            }

            return ret;
        }

        private bool Gravar()
        {
            bool ret = false;

            if (ValidarCampos())
            {
                tblFaturaObra table = new tblFaturaObra();
                table.cmpIdFaturaObra = ViewState["idFaturaObra"].ToString();
                table.cmpNoDestinoCarta = txtNomeDestinatario.Text;
                table.cmpNoOrgaoDestinoCarta = txtNomeOrgao.Text;
                table.cmpDtPeriodoInicialServico = txtDataInicial.Text + " 00:00";
                table.cmpDtPeriodoFinalServico = txtDataFinal.Text + " 00:00";
                table.cmpNuNotaFiscal = txtNuNotaFiscal.Text;
                table.cmpDtEmissaoNotaFiscal = txtEmissaoNota.Text + " 00:00";
                table.cmpVlNota = txtValorNota.Text.Replace(",", ".");
                table.cmpEdEmailEng = txtEmailEng.Text;
                table.cmpEdEmailAuxAdm = txtEmailAux.Text;
                table.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();

                ret = table.Save(Global.GetConnection());
            }

            return ret;
        }

        /// <summary>
        /// Carrega campos da página.
        /// </summary>
        private void loadTipoNumeracao()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpDcTipoNumeracao";
            lc.TipoCampo = TipoCampo.String;
            lc.ValorCampo = "CARTA";
            ls.Add(lc);

            DataTable table = tblTipoNumeracao.Get(Global.GetConnection(), ls);
            ViewState["cmpCoTipoNumeracao"] = table.Rows[0]["cmpCoTipoNumeracao"].ToString();
        }

        /// <summary>
        /// Carrega campos da página.
        /// </summary>
        //private void loadTipoServico()
        //{
        //    Objetos.LoadCombo(cmbTipoServico, tblTipoServicoObra.Get(Global.GetConnection()), "cmpDcTipoServico", "cmpIdTipoServico", "cmpDcTipoServico", "--- Selecione um Tipo de Serviço ---", true);
        //}

        /// <summary>
        /// Carrega campos da página.
        /// </summary>
        private void load()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpidFaturaObra";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = ViewState["idFaturaObra"].ToString();
            ls.Add(lc);

            using (DataTable table = tblFaturaObra.Get(Global.GetConnection(), ls))
            {
                lblNomeObra.Text            = table.Rows[0]["cmpNoObraFatura"].ToString();
                txtNomeDestinatario.Text    = table.Rows[0]["cmpNoDestinoCarta"].ToString();
                txtNomeOrgao.Text           = table.Rows[0]["cmpNoOrgaoDestinoCarta"].ToString();
                txtDataInicial.Text         = table.Rows[0]["cmpDtPeriodoInicialServico"].ToString();
                txtDataFinal.Text           = table.Rows[0]["cmpDtPeriodoFinalServico"].ToString();
                txtNuNotaFiscal.Text        = table.Rows[0]["cmpNuNotaFiscal"].ToString();
                txtEmissaoNota.Text         = table.Rows[0]["cmpDtEmissaoNotaFiscal"].ToString();
                txtValorNota.Text           = table.Rows[0]["cmpVlNota"].ToString();
                txtDtCartaOrion.Text        = table.Rows[0]["cmpDtEmissaoCartaORION"].ToString();
                txtNumeroDoc.Text           = table.Rows[0]["cmpNuCartaOrion"].ToString();
                txtEmailEng.Text            = table.Rows[0]["cmpEdEmailEng"].ToString();
                txtEmailAux.Text            = table.Rows[0]["cmpEdEmailAuxAdm"].ToString();
                txtNomeServico.Text         = table.Rows[0]["cmpDcTipoServico"].ToString();

                //cmbTipoServico.SelectedIndex = (string.IsNullOrEmpty(table.Rows[0]["cmpIdTipoServico"].ToString())) ? 0 : Objetos.RetornaIndiceCombo(cmbTipoServico, long.Parse(table.Rows[0]["cmpIdTipoServico"].ToString()));

                ViewState["cmpIdObraFatura"] = table.Rows[0]["cmpIdObraFatura"].ToString();
                ViewState["cmpNuMesFatura"] = table.Rows[0]["cmpNuMesFatura"].ToString();
                ViewState["cmpNuAnoFatura"] = table.Rows[0]["cmpNuAnoFatura"].ToString();
                ViewState["cmpNuContrato"] = table.Rows[0]["cmpNuContrato"].ToString();

                txtDtCartaOrion.Enabled = false;
                txtNumeroDoc.Enabled = false;
            }

            if (txtEmailEng.Text == "")
            {
                ls.Clear();

                lc = new ListCampo();
                lc.NomeCampo    = "cmpIdObraFatura";
                lc.TipoCampo    = TipoCampo.Numero;
                lc.ValorCampo   = ViewState["cmpIdObraFatura"].ToString();
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo    = "cmpNuNivelEmail";
                lc.TipoCampo    = TipoCampo.StringIN;
                lc.Sinal        = SinalPesquisa.IN;
                lc.ValorCampo   = "2,4";
                ls.Add(lc);

                DataTable tbl = tblFaturaEmailObraFatura.Get(Global.GetConnection(), ls);
                if (tbl != null && tbl.Rows.Count > 0)
                    txtEmailEng.Text = tbl.Rows[0]["cmpDcEmail"].ToString();
            }

            if (txtEmailAux.Text == "")
            {
                ls.Clear();

                lc = new ListCampo();
                lc.NomeCampo    = "cmpIdObraFatura";
                lc.TipoCampo    = TipoCampo.Numero;
                lc.ValorCampo   = ViewState["cmpIdObraFatura"].ToString();
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo    = "cmpNuNivelEmail";
                lc.TipoCampo    = TipoCampo.Numero;
                lc.Sinal        = SinalPesquisa.Igual;
                lc.ValorCampo   = "6";
                ls.Add(lc);

                DataTable tbl = tblFaturaEmailObraFatura.Get(Global.GetConnection(), ls);
                if (tbl != null && tbl.Rows.Count > 0)
                    txtEmailAux.Text = tbl.Rows[0]["cmpDcEmail"].ToString();
            }

            //if (!string.IsNullOrEmpty(txtNumeroDoc.Text))
            //{
            //    btnCartaOrion.Text = "Visualizar Carta Orion";
            //    btnGravar.Enabled = false;

            //}

        }

        /// <summary>
        ///  Retorna materiais de referência (tblMaterial)
        ///  que não estão cadastrados na obra (tblObraGrupoMaterial).
        /// </summary>
        private bool CarregaDocumentoNotObra()
        {
            try
            {
                using (DataTable table = tblFaturaObraDoc.RetornaDocumentoNotObra(Global.GetConnection(), ViewState["idFaturaObra"].ToString()))
                {
                    if (table != null && table.Rows.Count > 0)
                    {
                        Objetos.LoadCombo(lstDocumentos, table, "cmpDcFaturaDoc", "cmpIdFaturaDoc", "cmpDcFaturaDoc", true);
                        lstDocumentos.SelectedIndex = 0;
                        return true;
                    }
                    else
                    {
                        lstDocumentos.Items.Clear();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(ex.ToString(), "Erro");
                return false;
            }
        }

        /// <summary>
        ///  Retorna materiais de referência (tblMaterial)
        ///  que não estão cadastrados na obra (tblObraGrupoMaterial).
        /// </summary>
        private bool CarregaDocumentoObra()
        {
            try
            {
                using (DataTable table = tblFaturaObraDoc.RetornaDocumentoObra(Global.GetConnection(), ViewState["idFaturaObra"].ToString()))
                {
                    if (table != null && table.Rows.Count > 0)
                    {
                        Objetos.LoadCombo(lstDocAnexo, table, "cmpDcFaturaDoc", "cmpIdFaturaDoc", "cmpDcFaturaDoc", true);
                        lstDocAnexo.SelectedIndex = 0;
                        return true;
                    }
                    else
                    {
                        lstDocAnexo.Items.Clear();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(ex.ToString(), "Erro");
                return false;
            }
        }

        /// <summary>
        /// Evento ao vincular material de referência (tblMaterial) com a Obra (tblObraGrupoMaterial).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnVincular_Click(object sender, EventArgs e)
        {
            bool ok = false;
            tblFaturaObraDoc table = new tblFaturaObraDoc();
            try
            {
                foreach (ListItem item in lstDocumentos.Items)
                {
                    if (item.Selected)
                    {
                        table.cmpIdFaturaObra = ViewState["idFaturaObra"].ToString();
                        table.cmpIdFaturaDoc = item.Value;
                        table.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;

                        if (!(ok = table.GravarDocFaturaObra(Global.GetConnection())))
                            throw new Exception("Erro ao cadastrar o(s) documento(s) da obra!");
                    }
                }

                CarregaDocumentoNotObra();
                CarregaDocumentoObra();
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(ex.ToString(), "Erro");
            }
        }

        /// <summary>
        /// Evento ao desvincular documento da Obra.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDesvincular_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstDocAnexo.Items.Count != 0)
                {
                    tblFaturaObraDoc table = new tblFaturaObraDoc();

                    foreach (ListItem item in lstDocAnexo.Items)
                    {
                        if (item.Selected)
                        {
                            table.cmpIdFaturaObra = ViewState["idFaturaObra"].ToString();
                            table.cmpIdFaturaDoc = item.Value.ToString();

                            table.ExcluirDocFaturaObra(Global.GetConnection());
                        }
                    }

                    CarregaDocumentoNotObra();
                    if (lstDocAnexo.Items.Count == 1)
                        lstDocAnexo.Items.Clear();
                    CarregaDocumentoObra();
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(ex.ToString(), "Erro");
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
                    ViewState["idFaturaObra"] = Request.QueryString["idFaturaObra"];
                    loadTipoNumeracao();
                    //loadTipoServico();
                    load();
                    CarregaDocumentoNotObra();
                    CarregaDocumentoObra();
                }
            }
        }

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            if (Gravar())
                MsgBox.ShowMessage("Dados gravado com sucesso!", "Aviso");
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Faturamento/webFAT_FaturamentoMes.aspx?NuMesFatura=" + ViewState["cmpNuMesFatura"].ToString() + "&NuAnoFatura=" + ViewState["cmpNuAnoFatura"].ToString(), false);
        }

        protected void btnCartaOrion_Click(object sender, EventArgs e)
        {
            if (Gravar())
            {
                load();
                DateTime dated;
                dated = DateTime.Now;
                CCWordApp DocWord = new CCWordApp();

                try
                {
                    CultureInfo culture = new CultureInfo("pt-BR");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;

                    int dia = DateTime.Now.Day;
                    int ano = DateTime.Now.Year;
                    string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month));
                    string diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(DateTime.Now.DayOfWeek));
                    string data = diasemana + ", " + dia + " de " + mes + " de " + ano;

                    CNumeroPorExtenso ValorExtenso = new CNumeroPorExtenso();
                    ValorExtenso.SetNumero(Convert.ToDecimal(txtValorNota.Text.Replace(".", ",")));

                    #region Numerar documento
                    string cmpconumeracaodocumento = "";
                    tblNumeracaoDocumento tbl = new tblNumeracaoDocumento();
                    if (string.IsNullOrEmpty(txtNumeroDoc.Text))
                    {
                        tbl.cmpCoNumeracaoDocumento = "0";
                        tbl.cmpCoFuncionario        = "47"; //Departamento Faturamento
                        tbl.cmpCoObra               = "23"; //Escritório GrupoOrion
                        tbl.cmpIdObraFatura         = ViewState["cmpIdObraFatura"].ToString();
                        tbl.cmpCoTipoNumeracao      = ViewState["cmpCoTipoNumeracao"].ToString();
                        tbl.cmpTxObservacoes        = "Carta de entrega de NF " + txtNuNotaFiscal.Text.ToString() + " R$ " + txtValorNota.Text.ToString() +
                                                        " referente a " + ViewState["cmpNuMesFatura"].ToString() + "/" + ViewState["cmpNuAnoFatura"].ToString() +
                                                        " " + lblNomeObra.Text.ToString();
                        tbl.cmpDcDocumento          = "";
                        tbl.cmpNoUsuario            = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();
                        txtNumeroDoc.Text           = tbl.Save(Global.GetConnection(), ref cmpconumeracaodocumento);
                    }
                    #endregion

                    DocWord.Open(ConfigurationManager.AppSettings["WordMod"] + "Modelo de Carta Orion.dot");

                    DocWord.GotoBookMark("Numero_Doc");
                    DocWord.SetAlignment("Right");
                    DocWord.InsertText(txtNumeroDoc.Text.ToString());
                    DocWord.SetFont("nothing");

                    DocWord.GotoBookMark("Data_Carta");
                    DocWord.SetAlignment("Right");
                    DocWord.InsertText(data);
                    DocWord.SetFont("nothing");

                    string NomeDestinatario = "";
                    if (!string.IsNullOrEmpty(txtNomeDestinatario.Text))
                        NomeDestinatario = "Ilmo(a). Sr(a). " + txtNomeDestinatario.Text;

                    DocWord.GotoBookMark("Nome_Destino");
                    DocWord.SetFont("Bold");
                    DocWord.InsertText(NomeDestinatario);
                    DocWord.SetFont("nothing");

                    DocWord.GotoBookMark("Nome_Orgao");
                    DocWord.InsertText(txtNomeOrgao.Text);
                    DocWord.SetFont("nothing");

                    DocWord.GotoBookMark("Data_Inicial");
                    DocWord.InsertText(txtDataInicial.Text);

                    DocWord.GotoBookMark("Data_Final");
                    DocWord.InsertText(txtDataFinal.Text);

                    DocWord.GotoBookMark("Numero_Contrato");
                    DocWord.InsertText(ViewState["cmpNuContrato"].ToString().TrimEnd());

                    DocWord.GotoBookMark("Data_Emissao_Nota");
                    DocWord.InsertText(txtEmissaoNota.Text.ToString());

                    DocWord.GotoBookMark("Tipo_Servico");
                    DocWord.InsertText(txtNomeServico.Text.ToString());

                    DocWord.GotoBookMark("Numero_Nota");
                    DocWord.InsertText(txtNuNotaFiscal.Text);

                    DocWord.GotoBookMark("Valor_Nota");
                    string ValorNota = txtValorNota.Text.Replace(".", "");
                    DocWord.InsertText(Convert.ToDecimal(ValorNota).ToString("#,##0.00"));

                    DocWord.GotoBookMark("Valor_NotaExtenso");
                    DocWord.InsertText(ValorExtenso.ToString());

                    ArrayList list = new ArrayList();
                    list.Add("Anexo1");
                    list.Add("Anexo2");
                    list.Add("Anexo3");
                    list.Add("Anexo4");
                    list.Add("Anexo5");
                    list.Add("Anexo6");
                    list.Add("Anexo7");
                    list.Add("Anexo8");
                    list.Add("Anexo9");
                    list.Add("Anexo10");

                    int i = 0;
                    foreach (ListItem item in lstDocAnexo.Items)
                    {
                        DocWord.GotoBookMark(list[i].ToString());
                        DocWord.InsertText("-" + item.Text);
                        i++;
                    }

                    DocWord.InsertLineBreak();

                    DocWord.SaveAs(ConfigurationManager.AppSettings["WordDoc"] + "CO" + txtNumeroDoc.Text.Replace("/", "-") + " " + lblNomeObra.Text.ToString() + ".doc");
                    DocWord.Quit();

                    #region Grava nome do arquivo na tabela de documentos
                    if (string.IsNullOrEmpty(txtNumeroDoc.Text))
                    {
                        tbl.cmpCoNumeracaoDocumento = cmpconumeracaodocumento;
                        tbl.cmpDcDocumento = ConfigurationManager.AppSettings["WordDoc"].ToString() + " CO" + txtNumeroDoc.Text.Replace("/", "-") + " " + lblNomeObra.Text.ToString() + ".doc";
                        tbl.Save(Global.GetConnection(), ref cmpconumeracaodocumento);
                    }
                    #endregion

                    #region gravar data e número da carta orion
                    tblFaturaObra table = new tblFaturaObra();
                    table.cmpIdFaturaObra = ViewState["idFaturaObra"].ToString();
                    table.cmpEdCartaoOrion = ConfigurationManager.AppSettings["WordDoc"].ToString() + " CO" + txtNumeroDoc.Text.Replace("/", "-") + " " + lblNomeObra.Text.ToString() + ".doc";
                    table.cmpNuCartaOrion = txtNumeroDoc.Text.ToString();
                    table.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();
                    table.GravarCartaOrionFatura(Global.GetConnection());
                    load();
                    #endregion

                    Response.Clear();
                    Response.ContentType = "application/msword";
                    Response.AddHeader("Content-disposition", "filename=" + ConfigurationManager.AppSettings["WordDoc"] + "CO" + txtNumeroDoc.Text.Replace("/", "-") + " " + lblNomeObra.Text.ToString() + ".doc");
                    Response.WriteFile(ConfigurationManager.AppSettings["WordDoc"] + "CO" + txtNumeroDoc.Text.Replace("/", "-") + " " + lblNomeObra.Text.ToString() + ".doc");
                    Response.End();

                    //Response.Write("<script language='javascript'>"
                    //            + "window.open('" + Global.UrlDocWord + "CO" + txtNumeroDoc.Text.Replace("/", "-") + " " + lblNomeObra.Text.ToString() + ".doc" + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105')"
                    //            + "</script>");

                }
                catch (Exception ex)
                {
                    DocWord.Quit();
                    MsgBox.ShowMessage(ex.ToString(), "Erro");
                }
            }
        }


        protected void btnEnviarEmail_Click(object sender, EventArgs e)
        {
            if (Gravar())
            {
                tblFaturaObra.EnviarEmailFaturamentoEmissaoNF(Global.GetConnection(), ViewState["idFaturaObra"].ToString());
                MsgBox.ShowMessage("Email enviado com sucesso!", "Aviso");
            }
        }
        #endregion


        //FileInfo myDoc = new FileInfo(ConfigurationManager.AppSettings["WordDoc"] + "CO" + numero.Replace("/", "-") + " " + lblNomeObra.Text.ToString() + ".dotx");
        //Response.Clear();
        //Response.ContentType = "Application/msword";
        //Response.AddHeader("content-disposition", "attachment;filename=" + myDoc.Name);
        //Response.AddHeader("Content-Length", myDoc.Length.ToString());
        //Response.ContentType = "application/octet-stream";
        //Response.WriteFile(myDoc.FullName);
        //Response.End();


        //DocWord.WordDocViewer(ConfigurationManager.AppSettings["WordDoc"] + "CO" + numero.Replace("/", "-") + " " + lblNomeObra.Text.ToString() + ".html");

        //Response.Clear();
        //Response.ContentType = "application/ms-word";
        //Response.AddHeader("Content-disposition", "inline; filename=" + "CO" + numero.Replace("/", "-") + " " + lblNomeObra.Text.ToString());

        ////String path = Server.MapPath("abc.doc");
        //Response.WriteFile(ConfigurationManager.AppSettings["WordDoc"] + "CO" + numero.Replace("/", "-") + " " + lblNomeObra.Text.ToString());

        //Response.End();
    }
}