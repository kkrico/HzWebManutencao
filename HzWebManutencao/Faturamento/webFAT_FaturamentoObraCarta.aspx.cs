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
using System.Net;

namespace HzWebManutencao.Faturamento
{
    public partial class webFAT_FaturamentoObraCarta : System.Web.UI.Page
    {
        #region Variables
        private decimal ValorTotal = 0;
        private decimal ValorTotalNota = 0;
        private int Rascunho = 0;
        private bool RegerarCarta = false;
        #endregion

        #region Events

        /// <summary>
        /// Carrega dados da Obra.
        /// </summary>
        private void LoadObra()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();

            lc = new ListCampo();
            lc.NomeCampo = "cmpIdFaturaObra";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = ViewState["cmpIdFaturaObra"].ToString();
            ls.Add(lc);

            using (DataTable table = tblFaturaObra.Get(Global.GetConnection(), ls))
            {
                lblNomeObra.Text              = table.Rows[0]["cmpNoObraFatura"].ToString();
                lblTipoServico.Text           = table.Rows[0]["cmpDcTipoServico"].ToString();
                ViewState["cmpIdObraFatura"]  = table.Rows[0]["cmpIdObraFatura"].ToString();
                ViewState["cmpNuMesFatura"]   = table.Rows[0]["cmpNuMesFatura"].ToString();
                ViewState["cmpNuAnoFatura"]   = table.Rows[0]["cmpNuAnoFatura"].ToString();
                ViewState["cmpNuContrato"]    = table.Rows[0]["cmpNuContrato"].ToString();
                ViewState["cmpDcTipoServico"] = table.Rows[0]["cmpDcTipoServico"].ToString();
                ViewState["cmpIdTipoServico"] = table.Rows[0]["cmpIdTipoServico"].ToString();

                ViewState["cmpNoOrgao"]          = table.Rows[0]["cmpNoOrgao"].ToString();
                ViewState["cmpNoPrimeiroGestor"] = table.Rows[0]["cmpNoPrimeiroGestor"].ToString();
                ViewState["cmpNoSegundoGestor"]  = table.Rows[0]["cmpNoSegundoGestor"].ToString();
                ViewState["cmpNoSetorGestor"]    = table.Rows[0]["cmpNoSetorGestor"].ToString();

                CarregaEmail();
            }
        }

        /// <summary>
        /// Exibe cartas da Obra.
        /// </summary>
        private void PesquisaCarta(int linha)
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();

            lc = new ListCampo();
            lc.NomeCampo = "cmpIdFaturaObra";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = ViewState["cmpIdFaturaObra"].ToString();
            ls.Add(lc);

            lc = new ListCampo();
            lc.NomeCampo = "cmpInStatusCarta";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = "1";
            ls.Add(lc);

            DataTable table = tblFaturaCarta.Get(Global.GetConnection(), ls);
            gvDadosCarta.DataSource = table;
            gvDadosCarta.DataBind();
            if (table.Rows.Count > 0)
            {
                gvDadosCarta.SelectedIndex = linha;
                GridViewRow row = gvDadosCarta.SelectedRow;
                ViewState["cmpIdFaturaCarta"] = row.Cells[7].Text;
                LoadCarta();
            }
            else
                LimpaCamposCarta();
        }

        private void PesquisaNota(int linha)
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();

            lc = new ListCampo();
            lc.NomeCampo = "cmpIdFaturaCarta";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = ViewState["cmpIdFaturaCarta"].ToString();
            ls.Add(lc);

            lc = new ListCampo();
            lc.NomeCampo = "cmpInStautsNota";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = "1";
            ls.Add(lc);

            DataTable table = tblFaturaCartaNota.Get(Global.GetConnection(), ls);
            gvDadosNota.DataSource = table;
            gvDadosNota.DataBind();

            ValorTotalNota = 0;

            if (table.Rows.Count > 0)
            {
                gvDadosNota.SelectedIndex = linha;
                GridViewRow row1 = gvDadosNota.SelectedRow;
                ViewState["cmpIdFaturaCartaNota"] = row1.Cells[3].Text;
                LoadNota();
            }
            else
                LimpaCamposNota();

        }

        /// <summary>
        /// Validar campos da carta
        /// </summary>
        private bool ValidarCamposCarta()
        {
            bool ret = true;
            string msg = "";
            if (txtNomeOrgao.Text == "")
                msg += "Nome do orgão em branco! <br />";
            if (txtEmailEng.Text == "")
                msg += "Endereço de email engenheiro em branco! <br />";

            if (msg != "")
            {
                MsgBox.ShowMessage(msg, "Erro");
                ret = false;
            }

            return ret;
        }

        /// <summary>
        /// Validar campos da Nota Fiscal
        /// </summary>
        private bool ValidarCamposNota()
        {
            bool ret = true;
            string msg = "";
            if (txtNuNotaFiscal.Text == "")
                msg += "Número da nota fiscal em branco! <br />";
            if (txtEmissaoNota.Text == "")
                msg += "Data da emissão da nota fiscal em branco! <br />";
            if (txtValorNota.Text == "")
                msg += "Valor da nota fiscal em branco! <br />";

            if (msg != "")
            {
                MsgBox.ShowMessage(msg, "Erro");
                ret = false;
            }

            return ret;
        }

        private bool GravarNota()
        {
            bool ret = false;

            if (ValidarCamposNota())
            {
                tblFaturaCartaNota table = new tblFaturaCartaNota();
                table.cmpIdFaturaCartaNota          = ViewState["cmpIdFaturaCartaNota"].ToString();
                table.cmpIdFaturaCarta              = ViewState["cmpIdFaturaCarta"].ToString();
                table.cmpNuNotaFiscal               = txtNuNotaFiscal.Text;
                table.cmpDtPeriodoInicialServico    = txtDataInicial.Text;
                table.cmpDtPeriodoFinalServico      = txtDataFinal.Text;
                table.cmpNoMesInicialServico        = txtMesInicial.Text;
                table.cmpNoMesFinalServico          = txtMesFinal.Text;
                table.cmpDtEmissaoNotaFiscal        = txtEmissaoNota.Text;
                table.cmpVlNota                     = txtValorNota.Text;
                table.cmpNoUsuario                  = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();

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
        private void LoadCarta()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpIdFaturaCarta";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = ViewState["cmpIdFaturaCarta"].ToString();
            ls.Add(lc);

            using (DataTable table = tblFaturaCarta.Get(Global.GetConnection(), ls))
            {
                lblNomeObra.Text            = table.Rows[0]["cmpNoObraFatura"].ToString();
                txtNomeDestinatario1.Text   = table.Rows[0]["cmpNoPrimeiroDestinatario"].ToString();
                txtNomeDestinatario2.Text   = table.Rows[0]["cmpNoSegundoDestinatario"].ToString();
                txtNomeOrgao.Text           = table.Rows[0]["cmpNoOrgaoDestinatario"].ToString();
                txtSetor.Text               = table.Rows[0]["cmpNoSetorOrgao"].ToString();
                txtEmailEng.Text            = table.Rows[0]["cmpEdEmailEng"].ToString();
                txtEmailAux.Text            = table.Rows[0]["cmpEdEmailAssistenteAdm"].ToString();

                ViewState["cmpIdFaturaCarta"]    = table.Rows[0]["cmpIdFaturaCarta"].ToString();
                ViewState["cmpNuCartaOrion"]     = table.Rows[0]["cmpNuCartaOrion"].ToString();
                ViewState["cmpEdCartaoOrion"]    = table.Rows[0]["cmpEdCartaoOrion"].ToString();
                ViewState["cmpStEntregaDocObra"] = table.Rows[0]["cmpStEntregaDocObra"].ToString();

                CarregaDocumentoNotAnexoCarta();
                CarregaDocumentoAnexoCarta();

                btnNovo.Enabled        = true;
                gvDadosCarta.Enabled   = true;
                btnVincular.Enabled    = true;
                btnDesvincular.Enabled = true;
                btnCartaOrion.Enabled  = true;
            }
            CarregaEmail();

        }

        private void CarregaEmail()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();

            if (txtEmailEng.Text == "")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpIdObraFatura";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ViewState["cmpIdObraFatura"].ToString();
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "cmpNuNivelEmail";
                lc.TipoCampo = TipoCampo.StringIN;
                lc.Sinal = SinalPesquisa.IN;
                lc.ValorCampo = "2,4";
                ls.Add(lc);

                DataTable tbl = tblFaturaEmailObraFatura.Get(Global.GetConnection(), ls);
                if (tbl != null && tbl.Rows.Count > 0)
                    txtEmailEng.Text = tbl.Rows[0]["cmpDcEmail"].ToString();
            }

            if (txtEmailAux.Text == "")
            {
                ls.Clear();

                lc = new ListCampo();
                lc.NomeCampo = "cmpIdObraFatura";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ViewState["cmpIdObraFatura"].ToString();
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "cmpNuNivelEmail";
                lc.TipoCampo = TipoCampo.Numero;
                lc.Sinal = SinalPesquisa.Igual;
                lc.ValorCampo = "6";
                ls.Add(lc);

                DataTable tbl = tblFaturaEmailObraFatura.Get(Global.GetConnection(), ls);
                if (tbl != null && tbl.Rows.Count > 0)
                    txtEmailAux.Text = tbl.Rows[0]["cmpDcEmail"].ToString();
            }
        }
        
        /// <summary>
        /// Carrega campos da página.
        /// </summary>
        private void LoadNota()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpIdFaturaCartaNota";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = ViewState["cmpIdFaturaCartaNota"].ToString();
            ls.Add(lc);

            using (DataTable table = tblFaturaCartaNota.Get(Global.GetConnection(), ls))
            {
                txtNuNotaFiscal.Text    = table.Rows[0]["cmpNuNotaFiscal"].ToString();
                txtEmissaoNota.Text     = table.Rows[0]["cmpDtEmissaoNotaFiscal"].ToString();
                txtValorNota.Text       = table.Rows[0]["cmpVlNota"].ToString();
                txtDataInicial.Text     = table.Rows[0]["cmpDtPeriodoInicialServico"].ToString();
                txtDataFinal.Text       = table.Rows[0]["cmpDtPeriodoFinalServico"].ToString();
                txtMesInicial.Text      = table.Rows[0]["cmpNoMesInicialServico"].ToString();
                txtMesFinal.Text        = table.Rows[0]["cmpNoMesFinalServico"].ToString();
            }

        }

        /// <summary>
        ///  Retorna tipos de documentos para anexar a carta.
        /// </summary>
        private bool CarregaDocumentoNotAnexoCarta()
        {
            try
            {
                using (DataTable table = tblFaturaCartaAnexo.RetornaDocumentoNotAnexoCarta(Global.GetConnection(), ViewState["cmpIdFaturaCarta"].ToString()))
                {
                    if (table != null && table.Rows.Count > 0)
                    {
                        Objetos.LoadCombo(lstDocumentos, table, "cmpDcFaturaTpoDocAnexo", "cmpIdFaturaTipoDocAnexo", "cmpDcFaturaTpoDocAnexo", true);
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
        ///  Retorna documentos anexados a carta.
        /// </summary>
        private bool CarregaDocumentoAnexoCarta()
        {
            try
            {
                using (DataTable table = tblFaturaCartaAnexo.RetornaDocumentoAnexoCarta(Global.GetConnection(),ViewState["cmpIdFaturaCarta"].ToString()))
                {
                    if (table != null && table.Rows.Count > 0)
                    {
                        Objetos.LoadCombo(lstDocAnexo, table, "cmpDcFaturaTpoDocAnexo", "cmpIdFaturaTipoDocAnexo", "cmpDcFaturaTpoDocAnexo", true);
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
        /// Vincular documento a carta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnVincular_Click(object sender, EventArgs e)
        {
            bool ok = false;
            tblFaturaCartaAnexo table = new tblFaturaCartaAnexo();
            try
            {
                foreach (ListItem item in lstDocumentos.Items)
                {
                    if (item.Selected)
                    {
                        table.cmpIdFaturaCarta          = ViewState["cmpIdFaturaCarta"].ToString();
                        table.cmpIdFaturaTipoDocAnexo   = item.Value;
                        table.cmpNoUsuario              = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();

                        if (!(ok = table.GravarFaturaCartaAnexo(Global.GetConnection())))
                            throw new Exception("Erro ao anexar o documento da carta!");
                    }
                }

                CarregaDocumentoNotAnexoCarta();
                CarregaDocumentoAnexoCarta();
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(ex.ToString(), "Erro");
            }
        }

        /// <summary>
        /// Desvincular documento da carta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDesvincular_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstDocAnexo.Items.Count != 0)
                {
                    tblFaturaCartaAnexo table = new tblFaturaCartaAnexo();

                    foreach (ListItem item in lstDocAnexo.Items)
                    {
                        if (item.Selected)
                        {
                            table.cmpIdFaturaCarta          = ViewState["cmpIdFaturaCarta"].ToString();
                            table.cmpIdFaturaTipoDocAnexo   = item.Value.ToString();

                            table.ExcluirFaturaCartaAnexo(Global.GetConnection());
                        }
                    }

                    CarregaDocumentoNotAnexoCarta();
                    if (lstDocAnexo.Items.Count == 0)
                        lstDocAnexo.Items.Clear();
                    CarregaDocumentoAnexoCarta();
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(ex.ToString(), "Erro");
            }
        }

        private void LimpaCamposCarta()
        {
            ViewState["cmpNuCartaOrion"]    = "0";
            txtNomeDestinatario1.Text       = "";
            txtNomeDestinatario2.Text       = "";
            txtNomeOrgao.Text               = "";
            txtSetor.Text                   = "";
            txtEmailEng.Text                = "";
            txtEmailAux.Text                = "";
            lstDocAnexo.Items.Clear();
            lblNuCarta.Text = "Número da Carta ==> ";
            LoadObra();

        }

        private void LimpaCamposNota()
        {
            ViewState["cmpIdFaturaCartaNota"] = "0";
            txtNuNotaFiscal.Text    = "";
            txtEmissaoNota.Text     = "";
            txtValorNota.Text       = "";
            txtDataInicial.Text     = "";
            txtDataFinal.Text       = "";
            txtMesInicial.Text      = "";
            txtMesFinal.Text        = "";

        }
        private void DownloadFile(string arquivo)
        {
            if (File.Exists(arquivo))
            {
                FileInfo fileInf = new FileInfo(arquivo);

                System.IO.Stream stream = null;
                stream = new FileStream(arquivo, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);

                long bytesToRead = stream.Length;

                if (fileInf.Extension == ".pdf")
                {
                    Response.ContentType = "application/pdf";
                }
                else
                {
                    Response.ContentType = "application/vnd.ms-word";
                }

                Response.AddHeader("content-Disposition", "attachment; filename=" + fileInf.Name.Replace(" ", "_"));
                byte[] buffer = new Byte[10000];

                while (bytesToRead > 0)
                {
                    if (Response.IsClientConnected)
                    {
                        int length = stream.Read(buffer, 0, 10000);
                        Response.OutputStream.Write(buffer, 0, length);
                        Response.Flush();
                        bytesToRead = bytesToRead - length;
                    }
                    else
                        bytesToRead = -1;
                }
            }
            else { MsgBox.ShowMessage("Documento não encontrado !"); }

        }
        private void DownloadFile()
        {
            System.IO.Stream stream = null;
            stream = new FileStream(ViewState["cmpEdCartaoOrion"].ToString(), System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
            long bytesToRead = stream.Length;
            Response.ContentType = "application/octet-stream";

            Response.AddHeader("content-Disposition", "attachment; filename=" + Path.GetFileName(ViewState["cmpEdCartaoOrion"].ToString()));
            byte[] buffer = new Byte[10000];

            while (bytesToRead > 0)
            {
                if (Response.IsClientConnected)
                {
                    int length = stream.Read(buffer, 0, 10000);
                    Response.OutputStream.Write(buffer, 0, length);
                    Response.Flush();
                    bytesToRead = bytesToRead - length;
                }
                else
                    bytesToRead = -1;
            }
        }

        private void GeraCarta()
        {
            DateTime dated;
            dated = DateTime.Now;
            CCWordApp DocWord = new CCWordApp();

            try
            {   //Verifica se a carta foi entregue no cliente
                if (ViewState["cmpStEntregaDocObra"].ToString() == "E" && ViewState["cmpEdCartaoOrion"].ToString()!="")
                    DownloadFile();
                else
                {
                    CultureInfo culture = new CultureInfo("pt-BR");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    CNumeroPorExtenso ValorExtenso = new CNumeroPorExtenso();

                    int dia = DateTime.Now.Day;
                    int ano = DateTime.Now.Year;
                    string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month));
                    string diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(DateTime.Now.DayOfWeek));
                    string data = diasemana + ", " + dia + " de " + mes + " de " + ano;


                    #region Numerar documento
                    string cmpconumeracaodocumento = "";
                    tblNumeracaoDocumento tbl = new tblNumeracaoDocumento();
                    if (string.IsNullOrEmpty(ViewState["cmpNuCartaOrion"].ToString()))
                    {
                        tbl.cmpCoNumeracaoDocumento = "0";
                        tbl.cmpCoFuncionario = "47";    //Departamento Faturamento
                        tbl.cmpCoObra = "23";    //Escritório GrupoOrion
                        tbl.cmpIdObraFatura = ViewState["cmpIdObraFatura"].ToString();
                        tbl.cmpCoTipoNumeracao = ViewState["cmpCoTipoNumeracao"].ToString();
                        tbl.cmpTxObservacoes = "Carta de entrega de NF " + txtNuNotaFiscal.Text.ToString() + " R$ " + txtValorNota.Text.ToString() +
                                                        " referente a " + ViewState["cmpNuMesFatura"].ToString() + "/" + ViewState["cmpNuAnoFatura"].ToString() +
                                                        " " + lblNomeObra.Text.ToString();
                        tbl.cmpDcDocumento = "";
                        tbl.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();
                        ViewState["cmpNuCartaOrion"] = tbl.Save(Global.GetConnection(), ref cmpconumeracaodocumento);
                    }
                    else
                    {
                        ListCampos ls = new ListCampos();
                        ListCampo lc = new ListCampo();
                        lc.NomeCampo = "cmpNuDocumento";
                        lc.TipoCampo = TipoCampo.String;
                        lc.ValorCampo = ViewState["cmpNuCartaOrion"].ToString();
                        ls.Add(lc);
                        DataTable table = tblNumeracaoDocumento.Get(Global.GetConnection(), ls, 10000);
                        if (table != null && table.Rows.Count > 0)
                            cmpconumeracaodocumento = table.Rows[0]["cmpCoNumeracaoDocumento"].ToString();
                    }
                    #endregion

                    string[] Numero_Nota = new string[gvDadosNota.Rows.Count];
                    string[] Emissao_Nota = new string[gvDadosNota.Rows.Count];
                    string[] Valor_Nota = new string[gvDadosNota.Rows.Count];
                    string[] Valor_Extenso = new string[gvDadosNota.Rows.Count];
                    string[] DataIni = new string[gvDadosNota.Rows.Count];
                    string[] DataFim = new string[gvDadosNota.Rows.Count];

                    foreach (GridViewRow row in gvDadosNota.Rows)
                    {
                        Numero_Nota[row.RowIndex] = "Numero_Nota" + row.RowIndex.ToString();
                        Emissao_Nota[row.RowIndex] = "Data_Emissao_Nota" + row.RowIndex.ToString();
                        Valor_Nota[row.RowIndex] = "Valor_Nota" + row.RowIndex.ToString();
                        Valor_Extenso[row.RowIndex] = "Valor_Extenso" + row.RowIndex.ToString();
                        DataIni[row.RowIndex] = "Data_Inicial" + row.RowIndex.ToString();
                        DataFim[row.RowIndex] = "Data_Final" + row.RowIndex.ToString();

                    }

                    Label lblNumeroNota;
                    Label lblValorNota;
                    switch (gvDadosNota.Rows.Count)
                    {
                        case 1:
                            DocWord.Open(ConfigurationManager.AppSettings["WordMod"] + "Modelo de Carta Orion 1.dot");
                            break;
                        case 2:
                            DocWord.Open(ConfigurationManager.AppSettings["WordMod"] + "Modelo de Carta Orion 2_1.dot");
                            break;
                        case 3:
                            DocWord.Open(ConfigurationManager.AppSettings["WordMod"] + "Modelo de Carta Orion 3.dot");
                            break;
                        case 4:
                            DocWord.Open(ConfigurationManager.AppSettings["WordMod"] + "Modelo de Carta Orion 4.dot");
                            break;
                        case 5:
                            DocWord.Open(ConfigurationManager.AppSettings["WordMod"] + "Modelo de Carta Orion 5.dot");
                            break;
                    }

                    DocWord.GotoBookMark("Numero_Doc");
                    DocWord.SetAlignment("Right");
                    DocWord.InsertText(ViewState["cmpNuCartaOrion"].ToString());
                    DocWord.SetFont("nothing");

                    DocWord.GotoBookMark("Data_Carta");
                    DocWord.SetAlignment("Right");
                    DocWord.InsertText(data);
                    DocWord.SetFont("nothing");

                    string NomeDestinatario = "Ilmo(a). Sr(a). ";
                    if (!string.IsNullOrEmpty(txtNomeDestinatario1.Text))
                    {
                        DocWord.GotoBookMark("Nome_Destino");
                        DocWord.SetFont("Bold");
                        DocWord.InsertText(NomeDestinatario + txtNomeDestinatario1.Text);
                        DocWord.SetFont("nothing");
                    }
                    if (!string.IsNullOrEmpty(txtNomeDestinatario2.Text))
                    {
                        DocWord.GotoBookMark("Nome_Destino2");
                        DocWord.SetFont("Bold");
                        DocWord.InsertText(NomeDestinatario + txtNomeDestinatario2.Text);
                        DocWord.SetFont("nothing");
                    }

                    DocWord.GotoBookMark("Nome_Orgao");
                    DocWord.InsertText(txtNomeOrgao.Text);
                    DocWord.SetFont("nothing");

                    if (!string.IsNullOrEmpty(txtSetor.Text))
                    {
                        DocWord.GotoBookMark("Nome_Setor");
                        DocWord.InsertText(txtSetor.Text);
                        DocWord.SetFont("nothing");
                    }

                    DocWord.GotoBookMark("Numero_Contrato");
                    DocWord.InsertText(ViewState["cmpNuContrato"].ToString().TrimEnd());

                    DocWord.GotoBookMark("Tipo_Servico");
                    DocWord.InsertText(ViewState["cmpDcTipoServico"].ToString());

                    foreach (GridViewRow row in gvDadosNota.Rows)
                    {
                        //Número da Nota Fiscal
                        lblNumeroNota = (Label)row.FindControl("lblNota");
                        DocWord.GotoBookMark(Numero_Nota[row.RowIndex]);
                        DocWord.InsertText(lblNumeroNota.Text);

                        //Data emissão da NF
                        DocWord.GotoBookMark(Emissao_Nota[row.RowIndex]);
                        DocWord.InsertText(row.Cells[1].Text);

                        //Valor da NF
                        lblValorNota = (Label)row.FindControl("lblValorNota");
                        DocWord.GotoBookMark(Valor_Nota[row.RowIndex]);
                        string ValorNota = lblValorNota.Text.Replace(".", "");
                        DocWord.InsertText(Convert.ToDecimal(ValorNota).ToString("#,##0.00"));

                        //Valor por extenso da NF
                        ValorExtenso.SetNumero(Convert.ToDecimal(ValorNota.Replace(".", ",")));
                        DocWord.GotoBookMark(Valor_Extenso[row.RowIndex]);
                        DocWord.InsertText(ValorExtenso.ToString());

                        //Período Inicial e Final da execução do serviço
                        if (row.Cells[4].Text != "&nbsp;")
                        {
                            DocWord.GotoBookMark(DataIni[row.RowIndex]);
                            DocWord.InsertText(", referente ao período de " + row.Cells[4].Text);
                            DocWord.GotoBookMark(DataFim[row.RowIndex]);
                            DocWord.InsertText(" a " + row.Cells[5].Text + ".");
                        }
                        else if (row.Cells[6].Text != "&nbsp;")
                        {
                            DocWord.GotoBookMark(DataIni[row.RowIndex]);
                            DocWord.InsertText(", referente ao período de " + row.Cells[6].Text);
                        }
                        else if (row.Cells[7].Text != "&nbsp;")
                        {
                            DocWord.GotoBookMark(DataFim[row.RowIndex]);
                            DocWord.InsertText(" a " + row.Cells[7].Text + ".");
                        }
                        else
                        {
                            DocWord.GotoBookMark(DataIni[row.RowIndex]);
                            DocWord.InsertText(".");
                        }
                    }

                    if (lstDocAnexo.Items.Count != 0)
                    {
                        DocWord.GotoBookMark("Titulo_Anexo");
                        DocWord.InsertText("P.S: Em anexo os seguintes documentos:");

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
                    }

                    DocWord.InsertLineBreak();

                    DocWord.SaveAs(ConfigurationManager.AppSettings["WordDoc"] + "CO" + ViewState["cmpNuCartaOrion"].ToString().Replace("/", "-") + " " + lblNomeObra.Text.ToString().TrimEnd() + ".doc");
                    DocWord.Quit();

                    #region Grava nome do arquivo na tabela de documentos
                    tblNumeracaoDocumento.AddFile(Global.GetConnection(), cmpconumeracaodocumento, ConfigurationManager.AppSettings["WordDoc"].ToString() + "CO" + ViewState["cmpNuCartaOrion"].ToString().Replace("/", "-") + " " + lblNomeObra.Text.ToString().TrimEnd() + ".doc");
                    #endregion

                    #region gravar data e número da carta orion
                    tblFaturaCarta tbl1 = new tblFaturaCarta();
                    tbl1.cmpIdFaturaCarta = ViewState["cmpIdFaturaCarta"].ToString();
                    tbl1.cmpIdFaturaObra = ViewState["cmpIdFaturaObra"].ToString();
                    tbl1.cmpEdCartaoOrion = ConfigurationManager.AppSettings["WordDoc"].ToString() + "CO" + ViewState["cmpNuCartaOrion"].ToString().Replace("/", "-") + " " + lblNomeObra.Text.ToString().TrimEnd() + ".doc";
                    tbl1.cmpNuCartaOrion = ViewState["cmpNuCartaOrion"].ToString();
                    tbl1.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();
                    tbl1.GravarEmissaoFaturaCarta(Global.GetConnection());
                    #endregion

                    Response.Clear();
                    Response.ContentType = "application/msword";
                    Response.AddHeader("Content-disposition", "filename=" + ConfigurationManager.AppSettings["WordDoc"] + "CO" + ViewState["cmpNuCartaOrion"].ToString().Replace("/", "-") + " " + lblNomeObra.Text.ToString().TrimEnd() + ".doc");
                    Response.WriteFile(ConfigurationManager.AppSettings["WordDoc"] + "CO" + ViewState["cmpNuCartaOrion"].ToString().Replace("/", "-") + " " + lblNomeObra.Text.ToString().TrimEnd() + ".doc");
                }
   
            }
            catch (Exception ex)
            {
                DocWord.Quit();
                DocWord.Dispose();
                //DocWord.Quit();
                DocWord = null;
                
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
                    ViewState["cmpIdFaturaObra"] = Request.QueryString["idFaturaObra"];
                    LoadObra();
                    loadTipoNumeracao();

                    PesquisaCarta(0);
                    if (gvDadosCarta.Rows.Count == 0)
                        btnNovo_Click(sender, null);
                    else
                    {
                        gvDadosCarta_SelectedIndexChanged(sender, null);
                        PesquisaNota(0);
                        if (gvDadosNota.Rows.Count == 0)
                            btnNovaNota_Click(sender, null);
                        else
                            gvDadosNota_SelectedIndexChanged(sender, null);
                    }
                }
            }
        }

        #region Grid Carta
        protected void gvDadosCarta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.Trim())
            {
                case "btnCarta":
                    if (gvDadosNota.Rows.Count != 0)
                    {
                        ImageButton img = (ImageButton)e.CommandSource;
                        if (string.IsNullOrEmpty(img.OnClientClick)==true)
                        {
                            GeraCarta();
                        }
                        else
                        {
                            DownloadFile(img.OnClientClick);
                        }
                    }
                    else
                    {
                        MsgBox.ShowMessage("Favor informar dados da nota fiscal!", "Erro");
                    }
                    break;
                case "btnEmail":
                    tblFaturaObra.EnviarEmailFaturamentoEmissaoNF(Global.GetConnection(), ViewState["cmpIdFaturaCarta"].ToString());
                    MsgBox.ShowMessage("Email enviado com sucesso!", "Aviso");
                    break;
                case "btnRegerar":
                    GeraCarta();
                    break;

            }
        }

        protected void gvDadosCarta_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow Linha = gvDadosCarta.SelectedRow;
            Label lblNumeroCarta = (Label)Linha.FindControl("lblNuCarta");
            if (lblNumeroCarta.Text == "")
            {
                Label lblRascunho = ((Label)Linha.Cells[1].FindControl("lblRascunho"));
                lblNuCarta.Text = "Número da Carta ==> " + lblRascunho.Text;
            }
            else
                lblNuCarta.Text = "Número da Carta ==> " + lblNumeroCarta.Text;

            ViewState["cmpIdFaturaCarta"] = Linha.Cells[7].Text;
            LoadCarta();

            PesquisaNota(0);
            if (gvDadosNota.Rows.Count == 0)
                btnNovaNota_Click(sender, null);
            else
                btnCancelarNota_Click(sender, null);
        }

        protected void gvDadosCarta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                    break;
                case DataControlRowType.DataRow:
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                    e.Row.Cells[8].Visible = false;

                    Label lblCaminho = (Label)e.Row.FindControl("lblCaminho");
                        e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
                        e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                            ImageButton img = (ImageButton)e.Row.FindControl("Image1");
                            ImageButton imgVerDo = (ImageButton)e.Row.FindControl("imgVerdoc");

                            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gvDadosCarta, String.Concat("Select$", e.Row.RowIndex.ToString()));
                            

                                if (string.IsNullOrEmpty(lblCaminho.Text) == true)
                                {

                                    imgVerDo.Visible = false;
                                    img.Visible = true;
                                }
                                else
                                {

                                    img.Visible = false;
                                    imgVerDo.Visible = true;
                                    string strPath = lblCaminho.Text;

                                    imgVerDo.OnClientClick = strPath;

                                }
                          

                    Label lblValor = (Label)e.Row.FindControl("lblValor");
                    if (lblValor.Text != "")
                        ValorTotal += Decimal.Parse(lblValor.Text);

                    Label lblNuCarta = (Label)e.Row.FindControl("lblNuCarta");
                    if (lblNuCarta.Text == "")
                    {
                        e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                        Label lblRascunho = ((Label)e.Row.Cells[1].FindControl("lblRascunho"));
                        Rascunho++;
                        lblRascunho.Text = "Rascunho " + Rascunho.ToString();
                        lblRascunho.Visible = true;

                        Label lblNumeroCarta = ((Label)e.Row.Cells[1].FindControl("lblNumeroCarta"));
                        lblNumeroCarta.Visible = true;
                    }
                    else
                    {
                        Label lblRascunho = ((Label)e.Row.Cells[1].FindControl("lblRascunho"));
                        lblRascunho.Visible = false;

                        Label lblNumeroCarta = ((Label)e.Row.Cells[1].FindControl("lblNumeroCarta"));
                        lblNumeroCarta.Visible = true;
                        e.Row.Cells[1].ForeColor = System.Drawing.Color.Green;
                    }
                    break;
                case DataControlRowType.Footer:
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;

                    Label lblValorTotal = (Label)e.Row.FindControl("lblValorTotal");
                    if (ValorTotal.ToString() != "")
                        lblValorTotal.Text = ValorTotal.ToString("N2");
                    break;
            }

        }
        #endregion

        #region Events Carta
        protected void btnNovo_Click(object sender, EventArgs e)
        {
            ViewState["cmpIdFaturaCarta"] = "0";
            LimpaCamposCarta();
            CarregaDocumentoNotAnexoCarta();
            LimpaCamposNota();
            PesquisaNota(0);

            divNota.Visible          = false;
            btnNovo.Enabled          = false;
            gvDadosCarta.Enabled     = false;
            btnVincular.Enabled      = false;
            btnDesvincular.Enabled   = false;
            btnExcluirCarta.Enabled  = false;
            btnCartaOrion.Enabled    = false;
            btnCancelarCarta.Enabled = true;

            txtNomeOrgao.Text           = ViewState["cmpNoOrgao"].ToString();
            txtNomeDestinatario1.Text   = ViewState["cmpNoPrimeiroGestor"].ToString();
            txtNomeDestinatario2.Text   = ViewState["cmpNoSegundoGestor"].ToString();
            txtSetor.Text               = ViewState["cmpNoSetorGestor"].ToString();

            txtNomeDestinatario1.Focus();
        }

        protected void btnGravarCarta_Click(object sender, EventArgs e)
        {
            string IdFaturaCarta = ViewState["cmpIdFaturaCarta"].ToString();
            if (ValidarCamposCarta())
            {
                tblFaturaCarta table = new tblFaturaCarta();
                table.cmpIdFaturaCarta          = ViewState["cmpIdFaturaCarta"].ToString();
                table.cmpIdFaturaObra           = ViewState["cmpIdFaturaObra"].ToString();
                table.cmpNoPrimeiroDestinatario = txtNomeDestinatario1.Text;
                table.cmpNoSegundoDestinatario  = txtNomeDestinatario2.Text;
                table.cmpNoOrgaoDestinatario    = txtNomeOrgao.Text;
                table.cmpNoSetorOrgao           = txtSetor.Text;
                table.cmpEdEmailEng             = txtEmailEng.Text;
                table.cmpEdEmailAssistenteAdm   = txtEmailAux.Text;
                table.cmpNoUsuario              = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();

                if (table.Save(Global.GetConnection(), ref IdFaturaCarta))
                {
                    int IndiceGridCarta;
                    if (ViewState["cmpIdFaturaCarta"].ToString() == "0")
                        IndiceGridCarta = gvDadosCarta.Rows.Count;
                    else
                        IndiceGridCarta = gvDadosCarta.SelectedIndex;

                    ViewState["cmpIdFaturaCarta"] = IdFaturaCarta;
                    PesquisaCarta(IndiceGridCarta);
                    gvDadosCarta_SelectedIndexChanged(sender, null);

                    btnNovo.Enabled             = true;
                    gvDadosCarta.Enabled        = true;
                    btnVincular.Enabled         = true;
                    btnDesvincular.Enabled      = true;
                    divNota.Visible             = true;
                    btnExcluirCarta.Enabled     = true;
                    btnCartaOrion.Enabled       = true;
                    btnCancelarCarta.Enabled    = false;
                }
            }
        }

        protected void btnExcluirCarta_Click(object sender, EventArgs e)
        {
            tblFaturaCarta tbl      = new tblFaturaCarta();
            tbl.cmpIdFaturaObra     = ViewState["cmpIdFaturaObra"].ToString();
            tbl.cmpIdFaturaCarta    = ViewState["cmpIdFaturaCarta"].ToString();
            tbl.cmpNoUsuario        = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();
            tbl.ExcluirFaturaCarta(Global.GetConnection());

            PesquisaCarta(0);
            if (gvDadosCarta.Rows.Count != 0)
                gvDadosCarta_SelectedIndexChanged(sender, null);

            MsgBox.ShowMessage("Registro excluído com sucesso!", "Aviso");
        }

        protected void btnCancelarCarta_Click(object sender, EventArgs e)
        {
            btnNovo.Enabled             = true;
            gvDadosCarta.Enabled        = true;
            btnVincular.Enabled         = true;
            btnDesvincular.Enabled      = true;
            btnExcluirCarta.Enabled     = true;
            divNota.Visible             = true;
            btnCartaOrion.Enabled       = true;
            btnCancelarCarta.Enabled    = false;

            gvDadosCarta_SelectedIndexChanged(sender, null);
            txtNomeDestinatario1.Focus();
        }
        #endregion

        #region Gride Notas
        protected void gvDadosNota_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                    break;
                case DataControlRowType.DataRow:
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                    e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
                    e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gvDadosNota, String.Concat("Select$", e.Row.RowIndex.ToString()));



                    //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gvDadosNota, String.Concat("Select$", "q"));
                    Label lblValorNota = (Label)e.Row.FindControl("lblValorNota");
                    lblValorNota = (Label)e.Row.FindControl("lblValorNota");
                    if (lblValorNota.Text != "")
                        ValorTotalNota += Decimal.Parse(lblValorNota.Text);
                    break;
                case DataControlRowType.Footer:
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                    Label lblValorTotalNota = (Label)e.Row.FindControl("lblValorTotalNota");
                    if (ValorTotalNota.ToString() != "")
                        lblValorTotalNota.Text = ValorTotalNota.ToString("N2");
                    break;
            }
        }

        protected void gvDadosNota_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.Trim())
            {
                case "lnkNota":
                    ViewState["cmpIdFaturaCartaNota"] = e.CommandArgument.ToString();
                    LoadNota();
                    break;
                case "excluir":
                    //Response.Redirect("webFAT_RecebeFatura.aspx?idFaturaObra=" + e.CommandArgument.ToString(), false);
                    break;
            }
        }

        protected void gvDadosNota_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow Linha = gvDadosNota.SelectedRow;
            ViewState["cmpIdFaturaCartaNota"] = Linha.Cells[3].Text;
            LoadNota();
        }
        #endregion

        #region events Nota
        protected void btnNovaNota_Click(object sender, EventArgs e)
        {
            ViewState["cmpIdFaturaCartaNota"] = "0";
            LimpaCamposNota();

            btnNovaNota.Enabled     = false;
            gvDadosNota.Enabled     = false;
            btnExcluirNota.Enabled  = false;
            btnCancelarNota.Enabled = true;

            txtNuNotaFiscal.Focus();
        }

        protected void btnGravarNota_Click(object sender, EventArgs e)
        {
            if (GravarNota())
            {
                ValorTotalNota = 0;
                int indiceGridNota;
                if (gvDadosNota.Rows.Count != 0)
                {
                    if (ViewState["cmpIdFaturaCartaNota"].ToString() == "0")
                        indiceGridNota = gvDadosNota.SelectedIndex + 1;
                    else
                        indiceGridNota = gvDadosNota.SelectedIndex;
                }
                else
                    indiceGridNota = 0;

                PesquisaNota(indiceGridNota);
                gvDadosNota_SelectedIndexChanged(sender, null);

                PesquisaCarta(gvDadosCarta.SelectedIndex);

                btnNovaNota.Enabled = true;
                gvDadosNota.Enabled = true;
                btnExcluirNota.Enabled = true;
                btnCancelarNota.Enabled = false;
            }
        }

        protected void btnExcluirNota_Click(object sender, EventArgs e)
        {
            tblFaturaCartaNota tbl = new tblFaturaCartaNota();
            tbl.cmpIdFaturaCartaNota    = ViewState["cmpIdFaturaCartaNota"].ToString();
            tbl.cmpIdFaturaCarta        = ViewState["cmpIdFaturaCarta"].ToString();
            tbl.ExcluirFaturaCartaNota(Global.GetConnection());

            PesquisaNota(0);
            PesquisaCarta(gvDadosCarta.SelectedIndex);

            MsgBox.ShowMessage("Registro excluído com sucesso!", "Aviso");
        }

        protected void btnCancelarNota_Click(object sender, EventArgs e)
        {
            btnNovaNota.Enabled     = true;
            gvDadosNota.Enabled     = true;
            btnExcluirNota.Enabled  = true;
            btnCancelarNota.Enabled = false;

            gvDadosNota_SelectedIndexChanged(sender, null);
        }
        #endregion

        protected void btnEnviarEmail_Click(object sender, EventArgs e)
        {
            tblFaturaObra.EnviarEmailFaturamentoEmissaoNF(Global.GetConnection(), ViewState["cmpIdFaturaCarta"].ToString());
            MsgBox.ShowMessage("Email enviado com sucesso!", "Aviso");
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Faturamento/webFAT_FaturamentoMes.aspx?NuMesFatura=" + ViewState["cmpNuMesFatura"].ToString() 
                                                                       + "&NuAnoFatura=" + ViewState["cmpNuAnoFatura"].ToString() 
                                                                       + "&IdTipoServico=" + ViewState["cmpIdTipoServico"].ToString(), false);
        }

        #endregion

        protected void btnRegerarCarta_Click1(object sender, EventArgs e)
        {
            if (RegerarCarta == false)
            {
                RegerarCarta = true;
            }
            else
            {
                RegerarCarta = false;
            }
            LoadCarta();
        }
    }
}