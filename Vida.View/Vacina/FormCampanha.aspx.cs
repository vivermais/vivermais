using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Vacina;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.View.Vacina
{
    public partial class FormCampanha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_CAMPANHA_VACINA", Modulo.VACINA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    int temp;

                    if (Request["co_campanha"] != null && int.TryParse(Request["co_campanha"].ToString(), out temp))
                    {
                        ViewState["co_campanha"] = temp;
                        Campanha campanha = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<Campanha>(temp);
                        tbxNome.Text = campanha.Nome;
                        tbxFaixaInicial.Text = campanha.FaixaEtariaInicial.ToString();
                        DropDownList_UnidadeFaixaInicial.SelectedValue = campanha.UnidadeFaixaInicial.ToString();
                        tbxFaixaFinal.Text = campanha.FaixaEtariaFinal.ToString();
                        DropDownList_UnidadeFaixaFinal.SelectedValue = campanha.UnidadeFaixaFinal.ToString();
                        TextBox_DataInicio.Text = campanha.DataInicio.ToString("dd/MM/yyyy");
                        TextBox_DataFim.Text = campanha.DataFim.ToString("dd/MM/yyyy");
                        ddlSexo.SelectedValue = campanha.Sexo.ToString();
                        TextBox_Meta.Text = campanha.Meta.ToString();
                        //Label_StatusCampanha.Text = campanha.Status == Convert.ToChar(Campanha.DescricaoStatus.Ativa) ? "Em andamento" : "Finalizada";

                        if (campanha.Status == Convert.ToChar(Campanha.DescricaoStatus.Finalizada))
                        {
                            PanelFinalizar.Visible = false;
                            PanelSalvar.Visible = false;
                            Label_StatusCampanha.Text = "Finalizada";
                        }
                        else
                        {
                            Label_StatusCampanha.Text = "Em andamento";
                            PanelFinalizar.Visible = true;
                        }
                    }
                    else
                        Label_StatusCampanha.Text = "Em andamento";
                }
            }
        }

        /// <summary>
        /// Salva a campanha de vacinação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (CustomValidator_FaixaEtaria.IsValid)
            {
                Campanha campanha = ViewState["co_campanha"] != null ? Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<Campanha>(int.Parse(ViewState["co_campanha"].ToString())) : new Campanha();

                campanha.Nome = tbxNome.Text.ToUpper();
                campanha.DataInicio = DateTime.Parse(TextBox_DataInicio.Text);
                campanha.DataFim = DateTime.Parse(TextBox_DataFim.Text);
                campanha.FaixaEtariaInicial = float.Parse(tbxFaixaInicial.Text);
                campanha.UnidadeFaixaInicial = char.Parse(DropDownList_UnidadeFaixaInicial.Text);
                campanha.UnidadeFaixaFinal = char.Parse(DropDownList_UnidadeFaixaFinal.Text);
                campanha.FaixaEtariaFinal = float.Parse(tbxFaixaFinal.Text);
                campanha.Sexo = int.Parse(ddlSexo.SelectedValue);
                campanha.Meta = int.Parse(TextBox_Meta.Text);
                campanha.Status = Convert.ToChar(Campanha.DescricaoStatus.Ativa);

                if (campanha.Codigo != 0)
                {
                    Factory.GetInstance<IViverMaisServiceFacade>().Atualizar(campanha);
                    Factory.GetInstance<IVacinaServiceFacade>().Inserir(new LogVacina(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 1, "id campanha: " + campanha.Codigo));
                }
                else
                {
                    Factory.GetInstance<IViverMaisServiceFacade>().Inserir(campanha);
                    Factory.GetInstance<IVacinaServiceFacade>().Inserir(new LogVacina(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 1, "id campanha: " + Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<Campanha>().Max(p=>p.Codigo).ToString()));
                }

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados salvos com sucesso!');location='FormExibeCampanha.aspx';", true);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + CustomValidator_FaixaEtaria.ErrorMessage + "');", true);
        }

        /// <summary>
        /// Finaliza a campanha para que não ser alterado seus nem os itens já cadastrados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_FinalizarCampanha(object sender, EventArgs e)
        {
            Campanha campanha = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<Campanha>(int.Parse(ViewState["co_campanha"].ToString()));

            campanha.Status = Convert.ToChar(Campanha.DescricaoStatus.Finalizada);
            try
            {
                Factory.GetInstance<IVacinaServiceFacade>().Atualizar(campanha);
                Factory.GetInstance<IVacinaServiceFacade>().Inserir(new LogVacina(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 2, "id campanha: " + campanha.Codigo));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Campanha finalizada com sucesso.');location='FormExibeCampanha.aspx';", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Valida o período da faixa etária da campanha
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnServerValidate_ValidarFaixaEtaria(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = true;

            if (float.Parse(tbxFaixaInicial.Text) == 0 && float.Parse(tbxFaixaFinal.Text) == 0)
            {
                e.IsValid = false;
                CustomValidator_FaixaEtaria.ErrorMessage = "Faixa Etária Inválida.";
            }
            else
            {
                if (Convert.ToChar(DropDownList_UnidadeFaixaInicial.SelectedValue) == Convert.ToChar(Campanha.DescricaoUnidadeFaixa.Meses))
                {
                    if (float.Parse(tbxFaixaInicial.Text) >= 12)
                    {
                        e.IsValid = false;
                        CustomValidator_FaixaEtaria.ErrorMessage = "Início da Faixa Etária em Meses deve ser menor que 12.";
                        return;
                    }
                }
                else
                {
                    if (float.Parse(tbxFaixaInicial.Text) < 1)
                    {
                        e.IsValid = false;
                        CustomValidator_FaixaEtaria.ErrorMessage = "Início da Faixa Etária em Anos deve ser maior que 1.";
                        return;
                    }
                }

                if (Convert.ToChar(DropDownList_UnidadeFaixaFinal.SelectedValue) == Convert.ToChar(Campanha.DescricaoUnidadeFaixa.Anos))
                {
                    if (float.Parse(tbxFaixaFinal.Text) < 1)
                    {
                        e.IsValid = false;
                        CustomValidator_FaixaEtaria.ErrorMessage = "Fim da Faixa Etária em Anos deve ser maior que 1.";
                        return;
                    }
                }
                else
                {
                    if (float.Parse(tbxFaixaFinal.Text) >= 12)
                    {
                        e.IsValid = false;
                        CustomValidator_FaixaEtaria.ErrorMessage = "Fim da Faixa Etária em Meses deve ser menor que 12.";
                        return;
                    }
                }

                if (DropDownList_UnidadeFaixaInicial.SelectedValue == DropDownList_UnidadeFaixaFinal.SelectedValue) //Mesmas unidades de período
                {
                    if (float.Parse(tbxFaixaFinal.Text) < float.Parse(tbxFaixaInicial.Text))
                    {
                        e.IsValid = false;
                        CustomValidator_FaixaEtaria.ErrorMessage = "O Fim da Faixa Etária deve ser igual ou maior que o seu Início.";
                    }
                }
                else //Unidades de período diferentes
                {
                    if (Convert.ToChar(DropDownList_UnidadeFaixaFinal.SelectedValue) == Convert.ToChar(Campanha.DescricaoUnidadeFaixa.Meses))
                    {
                        e.IsValid = false;
                        CustomValidator_FaixaEtaria.ErrorMessage = "O Fim da Faixa Etária deve ser igual ou maior que o seu Início.";
                    }
                }
            }
        }
    }
}
