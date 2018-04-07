using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibConnection.Sql;
using HzLibCorporativo.Funcional;
using HzLibManutencao;
using HzlibWEB;
using Apresentacao.Controles;


namespace HzWebManutencao.Preventiva
{
    public partial class webPRE_Equipamento : System.Web.UI.Page
    {
        #region Functions
        /// <summary>
        /// Pesquisa equipamento obra.
        /// </summary>
        private DataTable pesquisa()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpIdPreventivaConfirmacao";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = ViewState["IdPreventivaConfirmacao"].ToString();
            ls.Add(lc);

            DataTable dt;

            using (DataTable table = tblPreventivaConfirmacao.Get(Global.GetConnection(), ls))
            {
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Rows[0]["cmpNuOS"].ToString() != "")
                    {
                        lblNuOS.Text    = table.Rows[0]["cmpNuOS"].ToString();
                        lblDcSitua.Text = table.Rows[0]["cmpStOSDescricao"].ToString();

                        ViewState["cmpNuOS"] = table.Rows[0]["cmpNuOS"].ToString();
                        ViewState["cmpStOS"] = table.Rows[0]["cmpStOS"].ToString();
                    }
                }  
            }

            dt = tblManutencaoEquipamento.RetornaEquipamentoCorretiva(Global.GetConnection(), ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista, ViewState["cmpCoTipoAtividade"].ToString(), ViewState["IdPreventivaConfirmacao"].ToString());

            DataTable dt2 = RemoveDuplicateRows(dt, "cmpIdEquipamentoObra");

            lblTipoEqui.Text = dt.Rows[0]["cmpDcTipoAtividade"].ToString();

            return dt2;
        }

        public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            foreach (DataRow dtRow in dTable.Rows)
            {
                if (hTable.Contains(dtRow[colName]))
                    duplicateList.Add(dtRow);
                else
                    hTable.Add(dtRow[colName], string.Empty);
            }
            foreach (DataRow dtRow in duplicateList)
                dTable.Rows.Remove(dtRow);
            return dTable;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                NameValueCollection n = Request.QueryString;

                if (n.HasKeys())
                {
                    switch (n.GetKey(0))
                    {
                        case "CoAtividade":
                            ViewState["cmpCoTipoAtividade"] = n.Get(0);
                            ViewState["cmpNuPreventivaAgenda"] = n.Get(1);
                            lblAcao.Text = "Equipamentos da Obra - " + n.Get(2);
                            ViewState["IdPreventivaConfirmacao"] = n.Get(3);
                            lblNuPre.Text = n.Get(1);

                            grdPesquisa.DataSource = pesquisa();
                            grdPesquisa.DataBind();

                            break;
                    }
                }
            }
        }

        protected void lnkSelecionar_Click(object sender, EventArgs e)
        {
            //Seta a linha selecionada com a classe CSS para destacá-la e guarda seu índice em uma variável auxiliar (no viewstate da página)
            // grdEquipOrion.Rows[Int32.Parse((sender as Button).CommandArgument)].CssClass = "selected";

            LinkButton lnkSelecionar = sender as LinkButton;

            GridViewRow gvrow = (GridViewRow)lnkSelecionar.NamingContainer;

            ViewState["cmpIdEquipamentoObra"] = lnkSelecionar.CommandArgument.ToString();

            TxtDescricao.Text       = Server.HtmlDecode(gvrow.Cells[1].Text.ToString());
            txtCodigoEquip.Text     = Server.HtmlDecode(gvrow.Cells[2].Text.ToString());
            txtTpEquipamento.Text   = Server.HtmlDecode(gvrow.Cells[3].Text.ToString());
            txtMarcaModelo.Text     = Server.HtmlDecode(gvrow.Cells[4].Text.ToString());
            txtNumeroSerie.Text     = Server.HtmlDecode(gvrow.Cells[5].Text.ToString());
            txtPavimento.Text       = Server.HtmlDecode(gvrow.Cells[6].Text.ToString());
            txtLocalizacao.Text     = Server.HtmlDecode(gvrow.Cells[7].Text.ToString());

            TxtDescricao.Enabled        = false;
            txtCodigoEquip.Enabled      = false;
            txtTpEquipamento.Enabled    = false;
            txtMarcaModelo.Enabled      = false;
            txtNumeroSerie.Enabled      = false;
            txtNumeroSerie.Enabled      = false;
            txtPavimento.Enabled        = false;
            txtLocalizacao.Enabled      = false;

            if (lnkSelecionar.Text == "Selecionado")
            {
                ViewState["cmpIdManutencaoEquipamento"] = gvrow.Cells[8].Text.ToString(); ;
                txtDtManutencao.Text = gvrow.Cells[9].Text.ToString();
                txtDetalhamento.Text = Server.HtmlDecode(gvrow.Cells[10].Text.ToString());
            }
            else
            {
                ViewState["cmpIdManutencaoEquipamento"] = 0;
                txtDtManutencao.Text = "";
                txtDetalhamento.Text = "";
            }

            this.ModalPopupExtender1.Show();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtDtManutencao.Text = "";
            txtDetalhamento.Text = "";

            this.ModalPopupExtender1.Hide();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                tblManutencaoEquipamento table      = new tblManutencaoEquipamento();
                table.cmpIdManutencaoEquipamento    = ViewState["cmpIdManutencaoEquipamento"].ToString();
                table.cmpIdEquipamentoObra          = ViewState["cmpIdEquipamentoObra"].ToString();
                table.cmpDtManutencaoEquipamento    = txtDtManutencao.Text;
                table.cmpDcDetalhamentoManutencao   = txtDetalhamento.Text;
                table.cmpIdPreventivaConfirmacao    = ViewState["IdPreventivaConfirmacao"].ToString();
                table.cmpNoUsuario                  = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();

                using (DataTable tblManut = table.Save(Global.GetConnection()))
                {
                    if (tblManut != null && tblManut.Rows.Count > 0)
                    {
                        if (ViewState["cmpIdManutencaoEquipamento"].ToString() == "0")
                        {
                            tblPreventivaConfirmacaoOS tbl = new tblPreventivaConfirmacaoOS();
                            tbl.cmpIdManutencaoEquipamento = tblManut.Rows[0]["IdManutencaoEquipamento"].ToString();
                            tbl.cmpIdPreventivaConfirmacao = ViewState["IdPreventivaConfirmacao"].ToString();
                            if (tbl.Save(Global.GetConnection()))
                            {
                                CaixaMensagem.Mostar(Mensagem.Tipo.Sucesso, "Manutenção do equipamento incluido com sucesso!!!");
                            }
                        }
                        else
                        {
                            CaixaMensagem.Mostar(Mensagem.Tipo.Sucesso, "Manutenção do equipamento alterado com sucesso!!!");
                        }
                    }
                }
                grdPesquisa.DataSource = pesquisa();
                grdPesquisa.DataBind();
            }
            catch (Exception ex)
            {
                CaixaMensagem.Mostar(Mensagem.Tipo.Erro, ex.Message.ToString());
            }
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                tblManutencaoEquipamento table      = new tblManutencaoEquipamento();
                table.cmpIdManutencaoEquipamento    = ViewState["cmpIdManutencaoEquipamento"].ToString();
                table.cmpNoUsuario                  = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();

                if (table.ExcluirManutencaoEquipamento(Global.GetConnection()))
                {
                    CaixaMensagem.Mostar(Mensagem.Tipo.Sucesso, "Registro excluido com sucesso!!!");
                    grdPesquisa.DataSource = pesquisa();
                    grdPesquisa.DataBind();
                }
            }
            catch (Exception ex)
            {
                //CaixaMensagem.Mostar(Mensagem.Tipo.Erro, ex.Message.ToString());
            }
        }

        protected void grdPesquisa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdPesquisa.PageIndex = e.NewPageIndex;
                grdPesquisa.DataSource = pesquisa();
                grdPesquisa.DataBind();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        protected void grdPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[8].Visible = false;
                    e.Row.Cells[9].Visible = false;
                    e.Row.Cells[10].Visible = false;
                    break;
                case DataControlRowType.DataRow:
                    if (e.Row.Cells[8].Text != "&nbsp;")
                    {
                        LinkButton lnkSelecionar = ((LinkButton)e.Row.Cells[0].FindControl("lnkSelecionar"));
                        lnkSelecionar.Text = "Selecionado";
                        lnkSelecionar.ForeColor = System.Drawing.Color.Red;
                    }
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[8].Visible = false;
                    e.Row.Cells[9].Visible = false;
                    e.Row.Cells[10].Visible = false;
                    break;
                case DataControlRowType.Footer:
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[8].Visible = false;
                    e.Row.Cells[9].Visible = false;
                    e.Row.Cells[10].Visible = false;
                    break;
            }
        }

        protected void btnVincularOS_Click(object sender, EventArgs e)
        {
            ListCampos ls   = new ListCampos();
            ListCampo lc    = new ListCampo();
            lc.NomeCampo    = "cmpIdPreventivaConfirmacao";
            lc.TipoCampo    = TipoCampo.Numero;
            lc.ValorCampo   = ViewState["IdPreventivaConfirmacao"].ToString();
            ls.Add(lc);
   
            using (DataTable table = tblPreventivaConfirmacaoOS.Get(Global.GetConnection(), ls))
            {
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Rows[0]["cmpIdOs"].ToString() != "")
                        Response.Redirect("~/ATE/webATE_OS.aspx?id=" + table.Rows[0]["cmpIdOs"].ToString(), false);
                    else
                        Response.Redirect("~/ATE/webATE_OS.aspx?IdPreventivaConfirmacao=" + ViewState["IdPreventivaConfirmacao"].ToString() + "&NuPreventiva=" + ViewState["cmpNuPreventivaAgenda"].ToString(), false);
                }
            }
        }


    }
}