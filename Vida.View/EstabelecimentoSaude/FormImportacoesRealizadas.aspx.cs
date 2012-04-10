using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.EstabelecimentoSaude
{
    public partial class FormImportacoesRealizadas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "IMPORTAR_CNES"))
                    this.lkn_ButtonNovaImportacao.Visible = false;

                CarregaImportacoes();
            }
        }

        /// <summary>
        /// Carrega todas as importações via CNES realizadas em ordem descrecente de data
        /// </summary>
        private void CarregaImportacoes()
        {
            GridView_Importacao.DataSource = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<ImportacaoCNES>().OrderByDescending(p => p.HorarioInicio).ToList();
            GridView_Importacao.DataBind();
        }

        /// <summary>
        /// Paginação do gridview de importações
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            CarregaImportacoes();
            GridView_Importacao.PageIndex = e.NewPageIndex;
            GridView_Importacao.DataBind();
        }
    }
}
