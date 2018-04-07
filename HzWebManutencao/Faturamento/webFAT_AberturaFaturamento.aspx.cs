using System;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using HzLibCorporativo.Funcional;
using HzLibCorporativo.Faturamento;
using HzLibCorporativo.Geral;

using HzLibConnection.Sql;
using HzlibWEB;
using HzLibManutencao;
using Apresentacao.Controles;
using HzLibCorporativo.Config;

namespace HzWebManutencao.Faturamento
{
    public partial class webFAT_AberturaFaturamento : System.Web.UI.Page
    {
        #region functions
        /// <summary>
        /// Carrega as combos.
        /// </summary>
        private void load()
        {
            try
            {
                using (DataTable tbl = tblDataHoraServidor.Get(Global.GetConnection()))
                {
                    Objetos.LoadCombo(cmbTipoServico, tblTipoServicoObra.Get(Global.GetConnection()), "cmpDcTipoServico", "cmpIdTipoServico", "cmpDcTipoServico", "-- Selecione um serviço", true);

                    Objetos.LoadCombo(cmbAno, tblAno.Get(Global.GetConnection()), "cmpNuAno", "cmpNuAno", "cmpNuAno", "--Selecione o ano--", true);
                    cmbAno.SelectedIndex = Objetos.RetornaIndiceCombo(cmbAno, long.Parse(tbl.Rows[0]["cmpAnoAtual"].ToString()));
                    Objetos.LoadCombo(cmbMes, tblMes.GetAll(Global.GetConnection()), "cmpDcMes", "cmpIdMes", "cmpIdMes", "--Selecione o mês--", true);
                    cmbMes.SelectedIndex = Objetos.RetornaIndiceCombo(cmbMes, long.Parse(tbl.Rows[0]["cmpMesAtual"].ToString()));

                    Objetos.LoadCombo(cmbAnoAbertura, tblAno.Get(Global.GetConnection()), "cmpNuAno", "cmpNuAno", "cmpNuAno", "--Selecione o ano--", true);
                    cmbAnoAbertura.SelectedIndex = Objetos.RetornaIndiceCombo(cmbAno, long.Parse(tbl.Rows[0]["cmpAnoAtual"].ToString()));

                    Objetos.LoadCombo(cmbMesAbertura, tblMes.GetAll(Global.GetConnection()), "cmpDcMes", "cmpIdMes", "cmpIdMes", "--Selecione o mês--", true);
                    cmbMesAbertura.SelectedIndex = Objetos.RetornaIndiceCombo(cmbMes, long.Parse(tbl.Rows[0]["cmpMesAtual"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Pesquisa Preventiva Agenda.
        /// </summary>
        private DataTable 
            pesquisa()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();

            lc = new ListCampo();
            lc.NomeCampo = "cmpNuAnoFatura";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = cmbAno.SelectedValue.ToString();
            ls.Add(lc);

            lc = new ListCampo();
            lc.NomeCampo = "cmpNuMesFatura";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = cmbMes.SelectedValue.ToString();
            ls.Add(lc);

            if (cmbTipoServico.SelectedValue != "0")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpIdTipoServico";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = cmbTipoServico.SelectedValue;
                ls.Add(lc);
            }

            if (chkEmiteNotaFiscal.Checked == true)
            {
                lc = new ListCampo();
                lc.NomeCampo    = "cmpInEmiteNotaMes";
                lc.TipoCampo    = TipoCampo.Numero;
                lc.Sinal        = SinalPesquisa.Igual;
                lc.ValorCampo   = "0";
                ls.Add(lc);
            }

            return tblFaturaObra.Get(Global.GetConnection(), ls);
        }

        /// <summary>
        /// Validar campos abertura faturamento
        /// </summary>
        private bool ValidarCampos(string Abertura)
        {
            bool ret = true;
            string msg = "";
            if (Abertura == "true")
            {
                if (cmbAnoAbertura.SelectedIndex == 0)
                    msg += "Ano do faturamento não selecionado! <br />";
                if (cmbMesAbertura.SelectedIndex == 0)
                    msg += "Mês do faturamento não selecionado! <br />";
            }

            if (txtDtEmissaoNota.Text == "")
                msg += "Data para emissão da nota em branco! <br />";
            if (txtDtEntregaDoc.Text == "")
                msg += "Data para da entrega dos documentos no cliente em branco! <br />";
            if (TxtDtRecFatura.Text == "")
                msg += "Data para recebimento da nota fiscal em branco! <br />";

            if (msg != "")
            {
                MsgBox.ShowMessage(msg, "Erro");
                ret = false;
            }

            return ret;
        }
        #endregion

        #region events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                this.load();
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbAno.SelectedIndex == 0)
                    throw new Exception("Ano do faturamento não selecionado!");
                if (cmbMes.SelectedIndex == 0)
                    throw new Exception("Mês do faturamento não selecionado!");

                DataTable table = pesquisa();

                if (table == null || table.Rows.Count == 0)
                    throw new Exception("Não existe faturamento aberto para o ano e mês selecionado!");
                else
                {
                    double TamDiv = table.Rows.Count * 25;
                    TamDiv += 80;

                    this.divstyle.Style[HtmlTextWriterStyle.Height] = TamDiv.ToString() + "px";

                    gvDados.DataSource = table;
                    gvDados.DataBind();
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(ex.Message.ToString(), "Erro");
            }

        }

        protected void gvDados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvDados.PageIndex = e.NewPageIndex;
                gvDados.DataSource = pesquisa();
                gvDados.DataBind();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        protected void gvDados_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);  //creating new Header Type 
                TableCell HeaderCell = new TableCell(); //creating HeaderCell

                //HeaderCell.Text = "Faturamento GrupoOrion Mês/Ano :";
                //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //HeaderCell.ColumnSpan = 9;
                //HeaderGridRow.Cells.Add(HeaderCell);//Adding HeaderCell to header.
                //gvDados.Controls[0].Controls.AddAt(0, HeaderGridRow);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Dados da Obra";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //HeaderCell.RowSpan = 2;
                HeaderCell.ColumnSpan = 4;
                HeaderGridRow.Cells.Add(HeaderCell);//Adding HeaderCell to header.

                HeaderCell = new TableCell();
                HeaderCell.Text = "Emissão da Nota";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //HeaderCell.RowSpan = 2;
                HeaderCell.ColumnSpan = 1;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.Text = "Entega Documentos na Obra";
                //HeaderCell.RowSpan = 2;
                HeaderCell.ColumnSpan = 1;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.Text = "Recebimento da Nota";
                //HeaderCell.RowSpan = 2;
                HeaderCell.ColumnSpan = 1;
                HeaderGridRow.Cells.Add(HeaderCell);

                gvDados.Controls[0].Controls.AddAt(0, HeaderGridRow);

            }
        }

        protected void btnAbertura_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["AberturaFaturamento"] = "true";
                lblNomeObra.Visible     = false;
                lblObra.Visible         = false;
                lblAnoAbertura.Visible  = true;
                cmbAnoAbertura.Visible  = true;
                lblMesAbertura.Visible  = true;
                cmbMesAbertura.Visible  = true;
                ChkEmissaoNota.Visible = false;
                this.ModalPopupExtender2.Show();
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(ex.Message.ToString(), "Erro");
            }
        }

        protected void chkEmiteNotaFiscal_CheckedChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        protected void lnkFatura_Click(object sender, EventArgs e)
        {
            lblNomeObra.Visible     = true;
            lblObra.Visible         = true;
            lblAnoAbertura.Visible  = false;
            cmbAnoAbertura.Visible  = false;
            lblMesAbertura.Visible  = false;
            cmbMesAbertura.Visible  = false;
            ChkEmissaoNota.Visible  = true;

            ViewState["AberturaFaturamento"] = "false";

            LinkButton lnkFatura = sender as LinkButton;

            GridViewRow gvrow            = (GridViewRow)lnkFatura.NamingContainer;
            ViewState["cmpIdFaturaObra"] = lnkFatura.CommandArgument.ToString();
            ViewState["cmpTpFatura"]     = gvrow.Cells[0].Text;

            ListCampos ls   = new ListCampos();
            ListCampo lc    = new ListCampo();
            lc.NomeCampo    = "cmpidFaturaObra";
            lc.TipoCampo    = TipoCampo.Numero;
            lc.ValorCampo   = ViewState["cmpIdFaturaObra"].ToString();
            ls.Add(lc);

            using (DataTable table = tblFaturaObra.Get(Global.GetConnection(), ls))
            {
                lblObra.Text            = table.Rows[0]["cmpNoObraFatura"].ToString();
                txtDtEmissaoNota.Text   = table.Rows[0]["cmpDtPrevEmissaoNota"].ToString();
                txtDtEntregaDoc.Text    = table.Rows[0]["cmpDtPrevEntregaDocObra"].ToString();
                TxtDtRecFatura.Text     = table.Rows[0]["cmpDtPrevRecebeNota"].ToString();
                ChkEmissaoNota.Checked  = bool.Parse(table.Rows[0]["cmpInEmiteNotaMes"].ToString());
            }

            this.ModalPopupExtender2.Show();
        }

        protected void btnCancelRepro_Click(object sender, EventArgs e)
        {
            this.txtDtEmissaoNota.Text = null;
            this.txtDtEntregaDoc.Text = null;
            this.TxtDtRecFatura.Text = null;

            this.ModalPopupExtender2.Hide();
        }

        protected void btnSaveFatura_Click(object sender, EventArgs e)
        {
            if (ViewState["AberturaFaturamento"].ToString() == "true")
            {
                if (ValidarCampos(ViewState["AberturaFaturamento"].ToString()))
                {
                    tblFaturaObra table = new tblFaturaObra();
                    table.cmpNuAnoFatura = cmbAnoAbertura.SelectedValue.ToString();
                    table.cmpNuMesFatura = cmbMesAbertura.SelectedValue.ToString();
                    table.cmpDtPrevEmissaoNota = txtDtEmissaoNota.Text.ToString();
                    table.cmpDtPrevEntregaDocObra = txtDtEntregaDoc.Text.ToString();
                    table.cmpDtPrevRecebeNota = TxtDtRecFatura.Text.ToString();
                    table.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();

                    table.FaturamentoAbertura(Global.GetConnection());
                    MsgBox.ShowMessage("Faturamento aberto com sucesso!", "Aviso");

                    gvDados.DataSource = pesquisa();
                    gvDados.DataBind();
                }
                else
                    this.ModalPopupExtender2.Show();
            }
            else
            {
                if (ViewState["cmpTpFatura"].ToString() == "1" ? ValidarCampos(ViewState["AberturaFaturamento"].ToString()) : true)
                {
                    try
                    {
                        tblFaturaObra table = new tblFaturaObra();
                        table.cmpIdFaturaObra           = ViewState["cmpIdFaturaObra"].ToString();
                        table.cmpDtPrevEmissaoNota      = txtDtEmissaoNota.Text.ToString();
                        table.cmpDtPrevEntregaDocObra   = txtDtEntregaDoc.Text.ToString();
                        table.cmpDtPrevRecebeNota       = TxtDtRecFatura.Text.ToString();
                        table.cmpInEmiteNotaMes         = ChkEmissaoNota.Checked.ToString();
                        table.cmpNoUsuario              = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();

                        table.GravarAberturaFaturamento(Global.GetConnection());
                        MsgBox.ShowMessage("Registro atualizado com sucesso!", "Aviso");
                        btnPesquisar_Click(sender, e);
                    }
                    catch (Exception ex)
                    {
                        Global.ShowError(Global.Title, ex);
                    }
                }
                else
                    this.ModalPopupExtender2.Show();
            }
        }

        protected void gvDados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    e.Row.Cells[0].Visible = false;
                    break;
                case DataControlRowType.DataRow:
                    e.Row.Cells[0].Visible = false;
                    if (e.Row.Cells[2].Text != "Sim")
                    {
                        e.Row.Font.Bold = true;
                        e.Row.ForeColor = System.Drawing.Color.Red;
                    }
                    break;
            }

        }
        #endregion

    }
}