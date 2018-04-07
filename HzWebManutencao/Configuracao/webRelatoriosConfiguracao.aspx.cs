using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HzWebManutencao.Configuracao
{
    public partial class webRelatoriosConfiguracao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUsuariosAcessos_Click(object sender, EventArgs e)
        {
            string nomeRel;
            RelatoriosReportServer.clRelatorios rel = new RelatoriosReportServer.clRelatorios();
            nomeRel = rel.spRelatorioMenuPermissoes();
            this.Response.Write("<SCRIPT language=javascript>window.open('../" + nomeRel + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');</SCRIPT>");
        }

        protected void btnRelPermissaoGrupo_Click(object sender, EventArgs e)
        {
            string nomeRel;
            RelatoriosReportServer.clRelatorios rel = new RelatoriosReportServer.clRelatorios();
            nomeRel = rel.RelatorioUsuariosAcessos();
            this.Response.Write("<SCRIPT language=javascript>window.open('../" + nomeRel + "', '_blank', 'width=850, height=600, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');</SCRIPT>");
        }
    }
}