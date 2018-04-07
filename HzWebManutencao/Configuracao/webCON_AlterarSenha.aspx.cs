using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibConnection.Sql;
using HzLibCorporativo.Funcional;
using HzLibCorporativo.Config;
using HzLibManutencao;
using HzlibWEB;

namespace HzWebManutencao.Configuracao
{
    public partial class webCON_AlterarSenha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
 
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string mensagem = "";

            tblUsuario.UpdatePassword(Global.GetConnection(), ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoUsuario, CurrentPassword.Text, NewPassword.Text, ref mensagem);
            if (mensagem != "")
                MsgBox.ShowMessage(mensagem, "Erro");
            else
                MsgBox.ShowMessage("Senha alterada!", "Aviso");
        }

    }
}