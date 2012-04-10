﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;

namespace ViverMais.View.Urgencia
{
    public partial class Inc_MenuHistorico : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BulletedList_Menu.Items.Add(new ListItem("Acolhimento", "FormImprimirHistoricoProntuario.aspx?co_prontuario=" + Request["codigo"].ToString() + "&tipo=acolhimento"));
                    BulletedList_Menu.Items.Add(new ListItem("Aprazamentos", "FormImprimirHistoricoProntuario.aspx?co_prontuario=" + Request["codigo"].ToString() + "&tipo=aprazamentos"));
                    BulletedList_Menu.Items.Add(new ListItem("Atestados/Receitas", "FormImprimirHistoricoProntuario.aspx?co_prontuario=" + Request["codigo"].ToString() + "&tipo=atestadosreceitas"));
                    BulletedList_Menu.Items.Add(new ListItem("Consulta Médica", "FormImprimirHistoricoProntuario.aspx?co_prontuario=" + Request["codigo"].ToString() + "&tipo=consultamedica"));
                    BulletedList_Menu.Items.Add(new ListItem("Evoluções de Enfermagem", "FormImprimirHistoricoProntuario.aspx?co_prontuario=" + Request["codigo"].ToString() + "&tipo=evolucoesenfermagem"));
                    BulletedList_Menu.Items.Add(new ListItem("Evoluções Médica", "FormImprimirHistoricoProntuario.aspx?co_prontuario=" + Request["codigo"].ToString() + "&tipo=evolucoesmedica"));
                    BulletedList_Menu.Items.Add(new ListItem("Exames Eletivos", "FormImprimirHistoricoProntuario.aspx?co_prontuario=" + Request["codigo"].ToString() + "&tipo=exameseletivos"));
                    BulletedList_Menu.Items.Add(new ListItem("Exames Internos", "FormImprimirHistoricoProntuario.aspx?co_prontuario=" + Request["codigo"].ToString() + "&tipo=exames"));

                    Prontuario prontuario = Factory.GetInstance<IProntuario>().BuscarPorCodigo<ViverMais.Model.Prontuario>(long.Parse(Request["codigo"].ToString()));

                    if (string.IsNullOrEmpty(prontuario.Paciente.Descricao))
                        BulletedList_Menu.Items.Add(new ListItem("Ficha de Atendimento", "FormImprimirFichaAtendimento.aspx?numeroatendimento=" + prontuario.NumeroToString));

                    BulletedList_Menu.Items.Add(new ListItem("Prescrições", "FormImprimirHistoricoProntuario.aspx?co_prontuario=" + Request["codigo"].ToString() + "&tipo=prescricoes"));

                    //if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "IMPRIMIR_RELATORIO_GERAL_PACIENTE"))
                    UsuarioProfissionalUrgence usuarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);

                    if (usuarioprofissional != null)
                    {
                        ICBO iCbo = Factory.GetInstance<ICBO>();
                        CBO cbo = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<CBO>(usuarioprofissional.CodigoCBO);
                        
                        if (iCbo.CBOPertenceMedico<CBO>(cbo))
                            BulletedList_Menu.Items.Add(new ListItem("Relatório Geral", "FormImprimirHistoricoProntuario.aspx?co_prontuario=" + Request["codigo"].ToString() + "&tipo=relatoriogeral"));
                    }

                    BulletedList_Menu.DataBind();
                }
                catch (Exception f)
                {
                    throw f;
                }
            }
        }

        /// <summary>
        /// Redireciona o usuário para uma janela específica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_RedirecionaJanela(object sender, BulletedListEventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "window.open('" + BulletedList_Menu.Items[e.Index].Value + "');", true);
        }
    }
}