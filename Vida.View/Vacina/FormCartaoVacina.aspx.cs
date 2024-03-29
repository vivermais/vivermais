﻿using System;
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
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Vacina
{
    public partial class FormCartaoVacina : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_CARTAO_VACINA", Modulo.VACINA))
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    ViverMais.Model.Paciente paciente = (ViverMais.Model.Paciente)Session["pacienteSelecionado"];
                    if (paciente != null)
                    {
                        this.CarregaCampos();
                        ViewState["co_paciente"] = paciente.Codigo;
                        this.CarregaHistoricoCartaoVacina(paciente.Codigo);
                    }
                    else
                        Response.Redirect("FormPesquisaPaciente.aspx");
                }
            }
        }

        void CarregaCampos()
        {
            IList<DoseVacina> doses = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<DoseVacina>("NumeracaoDose", true);
            DropDown_Dose.DataSource = doses;
            DropDown_Dose.DataBind();
            DropDown_Dose.Items.Insert(0, new ListItem("Selecione...", "-1"));

            IList<ViverMais.Model.Vacina> vacinas = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<ViverMais.Model.Vacina>("Nome", true);
            DropDown_Vacina.DataSource = vacinas;
            DropDown_Vacina.DataBind();
            DropDown_Vacina.Items.Insert(0, new ListItem("Selecione...", "-1"));
        }

        void CarregaHistoricoCartaoVacina(string co_paciente)
        {
            Hashtable hash = Factory.GetInstance<ICartaoVacina>().RetornaCartoesPaciente(co_paciente);
            IList<CartaoVacina> cartaocrianca = (IList<CartaoVacina>)hash["cartaocrianca"];
            IList<CartaoVacina> cartaoadolescente = (IList<CartaoVacina>)hash["cartaoadolescente"];
            IList<CartaoVacina> cartaoadulto = (IList<CartaoVacina>)hash["cartaoadulto"];
            IList<CartaoVacina> cartaohistorico = (IList<CartaoVacina>)hash["cartaohistorico"];

            Session["cartaocrianca"] = cartaocrianca;
            this.CarregaGridViewCartao(this.GridView_CartaoVacina, cartaocrianca);
            //GridView_CartaoVacina.DataSource = cartaocrianca;
            //GridView_CartaoVacina.DataBind();

            Session["cartaoadolescente"] = cartaoadolescente;
            this.CarregaGridViewCartao(this.GridView_CartaoVacinaAdolescente, cartaoadolescente);
            //GridView_CartaoVacinaAdolescente.DataSource = cartaoadolescente;
            //GridView_CartaoVacinaAdolescente.DataBind();

            Session["cartaoadulto"] = cartaoadulto;
            this.CarregaGridViewCartao(this.GridView_CartaoVacinaAdulto, cartaoadulto);
            //GridView_CartaoVacinaAdulto.DataSource = cartaoadulto;
            //GridView_CartaoVacinaAdulto.DataBind();

            Session["cartaohistorico"] = cartaohistorico;
            this.CarregaGridViewCartao(this.GridView_HistoricoVacinacao, cartaohistorico);
            //GridView_HistoricoVacinacao.DataSource = cartaohistorico;
            //GridView_HistoricoVacinacao.DataBind();
        }

        private void CarregaGridViewCartao(GridView grid, IList<CartaoVacina> cartoes)
        {
            grid.DataSource = cartoes;
            grid.DataBind();
        }

        protected void OnRowCommand_GridView_CartaoVacina(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CommandName_VerInformacoes")
            {
                int co_vacina = int.Parse(GridView_CartaoVacina.DataKeys[int.Parse(e.CommandArgument.ToString())][0].ToString());
                int co_dose = int.Parse(GridView_CartaoVacina.DataKeys[int.Parse(e.CommandArgument.ToString())][1].ToString());
                this.CarregaInformacoesDoseVacina(co_vacina, co_dose);
                Panel_Info.Visible = true;
            }
        }

        protected void OnRowCommand_GridView_CartaoVacinaAdolescente(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CommandName_VerInformacoes")
            {
                int co_vacina = int.Parse(GridView_CartaoVacinaAdolescente.DataKeys[int.Parse(e.CommandArgument.ToString())][0].ToString());
                int co_dose = int.Parse(GridView_CartaoVacinaAdolescente.DataKeys[int.Parse(e.CommandArgument.ToString())][1].ToString());
                this.CarregaInformacoesDoseVacina(co_vacina, co_dose);
                Panel_Info.Visible = true;
            }
        }

        protected void OnRowCommand_GridView_CartaoVacinaAdulto(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CommandName_VerInformacoes")
            {
                int co_vacina = int.Parse(GridView_CartaoVacinaAdulto.DataKeys[int.Parse(e.CommandArgument.ToString())][0].ToString());
                int co_dose = int.Parse(GridView_CartaoVacinaAdulto.DataKeys[int.Parse(e.CommandArgument.ToString())][1].ToString());
                this.CarregaInformacoesDoseVacina(co_vacina, co_dose);
                Panel_Info.Visible = true;
            }
        }

        protected void OnRowCommand_GridView_HistoricoVacinacao(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CommandName_VerInformacoes")
            {
                CartaoVacina cartao = Factory.GetInstance<ICartaoVacina>().BuscarPorCodigo<CartaoVacina>(long.Parse(GridView_HistoricoVacinacao.DataKeys[int.Parse(e.CommandArgument.ToString())]["Codigo"].ToString()));
                this.CarregaInformacoesDoseVacina(cartao.Vacina.Codigo, cartao.DoseVacina.Codigo);
                Panel_Info.Visible = true;
            }
        }

        protected void OnRowDeleting_HistoricoVacinacao(object sender, GridViewDeleteEventArgs e)
        {
            ICartaoVacina icartao = Factory.GetInstance<ICartaoVacina>();
            CartaoVacina cartao = icartao.BuscarPorCodigo<CartaoVacina>(long.Parse(GridView_HistoricoVacinacao.DataKeys[e.RowIndex]["Codigo"].ToString()));
            icartao.Deletar(cartao);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Vacinação excluída com sucesso.');", true);

            IList<CartaoVacina> cartoeshistorico = (IList<CartaoVacina>)Session["cartaohistorico"];
            cartoeshistorico.RemoveAt(CartaoVacina.RetornaIndexCartao(cartoeshistorico,cartao.Codigo));
            Session["cartaohistorico"] = cartoeshistorico;
            this.CarregaGridViewCartao(this.GridView_HistoricoVacinacao, cartoeshistorico);

            IList<CartaoVacina> cartoescrianca = (IList<CartaoVacina>)Session["cartaocrianca"];
            cartoescrianca = CartaoVacina.RemoverCartaoCalendario(cartoescrianca, cartao);
            Session["cartaocrianca"] = cartoescrianca;
            this.CarregaGridViewCartao(this.GridView_CartaoVacina, cartoescrianca);

            IList<CartaoVacina> cartoesadolescente = (IList<CartaoVacina>)Session["cartaoadolescente"];
            cartoesadolescente = CartaoVacina.RemoverCartaoCalendario(cartoesadolescente, cartao);
            Session["cartaoadolescente"] = cartoesadolescente;
            this.CarregaGridViewCartao(this.GridView_CartaoVacinaAdolescente, cartoesadolescente);

            IList<CartaoVacina> cartoesadulto = (IList<CartaoVacina>)Session["cartaoadulto"];
            cartoesadulto = CartaoVacina.RemoverCartaoCalendario(cartoesadulto, cartao);
            Session["cartaoadulto"] = cartoesadulto;
            this.CarregaGridViewCartao(this.GridView_CartaoVacinaAdulto, cartoesadulto);

            //this.CarregaHistoricoCartaoVacina(ViewState["co_paciente"].ToString());
            Factory.GetInstance<IVacinaServiceFacade>().Inserir(new LogVacina(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 23, "co_paciente: " + cartao.Paciente.Codigo + ", vacina: " + cartao.Vacina.Nome + ", dose: " + cartao.DoseVacina.Descricao + ", data aplicacao: " + (cartao.DataAplicacao.HasValue ? cartao.DataAplicacao.Value.ToString("dd/MM/yyyy") : "Data de Aplicação não informada.")));
        }

        private void CarregaInformacoesDoseVacina(int co_vacina, int co_dose)
        {
            ViverMais.Model.Vacina vacina = Factory.GetInstance<IVacina>().BuscarPorCodigo<ViverMais.Model.Vacina>(co_vacina);
            DoseVacina dose = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<DoseVacina>(co_dose);
            IList<ParametrizacaoVacina> parametros = Factory.GetInstance<IParametrizacaoVacina>().BuscarPorDoseVacina<ParametrizacaoVacina>(co_dose, co_vacina);

            Label_InfoVacina.Text = vacina.Nome;
            Label_InfoDose.Text = dose.Descricao;

            string info = string.Empty;
            if (vacina.DoencasEvitadas.Count > 0)
            {
                int i = 1;
                foreach (Doenca d in vacina.DoencasEvitadas)
                {
                    info += i.ToString() + ". " + d.Nome.ToUpper() + "<br/>";
                    i++;
                }
            }
            else
                info += "Nenhum registro encontrado <br/>";

            Literal_InfoDoencas.Text = info;
            Literal_InfoDoencas.DataBind();

            info = string.Empty;
            if (parametros.Count > 0)
            {
                int i = 1;
                foreach (ParametrizacaoVacina p in parametros)
                {
                    info += i.ToString() + ". " + p.ToString().ToUpper() + "<br/>";
                    i++;
                }
            }
            else
                info += "Nenhum registro encontrado";

            Literal_InfoParametros.Text = info;
            Literal_InfoParametros.DataBind();
        }

        protected void OnPageIndexChanging_CartaoCrianca(object sender, GridViewPageEventArgs e)
        {
            GridView_CartaoVacina.DataSource = (IList<CartaoVacina>)Session["cartaocrianca"];
            GridView_CartaoVacina.DataBind();

            GridView_CartaoVacina.PageIndex = e.NewPageIndex;
            GridView_CartaoVacina.DataBind();
        }

        protected void OnPageIndexChanging_CartaoAdolescente(object sender, GridViewPageEventArgs e)
        {
            GridView_CartaoVacinaAdolescente.DataSource = (IList<CartaoVacina>)Session["cartaoadolescente"];
            GridView_CartaoVacinaAdolescente.DataBind();

            GridView_CartaoVacinaAdolescente.PageIndex = e.NewPageIndex;
            GridView_CartaoVacinaAdolescente.DataBind();
        }

        protected void OnPageIndexChanging_CartaoAdulto(object sender, GridViewPageEventArgs e)
        {
            GridView_CartaoVacinaAdulto.DataSource = (IList<CartaoVacina>)Session["cartaoadulto"];
            GridView_CartaoVacinaAdulto.DataBind();

            GridView_CartaoVacinaAdulto.PageIndex = e.NewPageIndex;
            GridView_CartaoVacinaAdulto.DataBind();
        }

        protected void OnPageIndexChanging_Historico(object sender, GridViewPageEventArgs e)
        {
            GridView_HistoricoVacinacao.DataSource = (IList<CartaoVacina>)Session["cartaohistorico"];
            GridView_HistoricoVacinacao.DataBind();

            GridView_HistoricoVacinacao.PageIndex = e.NewPageIndex;
            GridView_HistoricoVacinacao.DataBind();
        }

        protected void OnRowEditing_CartaoVacina(object sender, GridViewEditEventArgs e)
        {
            int co_vacina = int.Parse(GridView_CartaoVacina.DataKeys[e.NewEditIndex][0].ToString());
            int co_dose = int.Parse(GridView_CartaoVacina.DataKeys[e.NewEditIndex][1].ToString());
            ViverMais.Model.Vacina vacina = Factory.GetInstance<IVacina>().BuscarPorCodigo<ViverMais.Model.Vacina>(co_vacina);
            DoseVacina dose = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<DoseVacina>(co_dose);

            DropDown_Dose.Visible = false;
            DropDown_Vacina.Visible = false;
            Label_Imunobiologico.Visible = true;
            Label_DoseVacina.Visible = true;

            Label_Imunobiologico.Text = vacina.Nome;
            Label_DoseVacina.Text = dose.Descricao;

            ViewState["CodigoVacina"] = co_vacina;
            ViewState["CodigoDose"] = co_dose;

            Panel_AtualizarCartao.Visible = true;
        }

        protected void OnRowEditing_CartaoAdulto(object sender, GridViewEditEventArgs e)
        {
            int co_vacina = int.Parse(GridView_CartaoVacinaAdulto.DataKeys[e.NewEditIndex][0].ToString());
            int co_dose = int.Parse(GridView_CartaoVacinaAdulto.DataKeys[e.NewEditIndex][1].ToString());
            ViverMais.Model.Vacina vacina = Factory.GetInstance<IVacina>().BuscarPorCodigo<ViverMais.Model.Vacina>(co_vacina);
            DoseVacina dose = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<DoseVacina>(co_dose);

            DropDown_Dose.Visible = false;
            DropDown_Vacina.Visible = false;
            Label_Imunobiologico.Visible = true;
            Label_DoseVacina.Visible = true;

            Label_Imunobiologico.Text = vacina.Nome;
            Label_DoseVacina.Text = dose.Descricao;

            ViewState["CodigoVacina"] = co_vacina;
            ViewState["CodigoDose"] = co_dose;

            Panel_AtualizarCartao.Visible = true;
        }

        protected void OnRowEditing_CartaoAdolescente(object sender, GridViewEditEventArgs e)
        {
            int co_vacina = int.Parse(GridView_CartaoVacinaAdolescente.DataKeys[e.NewEditIndex][0].ToString());
            int co_dose = int.Parse(GridView_CartaoVacinaAdolescente.DataKeys[e.NewEditIndex][1].ToString());
            ViverMais.Model.Vacina vacina = Factory.GetInstance<IVacina>().BuscarPorCodigo<ViverMais.Model.Vacina>(co_vacina);
            DoseVacina dose = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<DoseVacina>(co_dose);

            DropDown_Dose.Visible = false;
            DropDown_Vacina.Visible = false;
            Label_Imunobiologico.Visible = true;
            Label_DoseVacina.Visible = true;

            Label_Imunobiologico.Text = vacina.Nome;
            Label_DoseVacina.Text = dose.Descricao;

            ViewState["CodigoVacina"] = co_vacina;
            ViewState["CodigoDose"] = co_dose;

            Panel_AtualizarCartao.Visible = true;
        }

        protected void OnRowDataBound_CartaoAdulto(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!string.IsNullOrEmpty(e.Row.Cells[2].Text) && !e.Row.Cells[2].Text.Equals("&nbsp;"))
                    e.Row.Cells[7].Controls[0].Visible = false; //Não mostrar botão de atualizar, pois já houve aplicação da dose e vacina para o paciente selecionado
            }
        }

        protected void OnRowDataBound_CartaoCrianca(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!string.IsNullOrEmpty(e.Row.Cells[2].Text) && !e.Row.Cells[2].Text.Equals("&nbsp;"))
                    e.Row.Cells[7].Controls[0].Visible = false; //Não mostrar botão de atualizar, pois já houve aplicação da dose e vacina para o paciente selecionado
            }
        }

        protected void OnRowDataBound_CartaoAdolescente(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!string.IsNullOrEmpty(e.Row.Cells[2].Text) && !e.Row.Cells[2].Text.Equals("&nbsp;"))
                    e.Row.Cells[7].Controls[0].Visible = false; //Não mostrar botão de atualizar, pois já houve aplicação da dose e vacina para o paciente selecionado
            }
        }

        protected void OnClick_Salvar(object sender, EventArgs e)
        {
            int co_vacina;
            int co_dose;

            if (Label_Imunobiologico.Visible)
            {
                co_vacina = int.Parse(ViewState["CodigoVacina"].ToString());
                co_dose = int.Parse(ViewState["CodigoDose"].ToString());
            }
            else
            {
                co_vacina = int.Parse(DropDown_Vacina.SelectedValue);
                co_dose = int.Parse(DropDown_Dose.SelectedValue);
            }

            ViverMais.Model.Paciente paciente = (ViverMais.Model.Paciente)Session["pacienteSelecionado"];

            CartaoVacina cartao = new CartaoVacina();
            cartao.DoseVacina = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<DoseVacina>(co_dose);
            cartao.Vacina = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<ViverMais.Model.Vacina>(co_vacina);
            cartao.DataAplicacao = DateTime.Parse(TextBox_Data.Text);
            cartao.Lote = TextBox_Lote.Text;
            cartao.ValidadeLote = DateTime.Parse(TextBox_ValidadeLote.Text);
            cartao.Local = TextBox_Local.Text;
            cartao.Motivo = TextBox_Motivo.Text;
            cartao.Paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(paciente.Codigo);

            bool atualizacaovalida = Factory.GetInstance<ICartaoVacina>().ValidarAtualizacaoCartaoVacina(paciente.Codigo, cartao.DataAplicacao.Value, co_vacina, co_dose);

            if (atualizacaovalida)
            {
                Factory.GetInstance<IVacinaServiceFacade>().Salvar(cartao);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Cartão atualizado com sucesso.');", true);
                IParametrizacaoVacina iParametrizacao = Factory.GetInstance<IParametrizacaoVacina>();

                IList<CartaoVacina> cartoeshistorico = (IList<CartaoVacina>)Session["cartaohistorico"];
                IList<ParametrizacaoVacina> parametrizacoes = iParametrizacao.BuscaProximaDose<ParametrizacaoVacina>(cartao.DoseVacina.Codigo, cartao.Vacina.Codigo);
                cartao.ProximaDose = CartaoVacina.RetornaProximaDose(parametrizacoes, cartao.DataAplicacao);
                cartoeshistorico.Add(cartao);
                Session["cartaohistorico"] = cartoeshistorico;
                this.CarregaGridViewCartao(this.GridView_HistoricoVacinacao, cartoeshistorico);

                if (iParametrizacao.VerificarRequisitos(cartao.Vacina.Codigo, cartao.DoseVacina.Codigo, CartaoVacina.CRIANCA) == true)
                {
                    IList<CartaoVacina> cartaocrianca = (IList<CartaoVacina>)Session["cartaocrianca"];
                    Session["cartaocrianca"] = CartaoVacina.SubstituirCartao(cartaocrianca,cartao);
                    this.CarregaGridViewCartao(this.GridView_CartaoVacina, (IList<CartaoVacina>)Session["cartaocrianca"]);
                }
                
                if (iParametrizacao.VerificarRequisitos(cartao.Vacina.Codigo, cartao.DoseVacina.Codigo, CartaoVacina.ADULTOIDOSO) == true)
                {
                    IList<CartaoVacina> cartaoadulto = (IList<CartaoVacina>)Session["cartaoadulto"];
                    Session["cartaoadulto"] = CartaoVacina.SubstituirCartao(cartaoadulto, cartao);
                    this.CarregaGridViewCartao(this.GridView_CartaoVacinaAdulto, (IList<CartaoVacina>)Session["cartaoadulto"]);
                }
                
                if (iParametrizacao.VerificarRequisitos(cartao.Vacina.Codigo, cartao.DoseVacina.Codigo, CartaoVacina.ADOLESCENTE) == true)
                {
                    IList<CartaoVacina> cartaoadolescente= (IList<CartaoVacina>)Session["cartaoadolescente"];
                    Session["cartaoadolescente"] = CartaoVacina.SubstituirCartao(cartaoadolescente, cartao);
                    this.CarregaGridViewCartao(this.GridView_CartaoVacinaAdolescente, (IList<CartaoVacina>)Session["cartaoadolescente"]);
                }

                //this.CarregaHistoricoCartaoVacina(paciente.Codigo);
                this.OnClick_Cancelar(sender, e);
                Factory.GetInstance<IVacinaServiceFacade>().Inserir(new LogVacina(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 24, "id cartao: " + cartao.Codigo));

                //Response.Redirect("FormCartaoVacina.aspx");
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A dose desta vacina já se encontra no cartão de vacina do paciente para a data informada.');", true);
        }

        protected void OnClick_NovoRegistro(object sender, EventArgs e)
        {
            this.LimparCamposNovoCartao();

            DropDown_Vacina.Visible = true;
            DropDown_Dose.Visible = true;

            Label_DoseVacina.Visible = false;
            Label_Imunobiologico.Visible = false;

            Panel_AtualizarCartao.Visible = true;
        }

        protected void OnClick_Cancelar(object sender, EventArgs e)
        {
            this.LimparCamposNovoCartao();
            Panel_AtualizarCartao.Visible = false;
        }

        private void LimparCamposNovoCartao()
        {
            DropDown_Vacina.SelectedValue = "-1";
            DropDown_Dose.SelectedValue = "-1";
            TextBox_Data.Text = "";
            TextBox_Lote.Text = "";
            TextBox_Local.Text = "";
            TextBox_Motivo.Text = "";
            TextBox_ValidadeLote.Text = "";
        }

        protected void btnCartaoVacina_Click(object sender, EventArgs e)
        {
            if (Session["pacienteSelecionado"] != null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "window.open('FormConfiguraImpressaoCartaoVacina.aspx','_blank');", true);
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "javascript:GB_showFullScreen('Imprimir Cartão de Vacina','../FormConfiguraImpressaoCartaoVacina.aspx');", true);
            }
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "window.open('FormConfiguraImpressaoCartaoVacina.aspx');", true);
        }

        protected void OnClick_FecharInformacoes(object sender, EventArgs e)
        {
            Panel_Info.Visible = false;
        }
    }
}
