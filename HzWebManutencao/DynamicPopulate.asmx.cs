using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using HzLibConnection.Sql;
using HzLibConnection.Data;
using HzLibCorporativo.Funcional;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;

namespace HzWebManutencao
{
    /// <summary>
    /// Summary description for DynamicPopulate
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[System.ComponentModel.ToolboxItem(false)]

    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]

    public class DynamicPopulate : System.Web.Services.WebService
    {
       
        //[WebMethod]
        //public String[] Busca(string contextkey)
        //{
        //    switch (contextkey)
        //    {
        //        case "Funcionario":
        //            return RetornaFuncionarioObra();
        //            break;
        //        //case "Material":
        //        //    return RetornaDescricaoMaterial(prefixText);
        //        //    break;
        //    }
        //}

        [WebMethod]
        public String[] RetornaFuncionarioObra()
        {
            List<String> res = new List<String>();
            
            using (DataTable table = tblObraFuncionario.Get(Global.GetConnection(), "36"))
            {
                foreach (DataRow row in table.Rows)
                {
                    res.Add(table.Rows[0]["cmpNoFuncionario"].ToString() + ",");
                }
            }

                HttpContext.Current.Cache["Funcionarios"] = res.ToArray();

            return res.ToArray();
        }

        //[WebMethod]
        //[System.Web.Script.Services.ScriptMethod]
        //public string[] GetCompletionList(string prefixText, int count, string contextKey)
        //{

        //    return " ";
        //}
    
    }
}
