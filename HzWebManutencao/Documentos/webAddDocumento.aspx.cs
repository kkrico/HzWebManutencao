using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HzClasses.Tabelas.Apoio;
using HzClasses.Numeracao;
using System.IO;
using HzLibConnection.Data;
using HzLibConnection.Sql;
using HzLibCorporativo.Funcional;
using HzLibGeneral.Util;

namespace HzWebManutencao.Documentos
{
    public partial class webAddDocumento : System.Web.UI.Page
    {
        /// <summary>
        /// Carrega as combos auxiliares.
        /// </summary>
        /// <returns></returns>
        private bool load()
        {
            try
            {
                DataTable table = tblTipoNumeracao.Get((HzConexao)HttpContext.Current.Session["connection"], new ListCampos());
                HzlibWEB.Objetos.LoadCombo(cmbTipo, table, "cmpDcTipoNumeracao", "cmpCoTipoNumeracao", "cmpDcTipoNumeracao", true);
                cmbTipo_SelectedIndexChanged(cmbTipo, new EventArgs());
                table.Reset();

                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoContratante";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLogin)Session["login"]).cmpCoContratante;
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "cmpInAtivo";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = "1";
                ls.Add(lc);

                table = tblObra.Get((HzConexao)HttpContext.Current.Session["connection"], ls);
                HzlibWEB.Objetos.LoadCombo(cmbObra, table, "cmpNoObra", "cmpCoObra", "cmpNoObra", true);
                table.Reset();

                ls = new ListCampos();
                lc = new ListCampo();
                lc.NomeCampo = "cmpAtivo";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = "1";
                ls.Add(lc);

                table = tblFuncionario.Get((HzConexao)HttpContext.Current.Session["connection"], ls);
                HzlibWEB.Objetos.LoadCombo(cmbPessoal, table, "cmpNoFuncionario", "cmpCoFuncionario", "cmpNoFuncionario", true);
                table.Reset();
                txtFornecedor.Text = "";
                //table = tblFornecedor.RetornarDados((HzConexao)HttpContext.Current.Session["connection"], new ListCampos());
                //HzlibWEB.Objetos.LoadCombo(cmbFornecedor, table, "cmpNoFornecedor", "cmpCoFornecedor", "cmpNoFornecedor", "---- Selecione um Fornecedor ----", true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Carrega as combos auxiliares.
        /// </summary>
        /// <returns></returns>
        private bool loadFornecedor()
        {
            try
            {
                txtFornecedor.Text = "";
                //DataTable table = tblFornecedor.RetornarDados((HzConexao)HttpContext.Current.Session["connection"], new ListCampos());
                //HzlibWEB.Objetos.LoadCombo(cmbFornecedor, table, "cmpNoFornecedor", "cmpCoFornecedor", "cmpNoFornecedor", "---- Selecione um Fornecedor ----", true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Exibe os campos para cadastramento do fornecedor.
        /// </summary>
        /// <param name="b"></param>
        private void showFornecedor(bool b)
        {
            lblFornecedor.Visible = b;
            txtFornecedor.Visible = b;
            cmbFornecedor.Visible = b;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.load();
                this.loadFornecedor();
            }
        }

        protected void cmbObra_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //string numero = tblNumeracaoDocumento.RetornarUltimoNumero((HzConexao)Global.getConnection(), cmbTipo.SelectedItem.Value);
                //lblUltimoNumero.Text = "Último Número: " + numero;
                lblNovoNumero.Text = "";
                showFornecedor(cmbTipo.SelectedItem.Text.Trim().ToLower() == "ordem de compra");
            }
            catch
            {
            }
        }

        protected void grdReport_PageIndexChanged(object sender, EventArgs e)
        {
        }

        protected void grdReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
        }

        protected void grdReport_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DownloadFile(string filename)
        {
            System.IO.Stream stream = null;

            try
            {
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
            catch (Exception ex)
            {
            }
        }

        protected void grdReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToLower().Trim())
                {
                    case "lnk":
                        Response.Redirect("webTabNumeracaoDocumento.aspx?cmpNuDocumento=" + e.CommandArgument.ToString());
                        break;

                    case "cmd":
                        DownloadFile(e.CommandArgument.ToString());
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void reset()
        {
            ViewState["cmpCoNumeracaoDocumento"] = "0";
            this.loadFornecedor();
            showFornecedor(false);
            txtDescription.Text = "";
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                string cmpconumeracaodocumento = "";
                tblNumeracaoDocumento tbl   = new tblNumeracaoDocumento();
                tbl.cmpCoNumeracaoDocumento = ViewState["cmpCoNumeracaoDocumento"] == null ? "0" : ViewState["cmpCoNumeracaoDocumento"].ToString();
                tbl.cmpCoFuncionario        = cmbPessoal.SelectedItem.Value;
                tbl.cmpCoObra               = cmbObra.SelectedItem.Value;
                tbl.cmpCoTipoNumeracao      = cmbTipo.SelectedItem.Value;
                tbl.cmpTxObservacoes        = txtDescription.Text;
                //tbl.cmpNoFornecedor = cmbFornecedor.SelectedItem.Value == "0" ? txtFornecedor.Text : cmbFornecedor.SelectedItem.Text;
                tbl.cmpDcDocumento          = "";
                string numero               = tbl.Save((HzConexao)HttpContext.Current.Session["connection"], ref cmpconumeracaodocumento);
                lblNovoNumero.Text          = "O número gerado foi: " + numero;
                this.reset();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                StreamWriter w = new StreamWriter("c:\\svn\\orion\\errorona" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Hour.ToString() + ".txt");
                w.WriteLine(ex.Message);
                w.Close();
                w = null;
            }
        }

        protected void cmbPessoal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}