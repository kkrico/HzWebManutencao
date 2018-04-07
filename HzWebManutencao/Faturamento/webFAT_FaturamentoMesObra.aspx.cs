using System;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using HzLibCorporativo.Funcional;
using HzLibCorporativo.Faturamento;
using HzLibCorporativo.Geral;
using HzLibCorporativo.Config;

using HzLibConnection.Sql;
using HzlibWEB;
using HzLibManutencao;
using Apresentacao.Controles;

namespace HzWebManutencao.Faturamento
{
    public partial class webFAT_FaturamentoMesObra : System.Web.UI.Page
    {
        #region Variables
        private decimal ValorTotal = 0;
        private decimal ValorTotalRecebido = 0;
        private ListItem li;
        static string prevPage = string.Empty;
        #endregion

        #region functions
        /// <summary>
        /// Carrega as combos.
        /// </summary>
        private void loadObra()
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoUsuario";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoUsuario;
                ls.Add(lc);

                Objetos.LoadCombo(cmbObra, tblObraFaturaUsuario.RetornarObrasInUsuario(Global.GetConnection(), ls), "cmpNoObraFatura", "cmpIdObraFatura", "cmpNoObraFatura", "--- Selecione a Obra ---", true);
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Carrega as combos.
        /// </summary>
        private void load()
        {
            try
            {
                using (DataTable tbl = tblDataHoraServidor.Get(Global.GetConnection()))
                {
                    Objetos.LoadCombo(cmbAno, tblAno.Get(Global.GetConnection()), "cmpNuAno", "cmpNuAno", "cmpNuAno", true);
                    cmbAno.SelectedIndex = Objetos.RetornaIndiceCombo(cmbAno, long.Parse(tbl.Rows[0]["cmpAnoAtual"].ToString()));
                    Objetos.LoadCombo(cmbMes, tblMes.GetAll(Global.GetConnection()), "cmpDcMes", "cmpIdMes", "cmpIdMes", true);
                    cmbMes.SelectedIndex = Objetos.RetornaIndiceCombo(cmbMes, long.Parse(tbl.Rows[0]["cmpMesAtual"].ToString()));
                }

                //Combo da emissão da nota
                string[,] Elementos = new string[,] { { "0", "Selecione o item" }, { "A", "Em Aberto" }, { "E", "Emitido" }, { "P", "Pendente" } };
                cmbEmiFatura.Items.Clear();

                for (int i = 0; i <= Elementos.GetUpperBound(0); i++)
                {
                    li = new ListItem();
                    li.Value = Elementos[i, 0];
                    li.Text = Elementos[i, 1];
                    cmbEmiFatura.Items.Add(li);
                }

                //Combo da entrega documento no cliente
                Array.Clear(Elementos, 0, 8);

                Elementos = new string[,] { { "0", "Selecione o item" }, { "A", "Em Aberto" }, { "E", "Emitido" }, { "P", "Pendente" } };
                cmbEntFatura.Items.Clear();

                for (int i = 0; i <= Elementos.GetUpperBound(0); i++)
                {
                    li = new ListItem();
                    li.Value = Elementos[i, 0];
                    li.Text = Elementos[i, 1];
                    cmbEntFatura.Items.Add(li);
                }

                //Combo do recebimento da fatura
                Array.Clear(Elementos, 0, 8);

                Elementos = new string[,] { { "0", "Selecione o item" }, { "RT", "Recebimento Total" }, { "RP", "Recebimento Parcial" }, { "PE", "Pendência" } };
                cmbRecFatura.Items.Clear();

                for (int i = 0; i <= Elementos.GetUpperBound(0); i++)
                {
                    li = new ListItem();
                    li.Value = Elementos[i, 0];
                    li.Text = Elementos[i, 1];
                    cmbRecFatura.Items.Add(li);
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
        private DataTable pesquisa()
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

            if (cmbObra.SelectedValue != "0")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpIdObraFatura";
                lc.TipoCampo = TipoCampo.String;
                lc.Sinal = SinalPesquisa.Igual;
                lc.ValorCampo = cmbObra.SelectedValue;
                ls.Add(lc);
            }

            if (cmbEmiFatura.SelectedValue != "0")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpStEmissaoNota";
                lc.TipoCampo = TipoCampo.String;
                lc.ValorCampo = cmbEmiFatura.SelectedValue;
                ls.Add(lc);
            }

            if (cmbEntFatura.SelectedValue != "0")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpStEntregaDocObra";
                lc.TipoCampo = TipoCampo.String;
                lc.ValorCampo = cmbEntFatura.SelectedValue;
                ls.Add(lc);
            }

            if (cmbRecFatura.SelectedValue != "0")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpStRecebeNota";
                lc.TipoCampo = TipoCampo.String;
                lc.ValorCampo = cmbRecFatura.SelectedValue;
                ls.Add(lc);
            }

            lc = new ListCampo();
            lc.NomeCampo = "cmpCoUsuario";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoUsuario;
            ls.Add(lc);

            lc = new ListCampo();
            lc.NomeCampo = "cmpInAtivo";
            lc.TipoCampo = TipoCampo.String;
            lc.ValorCampo = "true";
            ls.Add(lc);

            return tblObraFaturaUsuario.Get(Global.GetConnection(), ls);
        }
        #endregion

        #region events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.load();
                this.loadObra();

                if (Request.QueryString != null && Request.QueryString.Count > 0)
                {
                    cmbAno.SelectedIndex = Objetos.RetornaIndiceCombo(cmbAno, int.Parse(Request.QueryString["NuAnoFatura"]));
                    cmbMes.SelectedIndex = Objetos.RetornaIndiceCombo(cmbMes, int.Parse(Request.QueryString["NuMesFatura"]));
                }

                btnPesquisar_Click(sender, e);
            }    
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            DataTable tbl = pesquisa();

            if (tbl.Rows.Count > 0)
            {
                double TamDiv = tbl.Rows.Count * 30;
                TamDiv += 120;

                this.divstyle.Style[HtmlTextWriterStyle.Height] = TamDiv.ToString() + "px";
                gvDados.DataSource = tbl;
                gvDados.DataBind();
            }

        }

        protected void cmbObra_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void rdbData_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        protected void gvDados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.Trim())
            {
                case "lnkEntregaRelManu":
                    Response.Redirect("webFAT_FaturamentoEnviaRelManut.aspx?idFaturaObra=" + e.CommandArgument.ToString(), false);
                    break;
                case "lnkEntregaCarta":
                    Response.Redirect("webFAT_FaturamentoEntregaCarta.aspx?idFaturaObra=" + e.CommandArgument.ToString(), false);
                    break;
                case "lnkRecebe":
                    Response.Redirect("webFAT_RecebeFatura.aspx?idFaturaObra=" + e.CommandArgument.ToString(), false);
                    break;
            }
        }

        protected void gvDados_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //lblSummary.Text = String.Format("Total Exp: {0:c}", _TotalExpTotal);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Valor da Fatura
                Label lblValor = (Label)e.Row.FindControl("lblValor");
                if (lblValor.Text != "")
                {
                    ValorTotal += Decimal.Parse(lblValor.Text);

                    if (e.Row.Cells[14].Text == "Recebimento Total")
                        e.Row.Cells[2].FindControl("lnkRecebe").Visible = false;
                }
                else
                {
                    e.Row.Cells[1].FindControl("lnkEntregaCarta").Visible = false;
                    e.Row.Cells[2].FindControl("lnkRecebe").Visible = false;
                }

                //Valor Recebido
                Label lblValorRecebido = (Label)e.Row.FindControl("lblValorRecebido");
                if (lblValorRecebido.Text != "")
                    ValorTotalRecebido += Decimal.Parse(lblValorRecebido.Text);

                // Sub grid com dados das notas e recebimento
                LinkButton IdFatura = (LinkButton)e.Row.FindControl("lnkEntregaCarta");
 
                GridView gv = (GridView)e.Row.FindControl("gvChildGrid");

                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();

                lc = new ListCampo();
                lc.NomeCampo  = "cmpIdFaturaObra";
                lc.TipoCampo  = TipoCampo.Numero;
                lc.ValorCampo = IdFatura.CommandArgument.ToString();
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo  = "cmpInStautsNota";
                lc.TipoCampo  = TipoCampo.String;
                lc.ValorCampo = "true";
                ls.Add(lc);

                gv.DataSource = tblFaturaCartaNota.GetRecebimento(Global.GetConnection(), ls);
                gv.DataBind();
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //Valor total fatura
                Label lblValorTotal = (Label)e.Row.FindControl("lblValorTotal");
                if (ValorTotal.ToString() != "")
                    lblValorTotal.Text = ValorTotal.ToString("N2");

                //Valor total recebido
                Label lblValorTotalRecebido = (Label)e.Row.FindControl("lblValorTotalRecebido");
                if (lblValorTotalRecebido.ToString() != "")
                    lblValorTotalRecebido.Text = ValorTotalRecebido.ToString("N2");
            }
        }

        protected void gvDados_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView    HeaderGrid    = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0,0,  DataControlRowType.Header,DataControlRowState.Insert);  //creating new Header Type 
                TableCell   HeaderCell    = new TableCell(); //creating HeaderCell

                //HeaderCell.Text = "Faturamento GrupoOrion Mês/Ano :";
                //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //HeaderCell.ColumnSpan = 9;
                //HeaderGridRow.Cells.Add(HeaderCell);//Adding HeaderCell to header.
                //gvDados.Controls[0].Controls.AddAt(0, HeaderGridRow);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Dados da Obra";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //HeaderCell.RowSpan = 2;
                HeaderCell.ColumnSpan = 5;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Relatório Manutenção";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //HeaderCell.RowSpan = 2;
                HeaderCell.ColumnSpan = 2;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Emissão da Fatura";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                //HeaderCell.RowSpan = 2;
                HeaderCell.ColumnSpan = 3;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.Text = "Entega Fatura";
                //HeaderCell.RowSpan = 2;
                HeaderCell.ColumnSpan = 2;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.Text = "Recebimento da Fatura";
                //HeaderCell.RowSpan = 2;
                HeaderCell.ColumnSpan = 4;
                HeaderGridRow.Cells.Add(HeaderCell);

                gvDados.Controls[0].Controls.AddAt(0, HeaderGridRow);

            }
        }
        #endregion
    }
}