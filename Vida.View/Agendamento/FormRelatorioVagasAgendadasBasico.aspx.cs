using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using ViverMais.Model;
using ViverMais.View.Agendamento.RelatoriosCrystal;
using CrystalDecisions.Shared;
using System.Collections.Generic;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;
using CrystalDecisions.CrystalReports.Engine;
using System.Globalization;
using ViverMais.View.Agendamento.Helpers;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;

namespace ViverMais.View.Agendamento
{
    public partial class FormRelatorioVagasAgendadasBasico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "RELATORIO_VAGAS_PROCED_AGENDADO_BASICO", Modulo.AGENDAMENTO))
                {
                    CarregaTiposProcedimento();
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        void CarregaTiposProcedimento()
        {
            rbtnTipoProcedimento.Items.Add(new ListItem("AGENDADO", Convert.ToInt32(TipoProcedimento.TiposDeProcedimento.AGENDADO).ToString()));
            rbtnTipoProcedimento.Items.Add(new ListItem("ATENDIMENTO BÁSICO", Convert.ToInt32(TipoProcedimento.TiposDeProcedimento.ATENDIMENTOBASICO).ToString()));
        }

        protected void btnImprimir_OnClick(object sender, EventArgs e)
        {
            //CrystalReportViewer_VagasAgendadasBasico relatorio = new CrystalReportViewer_VagasAgendadasBasico();
            string tipoRelatorio = String.Empty;

            if (rbtnTipoProcedimento.SelectedValue == Convert.ToInt32(TipoProcedimento.TiposDeProcedimento.AGENDADO).ToString())
                tipoRelatorio = "AGENDADO";
            else if (rbtnTipoProcedimento.SelectedValue == Convert.ToInt32(TipoProcedimento.TiposDeProcedimento.ATENDIMENTOBASICO).ToString())
                tipoRelatorio = "ATENDIMENTO BÁSICO";

            int qtdRegistros = 0;


            //SetReportParameters("tipoProcedimento", tipoRelatorio, relatorio);            


            IAmbulatorial iAmbulatorial = Factory.GetInstance<IAmbulatorial>();
            //Verifico Os parametros da Unidade para limitar a Busca de Agendas de Acordo a Minimo e Máximos de dias definidos nos Parametros
            Parametros parametroVagas = iAmbulatorial.ListarTodos<ViverMais.Model.Parametros>().FirstOrDefault();
            if (parametroVagas != null)
            {
                //Lista as Agendas Diponíveis no periodo parametrizado
                //DateTime data_inicial = DateTime.Now.AddDays(parametroVagas.Min_Dias);
                //DateTime data_final = DateTime.Now.AddDays(parametroVagas.Max_Dias);
                DateTime data_inicial = DateTime.Parse("01/02/2011");
                DateTime data_final = DateTime.Now;

                //Lista os Procedimentos para o tipo de Procedimento Selecionado
                IList<TipoProcedimento> tiposProcedimentos = Factory.GetInstance<ITipoProcedimento>().ListarProcedimentosPorTipo<TipoProcedimento>(rbtnTipoProcedimento.SelectedValue);
                if (tiposProcedimentos.Count != 0)
                {
                    DSRelatorioVagasBasicoAgendado dsVgas = new DSRelatorioVagasBasicoAgendado();
                    List<String> procedimentos = new List<String>();
                    for (int i = 0; i < tiposProcedimentos.Count; i++)
                    {
                        //Para cada procedimento, verifico a disponibilidade de vagas, de acordo com o parametro de dias Parametrizado
                        Procedimento procedimento = iAmbulatorial.BuscarPorCodigo<ViverMais.Model.Procedimento>(tiposProcedimentos[i].Procedimento);
                        if (procedimento != null)
                        {
                            IList<Agenda> agendas;
                            if (procedimento.Codigo == "0301010072")//Código do Procedimento de COnsulta em atenção Especializada
                            {
                                //Listo todos CBos vinculados ao Procedimento de Consulta Especializada
                                IList<CBO> cbos = Factory.GetInstance<ICBO>().ListarCBOsPorProcedimento<CBO>(procedimento.Codigo);
                                foreach (CBO cbo in cbos)
                                {
                                    agendas = iAmbulatorial.BuscarAgendaProcedimento<ViverMais.Model.Agenda>(procedimento.Codigo, cbo.Codigo, data_inicial, data_final).Where(p => p.Quantidade > p.QuantidadeAgendada).ToList();
                                    if (agendas.Count > 0)//Se existir vaga, adiciono nos parametros do relatório
                                    {
                                        procedimentos.Add(procedimento.Nome + " - " + cbo.Nome);
                                        qtdRegistros++;
                                    }
                                }
                            }
                            else
                            {
                                agendas = iAmbulatorial.BuscarAgendaProcedimento<ViverMais.Model.Agenda>(procedimento.Codigo, string.Empty, data_inicial, data_final).Where(p => p.Quantidade > p.QuantidadeAgendada).ToList();
                                if (agendas.Count > 0)//Se existir vaga, adiciono nos parametros do relatório
                                {
                                    procedimentos.Add(procedimento.Nome);
                                    qtdRegistros++;
                                }
                            }
                            
                        }
                    }
                    procedimentos.Sort();
                    
                    foreach (String proced in procedimentos)
                    {
                        DataRow row = dsVgas.Tables["Procedimentos"].NewRow();
                        row["NomeProcedimento"] = proced.ToUpper();
                        dsVgas.Tables["Procedimentos"].Rows.Add(row);
                    }

                    DataRow row2 = dsVgas.Tables["Cabecalho"].NewRow();
                    row2["qtdRegistros"] = qtdRegistros;
                    row2["tipoRelatorio"] = tipoRelatorio;
                    ReportDocument repDoc = new ReportDocument();
                    repDoc.Load(Server.MapPath("RelatoriosCrystal/CrystalReportViewer_VagasAgendadasBasico.rpt"));
                    repDoc.SetDataSource(dsVgas);

                    System.IO.Stream s = repDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    Session["StreamRelatorioVagas"] = s;
                    Redirector.Redirect("RelatorioVagasAgendadasBasico.aspx", "_blank", String.Empty);
                    Usuario usuario = (Usuario)(Session["Usuario"]);
                    Factory.GetInstance<IAmbulatorial>().Salvar(new LogAgendamento(DateTime.Now, usuario.Codigo, 49, "NomeUsuario: " + usuario.Nome.ToUpper()));
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Os Parâmetros Ambulatorias ainda não estão definidos. Por favor entre em contato com a administração!';", true);
                return;
            }
        }

        protected void SetReportParameters(string name, string value, CrystalReportViewer_VagasAgendadasBasico relatorio)
        {
            ParameterValues parameterValue = new ParameterValues();
            ParameterDiscreteValue parameterDiscreteValue = new ParameterDiscreteValue();
            CultureInfo Iformat = CultureInfo.CurrentCulture;

            for (int i = 0; i < relatorio.DataDefinition.ParameterFields.Count; i++)
            {
                if (relatorio.DataDefinition.ParameterFields[i].Name == name)
                {
                    switch (relatorio.DataDefinition.ParameterFields[i].ValueType)
                    {

                        case FieldValueType.NumberField:

                            parameterDiscreteValue.Value = Convert.ToInt32(value);
                            parameterValue.Add(parameterDiscreteValue);
                            relatorio.DataDefinition.ParameterFields[i].ApplyCurrentValues(parameterValue);
                            break;
                        case FieldValueType.CurrencyField:

                            parameterDiscreteValue.Value = Convert.ToDouble(value);
                            parameterValue.Add(parameterDiscreteValue);
                            relatorio.DataDefinition.ParameterFields[i].ApplyCurrentValues(parameterValue);
                            break;
                        case FieldValueType.DateField:

                            parameterDiscreteValue.Value = Convert.ToDateTime(value, Iformat);
                            parameterValue.Add(parameterDiscreteValue);
                            relatorio.DataDefinition.ParameterFields[i].ApplyCurrentValues(parameterValue);
                            break;
                        case FieldValueType.DateTimeField:

                            parameterDiscreteValue.Value = Convert.ToDateTime(value, Iformat);
                            parameterValue.Add(parameterDiscreteValue);
                            relatorio.DataDefinition.ParameterFields[i].ApplyCurrentValues(parameterValue);
                            break;
                        case FieldValueType.BooleanField:

                            parameterDiscreteValue.Value = Convert.ToBoolean(value);
                            parameterValue.Add(parameterDiscreteValue);
                            relatorio.DataDefinition.ParameterFields[i].ApplyCurrentValues(parameterValue);
                            break;
                        case FieldValueType.StringField:
                            parameterDiscreteValue.Value = Convert.ToString(value);
                            parameterValue.Add(parameterDiscreteValue);
                            relatorio.DataDefinition.ParameterFields[i].ApplyCurrentValues(parameterValue);
                            break;
                    }
                }
            }
        }
    }
}
