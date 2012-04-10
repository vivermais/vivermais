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
using System.Collections.Generic;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.View.Agendamento.Helpers;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;

namespace ViverMais.View.Agendamento
{
    public partial class FormRelatorioSolicitacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "RELATORIO_SOLICITACAO_AMBULATORIAL", Modulo.AGENDAMENTO))
                    CarregaUnidadesSolicitantes();
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        protected void ddlUnidadeSolicitante_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaUsuariosDaUnidade(ddlUnidadeSolicitante.SelectedValue);
        }

        protected void btnGeraRelatorio_Click(object sender, EventArgs e)
        {
            DateTime dataInicial = DateTime.Parse(tbxData_Inicial.Text);
            DateTime dataFinal = DateTime.Parse(tbxData_Final.Text);
            Hashtable hash = Factory.GetInstance<IRelatorioAgendamento>().SolicitacaoAmbulatorial(ddlUnidadeSolicitante.SelectedValue, dataInicial, dataFinal, ddlUsuarios.SelectedValue);
            if (hash.Count != 0)
            {
                Session["HashSolicitacoes"] = hash;
                Redirector.Redirect("RelatorioSolicitacaoAmbulatorial.aspx", "_blank", "");
                Usuario usuario = (Usuario)Session["Usuario"];
                Factory.GetInstance<IAmbulatorial>().Salvar(new LogAgendamento(DateTime.Now, usuario.Codigo, 46, "CNES_UNIDADE: " + usuario.Unidade.CNES));
            }
        }

        protected void CarregaUnidadesSolicitantes()
        {
            ddlUnidadeSolicitante.Items.Clear();
            ddlUnidadeSolicitante.Items.Add(new ListItem("Selecione...", "0"));
            Usuario usuario = (Usuario)Session["Usuario"];
            if (usuario.Unidade.CNES == "6385907" || usuario.Perfis.Where(p => p.Modulo.Codigo == Modulo.AGENDAMENTO && p.Nome.ToUpper() == "ADMINISTRADOR").ToList().Count != 0)
            {
                IList<ViverMais.Model.EstabelecimentoSaude> unidades = Factory.GetInstance<IEstabelecimentoSaude>().ListarTodos<ViverMais.Model.EstabelecimentoSaude>("NomeFantasia", true);
                foreach (ViverMais.Model.EstabelecimentoSaude unidade in unidades)
                    ddlUnidadeSolicitante.Items.Add(new ListItem(unidade.NomeFantasia.ToUpper(), unidade.CNES));
            }
            else
                ddlUnidadeSolicitante.Items.Add(new ListItem(usuario.Unidade.NomeFantasia.ToUpper(), usuario.Unidade.CNES));
            ddlUnidadeSolicitante.Focus();
        }

        protected void CarregaUsuariosDaUnidade(string cnes)
        {
            ddlUsuarios.Items.Clear();
            ddlUsuarios.Items.Add(new ListItem("Selecione...", "0"));
            IList<Usuario> usuarios = Factory.GetInstance<IUsuario>().BuscarUsuariosPorCNES<Usuario>(cnes);
            foreach (Usuario usuario in usuarios)
                ddlUsuarios.Items.Add(new ListItem(usuario.Nome.ToUpper(), usuario.Codigo.ToString()));
            ddlUsuarios.Focus();
        }
    }
}
