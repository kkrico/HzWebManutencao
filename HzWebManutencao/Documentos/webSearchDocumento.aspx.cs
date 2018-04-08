using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using HzLibConnection.Sql;
using HzLibConnection.Data;
using HzClasses.Tabelas.Apoio;
using HzLibCorporativo.Funcional;
using HzClasses.Numeracao;

namespace HzWebManutencao.Documentos
{
    public partial class webSearchDocumento : System.Web.UI.Page
    {
        /// <summary>
        /// Carrega as combos auxiliares.
        /// </summary>
        /// <returns></returns>
        private void load()
        {
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
            RequiredFieldValidator1.Enabled = false;
        }

        private void enableFields()
        {
            btnSave.Enabled = true;
            RequiredFieldValidator1.Enabled = false; 
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
            string dir = "";
            try
            {
                dir = Server.MapPath(txtYear.Text + "/" + this.setDirDocName());

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                dir += "\\" + System.IO.Path.GetFileName(fileUpload.PostedFile.FileName);
                tblNumeracaoDocumento.AddFile((HzConexao)HttpContext.Current.Session["connection"], ViewState["cmpCoNumeracaoDocumento"].ToString(), dir);
                if (fileUpload.FileName != "")
                    fileUpload.PostedFile.SaveAs(dir);
                this.disableFields();
                btnSearch_Click(sender, e);
            }
            catch (Exception ex)
            {
                Label4.Text = ex.Message + " -- " + dir;
            }
        }
    }
}