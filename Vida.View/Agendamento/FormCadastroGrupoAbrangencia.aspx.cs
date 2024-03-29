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
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Agendamento
{
    public partial class FormCadastroGrupoAbrangencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CADASTRAR_GRUPO_AGRANGENCIA", Modulo.AGENDAMENTO))
                {
                    ListarGruposAbrangenciaAtivos();
                    ListarGruposAbrangenciaInativos();
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                //Caso seja uma atualização
                if (Request.QueryString["codigo"] != null)
                {
                    string id_grupo = Request.QueryString["codigo"].ToString();
                    GrupoAbrangencia grupoAbrangencia = Factory.GetInstance<IAgendamentoServiceFacade>().BuscarPorCodigo<GrupoAbrangencia>(id_grupo);
                    tbxNomeGrupo.Text = grupoAbrangencia.NomeGrupo;
                    btnSalvar.Visible = true;
                    btnAddGrupo.Visible = false;
                }
                else
                {
                    tbxNomeGrupo.Text = String.Empty;
                    btnSalvar.Visible = false;
                    btnAddGrupo.Visible = true;
                }
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            GrupoAbrangencia grupoAbrangencia = new GrupoAbrangencia();
            //Caso Seja uma atualização
            if (Request.QueryString["codigo"] != null)
            {
                grupoAbrangencia = Factory.GetInstance<IGrupoAbrangencia>().BuscarPorCodigo<GrupoAbrangencia>(Request.QueryString["codigo"].ToString());
            }
            grupoAbrangencia.NomeGrupo = tbxNomeGrupo.Text.ToUpper();
            grupoAbrangencia.Ativo = true;
            Factory.GetInstance<IGrupoAbrangencia>().Salvar(grupoAbrangencia);
            Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 38, "ID::" + grupoAbrangencia.Codigo));
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados Salvos com Sucesso!');", true);
            ListarGruposAbrangenciaAtivos();
            ListarGruposAbrangenciaInativos();
        }

        /// <summary>
        /// Método Responsável por Listar na Gridview os Grupos de Abrangência Inativos
        /// </summary>
        void ListarGruposAbrangenciaInativos()
        {
            IList<GrupoAbrangencia> gruposInativos = Factory.GetInstance<IGrupoAbrangencia>().ListarGruposInativos<GrupoAbrangencia>();
            if (gruposInativos.Count != 0)
            {
                GridViewGruposAbrangenciaInativo.DataSource = gruposInativos;
                GridViewGruposAbrangenciaInativo.DataBind();
                lblSemRegistroInativo.Visible = false;
                Session["GruposInativos"] = gruposInativos;
            }
            else
            {
                lblSemRegistroInativo.Visible = true;
            }
        }

        /// <summary>
        /// Método Responsável por Listar na Gridview os Grupos de Abrangência Ativos
        /// </summary>
        void ListarGruposAbrangenciaAtivos()
        {
            IList<GrupoAbrangencia> gruposAtivos = Factory.GetInstance<IGrupoAbrangencia>().ListarGruposAtivos<GrupoAbrangencia>();
            if (gruposAtivos.Count != 0)
            {
                GridViewGruposAbrangencia.DataSource = gruposAtivos;
                GridViewGruposAbrangencia.DataBind();
                lblSemRegistroAtivo.Visible = false;
                Session["GruposAtivos"] = gruposAtivos;
            }
            else
            {
                lblSemRegistroAtivo.Visible = true;
            }
            
            //Session["ListaGrupos"] = grupos;
        }

        protected void GridViewGruposAbrangencia_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Inativar")
            {
                string id_grupo = e.CommandArgument.ToString();
                GrupoAbrangencia grupo = Factory.GetInstance<IGrupoAbrangencia>().BuscarPorCodigo<GrupoAbrangencia>(id_grupo);
                grupo.Ativo = false;
                Factory.GetInstance<IGrupoAbrangencia>().Salvar(grupo);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Grupo Inativado com Sucesso!');", true);
                ListarGruposAbrangenciaAtivos();
                ListarGruposAbrangenciaInativos();
            }
        }

        protected void GridViewGruposAbrangenciaInativo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            IList<GrupoAbrangencia> gruposInativos = (IList<GrupoAbrangencia>)Session["GruposInativos"];
            GridViewGruposAbrangenciaInativo.DataSource = gruposInativos;
            GridViewGruposAbrangenciaInativo.PageIndex = e.NewPageIndex;
            GridViewGruposAbrangenciaInativo.DataBind();
        }

        protected void GridViewGruposAbrangencia_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            IList<GrupoAbrangencia> gruposAtivos = (IList<GrupoAbrangencia>)Session["GruposAtivos"];
            GridViewGruposAbrangencia.DataSource = gruposAtivos;
            GridViewGruposAbrangencia.PageIndex = e.NewPageIndex;
            GridViewGruposAbrangencia.DataBind();
        }

        protected void btnAddGrupo_Click(object sender, EventArgs e)
        {
            //Verifica se já Existe Grupo com o mesmo nome
            GrupoAbrangencia grupo = Factory.GetInstance<IGrupoAbrangencia>().BuscarGrupoPorNome<GrupoAbrangencia>(tbxNomeGrupo.Text.ToUpper());
            if (grupo != null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já Existe Grupo com este nome!');", true);
                return;
            }
            else
            {
                grupo = new GrupoAbrangencia();
                grupo.NomeGrupo = tbxNomeGrupo.Text.ToUpper();
                grupo.Ativo = true;
                Factory.GetInstance<IViverMaisServiceFacade>().Salvar(grupo);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Grupo Cadastrado com Sucesso!');", true);
            }
            ListarGruposAbrangenciaAtivos();
        }
    }
}
