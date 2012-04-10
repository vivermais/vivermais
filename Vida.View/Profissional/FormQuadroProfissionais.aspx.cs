using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;

namespace ViverMais.View.Profissional
{
    public partial class FormQuadroProfissionais : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Teste
                IList<ViverMais.Model.Profissional> lp = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<ViverMais.Model.Profissional>().OrderBy(p => p.Nome).ToList();
                GridView1.DataSource = lp;
                GridView1.DataBind();
            }
        }
    }
}
