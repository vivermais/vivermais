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
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using System.Collections.Generic;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Agendamento
{
    public partial class ImpressaoProtocolo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MOVIMENTACAO_SOLICITACAO_AMBULATORIAL",Modulo.AGENDAMENTO))
            {
                int id_solicitacao = int.Parse(Request.QueryString["id_solicitacao"]);
                IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
                ViverMais.Model.Solicitacao solicitacao = iAgendamento.BuscarPorCodigo<ViverMais.Model.Solicitacao>(id_solicitacao);
                ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(solicitacao.ID_Paciente);
                IList<CartaoSUS> cartoes = Factory.GetInstance<IPaciente>().ListarCartoesSUS<ViverMais.Model.CartaoSUS>(paciente.Codigo);
                long cartao = (from c in cartoes select long.Parse(c.Numero)).Min();
                lblPaciente.Text = paciente.Nome;
                lblData.Text = solicitacao.Data_Solicitacao.ToString("dd/MM/yyyy");
                lblProcedimento.Text = solicitacao.Procedimento.Nome;
                lblCodigo.Text = solicitacao.NumeroProtocolo.ToString();
                lblCartaoSus.Text = cartao.ToString();
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
        }
    }
}
