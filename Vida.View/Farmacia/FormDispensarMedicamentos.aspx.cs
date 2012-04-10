﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Dispensacao;
using System.Collections;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Inventario;

namespace ViverMais.View.Farmacia
{
    public partial class FormDispensarMedicamentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove("itensdispensar");
                Session.Remove("receita");
                Session.Remove("medicamentosReceita");
                Session.Remove("dispensacao");
                Session.Remove("estoqueMedicamentosFarmacia");

                long temp;
                if (Request["co_receita"] != null && long.TryParse(Request["co_receita"].ToString(), out temp))                {
                    Usuario usuario = (Usuario)Session["Usuario"];
                    bool permissao = Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "DISPENSAR_MEDICAMENTO");
                    if (permissao)
                    {
                        Label_Tit_DataAtendimento.Visible = true;
                        tbxDataAtendimento.ReadOnly = true;
                        tbxDataAtendimento.Text = DateTime.Now.ToString("dd/MM/yyyy");

                        ViewState["co_receita"] = Request["co_receita"].ToString();

                        ReceitaDispensacao r = RetornaReceitaCorrente();
                        //TimeSpan span = DateTime.Now.Subtract(r.DataReceita);

                        if ((r.ReceitaVencida) && (Request.QueryString["co_farmacia"] == null))
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Receita com validade superior a 6 meses! Por favor, trocar de receita.');location='Default.aspx';", true);
                        else
                        {
                            if (r.ReceitaPrestesAVencer)
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Receita vence em "+r.TempoRestanteValidade+" dias.');", true);
                            }

                            Label_NumeroReceita.Text = r.Codigo.ToString();
                            Label_NomePaciente.Text = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(r.CodigoPaciente).Nome;
                            Label_NomeProfissional.Text = Factory.GetInstance<IProfissionalSolicitante>().BuscarPorCodigo<ViverMais.Model.ProfissionalSolicitante>(Convert.ToInt32(r.CodigoProfissional)).Nome;
                            Label_DataReceita.Text = r.DataReceita.ToString("dd/MM/yyyy");
                            AtualizaGridDispensacao(RetornaItensDispensar());


                            if (Request.QueryString["co_farmacia"] != null)
                            {
                                int co_farmacia = int.Parse(Request.QueryString["co_farmacia"]);
                                ViverMais.Model.Farmacia farm = Factory.GetInstance<IFarmacia>().BuscarPorCodigo<ViverMais.Model.Farmacia>(co_farmacia);
                                Label_FarmaciaDispensacao.Visible = true;
                                Label_FarmaciaDispensacao.Text = farm.Nome;
                                DropDownList_Farmacia.Visible = false;
                                Panel_ConteudoDispensacao.Visible = true;
                            }
                            else
                            {
                                IList<ViverMais.Model.Farmacia> farmacias = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia, Usuario>(usuario, true, true);

                                if (farmacias.Count() == 1) //Farmácia Default 'Selecione'
                                {
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não está vinculado a farmácia alguma! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                                    return;
                                }
                                else
                                {
                                    if (farmacias.Count > 2)
                                    {
                                        //Panel_Farmacia.Visible = true;
                                        Label_FarmaciaDispensacao.Visible = false;
                                        CompareValidator_PesquisarMedicamentoFarmacia.Enabled = true;
                                        CompareValidator_DispensarMedicamento.Enabled = true;
                                        DropDownList_Farmacia.DataSource = farmacias;
                                        DropDownList_Farmacia.DataBind();
                                    }
                                    else
                                    {
                                        Label_FarmaciaDispensacao.Text = farmacias[1].Nome;
                                        DropDownList_Farmacia.Visible = false;
                                        Panel_ConteudoDispensacao.Visible = true;
                                    }
                                }
                            }
                            if (Request.QueryString["co_farmacia"] != null)
                            {
                                ViewState["co_farmacia"] = Request["co_farmacia"].ToString();
                                ViewState["dataAtendimento"] = Request["dataAtendimento"].ToString();
                                DateTime dataAtendimento = DateTime.Parse(ViewState["dataAtendimento"].ToString());
                                Label_DataAtendimento.Visible = true;
                                Panel_MedicamentosDispensados.Visible = true;
                                Label_DataAtendimento.Text = dataAtendimento.ToString("dd/MM/yyyy");
                                IList<ItemDispensacao> itens = Factory.GetInstance<IDispensacao>().BuscarItensPorAtendimento<ItemDispensacao>(dataAtendimento, long.Parse(ViewState["co_receita"].ToString()), int.Parse(ViewState["co_farmacia"].ToString()));
                                Session["itensdispensar"] = itens;
                                AtualizaGridDispensacao(itens);

                                if (r.ReceitaVencida)
                                {
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Receita com validade superior a 6 meses! Por favor, trocar de receita.');", true);
                                    GridView_MedicamentosDispensar.Columns[6].Visible = false;
                                    GridView_MedicamentosDispensar.Columns[7].Visible = false;
                                    Panel_ConteudoDispensacao.Visible = false;
                                }
                                BloqueiaInclusaoAlteracaoExclusao();
                                tbxDataAtendimento.Visible = false;
                            }
                            else
                                Label_DataAtendimento.Visible = false;
                        }
                    }
                    else
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                }
                else if(Request["co_dispensacao"] != null)
                {
                    Usuario usuario = (Usuario)Session["Usuario"];
                    bool permissao = Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "EDITAR_ULTIMA_DISPENSACAO");
                    if (permissao)
                    {
                        int codigoDispensacao = int.Parse(Request["co_dispensacao"]);
                        Dispensacao dispensacao = Factory.GetInstance<IDispensacao>().BuscarPorCodigo<Dispensacao>(codigoDispensacao);
                        dispensacao.Farmacia = Factory.GetInstance<IFarmacia>().BuscarPorCodigo<ViverMais.Model.Farmacia>(dispensacao.Farmacia.Codigo);
                        dispensacao.Receita = Factory.GetInstance<IReceitaDispensacao>().BuscarPorCodigo<ReceitaDispensacao>(dispensacao.Receita.Codigo);

                        if (dispensacao.Receita.ReceitaVencida)
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Receita com validade superior a 6 meses! Por favor, trocar de receita.');location='Default.aspx';", true);

                        if (dispensacao.Receita.ReceitaPrestesAVencer)                        
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Receita vence em " + dispensacao.Receita.TempoRestanteValidade + " dias.');", true);
                        

                        dispensacao.Receita.ItensPrescritos = Factory.GetInstance<IReceitaDispensacao>().BuscarMedicamentos<ItemReceitaDispensacao>(dispensacao.Receita.Codigo).ToList();

                        List<ItemDispensacao> itensDispensados = Factory.GetInstance<IDispensacao>().BuscarItensDispensacao<ItemDispensacao>(codigoDispensacao);
                        
                        Label_Tit_DataAtendimento.Visible = true;
                        tbxDataAtendimento.ReadOnly = true;
                        tbxDataAtendimento.Text = DateTime.Now.ToString("dd/MM/yyyy");

                        foreach (ItemDispensacao item in itensDispensados)
                        {
                            item.Dispensacao = dispensacao;
                        }

                        dispensacao.ItensDispensados = itensDispensados;

                        Session["dispensacao"] = dispensacao;
                        Session["itensdispensar"] = itensDispensados;

                        
                        Label_NumeroReceita.Text = dispensacao.Receita.Codigo.ToString();
                        Label_NomePaciente.Text = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(dispensacao.Receita.CodigoPaciente).Nome;
                        Label_NomeProfissional.Text = Factory.GetInstance<IProfissionalSolicitante>().BuscarPorCodigo<ViverMais.Model.ProfissionalSolicitante>(Convert.ToInt32(dispensacao.Receita.CodigoProfissional)).Nome;
                        Label_DataReceita.Text = dispensacao.Receita.DataReceita.ToString("dd/MM/yyyy");
                        GridView_MedicamentosDispensar.Columns[GridView_MedicamentosDispensar.Columns.Count - 1].Visible = false;
                        AtualizaGridDispensacao(RetornaItensDispensar());

                        Label_FarmaciaDispensacao.Text = dispensacao.Farmacia.Nome;
                        Label_DataAtendimento.Text = dispensacao.DataAtendimento.ToString("dd/MM/yyyy");

                        ButtonCancelarDispensacao.Visible = false;
                        ButtonConcluirDispensacao.Text = "Concluir alteração";

                        Label_DataAtendimento.Visible = true;
                        Label_FarmaciaDispensacao.Visible = true;
                        tbxDataAtendimento.Visible = false;
                        DropDownList_Farmacia.Visible = false;                        

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                    }
                }
            }
        }

        private void PreencherDropDownMedicamentos(int codigoFarmacia, ReceitaDispensacao receita)
        {
            IList<Estoque> estoqueMedicamentosFarmacia = RetornaItensDispensacaoEstoqueFarmacia();

            if (estoqueMedicamentosFarmacia.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Nenhum medicamento prescrito na receita e que tenha quantidade disponível para dispensação foi encontrado na farmácia indicada.');", true);
                Panel_ConteudoDispensacao.Visible = false;
            }
            DropDownList_Medicamento.DataSource = estoqueMedicamentosFarmacia;
            DropDownList_Medicamento.DataBind();
            DropDownList_Medicamento.Items.Insert(0, new ListItem("Selecione...", "-1"));
        }

        private List<Estoque> RetornaItensDispensacaoEstoqueFarmacia()
        {

            List<Estoque> estoqueMedicamentosFarmacia = (List<Estoque>)Session["estoqueMedicamentosFarmacia"];
            if (estoqueMedicamentosFarmacia == null)
            {
                IList<ItemReceitaDispensacao> medicamentosReceita = RetornaItensReceitaCorrente();
                //IList<Estoque> les = Factory.GetInstance<IEstoque>().BuscarPorNomeMedicamentoQuantidadeSuperior<Estoque>(TextBox_BuscarMedicamento.Text, DropDownList_Farmacia.Visible ? int.Parse(DropDownList_Farmacia.SelectedValue) : int.Parse(ViewState["co_farmacia"].ToString()));
                List<int> codigosMedicamentosReceita = new List<int>();

                foreach (ItemReceitaDispensacao itemReceita in medicamentosReceita)
                {
                    codigosMedicamentosReceita.Add(itemReceita.Medicamento.Codigo);
                }

                estoqueMedicamentosFarmacia = Factory.GetInstance<IEstoque>().BuscarPorFarmaciaReceita<Estoque>(RetornaCodigoFarmacia(), codigosMedicamentosReceita.ToArray()).ToList();
                Session["estoqueMedicamentosFarmacia"] = estoqueMedicamentosFarmacia;
            }
            return estoqueMedicamentosFarmacia;
        }

        protected void OnSelectedIndexChanged_CarregaConteudoDispensacao(object sender, EventArgs e)
        {
            Session.Remove("estoqueMedicamentosFarmacia");

            //Verifica se existe inventário em aberto para a farmácia
            //Caso exista não deixa realizar o procedimento            
            IList<Inventario> inventariosAbertos = Factory.GetInstance<IInventario>().BuscarPorSituacao<Inventario>('A', Convert.ToInt32(DropDownList_Farmacia.SelectedValue));
            if (inventariosAbertos.Count > 0)
            {
                //ClientScript.RegisterClientScriptBlock(Page.GetType(), "mensagem", "<script language='javascript'>alert('Necessário fechar o inventário aberto em " + inventariosAbertos[0].DataInventario.ToString("dd/MM/yyyy") + " antes de realizar esta operação.');document.location.href='FormGerarReceitaDispensacao.aspx';</script>");
                ScriptManager.RegisterClientScriptBlock(DropDownList_Farmacia, Page.GetType(), "mensagem", "<script language='javascript'>alert('Necessário fechar o inventário aberto em " + inventariosAbertos[0].DataInventario.ToString("dd/MM/yyyy") + " antes de realizar esta operação.');document.location.href='FormGerarReceitaDispensacao.aspx';</script>", false);
            }

            //AtualizaGridDispensacao(RetornaItensDispensar());
            //OnClick_CancelarInclusao(sender, e);
            LimparFormItemDispensado();

            if (DropDownList_Farmacia.SelectedValue != "-1")
            {
                ReceitaDispensacao receita = RetornaReceitaCorrente();
                Panel_ConteudoDispensacao.Visible = true;
                PreencherDropDownMedicamentos(RetornaCodigoFarmacia(), receita);
            }
            else
                Panel_ConteudoDispensacao.Visible = false;
        }

        protected int RetornaCodigoFarmacia()
        {
            int co_farmacia;
            if (Request.QueryString["co_farmacia"] != null)
                co_farmacia = int.Parse(Request.QueryString["co_farmacia"].ToString());
            else
                if (DropDownList_Farmacia.Visible)
                    co_farmacia = int.Parse(DropDownList_Farmacia.SelectedValue);
                else
                {
                    Usuario usuario = (Usuario)Session["Usuario"];
                    co_farmacia = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia, Usuario>(usuario, true, true)[1].Codigo;
                }
                    //co_farmacia = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia>(((Usuario)Session["Usuario"]).Codigo).Codigo;

            return co_farmacia;
        }

        protected void OnSelectedIndexChanged_FormataCamposDispensacao(object sender, EventArgs e)
        {
            ReceitaDispensacao receita = RetornaReceitaCorrente();
            List<ItemReceitaDispensacao> medicamentosReceita = receita.ItensPrescritos;
            List<Estoque> estoqueDisponivelDispensacao = (List<Estoque>)Session["estoqueMedicamentosFarmacia"];
            ItemReceitaDispensacao medicamentoSelecionado = medicamentosReceita.Find(x => x.Medicamento.Codigo == estoqueDisponivelDispensacao.Find(a => a.LoteMedicamento.Codigo == Convert.ToInt32(DropDownList_Medicamento.SelectedValue)).Medicamento.Codigo);
            TextBox_QuantidadePrescrita.Enabled = true;
            TextBox_QuantidadePrescrita.Text = medicamentoSelecionado.QtdPrescrita.ToString();
            TxtSaldoMedicamento.Text = receita.SaldoRestanteMedicamento(medicamentoSelecionado.Medicamento.Codigo).ToString();

            /*TextBox_QuantidadePrescrita.Enabled = true;

            if (DropDownList_Medicamento.SelectedValue != "-1")
            {
                ItemDispensacao pid = Factory.GetInstance<IDispensacao>().BuscarPrimeiroItemDispensadoPorReceita<ItemDispensacao>(long.Parse(ViewState["co_receita"].ToString()), Factory.GetInstance<ILoteMedicamento>().BuscarPorCodigo<LoteMedicamento>(int.Parse(DropDownList_Medicamento.SelectedValue)).Medicamento.Codigo);
                ItemDispensacao id = RetornaItensDispensar().Where(p => p.LoteMedicamento.Medicamento.Codigo == Factory.GetInstance<ILoteMedicamento>().BuscarPorCodigo<LoteMedicamento>(int.Parse(DropDownList_Medicamento.SelectedValue)).Medicamento.Codigo).FirstOrDefault();

                if (pid != null)
                {
                    TextBox_QuantidadePrescrita.Text = pid.QtdPrescrita.ToString();
                    TextBox_QuantidadePrescrita.Enabled = false;
                }
                else
                {
                    if (id != null)
                    {
                        TextBox_QuantidadePrescrita.Text = id.QtdPrescrita.ToString();
                        TextBox_QuantidadePrescrita.Enabled = false;
                    }
                }
            }*/
        }

        private ReceitaDispensacao RetornaReceitaCorrente()
        {
            ReceitaDispensacao receita = (ReceitaDispensacao)Session["receita"];
            if (receita == null)
            {
                receita = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ReceitaDispensacao>(long.Parse(ViewState["co_receita"].ToString()));
                receita.ItensPrescritos = Factory.GetInstance<IReceitaDispensacao>().BuscarMedicamentos<ItemReceitaDispensacao>(receita.Codigo).ToList();
                receita.Dispensacoes = Factory.GetInstance<IDispensacao>().BuscarDispensacoesPorReceita<Dispensacao>(receita.Codigo).OrderBy(x => x.DataAtendimento).ToList();
                foreach (Dispensacao dispensacao in receita.Dispensacoes)
                {
                    dispensacao.ItensDispensados = Factory.GetInstance<IDispensacao>().BuscarItensDispensacao<ItemDispensacao>(dispensacao.Codigo);
                }
            }
            return receita;
        }

        private List<ItemReceitaDispensacao> RetornaItensReceitaCorrente()
        {
            ReceitaDispensacao receita = RetornaReceitaCorrente();

            if (receita.ItensPrescritos == null)
            {
                receita.ItensPrescritos = Factory.GetInstance<IReceitaDispensacao>().BuscarMedicamentos<ItemReceitaDispensacao>(RetornaReceitaCorrente().Codigo).ToList();
                Session["receita"] = receita;
            }
            return receita.ItensPrescritos;
        }

        private Dispensacao RetornaDispensacaoCorrente()
        {
            Dispensacao dispensacao = (Dispensacao)Session["dispensacao"];
            if (dispensacao == null)
            {
                dispensacao = new Dispensacao();
                dispensacao.Receita = RetornaReceitaCorrente();
                dispensacao.Farmacia = new ViverMais.Model.Farmacia();
                dispensacao.Farmacia.Codigo = RetornaCodigoFarmacia();
                dispensacao.DataAtendimento = DateTime.Now;
            }
            return dispensacao;
        }

        private void CadastraDispensacao(Dispensacao dispensacao)
        {            
            Factory.GetInstance<IDispensacao>().Salvar(dispensacao);
            Session["dispensacao"] = dispensacao;
        }

        protected ItemDispensacao CriaItemDispensacao()
        {
            ItemDispensacao item = new ItemDispensacao();
            item.LoteMedicamento = Factory.GetInstance<ILoteMedicamento>().BuscarPorCodigo<LoteMedicamento>(int.Parse(DropDownList_Medicamento.SelectedValue));
            item.QtdDias = int.Parse(TextBox_Dias.Text);
            item.QtdDispensada = int.Parse(TextBox_QuantidadeDispensada.Text);
            item.Observacao = tbxObservacao.Text;
            return item;
        }

        private void AtualizaGridDispensacao(IList<ItemDispensacao> iList)
        {
            GridView_MedicamentosDispensar.DataSource = iList;
            GridView_MedicamentosDispensar.DataBind();
        }

        protected void GridView_MedicamentosDispensar_DataBound(object sender, EventArgs e)
        {
            if (((GridView)sender).Rows.Count > 0)
                Panel_MedicamentosDispensados.Visible = true;
            else
                Panel_MedicamentosDispensados.Visible = false;
        }

        protected void GridView_MedicamentosDispensar_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView_MedicamentosDispensar.EditIndex = e.NewEditIndex;
            AtualizaGridDispensacao(RetornaDispensacaoCorrente().ItensDispensados);
        }

        protected void GridView_MedicamentosDispensar_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow r = GridView_MedicamentosDispensar.Rows[e.RowIndex];
            IList<ItemDispensacao> itens = RetornaDispensacaoCorrente().ItensDispensados;

            if (((TextBox)r.FindControl("TextBox_Qtd")).Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert",
                     "alert('Informe a quantidade dispensada!');", true);
                return;
            }

            if (((TextBox)r.FindControl("TextBox_Qtd")).Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert",
                     "alert('A quantidade dispensada deve ser diferente de 0 (Zero)!');", true);
                return;
            }

            if (((TextBox)r.FindControl("TextBox_QtdDi")).Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert",
                     "alert('Informe a quantidade de dias!');", true);
                return;
            }

            if (((TextBox)r.FindControl("TextBox_QtdDi")).Text == "0")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert",
                     "alert('A quantidade de dias deve ser diferente de 0 (Zero)!');", true);
                return;
            }
            int index = GridView_MedicamentosDispensar.PageSize * GridView_MedicamentosDispensar.PageIndex + e.RowIndex;

            Dispensacao dispensacao = RetornaDispensacaoCorrente();
            int quantidadeDispensadaAnterior = dispensacao.ItensDispensados[index].QtdDispensada;
            dispensacao.ItensDispensados[index].QtdDispensada = int.Parse(((TextBox)r.FindControl("TextBox_Qtd")).Text);
            dispensacao.ItensDispensados[index].QtdDias = int.Parse(((TextBox)r.FindControl("TextBox_QtdDi")).Text);
            dispensacao.ItensDispensados[index].Observacao = ((TextBox)r.FindControl("TextBox_Observacao")).Text;
            //Atualiza em banco ainda sem regras
            Factory.GetInstance<IDispensacao>().AlterarItemDispensacao<ItemDispensacao>(dispensacao.ItensDispensados[index], quantidadeDispensadaAnterior);

            GridView_MedicamentosDispensar.EditIndex = -1;
            AtualizaGridDispensacao(dispensacao.ItensDispensados);
            Session["dispensacao"] = dispensacao;
        }

        protected void GridView_MedicamentosDispensar_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_MedicamentosDispensar.EditIndex = -1;
            AtualizaGridDispensacao(RetornaDispensacaoCorrente().ItensDispensados);
        }

        protected void GridView_MedicamentosDispensar_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = GridView_MedicamentosDispensar.PageSize * GridView_MedicamentosDispensar.PageIndex + e.RowIndex;
            Dispensacao dispensacao = RetornaDispensacaoCorrente();
            //Atualiza em banco ainda sem regras
            Factory.GetInstance<IDispensacao>().DeletarItemDispensacao<ItemDispensacao>(dispensacao.ItensDispensados[index]);
            dispensacao.ItensDispensados.RemoveAt(index);
            Session["dispensacao"] = dispensacao;
            PreencherDropDownMedicamentos(RetornaCodigoFarmacia(), dispensacao.Receita);
            AtualizaDropDownListMedicamento(dispensacao.ItensDispensados);
            AtualizaGridDispensacao(dispensacao.ItensDispensados);
        }

        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            AtualizaGridDispensacao(RetornaDispensacaoCorrente().ItensDispensados);
            GridView_MedicamentosDispensar.PageIndex = e.NewPageIndex;
            GridView_MedicamentosDispensar.DataBind();
        }

        protected void OnClick_CancelarInclusao(object sender, EventArgs e)
        {
            LimparFormItemDispensado();
        }

        private void LimparFormItemDispensado()
        {
            TextBox_QuantidadePrescrita.Text = "";
            TxtSaldoMedicamento.Text = "";
            TextBox_QuantidadeDispensada.Text = "";
            TextBox_Dias.Text = "";
            tbxObservacao.Text = "";
        }

        private void AtualizaDropDownListMedicamento(List<ItemDispensacao> itensDispensados)
        {
            foreach (ItemDispensacao id in itensDispensados)
            {
                ListItem li = DropDownList_Medicamento.Items.FindByValue(id.LoteMedicamento.Codigo.ToString());
                if (li != null)
                    DropDownList_Medicamento.Items.Remove(li);
            }
        }

        /// <summary>
        /// Verifica se ainda está no mesmo dia para que possa ser realizada uma outrao dispensação.
        /// Dispensações podem ser realizadas no mesmo dia, para o mesmo medicamento em farmácias
        /// diferentes.
        /// </summary>
        /// <param name="dataUltimoAtendimento">Data do ultimo atendimento</param>
        /// <returns></returns>
        protected bool AindaPodeDispensar(DateTime dataUltimoAtendimento)
        {
            return dataUltimoAtendimento.Date == DateTime.Today;
        }

        protected void OnClick_IncluirMedicamento(object sender, EventArgs e)
        {
            Dispensacao dispensacao = RetornaDispensacaoCorrente();
            ItemDispensacao itemDispensacao = CriaItemDispensacao();
            itemDispensacao.Dispensacao = dispensacao;
            ReceitaDispensacao receita = RetornaReceitaCorrente();
            Dispensacao ultimaDispensacaoMedicamento = receita.UltimaDispensacaoMedicamento(itemDispensacao.LoteMedicamento.Medicamento.Codigo);

            if (ultimaDispensacaoMedicamento != null)
            {
                ItemDispensacao ultimoDispensado = ultimaDispensacaoMedicamento.ItensDispensados.Find(delegate(ItemDispensacao i) { return i.LoteMedicamento.Medicamento.Codigo == itemDispensacao.LoteMedicamento.Medicamento.Codigo; });
                //Se Tem ultima dispensação verifica a regra das 24 horas
                if (receita.Passou24HorasUltimaDispensacao(itemDispensacao.LoteMedicamento.Medicamento.Codigo)){
                    if (!receita.PodeDispensarNovamente(itemDispensacao))
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não é possível dispensar o medicamento " + itemDispensacao.Medicamento + " pois o tempo decorrido da sua ultima dispensação em " + ultimaDispensacaoMedicamento.DataAtendimento.ToString("dd/MM/yyyy") + " são " + ultimaDispensacaoMedicamento.DiasDecorridos().ToString() + " dias. Este medicamento pode ser dispensado apenas após " + receita.TempoEsperaNovaDispensacao(itemDispensacao.LoteMedicamento.Medicamento.Codigo).ToString() + " dias em " + ultimaDispensacaoMedicamento.DataAtendimento.AddDays(receita.TempoEsperaNovaDispensacao(itemDispensacao.LoteMedicamento.Medicamento.Codigo) + 1).ToString("dd/MM/yyyy") + ".')", true);
                        return;                        
                    }
                    if (receita.PodeDispensarComAutorizacao(itemDispensacao))
                    {
                        Panel_MensagemAutorizacao.Visible = true;
                        lblMensagemAutorizacao.Text = "FOI DISPENSADO PARA O PACIENTE, NO DIA <font color='Red'>" + ultimaDispensacaoMedicamento.DataAtendimento.ToString("dd/MM/yyyy") + "</font>"
                             + ", A QTD DE <font color='Red'>" + ultimoDispensado.QtdDispensada.ToString() + "</font> DESTE MEDICAMENTO"
                             + " NA FARMÁCIA: <font color='Red'>" + ultimaDispensacaoMedicamento.Farmacia.Nome + "</font>."
                             + "<br>O PACIENTE PODERÁ PEGAR ESSE MEDICAMENTO DEPOIS DO DIA <font color='Red'>" + ultimaDispensacaoMedicamento.DataAtendimento.AddDays(ultimoDispensado.QtdDias + 1).ToString("dd/MM/yyyy")
                             + "</font><br><br> DESEJA LIBERAR ESSE MEDICAMENTO PARA O PACIENTE?";
                        return;
                    }
                }
            }

            // Verifica se a quantidade solicitada é maior que a quantidade
            // que resta para ser dispensada
            if (itemDispensacao.isQuantidadeSolicitadaMaior(receita.SaldoRestanteMedicamento(itemDispensacao.LoteMedicamento.Medicamento.Codigo)))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Quantidade solicitada do medicamento " + itemDispensacao.Medicamento + " é maior que o saldo restante da quantidade prescrita.');", true);
                return;
            }

            //Verifica se possui estoque
            if (!PossuiEstoque())
                return;

            if (itemDispensacao.QtdDispensada > 300)
            {
                Panel_MensagemAutorizacao.Visible = true;
                lblMensagemAutorizacao.Text = "DESEJA REALMENTE DISPENSAR <font color='Red'>" + itemDispensacao.QtdDispensada.ToString()+ " </font>"
                     + "UNIDADES DO MEDICAMENTO <font color='Red'>" + itemDispensacao.LoteMedicamento.NomeMedicamento + "</font>?";                     
                return;
            }

            if (dispensacao.Codigo == 0)
            {
                CadastraDispensacao(dispensacao);
                dispensacao = RetornaDispensacaoCorrente();
            }                
            itemDispensacao.Dispensacao = dispensacao;
            Factory.GetInstance<IDispensacao>().SalvarItemDispensacao<ItemDispensacao>(itemDispensacao);
            dispensacao.ItensDispensados.Add(itemDispensacao);
            dispensacao.ItensDispensados.OrderBy(x => x.LoteMedicamento.Medicamento.Nome);
            Session["dispensacao"] = dispensacao;
            AtualizaDropDownListMedicamento(dispensacao.ItensDispensados);
            AtualizaGridDispensacao(dispensacao.ItensDispensados);
            LimparFormItemDispensado();
        }

        protected bool PossuiEstoque()
        {
            Estoque es = Factory.GetInstance<IEstoque>().BuscarItemEstoquePorFarmacia<Estoque>(RetornaCodigoFarmacia(), int.Parse(DropDownList_Medicamento.SelectedValue));
            if (es.QuantidadeEstoque >= int.Parse(TextBox_QuantidadeDispensada.Text))
                return true;
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Quantidade insuficiente no estoque para dispensação. Salto atual: " + es.QuantidadeEstoque + ". Quantidade solicitada: " + TextBox_QuantidadeDispensada.Text + ".');", true);
                return false;
            }
        }

        protected void OnClick_ConcluirDispensacao(object sender, EventArgs e)
        {
            Response.Redirect("FormGerarReceitaDispensacao.aspx");
        }

        protected void OnClick_CancelarDispensacao(object sender, EventArgs e)
        {
            Dispensacao dispensacao = RetornaDispensacaoCorrente();
            if (dispensacao != null)
            {
                foreach (ItemDispensacao item in dispensacao.ItensDispensados)
                {
                    Factory.GetInstance<IDispensacao>().DeletarItemDispensacao<ItemDispensacao>(item);
                }
                Factory.GetInstance<IDispensacao>().Deletar(dispensacao);
            }
            Response.Redirect("FormGerarReceitaDispensacao.aspx");
        }

        protected void OnClick_AutorizaDispensacao(object sender, EventArgs e)
        {
            if (tbxObservacao.Text.Trim() == string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Favor preencher o campo observação.');", true);
                return;
            }

            Dispensacao dispensacao = RetornaDispensacaoCorrente();
            ItemDispensacao itemDispensacao = CriaItemDispensacao();
            itemDispensacao.Dispensacao = dispensacao;
            itemDispensacao.Observacao = tbxObservacao.Text;
            ReceitaDispensacao receita = RetornaReceitaCorrente();

            //Verifica se possui estoque
            if (!PossuiEstoque())
                return;

            if (dispensacao.Codigo == 0)
            {
                CadastraDispensacao(dispensacao);
                dispensacao = RetornaDispensacaoCorrente();
            }
            itemDispensacao.Dispensacao = dispensacao;
            Factory.GetInstance<IDispensacao>().SalvarItemDispensacao<ItemDispensacao>(itemDispensacao);
            dispensacao.ItensDispensados.Add(itemDispensacao);
            dispensacao.ItensDispensados.OrderBy(x => x.LoteMedicamento.Medicamento.Nome);
            Session["dispensacao"] = dispensacao;
            AtualizaDropDownListMedicamento(dispensacao.ItensDispensados);
            AtualizaGridDispensacao(dispensacao.ItensDispensados);
            LimparFormItemDispensado();

            Panel_MensagemAutorizacao.Visible = false;

            //if (PossuiEstoque())
            //{
            //    Factory.GetInstance<IDispensacao>().SalvarItemDispensacao<ItemDispensacao>(CriaItemDispensacao(receita));
            //    AdicionaItemDispensar(CriaItemDispensacao(receita));
            //    AtualizaGridDispensacao(IgualaQuantidadePrescritaMedicamento(RetornaItensDispensar(), CriaItemDispensacao(receita)));
            //    AtualizaDropDownListMedicamento();
            //    OnClick_CancelarInclusao(sender, e);
            //    Panel_MensagemAutorizacao.Visible = false;
            //    Panel_MedicamentosDispensados.Visible = true;
            //}            
        }

        protected void OnClick_NaoAutorizaDispensacao(object sender, EventArgs e)
        {
            Panel_MensagemAutorizacao.Visible = false;
        }

        //==============================================================
        protected void OnClick_PesquisarMedicamento(object sender, EventArgs e)
        {
            IList<Estoque> les = null;// Factory.GetInstance<IEstoque>().BuscarPorNomeMedicamentoQuantidadeSuperior<Estoque>(TextBox_BuscarMedicamento.Text, DropDownList_Farmacia.Visible ? int.Parse(DropDownList_Farmacia.SelectedValue) : int.Parse(ViewState["co_farmacia"].ToString()));
            if (les.Count == 0)
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Nenhum medicamento com a descrição informada e que tenha quantidade disponível para dispensação foi encontrado na farmácia indicada.');", true);
            else
            {
                DropDownList_Medicamento.Items.Clear();
                DropDownList_Medicamento.Items.Add(new ListItem("Selecione...", "-1"));

                foreach (Estoque es in les)
                {
                    if (RetornaItensDispensar().Where(pt => pt.LoteMedicamento.Codigo == es.LoteMedicamento.Codigo).FirstOrDefault() == null)
                        DropDownList_Medicamento.Items.Add(new ListItem(es.LoteMedicamento.Medicamento.Nome + " - Lote: " + es.LoteMedicamento.Lote, es.LoteMedicamento.Codigo.ToString()));
                }
            }
        }



        //protected bool Calculo24Horas(DateTime dataUltimoAtendimento)
        //{
        //    DateTime dataHoraHoje = CalculaDataHoraAtendimento();
        //    TimeSpan diferenca = dataHoraHoje.Subtract(dataUltimoAtendimento);
        //    if (diferenca.TotalHours <= 24)
        //        return true;
        //    else
        //        return false;
        //}

        protected void BloqueiaInclusaoAlteracaoExclusao()
        {
            DateTime dataAtendimentoPesquisado = DateTime.Parse(Request.QueryString["dataAtendimento"].ToString());
            dataAtendimentoPesquisado = DateTime.Parse(dataAtendimentoPesquisado.ToString("dd/MM/yyyy"));
            DateTime dataUltimoAtendimento = Factory.GetInstance<IDispensacao>().BuscarDataAtendimentoRecente(long.Parse(ViewState["co_receita"].ToString()));
            dataUltimoAtendimento = DateTime.Parse(dataUltimoAtendimento.ToString("dd/MM/yyyy"));
            if (dataUltimoAtendimento > dataAtendimentoPesquisado)
            {
                GridView_MedicamentosDispensar.Columns[6].Visible = false;
                GridView_MedicamentosDispensar.Columns[7].Visible = false;
                Panel_ConteudoDispensacao.Visible = false;
            }
        }

        protected bool CalculoTetoReceita(long co_receita, int co_medicamento, ItemDispensacao item)
        {
            int qtdjadispensada = Factory.GetInstance<IDispensacao>().QuantidadeDispensadaMedicamentoReceita(long.Parse(ViewState["co_receita"].ToString()), item.LoteMedicamento.Medicamento.Codigo);
            int qtditensseremdispensados = RetornaItensDispensar().Where(p => p.LoteMedicamento.Medicamento.Codigo == item.LoteMedicamento.Medicamento.Codigo).Sum(p => p.QtdDispensada);

            //Corrigir quando utilizar o método
            //if (qtditensseremdispensados + qtdjadispensada + item.QtdDispensada > item.QtdPrescrita)
            if (false)
            {   //Corrigir quando utilizar o método
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, este medicamento não pode ser colocado na lista de itens a serem dispensados, pois a quantidade prescrita para o medicamento nesta receita chegou ao seu limite: " + item.QtdPrescrita + " unidades. Quantidade já dispensada = " + qtdjadispensada + ". Quantidade a ser dispensada = " + (qtditensseremdispensados + item.QtdDispensada) + ".');", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('CORRIGIR ALERT');", true);
                return true;
            }
            else
                return false;
        }

        //protected ItemDispensacao CriaItemDispensacao(ReceitaDispensacao receita)
        //{
        //    ItemDispensacao item = new ItemDispensacao();
        //    item.DataAtendimento = CalculaDataHoraAtendimento();
        //    item.Farmacia = Factory.GetInstance<IFarmacia>().BuscarPorCodigo<ViverMais.Model.Farmacia>(RetornaCodigoFarmacia());
        //    item.LoteMedicamento = Factory.GetInstance<ILoteMedicamento>().BuscarPorCodigo<LoteMedicamento>(int.Parse(DropDownList_Medicamento.SelectedValue));
        //    item.Receita = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ReceitaDispensacao>(long.Parse(ViewState["co_receita"].ToString()));
        //    item.QtdDias = int.Parse(TextBox_Dias.Text);
        //    item.QtdDispensada = int.Parse(TextBox_QuantidadeDispensada.Text);
        //    item.QtdPrescrita = int.Parse(TextBox_QuantidadePrescrita.Text);
        //    item.Receita = receita;
        //    item.Observacao = tbxObservacao.Text;
        //    return item;
        //}

        protected int CaculdaQuantidadeDias(int qtdDiasDispensada)
        {
            double calculo = (10 * qtdDiasDispensada) / 30.00;
            int periodo = int.Parse(Math.Round(calculo).ToString());
            return qtdDiasDispensada - periodo;
        }

        //protected bool VerificaEstoque()
        //{
        //    Estoque es = Factory.GetInstance<IEstoque>().BuscarItemEstoquePorFarmacia<Estoque>(RetornaCodigoFarmacia(), int.Parse(DropDownList_Medicamento.SelectedValue));
        //    if (es.QuantidadeEstoque >= int.Parse(TextBox_QuantidadeDispensada.Text))
        //        return true;
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Quantidade insuficiente no estoque para dispensação. Salto atual: " + es.QuantidadeEstoque + ". Quantidade solicitada: " + TextBox_QuantidadeDispensada.Text + ".');", true);
        //        return false;
        //    }
        //}


        protected DateTime CalculaDataHoraAtendimento()
        {
            string[] horas = DateTime.Now.ToString("HH:mm:ss").Split(':');
            string[] data;
            if (tbxDataAtendimento.Visible)
                data = tbxDataAtendimento.Text.Split('/');
            else
                data = Label_DataAtendimento.Text.Split('/');

            return new DateTime(int.Parse(data[2]), int.Parse(data[1]), int.Parse(data[0]), int.Parse(horas[0]), int.Parse(horas[1]), int.Parse(horas[2]));
        }

        //protected void OnClick_IncluirMedicamento(object sender, EventArgs e)
        //{
        //    ReceitaDispensacao receita = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ReceitaDispensacao>(long.Parse(ViewState["co_receita"].ToString()));
        //    long co_receita = receita.Codigo;
        //    LoteMedicamento lote = Factory.GetInstance<ILoteMedicamento>().BuscarPorCodigo<LoteMedicamento>(int.Parse(DropDownList_Medicamento.SelectedValue));
        //    DateTime dataAtendimentoRecente = Factory.GetInstance<IDispensacao>().BuscarDataAtendimentoRecente(receita.CodigoPaciente, lote.Medicamento.Codigo);
        //    ItemDispensacao item = CriaItemDispensacao(receita);

        //    if (Calculo24Horas(dataAtendimentoRecente))
        //    {
        //        if (VerificaEstoque())
        //        {
        //            if (!CalculoTetoReceita(co_receita, item.LoteMedicamento.Medicamento.Codigo, item))
        //            {
        //                Factory.GetInstance<IDispensacao>().SalvarItemDispensacao<ItemDispensacao>(item);
        //                AdicionaItemDispensar(CriaItemDispensacao(receita));
        //                AtualizaGridDispensacao(IgualaQuantidadePrescritaMedicamento(RetornaItensDispensar(), item));
        //                AtualizaDropDownListMedicamento();
        //                OnClick_CancelarInclusao(sender, e);
        //                Panel_MedicamentosDispensados.Visible = true;
        //            }
        //        }
        //        else
        //            VerificaEstoque();
        //    }
        //    else
        //    {
        //        object[] itemArray = Factory.GetInstance<IDispensacao>().BuscarUltimoAtendimento(receita.CodigoPaciente, lote.Medicamento.Codigo, dataAtendimentoRecente);
        //        if (itemArray != null)
        //        {
        //            if (!CalculoTetoReceita(co_receita, item.LoteMedicamento.Medicamento.Codigo, item))
        //            {
        //                DateTime dataCalculada = DateTime.Parse(dataAtendimentoRecente.AddDays(CaculdaQuantidadeDias(int.Parse(itemArray[1].ToString()))).ToString("dd/MM/yyyy"));
        //                DateTime dataAtendimento = DateTime.Parse(CalculaDataHoraAtendimento().ToString("dd/MM/yyyy"));
        //                if (dataCalculada >= dataAtendimento)
        //                {
        //                    if (int.Parse(itemArray[1].ToString()) >= 30)
        //                    {
        //                        DateTime dataLimiteAutorizacao = dataCalculada.AddDays(-5);
        //                        if ((dataAtendimento > dataLimiteAutorizacao) && (dataAtendimento <= dataCalculada))
        //                        {
        //                            //Autorizacao
        //                            Panel_MensagemAutorizacao.Visible = true;
        //                            lblMensagemAutorizacao.Text = "FOI DISPENSADO PARA O PACIENTE, NO DIA <font color='Red'>" + dataAtendimentoRecente.ToString("dd/MM/yyyy") + "</font>"
        //                                 + ", A QTD DE <font color='Red'>" + itemArray[0].ToString() + "</font> DESTE MEDICAMENTO"
        //                                 + " NA FARMÁCIA: <font color='Red'>" + itemArray[2].ToString() + "</font>."
        //                                 + "<br> O PACIENTE PODERÁ PEGAR ESSE MEDICAMENTO DEPOIS DO DIA <font color='Red'>" + dataCalculada.AddDays(1).ToString("dd/MM/yyyy")
        //                                 + "</font><br><br> DESEJA LIBERAR ESSE MEDICAMENTO PARA O PACIENTE?";
        //                            return;
        //                        }
        //                        else
        //                        {
        //                            //Bloqueia Dispensação
        //                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('FOI DISPENSADO PARA O PACIENTE, NO DIA " + dataAtendimentoRecente.ToString("dd/MM/yyyy") + ", A QTD DE " + itemArray[0].ToString() + " DESTE MEDICAMENTO NA FARMÁCIA: " + itemArray[2].ToString() + ". ESTE MEDICAMENTO SÓ PODERÁ SER LIBERADO PARA O PACIENTE DEPOIS DO DIA " + dataCalculada.AddDays(1).ToString("dd/MM/yyyy") + ".');", true);
        //                            return;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        //Bloqueia Dispensação
        //                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('FOI DISPENSADO PARA O PACIENTE, NO DIA " + dataAtendimentoRecente.ToString("dd/MM/yyyy") + ", A QTD DE " + itemArray[0].ToString() + " DESTE MEDICAMENTO NA FARMÁCIA: " + itemArray[2].ToString() + ". ESTE MEDICAMENTO SÓ PODERÁ SER LIBERADO PARA O PACIENTE DEPOIS DO DIA " + dataCalculada.AddDays(1).ToString("dd/MM/yyyy") + ".');", true);
        //                        return;
        //                    }
        //                }
        //            }
        //        }
        //        if (VerificaEstoque())
        //        {
        //            if (!CalculoTetoReceita(co_receita, item.LoteMedicamento.Medicamento.Codigo, item))
        //            {
        //                Factory.GetInstance<IDispensacao>().SalvarItemDispensacao<ItemDispensacao>(item);
        //                AdicionaItemDispensar(item);
        //                AtualizaGridDispensacao(IgualaQuantidadePrescritaMedicamento(RetornaItensDispensar(), item));
        //                AtualizaDropDownListMedicamento();
        //                OnClick_CancelarInclusao(sender, e);
        //                Panel_MedicamentosDispensados.Visible = true;
        //            }
        //        }
        //        else
        //            VerificaEstoque();
        //    }
        //}

        //private void AtualizaDropDownListMedicamento()
        //{
        //    foreach (ItemDispensacao id in RetornaItensDispensar())
        //    {
        //        ListItem li = DropDownList_Medicamento.Items.FindByValue(id.LoteMedicamento.Codigo.ToString());
        //        if (li != null)
        //            DropDownList_Medicamento.Items.Remove(li);
        //    }
        //}

        //private void AtualizaGridDispensacao(IList<ItemDispensacao> iList)
        //{
        //    GridView_MedicamentosDispensar.DataSource = iList;
        //    GridView_MedicamentosDispensar.DataBind();
        //}

        //protected void OnClick_CancelarInclusao(object sender, EventArgs e)
        //{
        //    TextBox_QuantidadePrescrita.Enabled = true;
        //    DropDownList_Medicamento.SelectedValue = "-1";
        //    TextBox_QuantidadePrescrita.Text = "";
        //    TextBox_QuantidadeDispensada.Text = "";
        //    TextBox_Dias.Text = "";
        //    tbxObservacao.Text = "";
        //}



        private IList<ItemDispensacao> RetornaItensDispensar()
        {
            return Session["itensdispensar"] == null ? new List<ItemDispensacao>() : (List<ItemDispensacao>)Session["itensdispensar"];
        }

        private void AdicionaItemDispensar(ItemDispensacao id)
        {
            IList<ItemDispensacao> lid = RetornaItensDispensar();
            lid.Add(id);
            Session["itensdispensar"] = lid;
        }

        protected void OnRowCommand_VerificarAcao(object sender, GridViewCommandEventArgs e)
        {
            int indexlote = int.Parse(e.CommandArgument.ToString()) == 0 ? GridView_MedicamentosDispensar.PageIndex * GridView_MedicamentosDispensar.PageSize : (GridView_MedicamentosDispensar.PageIndex * GridView_MedicamentosDispensar.PageSize) + int.Parse(e.CommandArgument.ToString());
            IList<ItemDispensacao> ltemp = RetornaItensDispensar();

            if (e.CommandName == "CommandName_Excluir")
            {
                //Corrigir quando usar metodo
                //ItemDispensacao item = Factory.GetInstance<IDispensacao>().BuscarPorItem<ItemDispensacao>(long.Parse(ViewState["co_receita"].ToString()), ltemp[indexlote].LoteMedicamento.Codigo, ltemp[indexlote].DataAtendimento);
                ItemDispensacao item = null;
                Factory.GetInstance<IDispensacao>().DeletarItemDispensacao<ItemDispensacao>(item);
                ltemp.RemoveAt(indexlote);
                Session["itensdispensar"] = ltemp;
                AtualizaGridDispensacao(RetornaItensDispensar());
            }

        }

        //protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        //{
        //    AtualizaGridDispensacao(RetornaItensDispensar());
        //    GridView_MedicamentosDispensar.PageIndex = e.NewPageIndex;
        //    GridView_MedicamentosDispensar.DataBind();
        //}



        private IList<ItemDispensacao> IgualaQuantidadePrescritaMedicamento(IList<ItemDispensacao> lid, ItemDispensacao ultimoitemalterado)
        {

            //CORRIGUR AO USAR O METODO
            //foreach (ItemDispensacao temp in lid)
            //{
            //    if (temp.LoteMedicamento.Medicamento.Codigo == ultimoitemalterado.LoteMedicamento.Medicamento.Codigo)
            //        temp.QtdPrescrita = ultimoitemalterado.QtdPrescrita;
            //}
            return lid;
        }

        //protected void GridView_MedicamentosDispensar_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    GridView_MedicamentosDispensar.EditIndex = e.NewEditIndex;
        //    IList<ItemDispensacao> itens;
        //    itens = RetornaItensDispensar();
        //    AtualizaGridDispensacao(itens);
        //}

        //protected void GridView_MedicamentosDispensar_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    GridViewRow r = GridView_MedicamentosDispensar.Rows[e.RowIndex];
        //    IList<ItemDispensacao> itens;
        //    itens = RetornaItensDispensar();

        //    if (((TextBox)r.FindControl("TextBox_Qtd")).Text == "")
        //    {
        //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert",
        //             "alert('Informe a quantidade dispensada!');", true);
        //        return;
        //    }

        //    if (((TextBox)r.FindControl("TextBox_Qtd")).Text == "0")
        //    {
        //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert",
        //             "alert('A quantidade dispensada deve ser diferente de 0 (Zero)!');", true);
        //        return;
        //    }

        //    if (((TextBox)r.FindControl("TextBox_QtdDi")).Text == "")
        //    {
        //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert",
        //             "alert('Informe a quantidade de dias!');", true);
        //        return;
        //    }

        //    if (((TextBox)r.FindControl("TextBox_QtdDi")).Text == "0")
        //    {
        //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert",
        //             "alert('A quantidade de dias deve ser diferente de 0 (Zero)!');", true);
        //        return;
        //    }

        //    //CORRIGUR AO USAR O METODO
        //    //Estoque estoque = Factory.GetInstance<IEstoque>().BuscarItemEstoquePorFarmacia<Estoque>(itens[r.RowIndex].Farmacia.Codigo, itens[r.RowIndex].LoteMedicamento.Codigo);
        //    Estoque estoque = null;

        //    int qtdEstoque = estoque.QuantidadeEstoque + itens[r.RowIndex].QtdDispensada;

        //    if (qtdEstoque >= int.Parse(((TextBox)r.FindControl("TextBox_Qtd")).Text))
        //    {
        //        //CORRIGUR AO USAR O METODO
        //        //ItemDispensacao item = Factory.GetInstance<IDispensacao>().BuscarPorItem<ItemDispensacao>(long.Parse(ViewState["co_receita"].ToString()), itens[r.RowIndex].LoteMedicamento.Codigo, itens[r.RowIndex].DataAtendimento);
        //        ItemDispensacao item = null;
        //        int qtdAnterior = item.QtdDispensada;
        //        int qtdJaDispensada = Factory.GetInstance<IDispensacao>().QuantidadeDispensadaMedicamentoReceita(long.Parse(ViewState["co_receita"].ToString()), item.LoteMedicamento.Medicamento.Codigo);
        //        item.QtdDispensada = int.Parse(((TextBox)r.FindControl("TextBox_Qtd")).Text);
        //        item.QtdDias = int.Parse(((TextBox)r.FindControl("TextBox_QtdDi")).Text);

        //        //int qtdItensSeremDispensados = RetornaItensDispensar().Where(p => p.LoteMedicamento.Medicamento.Codigo == item.LoteMedicamento.Medicamento.Codigo).Sum(p => p.QtdDispensada);

        //        //CORRIGUR AO USAR O METODO
        //        //if ((qtdJaDispensada - qtdAnterior) + item.QtdDispensada > item.QtdPrescrita)
        //        if(true)
        //        {
        //            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, este medicamento não pode ser colocado na lista de itens a serem dispensados, pois a quantidade prescrita para o medicamento nesta receita chegou ao seu limite: " + item.QtdPrescrita + " unidades. Quantidade já dispensada = " + (qtdJaDispensada - qtdAnterior) + " + " + " Quantidade a ser dispensada = " + item.QtdDispensada + ". Total = " + ((qtdJaDispensada - qtdAnterior)+ item.QtdDispensada) + ".');", true);
        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('CORRIGIR NO METODO');", true);
        //            return;
        //        }
        //        else
        //        {
        //            Factory.GetInstance<IDispensacao>().AlterarItemDispensacao<ItemDispensacao>(item,qtdAnterior);
        //            //AtualizaGridDispensacao(IgualaQuantidadePrescritaMedicamento(RetornaItensDispensar(), item));
        //            //AtualizaDropDownListMedicamento();
        //            //OnClick_CancelarInclusao(sender, e);
        //            itens[r.RowIndex].QtdDispensada = int.Parse(((TextBox)r.FindControl("TextBox_Qtd")).Text);
        //            itens[r.RowIndex].QtdDias = int.Parse(((TextBox)r.FindControl("TextBox_QtdDi")).Text);
        //            GridView_MedicamentosDispensar.EditIndex = -1;
        //            AtualizaGridDispensacao(itens);
        //            Session["itensdispensar"] = itens;
        //        }
        //    }
        //    else
        //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Quantidade insuficiente no estoque para dispensação. Salto atual: " + estoque.QuantidadeEstoque + ". Quantidade solicitada: " + TextBox_QuantidadeDispensada.Text + ".');", true);
        //}

        //protected void GridView_MedicamentosDispensar_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    GridView_MedicamentosDispensar.EditIndex = -1;
        //    IList<ItemDispensacao> itens;
        //    itens = RetornaItensDispensar();
        //    AtualizaGridDispensacao(itens);
        //}

        //protected void OnClick_AutorizaDispensacao(object sender, EventArgs e)
        //{
        //    //CORRIGIR AO USAR
        //    //ReceitaDispensacao receita = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ReceitaDispensacao>(long.Parse(ViewState["co_receita"].ToString()));
        //    //long co_receita = receita.Codigo;            
        //    //if (VerificaEstoque())
        //    //{
        //    //    Factory.GetInstance<IDispensacao>().SalvarItemDispensacao<ItemDispensacao>(CriaItemDispensacao(receita));
        //    //    AdicionaItemDispensar(CriaItemDispensacao(receita));
        //    //    AtualizaGridDispensacao(IgualaQuantidadePrescritaMedicamento(RetornaItensDispensar(), CriaItemDispensacao(receita)));
        //    //    AtualizaDropDownListMedicamento();
        //    //    OnClick_CancelarInclusao(sender, e);
        //    //    Panel_MensagemAutorizacao.Visible = false;
        //    //    Panel_MedicamentosDispensados.Visible = true;
        //    //}
        //    //else
        //    //    VerificaEstoque();
        //}

        //protected void OnClick_NaoAutorizaDispensacao(object sender, EventArgs e)
        //{
        //    Panel_MensagemAutorizacao.Visible = false;
        //}
    }
}