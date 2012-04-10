using System;
using System.Collections;
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
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;

namespace ViverMais.View.Vacina
{
    public partial class FormExibeCartaoVacina : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViverMais.Model.Paciente paciente = (ViverMais.Model.Paciente)Session["pacienteSelecionado"];

                if (paciente != null)
                    this.CarregaHistoricoCartaoVacina(paciente.Codigo);
                else
                    Response.Redirect("FormPesquisaCartaoVacina.aspx");
            }
        }

        void CarregaHistoricoCartaoVacina(string co_paciente)
        {
            Hashtable hash = Factory.GetInstance<ICartaoVacina>().RetornaCartoesPaciente(co_paciente);
            IList<CartaoVacina> cartaocrianca = (IList<CartaoVacina>)hash["cartaocrianca"];
            IList<CartaoVacina> cartaoadolescente = (IList<CartaoVacina>)hash["cartaoadolescente"];
            IList<CartaoVacina> cartaoadulto = (IList<CartaoVacina>)hash["cartaoadulto"];
            IList<CartaoVacina> cartaohistorico = (IList<CartaoVacina>)hash["cartaohistorico"];

            Session["cartaocrianca"] = cartaocrianca;
            GridView_CartaoVacina.DataSource = cartaocrianca;
            GridView_CartaoVacina.DataBind();

            Session["cartaoadolescente"] = cartaoadolescente;
            GridView_CartaoVacinaAdolescente.DataSource = cartaoadolescente;
            GridView_CartaoVacinaAdolescente.DataBind();

            Session["cartaoadulto"] = cartaoadulto;
            GridView_CartaoVacinaAdulto.DataSource = cartaoadulto;
            GridView_CartaoVacinaAdulto.DataBind();

            Session["cartaohistorico"] = cartaohistorico;
            GridView_HistoricoVacinacao.DataSource = cartaohistorico;
            GridView_HistoricoVacinacao.DataBind();
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
                int co_vacina = int.Parse(GridView_HistoricoVacinacao.DataKeys[int.Parse(e.CommandArgument.ToString())][0].ToString());
                int co_dose = int.Parse(GridView_HistoricoVacinacao.DataKeys[int.Parse(e.CommandArgument.ToString())][1].ToString());
                this.CarregaInformacoesDoseVacina(co_vacina, co_dose);
                Panel_Info.Visible = true;
            }
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

        protected void OnClick_FecharInformacoes(object sender, EventArgs e)
        {
            Panel_Info.Visible = false;
        }

        protected void btnCartaoVacina_Click(object sender, EventArgs e)
        {
            if (Session["pacienteSelecionado"] != null)
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "window.open('FormConfiguraImpressaoCartaoVacina.aspx');", true);
        }
    }
}
