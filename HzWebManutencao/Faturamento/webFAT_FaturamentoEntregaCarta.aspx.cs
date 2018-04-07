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

namespace HzWebManutencao.Faturamento
{
    public partial class webFAT_FaturamentoEntregaCarta : System.Web.UI.Page
    {
        #region Variables
        private decimal ValorTotal = 0;
        static string prevPage = string.Empty;
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
                lblNomeObra.Text = table.Rows[0]["cmpNoObraFatura"].ToString();
                lblTipoServico.Text = table.Rows[0]["cmpDcTipoServico"].ToString();
                ViewState["cmpIdObraFatura"] = table.Rows[0]["cmpIdObraFatura"].ToString();
                ViewState["cmpNuMesFatura"] = table.Rows[0]["cmpNuMesFatura"].ToString();
                ViewState["cmpNuAnoFatura"] = table.Rows[0]["cmpNuAnoFatura"].ToString();
                ViewState["cmpNuContrato"] = table.Rows[0]["cmpNuContrato"].ToString();
                ViewState["cmpDcTipoServico"] = table.Rows[0]["cmpDcTipoServico"].ToString();
                ViewState["cmpIdTipoServico"] = table.Rows[0]["cmpIdTipoServico"].ToString();

                ViewState["cmpNoOrgao"] = table.Rows[0]["cmpNoOrgao"].ToString();
                ViewState["cmpNoPrimeiroGestor"] = table.Rows[0]["cmpNoPrimeiroGestor"].ToString();
                ViewState["cmpNoSegundoGestor"] = table.Rows[0]["cmpNoSegundoGestor"].ToString();
                ViewState["cmpNoSetorGestor"] = table.Rows[0]["cmpNoSetorGestor"].ToString();
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
                ViewState["cmpIdFaturaCarta"] = row.Cells[4].Text;
                LoadCarta();
            }
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
                lblNomeObra.Text = table.Rows[0]["cmpNoObraFatura"].ToString();
                txtNomeDestinatario1.Text = table.Rows[0]["cmpNoPrimeiroDestinatario"].ToString();
                txtNomeDestinatario2.Text = table.Rows[0]["cmpNoSegundoDestinatario"].ToString();
                txtNomeOrgao.Text = table.Rows[0]["cmpNoOrgaoDestinatario"].ToString();
                txtSetor.Text = table.Rows[0]["cmpNoSetorOrgao"].ToString();

                ViewState["cmpIdFaturaCarta"] = table.Rows[0]["cmpIdFaturaCarta"].ToString();
                ViewState["cmpNuCartaOrion"] = table.Rows[0]["cmpNuCartaOrion"].ToString();

                LoadNumeroDocumento();
            }

 
        }

        /// <summary>
        /// Carrega campos da página.
        /// </summary>
        private void LoadNumeroDocumento()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpNuDocumento";
            lc.TipoCampo = TipoCampo.String;
            lc.ValorCampo = ViewState["cmpNuCartaOrion"].ToString();
            ls.Add(lc);
            DataTable table = tblNumeracaoDocumento.Get(Global.GetConnection(), ls, 10000);
            if (table != null && table.Rows.Count > 0)
                ViewState["cmpCoNumeracaoDocumento"] = table.Rows[0]["cmpCoNumeracaoDocumento"].ToString();
        }


        private void DownloadFile(string filename)
        {
            System.IO.Stream stream = null;
            stream = new FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
            long bytesToRead = stream.Length;
            Response.ContentType = "application/octet-stream";

            Response.AddHeader("content-Disposition", "attachment; filename=" + Path.GetFileName(filename));
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
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                prevPage = Request.UrlReferrer.AbsolutePath;

                if (Request.QueryString != null && Request.QueryString.Count > 0)
                {
                    ViewState["cmpIdFaturaObra"] = Request.QueryString["idFaturaObra"];
                    LoadObra();
                    PesquisaCarta(0);
                }
            }
        }

        #region Grid Carta
        protected void gvDadosCarta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.Trim())
            {
                case "btnCarta":
                    DownloadFile(e.CommandArgument.ToString());
                    
                    break;
            }
        }

        protected void gvDadosCarta_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow Linha = gvDadosCarta.SelectedRow;
            Label lblNumeroCarta = (Label)Linha.FindControl("lblNumeroCarta");
            lblNuCarta.Text = "Número da Carta ==> " + lblNumeroCarta.Text;
            ViewState["cmpIdFaturaCarta"] = Linha.Cells[4].Text;
            LoadCarta();
        }

        protected void gvDadosCarta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Cells[4].Visible = false;
                    break;
                case DataControlRowType.DataRow:
                    e.Row.Cells[4].Visible = false;

                    e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
                    e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gvDadosCarta, String.Concat("Select$", e.Row.RowIndex.ToString()));

                    Label lblValor = (Label)e.Row.FindControl("lblValor");
                    if (lblValor.Text != "")
                        ValorTotal += Decimal.Parse(lblValor.Text);

                        break;
                case DataControlRowType.Footer:
                    e.Row.Cells[4].Visible = false;

                    Label lblValorTotal = (Label)e.Row.FindControl("lblValorTotal");
                    if (ValorTotal.ToString() != "")
                        lblValorTotal.Text = ValorTotal.ToString("N2");
                    break;
            }

        }
        #endregion

        #region Events Carta

        protected void btnGravarCarta_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(ConfigurationManager.AppSettings["WordDoc"]))
                Directory.CreateDirectory(ConfigurationManager.AppSettings["WordDoc"]);
            string sNomeArquivo = ConfigurationManager.AppSettings["WordDoc"] + System.IO.Path.GetFileName(fileUpload.PostedFile.FileName);

            //ConfigurationManager.AppSettings["WordDoc"] += System.IO.Path.GetFileName(fileUpload.PostedFile.FileName);

            //Atualiza o nome do novo arquivo entregue no cliente
            tblNumeracaoDocumento.AddFile(Global.GetConnection(), ViewState["cmpCoNumeracaoDocumento"].ToString(), sNomeArquivo);

            //Grava arquivo no servidor
            if (fileUpload.FileName != "")
                fileUpload.PostedFile.SaveAs(sNomeArquivo);

            //Atualiza o nome do novo arquivo entregue no cliente
            tblFaturaCarta table = new tblFaturaCarta();
            table.cmpIdFaturaCarta  = ViewState["cmpIdFaturaCarta"].ToString();
            table.cmpIdFaturaObra   = ViewState["cmpIdFaturaObra"].ToString();
            table.cmpEdCartaoOrion  = sNomeArquivo;
            table.cmpNoUsuario      = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();

            table.GravaEntregaCarta(Global.GetConnection());
            int IndiceGridCarta;
            if (ViewState["cmpIdFaturaCarta"].ToString() == "0")
                IndiceGridCarta = gvDadosCarta.Rows.Count;
            else
                IndiceGridCarta = gvDadosCarta.SelectedIndex;

            PesquisaCarta(IndiceGridCarta);
            gvDadosCarta_SelectedIndexChanged(sender, null);
        }
        #endregion

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage + "?NuMesFatura=" + ViewState["cmpNuMesFatura"].ToString() + "&NuAnoFatura=" + ViewState["cmpNuAnoFatura"].ToString() + "&IdTipoServico=" + ViewState["cmpIdTipoServico"].ToString(), false);
        }

        #endregion
    }
}