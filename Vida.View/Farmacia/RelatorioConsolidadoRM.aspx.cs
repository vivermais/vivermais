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
using ViverMais.ServiceFacade.ServiceFacades.Farmacia;
using ViverMais.DAO;

namespace ViverMais.View.Farmacia
{
    public partial class RelatorioConsolidadoRM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IRelatorioFarmacia relatorio = Factory.GetInstance<IRelatorioFarmacia>();
            int id_distrito = int.Parse(Request.QueryString["id_distrito"]);
            int mes = int.Parse(Request.QueryString["mes"]);
            int ano = int.Parse(Request.QueryString["ano"]);
            DataTable table = new DataTable();
            DataColumn c0 = new DataColumn("Medicamento");
            DataColumn c1 = new DataColumn("Quantidade");
            DataColumn c2 = new DataColumn("QuantidadeFornecida");
            DataColumn c3 = new DataColumn("QuantidadePedida");
            table.Columns.Add(c0);
            table.Columns.Add(c1);
            table.Columns.Add(c2);
            table.Columns.Add(c3);
            IList result = relatorio.ObterRelatorioConsolidadoRM(id_distrito, mes, ano);
            foreach (Object item in result)
            {
                object[] valor = (object[])item;
                DataRow row = table.NewRow();
                row[0] = valor[0].ToString();
                row[1] = valor[1].ToString();
                row[2] = valor[2].ToString();
                row[3] = valor[3].ToString();
                table.Rows.Add(row);
            }
            GridView1.DataSource = table;
            GridView1.DataBind();
        }
    }
}
