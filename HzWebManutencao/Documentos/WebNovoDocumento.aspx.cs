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

namespace HzWebManutencao.Documentos
{
    public partial class WebNovoDocumento : System.Web.UI.Page
    {
        /// <summary>
        /// Carregar combos 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        private void LoadCarregar()
        {


            DataTable table = tblFuncionario.Get((HzConexao)HttpContext.Current.Session["connection"], new ListCampos());
            HzlibWEB.Objetos.LoadCombo(ddlautor, table, "cmpNoFuncionario", "cmpCoFuncionario", "cmpNoFuncionario", "---Selecione o Funcionario---", true);
            table.Reset();
            ddlautor.Text = "";

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


            table = tblTipoNumeracao.Get((HzConexao)HttpContext.Current.Session["connection"], new ListCampos());
            HzlibWEB.Objetos.LoadCombo(ddllista, table, "cmpDcTipoNumeracao", "cmpCoTipoNumeracao", "cmpDcTipoNumeracao", "---Selecione o Tipo---", true);
            ddllista.Text = "";


            lc = new ListCampo();
            lc.NomeCampo = "cmpInAtivo";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = "1";
            ls.Add(lc);


        }

        /// <summary>
        /// listando os campos
        /// </summary>
        private void Pesquisar()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            
            //tomada de decisão

            if (ddlautor.SelectedItem.Value !="0")
            {
                lc = new ListCampo();//sempre estanciar
                lc.NomeCampo = "cmpCoFuncionario";
                lc.ValorCampo = ddlautor.SelectedItem.Value;
                ls.Add(lc);
            }

            if (ddllista.SelectedItem.Value !="0")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpCoTipoNumeracao";
                lc.ValorCampo = ddllista.SelectedItem.Value;
                ls.Add(lc);
            }

            if (txtpesquisa.Text != "")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpNuDocumento";
                lc.ValorCampo = txtpesquisa.Text;
                ls.Add(lc);

            }

            if (txtnumero.Text !="")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpCoDocumento";
                lc.ValorCampo = txtnumero.Text;
                ls.Add(lc);
            }

            else
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpNuAno";
                lc.ValorCampo = txtano.Text == "" ? DateTime.Now.ToString() : txtano.Text;
                ls.Add(lc);

            }


            //lblTip.Visible = true;
            DataTable table = tblNumeracaoDocumento.Get((HzConexao)HttpContext.Current.Session["connection"], ls, 10000);
            grvlista.DataSource = table;
            grvlista.DataBind();
           
        }
                



        /// <summary>
        /// Retorna dados do documento
        /// Combo da lista carrega a grid
        /// </summary>
        /// <param name="cmpconumeracaodocumento"></param>
        private void LoadDados(string cmpconumeracaodocumento)
        {

            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpCoNumeracaoDocumento";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = cmpconumeracaodocumento;
            ls.Add(lc);
            DataTable table = tblNumeracaoDocumento.Get((HzConexao)HttpContext.Current.Session["connection"], ls, 10000);

            if (table != null  && table.Rows.Count>0)
            {
                ddlautor.SelectedIndex = HzlibWEB.Objetos.RetornaIndiceCombo(ddlautor, long.Parse(table.Rows[0]["cmpCoFuncionario"].ToString()));
                ddllista.SelectedIndex = HzlibWEB.Objetos.RetornaIndiceCombo(ddllista, long.Parse(table.Rows[0]["cmpCoTipoNumeracao"].ToString()));
                txtano.Text = table.Rows[0]["cmpNuAno"].ToString();
                txtnumero.Text = table.Rows[0]["cmpNuDocumento"].ToString().Trim();
                this.enableFields();
            }

        }


        /// <summary>
        /// Evento dos botões
        /// </summary>
        private void disableFields()
        {

            btnsalvar.Enabled = false; 
        }

        private void enableFields()
        {
            btnsalvar.Enabled = true;
        }
                






        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadCarregar();

                if (!Page.IsPostBack)
                {
                    lbllista.Visible = false;
                    txtano.Text = DateTime.Now.ToString();
                    this.LoadCarregar();

                    if (Request.QueryString != null && Request.QueryString.Count > 0)
                    {
                        ViewState["cmpCoNumeracaoDocumento"] = Request.QueryString[0];
                        LoadDados(ViewState["cmpCoNumeracaoDocumento"].ToString()); 
                    }

                }
            }

        }

        protected void grvlista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
             grvlista.PageIndex = e.NewPageIndex;
             this.Pesquisar();
        }

        protected void ddllista_SelectedIndexChanged(object sender, EventArgs e)
        {
            Pesquisar();
        }

        protected void btnpesquisar_Click(object sender, EventArgs e)
        {
            this.Pesquisar();
        }
        }

       
    }



        