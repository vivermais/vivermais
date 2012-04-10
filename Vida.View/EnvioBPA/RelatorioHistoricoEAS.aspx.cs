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
using System.Collections.Generic;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.BPA;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;

namespace ViverMais.View.EnvioBPA
{
    public partial class RelatorioHistoricoEAS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void imgEnviar_Click(object sender, EventArgs e)
        {
            ViverMais.Model.EstabelecimentoSaude eas = Factory.GetInstance<IEstabelecimentoSaude>().BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(tbxCNES.Text);
            IList<ProtocoloEnvioBPA> protocolos = Factory.GetInstance<IEnviarBPA>().ListarProtocolos<ProtocoloEnvioBPA>(eas, int.Parse(ddlAno.SelectedValue));
            GridView1.DataSource = protocolos;
            GridView1.DataBind();
        }
    }
}
