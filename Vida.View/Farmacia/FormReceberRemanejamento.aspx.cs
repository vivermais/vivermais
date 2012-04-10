using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Farmacia
{
    public partial class FormReceberRemanejamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                bool permissao = Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "RECEBER_REMANEJAMENTO", Modulo.FARMACIA);

                if (!permissao)
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    DropDownList_Farmacia.DataSource = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia, Usuario>(usuario, true, true);
                    DropDownList_Farmacia.DataBind();

                    OnSelectedIndexChanged_CarregaRemanejamento(sender, e);
                }
                //ViverMais.Model.Farmacia farm = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia>(((Usuario)Session["Usuario"]).Codigo);

                //if (farm == null)
                //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O usuário logado não está vinculado a farmácia alguma. Para realizar qualquer tipo de movimentação, favor efetivar este vínculo.');location='Default.aspx';", true);

                //ViewState["co_farmacia"] = farm.Codigo;
                //CarregarRemanejamentos(farm.Codigo);
            }
        }

        /// <summary>
        /// Carrega os remanejamentos registrados para da farmácia informada
        /// </summary>
        /// <param name="co_farmacia">código da farmácia</param>
        private void CarregarRemanejamentos(int co_farmacia)
        {
            GridView_ItensRemanejamento.DataSource = Factory.GetInstance<IMovimentacao>().BuscarRemanejamentosPorFarmacia<RemanejamentoMedicamento>(co_farmacia).Where(p=>p.Status != RemanejamentoMedicamento.FECHADO).ToList();
            GridView_ItensRemanejamento.DataBind();
        }

        /// <summary>
        /// Paginação do gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            CarregarRemanejamentos(int.Parse(ViewState["co_farmacia"].ToString()));
            GridView_ItensRemanejamento.PageIndex = e.NewPageIndex;
            GridView_ItensRemanejamento.DataBind();
        }

        protected void OnSelectedIndexChanged_CarregaRemanejamento(object sender, EventArgs e)
        {
            ViewState["co_farmacia"] = DropDownList_Farmacia.SelectedValue;
            CarregarRemanejamentos(int.Parse(DropDownList_Farmacia.SelectedValue));
        }
    }
}
