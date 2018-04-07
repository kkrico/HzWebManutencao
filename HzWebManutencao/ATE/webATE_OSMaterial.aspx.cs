using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzlibWEB;
using HzLibConnection.Sql;
using HzLibManutencao;
using HzLibCorporativo.Funcional;
using Apresentacao.Controles;

namespace HzWebManutencao.ATE
{

    public partial class webATE_OSMaterial : System.Web.UI.Page
    {
        #region Funções
        /// <summary>
        /// Validar inclusão do material da O.S.
        /// </summary>
        private bool ValidarInclusaoMaterial()
        {
            bool ret = true;
            string msg = "";

            if (txtQtMaterial.Text == "")
                msg += "Quantidade do material em branco! <br />";
            else if (txtValorMaterial.Text =="")
                msg += "Valor do material em branco! <br />";
            else if (txtMaterialSelecionado.Text == "")
                msg += "Falta pesquisar material! <br />";
            if (msg != "")
            {
                CaixaMensagem.Mostar(Mensagem.Tipo.Erro, msg);
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Validar alteração do material da O.S.
        /// </summary>
        private bool ValidarAlteracaoMaterial()
        {
            bool ret = true;
            string msg = "";

            if (txtQuantiMaterial.Text == "")
                msg += "Quantidade do material em branco! <br />";
            else if (txtVlMat.Text == "")
                msg += "Valor do material em branco! <br />";
            if (msg != "")
            {
                CaixaMensagem.Mostar(Mensagem.Tipo.Erro, msg);
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Gravar total do material na Ordem de serviço.
        /// /// </summary>
        private void TotalizaMaterialOS()
        {
            tblOSMaterial table = new tblOSMaterial();
            table.cmpIdOs       = ViewState["cmpIdOs"].ToString();
            table.cmpNoUsuario  = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;
            table.TotalizaMaterialOS(Global.GetConnection());
        }
       
        private void LimpaCampos()
        {
            txtItemMaterial.Text        = "";
            txtQtMaterial.Text          = "";
            txtValorMaterial.Text       = "";
            txtMaterialSelecionado.Text = "";
        }

        /// <summary>
        /// Carrega campos da página.
        /// </summary>
        private void loadOS()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpIdOS";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = ViewState["cmpIdOs"].ToString();
            ls.Add(lc);

            using (DataTable table = tblOS.Get(Global.GetConnection(), ls))
            {
                lblAcao.Text = "Lista de Material da Ordem de Serviço Número ==> " + table.Rows[0]["cmpNuOS"].ToString();
                ViewState["cmpInEstoqueMaterial"] = table.Rows[0]["cmpInEstoqueMaterial"].ToString();
                TxtObra.Text = table.Rows[0]["cmpNoObra"].ToString();
                ViewState["cmpStOs"] = table.Rows[0]["cmpStOS"].ToString();
                TxtObra.ReadOnly = true;
            }
        }

        /// <summary>
        /// Carrega grid com os materiais cadastrados para a O.S.
        /// </summary>
        private void loadMaterial()
        { 
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpIdOS";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = ViewState["cmpIdOs"].ToString();
            ls.Add(lc);

            lc = new ListCampo();
            lc.NomeCampo = "cmpCoObraGrupoLista";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista;
            ls.Add(lc);

            using (DataTable table = tblOSMaterial.Get(Global.GetConnection(), ls))
            {
                if (table != null && table.Rows.Count > 0)
                {
                    lblTotalMaterial.Text = "Total Material ==> " + string.Format("{0:n2}", TotalMaterial(table));
                    ViewState["TotalGrid"] = TotalMaterial(table);
                    ViewState["cmpCoObraGrupoMaterial"] = table.Rows[0]["cmpCoObraGrupoMaterial"];
                    lblTotalMaterial.Visible = true;
                    grdOSMaterial.Visible = true;
                    grdOSMaterial.DataSource = table;
                    grdOSMaterial.DataBind();

                    txtQtMaterial.Text = "";
                }
                else
                {
                    grdOSMaterial.Visible = false;
                    lblTotalMaterial.Visible = false;
                }
            }
            
        }

        /// <summary>
        /// Totaliza valor do material.
        /// </summary>
        private decimal TotalMaterial(DataTable table)
        {
            decimal total = 0;
            foreach (DataRow r in table.Rows){   
                total += decimal.Parse(r["cmpVlSubTotal"].ToString());
            }
            
            return total;
        }

        /// <summary>
        /// Grava dados da OS na tblOS.
        /// </summary>
        private bool GravarOSMaterial()
        {
            bool retval = false;

            try
            {
                if (ValidarInclusaoMaterial())
                {
                    tblOSMaterial table = null;

                    table = new tblOSMaterial();
                    table.cmpIdOsMaterial           = ViewState["cmpIdOSMaterial"].ToString();
                    table.cmpIdOs                   = ViewState["cmpIdOs"].ToString();
                    table.cmpCoObraGrupoMaterial    = ViewState["cmpCoObraGrupoMaterial"].ToString();
                    table.cmpQtMaterial             = (txtQtMaterial.Text.Length < 4 ? "0." + txtQtMaterial.Text : txtQtMaterial.Text);
                    table.cmpVlMaterial             = txtValorMaterial.Text.Replace(",", ".");
                    table.cmpInControle             = chkControle.Checked == true ? "1" : "0";
                    table.cmpNoUsuario              = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;

                    if ((retval = table.GravarOSMaterial(Global.GetConnection())))
                    {
                        loadMaterial();
                        LimpaCampos();
                        txtItemMaterial.Focus();
                    }
                }

                return retval;
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
                return false; 
            }
        }

        /// <summary>
        /// Controla o botão de pesquisa dos materiais.
        /// </summary>
        private bool ControlaBotaoPesquisa(bool tipo)
        {   
            txtItemMaterial.Focus();
            LimpaCampos();
            btnPesqMaterial.Text = tipo ? "Nova Pesquisa" : "Pesquisar";
            return tipo;
        }

        /// <summary>
        /// Retorna os materiais pesquisados.
        /// </summary>
        private DataTable PesquisaMaterial()
        {
            return tblObraGrupoMaterial.PesquisaMaterialObra(Global.GetConnection(), ViewState["cmpCoObra"].ToString(), txtItemMaterial.Text);
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString != null && Request.QueryString.Count > 0)
                {
                    ViewState["cmpIdOs"]    = Request.QueryString["id"];
                    ViewState["cmpCoObra"]  = Request.QueryString["obr"];
                    this.loadOS();
                    this.loadMaterial();
                    txtItemMaterial.Focus();
                }
            }
        }

        protected void btnPesqMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtItemMaterial.Text.Length >= 3)
                {
                    ViewState["ItemMaterial"] = txtItemMaterial.Text;
                    DataTable tbl = PesquisaMaterial();
                    if (tbl != null && tbl.Rows.Count > 0)
                    {
                        grdMaterialOrion.DataSource = tbl;
                        grdMaterialOrion.DataBind();

                        ModalPopupExtender1.Show();
                    }
                    else
                    {
                        CaixaMensagem.Mostar(Mensagem.Tipo.Aviso, "Material não encontrado.");
                    }
                }
                else
                {
                    CaixaMensagem.Mostar(Mensagem.Tipo.Aviso, "Informe no mínimo 3 digitos para pesquisa!");
                    txtItemMaterial.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            GravarOSMaterial();
            TotalizaMaterialOS();
        }

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ATE/webATE_OS.aspx?id=" + ViewState["cmpIdOs"].ToString(), false);
        }

        protected void grdOSMaterial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdOSMaterial.PageIndex = e.NewPageIndex;
                loadMaterial();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        protected void lnkSelecao_Click(object sender, EventArgs e)
        {
            LinkButton lnkSelecao = sender as LinkButton;

            GridViewRow gvrow = (GridViewRow)lnkSelecao.NamingContainer;

            ViewState["cmpIdOSMaterial"]        = 0;
            ViewState["cmpCoObraGrupoMaterial"] = lnkSelecao.CommandArgument.ToString();
            ViewState["cmpDcMaterial"]          = Server.HtmlDecode(gvrow.Cells[1].Text.ToString());
            ViewState["cmpDcUnidade"]           = gvrow.Cells[2].Text.ToString();
            ViewState["QtContrato"]             = gvrow.Cells[3].Text.ToString();
            ViewState["EstoqueAtual"]           = gvrow.Cells[4].Text.ToString();
            ViewState["cmpVlPrecoUnitario"]     = gvrow.Cells[5].Text.ToString();
            txtValorMaterial.Text               = gvrow.Cells[5].Text.ToString();
            txtMaterialSelecionado.Text         = Server.HtmlDecode(gvrow.Cells[1].Text.ToString());

            ViewState["ItemMaterial"] = "";

            this.ModalPopupExtender2.Hide();
        }

        protected void btnRetornar_Click(object sender, EventArgs e)
        {
            ViewState["ItemMaterial"] = "";
            this.ModalPopupExtender2.Hide();
        }

        protected void grdOSMaterial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            tblOSMaterial table = null;

            try
            {
                switch (e.CommandName.ToLower().Trim())
                {
                    case "excluir":
                        table = new tblOSMaterial();
                        table.cmpIdOsMaterial   = e.CommandArgument.ToString();
                        table.cmpNoUsuario      = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;

                        if (table.ExcluirOSMaterial(Global.GetConnection()))
                        {
                            TotalizaMaterialOS();
                            loadMaterial();
                            LimpaCampos();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        protected void grdOSMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            TableCellCollection col = e.Row.Cells;

            if ((col.Count > 1) && (ViewState["cmpStOs"].ToString() != "N" && 
                                    ViewState["cmpStOs"].ToString() != "G" &&
                                    ViewState["cmpStOs"].ToString() != "R"&&
                                    ViewState["cmpStOs"].ToString() != "B"))
            {
                col[0].Visible = false;
                col[1].Visible = false;
                pnlMaterial.Enabled = false;
            }

            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    break;
                case DataControlRowType.DataRow:
                    if (e.Row.Cells[9].Text == "True")
                        e.Row.Cells[9].Text = "Sim";
                    else
                        e.Row.Cells[9].Text = "Não";
                    break;
                case DataControlRowType.Footer:
                    break;
            }

        }

        protected void grdOSMaterial_DataBound(object sender, EventArgs e)
        {

            //decimal Total = 0;
            
            //foreach (GridViewRow row in grdOSMaterial.Rows){
            //    Total += decimal.Parse(row.Cells[9].Text); 
            //}

            //lblTotalMaterial.Text = "Total Material ==> " + string.Format("{0:n2}",Total.ToString());

            //string.Format("{0:n2}", ViewState["TotalGrid"])

            //GridViewRow footer = grdOSMaterial.FooterRow;
            //TotalGrid TotalPagina = new TotalGrid();
            //TotalPagina.SomaPagina(Total);

            //footer.Cells[0].ColumnSpan = 7;
            //footer.Cells[0].HorizontalAlign = HorizontalAlign.Right;

            //for (int i = grdOSMaterial.Columns.Count - 1; i >= 1; i--)
            //{
            //    footer.Cells.RemoveAt(i);
            //}

            //footer.Cells.RemoveAt(7);
            //footer.Cells[0].Text = string.Format("Total ==> {0:n2}", Total);

            //footer.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            //footer.Cells[8].Text = "Subtotal => ";
            //footer.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            //footer.Cells[9].Text = string.Format("{0:n2}", Total);

            //if (grdOSMaterial.PageCount == (grdOSMaterial.PageIndex + 1))
            //{
            //    footer.Cells[8].Text = "Total => ";
            //    footer.Cells[9].Text = string.Format("{0:n2}", ViewState["TotalGrid"]);
            //}
        }

         protected void btnCancel1_Click(object sender, EventArgs e)
        {
            //RvfAlteraQuant.Enabled  = false;
            //RvfValorMat2.Enabled    = false;
            //RvfQuant.Enabled        = true;
        }
       
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            tblOSMaterial table = null;
            
            try
            {
                if (ValidarAlteracaoMaterial())
                {
                    table = new tblOSMaterial();
                    table.cmpIdOsMaterial           = ViewState["cmpIdOSMaterial"].ToString();
                    table.cmpCoObraGrupoMaterial    = ViewState["cmpCoObraGrupoMaterial"].ToString();
                    table.cmpQtMaterial             = (txtQuantiMaterial.Text.Length < 4 ? "0." + txtQuantiMaterial.Text : txtQuantiMaterial.Text);
                    table.cmpVlMaterial             = txtVlMat.Text.Replace(",", ".");
                    table.cmpInControle             = chkControle2.Checked == true ? "1" : "0";
                    table.cmpNoUsuario              = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();

                    if ((table.AlterarOSMaterial(Global.GetConnection())))
                    {
                        loadMaterial();
                        TotalizaMaterialOS();
                        this.ModalPopupExtender2.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        protected void lnkEditar_Click(object sender, EventArgs e)
        {            
            LinkButton lnkEditar = sender as LinkButton;
            GridViewRow gvrow = (GridViewRow)lnkEditar.NamingContainer;

            ViewState["cmpIdOSMaterial"]    = lnkEditar.CommandArgument.ToString();
            lblDadosMaterial.Text           = gvrow.Cells[2].Text + " - " + gvrow.Cells[3].Text;
            txtQuantiMaterial.Text          = gvrow.Cells[8].Text;
            txtVlMat.Text                   = gvrow.Cells[7].Text;
            chkControle2.Checked            = gvrow.Cells[10].Text == "Sim" ? true : false;

            this.ModalPopupExtender2.Show();
        }

        protected void grdMaterialOrion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string DcMaterial;

            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    break;
                case DataControlRowType.DataRow:
                    DcMaterial = DataBinder.Eval(e.Row.DataItem, "cmpDcMaterial").ToString();
                    if(DcMaterial.Length > 60)
                    {
                        e.Row.Cells[1].Text = Server.HtmlDecode(DataBinder.Eval(e.Row.DataItem, "cmpDcMaterial").ToString().Substring(0,60)); 
                    }
                    else                          
                    {
                        e.Row.Cells[1].Text = Server.HtmlDecode(DataBinder.Eval(e.Row.DataItem, "cmpDcMaterial").ToString());                           
                    }
                    e.Row.Cells[1].ToolTip = Server.HtmlDecode(DataBinder.Eval(e.Row.DataItem, "cmpDcMaterial").ToString()); 
                    break;
                case DataControlRowType.Footer:
                    break;
            }
        }

        protected void grdMaterialOrion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdMaterialOrion.PageIndex = e.NewPageIndex;
                grdMaterialOrion.DataSource = PesquisaMaterial();
                grdMaterialOrion.DataBind();
                ModalPopupExtender1.Show();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }

        }

        #endregion

    }
}