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
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.View.Agendamento.Helpers;

namespace ViverMais.View.Agendamento
{
    public partial class FormCadastrarProgramaSaude : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CADASTRAR_PROGRAMA_SAUDE", Modulo.AGENDAMENTO))
                {
                    if (Request.QueryString["codigo"] != null)
                    {
                        int id_programa = int.Parse(Request.QueryString["codigo"]);
                        ProgramaDeSaude programa = Factory.GetInstance<IAgendamentoServiceFacade>().BuscarPorCodigo<ProgramaDeSaude>(id_programa);
                        if (programa != null)
                        {
                            tbxNomePrograma.Text = programa.Nome.ToUpper();
                            chkBoxMulti.Checked = programa.MultiDisciplinar;
                        }
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        protected void Incluir_Click(object sender, EventArgs e)
        {
            ProgramaDeSaude programa = new ProgramaDeSaude();

            string nomePrograma = ViverMais.View.Helpers.HelperRemoveDiaCritics.RemoveDiacritics(tbxNomePrograma.Text).ToUpper();
            if (Request.QueryString["codigo"] != null)
            {
                int id_programa = int.Parse(Request.QueryString["codigo"]);
                programa = Factory.GetInstance<IAgendamentoServiceFacade>().BuscarPorCodigo<ProgramaDeSaude>(id_programa);
            }
            else
            {
                if (Factory.GetInstance<IProgramaDeSaude>().BuscarPorNome<ProgramaDeSaude>(nomePrograma) != null)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe programa com este nome!');", true);
                    return;
                }
            }

            programa.Nome = ViverMais.View.Helpers.HelperRemoveDiaCritics.RemoveDiacritics(tbxNomePrograma.Text).ToUpper();
            programa.MultiDisciplinar = chkBoxMulti.Checked;
            if (programa.Codigo == 0)//Se for Cadastro Novo
                programa.Ativo = true;
            Factory.GetInstance<IAgendamentoServiceFacade>().Salvar(programa);
            Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 5, "id_programa:" + programa.Codigo));
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados Salvos com Sucesso!');", true);
            Redirector.Redirect("BuscaProgramaSaude.aspx", "_parent", "");
        }
    }
}
