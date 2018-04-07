using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibGeneral.Util;
using HzLibCorporativo.Faturamento;
using HzLibConnection.Sql;
using System.Data;

namespace HzWebManutencao.Faturamento
{
    public partial class webFAT_Faturamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();

                lc = new ListCampo();
                lc.NomeCampo = "cmpInEmiteNotaMes";
                lc.TipoCampo = TipoCampo.Numero;
                lc.Sinal = SinalPesquisa.Igual;
                lc.ValorCampo = "1";
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "cmpInAtivo";
                lc.TipoCampo = TipoCampo.String;
                lc.ValorCampo = "true";
                ls.Add(lc);


                HzLogin login;
                login = (HzLogin)Session["Login"];
                DataTable dtObras = tblFaturaObra.GetObrasUsuario(Global.GetConnection(), ls, int.Parse(login.cmpCoUsuario));

                for (int i = 0; i < dtObras.Rows.Count; i++)
                {
                    cmbListaObras.Items.Add(new ListItem(dtObras.Rows[i]["cmpNoObraFAtura"].ToString(), dtObras.Rows[i]["cmpCoObra"].ToString()));
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string nomeRel;
            RelatoriosReportServer.clRelatorios rel = new RelatoriosReportServer.clRelatorios();
            nomeRel = rel.FaturaNAORecebida(int.Parse(cmbListaObras.SelectedValue.ToString()), int.Parse(cmbAno.Text), int.Parse(cmbMesInicial.Text), int.Parse(cmbMesFinal.Text));
            this.Response.Write("<SCRIPT language=javascript>window.open('../" + nomeRel + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');</SCRIPT>");
        }

        protected void btnContasRecebidas_Click(object sender, EventArgs e)
        {
            string nomeRel;
            RelatoriosReportServer.clRelatorios rel = new RelatoriosReportServer.clRelatorios();
            nomeRel = rel.FaturaRecebida(int.Parse(cmbListaObras.SelectedValue.ToString()), int.Parse(cmbAno.Text), int.Parse(cmbMesInicial.Text), int.Parse(cmbMesFinal.Text));
            this.Response.Write("<SCRIPT language=javascript>window.open('../" + nomeRel + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');</SCRIPT>");
        }
    }
}