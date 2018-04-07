using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibManutencao;
using System.Data;

namespace HzWebManutencao.Horizon
{
    public partial class wucEquipamentosObra : System.Web.UI.UserControl
    {
        public int cmpCoObraPavimento {get;set;}
        public int  cmpIdEquipamento {get;set;}
        public int cmpCoGrupoAtividade {get;set;}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != false)
            {

            }
        }

        protected void btnPesquisarEquipamento_Click(object sender, EventArgs e)
        {

        }
        public void PesquisarEquipamentos()
        {
            DataTable dtEquipObra = tblEquipamentoObra.GetPavimentoObra(Global.GetConnection(), cmpCoObraPavimento, cmpIdEquipamento, cmpCoGrupoAtividade);
            dgvEquipamento.DataSource = dtEquipObra;
            dgvEquipamento.DataBind();
        }
        protected void btnCancelarEquipamento_Click(object sender, EventArgs e)
        {

        }

        protected void dgvEquipamento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void cmbTipoEquip_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}