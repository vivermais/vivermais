using System;
using System.Collections.Generic;
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
using Vida.ServiceFacade.ServiceFacades.EstabelecimentoDeSaude;

namespace Vida.View
{
    public partial class FormEstabelecimentoDeSaude : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CarregaEstabelecimentos();
        }

        /// <summary>
        /// Carrega todos estabelecimentos de saúde
        /// </summary>
        private void CarregaEstabelecimentos()
        {
            //IEstabelecimentoDeSaude iEstabelecimento  = Factory.GetInstance<IEstabelecimentoDeSaude>();
            //IList<EstabelecimentoDeSaude> estabelecimentos = iEstabelecimento.ListarTodos<EstabelecimentoDeSaude>();
            //grid_EstabelecimentoSaude.DataSource      = DataTableEstabelecimentos(estabelecimentos);
            //grid_EstabelecimentoSaude.DataBind();
        }

        /// <summary>
        /// Retorna a lista de estabelecimentos de sáude no formato DataTable.
        /// </summary>
        /// <param name="estabelecimentos">Lista de Estabelecimentos de Saúde</param>
        /// <returns>DataTable estabelecimentos</returns>
        //private DataTable DataTableEstabelecimentos(IList<EstabelecimentoDeSaude> estabelecimentos)
        private DataTable DataTableEstabelecimentos()
        {
            //if ( estabelecimentos != null && estabelecimentos.Count > 0)
            //{
            //    DataTable dt = new DataTable();
                
            //    //Adicionando quais colunas irão aparecer no gridView
            //    DataColumn dc = new DataColumn("Codigo", typeof(String));
            //    dt.Columns.Add(dc);
            //    dc = new DataColumn("RazaoSocial", typeof(String));
            //    dt.Columns.Add(dc);
            //    dc = new DataColumn("NomeFantasia", typeof(String));
            //    dt.Columns.Add(dc);
            //    dc = new DataColumn("Status", typeof(String));
            //    dt.Columns.Add(dc);

            //    foreach (EstabelecimentoDeSaude estabelecimento in estabelecimentos) 
            //    {
            //        DataRow dr         = dt.NewRow();
            //        dr["Codigo"]       = estabelecimento.Codigo;
            //        dr["RazaoSocial"]  = estabelecimento.R_SOCIAL;
            //        dr["NomeFantasia"] = estabelecimento.NOME_FANTA;
            //        dr["Status"]       = true ? "Ativo" : "Inativo";
            //        dt.Rows.Add(dr);
            //    }

            //    return dt;
            //}

            return null;
        }

        /// <summary>
        /// Função que redireciona o usuário para a página de edição do estabelecimento escolhido.
        /// </summary>
        /// <param name="sender">Objeto de envio</param>
        /// <param name="e">Comando escolhido no GridView para com o estabelecimento</param>
        protected void onRowCommand_verificarAcao(object sender, GridViewCommandEventArgs e) 
        {
            //if (e.CommandName == "cn_visualizarEstabelecimento") 
            //    Response.Redirect("FormEditarEstabelecimentoSaude.aspx?codigo=" + grid_EstabelecimentoSaude.DataKeys[int.Parse(e.CommandArgument.ToString())]["Codigo"].ToString());
        }

        /// <summary>
        /// Permite a paginação para o gridView de estabelecimentos
        /// </summary>
        /// <param name="sender">Objeto de envio</param>
        /// <param name="e">Página de acesso para listagem</param>
        protected void onPageEstabelecimento(object sender, GridViewPageEventArgs e) 
        {
            //CarregaEstabelecimentos();
            //grid_EstabelecimentoSaude.PageIndex = e.NewPageIndex;
            //grid_EstabelecimentoSaude.DataBind();
        }
    }
}
