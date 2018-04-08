using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibConnection.Sql;
using HzLibCorporativo.Funcional;
using HzLibGeneral.Util;
using HzClasses.Tabelas.Apoio;
using System.Data;
using HzLibConnection.Data;
using HzClasses.Numeracao;
using System.IO;
using System.Web.Services;

namespace HzWebManutencao.Documentos
{
    public partial class WebNovoDocumento : System.Web.UI.Page
    {
       private void load()
       {
           loadCadastrar();
            try
            {
                DataTable table = tblTipoNumeracao.Get((HzConexao)HttpContext.Current.Session["connection"], new ListCampos());
                HzlibWEB.Objetos.LoadCombo(cmbTipo, table, "cmpDcTipoNumeracao", "cmpCoTipoNumeracao", "cmpDcTipoNumeracao", "--- Selecione o tipo ---", true);
                table.Reset();
                table = tblFuncionario.Get((HzConexao)HttpContext.Current.Session["connection"], new ListCampos());
                HzlibWEB.Objetos.LoadCombo(cmbFuncionario, table, "cmpNoFuncionario", "cmpCoFuncionario", "cmpNoFuncionario", "--- Selecione o Funcionário ---", true);
            }
            catch
            {
            }
        }

        private void search()
        {
            try
            {
                ListCampos ls = new ListCampos();//classe do ronaldo 
                ListCampo lc = new ListCampo();
                if (cmbTipo.SelectedItem.Value != "0")
                {
                    lc = new ListCampo();
                    lc.NomeCampo = "cmpCoTipoNumeracao";
                    lc.ValorCampo = cmbTipo.SelectedItem.Value;
                    ls.Add(lc);
                }

                if (cmbFuncionario.SelectedItem.Value != "0")
                {
                    lc = new ListCampo();
                    lc.NomeCampo = "cmpCoFuncionario";
                    lc.ValorCampo = cmbFuncionario.SelectedItem.Value;
                    ls.Add(lc);
                }

                if (txtNumber.Text != "")
                {
                    lc = new ListCampo();
                    lc.NomeCampo = "cmpNuDocumento";
                    lc.ValorCampo = txtNumber.Text;
                    ls.Add(lc);

                }
                else
                {
                    lc = new ListCampo();
                    lc.NomeCampo = "cmpNuAno";
                    lc.TipoCampo = TipoCampo.Numero;
                    lc.ValorCampo = txtYear.Text == "" ? DateTime.Now.Year.ToString() : txtYear.Text;
                    ls.Add(lc);

                    if (txtDescription.Text != "")
                    {
                        lc = new ListCampo();
                        lc.NomeCampo = "cmpTxObservacoes";
                        lc.TipoCampo = TipoCampo.Like;
                        lc.Sinal = SinalPesquisa.Like;
                        lc.Percent = TipoPercent.InicioFim;
                        lc.ValorCampo = txtDescription.Text.Replace("'", "");
                        ls.Add(lc);
                    }
                }


                lblTip.Visible = true;
                DataTable table = tblNumeracaoDocumento.Get((HzConexao)HttpContext.Current.Session["connection"], ls, 10000);
                grdReport.DataSource = table;
                grdReport.DataBind();
            }
            catch
            {
            }
         
        }

        /// <summary>
        /// Carrega os dados do documento.
        /// </summary>
        /// <param name="cmpconumeracaodocumento"></param>
        private void loadData(string cmpconumeracaodocumento)
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoNumeracaoDocumento";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = cmpconumeracaodocumento;
                ls.Add(lc);
                DataTable table = tblNumeracaoDocumento.Get((HzConexao)HttpContext.Current.Session["connection"], ls, 10000);
                if (table != null && table.Rows.Count > 0)
                {
                    cmbTipo.SelectedIndex = HzlibWEB.Objetos.RetornaIndiceCombo(cmbTipo, long.Parse(table.Rows[0]["cmpCoTipoNumeracao"].ToString()));
                    cmbFuncionario.SelectedIndex = HzlibWEB.Objetos.RetornaIndiceCombo(cmbFuncionario, long.Parse(table.Rows[0]["cmpCoFuncionario"].ToString()));
                    txtYear.Text = table.Rows[0]["cmpNuAno"].ToString();
                    txtNumber.Text = table.Rows[0]["cmpNuDocumento"].ToString().Trim();
                    this.enableFields();
                }
            }
            catch
            {
            }
        }

        private void disableFields()
        {
            btnSave.Enabled = false;
            //RequiredFieldValidator1.Enabled = false;
        }

        private void enableFields()
        {
            btnSave.Enabled = true;
            //RequiredFieldValidator1.Enabled = false; 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    lblTip.Visible = false;
                    txtYear.Text = DateTime.Now.Year.ToString();
                    this.load();
                    if (Request.QueryString != null && Request.QueryString.Count > 0)
                    {
                        ViewState["cmpCoNumeracaoDocumento"] = Request.QueryString[0];
                        loadData(ViewState["cmpCoNumeracaoDocumento"].ToString());
                        search();
                    }
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }

        protected void grdReport_PageIndexChanging(object sender, GridViewPageEventArgs e)// dados da grid ....
        {
            grdReport.PageIndex = e.NewPageIndex;
            this.search();
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
                Label4.Text = ex.Message;
            }
        }//diretorio do DownloadFile

        protected void grdReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToLower().Trim())
                {
                    case "lnk":
                        Response.Redirect("webSearchDocumento.aspx?cmpNuDocumento=" + e.CommandArgument.ToString());
                        break;

                    case "cmd":
                        //int i = e.CommandArgument.ToString().LastIndexOf("\\");
                        //string filename = e.CommandArgument.ToString();
                        //if (i > -1)
                        //    filename = Server.MapPath(txtYear.Text + "/" + this.setDirDocName()) + "\\" + e.CommandArgument.ToString().Substring(i + 1, e.CommandArgument.ToString().Length - (i + 1));
                        DownloadFile(e.CommandArgument.ToString()); // ainda a parte 
                        break;
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.search();
        }

        private string setDirDocName()
        {
            string retval = "";
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoTipoNumeracao";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = cmbTipo.SelectedItem.Value;
                ls.Add(lc);

                using (DataTable table = tblTipoNumeracao.Get((HzConexao)HttpContext.Current.Session["connection"], ls))
                {
                    if (table != null && table.Rows.Count > 0)
                        retval = table.Rows[0]["cmpNoPasta"].ToString().Trim();
                }
                return retval;
            }
            catch (Exception ex)
            {
                Label4.Text = ex.Message;
                return "";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            //string dir = "";
            //try
            //{
            //    dir = Server.MapPath(txtYear.Text + "/" + this.setDirDocName());

            //    if (!Directory.Exists(dir))
            //        Directory.CreateDirectory(dir);
            //    dir += "\\" + System.IO.Path.GetFileName(fileUpload.PostedFile.FileName);
            //    tblNumeracaoDocumento.AddFile((HzConexao)HttpContext.Current.Session["connection"], ViewState["cmpCoNumeracaoDocumento"].ToString(), dir);
            //    if (fileUpload.FileName != "")
            //        fileUpload.PostedFile.SaveAs(dir);
            //    this.disableFields();
            //    btnSearch_Click(sender, e);
            //}
            //catch (Exception ex)
            //{
            //    Label4.Text = ex.Message + " -- " + dir;
            //}
        }


        private bool loadCadastrar()
        {
            try
            {
                DataTable table = tblTipoNumeracao.Get((HzConexao)HttpContext.Current.Session["connection"], new ListCampos());
                HzlibWEB.Objetos.LoadCombo(cmbTipoGerar, table, "cmpDcTipoNumeracao", "cmpCoTipoNumeracao", "cmpDcTipoNumeracao", true);
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

        protected void btnShow_OnClick(object sender, EventArgs e)
        {
            loadCadastrar();
            mp1.Show();
        }

        private bool LoadFornecedor()
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


        private void reset()
        {
            ViewState["cmpCoNumeracaoDocumento"] = "0";
            this.LoadFornecedor();
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
                tbl.cmpCoTipoNumeracao      = cmbTipoGerar.SelectedItem.Value;
                tbl.cmpTxObservacoes        = txtDescription.Text;
                //tbl.cmpNoFornecedor = cmbFornecedor.SelectedItem.Value == "0" ? txtFornecedor.Text : cmbFornecedor.SelectedItem.Text;
                tbl.cmpDcDocumento          = "";
                string numero               = tbl.Save((HzConexao)HttpContext.Current.Session["connection"], ref cmpconumeracaodocumento);
                lblNovoNumero.Text          = "O número gerado foi: " + numero;
                this.reset();
                Response.Redirect("WebNovoDocumento.aspx?cmpNuDocumento=" +numero, false);
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

        private void showFornecedor(bool b)
        {
            lblFornecedor.Visible = b;
            txtFornecedor.Visible = b;
            cmbFornecedor.Visible = b;
        }
    }
}
