using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vida.DAO;
using Vida.ServiceFacade.ServiceFacades.Misc;
using Vida.Model;
using Vida.ServiceFacade.ServiceFacades;

namespace Vida.View.EstabelecimentoSaude
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IList<Bairro> bairros = Factory.GetInstance<IServicoSaude>().BuscarBairrosServicos<Bairro, Vida.Model.EstabelecimentoSaude>(Factory.GetInstance<IVidaServiceFacade>().ListarTodos<Vida.Model.EstabelecimentoSaude>().Where(p => p.Distrito != null && (p.Distrito.Codigo == 1 || p.Distrito.Codigo == 2)).ToList());
            }
        }
    }
}
