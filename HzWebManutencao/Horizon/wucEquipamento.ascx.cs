using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HzLibCorporativo.Funcional;
using HzLibManutencao;
using HzLibConnection.Sql;

namespace HzWebManutencao.Horizon
{
    public partial class wucEquipamento : System.Web.UI.UserControl
    {
        public string cmpIdEquipamento;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true)
            {

                CarregarDados();

            }
        }
        private void CarregarDados()
        {
            ListCampos ls = new ListCampos();
            ListCampo lc = new ListCampo();

            lc.NomeCampo = "cmpIdEquipamentoObra";
            lc.TipoCampo = TipoCampo.Numero;
            lc.Sinal = SinalPesquisa.Igual;
            lc.ValorCampo = cmpIdEquipamento;
            ls.Add(lc);

            DataTable dt = tblEquipamentoObra.Get(Global.GetConnection(),ls);
            if (dt.Rows.Count > 0)
            {
                TxtDescricao.Text = dt.Rows[0]["cmpDcEquipamentoObra"].ToString();
                txtCapacidade.Text = dt.Rows[0]["cmpQtCapacidadeEquipamento"].ToString();
                txtCodigoEquip.Text = dt.Rows[0]["CodEquipamento"].ToString();
                txtLocalizacao.Text = dt.Rows[0]["cmpDcLocalEquipamento"].ToString();
                txtMarcaModelo.Text = dt.Rows[0]["cmpDcMarcaModeloEquipamento"].ToString();
                txtNumeroPatrimonio.Text = dt.Rows[0]["cmpNuPatrimonio"].ToString();
                txtNumeroSerie.Text = dt.Rows[0]["cmpNuSerieEquipamento"].ToString();
                txtObservacao.Text = dt.Rows[0]["cmpDcObsEquipamento"].ToString();
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}