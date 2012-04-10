using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Vida.Model;
using Vida.ServiceFacade.ServiceFacades.Farmacia.Dispensacao;
using Vida.DAO;
using Vida.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;
using Vida.ServiceFacade.ServiceFacades.Farmacia.Misc;

namespace Vida.View.Farmacia
{
    public partial class FormItensAtendimentoDispensacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime dataTemp;
                int intTemp;

                if (Request["data"] != null && DateTime.TryParse(Request["data"].ToString(), out dataTemp)
                    && Request["receita"] != null && int.TryParse(Request["receita"].ToString(), out intTemp)
                    && Request["farmacia"] != null && int.TryParse(Request["farmacia"].ToString(), out intTemp))
                    CarregaItensDispensacao(DateTime.Parse(Request["data"].ToString()), int.Parse(Request["receita"].ToString()), int.Parse(Request["farmacia"].ToString()));
            }
        }

        /// <summary>
        /// Carrega os medicamentos dispensados com a data, dispensação e farmácia informada.
        /// </summary>
        /// <param name="data">data de atendimento</param>
        /// <param name="receita">código da receita</param>
        /// <param name="farmacia">código da farmácia</param>
        private void CarregaItensDispensacao(DateTime data, int receita, int farmacia)
        {
            //ItensDispensacaoBsn dispensacaoBsn = new ItensDispensacaoBsn();
            IDispensacao idispensacao = Factory.GetInstance<IDispensacao>();
            IList<ItemDispensacao> lista = idispensacao.BuscarItensPorAtendimento<ItemDispensacao>(data, receita, farmacia);//dispensacaoBsn.BuscarPorAtendimento(data, receita, farmacia);
            GridView1.DataSource = lista;
            GridView1.DataBind();
        }

        /// <summary>
        /// Verifica se a ação que está sendo realizada é de alteração para o medicamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void verificar_acao(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "c_alterar")
            {
                IDispensacao idispensacao = Factory.GetInstance<IDispensacao>();

                LimparCampos();

                table_Medicamento.Visible = true;
                table_Botoes_Atualizar.Visible = true;

                int codigo_lote = int.Parse(GridView1.DataKeys[int.Parse(e.CommandArgument.ToString())]["CodigoLoteMedicamento"].ToString());
                int dispensacao = int.Parse(Request["receita"].ToString());

                DateTime data = DateTime.Parse(Request["data"].ToString());
                //ItensDispensacaoBsn itensDispensacaoBsn = new ItensDispensacaoBsn();
                ItemDispensacao itensDispensacao = idispensacao.BuscarPorItem<ItemDispensacao>(dispensacao, codigo_lote, data);//itensDispensacaoBsn.BuscarPorItem(dispensacao, codigo_lote, data);

                lbNomeMedicamento.Text = itensDispensacao.LoteMedicamento.Medicamento.Nome;
                tbxQuantidadeDias.Text = itensDispensacao.QtdDias.ToString();
                tbxQuantidadePrescrita.Text = itensDispensacao.QtdPrescrita.ToString();
                tbxQuantidadeDispensada.Text = itensDispensacao.QtdDispensada.ToString();

                if (!string.IsNullOrEmpty(itensDispensacao.Observacao))
                {
                    linha_label_observacao.Visible = true;
                    linha_textbox_observacao.Visible = true;
                    tbxObservacao.Text = itensDispensacao.Observacao;
                }

                ViewState["lote_escolhido"] = codigo_lote.ToString();
            }
        }

        /// <summary>
        /// Exclui o medicamento escolhido e dá baixa no estoque para este lote
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void e_excluir_medicamento(object sender, EventArgs e)
        {
            IDispensacao idispensacao = Factory.GetInstance<IDispensacao>();
            LinkButton lb = (LinkButton)sender;
            //ItensDispensacaoBsn itensDispensacaoBsn = new ItensDispensacaoBsn();
            ItemDispensacao itensDispensacao = idispensacao.BuscarPorItem<ItemDispensacao>(int.Parse(Request["receita"]), int.Parse(lb.CommandArgument), DateTime.Parse(Request["data"]));//itensDispensacaoBsn.BuscarPorItem(int.Parse(Request["receita"].ToString()), int.Parse(lb.CommandArgument.ToString()), DateTime.Parse(Request["data"].ToString()));

            try
            {
                //EstoqueBsn estoqueBsn = new EstoqueBsn();
                IEstoque iestoque = Factory.GetInstance<IEstoque>();
                Estoque estoque = iestoque.BuscarItemEstoquePorFarmacia<Estoque>(int.Parse(Request["farmacia"]), itensDispensacao.LoteMedicamento.Codigo);//estoqueBsn.buscarPorFarmaciaElote(int.Parse(Request["farmacia"].ToString()), itensDispensacao.LoteMedicamento.Codigo); //Atualiza o estoque
                DarBaixaEstoque(estoque, 0, itensDispensacao.QtdDispensada, itensDispensacao, "remover");

                LimparCampos();
                CarregaItensDispensacao(DateTime.Parse(Request["data"].ToString()), int.Parse(Request["receita"].ToString()), int.Parse(Request["farmacia"].ToString()));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Medicamento excluído com sucesso!');", true);
            }
            catch (Exception f)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O medicamento não pôde ser excluído no momento. Por favor, tente mais tarde.');", true);
            }
        }

        /// <summary>
        /// Função que salva a alteração sobre o medicamento.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Salvar_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            //ItensDispensacaoBsn itensDipensacaoBsn = new ItensDispensacaoBsn();
            IDispensacao idispensacao = Factory.GetInstance<IDispensacao>();
            ItemDispensacao itensDispensacao = idispensacao.BuscarPorItem<ItemDispensacao>(int.Parse(Request["receita"]), int.Parse(ViewState["lote_escolhido"].ToString()), DateTime.Parse(Request["data"]));//itensDipensacaoBsn.BuscarPorItem(int.Parse(Request["receita"].ToString()), int.Parse(ViewState["lote_escolhido"].ToString()), DateTime.Parse(Request["data"].ToString()));

            int qtd_anterior = itensDispensacao.QtdDispensada;

            itensDispensacao.QtdDias = int.Parse(tbxQuantidadeDias.Text);
            itensDispensacao.QtdDispensada = int.Parse(tbxQuantidadeDispensada.Text);
            itensDispensacao.QtdPrescrita = int.Parse(tbxQuantidadePrescrita.Text);

            if (linha_textbox_observacao.Visible && !string.IsNullOrEmpty(tbxObservacao.Text))
                itensDispensacao.Observacao = tbxObservacao.Text;

            if (bt.CommandArgument.ToString() == "c_inserir")
            {
                if (int.Parse(tbxQuantidadeDispensada.Text) >= 300) //Bloqueia o UpdatePanel para confirmar a alteração
                {
                    Panel1.Enabled = false;
                    Panel2.Visible = true;
                    table_Botoes_Atualizar.Visible = false;
                    table_Botoes_Confirmar.Visible = true;
                }
                else
                    AlterarDadosMedicamento(itensDispensacao, qtd_anterior);
            }
            else
                AlterarDadosMedicamento(itensDispensacao, qtd_anterior);
        }

        /// <summary>
        /// Salva o medicamento de acordo com os dados informados.
        /// </summary>
        /// <param name="item"></param>
        private void AtualizaMedicamentosMesmoLote(ItemDispensacao item)
        {
            //ItensDispensacaoBsn itemBsn = new ItensDispensacaoBsn();
            IDispensacao idispensacao = Factory.GetInstance<IDispensacao>();
            IFarmacia ifarmacia = Factory.GetInstance<IFarmacia>();
            IList<ItemDispensacao> lista = idispensacao.BuscarItensPorAtendimentoEmedicamento<ItemDispensacao>(int.Parse(Request["receita"].ToString()), int.Parse(Request["farmacia"].ToString()), item.LoteMedicamento.Medicamento.Codigo);//itemBsn.BuscarPorAtendimentoEmedicamento(int.Parse(Request["receita"].ToString()), int.Parse(Request["farmacia"].ToString()), item.LoteMedicamento.Medicamento.Codigo);

            //===========COMENTÁRIO===========//
            /*
             Atualizando a quantidade de dias e quantidade prescrita
             para medicamentos iguais de lotes diferentes. A data
             neste caso não é importante, já que o que importa
             é procedência do medicamento.
            */

            foreach (ItemDispensacao i in lista)
            {
                i.QtdDias = item.QtdDias;
                i.QtdPrescrita = item.QtdPrescrita;
                //itemBsn.Salvar(i);
                ifarmacia.Salvar(i);
            }
        }

        /// <summary>
        /// Cancelar a ação de alteração.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Cancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        /// <summary>
        /// Função que deixa a tabela do medicamento para alteração invísivel.
        /// E coloca o indíce do medicamento alterado como zero.
        /// </summary>
        private void LimparCampos()
        {
            linha_label_observacao.Visible = false;
            linha_textbox_observacao.Visible = false;
            table_Medicamento.Visible = false;
            table_Botoes_Atualizar.Visible = false;
            table_Botoes_Confirmar.Visible = false;
            Panel1.Enabled = true;
            Panel2.Visible = false;

            tbxQuantidadeDias.Text = "";
            tbxQuantidadeDispensada.Text = "";
            tbxQuantidadePrescrita.Text = "";
            tbxObservacao.Text = "";
        }

        /// <summary>
        /// Função que dá baixa no estoque e atualiza os seus valores
        /// </summary>
        private void DarBaixaEstoque(Estoque estoque, int qtd_atual, int qtd_anterior, ItemDispensacao item, string acao)
        {
            //EstoqueBsn estoqueBsn = new EstoqueBsn();
            IEstoque iestoque = Factory.GetInstance<IEstoque>();
            estoque.QuantidadeEstoque = estoque.QuantidadeEstoque - (qtd_atual - qtd_anterior);

            try
            {
                //estoqueBsn.AtualizarEstoque(estoque, item, acao);
                iestoque.AtualizarEstoque(estoque, item, acao);

                if (acao == "inserir")
                    AtualizaMedicamentosMesmoLote(item);
            }
            catch (Exception e)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O medicamento não pôde ser atualizado! Tente novamente mais tarde!');", true);
            }
        }

        /// <summary>
        /// Constrõe o objeto medicamento de acordo com os dados passados
        /// </summary>
        /// <param name="itensDispensacao"></param>
        /// <param name="qtd_anterior"></param>
        private void AlterarDadosMedicamento(ItemDispensacao itensDispensacao, int qtd_anterior)
        {
            bool dar_baixa = false;

            try
            {
                //EstoqueBsn estoqueBsn = new EstoqueBsn();
                IEstoque iestoque = Factory.GetInstance<IEstoque>();
                Estoque estoque = iestoque.BuscarItemEstoquePorFarmacia<Estoque>(int.Parse(Request["farmacia"].ToString()), itensDispensacao.LoteMedicamento.Codigo);//estoqueBsn.buscarPorFarmaciaElote(int.Parse(Request["farmacia"].ToString()), itensDispensacao.LoteMedicamento.Codigo);

                if (int.Parse(tbxQuantidadeDispensada.Text) > qtd_anterior)
                {
                    int desconto_estoque = int.Parse(tbxQuantidadeDispensada.Text) - qtd_anterior;

                    if (desconto_estoque > estoque.QuantidadeEstoque)
                    {
                        DesbloquearPanelAtualizacao();
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A quantidade disponível neste estoque é igual = " + estoque.QuantidadeEstoque.ToString() + " unidades. Por favor, informe uma quantidade menor para este medicamento.');", true);
                    }
                    else
                        dar_baixa = true;
                }
                else
                    dar_baixa = true;

                if (dar_baixa)
                {
                    DarBaixaEstoque(estoque, int.Parse(tbxQuantidadeDispensada.Text), qtd_anterior, itensDispensacao, "inserir");

                    LimparCampos();
                    CarregaItensDispensacao(DateTime.Parse(Request["data"].ToString()), int.Parse(Request["receita"].ToString()), int.Parse(Request["farmacia"].ToString()));
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Medicamento alterado com sucesso!');", true);
                }
            }
            catch (Exception f)
            {
                DesbloquearPanelAtualizacao();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O medicamento não pôde ser alterado no momento. Por favor, tente mais tarde.');", true);
            }
        }

        /// <summary>
        /// Cancela a confirmação para dispensar o medicamento com quantidade maior que 300 unidades 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Cancelar_Confirmacao_Click(object sender, EventArgs e)
        {
            DesbloquearPanelAtualizacao();
        }

        /// <summary>
        /// Desbloqueia o Panel 1 (De Atualização) e Bloqueia o Panel 2 (De Confirmação)
        /// </summary>
        private void DesbloquearPanelAtualizacao()
        {
            Panel1.Enabled = true;
            Panel2.Visible = false;
            table_Botoes_Confirmar.Visible = false;
            table_Botoes_Atualizar.Visible = true;
        }
    }
}
