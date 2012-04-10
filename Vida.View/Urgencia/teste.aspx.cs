using System;
using System.Collections;
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
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.View.Urgencia
{
    public partial class teste : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SenhadorUrgence senhador = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<SenhadorUrgence>().First();
                WebServiceDinamico _webService = new WebServiceDinamico(senhador.EnderecoWebService, SenhadorUrgence.NomeServico);
            }
        }
    }
}
