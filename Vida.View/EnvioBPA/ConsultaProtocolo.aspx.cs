using System;
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
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.BPA;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.View.EnvioBPA
{
    public partial class ConsultaProtocolo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void imgbtnEnviar_Click(object sender, EventArgs e)
        {
            ProtocoloEnvioBPA protocolo = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ProtocoloEnvioBPA>(int.Parse(tbxProtocolo.Text));
            GridViewProtocolo.DataSource = new ProtocoloEnvioBPA[] { protocolo };
            GridViewProtocolo.DataBind();
        }
    }
}
