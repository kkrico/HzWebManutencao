using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HzlibWEB;

using HzLibConnection.Sql;
using HzLibCorporativo.Config;
using HzLibCorporativo.Geral;
using HzLibCorporativo.Funcional;
using Apresentacao.Controles;

namespace HzWebManutencao.Manto
{
    public partial class webMTO_Graficos : System.Web.UI.Page
    {
        #region Functions
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
                    {
                        cmbObra.SelectedIndex = Objetos.RetornaIndiceCombo(cmbObra, int.Parse(((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista));
                    }
                    cmbObra_SelectedIndexChanged(cmbObra, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        private void CarregaMes()
        {
            DataTable table = tblMes.GetAll(Global.GetConnection());
            Objetos.LoadCombo(cmbDtInicial, table, "cmpDcMes", "cmpIdMes", "cmpIdMes", true);
            cmbDtInicial.SelectedIndex = DateTime.Now.Month - 1;

            Objetos.LoadCombo(cmbDtFinal, table, "cmpDcMes", "cmpIdMes", "cmpIdMes", true);
            cmbDtFinal.SelectedIndex = DateTime.Now.Month - 1;
        }

        private void ControleExibicao(bool periodo)
        {
            lblDtFinal.Visible = periodo;
            cmbDtFinal.Visible = periodo;
            Label6.Visible = periodo;
            TxtAnoFinal.Visible = periodo;
        }

        private void MontaPopUp()
        {
            btnGerar.Attributes.Add("onclick", "window.open('" + @"http://www.manto.com.br/Gera_Graficos.asp?co_usuario=" + ViewState["co_usuario"].ToString() + "&co_obra=" + ViewState["co_ObraManto"] + ViewState["MesInicial"] + ViewState["AnoInicial"] + ViewState["MesFinal"] + ViewState["AnoFinal"] + "&tipo_graficonet=" + ViewState["tipoGrafico"] + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');");
        }

        #endregion

        #region Events

        private void Page_Init(object sender, EventArgs e)
        {
            ViewState["tipoGrafico"] = "";
            ViewState["MesFinal"] = "";
            ViewState["AnoFinal"] = "";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoUsuario";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoUsuario;
                ls.Add(lc);

                DataTable table = tblUsuario.Get(Global.GetConnection(), ls);
                if (table != null && table.Rows.Count > 0)
                    ViewState["co_usuario"] = table.Rows[0]["chavemanto"].ToString();

                CarregaMes();
                txtAnoInicial.Text = DateTime.Now.Year.ToString();
                TxtAnoFinal.Text = DateTime.Now.Year.ToString();
                loadObra();
                cmbDtInicial.Focus();
            }
        }

        /// <summary>
        /// Evento ao selecionar uma obra.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista = cmbObra.SelectedValue;

                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoObraGrupoLista";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista;
                ls.Add(lc);

                using (DataTable table = tblObraUsuario.Get(Global.GetConnection(), ls))
                {
                  ViewState["co_ObraManto"] = table.Rows[0]["cmpCoObraManto"].ToString();
                }
                rdbHidra.Items[0].Selected = true;
                rdbHidra_SelectedIndexChanged(sender, null);
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        protected void rdbHidra_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdbEletrico.ClearSelection();
            ViewState["tipoGrafico"] = rdbHidra.SelectedValue;

            switch (rdbHidra.SelectedValue)
            {
                case "CAD": // Consumo diário de água 
                    ControleExibicao(false);
                    cmbDtInicial_SelectedIndexChanged(sender, null);
                    txtAnoInicial_TextChanged(sender, null);
                    break;
                case "CAM": // Consumo médio mensal de água 
                    //ViewState["filename"] = ViewState["co_ObraManto"].ToString() + "&mesinicial=" + cmbDtInicial.SelectedValue + "&anoinicial=" + txtAnoInicial.Text + "&mesfinal=" + cmbDtFinal.SelectedValue + "&anofinal=" + TxtAnoFinal.Text + "&tipo_graficonet=" + rdbHidra.SelectedValue;
                    ControleExibicao(true);
                    cmbDtInicial_SelectedIndexChanged(sender, null);
                    txtAnoInicial_TextChanged(sender, null);
                    cmbDtFinal_SelectedIndexChanged(sender, null);
                    TxtAnoFinal_TextChanged(sender, null);
                    cmbDtFinal_SelectedIndexChanged(sender, null);
                    break;
            }
        }

        protected void rdbEletrico_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdbHidra.ClearSelection();
            ViewState["tipoGrafico"] = rdbEletrico.SelectedValue;

            switch (rdbEletrico.SelectedValue)
            {
                case "CAQG": // Consumo absoluto ativo do quadro geral
                    ControleExibicao(true);
                    cmbDtInicial_SelectedIndexChanged(sender, null);
                    txtAnoInicial_TextChanged(sender, null);
                    cmbDtFinal_SelectedIndexChanged(sender, null);
                    TxtAnoFinal_TextChanged(sender, null);
                    cmbDtFinal_SelectedIndexChanged(sender, null);
                    break;
                case "CDAQG": // Consumo de energia ativo diário do QG
                    cmbDtInicial_SelectedIndexChanged(sender, null);
                    txtAnoInicial_TextChanged(sender, null);
                    ControleExibicao(false);
                    break;
                case "CMAQG": // Consumo médio diário de energia ativa mensal
                    ControleExibicao(true);
                    cmbDtInicial_SelectedIndexChanged(sender, null);
                    txtAnoInicial_TextChanged(sender, null);
                    cmbDtFinal_SelectedIndexChanged(sender, null);
                    TxtAnoFinal_TextChanged(sender, null);
                    cmbDtFinal_SelectedIndexChanged(sender, null);
                    break;
                case "CARQG": // Consumo absoluto reativo mensal
                    ControleExibicao(true);
                    cmbDtInicial_SelectedIndexChanged(sender, null);
                    txtAnoInicial_TextChanged(sender, null);
                    cmbDtFinal_SelectedIndexChanged(sender, null);
                    TxtAnoFinal_TextChanged(sender, null);
                    cmbDtFinal_SelectedIndexChanged(sender, null);
                    break;
                case "CDRQG": // Consumo de energia reativa diária do quadro geral
                    ControleExibicao(false);
                    cmbDtInicial_SelectedIndexChanged(sender, null);
                    txtAnoInicial_TextChanged(sender, null);
                    cmbDtFinal_SelectedIndexChanged(sender, null);
                    TxtAnoFinal_TextChanged(sender, null);
                    cmbDtFinal_SelectedIndexChanged(sender, null);
                    break;
                case "CMRQG": // Consumo médio de energia reativa mensal
                    ControleExibicao(true);
                    cmbDtInicial_SelectedIndexChanged(sender, null);
                    txtAnoInicial_TextChanged(sender, null);
                    break;
           }
        }

        protected void btnGerar_Click(object sender, EventArgs e)
        {
            if (ViewState["tipoGrafico"].ToString() == "")
                CaixaMensagem.Mostar(Mensagem.Tipo.Erro, "Selecione o tipo de gráfico.");
            else if (ViewState["MesInicial"].ToString() == "")
                CaixaMensagem.Mostar(Mensagem.Tipo.Erro, "Selecione o mês inicial.");
            else if (ViewState["AnoInicial"].ToString() == "")
                CaixaMensagem.Mostar(Mensagem.Tipo.Erro, "Informe o ano inicial.");
            else if (ViewState["tipoGrafico"].ToString() != "CAD" && ViewState["tipoGrafico"].ToString() != "CDAQG" && ViewState["tipoGrafico"].ToString() != "CDRQG")
                if (ViewState["MesFinal"].ToString() == "")
                    CaixaMensagem.Mostar(Mensagem.Tipo.Erro, "Informe o mês final.");
                else if (ViewState["AnoFinal"].ToString() == "")
                    CaixaMensagem.Mostar(Mensagem.Tipo.Erro, "Informe o ano final.");
        }

        protected void cmbDtInicial_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["MesInicial"] = "&mesinicial=" + cmbDtInicial.SelectedValue;

            if (ViewState["tipoGrafico"].ToString() == "CAD" 
             || ViewState["tipoGrafico"].ToString() == "CDAQG" 
             || ViewState["tipoGrafico"].ToString() == "CDRQG")
                ViewState["MesFinal"] = "&mesfinal=" + cmbDtInicial.SelectedValue;
            MontaPopUp();
        }

        protected void cmbDtFinal_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["MesFinal"] = "&mesfinal=" + cmbDtFinal.SelectedValue;
            MontaPopUp();
        }

        protected void txtAnoInicial_TextChanged(object sender, EventArgs e)
        {
            ViewState["AnoInicial"] = "&anoinicial=" + txtAnoInicial.Text;
            if (ViewState["tipoGrafico"].ToString() == "CAD" || ViewState["tipoGrafico"].ToString() == "CDAQG" || ViewState["tipoGrafico"].ToString() == "CDRQG")
                ViewState["AnoFinal"] = "&anofinal=" + txtAnoInicial.Text;
            MontaPopUp();
        }

        protected void TxtAnoFinal_TextChanged(object sender, EventArgs e)
        {
            ViewState["AnoFinal"] = "&anofinal=" + TxtAnoFinal.Text;
            MontaPopUp();
        }

        #endregion

    }
}