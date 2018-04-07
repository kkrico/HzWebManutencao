using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Data;
using HzLibManutencao;
using HzLibConnection.Sql;
using HzLibCorporativo.Funcional;
using HzlibWEB;
using HzLibCorporativo.Geral;

namespace HzWebManutencao.Financeiro
{
    public partial class webChart : System.Web.UI.Page
    {
        #region Functions
        /// <summary>
        /// Carrega as combos de tipo, atividade e forma de solicitação.
        /// </summary>
        private void load()
        {
            try
            {
                ListCampos ls = new ListCampos();
                ListCampo lc = new ListCampo();
                lc.NomeCampo = "cmpCoLocal";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoLocal;
                ls.Add(lc);

                lc = new ListCampo();
                lc.NomeCampo = "cmpCoUsuario";
                lc.TipoCampo = TipoCampo.Numero;
                lc.ValorCampo = ((HzLibGeneral.Util.HzLogin)Session["login"]).cmpCoUsuario;
                ls.Add(lc);

                using (DataTable table = tblObraUsuario.Get(Global.GetConnection(), ls))
                    Objetos.LoadCombo(cmbObra, table, "cmpNoObra", "cmpCoObraGrupoLista", "cmpNoObra", true);

                //using (DataTable table = tblObraGrupoLista.GetObraUsuario(Global.GetConnection(), ls))
                //    Objetos.LoadCombo(cmbObra, table, "cmpNoObra", "cmpCoObraGrupoLista", "cmpNuOrdemExibicao", true);

                using (DataTable table = tblMes.Get(Global.GetConnection(), new ListCampos()))
                {
                    Objetos.LoadCombo(cmbMesInicial, table, "cmpDcMes", "cmpCoMes", "cmpIdMes", true);
                    Objetos.LoadCombo(cmbMesFinal, table, "cmpDcMes", "cmpCoMes", "cmpIdMes", true);
                    cmbMesFinal.SelectedIndex = Objetos.RetornaIndiceCombo(cmbMesFinal, DateTime.Now.Month);
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }        
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    txtYaerInitial.Text = DateTime.Now.Year.ToString();
                    txtYearFinal.Text = DateTime.Now.Year.ToString();
                    this.load();
                }
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }

        private string getMonth(string comes)
        {
            DataTable table = Global.GetConnection().loadDataTable("select cmpSgMes from HzCorporativo..tblMes where cmpIdMes = " + comes);
            return table.Rows[0]["cmpSgMes"].ToString().Trim();
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    DataTable table = tblOS.RetornarMediaAtendimento(Global.GetConnection(), "1", "1", "2012");

        //    //Chart1.Titles["title1"].Text = "Quantidade de Ordens de Serviço no Período (" + DateTime.Now.Year.ToString() + ")";
        //    //Chart1.Series["Series1"].LegendText = "Qtd.";
        //    //Chart1.Series["Series1"].Label = "#PERCENT{P2}";
        //    //Chart1.Series["Series1"].LegendText = "#VALX(#PERCENT{P2})";//"#VALX (#PERCENT)";
        //    Chart1.Series["Series1"].ChartType = SeriesChartType.Column;
        //    Chart1.Series["Series1"].ChartArea = "ChartArea1";
        //    Chart1.ChartAreas[0].AxisX.Title = "Meses";
        //    Chart1.ChartAreas[0].AxisX.Interval = 1;
        //    Chart1.ChartAreas[0].AxisY.Title = "Qtd.";
        //    foreach (DataRow r in table.Rows)
        //    {
        //        Chart1.Series["Series1"].Points.AddXY(r[0].ToString(), r["qtd"].ToString());
        //    }
        //}

        private Chart setChart(double top, string title, string xtitle, string ytitle, string legendtext, string labeltext, SeriesChartType type, DataTable table, string xname, string yname, string graphname)
        {
            Chart chart = new Chart();
            try
            {
                chart.Series.Add(new Series("Series1"));
                chart.ChartAreas.Add(new ChartArea("ChartArea1"));
                chart.Titles.Add(title);
                chart.Legends.Add(new Legend("Legend1"));

                //chart.Titles["title1"].Text = title;
                chart.Series["Series1"].ChartType = type;
                chart.Series["Series1"].ChartArea = "ChartArea1";
                chart.Series["Series1"].Legend = "Legend1";
                chart.ChartAreas[0].AxisX.Title = xtitle;
                chart.ChartAreas[0].AxisY.Title = ytitle;
                chart.ChartAreas[0].BorderColor = HzUtilClasses.Util.Color.convertColorHTML("#FFFFFF");
                chart.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                chart.ChartAreas[0].BorderWidth = 2;
                chart.BorderlineColor = HzUtilClasses.Util.Color.convertColorHTML("#FFFFFF");
                chart.BorderlineDashStyle = ChartDashStyle.Solid;
                chart.BorderlineWidth = 2;
                chart.Series["Series1"].LegendText = legendtext;
                chart.ChartAreas[0].AxisX.Interval = 1;
                chart.Width = 490;

                foreach (DataRow r in table.Rows)
                {
                    chart.Series["Series1"].Points.AddXY(r[xname].ToString(), r[yname].ToString());
                }

                chart.SaveImage(Server.MapPath(graphname), ChartImageFormat.Jpeg);

                chart.Style[HtmlTextWriterStyle.Position] = "absolute";
                chart.Style[HtmlTextWriterStyle.Top] = top.ToString() + "px";
                chart.Style[HtmlTextWriterStyle.Left] = "550px";
                this.Controls.Add(chart);

                HyperLink hl = new HyperLink();
                hl.NavigateUrl = "~/Financeiro/" + graphname;
                hl.Text = "~/Financeiro/" + graphname;
                hl.Style[HtmlTextWriterStyle.Position] = "absolute";
                hl.Style[HtmlTextWriterStyle.Top] = (top += chart.Height.Value).ToString() + "px";
                hl.Style[HtmlTextWriterStyle.Left] = "550px";
                this.Controls.Add(hl);

                return chart;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void btnGraphic_Click(object sender, EventArgs e)
        {
            string title = "";
            string xtitle = "";
            string ytitle = "";
            string legendtext = "";
            string labeltext = "";
            string graphname = "";
            double top = 400;
            DataTable table = null;
            Chart chart = null;
            try
            {
                foreach (ListItem item in chkGraphic.Items)
                {
                    if (item.Selected)
                    {
                        switch (item.Value)
                        {
                            case "nabsolutomes":
                                title = "N. absolutos de ordens de serviço por mês";
                                xtitle = "meses";
                                ytitle = "qtd.";
                                legendtext = "Qtd.";
                                table = tblOS.RetornarNOSPeriodo(Global.GetConnection(), cmbObra.SelectedValue, "01/" + cmbMesInicial.SelectedValue.ToString() + "/" + txtYaerInitial.Text, DateTime.DaysInMonth(int.Parse(txtYearFinal.Text), int.Parse(cmbMesFinal.SelectedValue.ToString())) + "/" + cmbMesFinal.SelectedValue.ToString() + "/" + txtYearFinal.Text, chkDataGrafico.Checked);
                                graphname = cmbObra.SelectedItem.Text.Trim() + "_" + cmbMesFinal.SelectedItem.Text.Trim() + txtYearFinal.Text + "_nabsolutomes.jpg";
                                chart = this.setChart(top, title, xtitle, ytitle, legendtext, "", SeriesChartType.Line, table, "cmp", "qtd", graphname);
                                top += chart.Height.Value + 50;
                                break;

                            case "natendimentodiario":
                                title = "N. de ordens de serviço por dia";
                                xtitle = "dias";
                                ytitle = "qtd.";
                                legendtext = "Qtd.";
                                table = tblOS.RetornarNOSMes(Global.GetConnection(), cmbObra.SelectedValue, cmbMesFinal.SelectedValue, txtYearFinal.Text, chkDataGrafico.Checked);
                                graphname = cmbObra.SelectedItem.Text.Trim() + "_" + cmbMesFinal.SelectedItem.Text.Trim() + txtYearFinal.Text + "_natendimentodiario.jpg";
                                chart = this.setChart(top, title, xtitle, ytitle, legendtext, "", SeriesChartType.Line, table, "mes", "qtd", graphname);
                                top += chart.Height.Value + 50;
                                break;

                            case "natendimentotipo":
                                title = "Porcentagem de ordens de serviço por tipo";
                                xtitle = "Tipo";
                                ytitle = "minutos";
                                labeltext = "#PERCENT{P2}";
                                legendtext = "#VALX(#PERCENT{P2})";
                                table = tblOS.RetornarNOSTipoAtividade(Global.GetConnection(), cmbObra.SelectedValue, cmbMesFinal.SelectedValue, txtYearFinal.Text, chkDataGrafico.Checked);
                                graphname = cmbObra.SelectedItem.Text.Trim() + "_" + cmbMesFinal.SelectedItem.Text.Trim() + txtYearFinal.Text + "mediatempoatendimento.jpg";
                                chart = this.setChart(top, title, xtitle, ytitle, legendtext, "", SeriesChartType.Pie, table, "cmpDcTipoAtividade", "qtd", graphname);
                                top += chart.Height.Value + 50;
                                break;

                            case "mediatempoatendimento":
                                title = "Tempo médio de atendimento das ordens de serviço";
                                xtitle = "Tipo";
                                ytitle = "minutos";
                                legendtext = "Minutos";
                                table = tblOS.RetornarMediaAtendimento(Global.GetConnection(), cmbObra.SelectedValue, cmbMesFinal.SelectedValue, txtYearFinal.Text, chkDataGrafico.Checked);
                                graphname = cmbObra.SelectedItem.Text.Trim() + "_" + cmbMesFinal.SelectedItem.Text.Trim() + txtYearFinal.Text + "mediatempoatendimento1.jpg";
                                chart = this.setChart(top, title, xtitle, ytitle, legendtext, "", SeriesChartType.Column, table, "cmpDcTipoAtividade", "qtd", graphname);
                                top += chart.Height.Value + 50;
                                break;
                        }
                    }
                }

                //Chart chart = new Chart();
                //chart.Series.Add(new Series("Series1"));
                //chart.ChartAreas.Add(new ChartArea("ChartArea1"));
                //chart.Titles.Add(title);
                //chart.Legends.Add(new Legend("Legend1"));

                ////chart.Titles["title1"].Text = title;
                //chart.Series["Series1"].ChartType = SeriesChartType.Line;
                //chart.Series["Series1"].ChartArea = "ChartArea1";
                //chart.Series["Series1"].Legend = "Legend1";
                //chart.ChartAreas[0].AxisX.Title = xtitle;
                //chart.ChartAreas[0].AxisY.Title = ytitle;
                //chart.Series["Series1"].LegendText = legendtext;
                //chart.ChartAreas[0].AxisX.Interval = 1;
                //chart.Width = 490;

                //using (DataTable table = tblOS.RetornarNOSAno(Global.GetConnection(), cmbObra.SelectedValue, txtYear.Text))
                //{
                //    foreach (DataRow r in table.Rows)
                //    {
                //        chart.Series["Series1"].Points.AddXY(r[0].ToString(), r["qtd"].ToString());
                //    }

                //    chart.SaveImage(Server.MapPath(cmbObra.SelectedItem.Text.Trim() + "_" + txtYear.Text + "_nabsolutomes.jpg"), ChartImageFormat.Jpeg);
                //}
                //chart.Style[HtmlTextWriterStyle.Position] = "absolute";
                //chart.Style[HtmlTextWriterStyle.Top] = "300px";
                //chart.Style[HtmlTextWriterStyle.Left] = "550px";
                //this.Controls.Add(chart);
            }
            catch (Exception ex)
            {
                Global.ShowError(Global.Title, ex);
            }
        }
  
    }
}