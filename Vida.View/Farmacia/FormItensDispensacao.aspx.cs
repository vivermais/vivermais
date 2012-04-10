using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using Vida.ServiceFacade.ServiceFacades.Farmacia.Dispensacao;
using Vida.DAO;
using Vida.Model;
using Vida.ServiceFacade.ServiceFacades.Farmacia.Misc;
using Vida.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;
using Vida.ServiceFacade.ServiceFacades;
using Vida.ServiceFacade.ServiceFacades.Paciente;

namespace Vida.View.Farmacia
{
    public partial class FormItensDispensacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tbxDataAtendimento.Text = DateTime.Today.ToString("dd/MM/yyyy");

                int temp;

                if (Request["id_dispensacao"] != null && int.TryParse(Request["id_dispensacao"].ToString(), out temp))
                {
                    CarregaDadosPaciente(int.Parse(Request["id_dispensacao"].ToString()));
                    CarregaItensDefinitivo();
                }

                if (Session["unidade"] != null && Session["unidade"] is Vida.Model.EstabelecimentoSaude)
                    CarregaFarmaciasUnidade((Vida.Model.EstabelecimentoSaude)Session["unidade"]);

                CarregaItensDispensacao();
            }
        }

        /// <summary>
        /// Carrega no gridview os itens que já foram dispensados para esta receita
        /// </summary>
        private void CarregaItensDefinitivo()
        {
            //ItensDispensacaoBsn itensDispensacaoBsn = new ItensDispensacaoBsn();

            IFarmacia ifarmacia = Factory.GetInstance<IFarmacia>();
            IDispensacao idispensacao = Factory.GetInstance<IDispensacao>();

            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            /*
             A dispensação é feita por paciente e medicamento. Logo a busca deverá se feita pelo paciente.
             Caso a dispensação seja feita pela receita, basta somente descomentar a linha
             com a numeração = NF5 e retirar as duas logo abaixo dela.
             */
            //NF5 - IList<ItensDispensacao> itens = itensDispensacaoBsn.BuscarPorDispensacao(int.Parse(Request["id_dispensacao"].ToString()));
            //DispensacaoBsn dBsn = new DispensacaoBsn();
            Vida.Model.ReceitaDispensacao dispensacao = ifarmacia.BuscarPorCodigo<Vida.Model.ReceitaDispensacao>(int.Parse(Request["id_dispensacao"].ToString()));
            IList<ItemDispensacao> itens = idispensacao.BuscarItensDispensacaoPorPaciente<ItemDispensacao>(dispensacao.CodigoPaciente); //itensDispensacaoBsn.BuscarPorPaciente(dBsn.buscarPorCodigo(int.Parse(Request["id_dispensacao"].ToString())).Paciente.Codigo);

            Session["itens_dispensacao_definitivo"] = itens;
        }

        /// <summary>
        /// Limpa os dados das sessões que somente são utilizados nesta página
        /// </summary>
        private void LimparSessaoPagina()
        {
            Session.Remove("estoque_escolhido");
            Session.Remove("itens_dispensacao");
            Session.Remove("itens_dispensacao_definitivo");
        }

        /// <summary>
        /// Carrega as farmácias cadastradas na unidade escolhido pelo usuário quando este efetua seu login.
        /// </summary>
        /// <param name="unidade"></param>
        private void CarregaFarmaciasUnidade(Vida.Model.EstabelecimentoSaude unidade)
        {
            //FarmaciaBsn farmaciaBsn = new FarmaciaBsn();
            //IList<Farmacia> farmacias = farmaciaBsn.buscarPorUnidade(unidade.Codigo);
            IFarmacia ifarmacia = Factory.GetInstance<IFarmacia>();
            IList<Vida.Model.Farmacia> farmacias = ifarmacia.BuscarPorEstabelecimentoSaude<Vida.Model.Farmacia>(unidade.Codigo);

            ddlFarmacia.Items.Add(new ListItem("Selecione...", "0"));

            foreach (Vida.Model.Farmacia farmacia in farmacias)
                ddlFarmacia.Items.Add(new ListItem(farmacia.Nome, farmacia.Codigo.ToString()));

            if (farmacias != null && farmacias.Count == 1)
            {
                ddlFarmacia.SelectedValue = farmacias[0].Codigo.ToString();
                ddlFarmacia.Enabled = false;
            }
        }

        /// <summary>
        /// Função que busca o medicamento pela letra escolhida.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void e_buscar_medicamento_letra(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            ViewState["escolha"] = "letra";
            ViewState["letra"] = lb.CommandArgument.ToString();
            CarregaMedicamentos(lb.CommandArgument.ToString(), true);
        }

        /// <summary>
        /// Função que busca o medicamento pelo nome informado pelo usuário. O número mínimo de caracter
        /// é de 4 posições.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void e_bucsar_medicamento_nome(object sender, EventArgs e)
        {
            if (tbxNomeMedicamento.Text.Trim().Length >= 4)
            {
                ViewState["escolha"] = "nome";
                ViewState["nome"] = tbxNomeMedicamento.Text.ToString();
                CarregaMedicamentos(tbxNomeMedicamento.Text.ToString(), false);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Digite um medicamento com 4 ou mais letras!');", true);
        }

        /// <summary>
        /// Verifica se o usuário está tentando incluir um novo medicamento na lista temporária.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void e_verificar_acao(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "c_incluir")
            {
                int index_objeto;

                if (int.Parse(e.CommandArgument.ToString()) == 0)
                    index_objeto = GridView1.PageIndex * GridView1.PageSize;
                else
                    index_objeto = (GridView1.PageIndex * GridView1.PageSize) + int.Parse(e.CommandArgument.ToString());

                ApagarDadosMedicamento();
                DesbloqueiaConteudo();

                Carrega_Medicamento(index_objeto);
            }
        }

        /// <summary>
        /// Função que Carrega os dados do Medicamento para atualizar os dias, a quantidade prescrita 
        /// e a quantidade dispensada. Caso o medicamento já esteja incluso na lista de itens ele é buscado
        /// e colocado para o usuário como uma alteração a ser feita em cima do lote utilizado para dispensa.
        /// </summary>
        /// <param name="index_medicamento"></param>
        private void Carrega_Medicamento(int index_medicamento)
        {
            IList<Estoque> estoques = (IList<Estoque>)Session["estoques"];
            Estoque estoque = estoques[index_medicamento];
            lbMedicamentoEscolhido.Text = estoque.LoteMedicamento.Medicamento.Nome + " - " + estoque.LoteMedicamento.Lote;

            bool resultado_pesquisa_lote = BuscarNaListaDeItens(estoque);

            if (!resultado_pesquisa_lote)
                Session["estoque_escolhido"] = estoque;

            //BuscarNaListaDefinitiva(estoque);
        }

        /// <summary>
        /// Caso haja um registro do medicamento no banco de dados ele será bloqueado para a quantidade de dias
        /// e quantidade prescrita.
        /// </summary>
        /// <param name="estoque"></param>
        private void BuscarNaListaDefinitiva(Estoque estoque)
        {
            bool achou = false;
            int i = 0;
            List<ItemDispensacao> itens_dispensacao = null;

            if (Session["itens_dispensacao_definitivo"] != null && Session["itens_dispensacao_definitivo"] is List<ItemDispensacao>)
            {
                itens_dispensacao = (List<ItemDispensacao>)Session["itens_dispensacao_definitivo"];
                foreach (ItemDispensacao item in itens_dispensacao)
                {
                    if (item.LoteMedicamento.Medicamento.Codigo == estoque.LoteMedicamento.Medicamento.Codigo)
                    {
                        achou = true;
                        break;
                    }
                    i++;
                }
            }

            if (achou)
                BloqueiaDiasEquantidadePrescrita(itens_dispensacao[i].QtdDias, itens_dispensacao[i].QtdPrescrita);
        }

        /// <summary>
        /// Bloqueia os campos de dias e quantidade prescrita
        /// </summary>
        /// <param name="dias"></param>
        /// <param name="qtd_prescrita"></param>
        private void BloqueiaDiasEquantidadePrescrita(int dias, int qtd_prescrita)
        {
            tbxQuantidadeDias.Text = dias.ToString();
            tbxQuantidadeDias.ReadOnly = true;
            tbxQuantidadePrescrita.Text = qtd_prescrita.ToString();
            tbxQuantidadePrescrita.ReadOnly = true;
        }

        /// <summary>
        /// Função que busca a existência do medicamento do mesmo Lote
        /// na lista que está sendo criada para dar baixa no estoque.
        /// </summary>
        /// <param name="estoque"></param>
        private bool BuscarNaListaDeItens(Estoque estoque)
        {
            bool achou = false;
            int i = 0;

            if (Session["itens_dispensacao"] != null && Session["itens_dispensacao"] is List<ItemDispensacao>)
            {
                List<ItemDispensacao> itens_dispensacao = (List<ItemDispensacao>)Session["itens_dispensacao"];

                foreach (ItemDispensacao item in itens_dispensacao)
                {
                    if (estoque.LoteMedicamento.Codigo == item.LoteMedicamento.Codigo)
                    {
                        achou = true;
                        break;
                    }
                    i++;
                }

                if (achou)
                    PreencheDadosMedicamento(itens_dispensacao[i], estoque, i);
            }

            return achou;
        }

        /// <summary>
        /// Carrega os medicamentos buscados no gridview.
        /// </summary>
        /// <param name="palavra"></param>
        /// <param name="letra"></param>
        private void CarregaMedicamentos(string palavra, bool letra)
        {
            ApagarDadosMedicamento();
            DesbloqueiaConteudo();

            if (ddlFarmacia.SelectedValue != "0")
            {
                //EstoqueBsn estoqueBsn = new EstoqueBsn();
                IEstoque iestoque = Factory.GetInstance<IEstoque>();
                //IList<Estoque> estoques = estoqueBsn.buscarPorFarmaciaEmedicamento(int.Parse(ddlFarmacia.SelectedValue.ToString()), true, letra, palavra);
                IList<Estoque> estoques = iestoque.BuscarPorFarmacia<Vida.Model.Estoque>(int.Parse(ddlFarmacia.SelectedValue));
                if (letra)
                    estoques = (IList<Estoque>)from estoque in estoques where estoque.LoteMedicamento.Medicamento.Nome.Contains(palavra) select estoque;
                GridView1.DataSource = estoques;
                GridView1.DataBind();
                Session["estoques"] = estoques;

                if (estoques == null || estoques.Count == 0)
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não foram encontrados medicamentos na farmácia selecionada com os dados informados!');", true);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Escolha uma Farmácia!');", true);
        }

        /// <summary>
        /// Carrega os dados do paciente na tela para visualização.
        /// </summary>
        /// <param name="id_dispensacao"></param>
        private void CarregaDadosPaciente(int id_dispensacao)
        {
            //DispensacaoBsn dispensacaoBsn = new DispensacaoBsn();
            //Dispensacao dispensacao = dispensacaoBsn.BuscarPorCodigo<Dispensacao>(id_dispensacao);
            IFarmacia ifarmacia = Factory.GetInstance<IFarmacia>();
            //IVidaServiceFacade ivida = Factory.GetInstance<IVidaServiceFacade>();
            IPaciente ipaciente = Factory.GetInstance<IPaciente>();
            Vida.Model.ReceitaDispensacao dispensacao = ifarmacia.BuscarPorCodigo<Vida.Model.ReceitaDispensacao>(id_dispensacao);
            tbxNumeroReceita.Text = id_dispensacao.ToString();
            Vida.Model.Paciente paciente = ipaciente.BuscarPorCodigo<Vida.Model.Paciente>(dispensacao.CodigoPaciente);
            IList<Vida.Model.CartaoSUS> cartoes = ipaciente.ListarCartoesSUS<Vida.Model.CartaoSUS>(dispensacao.CodigoPaciente);
            tbxCartaoSUS.Text = cartoes[0].Numero;//dispensacao.Paciente.CartaoSus.ToString();
            tbxPaciente.Text = paciente.Nome;
        }

        /// <summary>
        /// Permite a paginação dos medicamentos mostrados para visualização do usuário.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void e_paginacao(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["escolha"] != null)
            {
                if (ViewState["escolha"].ToString() == "letra")
                    CarregaMedicamentos(ViewState["letra"].ToString(), true);
                else
                    CarregaMedicamentos(ViewState["nome"].ToString(), false);

                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataBind();
            }
        }

        /// <summary>
        /// Função que salva o medicamento em uma lista temporária para cadastrá-lo posteriormente.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Salvar_Medicamento_click(object sender, EventArgs e)
        {
            IFarmacia ifarmacia = Factory.GetInstance<IFarmacia>();
            IDispensacao idispensacao = Factory.GetInstance<IDispensacao>();
            if (Session["estoque_escolhido"] != null && Session["estoque_escolhido"] is Estoque)
            {
                string acao = string.Empty;

                if (sender.GetType() == typeof(Button))
                {
                    Button bt = (Button)sender;
                    acao = bt.CommandArgument;
                }
                else
                    if (sender.GetType() == typeof(string))
                        acao = sender.ToString();

                Estoque estoque = (Estoque)Session["estoque_escolhido"];

                if (int.Parse(tbxQuantidadeDispensada.Text) >= 300 && (ViewState["quantidade_liberada"] == null || (ViewState["quantidade_liberada"] != null && ViewState["quantidade_liberada"].ToString() == "não"))) //Bloqueia a quantidade para confirmação
                    BloquearPanelQuantidade(acao);
                else
                {
                    ViewState.Remove("quantidade_liberada");

                    if ((acao == "c_inserir" || acao == "c_confirmar_observacao") &&
                        estoque.QuantidadeEstoque < int.Parse(tbxQuantidadeDispensada.Text.ToString())) ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A quantidade do estoque para o lote deste medicamento é insuficiente! Por favor, informe uma quantidade menor ou escolha outro lote.');", true);
                    else
                    {
                        ItemDispensacao item = new ItemDispensacao();
                        //DispensacaoBsn dispensacaoBsn = new DispensacaoBsn();

                        item.DataAtendimento = DateTime.Today;
                        //item.Dispensacao = ifarmacia.BuscarPorCodigo<Vida.Model.ReceitaDispensacao>(int.Parse(Request["id_dispensacao"]));//dispensacaoBsn.BuscarPorCodigo<Dispensacao>(int.Parse(Request["id_dispensacao"].ToString()));
                        item.LoteMedicamento = estoque.LoteMedicamento;
                        item.Farmacia = estoque.Farmacia;
                        item.QtdDias = int.Parse(tbxQuantidadeDias.Text.ToString());
                        item.QtdDispensada = int.Parse(tbxQuantidadeDispensada.Text.ToString());
                        item.QtdPrescrita = int.Parse(tbxQuantidadePrescrita.Text.ToString());

                        if (tabelaObservacao.Visible == true)
                            item.Observacao = tbxObservacao.Text.ToString();

                        if (acao == "c_inserir")
                        {
                            string resultado = string.Empty;
                            resultado = VerificarLiberacaoMedicamento(item, (List<ItemDispensacao>)Session["itens_dispensacao_definitivo"]);

                            if (resultado.Contains("Codigo_1")) //Não pode pegar o medicamento por algum motivo
                            {
                                resultado = resultado.Replace("Codigo_1 - ", "");
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + resultado + "');", true);
                            }
                            else
                                if (resultado.Contains("Codigo_2")) //Habilitando propriedade para pegar medicamento com observação
                                {
                                    resultado = resultado.Replace("Codigo_2 - ", "");
                                    btn_Confirmar_Medicamento.Visible = true;
                                    tabelaObservacao.Visible = true;
                                    lbConteudo.Text = resultado;
                                    btn_Salvar_Medicamento.Visible = false;
                                }
                                else
                                {
                                    resultado = resultado.Replace("Codigo_3 - ", ""); //Pegando o medicamento pela primeira vez
                                    //ItensDispensacaoBsn itensDispensacaoBsn = new ItensDispensacaoBsn();
                                    //itensDispensacaoBsn.Salvar(item);
                                    //BuscarNaListaTemporaria(item, -1);
                                    //ApagarDadosMedicamento();
                                    //DesbloqueiaConteudo();
                                    //CarregaItensDispensacao();
                                    ItemDispensacao valor_antigo = new ItemDispensacao();
                                    valor_antigo.QtdDispensada = 0;
                                    DarBaixaEstoque(item, valor_antigo, -1, "inserir");
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + resultado + "');", true);
                                }
                        }
                        else
                        {
                            if (acao == "c_confirmar_observacao") //Confirmando a liberação do medicamento com observação
                            {
                                //ItensDispensacaoBsn itensDispensacaoBsn = new ItensDispensacaoBsn();
                                //itensDispensacaoBsn.Salvar(item);
                                //BuscarNaListaTemporaria(item, -1);
                                //ApagarDadosMedicamento();
                                //DesbloqueiaConteudo();
                                //CarregaItensDispensacao();
                                ItemDispensacao valor_antigo = new ItemDispensacao();
                                valor_antigo.QtdDispensada = 0;
                                DarBaixaEstoque(item, valor_antigo, -1, "inserir");
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Medicamento incluído com sucesso!');", true);
                            }
                            else
                            {   //Neste caso, o medicamento está sendo alterado. Obs:. btCommandArgument = "c_alterar"
                                List<ItemDispensacao> lista_temp = (List<ItemDispensacao>)Session["itens_dispensacao"];
                                ItemDispensacao disp_temp = lista_temp[int.Parse(ViewState["index_item_alteracao"].ToString())];
                                //ItensDispensacaoBsn itensDispensacaoBsn = new ItensDispensacaoBsn();
                                //ItensDispensacao item_esc = itensDispensacaoBsn.BuscarPorItem(disp_temp.Dispensacao.Codigo, disp_temp.LoteMedicamento.Codigo, disp_temp.DataAtendimento);
                                ItemDispensacao item_esc = new ItemDispensacao();//idispensacao.BuscarPorItem<Vida.Model.ItensDispensacao>(disp_temp.Dispensacao.Codigo, disp_temp.LoteMedicamento.Codigo, disp_temp.DataAtendimento);
                                ItemDispensacao valor_antigo = new ItemDispensacao();
                                valor_antigo.QtdDispensada = item_esc.QtdDispensada;

                                item_esc.QtdDias = item.QtdDias;
                                item_esc.QtdDispensada = item.QtdDispensada;
                                item_esc.QtdPrescrita = item.QtdPrescrita;

                                if (item_esc.QtdDispensada > valor_antigo.QtdDispensada + estoque.QuantidadeEstoque)
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A quantidade do estoque para o lote deste medicamento é insuficiente! Por favor, informe uma quantidade menor ou escolha outro lote.');", true);
                                else
                                {
                                    //itensDispensacaoBsn.Salvar(item_esc);
                                    ifarmacia.Salvar(item_esc);
                                    //BuscarNaListaTemporaria(item_esc, int.Parse(ViewState["index_item_alteracao"].ToString())); //Alterando o medicamento
                                    //ApagarDadosMedicamento();
                                    //DesbloqueiaConteudo();
                                    //CarregaItensDispensacao();
                                    DarBaixaEstoque(item_esc, valor_antigo, int.Parse(ViewState["index_item_alteracao"].ToString()), "inserir");
                                    CarregaItensDefinitivo();
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Medicamento alterado com sucesso!');", true);
                                }
                            }
                        }
                    }
                }
            }

            CarregaItensDefinitivo();
        }

        /// <summary>
        /// Função que bloqueia o Panel_um para confirmação do usuário se a quantidade de medicamento dispensada realmente é aquela.
        /// </summary>
        /// <param name="acao_anterior">Ação anterior antes do bloqueio da quantidade</param>
        private void BloquearPanelQuantidade(string acao_anterior)
        {
            Panel_um.Enabled = false;
            Panel_dois.Visible = true;
            ViewState["acao_anterior"] = acao_anterior;
        }

        /// <summary>
        /// Desbloqueia o Panel_um e deixa o Panel_dois invisível para quantidade
        /// </summary>
        private void DesbloquearPanelQuantidade()
        {
            Panel_um.Enabled = true;
            Panel_dois.Visible = false;
            ViewState.Remove("acao_anterior");
            ViewState.Remove("quantidade_liberada");
        }

        /// <summary>
        /// Função que confirma a liberação do medicamento para quantidade superior ou igual a 300 unidades
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Confirmar_Quantidade_Medicamento_click(object sender, EventArgs e)
        {
            string acao_anterior = ViewState["acao_anterior"].ToString();
            DesbloquearPanelQuantidade();
            ViewState["quantidade_liberada"] = "sim";
            btn_Salvar_Medicamento_click(acao_anterior, e);
        }

        /// <summary>
        /// Função que desbloqueia o Panel_um, já que o usuário cancelou a confirmação para liberar a quantidade
        /// do medicamento superior ou igual a 300 unidades.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Cancelar_Quantidade_Medicamento_click(object sender, EventArgs e)
        {
            DesbloquearPanelQuantidade();
        }

        /// <summary>
        /// Caso o medicamento escolhido (sempre o último colocado na lista) já exista na lista temporária
        /// e pertença a um lote diferente, haverá uma alteração em todos os itens da lista com o mesmo nome.
        /// </summary>
        /// <param name="estoque"></param>
        private void BuscarNaListaTemporaria(ItemDispensacao item, int index, string acao)
        {
            if (Session["itens_dispensacao"] != null && Session["itens_dispensacao"] is List<ItemDispensacao>)
            {
                List<ItemDispensacao> itens_dispensacao = (List<ItemDispensacao>)Session["itens_dispensacao"];
                List<ItemDispensacao> itens_dispensacao_temp = new List<ItemDispensacao>();

                foreach (ItemDispensacao item_f in itens_dispensacao)
                {
                    if (item_f.LoteMedicamento.Medicamento.Codigo == item.LoteMedicamento.Medicamento.Codigo)
                    {
                        item_f.QtdDias = item.QtdDias;
                        item_f.QtdPrescrita = item.QtdPrescrita;
                    }
                    itens_dispensacao_temp.Add(item_f);
                }

                if (acao != "remover")
                {
                    if (index < 0)
                        itens_dispensacao_temp.Add(item);
                    else
                        itens_dispensacao_temp[index] = item;
                }
                else
                    itens_dispensacao_temp.RemoveAt(index);

                Session["itens_dispensacao"] = itens_dispensacao_temp;
            }
            else
            {
                List<ItemDispensacao> itens_dispensacao = new List<ItemDispensacao>();
                itens_dispensacao.Add(item);
                Session["itens_dispensacao"] = itens_dispensacao;
            }
        }

        /// <summary>
        /// Cancela a inclusão do medicamento na lista temporária apagando todos seus dados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Cancelar_Medicamento_click(object sender, EventArgs e)
        {
            ApagarDadosMedicamento();
            DesbloqueiaConteudo();
        }

        /// <summary>
        /// Função que apaga os dados do medicamento temporário.
        /// </summary>
        private void ApagarDadosMedicamento()
        {
            Session["estoque_escolhido"] = null;
            btn_Salvar_Medicamento.Visible = true;
            btn_Alterar_Medicamento.Visible = false;
            btn_Confirmar_Medicamento.Visible = false;
            tabelaObservacao.Visible = false;
            lbMedicamentoEscolhido.Text = "Nenhum";
            tbxQuantidadeDias.Text = "";
            tbxQuantidadeDispensada.Text = "";
            tbxQuantidadePrescrita.Text = "";
        }

        /// <summary>
        /// Função que desbloqueia os campos de dias e quantidade prescrita para o caso de inserção.
        /// </summary>
        private void DesbloqueiaConteudo()
        {
            tbxQuantidadeDias.ReadOnly = false;
            tbxQuantidadePrescrita.ReadOnly = false;
        }

        /// <summary>
        /// Carrega a lista temporária no gridview para visualização.
        /// </summary>
        private void CarregaItensDispensacao()
        {
            List<ItemDispensacao> itens = (List<ItemDispensacao>)Session["itens_dispensacao"];
            GridView2.DataSource = itens;
            GridView2.DataBind();
        }

        /// <summary>
        /// Função que preenche os dados do medicamento para alterá-lo ou exclui o mesmo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void e_atualizar_itens_escolhidos(object sender, GridViewCommandEventArgs e)
        {
            IDispensacao idispensacao = Factory.GetInstance<IDispensacao>();
            IEstoque iestoque = Factory.GetInstance<IEstoque>();
            List<ItemDispensacao> itens = (List<ItemDispensacao>)Session["itens_dispensacao"];

            if (e.CommandName == "c_alterar")
            {
                ItemDispensacao item = itens[int.Parse(e.CommandArgument.ToString())];
                //EstoqueBsn estoqueBsn = new EstoqueBsn();
                //Estoque estoque = estoqueBsn.buscarPorFarmaciaElote(item.Farmacia.Codigo, item.LoteMedicamento.Codigo);
                Estoque estoque = iestoque.BuscarItemEstoquePorFarmacia<Estoque>(item.Farmacia.Codigo, item.LoteMedicamento.Codigo);

                DesbloqueiaConteudo();

                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                /*
                  Modifiquei aqui porque a receita pode mudar para o medicamento.
                  Caso a regra de negócio mude é só descomentar onde houver BuscarNaListaDefinitiva(Estoque es)
                  Com isso, o função irá buscar no banco os dados daquele medicamento e caso exista algum registro
                  os dados de quantidade de dias e quantida prescrita será preenchido e bloqueado automaticamente.
                */

                //BuscarNaListaDefinitiva(estoque);

                PreencheDadosMedicamento(item, estoque, int.Parse(e.CommandArgument.ToString()));
            }
            else
            {
                int index = int.Parse(e.CommandArgument.ToString());

                ItemDispensacao item_esc = ((List<ItemDispensacao>)Session["itens_dispensacao"])[index];
                //ItensDispensacaoBsn itensDispensacaoBsn = new ItensDispensacaoBsn();
                //ItensDispensacao item = itensDispensacaoBsn.BuscarPorItem(item_esc.Dispensacao.Codigo, item_esc.LoteMedicamento.Codigo, item_esc.DataAtendimento);
                ItemDispensacao item = idispensacao.BuscarPorItem<ItemDispensacao>(1, item_esc.LoteMedicamento.Codigo, item_esc.DataAtendimento);
                try
                {
                    //itensDispensacaoBsn.Delete(item);
                    //itens.RemoveAt(index);
                    //item_esc.QtdDispensada = 0;
                    item.QtdDispensada = 0;
                    DarBaixaEstoque(item, item_esc, index, "remover");
                }
                catch (Exception f)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O medicamento não pôde ser excluído no momento! Por favor, tente mais tarde.');", true);
                }

                //Session["itens_dispensacao"] = itens;

                //ApagarDadosMedicamento();
                //DesbloqueiaConteudo();
                //CarregaItensDispensacao();
                CarregaItensDefinitivo();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Medicamento excluído com sucesso!');", true);
            }
        }

        /// <summary>
        /// Preenche os dados do medicamento temporário escolhido no quadro disponível 
        /// para visualização do usuário.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="estoque"></param>
        /// <param name="index_item_dispensacao"></param>
        private void PreencheDadosMedicamento(ItemDispensacao item, Estoque estoque, int index_item_dispensacao)
        {
            lbMedicamentoEscolhido.Text = item.LoteMedicamento.Medicamento.Nome + " - Lote: " + item.LoteMedicamento.Lote;
            tbxQuantidadeDias.Text = item.QtdDias.ToString();
            tbxQuantidadePrescrita.Text = item.QtdPrescrita.ToString();
            tbxQuantidadeDispensada.Text = item.QtdDispensada.ToString();

            Session["estoque_escolhido"] = estoque;

            ViewState["index_item_alteracao"] = index_item_dispensacao.ToString();
            btn_Alterar_Medicamento.Visible = true;

            if (!string.IsNullOrEmpty(item.Observacao))
            {
                lbConteudo.Text = "Observação para Adiantamento de Entrega.";
                tabelaObservacao.Visible = true;
                tbxObservacao.Text = item.Observacao;
            }
            else
                tabelaObservacao.Visible = false;

            btn_Salvar_Medicamento.Visible = false;
        }

        /// <summary>
        /// Salva os medicamentos na lista temporária e dá baixa nos seus respectivos estoques.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Salvar_click(object sender, EventArgs e)
        {
            if (Session["itens_dispensacao"] != null && Session["itens_dispensacao"] is List<ItemDispensacao>)
            {
                LimparSessaoPagina();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Medicamentos dispensados com sucesso!');location='Default.aspx';", true);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('É necessário incluir pelo menos um medicamento para concluir esta dispensação!');", true);
        }

        /// <summary>
        /// Verifica se é possível liberar o medicamento de acordo com as regras de negócio
        /// </summary>
        /// <param name="itens"></param>
        private string VerificarLiberacaoMedicamento(ItemDispensacao item_disp, List<ItemDispensacao> itens_jd)
        {
            string resultado = string.Empty;

            //=========================================================================================
            //Inicialmente a liberação do medicamento está correta
            resultado = "Codigo_3 - O medicamento: " + item_disp.LoteMedicamento.Medicamento.Nome +
            " foi liberado no dia: " + DateTime.Today.ToString("dd/MM/yyyy") +
            ". A sua próxima dispensação deve ser no dia: " +
            DateTime.Today.AddDays(double.Parse(item_disp.QtdDias.ToString()) - double.Parse(getDias(item_disp.QtdDias).ToString())).ToString("dd/MM/yyyy") + ".";
            //=========================================================================================

            if (itens_jd != null)
            {
                Medicamento i = item_disp.LoteMedicamento.Medicamento;

                //=========================================================================================
                //Verifica se o paciente já ultrapassou a cota dele
                var cota_medicamento =
                    (from m in itens_jd
                     where m.LoteMedicamento.Medicamento.Codigo == i.Codigo
                     group m by new { m.LoteMedicamento.Medicamento.Codigo, m.QtdPrescrita }
                         into g
                         select new
                         {
                             CodigoMedicamento = g.Key.Codigo,
                             QtdPrescrita = g.Key.QtdPrescrita,
                             TotalDispensada = g.Sum(m => m.QtdDispensada)
                         }
                    );

                foreach (var cota in cota_medicamento)
                {
                    if (cota.TotalDispensada >= cota.QtdPrescrita)
                    {
                        resultado = "Codigo_1 - A cota para este medicamento foi atingida." +
                        " A quantidade dispensada até o momento foi de: " +
                        cota.TotalDispensada.ToString() + " unidades. Sendo que a sua quantidade prescrita (total) é de: " + cota.QtdPrescrita.ToString() + " unidades.";
                        return resultado;
                    }
                }
                //=========================================================================================

                //(Pesquisa do medicamento no mesmo dia)
                var consulta_um = (from m in itens_jd
                                   where m.DataAtendimento.ToString("dd/MM/yyyy")
                                       == DateTime.Today.ToString("dd/MM/yyyy") &&
                                       m.LoteMedicamento.Medicamento.Codigo == i.Codigo
                                   select m);

                IEnumerable<ItemDispensacao> lista_um = consulta_um.Cast<ItemDispensacao>();
                if (lista_um != null && lista_um.Count() > 0)
                {
                    foreach (ItemDispensacao item in lista_um)
                    {
                        //=========================================================================================
                        if (item.Farmacia.Codigo != int.Parse(ddlFarmacia.SelectedValue.ToString())) //Verifica se o medicamento já foi liberado no mesmo dia em farmácias diferentes
                            return "Codigo_1 - O medicamento: " + item_disp.LoteMedicamento.Medicamento.Nome + " já foi liberado " +
                            "no dia: " + item.DataAtendimento.ToString("dd/MM/yyyy") + " na farmácia " + item.Farmacia.Nome;
                        else
                            if (item.LoteMedicamento.Codigo == item_disp.LoteMedicamento.Codigo) //Verifica se o medicamento já foi liberado no mesmo dia, na mesma farmácia e mesmo lote de medicamento
                                return "Codigo_1 - O medicamento: " + item_disp.LoteMedicamento.Medicamento.Nome + " já foi liberado " +
                                "do lote escolhido na data de hoje. Por favor escolha outro lote.";
                        //=========================================================================================
                    }
                }

                //=========================================================================================
                //Consulta todas as dispensações diferentes do dia de hoje para verificar se é possível liberar,
                //liberar com observação de adiantamento ou 'barrar' o medicamento para o paciente
                var consulta =
                    (from m in itens_jd
                     where m.LoteMedicamento.Medicamento.Codigo == i.Codigo
                     && m.DataAtendimento ==
                    (from mi in itens_jd
                     where mi.LoteMedicamento.Medicamento.Codigo == i.Codigo
                     && mi.DataAtendimento.ToString("dd/MM/yyyy") != DateTime.Today.ToString("dd/MM/yyyy")
                     select mi.DataAtendimento == null ? new Nullable<DateTime>() : mi.DataAtendimento).Max()
                     select m);

                IEnumerable<ItemDispensacao> lista_itens = consulta.Cast<ItemDispensacao>();

                if (lista_itens.Count() > 0)
                {
                    foreach (ItemDispensacao item in lista_itens)
                    {
                        int subtrair = getDias(item.QtdDias);

                        double d = double.Parse(subtrair.ToString());

                        DateTime data = item.DataAtendimento.AddDays(double.Parse(item.QtdDias.ToString()) - d);

                        TimeSpan diff = data.Subtract(DateTime.Today);

                        //var sub_consulta =
                        //    (from m in itens_jd
                        //     where m.LoteMedicamento.Medicamento.Codigo == i.Codigo
                        //     group m by new { m.LoteMedicamento.Medicamento.Codigo, m.QtdPrescrita }
                        //         into g
                        //         select new
                        //         {
                        //             CodigoMedicamento = g.Key.Codigo,
                        //             QtdPrescrita = g.Key.QtdPrescrita,
                        //             TotalDispensada = g.Sum(m => m.QtdDispensada)
                        //         });

                        //foreach (var tipo in sub_consulta)
                        //{
                        //    if (tipo.TotalDispensada >= tipo.QtdPrescrita)
                        //    {
                        //        resultado = "Codigo_1 - A cota para este medicamento foi atingida." +
                        //        " A quantidade dispensada até o momento foi de: " +
                        //        tipo.TotalDispensada.ToString() + " unidades. Sendo que a sua quantidade prescrita (total) é de: " + tipo.QtdPrescrita.ToString() + " unidades.";
                        //        return resultado;
                        //    }
                        //}

                        if (diff.Days > 5)
                        {
                            resultado = "Codigo_1 - O medicamento: " + item.LoteMedicamento.Medicamento.Nome +
                            " não pode ser dispensado. Ele foi liberado no dia: " +
                            item.DataAtendimento.ToString("dd/MM/yyyy") +
                            " na farmácia: " + item.Farmacia.Nome + ". A quantidade dispensada " +
                            "nesta data foi de: " + item.QtdDispensada.ToString() + " unidades." +
                            " A sua próxima dispensação deve ser no dia: " + data.ToString("dd/MM/yyyy") + ".";
                            return resultado;
                        }
                        else
                            if (diff.Days > 0 && diff.Days <= 5)
                            {
                                resultado = "Codigo_2 - O medicamento: " + item.LoteMedicamento.Medicamento.Nome +
                                " foi liberado no dia: " + item.DataAtendimento.ToString("dd/MM/yyyy") +
                                " na farmácia: " + item.Farmacia.Nome + ". A quantidade dispensada " +
                                "nesta data foi de: " + item.QtdDispensada.ToString() + " unidades." +
                                " A sua próxima dispensação deve ser no dia: " +
                                data.ToString("dd/MM/yyyy") + ". Para dispensá-lo agora, preencha o campo de Observação a seguir.";
                                return resultado;
                            }
                            else
                            {
                                resultado = "Codigo_3 - O medicamento: " + item.LoteMedicamento.Medicamento.Nome +
                                " foi liberado no dia: " + DateTime.Today.ToString("dd/MM/yyyy") +
                                ". A sua próxima dispensação deve ser no dia: " +
                                DateTime.Today.AddDays(double.Parse(item_disp.QtdDias.ToString()) - double.Parse(getDias(item_disp.QtdDias).ToString())).ToString("dd/MM/yyyy") + ".";
                                return resultado;
                            }
                    }
                }
            }

            return resultado;
        }

        /// <summary>
        /// Função que retorna a quantidade de dias a ser adicionada para a próxima dispensação.
        /// </summary>
        /// <param name="dias"></param>
        /// <returns></returns>
        private int getDias(int dias)
        {
            if (dias > 20)
                return 10;
            else
            {
                if (dias > 10)
                    return int.Parse(((float)(dias / 2)).ToString("0"));
                else
                    return 0;
            }
        }

        /// <summary>
        /// Função que dá baixa no estoque e insere o item com seus respectivos valores.
        /// </summary>
        /// <returns></returns>
        private void DarBaixaEstoque(ItemDispensacao valor_novo, ItemDispensacao valor_antigo, int index_medicamento, string acao)
        {
            //EstoqueBsn estoqueBsn = new EstoqueBsn();
            IEstoque iestoque = Factory.GetInstance<IEstoque>();
            //Estoque estoque = estoqueBsn.buscarPorFarmaciaElote(valor_novo.Farmacia.Codigo, valor_novo.LoteMedicamento.Codigo);
            Estoque estoque = iestoque.BuscarItemEstoquePorFarmacia<Estoque>(valor_novo.Farmacia.Codigo, valor_novo.LoteMedicamento.Codigo);

            estoque.QuantidadeEstoque = estoque.QuantidadeEstoque - (valor_novo.QtdDispensada - valor_antigo.QtdDispensada);

            try
            {
                //estoqueBsn.AtualizarEstoque(estoque, valor_novo, acao);
                iestoque.AtualizarEstoque(estoque, valor_novo, acao);
                BuscarNaListaTemporaria(valor_novo, index_medicamento, acao);

                ApagarDadosMedicamento();
                DesbloqueiaConteudo();
                CarregaItensDispensacao();
            }
            catch (Exception e)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('No momento, não foi possível atualizar o estoque para este medicamento! Por favor, tente novamente.');", true);
                throw e;
            }
            finally
            {
                AtualizaMedicamentosUsuario();
            }
        }

        /// <summary>
        /// Função que atualiza os medicamentos para o usuário visualizar o registro de movimentação do estoque.
        /// </summary>
        private void AtualizaMedicamentosUsuario()
        {
            if (ViewState["escolha"] != null)
            {
                if (ViewState["escolha"].ToString() == "letra")
                    CarregaMedicamentos(ViewState["letra"].ToString(), true);
                else
                    CarregaMedicamentos(ViewState["nome"].ToString(), false);
            }
        }

        /// <summary>
        /// Função que cancela a possível dispensação naquele momento.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Cancelar_click(object sender, EventArgs e)
        {
            if (Session["itens_dispensacao"] != null && Session["itens_dispensacao"] is List<ItemDispensacao>)
            {
                //ItensDispensacaoBsn iBsn = new ItensDispensacaoBsn();
                //EstoqueBsn eBsn = new EstoqueBsn();
                IDispensacao idispensacao = Factory.GetInstance<IDispensacao>();
                IEstoque iestoque = Factory.GetInstance<IEstoque>();

                bool ok = true;
                string medicamento = string.Empty;

                foreach (ItemDispensacao i in ((List<ItemDispensacao>)Session["itens_dispensacao"]))
                {
                    try
                    {
                        //Estoque es = eBsn.buscarPorFarmaciaElote(i.Farmacia.Codigo, i.LoteMedicamento.Codigo);
                        Estoque es = iestoque.BuscarItemEstoquePorFarmacia<Estoque>(i.Farmacia.Codigo, i.LoteMedicamento.Codigo);
                        //ItensDispensacao iB = iBsn.BuscarPorItem(i.Dispensacao.Codigo, i.LoteMedicamento.Codigo, i.DataAtendimento);
                        ItemDispensacao iB = idispensacao.BuscarPorItem<ItemDispensacao>(1, i.LoteMedicamento.Codigo, i.DataAtendimento);
                        es.QuantidadeEstoque = es.QuantidadeEstoque + i.QtdDispensada;
                        //eBsn.AtualizarEstoque(es, iB, "remover");
                        iestoque.AtualizarEstoque(es, iB, "remover");
                    }
                    catch (Exception f)
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok)
                {
                    LimparSessaoPagina();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dispensação cancelada com sucesso!');location='Default.aspx';", true);
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A dispensação não pôde ser cancelada! Por favor, tente mais tarde.');", true);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dispensação cancelada com sucesso!');location='Default.aspx';", true);
        }
    }
}