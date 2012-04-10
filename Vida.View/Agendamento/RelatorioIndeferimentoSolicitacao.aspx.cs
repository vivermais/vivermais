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
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.BLL;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;

namespace ViverMais.View.Agendamento
{
    public partial class RelatorioIndeferimentoSolicitacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id_solicitacao = int.Parse(Request.QueryString["id_solicitacao"].ToString());
                if (!String.IsNullOrEmpty(id_solicitacao.ToString()))
                {
                    IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
                    IViverMaisServiceFacade iViverMaisFacade = Factory.GetInstance<IViverMaisServiceFacade>();
                    IPaciente ipaciente = Factory.GetInstance<IPaciente>();

                    
                    ViewState.Add("id_solicitacao", id_solicitacao);
                    Solicitacao solicitacao = Factory.GetInstance<IAgendamentoServiceFacade>().BuscarPorCodigo<Solicitacao>(id_solicitacao);
                    if (solicitacao != null)
                    {
                        ViverMais.Model.Paciente paciente = PacienteBLL.Pesquisar(solicitacao.ID_Paciente);
                        if (paciente != null)
                        {
                            lblPaciente.Text = paciente.Nome.ToUpper();
                            IList<CartaoSUS> cartoes = ipaciente.ListarCartoesSUS<ViverMais.Model.CartaoSUS>(paciente.Codigo);
                            if (cartoes.Count != 0)
                            {
                                long cartao = (from c in cartoes select long.Parse(c.Numero)).Min();
                                lblCartaoSus.Text = cartao.ToString();
                            }
                            lblData.Text = solicitacao.Data_Solicitacao.ToString("dd/MM/yyyy");
                            //Procedimento procedimento = Factory.GetInstance<ViverMais.ServiceFacade.ServiceFacades.Procedimento.IProcedimento>().BuscarPorCodigo<Procedimento>(solicitacao.ID_Procedimento);
                            if (solicitacao.Procedimento != null)
                            {
                                lblProcedimento.Text = solicitacao.Procedimento.Nome.ToUpper();
                            }
                            if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.AUTORIZADA).ToString())
                                lblSituacao.Text = "Autorizada";
                            else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.CONFIRMADA).ToString())
                                lblSituacao.Text = "Confirmada";
                            else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA).ToString())
                                lblSituacao.Text = "Desmarcada";
                            else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.INDEFERIDA).ToString())
                                lblSituacao.Text = "Indeferida";
                            lblDataIndeferimento.Text = solicitacao.DataIndeferimento.ToString("dd/MM/yyyy");
                            //lblCRM.Text = "-";
                            lblJustificativa.Text = solicitacao.JustificativaIndeferimento.ToUpper();
                            if (!string.IsNullOrEmpty(solicitacao.NumeroProtocolo))
                                lblCodigo.Text = solicitacao.NumeroProtocolo;
                            else if (!string.IsNullOrEmpty(solicitacao.Identificador))
                                lblCodigo.Text = solicitacao.Identificador;
                            else
                                lblCodigo.Text = string.Empty;
                            
                        }
                    }
                }
            }
        }
    }
}
