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
    public partial class webCON_AlterarSenhaUsu : System.Web.UI.Page
    {

    #region Events

        /// <summary>
        /// Grava dados do Usuario na HzCorporativo..tblUsuario.
        /// </summary>
        protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (tblUsuario.UpdatePassword(Global.GetConnection(), ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoUsuario, ChangeUserPassword.CurrentPassword.ToString(), ChangeUserPassword.NewPassword.ToString()))
                    MsgBox.ShowMessage("Senha alterada com sucesso!", "Aviso");
                else
                    MsgBox.ShowMessage("Senha não alterada!", "Erro");
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

    #endregion

    }

}