using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;

namespace ViverMais.View.Urgencia
{
    public partial class FormExibeExame : System.Web.UI.Page
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

        private void ExibeExames()
        {
            IList<ViverMais.Model.Exame> exames = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ViverMais.Model.Exame>();
            gridExames.DataSource = exames;
            gridExames.DataBind();
            Session["exames"] = exames;
        }

        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            gridExames.DataSource = (IList<ViverMais.Model.Exame>)Session["exames"];
            gridExames.DataBind();
            //AtualizaExames();
            gridExames.PageIndex = e.NewPageIndex;
            gridExames.DataBind();
        }
    }
}
