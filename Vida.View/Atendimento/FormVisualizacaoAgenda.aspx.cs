using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;
using System.Data;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;

namespace ViverMais.View.Atendimento
{
    public partial class FormVisualizacaoAgenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "VISUALIZACAO_DE_AGENDA", Modulo.ATENDIMENTO))
            {
                if (!IsPostBack)
                {
                    CarregaUnidadesExecutantes();
                }
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);

        }
        protected void CarregaUnidadesExecutantes()
        {
            ddlUnidadeExecutante.Items.Clear();
            ddlUnidadeExecutante.Items.Add(new ListItem("Selecione...", "0"));
            IList<ViverMais.Model.EstabelecimentoSaude> unidades = Factory.GetInstance<IEstabelecimentoSaude>().ListarTodos<ViverMais.Model.EstabelecimentoSaude>("NomeFantasia", true);
            Usuario usuario = (Usuario)Session["Usuario"];

            //Se o usuário for da Regulação ou tiver o Perfil de Administrador do Módulo Regulação, poderá visualizar o Relatório de Todas as Unidades
            //Segundo Marcelo, o CNES da Regulação é o mesmo da SMS
            if (usuario.Unidade.CNES == "6385907")// || usuario.Perfis.Where(p => p.Modulo.Codigo == Modulo.AGENDAMENTO && p.Nome.ToUpper() == "ADMINISTRADOR").ToList().Count != 0)
                foreach (ViverMais.Model.EstabelecimentoSaude unidade in unidades)
                    ddlUnidadeExecutante.Items.Add(new ListItem(unidade.NomeFantasia.ToUpper(), unidade.CNES));
            else
                ddlUnidadeExecutante.Items.Add(new ListItem(usuario.Unidade.NomeFantasia.ToUpper(), usuario.Unidade.CNES));

            ddlUnidadeExecutante.Focus();

        }

        protected void CarregaProcedimentos(string cnes)
        {
            if (String.IsNullOrEmpty(tbxData_Inicial.Text) || String.IsNullOrEmpty(tbxData_Final.Text))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Preencha as Datas!');", true);
                ddlUnidadeExecutante.SelectedValue = "0";
                return;
            }
            Usuario usuario = (Usuario)Session["Usuario"];
            if (usuario.Perfis.Where(p => p.Modulo.Codigo == Modulo.ATENDIMENTO && p.PerfilProfissionalSaude).ToList().Count != 0)
            {
                IList<ViverMais.Model.Agenda> agendas = Factory.GetInstance<IAmbulatorial>().VerificarAgendas<ViverMais.Model.Agenda>(cnes, usuario.ProfissionalSaude.CPF, tbxData_Inicial.Text, tbxData_Final.Text, "0");
                agendas = agendas.OrderBy(p => p.Procedimento).Distinct().ToList();
                ddlProcedimento.Items.Clear();
                ddlProcedimento.Items.Add(new ListItem("Selecione...", "0"));
                foreach (Agenda agenda in agendas)
                    ddlProcedimento.Items.Add(new ListItem(agenda.Procedimento.Nome.ToUpper(), agenda.Procedimento.Codigo));
            }
            else
            {
                DateTime dataInicial = DateTime.Parse(tbxData_Inicial.Text);
                DateTime dataFinal = DateTime.Parse(tbxData_Final.Text);

                string competenciaInicial = (dataInicial.Year.ToString() + dataInicial.Month.ToString("00"));
                string competenciaFinal = (dataFinal.Year.ToString() + dataFinal.Month.ToString("00"));

                IList<ViverMais.Model.FPO> fpos = Factory.GetInstance<IFPO>().BuscarFPO<ViverMais.Model.FPO>(cnes, int.Parse(competenciaInicial));
                ddlProcedimento.Items.Clear();
                ddlProcedimento.Items.Add(new ListItem("Selecione...", "0"));
                List<Procedimento> procedimentos = new List<Procedimento>();
                foreach (ViverMais.Model.FPO fpo in fpos)
                        procedimentos.Add(fpo.Procedimento);
                

                //Busca os FPOs referente a Segunda Competencia
                fpos = Factory.GetInstance<IFPO>().BuscarFPO<ViverMais.Model.FPO>(cnes, int.Parse(competenciaFinal));
                foreach (ViverMais.Model.FPO fpo in fpos)
                    procedimentos.Add(fpo.Procedimento);
                
                procedimentos = procedimentos.OrderBy(p => p.Nome).Distinct().ToList();
                foreach (Procedimento proced in procedimentos)
                    ddlProcedimento.Items.Add(new ListItem(proced.Nome.ToUpper(), proced.Codigo));
            }
        }

        protected void CarregaProfissionaisUnidade(ViverMais.Model.EstabelecimentoSaude unidade, string id_procedimento)
        {
            Usuario usuario = (Usuario)Session["Usuario"];
            List<ViverMais.Model.Profissional> profissionais = new List<ViverMais.Model.Profissional>();
            if (usuario.Perfis.Where(p => p.Modulo.Codigo == Modulo.ATENDIMENTO && p.PerfilProfissionalSaude).ToList().Count != 0)
            {
                ddlProfissional.Items.Clear();
                ddlProfissional.Items.Add(new ListItem("Selecione...", "0"));
                ViverMais.Model.Profissional profissional = Factory.GetInstance<IProfissional>().BuscarPorCodigo<ViverMais.Model.Profissional>(usuario.ProfissionalSaude.CPF);
                if (profissional != null)
                    profissionais.Add(profissional);
                foreach (ViverMais.Model.Profissional prof in profissionais)
                    ddlProfissional.Items.Add(new ListItem(prof.Nome.ToUpper(), prof.CPF));


            }
            else
            {
                IList<ViverMais.Model.CBO> cbosDoProcedimento = Factory.GetInstance<ICBO>().ListarCBOsPorProcedimento<ViverMais.Model.CBO>(id_procedimento);
                IList<CBO> cbosDoVinculo = Factory.GetInstance<IVinculo>().BuscarCbosDaUnidade<CBO>(unidade.CNES).Distinct().ToList();
                var intersecao = from result in cbosDoVinculo
                                 where cbosDoProcedimento.Select(p => p.Codigo).ToList().Contains(result.Codigo)
                                 select result;

                ddlProfissional.Items.Clear();
                ddlProfissional.Items.Add(new ListItem("Selecione...", "0"));
                IList<ViverMais.Model.VinculoProfissional> vinculos = Factory.GetInstance<IVinculo>().BuscarVinculoPorCNES<ViverMais.Model.VinculoProfissional>(unidade.CNES).Where(p => p.Status == Convert.ToChar(VinculoProfissional.DescricaStatus.Ativo).ToString()).ToList().Distinct().ToList();
                var linqVinculos = from result in vinculos
                                   where intersecao.Select(p => p.Codigo).ToList().Contains(result.CBO.Codigo)
                                   select result;

                if (vinculos.Count != 0)
                {
                    foreach (VinculoProfissional vinculo in linqVinculos)
                    {
                        //Verifica Se o Profissional já existe na Lista de Profissionais
                        if (profissionais.Where(p => p.CPF == vinculo.Profissional.CPF).ToList().Count == 0)
                        {
                            ViverMais.Model.Profissional profissional = Factory.GetInstance<IProfissional>().BuscarPorCodigo<ViverMais.Model.Profissional>(vinculo.Profissional.CPF);
                            if (profissional != null)
                                profissionais.Add(profissional);
                        }
                    }
                }

                profissionais = profissionais.OrderBy(p => p.Nome).ToList();
                foreach (ViverMais.Model.Profissional prof in profissionais)
                    ddlProfissional.Items.Add(new ListItem(prof.Nome.ToUpper(), prof.CPF));
            }
        }

        protected void ddlUnidadeExecutante_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProcedimentos(ddlUnidadeExecutante.SelectedValue);
        }

        protected void ddlProcedimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProfissionaisUnidade(Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(ddlUnidadeExecutante.SelectedValue), ddlProcedimento.SelectedValue);
        }
        protected void btnVisualizarAgenda_OnClick(object sender, EventArgs e)
        {
            DateTime dataInicial = DateTime.Parse(tbxData_Inicial.Text);
            DateTime dataFinal = DateTime.Parse(tbxData_Final.Text);
            DataTable table = new DataTable();
            DataColumn Paciente = new DataColumn("Paciente");
            DataColumn Turno = new DataColumn("Turno");

            table.Columns.Add(Paciente);
            table.Columns.Add(Turno);
            IList<Solicitacao> solicitacoes = Factory.GetInstance<ISolicitacao>().ListarSolicitacoesAgendaPrestador<Solicitacao>(ddlUnidadeExecutante.SelectedValue, ddlProcedimento.SelectedValue, ddlProfissional.SelectedValue, dataInicial, dataFinal).OrderBy(p => p.Agenda.Data).ToList();
            solicitacoes = solicitacoes.OrderBy(p => p.Agenda.Turno).ToList();
            if (solicitacoes.Count != 0)
            {
                foreach (Solicitacao solicitacao in solicitacoes)
                {
                    DataRow row = table.NewRow();
                    ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(solicitacao.ID_Paciente);
                    row[0] = paciente.Nome;
                    if (solicitacao.Agenda.Turno == "M")
                    {
                        row[1] = "Manhã";
                    }
                    else
                    {
                        row[1] = "Tarde";
                    }
                    table.Rows.Add(row);

                }
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não existe Paciente(s) Agendado(s).');", true);

            GridViewPaciente.DataSource = table;
            GridViewPaciente.DataBind();

        }

    }
}
