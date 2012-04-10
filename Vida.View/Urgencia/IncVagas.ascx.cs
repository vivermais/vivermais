using System;
using ViverMais.Model;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using System.Web.UI.HtmlControls;
using System.Data;

namespace ViverMais.View.Urgencia
{
    public partial class IncVagas : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] != null && Session["Usuario"] is Usuario)
                {
                    Usuario usuario = (ViverMais.Model.Usuario)Session["Usuario"];
                    lbQuadroVagas.Text = "Quadro de Vagas: ";
                    lbQuadroVagas.Text += usuario.Unidade.NomeFantasia.ToUpper();

                    GridView_Vagas.DataSource = Factory.GetInstance<IVagaUrgencia>().QuadroVagas(usuario.Unidade.CNES, true);
                    GridView_Vagas.DataBind();
                }
            }
        }
    }
}