using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibManutencao;
using System.Data;
using HzLibConnection.Sql;
using HzlibWEB;
using HzLibCorporativo.Funcional;
using HzLibConnection.Data;

namespace HzWebManutencao.Tabelas_Apoio
{
    public partial class WebCadastroArea : System.Web.UI.Page
    {
        /// <summary>
        /// Carrega dados da obra 
        /// </summary>
        private void load()
        {


            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();

            lc.NomeCampo = "cmpCoObra";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoContratante;
            ls.Add(lc);

            DataTable table = tblObraUsuario.Get(Global.GetConnection(), ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoUsuario);
            //Objetos.LoadCombo(cmbObra, table, "[cmpNome]", "[cmpMetros]", "[cmpTag]", true);
            Objetos.LoadCombo(cmbObra, table, "cmpNoObra", "cmpCoObraGrupoLista", "cmpNoObra", true);

            //ListCampo lc = new ListCampo();//verificar se so estancia esta classe ....
            //lc.NomeCampo = "cmpNome";
            //lc.TipoCampo = TipoCampo.String;
            //lc.ValorCampo = lc.ValorCampo = ((HzLibConnection.Sql.ListCampo)Session["cmpNome"]).ValorCampo; 

            //lc = new ListCampo();
            //lc.NomeCampo = "cmpNome";//verificar qual o campo certo 
            //lc.TipoCampo = TipoCampo.StringIN;
            //lc.Sinal = SinalPesquisa.IN;
            //lc.ValorCampo = "";// verificar quais os campo vem 

            //DataTable table = tblArea.RetornaArea(Global.GetConnection(),"1");//verificar qual argumento passar
            //Objetos.LoadCombo(cmbObra, table, "[cmpNome]", "[cmpMetros]", "[cmpTag]", true);//verificar
            //cmbObra_SelectedIndexChanged(cmbObra, EventArgs.Empty);
            //table.Reset();


            lc = new ListCampo();
            lc.NomeCampo = "cmpNome";
            lc.TipoCampo = TipoCampo.StringIN;
            lc.Sinal = SinalPesquisa.IN;
            lc.ValorCampo = "";// verificar como preencher 

            //table = tblArea.Get(Global.GetConnection());
            //Objetos.LoadCombo(cmbObra, table, "[cmpNome]", "[cmpMetros]", "[cmpTag]", true);

        }

        /// <summary>
        /// Carregar os dados da area 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void loadArea()
        {

            ListCampo lc = new ListCampo();
            lc.NomeCampo = "[cmpIdArea]";
            lc.TipoCampo = TipoCampo.String;
            lc.Sinal = SinalPesquisa.Igual;
            lc.ValorCampo = ViewState["[cmpIdArea]"].ToString();


        }
        /// <summary>
        /// Carrega as obras e quem tem permisão na mesma.
        /// </summary>
        private void loadObra()
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoLocal";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoLocal;
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "cmpCoUsuario";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoUsuario;
                ls.Add(lc);

                using (DataTable table = tblObraUsuario.Get(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbObra, table, "cmpNoObra", "cmpCoObraGrupoLista", "cmpNoObra", true);
                    //if (Session["login"] != null && ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista != null)
                    cmbObra.SelectedIndex = Objetos.RetornaIndiceCombo(cmbObra, int.Parse(((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista));
                    cmbObra_SelectedIndexChanged(cmbObra, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }
        private DataTable pesquisa()
        {

            ListCampo lc = new ListCampo();
            ListCampos lcs = new ListCampos();

            lc = new ListCampo();
            lc.NomeCampo = "cmpCoObra"; // verificar o que preencher 
            lc.TipoCampo = TipoCampo.Numero;
            lc.Percent = TipoPercent.InicioFim;
            lc.ValorCampo = cmbObra.SelectedValue;

            lcs.Add(lc);
            if (txtdescricaodaarea.Text != "")
            {

                lc = new ListCampo();
                lc.NomeCampo = "cmpNome"; // verificar o que preencher 
                lc.TipoCampo = TipoCampo.String;
                lc.Sinal = SinalPesquisa.Like;
                lc.Percent = TipoPercent.InicioFim;
                lc.ValorCampo = txtdescricaodaarea.Text.ToString();
                lcs.Add(lc);
            }
            //DataTable table = tblArea.Get(Global.GetConnection(), lcs);
            //return new DataTable();
            return null;
        }


        //tblArea Area = new tblArea();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                load();
                txtdescricaodaarea.Focus();
            }
        }

        protected void txtdescricaodaarea_TextChanged(object sender, EventArgs e)
        {
            //btnpesquisar_Click1(sender, e);
        }



        protected void btnpesquisar_Click1(object sender, EventArgs e)
        {
            //grvarea.DataSource = pesquisa();
            //grvarea.DataBind();
        }


        protected void btnnovo_Click(object sender, EventArgs e)
        {

            ModArea.Show();
        }

        //links da grid
        protected void lbtnincluir_Click(object sender, EventArgs e)
        {
            ////ViewState["cmpIdArea"] = 0;

            this.txtdescricaodaarea.Text = "";
            this.cmbObra.SelectedIndex = 0;
            this.cmbObra.Enabled = true;


        }
        //links da grid
        protected void lbtneditar_Click(object sender, EventArgs e)
        {
            //tblArea table = new tblArea(HzLibConnection.Data.HzConexao(),table);

            //table.cmpIdArea = ViewState["cmpIdArea"].ToString();
            //table.cmpIdCoPavimento = cmbObra.SelectedValue;
            //table.cmpNome = txtdescricaodaarea.Text.ToUpper().TrimEnd();
            //// verificar quais comandos ainda faltam .......
            //if (table.Salvar(Global.GetConnection()))
            //{
            //    lbtnexcluir_Click(sender, e);
            //    txtdescricaodaarea.Text = cmbObra.Text.TrimEnd();
            //    grvarea.DataSource = pesquisa();
            //    grvarea.DataBind();
            //}

        }
        //links da grid
        protected void lbtnexcluir_Click(object sender, EventArgs e)
        {
            this.txtdescricaodaarea.Text = "";
            this.cmbObra.SelectedIndex = 0;
            // ainda faltando comando .....

        }



        protected void btnsalvar_Click(object sender, EventArgs e)
        {

        }

        protected void btnpesquisar_Click(object sender, EventArgs e)
        {

            grvarea.DataSource = pesquisa();
            grvarea.DataBind();

        }

        protected void grvarea_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {

            grvarea.PageIndex = e.NewPageIndex;
            grvarea.DataSource = pesquisa();
            grvarea.DataBind();

        }

        protected void cmbObra_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void grvarea_RowDataBound1(object sender, GridViewRowEventArgs e)
        {


            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Cells[4].Visible = false;
                    break;
                case DataControlRowType.DataRow:
                    e.Row.Cells[4].Visible = false;
                    break;

                case DataControlRowType.Footer:
                    e.Row.Cells[4].Visible = false;
                    break;

            }
        }
    }
}



           
        

        


      

 

       
    








