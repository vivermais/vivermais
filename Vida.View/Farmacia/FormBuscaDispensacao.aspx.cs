﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Dispensacao;
using ViverMais.ServiceFacade.ServiceFacades;
using System.Data;
using ViverMais.View.Agendamento.Helpers;

namespace ViverMais.View.Farmacia
{
    public partial class FormBuscaDispensacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Pesquisa as dispensações de acordo com a receita informada ou a partir do número do cartão SUS do paciente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_PesquisarDispensacao(object sender, EventArgs e) 
        {
            if (CustomValidator_PesquisaBusca.IsValid)
            {
                if (rblPesquisaDispensacao.Items[1].Selected)//por Paciente
                {
                    if (!string.IsNullOrEmpty(tbxCartaoSUS.Text))
                    {
                        Regex validaCartaoSUS = new Regex(@"^\d{15}$");
                        if (validaCartaoSUS.IsMatch(tbxCartaoSUS.Text))
                        {
                            ViewState.Remove("nomepaciente");
                            ViewState.Remove("nomemae");
                            ViewState.Remove("datanascimento");
                            ViewState["cartaosus"] = tbxCartaoSUS.Text;
                            CarregaGridViewReceitas(0,"",tbxCartaoSUS.Text);
                        }
                        else
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O cartão SUS deve conter 15 dígitos!');", true);
                    }
                    else
                    {
                        char[] del = { ' ' };
                        if (!string.IsNullOrEmpty(tbxNomePaciente.Text) && tbxNomePaciente.Text.Split(del).Length >= 2 && ((!string.IsNullOrEmpty(tbxNomeMae.Text) && tbxNomeMae.Text.Split(del).Length >= 2) || !string.IsNullOrEmpty(tbxDataNascimento.Text)))
                        {
                            ViewState.Remove("cartaosus");
                            ViewState["nomepaciente"] = tbxNomePaciente.Text;
                            ViewState["nomemae"] = tbxNomeMae.Text;
                            ViewState["datanascimento"] = tbxDataNascimento.Text;
                            CarregaGridViewPacientes(tbxNomePaciente.Text, tbxNomeMae.Text, tbxDataNascimento.Text);
                        }
                        else
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Informe um dos seguintes campos: \\n (1) Número do cartão SUS. \\n (2) Nome e Sobrenome do Paciente e Nome e Sobrenome da Mãe. \\n (3) Nome e Sobrenome do Paciente e Data de Nascimento.');", true);
                    }
                }
                else //por Receita
                {
                    if (!string.IsNullOrEmpty(tbxReceita.Text))
                        CarregaGridViewReceitas(long.Parse(tbxReceita.Text),"","");
                }
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + CustomValidator_PesquisaBusca.ErrorMessage + "');", true);
        }


        /// <summary>
        /// Carrega o gridview de pacientes para o usuário
        /// </summary>
        /// <param name="nomepaciente">nome do paciente</param>
        /// <param name="nomemae">nome da mãe</param>
        /// <param name="datanascimento">data de nascimento do paciente</param>
        private void CarregaGridViewPacientes(string nomepaciente, string nomemae, string datanascimento)
        {
            IList<ViverMais.Model.Paciente> lp = new List<ViverMais.Model.Paciente>();
            lp = Factory.GetInstance<IPaciente>().PesquisarPaciente<ViverMais.Model.Paciente>(nomepaciente, !string.IsNullOrEmpty(nomemae) ? nomemae : "", !string.IsNullOrEmpty(datanascimento) ? DateTime.Parse(datanascimento) : DateTime.MinValue);

            GridView_ResultadoPesquisaPaciente.DataSource = lp;
            GridView_ResultadoPesquisaPaciente.DataBind();

            PanelResultadoPaciente.Visible = true;
        }

        /// <summary>
        /// Carrega o gridview de receitas do paciente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarregaGridViewReceitas(long co_receita, string codigoPaciente, string cartaoSUS)
        {
            IList<ReceitaDispensacao> receitas = new List<ReceitaDispensacao>();
            ViverMais.Model.Paciente pac = null;
            if (codigoPaciente != "")           
                receitas = Factory.GetInstance<IReceitaDispensacao>().BuscarReceitaPorPaciente<ReceitaDispensacao>(codigoPaciente);
            else
            {
                if (cartaoSUS != "")
                {
                    pac = Factory.GetInstance<IPaciente>().PesquisarPacientePorCNS<ViverMais.Model.Paciente>(cartaoSUS);
                    receitas = Factory.GetInstance<IReceitaDispensacao>().BuscarReceitaPorPaciente<ReceitaDispensacao>(pac.Codigo);
                }
                else
                {
                    ReceitaDispensacao receita = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ReceitaDispensacao>(co_receita);
                    if (receita != null)
                        receitas.Add(receita);
                }
            }

            if ((receitas.Count != 0) && (cartaoSUS == ""))
            {
                ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(receitas[0].CodigoPaciente);
                lblNome.Text = paciente.Nome;
                lblNomeMae.Text = paciente.NomeMae;
                lblNascimento.Text = paciente.DataNascimento.ToString("dd/MM/yyyy");
            }
            else if(pac!=null)
            {
                lblNome.Text = pac.Nome;
                lblNomeMae.Text = pac.NomeMae;
                lblNascimento.Text = pac.DataNascimento.ToString("dd/MM/yyyy");
            }

            gridView_ResultadoReceitas.DataSource = receitas;
            gridView_ResultadoReceitas.DataBind();

            PanelResultado.Visible = true;
            PanelResultado.Focus();
        }

        private void CarregaGridviewMedicamentos(ReceitaDispensacao receita)
        {
            List<ItemReceitaDispensacao> medicamentos = Factory.GetInstance<IReceitaDispensacao>().BuscarMedicamentos<ItemReceitaDispensacao>(receita.Codigo).OrderBy(x => x.NomeMedicamento).ToList(); ;
            GridView_Medicamentos.DataSource = medicamentos;
            GridView_Medicamentos.DataBind();
        }

        /// <summary>
        /// Caso a receita esteja com o tempo limite de 6 meses a informação é bloqueada, sendo aprovada somente com a permissão do usuário
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Liberar_Busca(object sender, EventArgs e) 
        {
        }

        /// <summary>
        /// Cancela a aprovação de liberação de medicamentos para receita com o tempo de validade até 6 meses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Cancelar_Busca(object sender, EventArgs e) 
        {
        }

        /// <summary>
        /// Redireciona para a página de atendimentos relaizados com o número desta receita
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Pesquisar_Atendimento(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Paginação no gridview dos resultados encontrados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCommand_VerificarAcao(object sender, GridViewCommandEventArgs e)
        {
        }

        /// <summary>
        /// Valida se a receita/cartão SUS foram informados com os formatos corretos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnServerValidate_PesquisaDispensacao(object sender, ServerValidateEventArgs e) 
        {
            e.IsValid = true;

            if (string.IsNullOrEmpty(tbxReceita.Text) && string.IsNullOrEmpty(tbxCartaoSUS.Text))
            {
                e.IsValid = false;
                CustomValidator_PesquisaBusca.ErrorMessage = "Informe um número da receita ou número do cartão SUS!";
            }
            else
            {
                Regex regex = null;
                if (!string.IsNullOrEmpty(tbxReceita.Text))
                {
                    ViewState["pesquisa_dispensacao"] = tbxReceita.Text;
                    regex = new Regex(@"^\d*$");
                    if (!regex.IsMatch(tbxReceita.Text))
                    {
                        e.IsValid = false;
                        CustomValidator_PesquisaBusca.ErrorMessage = "Digite somente números na receita!";
                    }
                }
                else 
                {
                    ViewState["pesquisa_dispensacao"] = tbxCartaoSUS.Text;
                    regex = new Regex(@"^\d{15}$");
                    if (!regex.IsMatch(tbxReceita.Text))
                    {
                        e.IsValid = false;
                        CustomValidator_PesquisaBusca.ErrorMessage = "O número do cartão SUS deve conter 15 dígitos!";
                    }
                }
            }
        }

        protected void rblPesquisaDispensacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblPesquisaDispensacao.Items[0].Selected)
            {
                panelReceita.Visible = true;
                panelPaciente.Visible = false;
            }
            else
            {
                panelPaciente.Visible = true;
                panelReceita.Visible = false;
            }
        }

        /// <summary>
        /// Seleciona o Paciente para listar as receitas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_SelecionarPaciente(object sender, EventArgs e)
        {
            string i_index = ((LinkButton)sender).CommandArgument;
            int index;
            if (int.Parse(i_index) == 0)
                index = GridView_ResultadoPesquisaPaciente.PageIndex * GridView_ResultadoPesquisaPaciente.PageSize;
            else
                index = (GridView_ResultadoPesquisaPaciente.PageIndex * GridView_ResultadoPesquisaPaciente.PageSize) + int.Parse(i_index);

            GridViewRow row = GridView_ResultadoPesquisaPaciente.Rows[index];
            string codigoPaciente = Server.HtmlDecode(row.Cells[0].Text);
            GridView_ResultadoPesquisaPaciente.SelectedIndex = index;
            CarregaGridViewReceitas(0, codigoPaciente, "");

            //IList<ReceitaDispensacao> receitas = new List<ReceitaDispensacao>();
            //receitas = Factory.GetInstance<IReceitaDispensacao>().BuscarReceitaPorPaciente<ReceitaDispensacao>(codigoPaciente);
        }

        /// <summary>
        /// Seleciona a Receita para listar todos os atendimentos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridView_ResultadoReceitas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandName != "Page")
            {
                //int index = int.Parse(e.CommandArgument.ToString()) == 0 ? gridView_ResultadoReceitas.PageIndex * gridView_ResultadoReceitas.PageSize : (gridView_ResultadoReceitas.PageIndex * gridView_ResultadoReceitas.PageSize) + int.Parse(e.CommandArgument.ToString());
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = gridView_ResultadoReceitas.Rows[index];
                long co_receita = long.Parse(Server.HtmlDecode(row.Cells[0].Text));
                ReceitaDispensacao receita = null;

                //Depois melhorar pois não precisa ficar inso no banco para pegar isto aqui
                switch (e.CommandName)
                {
                    case "VisualizarAtendimentos":
                        GridViewAtendimentos.DataSource = Factory.GetInstance<IDispensacao>().BuscarDispensacaoPorReceita<Dispensacao>(co_receita).OrderBy(x => x.DataAtendimento).ToList();
                        GridViewAtendimentos.DataBind();
                        gridView_ResultadoReceitas.SelectedIndex = index;
                        PanelAtendimento.Visible = true;
                        if (PanelMedicamentos.Visible == true)
                        {
                            receita = new ReceitaDispensacao();
                            receita.Codigo = co_receita;
                            CarregaGridviewMedicamentos(receita);                            
                        }
                        break;
                    case "VisualizarMedicamentos":
                        receita = new ReceitaDispensacao();
                        receita.Codigo = co_receita;
                        CarregaGridviewMedicamentos(receita);
                        if (PanelMedicamentos.Visible == true)
                        {
                            GridViewAtendimentos.DataSource = Factory.GetInstance<IDispensacao>().BuscarDispensacaoPorReceita<Dispensacao>(co_receita).OrderBy(x => x.DataAtendimento).ToList();
                            GridViewAtendimentos.DataBind();
                            gridView_ResultadoReceitas.SelectedIndex = index;
                        }
                        PanelMedicamentos.Visible = true;
                        break;
                    case "NovoAtendimento":
                        Response.Redirect("FormDispensarMedicamentos.aspx?co_receita=" + co_receita);
                        break;
                }
            }
        }

        /// <summary>
        /// Seleciona o Atendimento e direciona para a tela da dispensação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewAtendimentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = int.Parse(e.CommandArgument.ToString()) == 0 ? GridViewAtendimentos.PageIndex * GridViewAtendimentos.PageSize : (GridViewAtendimentos.PageIndex * GridViewAtendimentos.PageSize) + int.Parse(e.CommandArgument.ToString());
            GridViewRow row = GridViewAtendimentos.Rows[index];
            long co_receita = long.Parse(Server.HtmlDecode(row.Cells[0].Text));

            if (e.CommandName == "DirecionarDispensacao")
            {
                int co_farmacia = int.Parse(Server.HtmlDecode(row.Cells[3].Text));
                DateTime dataAtendimento = DateTime.Parse(Server.HtmlDecode(row.Cells[2].Text));
                Response.Redirect("FormDispensarMedicamentos.aspx?co_receita=" + co_receita + "&co_farmacia=" + co_farmacia + "&dataAtendimento=" + dataAtendimento.ToString("dd-MM-yyyy"));
            }
        }

        protected void OnPageIndexChanging_PaginacaoPaciente(object sender, GridViewPageEventArgs e)
        {
            CarregaGridViewPacientes(ViewState["nomepaciente"] != null ? ViewState["nomepaciente"].ToString() : "", ViewState["nomemae"] != null ? ViewState["nomemae"].ToString() : "", ViewState["datanascimento"] != null ? ViewState["datanascimento"].ToString() : "");
            GridView_ResultadoPesquisaPaciente.PageIndex = e.NewPageIndex;
            GridView_ResultadoPesquisaPaciente.DataBind();
        }

        protected void GridViewAtendimentos_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            long co_receita = long.Parse(gridView_ResultadoReceitas.SelectedRow.Cells[0].Text);
            GridViewAtendimentos.DataSource = Factory.GetInstance<IDispensacao>().BuscarDispensacaoPorReceita<Dispensacao>(co_receita).OrderBy(x => x.DataAtendimento).ToList();
            GridViewAtendimentos.PageIndex = e.NewPageIndex;
            GridViewAtendimentos.DataBind();            
        }

        protected void gridView_ResultadoReceitas_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            //GridViewRow row = GridView_ResultadoPesquisaPaciente.Rows[index];
            //string codigoPaciente = Server.HtmlDecode(row.Cells[0].Text);
            gridView_ResultadoReceitas.PageIndex = e.NewPageIndex;
            CarregaGridViewReceitas(0, GridView_ResultadoPesquisaPaciente.SelectedValue.ToString(), "");            
            
        }

        protected void GridViewAtendimentos_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {                
                int index = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "VisualizarMedicamentos":                        
                        GridViewMedicamentosDispensados.DataSource = Factory.GetInstance<IDispensacao>().BuscarItensDispensacao<ItemDispensacao>(int.Parse(((GridView)sender).DataKeys[index].Value.ToString())).ToList();
                        GridViewMedicamentosDispensados.DataBind();                        
                        break;
                }
            }

        }

        protected void GridViewAtendimentos_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow linha in GridViewAtendimentos.Rows)
            {
                if (GridViewAtendimentos.Rows.Count > 1 && !linha.Equals(GridViewAtendimentos.Rows[GridViewAtendimentos.Rows.Count - 1]))
                {
                    ((HyperLink)(linha.Cells[0].Controls[0])).Enabled = false;

                }
            }
        }
    }
}
