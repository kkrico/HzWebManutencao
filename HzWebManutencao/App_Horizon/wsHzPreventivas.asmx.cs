using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using HzLibManutencao;
using HzLibCorporativo.Funcional;

namespace HzWebManutencao.App_Horizon
{
    /// <summary>
    /// Summary description for wsHzPreventivas
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]


    public class wsHzPreventivas : System.Web.Services.WebService
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
        public DataTable AgendaPreventiva()
        {
            if (Session["cmpcousuario"] != null)
            {
                DataTable dtObraUser = tblObraUsuario.Get(Global.GetConnection(), Session["cmpcousuario"].ToString().ToString());
                string obras = "";
                for (int i = 0; i < dtObraUser.Rows.Count; i++)
                {
                    if (obras.Length > 1) { obras += ","; }
                    obras += dtObraUser.Rows[i]["cmpCoObraGrupoLista"].ToString();
                }
                string diaIn = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
                string diaFim = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
                DataTable dtAgenda = tblPreventivaAgenda.RetornaAgendaPreventiva(Global.GetConnection(), obras, diaIn, diaFim);

                return dtAgenda;
            }
            else
            {
                return null;
            }

        }
        [WebMethod(EnableSession = true)]
        public DataTable AgendaPreventiva_CoPreventiva(int IdPreventivaAgenda)
        {
            if (Session["cmpcousuario"] != null)
            {
                DataTable dtAgenda = tblPreventivaAgenda.RetornaAgendaPreventiva(Global.GetConnection(), IdPreventivaAgenda);

                return dtAgenda;
            }
            else
            {
                return null;
            }

        }
        [WebMethod(EnableSession = true)]
        public DataTable Grupo_AgendaPreventiva()
        {
            if (Session["cmpcousuario"] != null)
            {
                DataTable dtObraUser = tblObraUsuario.Get(Global.GetConnection(), Session["cmpcousuario"].ToString().ToString());
                string obras = "";
                for (int i = 0; i < dtObraUser.Rows.Count; i++)
                {
                    if (obras.Length > 1) { obras += ","; }
                    obras += dtObraUser.Rows[i]["cmpCoObraGrupoLista"].ToString();
                }
                string diaIn = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
                string diaFim = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
                DataTable dtAgenda = tblPreventivaAgenda.RetornaAgendaPreventiva(Global.GetConnection(), obras, diaIn, diaFim);

                return dtAgenda;
            }
            else
            {
                return null;
            }

        }
        [WebMethod(EnableSession = true)]
        public DataTable AgendaPreventivaItensAtividade(int IdPreventivaAgenda)
        {
            if (Session["cmpcousuario"] != null)
            {
                DataTable dtAgenda = tblPreventivaAgenda.RetornaAgendaPreventivaITENS(Global.GetConnection(), IdPreventivaAgenda);

                return dtAgenda;
            }
            else
            { return null; }

        }
        [WebMethod(EnableSession = true)]
        public DataTable AgendaPreventivaValoresAtividade(int IdPreventivaAgenda)
        {
            if (Session["cmpcousuario"] != null)
            {
                DataTable dtAgenda = tblPreventivaAgenda.RetornaAgendaPreventivaValores(Global.GetConnection(), IdPreventivaAgenda);

                return dtAgenda;
            }
            else
            { return null; }

        }
        [WebMethod(EnableSession = true)]
        public List<tblItemAtividade> lstAgendaPreventivaValoresAtividade(int IdPreventivaAgenda)
        {
            if (Session["cmpcousuario"] != null)
            {
                DataTable dtAgenda = tblPreventivaAgenda.RetornaAgendaPreventivaValores(Global.GetConnection(), IdPreventivaAgenda);
                List<tblItemAtividade> lstItens = new List<tblItemAtividade>();
                for (int i = 0; i < dtAgenda.Rows.Count; i++)
                {
                    tblItemAtividade it = new tblItemAtividade();
                    it.cmpDcItemAtividade = dtAgenda.Rows[i]["cmpDcItemAtividadePreventiva"].ToString();
                    lstItens.Add(it);
                }
                return lstItens;
            }
            else
            { return null; }
        }
        [WebMethod(EnableSession = true)]
        public bool SetlstAgendaPreventivaValoresAtividade(List<tblPreventivaAtividade> lstListaAtividades)
        {
            if (Session["cmpcousuario"] != null)
            {
                for (int i = 0; i < lstListaAtividades.Count; i++)
                {
                    tblPreventivaAtividade prevAtividade = new tblPreventivaAtividade();
                    prevAtividade.cmpCoPreventivaAtividade = lstListaAtividades[i].cmpCoPreventivaAtividade;
                    prevAtividade.cmpValores = lstListaAtividades[i].cmpValores;
                    prevAtividade.cmpValor = lstListaAtividades[i].cmpValor;
                    prevAtividade.cmpCkValor = lstListaAtividades[i].cmpCkValor;
                    prevAtividade.UpdadeValores(Global.GetConnection());
                }
                return true;
            }
            else { return false; }
        }
        [WebMethod(EnableSession = true)]
        public bool SetlstAgendaPreventivaValoresAtividade1()
        {
            if (Session["cmpcousuario"] != null)
            {
                tblPreventivaAtividade prevAtividade = new tblPreventivaAtividade();
                prevAtividade.cmpCoPreventivaAtividade = "2";
                prevAtividade.cmpValores = true;
                prevAtividade.cmpValor = 10;
                prevAtividade.cmpCkValor = true;
                prevAtividade.UpdadeValores(Global.GetConnection());

                return true;
            }
            else { return false; }
        }
    }
}
