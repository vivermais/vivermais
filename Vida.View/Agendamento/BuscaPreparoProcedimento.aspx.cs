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
using ViverMais.Model;
//.Entities.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.DAO;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Agendamento
{
    public partial class BuscaPreparoProcedimento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "PREPARO_PROCEDIMENTO",Modulo.AGENDAMENTO))
            {
                IList<Preparo> preparos = Factory.GetInstance<IPreparo>().ListarTodos<ViverMais.Model.Preparo>("Descricao", true);
                GridViewPreparo.DataSource = preparos;
                GridViewPreparo.DataBind();
                Session["Preparos"] = preparos;
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
        }

        protected void GridViewPreparo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            IList<Preparo> preparos = (IList<Preparo>)Session["Preparos"];
            GridViewPreparo.DataSource = preparos;
            GridViewPreparo.PageIndex = e.NewPageIndex;
            GridViewPreparo.DataBind();
        }

        protected void GridViewPreparo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                int id_preparo = Convert.ToInt32(e.CommandArgument);
                IPreparo ipreparo = Factory.GetInstance<IPreparo>();
                Preparo preparo = ipreparo.BuscarPorCodigo<Preparo>(id_preparo);
                
                //Deleta o preparo da lista de Preparos que o Procedimento Possui
                ipreparo.Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 4, "Preparo:" + preparo.Descricao));
                ipreparo.Deletar(preparo);
                GridViewPreparo.DataSource = ipreparo.ListarTodos<ViverMais.Model.Preparo>();
                GridViewPreparo.DataBind();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Registro Excluído com Sucesso!');", true);
            }
        }
    }
}
