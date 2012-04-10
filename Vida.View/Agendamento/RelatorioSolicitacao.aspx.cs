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
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.Model;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;

namespace ViverMais.View.Agendamento
{
    public partial class RelatorioSolicitacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id_solicitacao = int.Parse(Request.QueryString["id_solicitacao"]);
            IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
            ViverMais.Model.Solicitacao solicitacao = iAgendamento.BuscarPorCodigo<ViverMais.Model.Solicitacao>(id_solicitacao);
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(solicitacao.ID_Paciente);
            IPaciente ipaciente = Factory.GetInstance<IPaciente>();
            IList<CartaoSUS> cartoes = ipaciente.ListarCartoesSUS<ViverMais.Model.CartaoSUS>(paciente.Codigo);
            long cartao = (from c in cartoes select long.Parse(c.Numero)).Min();
            
            if (solicitacao.Agenda != null)
            {
                ViverMais.Model.Agenda agenda = iAgendamento.BuscarPorCodigo<ViverMais.Model.Agenda>(solicitacao.Agenda.Codigo);
                ViverMais.Model.EstabelecimentoSaude eas = iViverMais.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(agenda.Estabelecimento.CNES);
                ViverMais.Model.Profissional profissional = iViverMais.BuscarPorCodigo<ViverMais.Model.Profissional>(agenda.ID_Profissional.CPF);
                ViverMais.Model.Procedimento procedimento = iViverMais.BuscarPorCodigo<ViverMais.Model.Procedimento>(agenda.Procedimento.Codigo);
                lblPaciente.Text = paciente.Nome;
                lblEas.Text = eas.NomeFantasia;
                if (eas.Bairro != null)
                {
                    lblEnderecoUnidade.Text = eas.Logradouro + " " + eas.Bairro.Nome;
                    lblTelefone.Text = eas.Telefone;
                }
                else
                {
                    lblEnderecoUnidade.Text = eas.Logradouro;
                }
                lblProfissional.Text = profissional.Nome;
                lblData.Text = agenda.Data.ToString("dd/MM/yyyy");
                lblHoraIni.Text = agenda.Hora_Inicial;
                lblHoraFim.Text = agenda.Hora_Final;
                if (agenda.Turno.ToString().Equals("M"))
                    lblTurno.Text = "Manhã";
                else if (agenda.Turno.ToString().Equals("T"))
                    lblTurno.Text = "Tarde";
                else if (agenda.Turno.ToString().Equals("N"))
                    lblTurno.Text = "Noite";

                //lblTurno.Text = agenda.Turno.ToString().Equals("M") ? "Manhã" : "Tarde";
                if ((procedimento.Codigo == "0301010072") || (procedimento.Codigo == "0301010064"))
                {
                    CBO cbo = Factory.GetInstance<ICBO>().BuscarPorCodigo<CBO>(solicitacao.Agenda.Cbo.Codigo);
                    if (cbo != null)
                        lblProcedimento.Text = procedimento.Nome + " - " + cbo.Nome;
                }
                else
                {
                    lblProcedimento.Text = procedimento.Codigo + " - " + procedimento.Nome;

                }
                lblCodigo.Text = solicitacao.Identificador.ToString();
                lblCartaoSus.Text = cartao.ToString();
                IList<Preparo> preparos = Factory.GetInstance<IPreparo>().BuscarPreparoPorProcedimento<Preparo>(solicitacao.Procedimento.Codigo);
                string recomendacoes = string.Empty;
                foreach (Preparo preparo in preparos)
                {
                    recomendacoes += preparo.Descricao + ";<br />";
                }
                lblRecomendacoes.Text = recomendacoes;
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não existe agenda para esta Solicitação! Por favor, entre em contato com a Regulação.');", true);
                return;
            }
        }
    }
}
