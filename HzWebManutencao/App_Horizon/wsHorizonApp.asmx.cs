using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using HzLibConnection.Data;
using System.Configuration;
using HzLibCorporativo.Config;
using HzLibConnection.Sql;
using HzLibCorporativo.Funcional;
using HzLibManutencao;

namespace HzWebManutencao.App_Horizon
{
    /// <summary>
    /// Summary description for wsHorizonApp
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsHorizonApp : System.Web.Services.WebService
    {

        [WebMethod]
        public string UltimaPreventiva(int IdEquipamento)
        {
            return HzLibManutencao.tblAppPreventiva.UltimaPreventiva( Conectar(), IdEquipamento);
            
        }
        [WebMethod]
        public string SalvarPreventiva(int IdEquipamento, string cmpNomeTecnico, string cmpDescricaoServ)
        {
            return HzLibManutencao.tblAppPreventiva.SalvarPreventiva(Conectar(),IdEquipamento, cmpNomeTecnico, cmpDescricaoServ);

        }
        [WebMethod(EnableSession=true)]
        public DataTable Login(string login, string senha)
        {
            DataTable table = Global.CheckUserContratante(login, senha);
  
           if(table.Rows.Count==1)
           {
               string cmpcousuario = table.Rows[0]["cmpcousuario"].ToString().Trim();
               //Application.Add("cmpCousuario", cmpcousuario);
               Session[cmpcousuario] = cmpcousuario;
               
               return table;
           }else {return null;}
        }
        [WebMethod(EnableSession = true)]
        public DataTable DadosUsuario(string Cousuario)
        {
            if (Session[Cousuario] != null)
            {
                //HzConexao conec = new HzConexao(ConfigurationManager.ConnectionStrings[1].ToString(), "System.Data.SqlClient");

                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCousuario";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = Session[Cousuario].ToString();
                ls.Add(lc);

                DataTable dtDadosUser= tblUsuario.GetDadosUser(Global.GetConnection(), ls);
                return dtDadosUser;
            }
            else { return null; }
        }
        [WebMethod(EnableSession = true)]
        public DataTable EmpresasUsuario(string Cousuario)
        {
            if (Session[Cousuario] != null)
            {
                DataTable dtObraUser = tblObraUsuario.Get(Global.GetConnection(), Session[Cousuario].ToString());
                return dtObraUser;
            }
            else
            { return null; }
        }
        public HzLibConnection.Data.HzConexao Conectar()
        {
            HzLibConnection.Data.HzConexao conec=new HzLibConnection.Data.HzConexao(@"Server=172.10.10.2\prod; Database=HzManutencao; User Id=sa; password= rona3007","System.Data.SqlClient");
            return conec;
        }

   
    }
}
