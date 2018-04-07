using System;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Net;
using System.IO;

namespace RelatoriosReportServer
{
    public class clExportarRelatorio
    {
        public enum enTipoRelatorio { PDF, WORD, EXCEL }
        public enTipoRelatorio TipoRelatorio;

        public void ExportarRelatorio1(string NomeRelatorio, enTipoRelatorio TipoRel)
        {
            ServerReport sReport = new ServerReport();
            sReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["Relatorios"]);
            sReport.ReportPath = NomeRelatorio;
            Byte[] mybytes1 = sReport.Render(TipoRel.ToString());
        }
        public string ExportarRelatorio(string NomeRelatorio, enTipoRelatorio TipoRel)
        {
            ServerReport sReport = new ServerReport();
            sReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["Relatorios"]);
            sReport.ReportPath = NomeRelatorio;

            sReport.ReportServerCredentials = new MyCredentials("saRelatorio", "rona3007");
            //sReport.ReportServerCredentials = new MyCredentials("", "");

            Byte[] mybytes1 = sReport.Render(TipoRel.ToString());

            return SalvarRelatorio(mybytes1, TipoRel);

        }
        public string ExportarRelatorio(string NomeRelatorio, enTipoRelatorio TipoRel, ReportParameter[] parameters)
        {
            ServerReport sReport = new ServerReport();
            sReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["Relatorios"]);
            sReport.ReportPath = NomeRelatorio;

            sReport.ReportServerCredentials = new MyCredentials("saRelatorio", "rona3007");
            //sReport.ReportServerCredentials = new MyCredentials("", "");

            sReport.SetParameters(parameters);
            Byte[] mybytes1 = sReport.Render(TipoRel.ToString());

            return SalvarRelatorio(mybytes1, TipoRel);

        }
        public void ExportarRelatorio(System.Web.UI.Page Pagina, string NomeRelatorio, enTipoRelatorio TipoRel, ReportParameter[] parameters)
        {
            ServerReport sReport = new ServerReport();
            sReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["Relatorios"]);
            sReport.ReportPath = NomeRelatorio;

            sReport.ReportServerCredentials = new MyCredentials("", "");

            sReport.SetParameters(parameters);
            Byte[] mybytes1 = sReport.Render(TipoRel.ToString());

            string nomeRel= SalvarRelatorio(mybytes1, TipoRel);

            nomeRel = nomeRel.Replace("\\", "/");

            Pagina.Response.Write("<SCRIPT language=javascript>window.open('" + nomeRel + "', '_blank', 'width=1200, height=900, menubar=no, resizable=yes, scrollbars=yes, top=35, left=105');</SCRIPT>");

        }
        private string SalvarRelatorio(Byte[] bytRelatorio,enTipoRelatorio TipoRel)
        {
            Random rd = new Random(10);
            string diretorio = ConfigurationManager.AppSettings["ArquivosRelatorios"];
            ExcluirArquivos(diretorio);

            string nomeRelatorio =  + rd.Next() + ExtenssaoArq(TipoRel) ;
            string dirRelatorio = diretorio + @"\" + nomeRelatorio;

            System.IO.FileStream pdfFile = new System.IO.FileStream(dirRelatorio, System.IO.FileMode.Create);
            pdfFile.Write(bytRelatorio, 0, bytRelatorio.Length);
            pdfFile.Close();

            return ConfigurationManager.AppSettings["EndRelatorios"] + @"/" +  nomeRelatorio;

        }
        private string ExtenssaoArq(enTipoRelatorio tipo)
        {
            switch (tipo)
            {
                case enTipoRelatorio.PDF:
                    return ".pdf";
                    break;
                case enTipoRelatorio.WORD:
                    return ".doc";
                    break;
                case enTipoRelatorio.EXCEL:
                    return ".xls";
                    break;
                default:
                    break;
            }
            return "";
        }
        private void ExcluirArquivos(string diretorio)
        {
            string[] Arquivos = Directory.GetFiles(diretorio);
            for (int i = 0; i < Arquivos.Length; i++)
            {
                string arq = Arquivos[i];
                try
                {
                    File.Delete(diretorio + @"\\" + arq);
                }
                catch (Exception)
                {
                    
                }
            }

        }

    }
}