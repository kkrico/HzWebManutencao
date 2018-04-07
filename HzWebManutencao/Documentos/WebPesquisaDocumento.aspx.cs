using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HzLibConnection.Data;
using HzLibConnection.Sql;
using HzClasses.Tabelas.Apoio;
using HzLibCorporativo.Funcional;
using HzClasses.Numeracao;
using Microsoft.Office.Interop.Word;
using HzLibManutencao;

namespace HzWebManutencao.Documentos
{
    public partial class WebPesquisaDocumento : System.Web.UI.Page
    {

        private void load()
        {
            try
            {
                System.Data.DataTable table = tblTipoNumeracao.Get((HzConexao)HttpContext.Current.Session["connection"], new ListCampos());
                HzlibWEB.Objetos.LoadCombo(ddllista, table, "cmpDcTipoNumeracao", "cmpCoTipoNumeracao", "cmpDcTipoNumeracao", "--- Selecione o tipo ---", true);
                table.Reset();
                table = tblFuncionario.Get((HzConexao)HttpContext.Current.Session["connection"], new ListCampos());
                HzlibWEB.Objetos.LoadCombo(ddlautor, table, "cmpNoFuncionario", "cmpCoFuncionario", "cmpNoFuncionario", "--- Selecione o autor ---", true);
            }
            catch
            {
            }
        }
        /// <summary>
        /// listar campos
        /// </summary>
        /// 
        private void Pesquisa()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            if (ddllista.SelectedItem.Value != "0")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpCoTipoNumeracao";
                lc.ValorCampo = ddllista.SelectedItem.Value;
                ls.Add(lc);
            }
            if (ddlautor.SelectedItem.Value != "0")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpCoFuncionario";
                lc.ValorCampo = ddlautor.SelectedItem.Value;
                ls.Add(lc);
            }

            if (txtnumero.Text != "")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpNuDocumento";
                lc.ValorCampo = txtnumero.Text;
                ls.Add(lc);
            }
            if (txtpesquisa.Text != "0")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpNuDocumento";
                lc.ValorCampo = txtpesquisa.Text;
           
            }
            else
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpNuAno";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = txtano.Text == "" ? DateTime.Now.Year.ToString() : txtano.Text;
                ls.Add(lc);
            }

            if (txtdescricao.Text != "")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpTxObservacoes";
                lc.TipoCampo = TipoCampo.Like;
                lc.Sinal = SinalPesquisa.Like;
                lc.Percent = TipoPercent.InicioFim;
                lc.ValorCampo = txtdescricao.Text.Replace("'", "");
                ls.Add(lc);

            }

        }

        /// <summary>
        /// Carregar os dados do documento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadData(string cmpconumeracaodocumento)
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpCoNumeracaoDocumento";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = cmpconumeracaodocumento;
            ls.Add(lc);
            System.Data.DataTable table = tblNumeracaoDocumento.Get((HzConexao)HttpContext.Current.Session["connection"], ls, 10000);

            if (table != null && table.Rows.Count > 0)
            {
                ddllista.SelectedIndex = HzlibWEB.Objetos.RetornaIndiceCombo(ddllista, long.Parse(table.Rows[0]["cmpCoTipoNumeracao"].ToString()));
                ddlautor.SelectedIndex = HzlibWEB.Objetos.RetornaIndiceCombo(ddlautor, long.Parse(table.Rows[0]["cmpCoFuncionario"].ToString()));
                txtano.Text = table.Rows[0]["cmpNuAno"].ToString();
                txtnumero.Text = table.Rows[0]["cmpNuDocumento"].ToString().Trim();
                this.enableFields();
            }
        }

        private void disableFields()
        {
            btnsalvar.Enabled = false;
            // RequiredFieldValidator.Enabled = false; (está contendo erros )
        }

        private void enableFields()
        {
            btnsalvar.Enabled = true;
            // RequiredFieldValidator= false;(está contendo erros )
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {


                txtano.Text = DateTime.Now.Year.ToString();
                this.load();
                if (Request.QueryString != null && Request.QueryString.Count > 0)
                {
                    ViewState["cmpCoNumeracaoDocumento"] = Request.QueryString[0];
                    LoadData(ViewState["cmpCoNumeracaoDocumento"].ToString()); //grvlista
                }


            }

        }

        protected void grvlista_SelectedIndexChanged(object sender, EventArgs e)
        {

            //grdReport.PageIndex = e.NewPageIndex;// verificar 
            //    this.search();
        }

        protected void btnpesquisar_Click(object sender, EventArgs e)
        {
            this.Pesquisa();

        }

        private string setDirDocName()
        {
            string retval = "";

            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpCoTipoNumeracao";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = ddllista.SelectedItem.Value;
            ls.Add(lc);

            using (System.Data.DataTable table = tblTipoNumeracao.Get((HzConexao)HttpContext.Current.Session["connection"], ls))
            {
                if (table != null && table.Rows.Count > 0)
                    retval = table.Rows[0]["cmpNoPasta"].ToString().Trim();
            }
            return retval;

        }

        protected void btnsalvar_Click(object sender, EventArgs e)
        {
           tblMaterial table = null;

            try
            {
                //table = new tblTipoNumeracao();
                //table.cmpCoContratante         = ViewState["cmpCoContratante"].ToString();
                //table.cmpDcTipoNumeracao     = ViewState["cmpDcTipoNumeracao"].ToString();
                




                if (table.GravarMaterial(Global.GetConnection()))
                {
                    //txtItMaterial.Enabled = false;
                    //PesquisaMaterial();
                    //LstMaterial.SelectedIndex = int.Parse(ViewState["cmpCoMaterial"].ToString());
                    Global.ShowMensager(Global.Title, "Material cadastrado.");
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, "Material já cadastrado.");
            }
        }

      

        }
           
            



    }
