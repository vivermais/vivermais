using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades;
using System.Drawing;

namespace ViverMais.View.Urgencia
{
    public partial class Inc_Aprazamento : System.Web.UI.UserControl
    {
        public event EventHandler OnRegistrarAprazamento;

        public GridView IncGridMedicamentosInserirAprazamento
        {
            get { return this.GridView_AprazamentoMedicamentoRegistrado; }
        }

        public GridView Inc_GridMedicamentosASeremAprazados 
        {
            get { return this.gridMedicamentos; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove("co_prescricao");
                long co_prontuario = long.Parse(Request["codigo"].ToString());
                //ViewState["co_prontuario"] = co_prontuario;
                CarregaPrescricao(co_prontuario);
                CarregaItens(-1);
                OnClick_CancelarMedicamentoAprazamento(sender, e);
            }
        }

        /// <summary>
        /// Carrega as prescrições do prontuário
        /// </summary>
        /// <param name="co_prontuario"></param>
        private void CarregaPrescricao(long co_prontuario)
        {
            IList<Prescricao> pp = new List<Prescricao>();

            Prescricao p = Factory.GetInstance<IPrescricao>().RetornaPrescricaoVigente<Prescricao>(co_prontuario);
            if (p != null)
                pp.Add(p);

            GridView_PrescricoesRegistradas.DataSource = pp;
            GridView_PrescricoesRegistradas.DataBind();
        }

        /// <summary>
        /// Verifica se a prescrição foi selecionada para aprazar os medicamentos/kit/procedimentos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCommand_SelecionarPrescricao(object sender, GridViewCommandEventArgs e) 
        {
            if (e.CommandName == "CommandName_Selecionar")
            {
                Session["co_prescricao"] = GridView_PrescricoesRegistradas.DataKeys[int.Parse(e.CommandArgument.ToString())]["Codigo"].ToString();
                CarregaItens(long.Parse(Session["co_prescricao"].ToString()));
            }
        }

        /// <summary>
        /// Carrega os itens da prescrição selecionada
        /// </summary>
        /// <param name="co_prescricao"></param>
        public void CarregaItens(long co_prescricao)
        {
            GridView_ProcedimentoNaoFaturavel.DataSource = Factory.GetInstance<IPrescricao>().BuscarProcedimentosNaoFaturaveis<PrescricaoProcedimentoNaoFaturavel>(co_prescricao);
            GridView_ProcedimentoNaoFaturavel.DataBind();

            IList<PrescricaoMedicamento> lpm = Factory.GetInstance<IPrescricao>().BuscarMedicamentos<PrescricaoMedicamento>(co_prescricao);
            foreach (PrescricaoMedicamento pm in lpm)
                pm.ObjetoMedicamento = Factory.GetInstance<IMedicamento>().BuscarPorCodigo<Medicamento>(pm.Medicamento);
            gridMedicamentos.DataSource = lpm;
            gridMedicamentos.DataBind();

            CarregaMedicamentosJaAprazados(co_prescricao);

            IList<PrescricaoProcedimento> lpp = Factory.GetInstance<IPrescricao>().BuscarProcedimentos<PrescricaoProcedimento>(co_prescricao);
            foreach (PrescricaoProcedimento pp in lpp)
                pp.Procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(pp.CodigoProcedimento);
            GridView_Procedimento.DataSource = lpp;
            GridView_Procedimento.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCommand_AprazarProcedimento(object sender, GridViewCommandEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCommand_AprazarMedicamento(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CommandName_Aprazar")
            {
                int co_medicamento = int.Parse(gridMedicamentos.DataKeys[int.Parse(e.CommandArgument.ToString())]["Medicamento"].ToString());
                Medicamento m = Factory.GetInstance<IMedicamento>().BuscarPorCodigo<Medicamento>(co_medicamento);
                ViewState["co_medicamentoaprazar"] = co_medicamento;
                OnClick_CancelarMedicamentoAprazamento(new object(), new EventArgs());
                DropDownList_MedicamentoAprazar.Items.Add(new ListItem(m.Nome, m.Codigo.ToString()));
                DropDownList_MedicamentoAprazar.SelectedValue = m.Codigo.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCommand_AprazarKit(object sender, GridViewCommandEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCommand_AprazarProcedimentoNaoFaturavel(object sender, GridViewCommandEventArgs e) 
        {
        }

        protected void OnRowDataBound_FormataGridViewMedicamentos(object sender, GridViewRowEventArgs e) 
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[5].Text == "Suspenso")
                {
                    LinkButton lb = (LinkButton)e.Row.Cells[6].Controls[0];
                    lb.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Adicionar medicamento para aprazamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_AdicionarMedicamentoAprazamento(object sender, EventArgs e)
        {
            try
            {
                Prescricao prescricao = Factory.GetInstance<IPrescricao>().BuscarPorCodigo<Prescricao>(long.Parse(Session["co_prescricao"].ToString()));

                char[] delimitador = { '/', ':' };
                string[] stringdata = TextBox_DataAprazarMedicamento.Text.Split(delimitador);
                string[] stringhora = TextBox_HoraAprazarMedicamento.Text.Split(delimitador);

                IList<AprazamentoMedicamento> lam = Factory.GetInstance<IPrescricao>().BuscarMedicamentosAprazadosPorPrescricaoVigente<AprazamentoMedicamento>(prescricao.Codigo);
                AprazamentoMedicamento am = new AprazamentoMedicamento();
                am.CodigoMedicamento = int.Parse(DropDownList_MedicamentoAprazar.SelectedValue);
                am.Medicamento = Factory.GetInstance<IMedicamento>().BuscarPorCodigo<Medicamento>(am.CodigoMedicamento);
                am.Horario = new DateTime(int.Parse(stringdata[2]), int.Parse(stringdata[1]), int.Parse(stringdata[0]), int.Parse(stringhora[0]), int.Parse(stringhora[1]), 0);
                am.Status = Convert.ToChar(AprazamentoMedicamento.StatusItem.Ativo);
                am.CodigoProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo).Id_Profissional;
                am.HorarioValidoPrescricao = prescricao.UltimaDataValida;
                am.Prescricao = prescricao;

                if (lam.Where(p => p.CodigoMedicamento == am.CodigoMedicamento && p.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Bloqueado)).FirstOrDefault() != null) 
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não é possível registrar um novo horário de aprazamento para o medicamento escolhido, pois existe(m) aprazamento(s) bloqueado(s). Para resolver este impasse realize uma das opções abaixo: \\n\\n (1) Desbloqueie e execute todos as aprazamentos bloqueados. \\n (2) Exclua todos os aprazamentos bloqueados.');", true);
                }else{
                    if (lam.Where(p => p.CodigoMedicamento == am.CodigoMedicamento && p.Horario.CompareTo(am.Horario) == 0).FirstOrDefault() != null)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe um mesmo horário de aprazamento para este medicamento! Por favor, informe outro horário.');", true);
                    }
                    else
                    {
                        if (am.Horario.CompareTo(DateTime.Now) <= 0)
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A data e hora informada para a execução do procedimento não pode ser igual ou menor que a data e hora atual.');", true);
                        else
                        {
                            if (am.Horario.CompareTo(prescricao.UltimaDataValida) >= 0)
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A data e hora informada para a execução do procedimento não pode ser igual ou maior que a data válida para aprazamento da prescrição.');", true);
                            else
                            {
                                TextBox_DataAprazarMedicamento.Text = "";
                                TextBox_HoraAprazarMedicamento.Text = "";
                                Factory.GetInstance<IUrgenciaServiceFacade>().Inserir(am);
                                CarregaMedicamentosJaAprazados(prescricao.Codigo);
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Horário de aprazamento incluído com sucesso.');", true);
                                this.OnRegistrarAprazamento.Invoke(this, new EventArgs());
                            }
                        }
                    }
                }
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Cancelar adição do medicamento para aprazamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_CancelarMedicamentoAprazamento(object sender, EventArgs e)
        {
            DropDownList_MedicamentoAprazar.Items.Clear();
            DropDownList_MedicamentoAprazar.Items.Add(new ListItem("Selecione...","-1"));
            DropDownList_MedicamentoAprazar.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
            DropDownList_MedicamentoAprazar.SelectedValue = "-1";
            TextBox_DataAprazarMedicamento.Text = "";
            TextBox_HoraAprazarMedicamento.Text = "";
        }

        /// <summary>
        /// Carrega os medicamentos já aprazados no horário corrente da prescrição atual
        /// </summary>
        /// <param name="co_prescricao"></param>
        /// <param name="horario"></param>
        private void CarregaMedicamentosJaAprazados(long co_prescricao)
        {
            GridView_AprazamentoMedicamentoRegistrado.DataSource = Factory.GetInstance<IPrescricao>().BuscarMedicamentosAprazadosPorPrescricaoVigente<AprazamentoMedicamento>(co_prescricao);
            GridView_AprazamentoMedicamentoRegistrado.DataBind();
        }

        protected void OnRowDeleting_DeletarMedicamentoAprazamentoRegistrado(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                char[] delimitador = { '-' };
                string[] codigoitem = GridView_AprazamentoMedicamentoRegistrado.DataKeys[e.RowIndex]["CodigoItem"].ToString().Split(delimitador);
                AprazamentoMedicamento am = Factory.GetInstance<IPrescricao>().BuscarMedicamentosAprazadosPorPrescricaoVigente<AprazamentoMedicamento>(long.Parse(Session["co_prescricao"].ToString())).Where(p => p.CodigoMedicamento == int.Parse(codigoitem[0]) && p.Horario == DateTime.Parse(codigoitem[1])).First();

                Factory.GetInstance<IUrgenciaServiceFacade>().Deletar(am);

                GridView_AprazamentoMedicamentoRegistrado.EditIndex = -1;
                CarregaMedicamentosJaAprazados(long.Parse(Session["co_prescricao"].ToString()));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Horário de aprazamento excluído com sucesso.');", true);
                this.OnRegistrarAprazamento.Invoke(this, new EventArgs());
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        protected void OnRowDataBound_FormataGridViewMedicamentoJaRegistrados(object sender, GridViewRowEventArgs e) 
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                char[] delimitador = { '-' };
                string[] codigoitem = GridView_AprazamentoMedicamentoRegistrado.DataKeys[e.Row.RowIndex]["CodigoItem"].ToString().Split(delimitador);
                AprazamentoMedicamento am = Factory.GetInstance<IPrescricao>().BuscarMedicamentosAprazadosPorPrescricaoVigente<AprazamentoMedicamento>(long.Parse(Session["co_prescricao"].ToString())).Where(p => p.CodigoMedicamento == int.Parse(codigoitem[0]) && p.Horario == DateTime.Parse(codigoitem[1])).First();

                if (Convert.ToChar(AprazamentoMedicamento.StatusItem.Finalizado) == am.Status)
                {
                    LinkButton lbexcluir = (LinkButton)e.Row.FindControl("LinkButton_Excluir");

                    if (lbexcluir != null)
                    {
                        lbexcluir.Enabled = false;
                        lbexcluir.OnClientClick = lbexcluir.OnClientClick.Length > 0 ? lbexcluir.OnClientClick.Remove(0) : lbexcluir.OnClientClick;
                    }
                }
            }
        }
    }
}