using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using HzLibConnection.Sql;
using HzLibManutencao;

namespace HzWebManutencao.App_Horizon
{
    /// <summary>
    /// Summary description for wsHorizonEquipamentos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsHorizonEquipamentos : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public bool Login(string login, string senha)
        {
            DataTable table = Global.CheckUserContratante(login, senha);

            if (table.Rows.Count == 1)
            {
                string cmpcousuario = table.Rows[0]["cmpcousuario"].ToString().Trim();
                //Application.Add("cmpCousuario", cmpcousuario);
                Session["cmpcousuario"] = cmpcousuario;

                return true;
            }
            else { return false; }
        }
        [WebMethod(EnableSession = true)]
        public DataTable EquipamentoObra(int IdEquipamentoObra)
        {

            if (Session["cmpcousuario"] != null)
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpIdEquipamentoObra";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = IdEquipamentoObra.ToString();
                ls.Add(lc);

                DataTable dtEqObra = tblEquipamentoObra.RetornarEquipamentoObra(Global.GetConnection(), ls);
                return dtEqObra;
            }
            else { return null; }
        }
        [WebMethod(EnableSession = true)]
        public DataTable EquipamentoObraCoEquipamento(string cmpCoEquipamentoObra)
        {

            if (Session["cmpcousuario"] != null)
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoEquipamentoObra";
                lc.TipoCampo = TipoCampo.String;
                lc.ValorCampo = cmpCoEquipamentoObra;
                ls.Add(lc);

                DataTable dtEqObra = tblEquipamentoObra.RetornarEquipamentoObra(Global.GetConnection(), ls);
                return dtEqObra;
            }
            else { return null; }
        }
        [WebMethod(EnableSession = true)]
        public DataTable EquipamentoPreventiva(int IdEquipamentoObra, string DtReprogramacaoPreventivaAgenda)
        {

            if (Session["cmpcousuario"] != null)
            {
             
                DataTable dtEqObra = tblEquipamentoObra.RetornarEquipamentoPreventiva(Global.GetConnection(), IdEquipamentoObra, DtReprogramacaoPreventivaAgenda);
                return dtEqObra;
            }
            else { return null; }
        }
    }
}
