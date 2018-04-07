using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibConnection.Sql;
using HzLibCorporativo.Funcional;
using HzLibManutencao;
using HzlibWEB;
using Apresentacao.Controles;
using System.Drawing;
using RelatoriosReportServer;
using System.Configuration;

namespace HzWebManutencao.Tabelas_Apoio
{
    public partial class webTAB_CadastroEquipamentoObra : System.Web.UI.Page
    {
        #region Funções

        /// <summary>
        /// Carrega as combos tipo de equipamento e capacidade equipamento
        /// </summary>
        private void load()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();

            lc.NomeCampo = "cmpCoContratante";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoContratante;
            ls.Add(lc);

            lc = new ListCampo();
            lc.NomeCampo = "cmpTpGrupoAtividade";
            lc.TipoCampo = TipoCampo.StringIN;
            lc.Sinal = SinalPesquisa.IN;
            lc.ValorCampo = "'T','E'";
            ls.Add(lc);

            DataTable table = tblTipoAtividade.RetornaTipoAtividade(Global.GetConnection(), ls);
            Objetos.LoadCombo(cmbTipoEquipamento, table, "cmpDcTipoAtividade", "cmpCoTipoAtividade", "cmpDcTipoAtividade", "--- Selecione ---", true);
            cmbTipoEquipamento_SelectedIndexChanged(cmbTipoEquipamento, EventArgs.Empty);
            Objetos.LoadCombo(cmbTipoEquip, table, "cmpDcTipoAtividade", "cmpCoTipoAtividade", "cmpDcTipoAtividade", "--- Selecione ---", true);
            
            table.Reset();
            ls = new ListCampos();
            lc = new ListCampo();
            lc.NomeCampo = "cmpCoContratante";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoContratante;
            ls.Add(lc);

            table = tblTipoCapacidade.Get(Global.GetConnection(), ls);
            Objetos.LoadCombo(cmbTipoCapacidade, table, "cmpDcTipoCapacidadeEquipamento", "cmpIdTipoCapacidadeEquipamento", "cmpDcTipoCapacidadeEquipamento", "--- Selecione ---", true);

        }

        /// <summary>
        /// Carrega combo de obras do banco HzCorporativo.
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
                    if (Session["login"] != null && ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista != null)
                        cmbObra.SelectedIndex = Objetos.RetornaIndiceCombo(cmbObra, int.Parse(((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista));
                    cmbObra_SelectedIndexChanged(cmbObra, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Carrega a combo da tabela tblObraPavimento do banco HzCorporativo.
        /// </summary>
        private void loadObraPavimento()
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoObraGrupoLista";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista;
                ls.Add(lc);

                DataTable table = tblObraPavimento.Get(Global.GetConnection(), ls);
                Objetos.LoadCombo(cmbObraPavimento, table, "cmpDcPavimento", "cmpCoObraPavimento", "cmpNuOrdenacao", "--- Selecione ---", true);
                Objetos.LoadCombo(cmbPavimento, table, "cmpDcPavimento", "cmpCoObraPavimento", "cmpNuOrdenacao", "--- Selecione ---", true);
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Carrega dados do equipamento da Obra
        /// </summary>
        private void loadEquipamentoObra()
        {
            ListCampos ls   = new ListCampos();
            ListCampo lc    = new ListCampo();

            lc.NomeCampo    = "cmpIdEquipamentoObra";
            lc.TipoCampo    = TipoCampo.Numero;
            lc.Sinal        = SinalPesquisa.Igual;
            lc.ValorCampo   = ViewState["cmpIdEquipamentoObra"].ToString();
            ls.Add(lc);

            using (DataTable table = tblEquipamentoObra.Get(Global.GetConnection(), ls))
            {
                this.TxtDescricao.Text                  = table.Rows[0]["CmpDcEquipamentoObra"].ToString();
                this.txtCodigoEquip.Text                = table.Rows[0]["CodEquipamento"].ToString();
                this.txtTpEquipamento.Text              = table.Rows[0]["cmpDcTipoEquipamento"].ToString();
                this.txtCapacidade.Text                 = table.Rows[0]["cmpQtCapacidadeEquipamento"].ToString();
                this.cmbTipoCapacidade.SelectedIndex    = Objetos.RetornaIndiceCombo(cmbTipoCapacidade, table.Rows[0]["cmpIdTipoCapacidadeEquipamento"].ToString() == "" ? 0 : long.Parse(table.Rows[0]["cmpIdTipoCapacidadeEquipamento"].ToString()));
                this.txtMarcaModelo.Text                = table.Rows[0]["cmpDcMarcaModeloEquipamento"].ToString();
                this.txtNumeroSerie.Text                = table.Rows[0]["cmpNuSerieEquipamento"].ToString();
                this.txtNumeroPatrimonio.Text           = table.Rows[0]["cmpNuPatrimonio"].ToString();
                this.txtObservacao.Text                 = table.Rows[0]["cmpDcObsEquipamento"].ToString();
                this.cmbPavimento.SelectedIndex         = Objetos.RetornaIndiceCombo(cmbPavimento, long.Parse(table.Rows[0]["cmpCoObraPavimento"].ToString()));
                this.txtLocalizacao.Text                = table.Rows[0]["cmpDcLocalEquipamento"].ToString();
                this.txtTag.Text                       = table.Rows[0]["cmpTagEquipamento"].ToString();
                ViewState["QRCODE"] =                   table.Rows[0]["cmpQCODE"].ToString();
                ViewState["cmpCoEquipamentoObra"]        = table.Rows[0]["cmpCoEquipamentoObra"].ToString();

                this.txtCodigoEquip.Enabled = false;
                this.txtTpEquipamento.Enabled = false;

                string qrCode = table.Rows[0]["cmpQCODE"].ToString();

                DataTable dtImagem = tblEquipamentoObra.SelectImagem(Global.GetConnection(), int.Parse(table.Rows[0]["cmpIdEquipamentoObra"].ToString()));

                if (dtImagem.Rows[0][0] !=null && string.IsNullOrEmpty(dtImagem.Rows[0][0].ToString())==false)
                {
                    QRCODE.clQRCODE qr = new QRCODE.clQRCODE();
                    string strQrcode= qr.GerarQRCODEStringImg(qrCode);
                    imgQrCode.ImageUrl=strQrcode;
                    btnImprimirQR.Visible = true;
                }
                else
                {
                    imgQrCode.ImageUrl="~/QRCODE/Orion.Jpeg";
                    btnGerarQRCode.Enabled = true;
                    btnImprimirQR.Visible = false;
                }
                
            }
        }

        /// <summary>
        /// Pesquisa equipamento obra.
        /// </summary>
        private DataTable pesquisa()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc;

            lc = new ListCampo();
            lc.NomeCampo    = "cmpCoObraGrupoLista";
            lc.TipoCampo    = TipoCampo.Numero;
            lc.Sinal        = SinalPesquisa.Igual;
            lc.ValorCampo   = cmbObra.SelectedValue;
            ls.Add(lc);

            if (txtDcEquipamentoPesq.Text != "")
            {
                lc = new ListCampo();
                lc.NomeCampo    = "CmpDcEquipamentoObra";
                lc.TipoCampo    = TipoCampo.Like;
                lc.Sinal        = SinalPesquisa.Like;
                lc.Percent      = TipoPercent.InicioFim;
                lc.ValorCampo   = txtDcEquipamentoPesq.Text.ToString();
                ls.Add(lc);
            }

            if (cmbTipoEquipamento.SelectedValue != "" && cmbTipoEquipamento.SelectedValue != "0")
            {
                lc = new ListCampo();
                lc.NomeCampo    = "cmpCoTipoAtividade";
                lc.TipoCampo    = TipoCampo.Numero;
                lc.Sinal        = SinalPesquisa.Igual;
                lc.ValorCampo   = cmbTipoEquipamento.SelectedValue;
                ls.Add(lc);
            }

            if (cmbObraPavimento.SelectedValue != "0")
            {
                lc = new ListCampo();
                lc.NomeCampo    = "cmpCoObraPavimento";
                lc.TipoCampo    = TipoCampo.Numero;
                lc.Sinal        = SinalPesquisa.Igual;
                lc.ValorCampo   = cmbObraPavimento.SelectedValue;
                ls.Add(lc);
            }
            return tblEquipamentoObra.Get(Global.GetConnection(), ls);
        }

        /// <summary>
        /// Pesquisa.
        /// </summary>
        private DataTable pesquisaEquipOrion()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc;

            if (txtDescEquip.Text != "")
            {
                lc = new ListCampo();
                lc.NomeCampo = "CmpDcEquipamento";
                lc.TipoCampo = TipoCampo.Like;
                lc.Sinal = SinalPesquisa.Like;
                lc.Percent = TipoPercent.InicioFim;
                lc.ValorCampo = txtDescEquip.Text.ToString();
                ls.Add(lc);
            }

            if (cmbTipoEquip.SelectedValue != "0")
            {
                lc = new ListCampo();
                lc.NomeCampo    = "cmpCoTipoAtividade";
                lc.TipoCampo    = TipoCampo.Numero;
                lc.Sinal        = SinalPesquisa.Igual;
                lc.ValorCampo   = cmbTipoEquip.SelectedValue;
                ls.Add(lc);
            }

            return tblEquipamento.Get(Global.GetConnection(), ls); 
        }

        #endregion

         protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                loadObra();
                load();

                TxtDescricao.Focus();
            }

        }

        protected void cmbTipoEquipamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPesquisa_Click(sender, e);
        }

        protected void cmbObraPavimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPesquisa_Click(sender, e);
        }

        protected void txtDcEquipamento_TextChanged(object sender, System.EventArgs e)
        {
            btnPesquisa_Click(sender, e);
        }

        protected void btnPesquisa_Click(object sender, System.EventArgs e)
        {
            grdPesquisa.DataSource = pesquisa();
            grdPesquisa.DataBind();
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
                    if (((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNuPerfil.ToString() == "3")// Perfil Cliente
                        e.Row.Cells[0].Visible = false;
                    e.Row.Cells[9].Visible = false;
                    e.Row.Cells[10].Visible = false;
                    e.Row.Cells[11].Visible = false;
                    e.Row.Cells[12].Visible = false;
                    break;
                case DataControlRowType.DataRow:
                    if (((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNuPerfil.ToString() == "3")// Perfil Cliente
                        e.Row.Cells[0].Visible = false;
                    e.Row.Cells[9].Visible = false;
                    e.Row.Cells[10].Visible = false;
                    e.Row.Cells[11].Visible = false;
                    e.Row.Cells[12].Visible = false;

                    if (string.IsNullOrEmpty(e.Row.Cells[12].Text))
                    {
                        e.Row.Cells[8].Text = "";
                        ImageButton lb = (ImageButton)e.Row.Cells[13].Controls[0];
                        lb.Visible = false;

                    }
                    else
                    {
                         LinkButton lb= (LinkButton) e.Row.Cells[8].Controls[1];
                         int quant = tblOS.RetornaQuantOSEquipamento(Global.GetConnection(), e.Row.Cells[12].Text);
                         if (quant > 0)
                         {
                             lb.Text = "OS(" + quant + ")";
                             ImageButton lbi = (ImageButton)e.Row.Cells[13].Controls[0];
                             lbi.CommandArgument = e.Row.Cells[12].Text;
                         }
                         else
                         {
                             ImageButton lbi = (ImageButton)e.Row.Cells[13].Controls[0];
                             lbi.Visible = false;
                             e.Row.Cells[8].Text = "";
                         }
                    }
                    break;
                case DataControlRowType.Footer:
                    if (((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNuPerfil.ToString() == "3")// Perfil Cliente
                        e.Row.Cells[0].Visible = false;
                    e.Row.Cells[9].Visible = false;
                    e.Row.Cells[10].Visible = false;
                    e.Row.Cells[11].Visible = false;
                    e.Row.Cells[12].Visible = false;
                    break;
            }
        }

        protected void grdPesquisa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string p;
            switch (e.CommandName.ToLower().Trim())
            {
                case "lnk":
                    ViewState.Add("IdEquipamentoObra", e.CommandArgument.ToString());
                        DataTable dt = tblOS.RetornaOSEquipamentoObra(Global.GetConnection(), e.CommandArgument.ToString());
                        
                        grvOS.DataSource = dt;
                        grvOS.DataBind();
                        ModalPopupExtender3.Show();

                    break;

                case "historico":
                    RelatoriosReportServer.clRelatorios rel = new RelatoriosReportServer.clRelatorios();
                    string nomeRel = rel.EquipamentoOS(e.CommandArgument.ToString());
                    this.Response.Write("<SCRIPT language=javascript>window.open('../" + nomeRel + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');</SCRIPT>");

                    break;
            }
        }

        protected void grdEquipOrion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdEquipOrion.PageIndex = e.NewPageIndex;
                grdEquipOrion.DataSource = pesquisaEquipOrion();
                grdEquipOrion.DataBind();

                this.ModalPopupExtender1.Show();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        protected void grdEquipOrion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Cells[3].Visible = false;
                    break;
                case DataControlRowType.DataRow:
                    e.Row.Cells[3].Visible = false;
                    break;
                case DataControlRowType.Footer:
                    e.Row.Cells[3].Visible = false;
                    break;
            }
            if (e.Row.RowIndex > -1)
            {
                e.Row.Attributes.Add("onMouseOver", "this.className = 'hover';");
                e.Row.Attributes.Add("onMouseOut", "this.className = 'normal';");
            }
        }
        protected void lnkSelecao_Click(object sender, EventArgs e)
        {
           //Seta a linha selecionada com a classe CSS para destacá-la e guarda seu índice em uma variável auxiliar (no viewstate da página)
           // grdEquipOrion.Rows[Int32.Parse((sender as Button).CommandArgument)].CssClass = "selected";
            
            LinkButton lnkSelecao = sender as LinkButton;

            GridViewRow gvrow = (GridViewRow)lnkSelecao.NamingContainer;

            ViewState["cmpIdEquipamento"] = lnkSelecao.CommandArgument.ToString();
            ViewState["cmpIdTipoEquipamento"] = gvrow.Cells[3].Text.ToString();
            ViewState["cmpCoEquipamentoObra"] = 0;
            TxtDescricao.Text               = Server.HtmlDecode(gvrow.Cells[1].Text.ToString());
            txtCodigoEquip.Text             = "";
            txtTpEquipamento.Text           = Server.HtmlDecode(gvrow.Cells[2].Text.ToString());
            txtCapacidade.Text              = "";
            cmbTipoCapacidade.SelectedIndex = 0;
            txtMarcaModelo.Text             = "";
            txtNumeroSerie.Text             = "";
            txtNumeroPatrimonio.Text        = "";
            txtObservacao.Text              = "";
            cmbPavimento.SelectedIndex      = 0;
            txtLocalizacao.Text             = "";
            txtTag.Text = "";

            this.txtCodigoEquip.Enabled = false;
            this.txtTpEquipamento.Enabled = false;
            btnGerarQRCode.Enabled = false;
            btnImprimirQR.Enabled = false;
            this.ModalPopupExtender1.Show();
            this.ModalPopupExtender2.Show();
        }

        protected void btnPesqEquipOrion_Click(object sender, EventArgs e)
        {
            grdEquipOrion.DataSource = pesquisaEquipOrion();
            grdEquipOrion.DataBind();

            this.ModalPopupExtender1.Show();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            TxtDescricao.Text = "";
            txtTpEquipamento.Text = "";
            txtCapacidade.Text = "";
            cmbTipoCapacidade.SelectedIndex = 0;
            txtMarcaModelo.Text = "";
            txtNumeroSerie.Text = "";
            txtNumeroPatrimonio.Text = "";
            txtObservacao.Text = "";
            cmbPavimento.SelectedIndex = 0;
            txtLocalizacao.Text = "";
            txtTag.Text = "";

            if (ViewState["edicao"].ToString() != "Sim")
            {
                this.ModalPopupExtender2.Hide();
                this.ModalPopupExtender1.Show();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            tblEquipamentoObra table = new tblEquipamentoObra();

            try
            {
                bool novoEquip = false;
                if (ViewState["cmpIdEquipamentoObra"] == null) { novoEquip = true; }
                table.cmpIdEquipamentoObra              = ViewState["cmpIdEquipamentoObra"].ToString();
                table.cmpIdEquipamento                  = ViewState["cmpIdEquipamento"].ToString();
                table.cmpCoGrupoAtividade               = ViewState["cmpIdTipoEquipamento"].ToString();
                table.CmpDcEquipamentoObra              = TxtDescricao.Text.ToUpper().TrimEnd();
                table.cmpCoObraGrupoLista               = cmbObra.SelectedValue;
                table.cmpCoObraPavimento                = cmbPavimento.SelectedValue;
                table.cmpDcLocalEquipamento             = txtLocalizacao.Text.TrimEnd();
                table.cmpIdTipoCapacidadeEquipamento    = cmbTipoCapacidade.SelectedValue;
                table.cmpQtCapacidadeEquipamento        = txtCapacidade.Text.TrimEnd();
                table.cmpDcMarcaModeloEquipamento       = txtMarcaModelo.Text.TrimEnd();
                table.cmpNuSerieEquipamento             = txtNumeroSerie.Text.TrimEnd();
                table.cmpNuPatrimonio                   = txtNumeroPatrimonio.Text.TrimEnd();
                table.cmpDcObsEquipamento               = txtObservacao.Text.TrimEnd();
                table.cmpTagEquipamento = txtTag.Text.Trim();
                table.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();
                if (ViewState["QRCODE"] != null)
                {
                    table.cmpQCODE = ViewState["QRCODE"].ToString();
                }
                using (DataTable tbl = table.Save(Global.GetConnection()))
                {
                    if (tbl != null && tbl.Rows.Count > 0)
                    {
                        txtCodigoEquip.Text = tbl.Rows[0][0].ToString();
                        btnPesquisa_Click(sender, e);
                        //CaixaMensagem.Mostar(Mensagem.Tipo.Sucesso, "Equipamento da obra cadastrado com sucesso!!!");
                        //ViewState["cmpCoEquipamentoObra"] = tbl.Rows[0][0];
                        Response.Write("<script>alert('Equipamento da obra cadastrado com sucesso!!!');</script>");
                        if (ViewState["QRCODE"] != null)
                        {
                            QRCODE.clQRCODE qr = new QRCODE.clQRCODE();

                            byte[] imagemBYTE = qr.GerarQRCODEStringBYTE(ViewState["QRCODE"].ToString());
                            table.UpdateImagem(Global.GetConnection(), imagemBYTE);
                            if (novoEquip == true)
                            {
                                this.ModalPopupExtender2.Show();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message.ToString() + "');</script>");
                //CaixaMensagem.Mostar(Mensagem.Tipo.Erro, ex.Message.ToString());
            }
        }

        protected void lnkIncluir_Click(object sender, EventArgs e)
        {
            ViewState["cmpIdEquipamentoObra"] = 0;
            txtDescEquip.Text = "";
            cmbTipoEquip.SelectedIndex = 0;

            grdEquipOrion.DataSource = pesquisaEquipOrion();
            grdEquipOrion.DataBind();

            ViewState["edicao"] = "Nao";

            TxtDescricao.Text = "";
            txtTpEquipamento.Text = "";
            txtCapacidade.Text = "";
            cmbTipoCapacidade.SelectedIndex = 0;
            txtMarcaModelo.Text = "";
            txtNumeroSerie.Text = "";
            txtNumeroPatrimonio.Text = "";
            txtObservacao.Text = "";
            cmbPavimento.SelectedIndex = 0;
            txtLocalizacao.Text = "";
            txtTag.Text = "";

            this.ModalPopupExtender1.Show();
        }

        protected void lnkEditar_Click(object sender, EventArgs e)
        {
            imgQrCode.ImageUrl="~/QRCODE/Orion.Jpeg";

            LinkButton lnkEditar = sender as LinkButton;

            GridViewRow gvrow = (GridViewRow)lnkEditar.NamingContainer;
            ViewState["cmpIdEquipamentoObra"]   = lnkEditar.CommandArgument.ToString();
            ViewState["cmpIdEquipamento"]       = gvrow.Cells[11].Text.ToString();
            ViewState["cmpIdTipoEquipamento"]   = gvrow.Cells[9].Text.ToString();
            ViewState["cmpIdTipoEquipamento"] = gvrow.Cells[9].Text.ToString();

            loadEquipamentoObra();

            ViewState["edicao"] = "Sim";

            this.ModalPopupExtender2.Show();
        }

        protected void lnkExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkExcluir = sender as LinkButton;

                GridViewRow gvrow = (GridViewRow)lnkExcluir.NamingContainer;
                ViewState["cmpIdEquipamentoObra"] = lnkExcluir.CommandArgument.ToString();

                tblEquipamentoObra table = new tblEquipamentoObra();
                table.cmpIdEquipamentoObra = ViewState["cmpIdEquipamentoObra"].ToString();
                table.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;

                if (table.Delete(Global.GetConnection()))
                {
                    grdPesquisa.DataSource = pesquisa();
                    grdPesquisa.DataBind();
                    CaixaMensagem.Mostar(Mensagem.Tipo.Sucesso, "Equipamento excluído com sucesso!!!");
                }
            }
            catch (Exception ex)
            {
                // CaixaMensagem.Mostar(Mensagem.Tipo.Erro, "Não é possível excluir o registro. Equipamento vinculado a uma obra!");
            }
        }

        protected void btnNovo_Click(object sender, System.EventArgs e)
        {
            lnkIncluir_Click(sender, e);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            this.ModalPopupExtender1.Hide();
        }

        protected void cmbObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista = cmbObra.SelectedValue;
            this.loadObraPavimento();
            btnPesquisa_Click(sender, e);
        }

        protected void grdPesquisa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdPesquisa_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void btnGerarQRCode_Click(object sender, EventArgs e)
        {
            //ModalPopupExtender2.Hide();

            imgQrCode.ImageUrl = "";// "~/QRCODE/Orion.Jpeg";
            imgQrCode.DataBind();

            string filepath = Server.MapPath("~/QRCODE");
        
            QRCODE.clQRCODE qr = new QRCODE.clQRCODE();

            string tag = ViewState["cmpCoEquipamentoObra"].ToString() + ";";

            string TAG = qr.GerarQRCODE(filepath, ViewState["cmpCoEquipamentoObra"].ToString(), tag,20);
            //imgQrCode.ImageUrl = "~/QRCODE/" + diretorio;

            string imagemSTR = qr.GerarQRCODEStringImg(TAG);
             imgQrCode.ImageUrl = imagemSTR;
           
             ViewState["QRCODE"] = TAG;
            btnImprimirQR.Enabled = true;
            btnImprimirQR.Visible = false;
            this.ModalPopupExtender2.Show();
        }

        protected void btnImprimirQR_Click(object sender, EventArgs e)
        {
            string site = ConfigurationManager.AppSettings["ImagensQRCODE"];

            clRelatorios clRel = new clRelatorios();
            string arquivo= clRel.QRCODE(ViewState["cmpCoEquipamentoObra"].ToString(),0);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('../" + arquivo + "','_blank')", true);

            this.ModalPopupExtender2.Show();
        }

        protected void grvOS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('../ATE/webATE_OS.aspx?id=" + grvOS.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString() + "','_blank')", true);
            }
        }

        protected void btnFecharOS_Click(object sender, EventArgs e)
        {
           
        }

        protected void btnRelatorioOS_Click(object sender, EventArgs e)
        {
  
        }

        protected void btnRelatorioOS_Click1(object sender, EventArgs e)
        {
            RelatoriosReportServer.clRelatorios rel = new RelatoriosReportServer.clRelatorios();
            string nomeRel = rel.EquipamentoOS(ViewState["IdEquipamentoObra"].ToString());
            this.Response.Write("<SCRIPT language=javascript>window.open('../" + nomeRel + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');</SCRIPT>");
        }

        protected void btnGerarQrcodes_Click(object sender, EventArgs e)
        {
            DataTable dtQr = pesquisa();
            string filepath = Server.MapPath("~/QRCODE");

            for (int i = 0; i < dtQr.Rows.Count; i++)
            {
                QRCODE.clQRCODE qr = new QRCODE.clQRCODE();

                string tag = dtQr.Rows[i]["cmpCoEquipamentoObra"].ToString() + ";";

                string TAG = qr.GerarQRCODE(filepath, dtQr.Rows[i]["cmpCoEquipamentoObra"].ToString(), tag, 20);

                tblEquipamentoObra table = new tblEquipamentoObra();
                
                byte[] imagemBYTE = qr.GerarQRCODEStringBYTE(TAG);
                table.UpdateImagem(Global.GetConnection(), imagemBYTE, int.Parse(dtQr.Rows[i]["cmpIdEquipamentoObra"].ToString()), TAG);

            }
        }

    
    }
}