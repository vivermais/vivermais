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
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.Collections.Generic;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.View.Agendamento.Helpers;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;

namespace ViverMais.View.Agendamento
{
    public partial class FormRelatorioAgendaPrestador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "RELATORIO_AGENDA_PRESTADOR", Modulo.AGENDAMENTO))
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
            if (usuario.Unidade.CNES == "6385907" || usuario.Perfis.Where(p => p.Modulo.Codigo == Modulo.AGENDAMENTO && p.Nome.ToUpper() == "ADMINISTRADOR").ToList().Count != 0)
                foreach (ViverMais.Model.EstabelecimentoSaude unidade in unidades)
                    ddlUnidadeExecutante.Items.Add(new ListItem(unidade.NomeFantasia.ToUpper(), unidade.CNES));
            else
                ddlUnidadeExecutante.Items.Add(new ListItem(usuario.Unidade.NomeFantasia.ToUpper(), usuario.Unidade.CNES));

            ddlUnidadeExecutante.Focus();
            //ddlUnidadeExecutante.Items.FindByValue(usuario.Unidade.CNES).Selected = true;

        }

        protected void CarregaDadosIniciais()
        {
            ddlUnidadeExecutante.Items.Add(new ListItem("Selecione...", "0"));
            ddlProcedimento.Items.Add(new ListItem("Selecione...", "0"));
            ddlProfissional.Items.Add(new ListItem("Selecione...", "0"));
        }

        protected void CarregaProcedimentos(string cnes)
        {
            if (String.IsNullOrEmpty(tbxData_Inicial.Text) || String.IsNullOrEmpty(tbxData_Final.Text))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Preencha as Datas!');", true);
                ddlUnidadeExecutante.SelectedValue = "0";
                return;
            }

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
                procedimentos.Add(fpo.Procedimento);//Verifico se o Procedimento não está na lista

            procedimentos = procedimentos.OrderBy(p => p.Nome).Distinct(new GenericComparer<Procedimento>("Codigo")).ToList();

            foreach (Procedimento proced in procedimentos)
                ddlProcedimento.Items.Add(new ListItem(proced.Nome.ToUpper(), proced.Codigo));
            //ddlProcedimento.Items.Add(new ListItem(procedimento.Nome.ToUpper(), procedimento.Codigo));
        }

        protected void CarregaProfissionaisUnidade(ViverMais.Model.EstabelecimentoSaude unidade, string id_procedimento)
        {
            IList<ViverMais.Model.CBO> cbosDoProcedimento = Factory.GetInstance<ICBO>().ListarCBOsPorProcedimento<ViverMais.Model.CBO>(id_procedimento);
            IList<CBO> cbosDoVinculo = Factory.GetInstance<IVinculo>().BuscarCbosDaUnidade<CBO>(unidade.CNES).Distinct().ToList();
            List<ViverMais.Model.Profissional> profissionais = new List<ViverMais.Model.Profissional>();
            var intersecao = from result in cbosDoVinculo
                             where cbosDoProcedimento.Select(p => p.Codigo).ToList().Contains(result.Codigo)
                             select result;

            ddlProfissional.Items.Clear();
            ddlProfissional.Items.Add(new ListItem("Selecione...", "0"));
            //foreach (CBO cbo in intersecao)
            //{   
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
            //}

            profissionais = profissionais.OrderBy(p => p.Nome).ToList();
            foreach (ViverMais.Model.Profissional prof in profissionais)
                ddlProfissional.Items.Add(new ListItem(prof.Nome.ToUpper(), prof.CPF));

            //ddlProfissional.Items.Add(new ListItem("Selecione...", "0"));

            //foreach (ViverMais.Model.VinculoProfissional f in vinculo)
            //{
            //    id_profissional = f.Profissional.CPF;
            //    IViverMaisServiceFacade iProfissional = Factory.GetInstance<IViverMaisServiceFacade>();
            //    ViverMais.Model.Profissional profissional = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(id_profissional);
            //    if (profissional != null)
            //    {
            //        ddlProfissional.Items.Add(new ListItem(profissional.Nome, profissional.CPF));
            //    }
            //}

            //ddlProfissional.Items.Clear();
            //ddlProfissional.Items.Add(new ListItem("Selecione...", "0"));
            //List<ViverMais.Model.Profissional> profissionais = new List<ViverMais.Model.Profissional>();
            //foreach (VinculoProfissional vinculo in vinculos)
            //{
            //    //Verifica Se o Profissional já existe na Lista de Profissionais
            //    if (ddlProfissional.Items.FindByValue(vinculo.Profissional.CPF) == null)
            //    {
            //        ViverMais.Model.Profissional profissional = Factory.GetInstance<IProfissional>().BuscarPorCodigo<ViverMais.Model.Profissional>(vinculo.Profissional.CPF);
            //        if (profissional != null)
            //            profissionais.Add(profissional);
            //    }
            //}
        }

        protected void ddlUnidadeExecutante_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProcedimentos(ddlUnidadeExecutante.SelectedValue);
        }

        protected void ddlProcedimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaProfissionaisUnidade(Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(ddlUnidadeExecutante.SelectedValue), ddlProcedimento.SelectedValue);
        }

        protected void btnGeraRelatorio_Click(object sender, EventArgs e)
        {
            DateTime dataInicial = DateTime.Parse(tbxData_Inicial.Text);
            DateTime dataFinal = DateTime.Parse(tbxData_Final.Text);
            Hashtable hash = Factory.GetInstance<IRelatorioAgendamento>().AgendaPrestador(ddlUnidadeExecutante.SelectedValue, ddlProcedimento.SelectedValue, ddlProfissional.SelectedValue, dataInicial, dataFinal);
            if (hash.Count != 0)
            {
                Session["HashAgendaPrestador"] = hash;
                Redirector.Redirect("RelatorioAgendaPrestador.aspx", "_blank", "");
                Usuario usuario = (Usuario)(Session["Usuario"]);
                Factory.GetInstance<IAmbulatorial>().Salvar(new LogAgendamento(DateTime.Now, usuario.Codigo, 45, "CNES_UNIDADE: " + usuario.Unidade.CNES));
            }
        }
    }
}
