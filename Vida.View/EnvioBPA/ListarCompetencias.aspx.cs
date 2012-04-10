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
using System.Collections.Generic;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.View.EnvioBPA
{
    public partial class ListarCompetencias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                //IList<CompetenciaBPA> competencias = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<CompetenciaBPA>().OrderBy(x=>x.Ano).ThenBy(x=>x.Mes);
                var competencias = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<CompetenciaBPA>().OrderBy(x => x.Ano).ThenBy(x => x.Mes);
                GridView1.DataSource = competencias;
                GridView1.DataBind();
            }
        }
    }
}
