using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Movimentacao;

namespace ViverMais.View.Vacina
{
    public partial class FormReceberRemanejamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                ISeguranca iSeguranca = Factory.GetInstance<ISeguranca>();

                if (!iSeguranca.VerificarPermissao(usuario.Codigo, "RECEBER_REMANEJAMENTO", Modulo.VACINA))
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    ISalaVacina iVacina = Factory.GetInstance<ISalaVacina>();
                    DropDownList_Sala.DataSource = iVacina.BuscarPorUsuario<ViverMais.Model.SalaVacina, Usuario>(usuario, true, true);
                    DropDownList_Sala.DataBind();
                    DropDownList_Sala.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
                }
            }
        }

        protected void OnSelectedIndexChanged_PesquisarRemanejamento(object sender, EventArgs e)
        {
            if (DropDownList_Sala.SelectedValue != "-1")
            {
                IMovimentoVacina iMovimento = Factory.GetInstance<IMovimentoVacina>();
                IList<RemanejamentoVacina> remanejamentos = iMovimento.BuscarRemanejamentosSalaDestino<RemanejamentoVacina>(int.Parse(DropDownList_Sala.SelectedValue), RemanejamentoVacina.ABERTO).OrderByDescending(p=>p.Data).ToList();
                Session["remanejamentosVacina"] = remanejamentos;
                this.CarregaRemanejamentos(remanejamentos);
                Panel_RemanejamentosPesquisados.Visible = true;
            }
        }

        private void CarregaRemanejamentos(IList<RemanejamentoVacina> remanejamentos)
        {
            GridView_Remanejamentos.DataSource = remanejamentos;
            GridView_Remanejamentos.DataBind();
        }

        protected void OnPageIndexChanging_Remanejamentos(object sender, GridViewPageEventArgs e)
        {
            this.CarregaRemanejamentos((IList<RemanejamentoVacina>)Session["remanejamentosVacina"]);

            GridView_Remanejamentos.PageIndex = e.NewPageIndex;
            GridView_Remanejamentos.DataBind();
        }
    }
}
