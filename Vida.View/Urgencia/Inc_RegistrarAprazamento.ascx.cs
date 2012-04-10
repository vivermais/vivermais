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

namespace ViverMais.View.Urgencia
{
    public partial class Inc_RegistrarAprazamento : System.Web.UI.UserControl
    {
        public event EventHandler OnConfirmarAprazamento;

        public GridView IncGridMedicamentosRegistarAprazamento
        {
            get { return this.GridView_MedicamentosAprazar; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //HttpContext.Current.Cache.Insert("Pages", DateTime.Now, null,
                //System.DateTime.MaxValue, System.TimeSpan.Zero,
                //System.Web.Caching.CacheItemPriority.NotRemovable,
                //null);

                long co_prontuario = long.Parse(Request["codigo"].ToString());
                //ViewState["co_prontuario"] = co_prontuario;
                Prescricao p = Factory.GetInstance<IPrescricao>().RetornaPrescricaoVigente<Prescricao>(co_prontuario);
                
                if (p != null)
                {
                    Label_DataPrescricao.Text = p.Data.ToString("dd/MM/yyyy HH:mm:ss");
                    Label_Status.Text = p.DescricaoStatus;
                    Label_ValidadePrescricao.Text = p.UltimaDataValida.ToString("dd/MM/yyyy HH:mm:ss");
                    Session["co_prescricao"] = p.Codigo;
                    CarregaMedicamentosPrescricao(p.Codigo);
                }
                else
                {
                    Label_DataPrescricao.Text = " - ";
                    Label_Status.Text = " - ";
                    Label_ValidadePrescricao.Text = " - ";
                    CarregaMedicamentosPrescricao(-1);
                }
            }

            Response.Cache.SetExpires(DateTime.Now.AddSeconds(1));
            Response.Cache.SetCacheability(HttpCacheability.Private);

            //Response.Cache.SetCacheability(HttpCacheability.NoCache); 
            //Response.AddCacheItemDependency("Pages");
        }

        /// <summary>
        /// Carrega os itens a serem aprazados da prescrição válida
        /// </summary>
        /// <param name="co_prescricao"></param>
        private void CarregaMedicamentosPrescricao(long co_prescricao)
        {
            GridView_MedicamentosAprazar.DataSource = Factory.GetInstance<IPrescricao>().BuscarMedicamentosAprazadosPorPrescricaoVigente<AprazamentoMedicamento>(co_prescricao);
            GridView_MedicamentosAprazar.DataBind();
        }

        /// <summary>
        /// Formata o gridview de medicamentos com seus respectivos aprazamentos a serem confirmados de acordo com o padrão específico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridViewMedicamentos(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) 
            {
                char[] delimitador = { '-' };
                string[] codigoitem = GridView_MedicamentosAprazar.DataKeys[e.Row.RowIndex]["CodigoItem"].ToString().Split(delimitador);
                AprazamentoMedicamento am = Factory.GetInstance<IPrescricao>().BuscarMedicamentosAprazadosPorPrescricaoVigente<AprazamentoMedicamento>(long.Parse(Session["co_prescricao"].ToString())).Where(p => p.CodigoMedicamento == int.Parse(codigoitem[0]) && p.Horario == DateTime.Parse(codigoitem[1])).First();

                if (am.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Bloqueado))
                {
                    LinkButton lb = (LinkButton)e.Row.FindControl("LinkButton_ConfirmarExcecucao");

                    if (lb != null)
                        lb.OnClientClick = "javascript:return confirm('A execução deste procedimento foi bloqueado por um dos seguintes motivos:\\n\\n (1) A sua execução ultrapassou o tempo máximo de 2 horas. \\n (2) Um procedimento anterior que não fora executado acabou por bloquear o seu registro. \\n\\n Deseja desbloqueá-lo e executá-lo agora ?');";
                }
                else
                {
                    if (am.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Finalizado))
                    {
                        LinkButton lb = (LinkButton)e.Row.FindControl("LinkButton_ConfirmarExcecucao");

                        if (lb != null)
                        {
                            lb.Enabled = false;
                            lb.OnClientClick = lb.OnClientClick.Length > 0 ? lb.OnClientClick.Remove(0) : lb.OnClientClick;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Cancela a confirmação de aprazamento para um medicamento específico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCancelingEdit_CancelarAprazamentoMedicamento(object sender, GridViewCancelEditEventArgs e) 
        {
            GridView_MedicamentosAprazar.EditIndex = -1;
            CarregaMedicamentosPrescricao(long.Parse(Session["co_prescricao"].ToString()));
        }

        /// <summary>
        /// Edita o gridview com as informações necessárias para confirmar a excecução do aprazamento de um medicamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowEditing_EditarAprazamentoMedicamento(object sender, GridViewEditEventArgs e) 
        {
            char[] delimitador = { '-' };
            string[] codigoitem = GridView_MedicamentosAprazar.DataKeys[e.NewEditIndex]["CodigoItem"].ToString().Split(delimitador);

            AprazamentoMedicamento am = Factory.GetInstance<IPrescricao>().BuscarMedicamentosAprazadosPorPrescricaoVigente<AprazamentoMedicamento>(long.Parse(Session["co_prescricao"].ToString())).Where(p => p.CodigoMedicamento == int.Parse(codigoitem[0]) && p.Horario == DateTime.Parse(codigoitem[1])).First();
            AprazamentoMedicamento amtemp = Factory.GetInstance<IPrescricao>().BuscarMedicamentosAprazadosPorPrescricaoVigente<AprazamentoMedicamento>(long.Parse(Session["co_prescricao"].ToString())).Where(p => p.CodigoMedicamento == int.Parse(codigoitem[0]) && p.Horario.CompareTo(am.Horario) < 0 && (p.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Bloqueado) || p.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Ativo))).FirstOrDefault();

            if (am.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Bloqueado) && !Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<Operacao>(12).Nome))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para executar um procedimento bloqueado! Por favor, procure a supervisão.');", true);
            else
            {
                if (amtemp != null)
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, existe um procedimento para este medicamento com horário de execução menor do que o escolhido que deve ser confirmado antes!');", true);
                else
                {
                    GridView_MedicamentosAprazar.EditIndex = e.NewEditIndex;
                    CarregaMedicamentosPrescricao(long.Parse(Session["co_prescricao"].ToString()));
                }
            }
        }

        /// <summary>
        /// Finaliza a execução do aprazamento para um medicamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowUpdating_ConfirmarAprazamentoMedicamento(object sender, GridViewUpdateEventArgs e) 
        {
            try
            {
                char[] delimitador = { '/', ':' };
                char[] delimitador2 = { '-' };
                string[] codigoitem = GridView_MedicamentosAprazar.DataKeys[e.RowIndex]["CodigoItem"].ToString().Split(delimitador2);
                GridViewRow linha = GridView_MedicamentosAprazar.Rows[e.RowIndex];
                string[] data = ((TextBox)linha.FindControl("TextBox_DataExecucao")).Text.Split(delimitador);
                string[] horario = ((TextBox)linha.FindControl("TextBox_HoraExecucao")).Text.Split(delimitador);
                DateTime horaexecucao = new DateTime(int.Parse(data[2]), int.Parse(data[1]), int.Parse(data[0]), int.Parse(horario[0]), int.Parse(horario[1]), 0);

                AprazamentoMedicamento am = Factory.GetInstance<IPrescricao>().BuscarMedicamentosAprazadosPorPrescricaoVigente<AprazamentoMedicamento>(long.Parse(Session["co_prescricao"].ToString())).Where(p => p.CodigoMedicamento == int.Parse(codigoitem[0]) && p.Horario == DateTime.Parse(codigoitem[1])).First();

                if (horaexecucao.CompareTo(am.Prescricao.UltimaDataValida) > 0)
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O horário de confirmação não pode ser maior que o período de vigência da prescrição.');", true);
                else
                    if (horaexecucao.CompareTo(am.Prescricao.UltimaDataValida.AddDays(-1)) < 0)
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O horário de confirmação não pode ser menor que o período de vigência da prescrição.');", true);
                    else
                    {
                        am.Status = Convert.ToChar(AprazamentoMedicamento.StatusItem.Finalizado);
                        am.HorarioConfirmacao = horaexecucao;
                        am.CodigoProfissionalConfirmacao = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo).Id_Profissional;
                        am.HorarioConfirmacaoSistema = DateTime.Now;
                        
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Procedimento confirmado com sucesso.');", true);
                        GridView_MedicamentosAprazar.EditIndex = -1;
                        Factory.GetInstance<IPrescricao>().AtualizarStatusItensAprazadosPrescricaoVigente<Prescricao>(Factory.GetInstance<IPrescricao>().RetornaPrescricaoVigente<Prescricao>(long.Parse(Request["codigo"].ToString())));

                        CarregaMedicamentosPrescricao(long.Parse(Session["co_prescricao"].ToString()));
                        this.OnConfirmarAprazamento.Invoke(this, new EventArgs());
                    }
            }
            catch (Exception f)
            {
                throw f;
            }
        }
    }
}