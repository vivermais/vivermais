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
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;

namespace ViverMais.View.Agendamento
{
    public partial class FormDefinirFaixaProcedimento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Conexão com o Web Service
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "PARAMETRIZACAO_FAIXA_PROCEDIMENTO", Modulo.AGENDAMENTO))
                {
                    // Os ListBox ficam desabilitados
                    lbxUnidade.Visible = false;
                    lbxProcedimento.Visible = false;

                    //Preenche o Combo Faixa

                    IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
                    IList<ViverMais.Model.FaixaProcedimento> faixaprocedimento = Factory.GetInstance<IFaixaProcedimento>().ListarTodos<FaixaProcedimento>();
                    //IList<ViverMais.Model.Faixa> faixas = iAgendamento.ListarTodos<ViverMais.Model.Faixa>();
                    IList<ViverMais.Model.Faixa> faixas = Factory.GetInstance<IFaixaProcedimento>().BuscaFaixa<Faixa, IList<FaixaProcedimento>>(faixaprocedimento);
                    ddlFaixa.Items.Add(new ListItem("Selecione...", "0"));
                    foreach (ViverMais.Model.Faixa faixa in faixas)
                    {
                        ddlFaixa.Items.Add(new ListItem(faixa.FaixaInicial.ToString() + " - " + faixa.FaixaFinal.ToString(), faixa.Codigo.ToString()));
                    }
                    ListarFaixasDeProcedimento(faixaprocedimento);

                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }

        }

        public void ListarFaixasDeProcedimento(IList<FaixaProcedimento> faixasProcedimento)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Codigo");
            table.Columns.Add("Procedimento");
            table.Columns.Add("Unidade");
            table.Columns.Add("Validade_Inicial");
            table.Columns.Add("Validade_Final");
            table.Columns.Add("Faixa");
            if (faixasProcedimento.Count != 0)
            {
                foreach (FaixaProcedimento faixaProcedimento in faixasProcedimento)
                {
                    DataRow row = table.NewRow();
                    row[0] = faixaProcedimento.Faixa.Codigo.ToString();
                    row[1] = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(faixaProcedimento.Id_Procedimento);
                    row[2] = Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(faixaProcedimento.Id_Unidade);
                    row[3] = faixaProcedimento.ValidadeInicial.ToString("dd/MM/yyyy");
                    row[4] = faixaProcedimento.ValidadeFinal.ToString("dd/MM/yyyy");
                    row[5] = faixaProcedimento.Faixa.FaixaInicial.ToString() + " - " + faixaProcedimento.Faixa.FaixaFinal.ToString();
                    table.Rows.Add(row);
                    GridViewFaixaProcedimento.DataSource = table;
                    GridViewFaixaProcedimento.DataBind();
                }
                lblSemRegistro.Visible = false;
            }
            else
            {
                lblSemRegistro.Visible = true;
            }
            GridViewFaixaProcedimento.DataSource = table;
            GridViewFaixaProcedimento.DataBind();
        }

        protected void GridViewFaixaProcedimento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {

                int id_faixa = Convert.ToInt32(e.CommandArgument);
                FaixaProcedimento faixaProcedimento = Factory.GetInstance<IFaixaProcedimento>().BuscarDeProcedimento<FaixaProcedimento>(id_faixa);
                Factory.GetInstance<IAgendamentoServiceFacade>().Deletar(faixaProcedimento);

                //IList<FaixaProcedimento> faixasProcedimento = Factory.GetInstance<IFaixaProcedimento>().ListarTodos<FaixaProcedimento>();
                IList<ViverMais.Model.FaixaProcedimento> faixaprocedimento = Factory.GetInstance<IFaixaProcedimento>().ListarTodos<FaixaProcedimento>();
                //IList<ViverMais.Model.Faixa> faixas = iAgendamento.ListarTodos<ViverMais.Model.Faixa>();
                ddlFaixa.Items.Clear();
                IList<ViverMais.Model.Faixa> faixas = Factory.GetInstance<IFaixaProcedimento>().BuscaFaixa<Faixa, IList<FaixaProcedimento>>(faixaprocedimento);
                ddlFaixa.Items.Add(new ListItem("Selecione...", "0"));
                foreach (ViverMais.Model.Faixa faixa in faixas)
                {
                    ddlFaixa.Items.Add(new ListItem(faixa.FaixaInicial.ToString() + " - " + faixa.FaixaFinal.ToString(), faixa.Codigo.ToString()));
                }
                ListarFaixasDeProcedimento(faixaprocedimento);
                //Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 2, "Nome Feriado:" + feriado.Descricao));

                //GridViewListaFeriados.DataSource = feriados;
                //GridViewListaFeriados.DataBind();
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            RequiredFieldValidator3.Enabled = false;
            //Limpa o ListBox
            lbxProcedimento.Items.Clear();

            IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();
            IList<ViverMais.Model.Procedimento> procedimentos = iProcedimento.BuscarPorNome<ViverMais.Model.Procedimento>(tbxProcedimento.Text.ToUpper());
            if (procedimentos.Count == 0)
            {
                tbxCodigo.Text = "";
                tbxProcedimento.Text = "";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('PROCEDIMENTO NÃO EXISTE!.');location='FormDefinirFaixaProcedimento.aspx';", true);
                //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Não existe procedimento com esse nome!'); window.location='FormDefinirFaixaProcedimento.aspx'</script>");
                //             ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert","alert('Informe o Nº Patrimonial!');", true);
                                return;
              //  tbxCodigo.Text = "";
              //  tbxProcedimento.Text = "PROCEDIMENTO NÃO EXISTE";
            }
            else
            {
                lbxProcedimento.Visible = true;

                foreach (ViverMais.Model.Procedimento procedimento in procedimentos)
                {
                    // preenche o ListBox
                    ListItem item = new ListItem();
                    item.Text = procedimento.Nome;
                    item.Value = procedimento.Codigo;
                    lbxProcedimento.Items.Add(item);
                }
            }
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();
            ViverMais.Model.Procedimento procedimentos = iProcedimento.BuscarProcedimentoAPAC<ViverMais.Model.Procedimento>(lbxProcedimento.SelectedValue);
            if (procedimentos == null)
            {
                //tbxProcedimento.Text = "PROCEDIMENTO INFORMADO NÃO É APAC";
                lbxProcedimento.Visible = false;
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('PROCEDIMENTO INFORMADO NÃO É APAC!.');location='FormDefinirFaixaProcedimento.aspx';", true);
                //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Não existe procedimento com esse código!'); window.location='FormParametrizarProcedimento.aspx'</script>");
                return;
            }

            tbxCodigo.Text = lbxProcedimento.SelectedValue;
            tbxProcedimento.Text = lbxProcedimento.SelectedItem.Text;
            lbxProcedimento.Visible = false;
        }

        protected void tbxCodigo_TextChanged(object sender, EventArgs e)
        {

            // Cria uma matriz de procedimento
            IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();
            ViverMais.Model.Procedimento procedimentos = iProcedimento.BuscarProcedimentoAPAC<ViverMais.Model.Procedimento>(tbxCodigo.Text);
            if (procedimentos == null)
            {
//                tbxProcedimento.Text = "PROCEDIMENTO INFORMADO NÃO É APAC OU  NÃO EXISTE";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('PROCEDIMENTO INFORMADO NÃO É APAC OU  NÃO EXISTE!.');location='FormDefinirFaixaProcedimento.aspx';", true);
                //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Não existe procedimento com esse código!'); window.location='FormParametrizarProcedimento.aspx'</script>");
                return;
            }
            tbxCodigo.Text = procedimentos.Codigo;
            tbxProcedimento.Text = procedimentos.Nome;

        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            // Pesquisar por uma parte do nome da Unidade
            IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();
            IList<ViverMais.Model.EstabelecimentoSaude> estabelecimentos = iEstabelecimento.BuscarEstabelecimentoPorNomeFantasia<ViverMais.Model.EstabelecimentoSaude>(tbxUnidade.Text);
            if (estabelecimentos == null)
            {
                tbxCnes.Text = "";
                tbxUnidade.Text = "UNIDADE NÃO EXISTE";

            }
            else
            {
                lbxUnidade.Visible = true;
                foreach (ViverMais.Model.EstabelecimentoSaude estabelecimento in estabelecimentos)
                {
                    // preenche o ListBox
                    ListItem item = new ListItem();
                    item.Text = estabelecimento.NomeFantasia;
                    item.Value = estabelecimento.CNES;
                    lbxUnidade.Items.Add(item);
                }
            }

        }

        protected void tbxCnes_TextChanged(object sender, EventArgs e)
        {
            IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();
            ViverMais.Model.EstabelecimentoSaude estabelecimento = iEstabelecimento.BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(tbxCnes.Text.Trim());
            if (estabelecimento == null)
            {
                tbxCodigo.Text = string.Empty;
                tbxUnidade.Text = "Estabelecimento Não Localizado!";
                return;
            }
            tbxCnes.Text = estabelecimento.CNES;
            tbxUnidade.Text = estabelecimento.NomeFantasia;
            Session["unidade"] = estabelecimento.CNES;
        }

        protected void Incluir_Click(object sender, EventArgs e)
        {
            TipoProcedimento tipoprocedimento = Factory.GetInstance<ITipoProcedimento>().BuscaTipoProcedimento<TipoProcedimento>(tbxCodigo.Text.Trim());
            if (tipoprocedimento == null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Procedimento não parametrizado!.');location='FormDefinirFaixaProcedimento.aspx';", true);
                return;
            }
            FaixaProcedimento faixaprocedimento = new FaixaProcedimento();
            faixaprocedimento.Faixa = Factory.GetInstance<IAgendamentoServiceFacade>().BuscarPorCodigo<Faixa>(int.Parse(ddlFaixa.SelectedValue));
            faixaprocedimento.Id_Unidade = tbxCnes.Text.Trim();
            faixaprocedimento.Id_Procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(tbxCodigo.Text.Trim()).Codigo;
            //if (Factory.GetInstance<IFaixaProcedimento>().BuscaFaixaDeProcedimento<FaixaProcedimento>(faixaprocedimento.Faixa.Codigo, tbxCodigo.Text.Trim(), tbxCnes.Text.Trim()) != null)
            //{
            //    ClientScript.RegisterClientScriptBlock(typeof(String),"ok","<script>alert('Já Existe Faixa de Procedimento cadastrada com Estes Parametros!');window.location='FormDefinirFaixaProcedimento.aspx';</script>");
            //    return;
            //}

            faixaprocedimento.ValidadeInicial = DateTime.Parse(tbxDataInicial.Text);
            faixaprocedimento.ValidadeFinal = DateTime.Parse(tbxDataFinal.Text);
            Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(faixaprocedimento);
            Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 39, "ID:"+ faixaprocedimento.Faixa.Codigo));
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados Salvos com Sucesso!.');location='FormDefinirFaixaProcedimento.aspx';", true);
            ListarFaixasDeProcedimento(Factory.GetInstance<IFaixaProcedimento>().ListarTodos<FaixaProcedimento>());
            return;
        }

        protected void lbxUnidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            ViverMais.Model.EstabelecimentoSaude estabelecimento = iViverMais.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(lbxUnidade.SelectedValue);
            tbxCnes.Text = estabelecimento.CNES;
            tbxUnidade.Text = lbxUnidade.SelectedItem.Text;
            lbxUnidade.Visible = false;
        }
    }
}
