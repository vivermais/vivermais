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
using System.Text;
using System.Collections;

namespace ViverMais.View.Vacina
{
    public partial class FormVacina : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_VACINA", Modulo.VACINA))
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    DropDown_Doenca.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
                    
                    IList<UnidadeMedidaVacina> listaunidademedida = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<UnidadeMedidaVacina>().OrderBy(um => um.Sigla).ToList();

                    DropDown_UnidadeMedida.DataSource = listaunidademedida;
                    DropDown_UnidadeMedida.DataBind();
                    DropDown_UnidadeMedida.Items.Insert(0, new ListItem("Selecione...", "0"));

                    DropDown_Fabricante.DataSource = Factory.GetInstance<IFabricanteVacina>().ListarTodos<FabricanteVacina>("Nome", true);
                    DropDown_Fabricante.DataBind();
                    DropDown_Fabricante.Items.Insert(0, new ListItem("Selecione...", "-1"));

                    Session.Remove("DoseVacina");
                    Session.Remove("doencasEvitadas");
                    Session.Remove("EstrategiasVacina");
                    Session.Remove("ItensVacina");

                    int temp;

                    if (Request["co_vacina"] != null && int.TryParse(Request["co_vacina"].ToString(), out temp))
                    {
                        try
                        {
                            ViverMais.Model.Vacina vacina = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<ViverMais.Model.Vacina>(temp);
                            TextBox_Nome.Text = vacina.Nome;
                            TextBox_Codigo.Text = vacina.CodigoCMADI;
                            DropDown_UnidadeMedida.SelectedValue = vacina.UnidadeMedida.Codigo.ToString();
                            CheckBox_PertenceAoCalendario.Checked = vacina.PertenceAoCalendario;
                            CheckBox_Ativo.Checked = vacina.Ativo == ViverMais.Model.Vacina.ATIVA;

                            IList<DoseVacina> dosesvacina = Factory.GetInstance<IVacina>().BuscarItensDoseVacina<ItemDoseVacina>(vacina.Codigo).Select(p => p.DoseVacina).ToList();
                            IList<Estrategia> estrategiasvacina = Factory.GetInstance<IEstrategiaImunobiologico>().BuscarPorVacina<Estrategia>(vacina.Codigo);
                            IList<ItemVacina> itensvacina = Factory.GetInstance<IItemVacina>().ListarPorVacina<ItemVacina>(vacina.Codigo);

                            this.CarregaDosesVacina(dosesvacina);
                            this.CarregaDoencasVacina(vacina.DoencasEvitadas);
                            this.CarregaEstrategiasVacina(estrategiasvacina);
                            this.CarregaItensVacina(itensvacina);
                        }
                        catch (Exception f)
                        {
                            throw f;
                        }
                    }
                    else
                    {
                        this.CarregaDosesVacina(new List<DoseVacina>());
                        this.CarregaDoencasVacina(new List<Doenca>());
                        this.CarregaEstrategiasVacina(new List<Estrategia>());
                        this.CarregaItensVacina(new List<ItemVacina>());
                    }
                }
            }
        }

        /// <summary>
        /// Carrega no DropDown somente as doses ainda não incluías
        /// </summary>
        void CarregaDosesVacina(IList<DoseVacina> dosesvacina)
        {
            IList<DoseVacina> listadose = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<DoseVacina>().OrderBy(d => d.NumeracaoDose).ToList();

            DropDown_Dose.DataSource = listadose.Where(p=> !dosesvacina.Select(p2=>p2.Codigo).ToList().Contains(p.Codigo)).ToList();
            DropDown_Dose.DataBind();

            DropDown_Dose.Items.Insert(0, new ListItem("Selecione...", "0"));

            Session["DoseVacina"] = dosesvacina;

            GridView_Dose.DataSource = dosesvacina;
            GridView_Dose.DataBind();
        }


        /// <summary>
        /// Carrega no DropDown somente as doses ainda não incluías
        /// </summary>
        void CarregaDoencasVacina(IList<Doenca> doencasvacina)
        {
            IList<Doenca> listaDoenca = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<Doenca>().OrderBy(d => d.Nome).ToList();

            DropDown_Doenca.Items.Clear();

            DropDown_Doenca.DataSource = listaDoenca.Where(p => !doencasvacina.Select(p2 => p2.Codigo).ToList().Contains(p.Codigo)).ToList();
            DropDown_Doenca.DataBind();

            DropDown_Doenca.Items.Insert(0, new ListItem("Selecione...", "0"));

            Session["doencasEvitadas"] = doencasvacina;

            GridView_Doenca.DataSource = doencasvacina;
            GridView_Doenca.DataBind();
        }

        private void CarregaEstrategiasVacina(IList<Estrategia> estrategiasvacina)
        {
            IList<Estrategia> estrategias = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<Estrategia>().OrderBy(d => d.Descricao).ToList();

            DropDownList_Estrategia.Items.Clear();

            DropDownList_Estrategia.DataSource = estrategias.Where(p => !estrategiasvacina.Select(p2 => p2.Codigo).ToList().Contains(p.Codigo)).ToList();
            DropDownList_Estrategia.DataBind();

            DropDownList_Estrategia.Items.Insert(0, new ListItem("Selecione...", "-1"));

            Session["EstrategiasVacina"] = estrategiasvacina;

            GridView_Estrategia.DataSource = estrategiasvacina;
            GridView_Estrategia.DataBind();
        }

        private void CarregaItensVacina(IList<ItemVacina> itensvacina)
        {
            Session["ItensVacina"] = itensvacina;
            
            GridView_ItemVacina.DataSource = itensvacina;
            GridView_ItemVacina.DataBind();
        }

        /// <summary>
        /// Salva o imunobiológico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Salvar(object sender, EventArgs e)
        {
            try
            {
                ViverMais.Model.Vacina vacina = Request["co_vacina"] != null ? Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<ViverMais.Model.Vacina>(int.Parse(Request["co_vacina"].ToString())) : new ViverMais.Model.Vacina();

                if (Factory.GetInstance<IVacina>().ValidarCadastroVacina(TextBox_Codigo.Text, vacina.Codigo))
                {
                    vacina.Nome = TextBox_Nome.Text.Trim().ToString();
                    vacina.UnidadeMedida = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<UnidadeMedidaVacina>(int.Parse(DropDown_UnidadeMedida.SelectedValue));
                    vacina.PertenceAoCalendario = CheckBox_PertenceAoCalendario.Checked;
                    vacina.Ativo = CheckBox_Ativo.Checked ? ViverMais.Model.Vacina.ATIVA : ViverMais.Model.Vacina.INATIVA;
                    vacina.CodigoCMADI = TextBox_Codigo.Text;
                    vacina.DoencasEvitadas = (IList<Doenca>)Session["doencasEvitadas"];

                    Factory.GetInstance<IVacina>().SalvarVacina<ViverMais.Model.Vacina, DoseVacina, Estrategia,ItemVacina>(vacina, (IList<DoseVacina>)Session["DoseVacina"],(IList<Estrategia>)Session["EstrategiasVacina"],(IList<ItemVacina>)Session["ItensVacina"],((Usuario)Session["Usuario"]).Codigo);

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Imunobiológico salvo com sucesso!');location='FormExibeVacina.aspx';", true);
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe um imunobiológico com este código! Por favor, informe outro código.');", true);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Método responsável por add uma dose
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button_Adicionar_OnClick(object sender, EventArgs e)
        {
            IList<DoseVacina> itens = Session["DoseVacina"] != null ? (IList<DoseVacina>)Session["DoseVacina"] : new List<DoseVacina>();
            DoseVacina item = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<DoseVacina>(int.Parse(DropDown_Dose.SelectedValue));

            if (!itens.Select(p=>p.Codigo).ToList().Contains(item.Codigo))
            {
                itens.Add(item);
                this.CarregaDosesVacina(itens);
            }
        }

        protected void OnClick_CancelarInclusaoItemVacina(object sender, EventArgs e)
        {
            DropDown_Fabricante.SelectedValue = "-1";
            TextBox_Aplicacao.Text = "";
        }

        protected void OnClick_AdicionarItemVacina(object sender, EventArgs e)
        {
            IList<ItemVacina> itens = (IList<ItemVacina>)Session["ItensVacina"];
            ItemVacina item = new ItemVacina();
            item.FabricanteVacina = Factory.GetInstance<IFabricanteVacina>().BuscarPorCodigo<FabricanteVacina>(int.Parse(DropDown_Fabricante.SelectedValue));
            item.Aplicacao = int.Parse(TextBox_Aplicacao.Text);

            ItemVacina itemexistente = itens.Where(p => p.FabricanteVacina.Codigo == item.FabricanteVacina.Codigo && p.Aplicacao == item.Aplicacao).FirstOrDefault();

            if (itemexistente != null)
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe um fabricante com a mesma quantidade de aplicação para esta vacina. Por favor, informe outros dados.');", true);
            else
            {
                itens.Add(item);
                //var ordem = from i in itens orderby i.FabricanteVacina.Nome, i.Aplicacao select i;
                this.CarregaItensVacina(itens);
                this.OnClick_CancelarInclusaoItemVacina(sender, e);
            }
        }

        /// <summary>
        ///  Método responsável por adicionar uma doenca ao gridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button_AdicionarDoenca_OnClick(object sender, EventArgs e)
        {
            IList<Doenca> doencasEvitadas = Session["doencasEvitadas"] != null ? (IList<Doenca>)Session["doencasEvitadas"] : new List<Doenca>();
            Doenca itemSelected = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<Doenca>(int.Parse(DropDown_Doenca.SelectedValue));

            if (!doencasEvitadas.Select(p => p.Codigo).Contains(itemSelected.Codigo))
            {
                doencasEvitadas.Add(itemSelected);
                this.CarregaDoencasVacina(doencasEvitadas);
            }
        }

        protected void OnClick_AdicionarEstrategia(object sender, EventArgs e)
        {
            IList<Estrategia> itens = Session["EstrategiasVacina"] != null ? (IList<Estrategia>)Session["EstrategiasVacina"] : new List<Estrategia>();
            Estrategia item = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<Estrategia>(int.Parse(DropDownList_Estrategia.SelectedValue));

            if (!itens.Select(p => p.Codigo).ToList().Contains(item.Codigo))
            {
                itens.Add(item);
                this.CarregaEstrategiasVacina(itens);
            }
        }

        protected void OnRowCommand_ExcluirDose(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CommandName_Excluir")
            {
                IList<DoseVacina> itens = (IList<DoseVacina>)Session["DoseVacina"];
                itens.RemoveAt(int.Parse(e.CommandArgument.ToString()));

                this.CarregaDosesVacina(itens);
            }
        }

        protected void OnRowCommand_ExcluirDoenca(object sender, GridViewCommandEventArgs e) 
        {
            if (e.CommandName == "CommandName_Excluir")
            {
                IList<Doenca> itens = (IList<Doenca>)Session["doencasEvitadas"];
                itens.RemoveAt(int.Parse(e.CommandArgument.ToString()));

                this.CarregaDoencasVacina(itens);
            }
        }

        protected void OnRowCommand_EstrategiaVacina(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CommandName_Excluir")
            {
                IList<Estrategia> itens = (IList<Estrategia>)Session["EstrategiasVacina"];
                itens.RemoveAt(int.Parse(e.CommandArgument.ToString()));

                this.CarregaEstrategiasVacina(itens);
            }
        }

        protected void OnRowCommand_ItemVacina(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CommandName_Excluir")
            {
                IList<ItemVacina> itens = (IList<ItemVacina>)Session["ItensVacina"];
                int co_item = int.Parse(GridView_ItemVacina.DataKeys[int.Parse(e.CommandArgument.ToString())]["Codigo"].ToString());

                if (!Factory.GetInstance<IItemVacina>().PermiteExclusao(co_item))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não é possível excluir este registro pois há lote(s) ligado(s) a este!');", true);
                else
                {
                    itens.RemoveAt(int.Parse(e.CommandArgument.ToString()));
                   // var ordem = from i in itens orderby i.FabricanteVacina.Nome, i.Aplicacao select i;
                    this.CarregaItensVacina(itens);
                }
            }
        }

        protected void OnRowDataBound_Dose(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btexcluir = (ImageButton)e.Row.Cells[1].Controls[0];
                btexcluir.OnClientClick = "javascript:return confirm('Usuário, ao excluir a dose desta vacina, caso exista alguma parametrização para a mesma, esta também será excluída. Deseja realmente prosseguir?');";
            }
        }
    }
}
