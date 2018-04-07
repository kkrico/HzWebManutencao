using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

using System.Web.SessionState;
using System.Collections;

using HzLibConnection.Sql;
using HzLibCorporativo.Funcional;
using HzLibManutencao;
using HzLibGeneral.Util;
using HzlibWEB;

using Apresentacao.Controles;

namespace HzWebManutencao.ATE
{
    public partial class webATE_OSArquivoProtocoladoCEF : System.Web.UI.Page
    {
        #region variables
        static string PastaArqUpLoad = string.Empty;

        #endregion

        #region Funções
        /// <summary>
        /// Carrega combo de obras do banco HzCorporativo.
        /// </summary>
        private string loadObra(string NuObra)
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpNuObra";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = NuObra;
            ls.Add(lc);

            DataTable table = tblObraGrupoLista.Get(Global.GetConnection(), ls);

            return table.Rows[0]["cmpCoObraGrupoLista"].ToString();
        }

        /// <summary>
        /// Carrega listbox com os nomes dos arquivos.
        /// </summary>
        private void loadArquivos()
        {
            try
            {
                var nomes = new List<String>();
                lstArquivo.Items.Clear();
                int i;
                foreach (var item in Directory.GetFiles(PastaArqUpLoad, "*.*"))
                {
                    i = item.LastIndexOf('\\');
                    nomes.Add(item.Substring(i + 1, (item.Length - i - 1)));
                }

                if (nomes != null && nomes.Count > 0)
                {
                    this.lstArquivo.DataSource = nomes;
                    this.lstArquivo.DataBind();
                    this.lstArquivo.SelectedIndex = 0;
                    CarregaDadosArquivo();
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                CaixaMensagem.Mostar(Mensagem.Tipo.Erro, ex.Message.ToString());
            }
            catch (System.IO.IOException ex)
            {
                CaixaMensagem.Mostar(Mensagem.Tipo.Erro, ex.Message.ToString());
            }
        }

        /// <summary>
        /// Exibe dados dos arquivos enviados.
        /// </summary>
        private void CarregaDadosArquivo()
        {
            //abre arquivo texto usando a declaração using
            using (StreamReader reader = new StreamReader(Path.Combine(PastaArqUpLoad, lstArquivo.SelectedValue)))
            {
                string line;
                int countline3 = 0;

                limpaCampos();

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Substring(0,2) == "01")
                    {
                        txtDataGeracao.Text     = line.Substring(14 - 2, 2) + "/" + line.Substring(14 - 4, 2) + "/" + line.Substring(14 - 8, 4);
                        txtHoraGeracao.Text     = line.Substring(14, 2) + ":" + line.Substring(16, 2) + ":" + line.Substring(18, 2);
                        txtNuChamandoCEF.Text   = line.Substring(20, 20).TrimStart('0');
                        txtObra.Text            = line.Substring(86, 40);
                        txtCodFornecedor.Text   = line.Substring(2, 4);
                        txtCodBanco.Text        = line.Substring(43,3);
                        txtCodUnidade.Text      = line.Substring(46,10).TrimStart('0');
                        txtEndereco.Text        = line.Substring(126, 60);
                        txtCidade.Text          = line.Substring(186, 40);
                        txtUF.Text              = line.Substring(226, 2);
                        txtAtividade.Text       = line.Substring(40, 3);
                    }

                    else if (line.Substring(0, 2) == "02")
                    {
                        txtContato.Text = line.Substring(122, 40);
                        txtFone.Text = line.Substring(162, 20);
                        txtServico.Text= line.Substring(182, 50);
                        txtPrioridade.Text = line.Substring(232, 1);
                        txtDataLimite.Text = line.Substring(239, 2) + "/" + line.Substring(237, 2) + "/" + line.Substring(233, 4) + " " + line.Substring(241, 2) + ":" + line.Substring(243, 2) + ":" + line.Substring(245, 2);
                    }

                    else if (line.Substring(0, 2) == "03")
                    {
                        if (countline3 > 0)
                            txtDescricao.Text = txtDescricao.Text + line.Substring(2, 248);
                        else
                            txtDescricao.Text = line.Substring(2, 248);
                        countline3 += 1;
                    }
                }
                reader.Close();
            }

        }

       /// <summary>
        /// limpa campos dos arquivos enviados.
        /// </summary>
        private void limpaCampos()
        {
            txtDataGeracao.Text     = "";
            txtHoraGeracao.Text     = "";
            txtNuChamandoCEF.Text   = "";
            txtObra.Text            = "";
            txtAtividade.Text       = "";
            txtCodFornecedor.Text   = "";
            txtCodBanco.Text        = "";
            txtCodUnidade.Text      = "";
            txtEndereco.Text        = "";
            txtCidade.Text          = "";
            txtUF.Text              = "";

            txtContato.Text         = "";
            txtFone.Text            = "";
            txtServico.Text         = "";
            txtPrioridade.Text      = "";
            txtDataLimite.Text      = "";

            txtDescricao.Text       = "";

        }

        /// <summary>
        /// Grava dados da OS na tblOS.
        /// </summary>
        private bool GravarOS()
        {
            bool retval = true;

            try
            {
                string cmpCoObraGrupoLista = loadObra(txtCodUnidade.Text);

                if (cmpCoObraGrupoLista == "")
                {
                    CaixaMensagem.Mostar(Mensagem.Tipo.Erro, "Obra não cadastrada!");
                    retval = false;
                }
                else
                {
                    DataTable tb = tblOS.VerificaNumeroDemanda(Global.GetConnection(), cmpCoObraGrupoLista, txtNuChamandoCEF.Text);
                    if (tb.Rows[0]["Mensagem"].ToString() != "")
                    {
                        CaixaMensagem.Mostar(Mensagem.Tipo.Erro, tb.Rows[0]["Mensagem"].ToString());
                        retval = false;
                    }
                }

                if (retval)
                {
                    Session["cmpCoObraGrupoLista"]  = cmpCoObraGrupoLista;
                    Session["txtNuDemanda"]         = txtNuChamandoCEF.Text;
                    Session["txtLocal"]             = txtEndereco.Text;
                    Session["txtSolicitante"]       = txtContato.Text;
                    Session["txtTelefone"]          = txtFone.Text;

                    Session["txtObservacoes"]       = txtDescricao.Text;

                    Response.Redirect("~/ATE/webATE_OS.aspx?OSCef=1", false);
                }
                return retval;
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
                return false;
            }
        }
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PastaArqUpLoad = Global.PathArquivos(@"C:\HORIZON-MANUTENCAO\UpLoad\Orion Sao Paulo Arquivos Caixa\Recebido\");
                loadArquivos();
            }
        }

        protected void lstArquivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaDadosArquivo();
        }

        protected void btnGerarOS_Click(object sender, EventArgs e)
        {
            GravarOS();
        }

        protected void upload_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            if (pageBannerUpload.HasFile)
            {
                int i = e.FileName.LastIndexOf('\\');
                string NoArquivo        = (e.FileName.Substring(i + 1, (e.FileName.Length - i - 1)));
                Session["NomeArquivo"]  = NoArquivo.Replace(".txt", "_1.txt");

                pageBannerUpload.SaveAs(PastaArqUpLoad + Path.GetFileName(e.FileName.Replace(".txt", "_1.txt")));
            }
        }

        protected void btnVerArquivos_Click(object sender, EventArgs e)
        {
            loadArquivos();
        }

        protected void btnRemoverArquivos_Click(object sender, EventArgs e)
        {
            while (lstArquivo.SelectedIndex != -1)
            {
                ListItem mySelectedItem = (from ListItem li in lstArquivo.Items where li.Selected == true select li).First();
                File.Delete(PastaArqUpLoad+mySelectedItem);
                lstArquivo.Items.Remove(mySelectedItem);
            };
        }

        #endregion

    }
}