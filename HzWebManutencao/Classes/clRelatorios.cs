using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Configuration;

namespace RelatoriosReportServer
{
    public class clRelatorios
    {
        private System.Web.UI.Page Pagina;
        public clRelatorios()
        {

        }
        public clRelatorios(System.Web.UI.Page pagina)
        {
            Pagina = pagina;
        }
        public string  ManutencaoPreventiva(int cmpCoObraGrupoLista, DateTime cmpDtInicial, DateTime cmpDtFinal)
        {
            string nomeRelatorio = @"/HzManutencao/Relatorios/spRetornaManutencaoPreventiva";
            string dataIn = cmpDtInicial.Year + @"-" + cmpDtInicial.Month + @"-" + cmpDtInicial.Day;
            string dataFim = cmpDtFinal.Year + @"-" + cmpDtFinal.Month + @"-" + cmpDtFinal.Day;

            ReportParameter[] parameters = new ReportParameter[3];
            parameters[0] = new ReportParameter("cmpCoObraGrupoLista", cmpCoObraGrupoLista.ToString());
            parameters[1] = new ReportParameter("cmpDtInicial", dataIn);
            parameters[2] = new ReportParameter("cmpDtFinal",dataFim);

            clExportarRelatorio ex = new clExportarRelatorio();
            return   ex.ExportarRelatorio(nomeRelatorio, clExportarRelatorio.enTipoRelatorio.PDF, parameters);

        }
        public string ManutencaoPreventivaNumPreventiva(string PreventivaAgenda, string cmpDtInicial, string cmpDtFinal)
        {
            string nomeRelatorio = @"/HzManutencao/Relatorios/spRelatorioManutencaoPreventiva";
            //string dataIn = cmpDtInicial.Year + @"-" + cmpDtInicial.Month + @"-" + cmpDtInicial.Day;
            //string dataFim = cmpDtFinal.Year + @"-" + cmpDtFinal.Month + @"-" + cmpDtFinal.Day;

            ReportParameter[] parameters = new ReportParameter[3];
            parameters[0] = new ReportParameter("PreventivaAgenda", PreventivaAgenda);
            parameters[1] = new ReportParameter("PeriodoInicial", cmpDtInicial);
            parameters[2] = new ReportParameter("PeriodoFinal", cmpDtFinal);

            clExportarRelatorio ex = new clExportarRelatorio();
            return ex.ExportarRelatorio(nomeRelatorio, clExportarRelatorio.enTipoRelatorio.PDF, parameters);

        }
        public string EquipamentoOS(string IdEquipamentoObra)
        {
            string nomeRelatorio = @"/HzManutencao/Relatorios/relOsEquipamentos";

            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("IdEquipamentoObra", IdEquipamentoObra);

            clExportarRelatorio ex = new clExportarRelatorio();
            return ex.ExportarRelatorio(nomeRelatorio, clExportarRelatorio.enTipoRelatorio.PDF, parameters);

        }
        public string spRelatorioMenuPermissoes()
        {
            string nomeRelatorio = @"/HzManutencao/Relatorios/spRelatorioMenuPermissoes";


            clExportarRelatorio ex = new clExportarRelatorio();
            return ex.ExportarRelatorio(nomeRelatorio, clExportarRelatorio.enTipoRelatorio.PDF);
        }
        public string RelatorioUsuariosAcessos()
        {
            string nomeRelatorio = @"/HzManutencao/Relatorios/RelatorioUsuariosAcessos";


            clExportarRelatorio ex = new clExportarRelatorio();
            return ex.ExportarRelatorio(nomeRelatorio, clExportarRelatorio.enTipoRelatorio.PDF);
        }
        public string ManutencaoPreventivaNumPreventivaObra(int CoObraGrupoLista, string cmpDtInicial, string cmpDtFinal,string NomeObra)
        {
            string nomeRelatorio = @"/HzManutencao/Relatorios/spRelatorioManutencaoPreventivaObra";
            //string dataIn = cmpDtInicial.Year + @"-" + cmpDtInicial.Month + @"-" + cmpDtInicial.Day;
            //string dataFim = cmpDtFinal.Year + @"-" + cmpDtFinal.Month + @"-" + cmpDtFinal.Day;

            DateTime dataIn = DateTime.Parse(cmpDtInicial + " 00:00:00", CultureInfo.CurrentCulture);
            DateTime dataFim = DateTime.Parse(cmpDtFinal + " 00:00:00", CultureInfo.CurrentCulture);

            ReportParameter[] parameters = new ReportParameter[4];
            parameters[0] = new ReportParameter("CoObraGrupoLista", CoObraGrupoLista.ToString());
            parameters[1] = new ReportParameter("PeriodoInicial",dataIn.ToShortDateString());
            parameters[2] = new ReportParameter("PeriodoFinal", dataFim.ToShortDateString());
            parameters[3] = new ReportParameter("NomeObra", NomeObra);

            clExportarRelatorio ex = new clExportarRelatorio();
            return ex.ExportarRelatorio(nomeRelatorio, clExportarRelatorio.enTipoRelatorio.PDF, parameters);

        }
        public string CalendarioPreventiva(int CoObraGrupoLista, string cmpDtInicial, string coTipoAtividade, string coPeriodicidade)
        {
            string nomeRelatorio = @"/HzManutencao/Relatorios/spCalendarioPreventivo2";


            ReportParameter[] parameters = new ReportParameter[4];
            parameters[0] = new ReportParameter("CoObraGrupoLista", CoObraGrupoLista.ToString());
            parameters[1] = new ReportParameter("Data", cmpDtInicial);
            parameters[2] = new ReportParameter("CoTipoAtividade", coTipoAtividade);
            parameters[3] = new ReportParameter("cmpCoPeriodicidade", coPeriodicidade);

            clExportarRelatorio ex = new clExportarRelatorio();
            return ex.ExportarRelatorio(nomeRelatorio, clExportarRelatorio.enTipoRelatorio.PDF, parameters);

        }
        public string CalendarioPreventivaANO(int CoObraGrupoLista, string cmpDtInicial, string coTipoAtividade)
        {
            string nomeRelatorio = @"/HzManutencao/Relatorios/spCalendarioPreventivoANO";


            ReportParameter[] parameters = new ReportParameter[3];
            parameters[0] = new ReportParameter("CoObraGrupoLista", CoObraGrupoLista.ToString());
            parameters[1] = new ReportParameter("Data", cmpDtInicial);
            parameters[2] = new ReportParameter("CoTipoAtividade", coTipoAtividade);

            clExportarRelatorio ex = new clExportarRelatorio();
            return ex.ExportarRelatorio(nomeRelatorio, clExportarRelatorio.enTipoRelatorio.PDF, parameters);

        }
        public string FaturaRecebida(int cmpCoObra,int Ano, int MesInicial, int MesFinal)
        {
            string nomeRelatorio = @"/HzManutencao/Relatorios/RelatorioFaturaRecebida";
            string situacao = "RT,RP,RG";
            ReportParameter[] parameters = new ReportParameter[3];
            parameters[0] = new ReportParameter("Ano", Ano.ToString());
            parameters[1] = new ReportParameter("Obra", cmpCoObra.ToString());
            parameters[2] = new ReportParameter("MesInicial", MesInicial.ToString());
            parameters[2] = new ReportParameter("MesFinal", MesFinal.ToString());
            //parameters[2] = new ReportParameter("SituracaoRecebeNota", situacao);

            clExportarRelatorio ex = new clExportarRelatorio();
            return ex.ExportarRelatorio(nomeRelatorio, clExportarRelatorio.enTipoRelatorio.PDF, parameters);

        }
        public string FaturaNAORecebida(int cmpCoObra, int Ano, int MesInicial, int MesFinal)
        {
            string nomeRelatorio = @"/HzManutencao/Relatorios/RelatorioFaturaNAORecebida";

            ReportParameter[] parameters = new ReportParameter[3];
            parameters[0] = new ReportParameter("Ano", Ano.ToString());
            parameters[1] = new ReportParameter("Obra", cmpCoObra.ToString());
            parameters[2] = new ReportParameter("MesInicial", MesInicial.ToString());
            parameters[2] = new ReportParameter("MesFinal", MesFinal.ToString());
            //parameters[2] = new ReportParameter("SituracaoRecebeNota", situacao);

            clExportarRelatorio ex = new clExportarRelatorio();
            return ex.ExportarRelatorio(nomeRelatorio, clExportarRelatorio.enTipoRelatorio.PDF, parameters);

        }
        public string QRCODE(string CoEquipamentoObra, int CoObraGrupoLista)
        {
            string nomeRelatorio = @"/HzManutencao/Relatorios/RelQRCODE";

            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("CoEquipamentoObra", CoEquipamentoObra);
            parameters[1] = new ReportParameter("CoObraGrupoLista", CoObraGrupoLista.ToString());

            clExportarRelatorio ex = new clExportarRelatorio();
            return ex.ExportarRelatorio(nomeRelatorio, clExportarRelatorio.enTipoRelatorio.PDF, parameters);

        }
        public string FechamentoMesSemMaterial(string CoObraGrupoLista, string DataAbertura, string DtAberturaF, string StOS)
        {
            string nomeRelatorio = @"/HzManutencao/Relatorios/vwFechamaneMesSMaterial";

            ReportParameter[] parameters = new ReportParameter[4];
            parameters[0] = new ReportParameter("CoObraGrupoLista", CoObraGrupoLista.ToString());
            parameters[1] = new ReportParameter("DataAbertura", DataAbertura.ToString());
            parameters[2] = new ReportParameter("cmpDtAberturaF", DtAberturaF.ToString());
            parameters[3] = new ReportParameter("StOS", StOS);

            clExportarRelatorio ex = new clExportarRelatorio();

            return ex.ExportarRelatorio(nomeRelatorio, clExportarRelatorio.enTipoRelatorio.PDF, parameters);

        }
    }
}