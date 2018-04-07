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
    public partial class webFAT_FaturamentoEnviaRelManut : System.Web.UI.Page
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
                lblNomeObra.Text    = table.Rows[0]["cmpNoObraFatura"].ToString();
                lblTipoServico.Text = table.Rows[0]["cmpDcTipoServico"].ToString();
                ViewState["cmpIdObraFatura"] = table.Rows[0]["cmpIdObraFatura"].ToString();
                ViewState["cmpNuMesFatura"]  = table.Rows[0]["cmpNuMesFatura"].ToString();
                ViewState["cmpNuAnoFatura"]  = table.Rows[0]["cmpNuAnoFatura"].ToString();
                ViewState["cmpCoObra"]       = table.Rows[0]["cmpCoObra"].ToString();
            }
        }

        /// <summary>
        /// Exibe relatórios da Obra.
        /// </summary>
        private void Pesquisa(int linha)
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();

            lc = new ListCampo();
            lc.NomeCampo = "cmpCoObra";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = ViewState["cmpCoObra"].ToString();
            ls.Add(lc);
            
            //Relatório de Manutenção
            lc = new ListCampo();
            lc.NomeCampo = "cmpCoTipoNumeracao";
            lc.ValorCampo = "5"; 
            ls.Add(lc);

            DataTable table = tblNumeracaoDocumento.Get(Global.GetConnection(), ls,1000);

            if (table.Rows.Count > 0)
            {
                gvDados.DataSource = table;
                gvDados.DataBind();

                gvDados.SelectedIndex = linha;
                GridViewRow row = gvDados.SelectedRow;
                ViewState["cmpCoNumeracaoDocumento"] = row.Cells[3].Text;
                Load();
            }
        }

        /// <summary>
        /// Carrega campos da página.
        /// </summary>
        private void Load()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpCoNumeracaoDocumento";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = ViewState["cmpCoNumeracaoDocumento"].ToString();
            ls.Add(lc);

            using (DataTable table = tblNumeracaoDocumento.Get(Global.GetConnection(), ls,1000))
            {
                txtNumeroRelManut.Text = table.Rows[0]["cmpNuDocumento"].ToString();
                txtDescDocumento.Text  = table.Rows[0]["cmpTxObservacoes"].ToString();
                txtEndDocumento.Text   = table.Rows[0]["cmpDcDocumento"].ToString();

                //LoadNumeroDocumento();
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
                    Pesquisa(0);
                }
            }
        }

        #region Grid Relatório Manutenção
        protected void gvDados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.Trim())
            {
                case "btnRelatorio":
                    DownloadFile(e.CommandArgument.ToString());
                    break;
            }
        }

        protected void gvDados_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow Linha = gvDados.SelectedRow;
            ViewState["cmpCoNumeracaoDocumento"] = Linha.Cells[3].Text;
            Load();
        }

        protected void gvDados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Cells[3].Visible = false;
                    break;
                case DataControlRowType.DataRow:
                    e.Row.Cells[3].Visible = false;
                    e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
                    e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gvDados, String.Concat("Select$", e.Row.RowIndex.ToString()));
                    break;
                case DataControlRowType.Footer:
                    e.Row.Cells[3].Visible = false;
                    break;
            }
        }
        #endregion

        #region Events Relatório
        protected void btnGerarNumero_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(ConfigurationManager.AppSettings["RelManutencao"]))
                Directory.CreateDirectory(ConfigurationManager.AppSettings["RelManutencao"]);
            string sNomeArquivo = ConfigurationManager.AppSettings["RelManutencao"] + System.IO.Path.GetFileName(fileUpload.PostedFile.FileName);

            //ConfigurationManager.AppSettings["WordDoc"] += System.IO.Path.GetFileName(fileUpload.PostedFile.FileName);

            //Atualiza o nome do novo arquivo entregue no cliente
            tblNumeracaoDocumento.AddFile(Global.GetConnection(), ViewState["cmpCoNumeracaoDocumento"].ToString(), sNomeArquivo);

            //Grava arquivo no servidor
            if (fileUpload.FileName != "")
                fileUpload.PostedFile.SaveAs(sNomeArquivo);

            //Atualiza o nome do arquivo entregue no cliente
            //tblFaturaCarta table = new tblFaturaCarta();
            //table.cmpIdFaturaCarta  = ViewState["cmpIdFaturaCarta"].ToString();
            //table.cmpIdFaturaObra   = ViewState["cmpIdFaturaObra"].ToString();
            //table.cmpEdCartaoOrion  = sNomeArquivo;
            //table.cmpNoUsuario      = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();

            //table.GravaEntregaCarta(Global.GetConnection());
            //int IndiceGridCarta;
            //if (ViewState["cmpIdFaturaCarta"].ToString() == "0")
            //    IndiceGridCarta = gvDados.Rows.Count;
            //else
            //    IndiceGridCarta = gvDados.SelectedIndex;

            //Pesquisa(IndiceGridCarta);
            //gvDados_SelectedIndexChanged(sender, null);
        }
        #endregion

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage + "?NuMesFatura=" + ViewState["cmpNuMesFatura"].ToString() + "&NuAnoFatura=" + ViewState["cmpNuAnoFatura"].ToString() + "&IdTipoServico=" + ViewState["cmpIdTipoServico"].ToString(), false);
        }

        #endregion

    }
}