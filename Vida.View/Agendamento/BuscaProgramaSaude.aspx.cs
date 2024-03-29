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
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model.Entities.ViverMais;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Agendamento
{
    public partial class BuscaProgramaSaude : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CADASTRAR_PROGRAMA_SAUDE", Modulo.AGENDAMENTO))
                {
                    CarregaProgramasAtivos();
                    CarregaProgramasInativos();
                    //GridViewListaProgramasAtivos.PageIndexChanging += new GridViewPageEventHandler(GridViewListaProgramas_PageIndexChanging);
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        void CarregaProgramasAtivos()
        {
            IList<ProgramaDeSaude> programasAtivos = Factory.GetInstance<IAgendamentoServiceFacade>().ListarTodos<ProgramaDeSaude>().Where(programa => programa.Ativo).OrderBy(programa => programa.Nome).ToList();
            if (programasAtivos.Count != 0)
            {
                Session["ProgramasAtivos"] = programasAtivos;
                GridViewListaProgramasAtivos.DataSource = programasAtivos;
                GridViewListaProgramasAtivos.DataBind();
            }
        }

        void CarregaProgramasInativos()
        {
            IList<ProgramaDeSaude> programasInativos = Factory.GetInstance<IAgendamentoServiceFacade>().ListarTodos<ProgramaDeSaude>().Where(programa => !programa.Ativo).OrderBy(programa => programa.Nome).ToList();
            Session["ProgramasInativos"] = programasInativos;
            GridViewListaProgramasInativos.DataSource = programasInativos;
            GridViewListaProgramasInativos.DataBind();

        }

        protected void GridViewListaProgramas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Inativar")
            {
                int id_programa = Convert.ToInt32(e.CommandArgument);
                ProgramaDeSaude programa = Factory.GetInstance<IAgendamentoServiceFacade>().BuscarPorCodigo<ProgramaDeSaude>(id_programa);
                if (programa != null)
                {
                    programa.Ativo = false;
                    Factory.GetInstance<IAgendamentoServiceFacade>().Salvar(programa);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Programa inativado com Sucesso!');", true);
                    Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 6, "PROGRAMA:" + programa.Nome));
                }
                CarregaProgramasAtivos();
                CarregaProgramasInativos();
                //IList<ProgramaDeSaude> programas = Factory.GetInstance<IAgendamentoServiceFacade>().ListarTodos<ProgramaDeSaude>();
                //Session["ProgramasAtivos"] = programas;
                //GridViewListaProgramasAtivos.DataSource = programas;
                //GridViewListaProgramasAtivos.DataBind();
            }
        }

        protected void GridViewListaProgramasAtivos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            IList<ProgramaDeSaude> programas = (IList<ProgramaDeSaude>)Session["ProgramasAtivos"];
            GridViewListaProgramasAtivos.DataSource = programas;
            GridViewListaProgramasAtivos.PageIndex = e.NewPageIndex;
            GridViewListaProgramasAtivos.DataBind();
        }

        protected void GridViewListaProgramasInativos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewListaProgramasInativos.DataSource = Session["ProgramasInativos"];
            GridViewListaProgramasInativos.PageIndex = e.NewPageIndex;
            GridViewListaProgramasInativos.DataBind();
        }
    }
}
