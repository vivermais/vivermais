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
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using System.Text.RegularExpressions;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Vacina
{
    public partial class FormParametrizarVacina : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_PARAMETRIZACAO_VACINA", Modulo.VACINA))
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    IList<ViverMais.Model.Vacina> vacinas = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<ViverMais.Model.Vacina>("Nome", true);
                    DropDownList_Vacina.DataSource = vacinas;
                    DropDownList_Vacina.DataBind();

                    DropDownList_Vacina.Items.Insert(0, new ListItem("Selecione...", "-1"));

                    IList<ViverMais.Model.PropriedadeVacina> propriedades = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<ViverMais.Model.PropriedadeVacina>("Nome", true);
                    DropDownList_Propriedade.DataSource = propriedades;
                    DropDownList_Propriedade.DataBind();

                    foreach (string unidadetempo in Enum.GetNames(typeof(ParametrizacaoVacina.UNIDADE_TEMPO)).ToList())
                    {
                        DropDown_UnidadeTempoInicial.Items.Add(new ListItem(unidadetempo, ((int)Enum.Parse(typeof(ParametrizacaoVacina.UNIDADE_TEMPO), unidadetempo)).ToString()));
                        DropDown_UnidadeTempoFinal.Items.Add(new ListItem(unidadetempo, ((int)Enum.Parse(typeof(ParametrizacaoVacina.UNIDADE_TEMPO), unidadetempo)).ToString()));
                        DropDown_UnidadeTempo.Items.Add(new ListItem(unidadetempo, ((int)Enum.Parse(typeof(ParametrizacaoVacina.UNIDADE_TEMPO), unidadetempo)).ToString()));
                    }

                    this.CarregaGridParametrizacaoDefault();
                }
            }
        }

        protected void CarregaGridParametrizacaoDefault()
        {
            GridView_Parametrizacoes.DataSource = null;
            GridView_Parametrizacoes.EmptyDataText = "Selecione uma dose das vacinas existentes para visualizar suas parametrizações.";
            GridView_Parametrizacoes.DataBind();
        }

        protected void OnSelectedIndexChanged_Dose(object sender, EventArgs e)
        {
            DropDown_DosePrevista.Items.Clear();

            if (DropDownList_Dose.SelectedValue != "-1")
            {
                int co_itemdosevacina = int.Parse(DropDownList_Dose.SelectedValue);

                IList<ItemDoseVacina> itens = (IList<ItemDoseVacina>)Session["itensdosesvacinaparametrizacao"];
                //IList<ItemDoseVacina> itensproxima = (from item in itens
                //                                      where item.Codigo != co_itemdosevacina
                //                                      select item).ToList();
                ItemDoseVacina dose = itens.Where(p => p.Codigo == co_itemdosevacina).FirstOrDefault();

                IList<ItemDoseVacina> itensproxima = ParametrizacaoVacina.ProximasDosesParametrizacao(dose, itens);
                DropDown_DosePrevista.DataSource = itensproxima.OrderBy(p => p.DoseVacina.Descricao).ToList();
                DropDown_DosePrevista.DataBind();

                this.CarregaParametrizacoesDoseVacina(co_itemdosevacina);
            }
            else
                this.CarregaGridParametrizacaoDefault();

            DropDown_DosePrevista.Items.Insert(0, new ListItem("Selecione...", "-1"));
        }

        protected void OnSelectedIndexChanged_Vacina(object sender, EventArgs e)
        {
            DropDown_DosePrevista.Items.Clear();
            DropDown_DosePrevista.Items.Add(new ListItem("Selecione...", "-1"));

            DropDownList_Dose.Items.Clear();

            if (DropDownList_Vacina.SelectedValue != "-1")
            {
                IList<ItemDoseVacina> itens = Factory.GetInstance<IVacina>().BuscarItensDoseVacina<ItemDoseVacina>(int.Parse(DropDownList_Vacina.SelectedValue));

                DropDownList_Dose.DataSource = itens.OrderBy(p=>p.DoseVacina.Descricao).ToList();
                DropDownList_Dose.DataBind();
                
                Session["itensdosesvacinaparametrizacao"] = itens;
            }

            DropDownList_Dose.Items.Insert(0, new ListItem("Selecione...", "-1"));
            this.CarregaGridParametrizacaoDefault();
        }

        protected void LimparCampos()
        {
            TextBox_FaixaEtariaInicial.Text = "";
            TextBox_FaixaEtariaFinal.Text = "";
            TextBox_Tempo.Text = "";
            DropDown_UnidadeTempoInicial.SelectedValue = Convert.ToInt32(ParametrizacaoVacina.UNIDADE_TEMPO.ANOS).ToString();
            DropDown_UnidadeTempoFinal.SelectedValue = Convert.ToInt32(ParametrizacaoVacina.UNIDADE_TEMPO.ANOS).ToString();
            DropDown_UnidadeTempo.SelectedValue = Convert.ToInt32(ParametrizacaoVacina.UNIDADE_TEMPO.ANOS).ToString();
            DropDown_DosePrevista.SelectedValue = "-1";
        }

        void CarregaParametrizacoesDoseVacina(int co_itemdosevacina)
        {
            IList<ParametrizacaoVacina> parameters = Factory.GetInstance<IParametrizacaoVacina>().BuscarPorDoseVacina<ParametrizacaoVacina>(co_itemdosevacina);
            Session["parametrizacoesdosevacina"] = parameters;

            GridView_Parametrizacoes.DataSource = parameters;
            GridView_Parametrizacoes.EmptyDataText = "Não foi encontrada parametrização alguma para a dose da vacina selecionada.";
            GridView_Parametrizacoes.DataBind();
        }

        protected void OnClick_AdicionarParametrizacao(object sender, EventArgs e)
        {
            ItemDoseVacina itemdosevacina = Factory.GetInstance<IItemDoseVacina>().BuscarPorCodigo<ItemDoseVacina>(int.Parse(DropDownList_Dose.SelectedValue));
            IList<ParametrizacaoVacina> parametrizacoes = Factory.GetInstance<IParametrizacaoVacina>().BuscarPorDoseVacina<ParametrizacaoVacina>(itemdosevacina.Codigo);
            ParametrizacaoVacina parametrizacao = new ParametrizacaoVacina();

            parametrizacao.ItemDoseVacina = itemdosevacina;
            string validacao = string.Empty;

            if (TabContainer_Paramerizacao.ActiveTabIndex == 0) //PARAMETRIZAÇÃO POR FAIXA ETÁRIA
            {
                validacao = this.ValidarParametrizacaoPorFaixaEtaria();

                if (validacao.Equals("ok"))
                {
                    parametrizacao.FaixaEtariaInicial = float.Parse(TextBox_FaixaEtariaInicial.Text);
                    parametrizacao.FaixaEtariaFinal = float.Parse(TextBox_FaixaEtariaFinal.Text);
                    parametrizacao.UnidadeTempoInicial = int.Parse(DropDown_UnidadeTempoInicial.SelectedValue);
                    parametrizacao.UnidadeTempoFinal = int.Parse(DropDown_UnidadeTempoFinal.SelectedValue);
                    parametrizacao.Tipo = ParametrizacaoVacina.POR_FAIXA_ETARIA;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + validacao + "');", true);
                    return;
                }
            }
            else //PARAMETRIZAÇÃO POR EVENTO
            {
                validacao = this.ValidarParametrizacaoPorEvento();

                if (validacao.Equals("ok"))
                {
                    parametrizacao.Propriedade = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<PropriedadeVacina>(int.Parse(DropDownList_Propriedade.SelectedValue));
                    parametrizacao.FaixaEtariaInicial = float.Parse(TextBox_Tempo.Text);
                    parametrizacao.UnidadeTempoInicial = int.Parse(DropDown_UnidadeTempo.SelectedValue);
                    parametrizacao.ProximaDose = Factory.GetInstance<IItemDoseVacina>().BuscarPorCodigo<ItemDoseVacina>(int.Parse(DropDown_DosePrevista.SelectedValue));
                    parametrizacao.Tipo = ParametrizacaoVacina.POR_EVENTO;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + validacao + "');", true);
                    return;
                }
            }

            if ((from parametro in parametrizacoes
                 where parametro.ToString().Equals(parametrizacao.ToString())
                 select parametro).FirstOrDefault() != null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe um parâmetro com estas informações!');", true);
                return;
            }

            Factory.GetInstance<IVacinaServiceFacade>().Salvar(parametrizacao);
            Factory.GetInstance<IVacinaServiceFacade>().Inserir(new LogVacina(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 16, "id parametro: " + parametrizacao.Codigo.ToString()));
            this.CarregaParametrizacoesDoseVacina(itemdosevacina.Codigo);
            this.LimparCampos();
        }

        private string ValidarParametrizacaoPorFaixaEtaria()
        {
            Regex regex = new Regex(@"^\d*$");

            if (string.IsNullOrEmpty(TextBox_FaixaEtariaInicial.Text))
                return "Faixa Etária Inicial é Obrigatório.";

            if (!regex.IsMatch(TextBox_FaixaEtariaInicial.Text))
                return "Digite somente números inteiros na Faixa Etária Inicial.";

            if (string.IsNullOrEmpty(TextBox_FaixaEtariaFinal.Text))
                return "Faixa Etária Final é Obrigatório.";

            if (!regex.IsMatch(TextBox_FaixaEtariaFinal.Text))
                return "Digite somente números inteiros na Faixa Etária Final.";

            if (ParametrizacaoVacina.ConverteUnidadeTempo(float.Parse(TextBox_FaixaEtariaInicial.Text), int.Parse(DropDown_UnidadeTempoInicial.SelectedValue), Convert.ToInt32(ParametrizacaoVacina.UNIDADE_TEMPO.HORAS)) >
                ParametrizacaoVacina.ConverteUnidadeTempo(float.Parse(TextBox_FaixaEtariaFinal.Text), int.Parse(DropDown_UnidadeTempoFinal.SelectedValue), Convert.ToInt32(ParametrizacaoVacina.UNIDADE_TEMPO.HORAS)))
                return "O início da faixa etária não pode ser maior que o final desta!";

            return "ok";
        }

        private string ValidarParametrizacaoPorEvento()
        {
            Regex regex = new Regex(@"^\d*$");

            if (string.IsNullOrEmpty(TextBox_Tempo.Text))
                return "Informe a quantidade de tempo para o evento!";

            if (!regex.IsMatch(TextBox_Tempo.Text))
                return "Digite somente números inteiros na quantidade de tempo para o evento.";

            if (DropDown_DosePrevista.SelectedValue == "-1")
                return "Selecione a próxima dose para o evento!";

            return "ok";
        }

        protected void OnRowDeleting_Parametrizacao(object sender, GridViewDeleteEventArgs e)
        {
            int co_itemdosevacina = int.Parse(GridView_Parametrizacoes.DataKeys[e.RowIndex]["Codigo"].ToString());
            ParametrizacaoVacina parametrizacao = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<ParametrizacaoVacina>(co_itemdosevacina);
            Factory.GetInstance<IVacinaServiceFacade>().Deletar(parametrizacao);
            Factory.GetInstance<IVacinaServiceFacade>().Inserir(new LogVacina(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 17, "id vacina: " + parametrizacao.ItemDoseVacina.Vacina.Codigo + ", id dose: " + parametrizacao.ItemDoseVacina.DoseVacina.Codigo + ", parametrizacao: " + parametrizacao.ToString()));

            this.CarregaParametrizacoesDoseVacina(int.Parse(DropDownList_Dose.SelectedValue));
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Parametrização excluída com sucesso.');", true);
        }

        protected void OnPageIndexChanging_Parametrizacao(object sender, GridViewPageEventArgs e)
        {
            IList<ParametrizacaoVacina> parametrizacoes = (IList<ParametrizacaoVacina>)Session["parametrizacoesdosevacina"];
            GridView_Parametrizacoes.DataSource = parametrizacoes;
            GridView_Parametrizacoes.DataBind();

            GridView_Parametrizacoes.PageIndex = e.NewPageIndex;
            GridView_Parametrizacoes.DataBind();
        }

        protected void OnRowDataBound_Parametrizacao(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbexcluir = (LinkButton)e.Row.Controls[1].Controls[0];
                lbexcluir.OnClientClick = "javascript:return confirm('Tem certeza que deseja excluir este parametrização ?');";
            }
        }
    }
}
