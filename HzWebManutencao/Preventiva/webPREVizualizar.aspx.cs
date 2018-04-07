using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibConnection.Sql;
using System.Data;
using HzlibWEB;
using HzLibManutencao;
using HzLibCorporativo.Funcional;
using Apresentacao.Controles;

namespace HzWebManutencao.Preventiva
{
    public partial class webPREVizualizar : System.Web.UI.Page
    {
        private string cmpDcGrupoAtividade = "";
        private string cmpDcTipoAtividade = "";
        private int intSubTotalIndex;
        private bool Grupo;
        private bool Tipo;
        private bool cabecalho;

        #region LOAD
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.loadObra();
                this.load();
                this.Session.Add("cmpCoPreventiva", 0);
                this.Session.Add("cmpCoObraGrupoLista", 0);
                this.Session.Add("cmpIdEquipamentoObra", 0);
            }
        }
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
        /// Carrega as listboxes.
        /// </summary>
        private void load()
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoContratante";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoContratante;
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "cmpCoObraGrupoLista";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = cmbObra.SelectedValue.ToString();
                ls.Add(lc);

                using (DataTable table = tblTipoAtividade.RetornaTipoAtividadeObra(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbTipoAtividade, table, "cmpDcTipoAtividade", "cmpCoTipoAtividade", "cmpDcTipoAtividade", "--- Selecione todas ---", true);
                    Objetos.LoadCombo(cmbTipoEquip, table, "cmpDcTipoAtividade", "cmpCoTipoAtividade", "cmpDcTipoAtividade", "--- Selecione todas ---", true);
                }

                using (DataTable table = tblPeriodicidade.RetornaPeriodicidadeObra(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbPeriodicidade, table, "cmpDcPeriodicidade", "cmpCoPeriodicidade", "cmpDcPeriodicidade", "--- Selecione todas ---", true);
                }

            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        } 
        #endregion
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            DataTable tbl = pesquisaPreventiva();

                grdFormPreventiva.DataSource = tbl;
                grdFormPreventiva.DataBind();
            
        }

        protected void cmbObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista = cmbObra.SelectedValue;
            this.load();
            cmbTipoAtividade.Focus();
            btnPesquisar_Click(sender, e);
        }

        protected void cmbTipoAtividade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                limpa();
                this.loadPeriodicidadeTpAtividade();
                cmbPeriodicidade.Focus();
                btnPesquisar_Click(sender, e);
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }
        private void limpa()
        {
            cmbPeriodicidade.Items.Clear();
        }
        /// <summary>
        /// Carrega as listboxes.
        /// </summary>
        private void loadPeriodicidadeTpAtividade()
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoContratante";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoContratante;
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "cmpCoObraGrupoLista";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = cmbObra.SelectedValue.ToString();
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "cmpCoTipoAtividade";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = cmbTipoAtividade.SelectedValue.ToString();
                ls.Add(lc);

                using (DataTable table = tblPeriodicidade.RetornaPeriodicidadeTipoAtividade(Global.GetConnection(), ls))
                {
                    Objetos.LoadCombo(cmbPeriodicidade, table, "cmpDcPeriodicidade", "cmpCoPeriodicidade", "cmpDcPeriodicidade", "--- Selecione todas ---", true);
                }

            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }
        protected void cmbPeriodicidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        protected void rdbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }
        protected void grdFormPreventiva_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdFormPreventiva.PageIndex = e.NewPageIndex;
                grdFormPreventiva.DataSource = pesquisaPreventiva();
                grdFormPreventiva.DataBind();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }

        }
        private DataTable pesquisaPreventiva()
        {
            string TpEquipamento = rdbType.SelectedValue != "T" ? rdbType.SelectedValue : "0";
            int espelho = 0;
            if (ckEspelhadas.Checked == true) { espelho = 1; }
            if (espelho == 1) { grdFormPreventiva.Columns[6].Visible = false; }
            else { grdFormPreventiva.Columns[6].Visible = true; }
            return tblPreventivaAgenda.RetornaPreventivaVizualizar(Global.GetConnection(), cmbObra.SelectedValue.ToString(), cmbPeriodicidade.SelectedValue.ToString(), cmbTipoAtividade.SelectedValue.ToString(), TpEquipamento,espelho);
        }

        #region Equipamentos
        protected void dgvEquipamento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvEquipamento.PageIndex = e.NewPageIndex;
            DataTable dtEquipamentoObra = PesquisaEquipamento();
            dgvEquipamento.DataSource = dtEquipamentoObra;
            dgvEquipamento.DataBind();
            this.ModalPopupExtender1.Show();

            this.ModalPopupExtender1.Show();
        }
        protected void btnCancelarEquipamento_Click(object sender, EventArgs e)
        {
            this.ModalPopupExtender1.Hide();
        }
        protected void dgvEquipamento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int linha = int.Parse(e.CommandArgument.ToString());
                int IdEquipamentoObra = int.Parse(dgvEquipamento.DataKeys[linha]["cmpIdEquipamentoObra"].ToString());
                Session["cmpIdEquipamentoObra"] = IdEquipamentoObra;
                this.ModalPopupExtender2.Show();
            }

        }
        private DataTable PesquisaEquipamento()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpCoObraGrupoLista";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = this.Session["cmpCoObraGrupoLista"].ToString();
            ls.Add(lc);

            if (txtDescEquip.Text != "")
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpDcEquipamentoObra";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = this.Session["cmpCoObraGrupoLista"].ToString();
                ls.Add(lc);
            }
            if (cmbTipoEquip.SelectedIndex>-0)
            {
                lc = new ListCampo();
                lc.NomeCampo = "cmpCoTipoAtividade";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = cmbTipoEquip.SelectedValue;
                ls.Add(lc);
            }

            DataTable dtEquipamentoObra = tblEquipamentoObra.RetornarEquipamentoObra(Global.GetConnection(), ls);
            return dtEquipamentoObra;
        } 
        #endregion

        protected void grdFormPreventiva_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

                int linha = int.Parse(e.CommandArgument.ToString());
                int CoPreventiva = int.Parse(grdFormPreventiva.DataKeys[linha]["cmpCoPreventiva"].ToString());
                int CoObraGrupoLista = int.Parse(grdFormPreventiva.DataKeys[linha]["cmpCoObraGrupoLista"].ToString());

                this.Session["cmpCoPreventiva"] = int.Parse(grdFormPreventiva.DataKeys[linha]["cmpCoPreventiva"].ToString());
                this.Session["cmpCoObraGrupoLista"] = int.Parse(grdFormPreventiva.DataKeys[linha]["cmpCoObraGrupoLista"].ToString());

                //DataTable dtEquipamentoObra = tblEquipamentoObra.RetornarEquipamentoObra(Global.GetConnection(), CoObraGrupoLista);
                DataTable dtEquipamentoObra =PesquisaEquipamento();
                dgvEquipamento.DataSource = dtEquipamentoObra;
                dgvEquipamento.DataBind();
                this.ModalPopupExtender1.Show();
            }
            if (e.CommandName == "Visualizar")
            {
                LinkButton lk = (LinkButton)e.CommandSource;
                string[] Valores = lk.Text.Split('/');
                //Response.Write("<script>window.open('webPRE_EditarPreventiva.aspx?idPreventivaAgenda=" + lk.Text + "','_blank');</script>");
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "[cmpCoPreventiva]";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = Valores[0];
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "[cmpCoGrupoAtividade]";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = Valores[1];
                ls.Add(lc);

                lblPreventiva.Text = "Codigo Preventiva / Codigo Grupo Atividade :" + Valores[0] + "/" + Valores[1];
                DataTable dtPreventiva = tblPreventiva.RetornaDados(Global.GetConnection(), ls);
                grdPreventiva.DataSource = dtPreventiva;
                grdPreventiva.DataBind();
                ModalPopupExtender3.Show();

            }
        }

        protected void dgvEquipamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.ModalPopupExtender1.Show();
        }


        protected void btnSalvarEspelho_Click(object sender, EventArgs e)
        {
            tblPreventiva preventiva = new tblPreventiva();

            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();
            lc.NomeCampo = "cmpCoPreventiva";
            lc.TipoCampo = TipoCampo.Numero;
            lc.ValorCampo = this.Session["cmpCoPreventiva"].ToString();
            ls.Add(lc);

            DataTable dtPreventiva = tblPreventiva.GetAll(Global.GetConnection(),ls);
            preventiva.cmpCoPreventiva = this.Session["cmpCoPreventiva"].ToString();
            preventiva.cmpCoObraGrupoLista = dtPreventiva.Rows[0]["cmpCoObraGrupoLista"].ToString();
            preventiva.cmpCoPeriodicidade = dtPreventiva.Rows[0]["cmpCoPeriodicidade"].ToString();
            preventiva.cmpCoTipoAtividade = dtPreventiva.Rows[0]["cmpCoTipoAtividade"].ToString();
            preventiva.cmpTpPreventiva = dtPreventiva.Rows[0]["cmpTpPreventiva"].ToString();
            preventiva.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario;
            DataTable dtCoPreventiva= preventiva.IncluirPreventivaEspelho(Global.GetConnection());

            for (int i = 0; i < dtPreventiva.Rows.Count-1; i++)
            {
                tblPreventivaAtividade table = new tblPreventivaAtividade();
                table.cmpCoPreventivaAtividade = "0";
                table.cmpCoObraGrupoLista = dtPreventiva.Rows[0]["cmpCoObraGrupoLista"].ToString();
                table.cmpCoPreventiva =dtCoPreventiva.Rows[0][0].ToString();
                table.cmpCoPeriodicidade =  dtPreventiva.Rows[0]["cmpCoPeriodicidade"].ToString();
                table.cmpCoTipoAtividade = dtPreventiva.Rows[0]["cmpCoTipoAtividade"].ToString();
                table.cmpCoItemAtividade = dtPreventiva.Rows[0]["cmpCoItemAtividade"].ToString();
                table.cmpTpPreventiva = dtPreventiva.Rows[0]["cmpTpPreventiva"].ToString();
                table.cmpDcItemAtividadePreventiva = dtPreventiva.Rows[0]["cmpDcItemAtividadePreventiva"].ToString();
                table.cmpNoUsuario = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpNoUsuario.TrimEnd();
                table.Save(Global.GetConnection());

                tblPreventivaEquipamentocs prevEquip = new tblPreventivaEquipamentocs();
                prevEquip.IncluirPreventivaEquipamentos(Global.GetConnection(), int.Parse(dtCoPreventiva.Rows[0][0].ToString()), int.Parse(Session["cmpIdEquipamentoObra"].ToString()), int.Parse(dtPreventiva.Rows[0]["cmpCoGrupoAtividade"].ToString()));
            }
           

            //prevatv.Save();
            this.ModalPopupExtender1.Show();
        }

        protected void btnCancelarEspelho_Click(object sender, EventArgs e)
        {
            this.ModalPopupExtender1.Show();
        }

        protected void grdFormPreventiva_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ckEspelhadas_CheckedChanged(object sender, EventArgs e)
        {
            grdFormPreventiva.DataSource = pesquisaPreventiva();
            grdFormPreventiva.DataBind();
        }

        protected void btnPesquisarEquipamento_Click(object sender, EventArgs e)
        {
            DataTable dtEquipamentoObra = PesquisaEquipamento();
            dgvEquipamento.DataSource = dtEquipamentoObra;
            dgvEquipamento.DataBind();
            this.ModalPopupExtender1.Show();
        }

        protected void cmbTipoEquip_SelectedIndexChanged(object sender, EventArgs e)
        {

                DataTable dtEquipamentoObra = PesquisaEquipamento();
                dgvEquipamento.DataSource = dtEquipamentoObra;
                dgvEquipamento.DataBind();
                this.ModalPopupExtender1.Show();

        }

        protected void grdPreventiva_RowCreated(object sender, GridViewRowEventArgs e)
        {
            cabecalho = true;
            if (cabecalho == false)
            {
                if (string.IsNullOrEmpty(cmpDcGrupoAtividade) && DataBinder.Eval(e.Row.DataItem, "cmpDcGrupoAtividade") != null) { Grupo = true; }
                if (string.IsNullOrEmpty(cmpDcTipoAtividade) && DataBinder.Eval(e.Row.DataItem, "cmpDcTipoAtividade") != null) { Tipo = true; }
            }
            else
            {
                if (DataBinder.Eval(e.Row.DataItem, "cmpDcGrupoAtividade") != null)
                {

                    if (cmpDcGrupoAtividade != DataBinder.Eval(e.Row.DataItem, "cmpDcGrupoAtividade").ToString().Trim()) { Grupo = true; };
                }
                if (DataBinder.Eval(e.Row.DataItem, "cmpDcTipoAtividade") != null)
                {
                    if (cmpDcTipoAtividade != DataBinder.Eval(e.Row.DataItem, "cmpDcTipoAtividade").ToString().Trim()) { ; Tipo = true; }
                }
            }

            if (Grupo == true)
            {
                GridView grdViewOrders = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "Grupo de Atividade : " + DataBinder.Eval(e.Row.DataItem, "cmpDcGrupoAtividade").ToString();
                cell.ColumnSpan = 6;
                cell.CssClass = "GroupHeaderStyle";
                cell.Font.Bold = true;
                row.Cells.Add(cell);
                grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
            }
            if (Tipo == true)
            {
                GridView grdViewOrders = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "Tipo de Atividade:" + DataBinder.Eval(e.Row.DataItem, "cmpDcTipoAtividade").ToString();
                cell.ColumnSpan = 6;
                cell.CssClass = "GroupHeaderStyle";
                cell.Font.Bold = true;
                row.Cells.Add(cell);
                grdViewOrders.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
            }

            if (DataBinder.Eval(e.Row.DataItem, "cmpDcGrupoAtividade") != null) { cmpDcGrupoAtividade = DataBinder.Eval(e.Row.DataItem, "cmpDcGrupoAtividade").ToString().Trim(); Grupo = true; }
            if (DataBinder.Eval(e.Row.DataItem, "cmpDcTipoAtividade") != null) { cmpDcTipoAtividade = DataBinder.Eval(e.Row.DataItem, "cmpDcTipoAtividade").ToString().Trim(); Tipo = true; }

            Grupo = false;
            Tipo = false;
        }
    }
}