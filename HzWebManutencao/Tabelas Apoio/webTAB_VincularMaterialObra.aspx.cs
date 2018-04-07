using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using sharpPDF;
using sharpPDF.Enumerators;
using sharpPDF.Tables;
using sharpPDF.Elements;

using HzlibWEB;
using HzLibConnection.Sql;
using HzLibManutencao;
using HzLibCorporativo.Funcional;
using HzLibGeneral.Util;
using HzWebManutencao.Classes.Relatorios;
using Apresentacao.Controles;

namespace HzWebManutencao.Tabelas_Apoio
{
    public partial class webTAB_VincularMaterialObra : System.Web.UI.Page
    {
        #region variables
        static DataTable DtMaterial;
        #endregion

        #region Funções
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
        /// Pesquisa materiais de referência (tblMaterial) e materiais da obra (tblObraGrupoMaterial).
        /// </summary>
        private void PesquisaMaterial()
        {
            LimparCampos();
            CarregaMaterialNotObra();
            CarregaMaterialObra();
        }

        /// <summary>
        /// Retorna materiais da obra (tblObraGrupoMaterial).
        /// </summary>
        private bool CarregaMaterialObra()
        {
            try
            {
                string Material = txtMaterial.Text.ToString();

                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();

                lc = new ListCampo();
                lc.NomeCampo = "cmpCoObraGrupoLista";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista;
                ls.Add(lc);
 
                //lc.NomeCampo = "cmpCoObraGrupo";
                //lc.TipoCampo = TipoCampo.Numero;
                //lc.ValorCampo = ViewState["cmpCoObraGrupo"].ToString();
                //ls.Add(lc);

                if (Char.IsNumber(txtMaterial.Text.ToString(), 0))
                {
                    lc = new ListCampo();
                    lc.NomeCampo = "cmpDcItem";
                    lc.Sinal = SinalPesquisa.Igual;
                    lc.ValorCampo = txtMaterial.Text;
                    ls.Add(lc);
                }
                else
                {
                    lc = new ListCampo();
                    lc.NomeCampo = "cmpDcMaterialObraGrupo COLLATE Latin1_General_CI_AI";
                    lc.TipoCampo = TipoCampo.Like;
                    lc.Sinal = SinalPesquisa.Like;
                    lc.Percent = TipoPercent.InicioFim;
                    lc.ValorCampo = txtMaterial.Text;
                    ls.Add(lc); 
                }
                if (rbdTipo.SelectedValue.ToString() != "2")
                {
                    lc = new ListCampo();
                    lc.NomeCampo  = "cmpInServico";
                    lc.TipoCampo  = TipoCampo.Numero;
                    lc.ValorCampo = rbdTipo.SelectedValue.ToString();
                    ls.Add(lc);
                }

                DtMaterial = tblObraGrupoMaterial.RetornarMaterialObra(Global.GetConnection(), ls);

                using (DtMaterial)
                {
                    if (DtMaterial != null && DtMaterial.Rows.Count > 0)
                    {
                        Objetos.LoadCombo(lstMaterial1, DtMaterial, "cmpDcMaterial", "cmpCoObraGrupoMaterial", "cmpDcMaterial", true);
                        lstMaterial1.SelectedIndex = 0;
                        lstMaterial1_SelectedIndexChanged1(lstMaterial1, EventArgs.Empty);
                        return true;
                    }
                    else
                    {
                        lstMaterial1.Items.Clear();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
                return false;
            }
        }

        /// <summary>
        /// Exibe materiais da obra (tblObraGrupoMaterial) na página.
        /// </summary>
        private void CarregaDadosMaterialObra()
        {
            try
            {
                txtQtUtilizadaAgrupada.Enabled  = false;
                txtQtEstoque.Enabled            = false;
                txtQtUtilizadaObra.Enabled      = false;

                using (DataTable table = tblObraGrupoMaterial.RetornarMaterialSaldoAgrupado(Global.GetConnection(), ViewState["cmpCoObraGrupo"].ToString(), ViewState["cmpCoObraGrupoMaterial"].ToString()))
                {
                    txtQtContrato.Text          = table.Rows[0]["cmpNuQtdContrato"].ToString();
                    txtQtUtilizadaAgrupada.Text = table.Rows[0]["QTUTILIZADO"].ToString();
                    txtQtEstoque.Text           = table.Rows[0]["QTDESTOQUE"].ToString();
                }

                int cmpCoObraGrupoMaterial_atual = int.Parse(ViewState["cmpCoObraGrupoMaterial"].ToString());

                foreach (DataRow row in DtMaterial.Rows)
                {
                    int cmpCoObraGrupoMaterial = int.Parse(row["cmpCoObraGrupoMaterial"].ToString());

                    if (cmpCoObraGrupoMaterial == cmpCoObraGrupoMaterial_atual)
                    {
                        txtItemMaterial2.Text   = row["cmpDcItem"].ToString();
                        txtTipo2.Text           = row["cmpDcTipoMaterial"].ToString();
                        txtUnidade2.Text        = row["cmpDcUnidade"].ToString();
                        txtPrecoMaterial2.Text  = row["cmpVlPrecoUnitario"].ToString();
                        txtDescricao2.Text      = row["cmpDcMaterialObraGrupo"].ToString();
                        lblSerMat2.Text         = row["cmpDcTpMaterial"].ToString();
                        txtQtUtilizadaObra.Text = row["cmpNuQtdAtual"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        ///  Retorna materiais de referência (tblMaterial)
        ///  que não estão cadastrados na obra (tblObraGrupoMaterial).
        /// </summary>
        private bool CarregaMaterialNotObra()
        {
            try
            {
                using (DataTable table = tblObraGrupoMaterial.RetornarMaterialNotObra(Global.GetConnection(), ViewState["cmpCoObraGrupo"].ToString(), txtMaterial.Text.ToString(), rbdTipo.SelectedValue.ToString()))
                {
                    if (table != null && table.Rows.Count > 0)
                    {
                        Objetos.LoadCombo(lstMaterial0, table, "cmpDescMaterial", "cmpCoMaterial", "cmpDescMaterial", true);
                        lstMaterial0.SelectedIndex = 0;
                        lstMaterial0_SelectedIndexChanged(lstMaterial0, EventArgs.Empty);
                        return true;
                    }
                    else
                    {
                        lstMaterial0.Items.Clear();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
                return false;
            }
        }

        /// <summary>
        /// Exibe na página os materiais de referência (tblMaterial) que não estão cadastrados 
        /// na obra.
        /// </summary>
        private void CarregaDadosMaterialNotObra()
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoMaterial";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ViewState["cmpCoMaterial"].ToString();
                ls.Add(lc);

                using (DataTable table = tblMaterial.Get(Global.GetConnection(), ls))
                {
                    txtItemMaterial1.Text   = table.Rows[0]["cmpDcItemMaterial"].ToString();
                    txtTipo1.Text           = table.Rows[0]["cmpDcTipoMaterial"].ToString();
                    txtUnidade1.Text        = table.Rows[0]["cmpDcUnidade"].ToString(); 
                    txtPrecoMaterial1.Text  = table.Rows[0]["cmpVlPrecoUnitario"].ToString();
                    txtDescricao1.Text      = table.Rows[0]["cmpDcMaterial"].ToString();
                    lblSerMat1.Text         = table.Rows[0]["cmpTpMaterial"].ToString();
                }

            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Limpar campos
        /// </summary>
        private void LimparCampos()
        {
            txtItemMaterial2.Text   = "";
            txtTipo2.Text           = "";
            txtUnidade2.Text        = "";
            txtPrecoMaterial2.Text  = "";
            txtDescricao2.Text      = "";
            lblSerMat2.Text         = "";
            txtQtUtilizadaObra.Text = "";
            txtItemMaterial1.Text   = "";
            txtTipo1.Text           = "";
            txtUnidade1.Text        = "";
            txtPrecoMaterial1.Text  = "";
            txtDescricao1.Text      = "";
            lblSerMat1.Text         = "";
            txtQtContrato.Text      = "";
            txtQtUtilizadaAgrupada.Text = "";
            txtQtEstoque.Text       = "";
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Evento ao carregar a página.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                loadObra();
            }
            else
            {
                lstMaterial0.SelectedIndex = lstMaterial0.SelectedIndex;
                lstMaterial1.SelectedIndex = lstMaterial1.SelectedIndex;
            }
        }

        /// <summary>
        /// Evento ao selecionar obra.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lstMaterial0.Items.Clear();
                lstMaterial1.Items.Clear();
                txtMaterial.Text = "";
                txtMaterial.Focus();

                ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista = cmbObra.SelectedValue;

                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoObraGrupoLista";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoObraGrupoLista;
                ls.Add(lc);

                using (DataTable table = tblObraUsuario.Get(Global.GetConnection(), ls))
                {
                    ViewState["cmpCoObraGrupo"] = table.Rows[0]["cmpCoObraGrupo"].ToString();
                    ViewState["cmpInLogoObra"]  = table.Rows[0]["cmpInLogoObra"].ToString();
                }

                LimparCampos();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Evento ao selecionar material de referência.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lstMaterial0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMaterial0.SelectedValue != "")
            {
                ViewState["cmpCoMaterial"] = lstMaterial0.SelectedValue;
                CarregaDadosMaterialNotObra();
            }
        }

        /// <summary>
        /// Evento ao selecionar material da obra.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lstMaterial1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (lstMaterial1.SelectedValue != "")
            {
                ViewState["cmpCoObraGrupoMaterial"] = lstMaterial1.SelectedValue;
                CarregaDadosMaterialObra();
            }
        }

        /// <summary>
        /// Evento ao selecionar tipo de material (Material/Serviço/Ambos).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rdbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        /// <summary>
        /// Evento ao vincular material de referência (tblMaterial) com a Obra (tblObraGrupoMaterial).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnVincular_Click(object sender, EventArgs e)
        {
            tblObraGrupoMaterial table = new tblObraGrupoMaterial();
            bool ok = false;
            try
            {
                foreach (ListItem item in lstMaterial0.Items)
                {
                    if (item.Selected)
                    {
                        ViewState["cmpCoMaterial"] = item.Value;
                        CarregaDadosMaterialNotObra(); 

                        table.cmpCoObraGrupoMaterial    = "0";
                        table.cmpCoObraGrupo            = ViewState["cmpCoObraGrupo"].ToString();
                        table.cmpCoMaterial             = ViewState["cmpCoMaterial"].ToString();
                        table.cmpDcItem                 = txtItemMaterial1.Text;
                        table.cmpDcMaterialObraGrupo    = txtDescricao1.Text.ToUpper();
                        table.cmpNuQtdContrato          = "0";
                        table.cmpVlPrecoUnitario        = txtPrecoMaterial1.Text.Replace(",",".");
                        table.cmpNoUsuario              = ((HzLogin)Session["login"]).cmpNoUsuario;

                        if (!(ok = table.GravarObraGrupoMaterial(Global.GetConnection())))
                            throw new Exception("Erro ao cadastrar o(s) material(ais) da obra!");
                    }
                }

                CarregaMaterialNotObra();
                CarregaMaterialObra();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Evento ao desvincular material da Obra (tblObraGrupoMaterial).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDesvincular_Click(object sender, EventArgs e)
         {
             try
             {
                 tblObraGrupoMaterial table = new tblObraGrupoMaterial();
                 string mensagem = "";

                 foreach (ListItem item in lstMaterial1.Items)
                {
                    if (item.Selected)
                    {
                        table.cmpCoObraGrupoMaterial = item.Value.ToString();
                        table.cmpCoObraGrupo         = ViewState["cmpCoObraGrupo"].ToString();
                        table.cmpNoUsuario           = ((HzLogin)Session["login"]).cmpNoUsuario;

                        table.ExcluirObraGrupoMaterial(Global.GetConnection(), ref mensagem);
                        if (mensagem != "")
                            CaixaMensagem.Mostar(Mensagem.Tipo.Erro, mensagem);
                        else
                        {
                            CaixaMensagem.Mostar(Mensagem.Tipo.Erro, "Material excluído da obra!");
                            LimparCampos();
                            CarregaMaterialNotObra();
                            if (lstMaterial1.Items.Count == 1)
                            {
                                lstMaterial1.Items.Clear();
                                break;
                            }
                            else
                                CarregaMaterialObra();
                        }
                    }
                }
             }
             catch (Exception ex)
             {
                 Global.ShowError(Global.Title, ex);
             }
         }

        /// <summary>
        /// Evento ao selecionar botão pesquisar material.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaterial.Text.Length >= 3)
                    PesquisaMaterial();
                else
                {
                    Global.ShowMensager(Global.Title, "Informe no mínimo 3 digitos para pesquisa!");
                    txtMaterial.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        /// <summary>
        /// Evento ao selecionar botão gravar. Alterar dados do material na obra (tblObraGrupoMaterial) 
        /// e dados de estoque de material (tblObraGrupoMaterialEstoque)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            tblObraGrupoMaterial table = new tblObraGrupoMaterial();
            bool ok = false;
            try
            {
                table.cmpCoObraGrupoMaterial    = ViewState["cmpCoObraGrupoMaterial"].ToString();
                table.cmpDcItem                 = txtItemMaterial2.Text;
                table.cmpDcMaterialObraGrupo    = txtDescricao2.Text;
                table.cmpNuQtdContrato          = txtQtContrato.Text.Replace(",", ".");
                table.cmpVlPrecoUnitario        = txtPrecoMaterial2.Text.Replace(",", ".");
                table.cmpNoUsuario              = ((HzLogin)Session["login"]).cmpNoUsuario;

                if (!(ok = table.AlterarObraGrupoMaterial(Global.GetConnection())))
                {
                    throw new Exception("Erro ao alterar dados do material da obra!");
                }
                CarregaDadosMaterialObra();
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

       #endregion

        protected void txtItemMaterial2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtTipo2_TextChanged(object sender, EventArgs e)
        {

        }

    }
    
}