using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Urgencia
{
    public partial class FormExibeExameEletivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CADASTRAR_EXAME", Modulo.URGENCIA))
                {
                    this.LinkButton_NovoRegistro.Visible = false;
                    gridExames.Columns.RemoveAt(2);
                }

                this.ExibeExames();
            }
        }

        /// <summary>
        /// Atualiza a lista de exames
        /// </summary>
        private void ExibeExames()
        {
            IList<ExameEletivo> exames = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ExameEletivo>().OrderBy(p => p.Descricao).ToList();
            gridExames.DataSource = exames;
            Session["exames"] = exames;
            gridExames.DataBind();
        }

        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            gridExames.DataSource = (IList<ExameEletivo>)Session["exames"];
            gridExames.DataBind();
            gridExames.PageIndex = e.NewPageIndex;
            gridExames.DataBind();
        }
    }
}
