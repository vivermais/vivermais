﻿using System;
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
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;
using ViverMais.View.Agendamento.Helpers;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;

namespace ViverMais.View.Agendamento
{
    public partial class FormRelatorioAgendaMontadaPublicada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "RELATORIO_AGENDA_MONTADA_PUBLICADA", Modulo.AGENDAMENTO))
                    CarregaDadosIniciais();
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        protected void CarregaDadosIniciais()
        {
            ddlEstabelecimentoSaude.Items.Add(new ListItem("Selecione...", "0"));
            ddlProcedimento.Items.Add(new ListItem("Selecione...", "0"));
            ddlCBO.Items.Add(new ListItem("Selecione...", "0"));
            ddlProfissional.Items.Add(new ListItem("Selecione...", "0"));
        }

        protected void CarregaUnidades(int competencia)
        {
            ddlEstabelecimentoSaude.Items.Clear();
            ddlEstabelecimentoSaude.Items.Add(new ListItem("Selecione...", "0"));

            ddlProcedimento.Items.Clear();
            ddlProcedimento.Items.Add(new ListItem("Selecione...", "0"));
            ddlCBO.Items.Clear();
            ddlCBO.Items.Add(new ListItem("Selecione...", "0"));
            ddlProfissional.Items.Clear();
            ddlProfissional.Items.Add(new ListItem("Selecione...", "0"));

            IList<string> estabelecimentos = Factory.GetInstance<IFPO>().ListarUnidadesPorCompetencia<string>(competencia);
            List<ViverMais.Model.EstabelecimentoSaude> estabelecimentosDeSaude = new List<ViverMais.Model.EstabelecimentoSaude>();
            Usuario usuario = (Usuario)Session["Usuario"];

            //Segundo Marcelo, o CNES da Regulação é o mesmo da SMS
            if (usuario.Unidade.CNES == ViverMais.Model.EstabelecimentoSaude.CNES_SMS_SSA || usuario.Perfis.Where(p => p.Modulo.Codigo == Modulo.AGENDAMENTO && p.Nome.ToUpper() == "ADMINISTRADOR").ToList().Count != 0)
            {
                foreach (string cnes in estabelecimentos)
                {
                    if (ddlEstabelecimentoSaude.Items.FindByValue(cnes) == null)
                    {
                        ViverMais.Model.EstabelecimentoSaude estabelecimento = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(cnes);
                        if (estabelecimento != null)
                            estabelecimentosDeSaude.Add(estabelecimento);
                    }
                }
                estabelecimentosDeSaude = estabelecimentosDeSaude.OrderBy(p => p.NomeFantasia).ToList().Distinct(new GenericComparer<ViverMais.Model.EstabelecimentoSaude>("NomeFantasia")).ToList();
                foreach (ViverMais.Model.EstabelecimentoSaude estabele in estabelecimentosDeSaude)
                    ddlEstabelecimentoSaude.Items.Add(new ListItem(estabele.NomeFantasia, estabele.CNES));
            }
            else
                ddlEstabelecimentoSaude.Items.Add(new ListItem(usuario.Unidade.NomeFantasia.ToUpper(), usuario.Unidade.CNES));
        }

        protected void CarregaProcedimento(int competencia, string cnes)
        {
            ddlCBO.Items.Clear();
            ddlCBO.Items.Add(new ListItem("Selecione...", "0"));
            ddlProfissional.Items.Clear();
            ddlProfissional.Items.Add(new ListItem("Selecione...", "0"));

            IList<Procedimento> procedimentos = Factory.GetInstance<IFPO>().ListarProcedimentosPorCompetenciaCNES<Procedimento>(competencia, cnes);
            procedimentos = procedimentos.OrderBy(p => p.Nome).ToList().Distinct(new GenericComparer<Procedimento>("Nome")).ToList();
            ddlProcedimento.Items.Clear();
            ddlProcedimento.Items.Add(new ListItem("Selecione...", "0"));
            foreach (Procedimento procedimento in procedimentos)
                ddlProcedimento.Items.Add(new ListItem(procedimento.Nome, procedimento.Codigo));
        }

        protected void CarregaEspecialidades(int competencia, string cnes, string id_procedimento)
        {
            ddlProfissional.Items.Clear();
            ddlProfissional.Items.Add(new ListItem("Selecione...", "0"));
            
            IList<ViverMais.Model.CBO> cbosDoProcedimento = Factory.GetInstance<ICBO>().ListarCBOsPorProcedimento<ViverMais.Model.CBO>(id_procedimento);

            // Monta lista de CBO ligados ao Vinculo do CNES
            IList<CBO> cbosDoVinculo = Factory.GetInstance<IVinculo>().BuscarCbosDaUnidade<CBO>(cnes).Distinct().ToList();

            var intersecao = from result in cbosDoVinculo
                             where
                                 cbosDoProcedimento.Select(p => p.Codigo).ToList().Contains(result.Codigo)
                             select result;
            intersecao = intersecao.OrderBy(p => p.Nome).ToList();
            if (intersecao.Count() == 0)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('O Estabelecimento Selecionado não atende ao procedimento selecionado.');", true);
                ddlProcedimento.SelectedValue = "0";
            }
            ddlCBO.Items.Clear();
            ddlCBO.Items.Add(new ListItem("Selecione...", "0"));
            foreach (CBO cbo in intersecao)
                ddlCBO.Items.Add(new ListItem(cbo.Nome, cbo.Codigo));
        }

        protected void btnCompetencia_OnClick(object sender, EventArgs e)
        {
            ddlProcedimento.Items.Clear();
            ddlCBO.Items.Clear();
            ddlProfissional.Items.Clear();
            ddlProcedimento.Items.Add(new ListItem("Selecione...", "0"));
            ddlCBO.Items.Add(new ListItem("Selecione...", "0"));
            ddlProfissional.Items.Add(new ListItem("Selecione...", "0"));
            CarregaUnidades(int.Parse(tbxCompetencia.Text));
        }

        protected void CustomValidatorCompetencia_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (tbxCompetencia.Text != string.Empty)
            {
                args.IsValid = false;
            }
        }

        protected void btnGeraRelatorio_Click(object sender, EventArgs e)
        {
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();

            ViverMais.Model.EstabelecimentoSaude estabelecimento = iViverMais.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(ddlEstabelecimentoSaude.SelectedValue);
            ViverMais.Model.Profissional profissional = iViverMais.BuscarPorCodigo<ViverMais.Model.Profissional>(ddlProfissional.SelectedValue);
            Procedimento procedimento = iViverMais.BuscarPorCodigo<Procedimento>(ddlProcedimento.SelectedValue);
            CBO cbo = iViverMais.BuscarPorCodigo<CBO>(ddlCBO.SelectedValue);
            int competencia;
            try
            {
                competencia = int.Parse(tbxCompetencia.Text);
            }
            catch (InvalidCastException)
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Competência Invalida!');</script>");
                return;
            }

            Hashtable hash = Factory.GetInstance<IRelatorioAgendamento>().RelatorioAgendaMontadaPublicada<ViverMais.Model.EstabelecimentoSaude, ViverMais.Model.Profissional, Procedimento, CBO>(competencia, estabelecimento, profissional, procedimento, cbo);
            if (hash.Count != 0)
            {
                Session["HashAgendaMontadaPublicada"] = hash;
                Redirector.Redirect("RelatorioAgendaMontadaPublicada.aspx", "_blank", "");
                Usuario usuario = (Usuario)(Session["Usuario"]);
                Factory.GetInstance<IAmbulatorial>().Salvar(new LogAgendamento(DateTime.Now, usuario.Codigo, 48, "CNES_UNIDADE: " + usuario.Unidade.CNES));
            }
        }

        protected void ddlEstabelecimentoSaude_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProcedimento.Items.Clear();
            ddlProcedimento.Items.Add(new ListItem("Selecione...", "0"));
            ddlCBO.Items.Clear();
            ddlCBO.Items.Add(new ListItem("Selecione...", "0"));
            ddlProfissional.Items.Clear();
            ddlProfissional.Items.Add(new ListItem("Selecione...", "0"));
            CarregaProcedimento(int.Parse(tbxCompetencia.Text), ddlEstabelecimentoSaude.SelectedValue);
        }

        protected void ddlProcedimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCBO.Items.Clear();
            ddlProfissional.Items.Clear();
            ddlCBO.Items.Add(new ListItem("Selecione...", "0"));
            ddlProfissional.Items.Add(new ListItem("Selecione...", "0"));

            CarregaEspecialidades(int.Parse(tbxCompetencia.Text), ddlEstabelecimentoSaude.SelectedValue, ddlProcedimento.SelectedValue);        }

        protected void ddlCBO_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProfissional.Items.Clear();
            ddlProfissional.Items.Add(new ListItem("Selecione...", "0"));

            string cnes = ddlEstabelecimentoSaude.SelectedValue;

            // Monta lista de Profissionais ligados ao Vinculo do CNES
            ddlProfissional.Items.Clear();

            IList<ViverMais.Model.VinculoProfissional> vinculo = Factory.GetInstance<IVinculo>().BuscarPorCNESCBO<ViverMais.Model.VinculoProfissional>(cnes, ddlCBO.SelectedValue).Distinct(new GenericComparer<VinculoProfissional>("Profissional")).ToList();
            ddlProfissional.Items.Add(new ListItem("Selecione...", "0"));
            List<ViverMais.Model.Profissional> profissionais = new List<ViverMais.Model.Profissional>();

            foreach (ViverMais.Model.VinculoProfissional f in vinculo)
            {
                ViverMais.Model.Profissional prof = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.Profissional>(f.Profissional.CPF);
                if (prof != null)
                    profissionais.Add(prof);

            }
            profissionais = profissionais.OrderBy(p => p.Nome).ToList();
            foreach (ViverMais.Model.Profissional profissional in profissionais)
                ddlProfissional.Items.Add(new ListItem(profissional.Nome, profissional.CPF));
        }
    }
}
