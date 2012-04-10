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
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using System.Collections.Generic;

namespace ViverMais.View.Vacina
{
    public partial class FormExibeUnidadeMedidaVacina : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_UNIDADE_MEDIDA_VACINA", Modulo.VACINA))
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                    CarregaUnidadeMedida();
            }
        }

        /// <summary>
        /// Carrega as unidades de medida cadastradas
        /// </summary>
        private void CarregaUnidadeMedida()
        {
            GridView_UnidadeMedidaVacina.DataSource = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<UnidadeMedidaVacina>().OrderBy(p => p.Nome).ToList();
            GridView_UnidadeMedidaVacina.DataBind();
        }

        /// <summary>
        /// Paginação do gridview de unidades de medida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            CarregaUnidadeMedida();
            GridView_UnidadeMedidaVacina.PageIndex = e.NewPageIndex;
            GridView_UnidadeMedidaVacina.DataBind();
        }

        /// <summary>
        /// Exclui a unidade de medida de uma vacina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_ExcluirUnidadeMedida(object sender, EventArgs e) 
        {
            LinkButton LinkButtonExcluir = (LinkButton)sender;
            int co_unidademedida = int.Parse(LinkButtonExcluir.CommandArgument.ToString());

            try
            {
                IList<ViverMais.Model.Vacina> vacinas = Factory.GetInstance<IVacina>().BuscarPorUnidadeMedida<ViverMais.Model.Vacina>(co_unidademedida);

                if (vacinas.Count == 0)
                {
                    UnidadeMedidaVacina unidade = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<UnidadeMedidaVacina>(co_unidademedida);

                    Factory.GetInstance<IVacinaServiceFacade>().Deletar(unidade);
                    Factory.GetInstance<IVacinaServiceFacade>().Inserir(new LogVacina(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 10, "unidade medida: " + unidade.Nome));

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Unidade de medida excluída com sucesso.');", true);
                    CarregaUnidadeMedida();
                }
                else
                {
                    string mensagemalerta = string.Empty;
                    mensagemalerta += "\\n";

                    int i = 1;
                    foreach (ViverMais.Model.Vacina vacina in vacinas)
                    {
                        mensagemalerta += "\\n(" + i + ") " + vacina.Nome + ",";
                        i++;
                    }
                    
                    mensagemalerta = mensagemalerta.Remove(mensagemalerta.Length - 1, 1);
                    mensagemalerta += ".";

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O registro não pode ser excluído, pois os seguintes imunobiológicos estão utilizando esta unidade de medida: " + mensagemalerta + "');", true);
                }
            }
            catch (Exception f)
            {
                throw f;
            }
        }
    }
}