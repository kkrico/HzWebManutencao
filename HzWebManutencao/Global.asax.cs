using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Data;
using System.IO;

using HzLibConnection.Data;
using System.Configuration;

namespace HzWebManutencao
{
    public class Global : System.Web.HttpApplication
    {
        public static string Title = "Horizon";
        /// <summary>
        /// Marca solicitação localhost.
        /// </summary>
        //public static bool   IsLocal        { get; set; }
        public static string UrlRelatorio   { get; set; }
        public static string UrlDocWord     { get; set; }

        public static string PastaArqUpLoad = string.Empty;
        //public static string PastaArqAdmitido  = string.Empty;
        //public static string PastaArqEnviado   = string.Empty;

        /// <summary>
        /// Verifica o login do usuário.
        /// </summary>
        /// <param name="username">Nome do usuário.</param>
        /// <param name="password">Senha do usuário.</param>
        /// <returns></returns>
        public static DataTable CheckUserContratante(string username, string password)
        {
            HttpContext.Current.Session["connection"] = new HzConexao(ConfigurationManager.ConnectionStrings[1].ToString(), "System.Data.SqlClient");
            DataTable table = HzLibCorporativo.Config.tblUsuario.ValidarUsuarioContratante((HzConexao)HttpContext.Current.Session["connection"], username, password);

            return table;
        }

        /// <summary>
        /// Verifica o login do usuário.
        /// </summary>
        /// <param name="username">Nome do usuário.</param>
        /// <param name="password">Senha do usuário.</param>
        /// <returns></returns>
        public static DataTable CheckUserManto(string chavemanto)
        {
            HttpContext.Current.Session["connection"] = new HzConexao(ConfigurationManager.ConnectionStrings[1].ToString(), "System.Data.SqlClient");
            DataTable table = HzLibCorporativo.Config.tblUsuario.ValidarUsuarioManto((HzConexao)HttpContext.Current.Session["connection"], chavemanto);

            return table;
        }

        /// <summary>
        /// Verifica o login do usuário.
        /// </summary>
        /// <param name="username">Nome do usuário.</param>
        /// <param name="password">Senha do usuário.</param>
        /// <returns></returns>
        public static DataTable CheckUser(string username, string password)
        {
            HttpContext.Current.Session["connection"] = new HzConexao(ConfigurationManager.ConnectionStrings[1].ToString(), "System.Data.SqlClient");
            DataTable table = HzLibCorporativo.Config.tblUsuario.ValidarUsuario((HzConexao)HttpContext.Current.Session["connection"], username, password);

            return table;
        }

        /// <summary>
        /// Retorna o objeto de conexão.
        /// </summary>
        /// <returns></returns>
        public static HzConexao GetConnection()
        {
            if (HttpContext.Current.Session["Connection"] == null)
                HttpContext.Current.Session["Connection"] = new HzConexao(ConfigurationManager.ConnectionStrings[1].ToString(), "System.Data.SqlClient");
            return (HzConexao)HttpContext.Current.Session["Connection"];
        }

        /// <summary>
        /// Retorna o objeto de conexão.
        /// </summary>
        /// <returns></returns>
        public static HzConexao GetConnectionManto()
        {
            if (HttpContext.Current.Session["ConnectionManto"] == null)
                HttpContext.Current.Session["ConnectionManto"] = new HzConexao(ConfigurationManager.ConnectionStrings[2].ToString(), "System.Data.SqlClient");
            return (HzConexao)HttpContext.Current.Session["ConnectionManto"];
        }

        public static string PathArquivos(string PathObra)
        {
            if (!Directory.Exists(@PathObra))
                Directory.CreateDirectory(@PathObra);
            
            return @PathObra;

        }


        /// <summary>
        /// Retorna url atual.
        /// </summary>
        /// <returns></returns>
        public static void UrlRel()
        {
            if (HttpContext.Current.Request.Url.AbsoluteUri.StartsWith("http://localhost"))
                UrlRelatorio = "http://localhost:" + HttpContext.Current.Request.Url.Port.ToString() + "/Relatorios/";

            //else
            //{
            //    UrlRelatorio = "http://172.10.10.2/HzWebManutencao_Desenv/Relatorios/";
            //    UrlDocWord = "http://172.10.10.2/HzWebManutencao_Desenv/Documentos/2014/carta/";
            //}
            else
                UrlRelatorio = "http://201.39.115.18/HzWEBManutencao/Relatorios/";


        }

        public static void ShowError(string title, Exception ex)
        {
            System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"" + ex.Message + "\")</SCRIPT>");
        }

        public static void ShowError(string title, string msg)
        {
            System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"" + msg + "\")</SCRIPT>");
        }

        public static void ShowMensager(string title, string msg)
        {
            System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"" + msg + "\")</SCRIPT>");
        }

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
