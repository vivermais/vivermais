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
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.DAO;
using System.Collections.Generic;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.View.Agendamento
{
    public partial class BuscaFeriado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CADASTRAR_FERIADO", Modulo.AGENDAMENTO))
                {
                    GridViewListaFeriados.PageIndexChanging += new GridViewPageEventHandler(GridViewListaFeriados_PageIndexChanging);
                    CarregaFeriados();
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        void CarregaFeriados()
        {
            IFeriado iFeriado = Factory.GetInstance<IFeriado>();
            IList<Feriado> feriados = iFeriado.ListarTodos<Feriado>("Data", true);
            Session["Feriados"] = feriados;
            GridViewListaFeriados.DataSource = feriados;
            GridViewListaFeriados.DataBind();
        }

        protected void GridViewListaFeriados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                int id_feriado = Convert.ToInt32(e.CommandArgument);
                IFeriado iFeriado = Factory.GetInstance<IFeriado>();
                Feriado feriado = iFeriado.BuscarPorCodigo<Feriado>(id_feriado);
                Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 2, "Nome Feriado:" + feriado.Descricao));
                iFeriado.Deletar(feriado);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Registro Excluído com Sucesso!');", true);
                IList<Feriado> feriados = iFeriado.ListarTodos<Feriado>("Data", true);
                Session["Feriados"] = feriados;
                GridViewListaFeriados.DataSource = feriados;
                GridViewListaFeriados.DataBind();
            }
        }

        protected void GridViewListaFeriados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Retorna a propriedade Feriados que está definida na sessão
            IList<Feriado> feriados = (IList<Feriado>)Session["Feriados"];
            GridViewListaFeriados.DataSource = feriados;
            GridViewListaFeriados.PageIndex = e.NewPageIndex;
            GridViewListaFeriados.DataBind();
        }
    }
}
