using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Movimentacao;

namespace ViverMais.View.Vacina
{
    public partial class FormDispensacaoDiferencial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "DISPENSACAO_DIFERENCIAL",Modulo.VACINA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    //Carrega a lista de itens da dispensação
                    Session["itensDispensacaoVacina"] = new List<ItemDispensacaoVacina>();

                    IList<ViverMais.Model.Vacina> vacinas = Factory.GetInstance<IVacina>().ListarTodos<ViverMais.Model.Vacina>();
                    ddlImunobiologico.Items.Add(new ListItem("Selecione...", "0"));
                    foreach (ViverMais.Model.Vacina vacina in vacinas)
                        ddlImunobiologico.Items.Add(new ListItem(vacina.Nome, vacina.Codigo.ToString()));

                    ViverMais.Model.Paciente paciente = (ViverMais.Model.Paciente)Session["pacienteSelecionado"];

                    //Desabilita a edição
                    if (Request.QueryString["id_dispensacao"] != null)
                    {
                        PanelTotal.Enabled = false;
                        //Carrega os campos para visualizacao
                        int codDispensacao = int.Parse(Request.QueryString["id_dispensacao"]);
                        DispensacaoVacina dispensacao = Factory.GetInstance<IVacina>().BuscarPorCodigo<DispensacaoVacina>(codDispensacao);
                        IList<ItemDispensacaoVacina> itensDispensacao = Factory.GetInstance<IDispensacao>().BuscarItensDispensacao<ItemDispensacaoVacina>(codDispensacao);
                        GridViewItensDispensacao.DataSource = itensDispensacao;
                        GridViewItensDispensacao.DataBind();
                        Session["itensDispensacaoVacina"] = itensDispensacao;
                    }
                }
            }
        }

        protected void rbtnResponsavel_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbxCartaoSus.Visible = true;
        }

        protected void ddlImunobiologico_SelectedIndexChanged(object sender, EventArgs e)
        {
            int co_vacina = int.Parse(ddlImunobiologico.SelectedValue);

            ddlDose.Items.Clear();
            ddlLote.Items.Clear();
            if (co_vacina != 0)
            {
                IList<LoteVacina> lotes = Factory.GetInstance<ILoteVacina>().BuscarLote<LoteVacina>("", DateTime.MinValue, co_vacina, -1,-1);
                if (lotes.Count == 0)
                {
                    ClientScript.RegisterClientScriptBlock(typeof(string), "erro", "<script>alert('Não há lote para esta vacina');</script>");
                    return;
                }
                ddlLote.Items.Add(new ListItem("Selecione...", "0"));
                foreach (LoteVacina lote in lotes)
                    ddlLote.Items.Add(new ListItem(lote.Identificacao, lote.Codigo.ToString()));

                //Retorna os tipos de dose e lotes para a vacina
                IList<ParametrizacaoVacina> parametrizacoes = Factory.GetInstance<IParametrizacaoVacina>().BuscarPorVacina<ParametrizacaoVacina>(co_vacina);
                ddlDose.Items.Add(new ListItem("Selecione...", "0"));
                if (parametrizacoes.Count != 0)
                {
                    foreach (ParametrizacaoVacina param in parametrizacoes)
                        ddlDose.Items.Add(new ListItem(param.ItemDoseVacina.DoseVacina.Descricao, param.ItemDoseVacina.Codigo.ToString()));
                }
                PanelDadosImunibiologico.Visible = true;
            }
        }

        protected void btnAddImunobiologico_Click(object sender, EventArgs e)
        {
            //Usuario usuario = (Usuario)Session["Usuario"];
            //SalaVacina sala = Factory.GetInstance<ISalaVacina>().BuscarPorUsuario<SalaVacina>(usuario.Codigo);

            //ItemDispensacaoVacina itemDispensacao = new ItemDispensacaoVacina();
            //int co_lotemedicamento = int.Parse(ddlLote.SelectedValue);

            //itemDispensacao.Lote = Factory.GetInstance<IVacina>().BuscarPorCodigo<LoteVacina>(co_lotemedicamento);
            //int co_dose = int.Parse(ddlDose.SelectedValue);
            //itemDispensacao.Dose = Factory.GetInstance<IVacina>().BuscarPorCodigo<DoseVacina>(co_dose);
            //itemDispensacao.AberturaAmpola = chkAberturaAmpola.Checked ? 'S' : 'N';

            //if (!ManipulaEstoque(itemDispensacao.Lote, sala, false))
            //{
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Não há Disponibilidade no estoque!');", true);
            //    return;
            //}

            ////Adiciona a lista do GridView de Itens da Dispensação
            //IList<ItemDispensacaoVacina> itens = (List<ItemDispensacaoVacina>)Session["itensDispensacaoVacina"];
            //itens.Add(itemDispensacao);
            //GridViewItensDispensacao.DataSource = itens;
            //GridViewItensDispensacao.DataBind();
            //Session["itensDispensacaoVacina"] = itens;

            ////Limpa os dados dos dropDownLists
            //ddlImunobiologico.SelectedValue = "0";
            //ddlDose.SelectedValue = "0";
            //ddlLote.SelectedValue = "0";
        }

        /// <summary>
        /// Função Responsável por verificar e retirar do estoque
        /// </summary>
        /// <param name="lote">Lote da vacina</param>
        /// <param name="sala">Sala a dispensar</param>
        /// <param name="retira">Variável que indica se deve retirar do estoque</param>
        /// <returns></returns>
        protected bool ManipulaEstoque(LoteVacina lote, SalaVacina sala, bool retiraDoEstoque)
        {
            bool existeEmEstoque = false;
            //IList<EstoqueVacina> itens = Factory.GetInstance<IEstoque>().BuscarItensEstoque<EstoqueVacina>(lote.Codigo, sala.Codigo);
            //foreach (EstoqueVacina item in itens)
            //{
            //    if (item.QuantidadeEstoque > 0)
            //    {
            //        existeEmEstoque = true;
            //        if (retiraDoEstoque)
            //        {
            //            item.QuantidadeEstoque--;
            //            Factory.GetInstance<IVacina>().Salvar(item);
            //        }
            //    }
            //}
            return existeEmEstoque;
        }


        /// <summary>
        /// Salva a Dispensação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSalvar_click(object sender, EventArgs e)
        {
            //DispensacaoVacina dispensacao = new DispensacaoVacina();
            //Usuario usuario = (Usuario)Session["Usuario"];
            //SalaVacina sala = Factory.GetInstance<ISalaVacina>().BuscarPorUsuario<SalaVacina>(usuario.Codigo);

            //ViverMais.Model.Paciente paciente = (ViverMais.Model.Paciente)Session["pacienteSelecionado"];

            //dispensacao.Paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(paciente.Codigo);
            //dispensacao.Data = DateTime.Now;

            //Factory.GetInstance<IVacina>().Inserir(dispensacao);

            ////Atualiza o estoque
            //IList<ItemDispensacaoVacina> itens = (List<ItemDispensacaoVacina>)Session["itensDispensacaoVacina"];
            //foreach (ItemDispensacaoVacina item in itens)
            //{
            //    item.Dispensacao = dispensacao;
            //    if (!ManipulaEstoque(item.Lote, sala, true))
            //    {
            //        ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Não há Disponibilidade no estoque!');", true);
            //        return;
            //    }
            //}
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "alert('Dados Salvos com Sucesso!');", true);
        }
    }
}
