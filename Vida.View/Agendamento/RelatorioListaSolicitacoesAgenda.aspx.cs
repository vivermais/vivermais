using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.View.Agendamento.RelatoriosCrystal;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using CrystalDecisions.CrystalReports.Engine;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Agendamento
{
    public partial class RelatorioListaSolicitacoesAgenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "RELATORIO_VAGAS_PROCED_AGENDADO_BASICO", Modulo.AGENDAMENTO))
            {
                criaPDF();
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
        }

        protected void criaPDF()
        {
            int id_agenda = int.Parse(Request.QueryString["codigo"]);
            Agenda agenda = Factory.GetInstance<IAmbulatorial>().BuscarPorCodigo<Agenda>(id_agenda);

            DataTable table = new DataTable();
            table.Columns.Add("Unidade", typeof(string));
            table.Columns.Add("Data", typeof(string));
            table.Columns.Add("Procedimento", typeof(string));
            table.Columns.Add("Profissional", typeof(string));

            DataRow row = table.NewRow();

            row["Unidade"] = Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(agenda.Estabelecimento.CNES).NomeFantasia;
            row["Data"] = agenda.Data.ToString("dd/MM/yyyy");
            row["Procedimento"] = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(agenda.Procedimento.Codigo).Nome;
            row["Profissional"] = Factory.GetInstance<IProfissional>().BuscarPorCodigo<ViverMais.Model.Profissional>(agenda.ID_Profissional).Nome;
            table.Rows.Add(row);
            DSCabecalhoListaSolicitacoesAgenda dscabecalho = new DSCabecalhoListaSolicitacoesAgenda();
            dscabecalho.Tables.Add(table);

            DataTable table2 = new DataTable();
            table2.Columns.Add("Nome Paciente", typeof(string));
            table2.Columns.Add("CNS", typeof(string));
            table2.Columns.Add("Telefone", typeof(string));

            IList<Solicitacao> solicitacoes = Factory.GetInstance<ISolicitacao>().ListaSolicitacoesDaAgenda<Solicitacao>(id_agenda);
            foreach (Solicitacao solicitacao in solicitacoes)
            {
                DataRow row2 = table2.NewRow();
                row2["Nome Paciente"] = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(solicitacao.ID_Paciente).Nome;
                IList<CartaoSUS> cartoes = Factory.GetInstance<IPaciente>().ListarCartoesSUS<CartaoSUS>(solicitacao.ID_Paciente);
                row2["CNS"] = cartoes[cartoes.Count - 1].Numero;
                row2["Telefone"] = Factory.GetInstance<IEndereco>().BuscarPorPaciente<Endereco>(solicitacao.ID_Paciente).Telefone;
                table2.Rows.Add(row2);
            }

            DSRelatorioListaSolicitacoesAgenda dsconteudo = new DSRelatorioListaSolicitacoesAgenda();
            dsconteudo.Tables.Add(table2);


            ReportDocument doc = new ReportDocument();
            doc.Load(Server.MapPath("RelatoriosCrystal/CrystalReportViewer_ListaSolicitacoesAgenda.rpt"));
            doc.SetDataSource(dsconteudo.Tables[1]);
            doc.Subreports[0].SetDataSource(dscabecalho.Tables[1]);

            CrystalReportViewer_ListaSolicitacoesAgenda.ReportSource = doc;
            CrystalReportViewer_ListaSolicitacoesAgenda.DataBind();

            //doc.PrintToPrinter(1, false, 1, 1);
        }
    }
}
