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
    public partial class RelatorioMovimentacaoDiaria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime data = DateTime.Parse(Request.QueryString["data"]);
            int codigoFarmacia = int.Parse(Request.QueryString["id_farmacia"]);
            IRelatorioFarmacia relatorio = Factory.GetInstance<IRelatorioFarmacia>();
            DataTable table = new DataTable();
            DataColumn c0 = new DataColumn("Medicamento");
            DataColumn c1 = new DataColumn("Unidade");
            DataColumn c2 = new DataColumn("Quantidade");
            table.Columns.Add(c0);
            table.Columns.Add(c1);
            table.Columns.Add(c2);
            IList result = relatorio.ObterMovimentacaoDiaria(data, codigoFarmacia);
            foreach (Object item in result)
            {
                object[] valor = (object[])item;
                DataRow row = table.NewRow();
                row[0] = valor[0].ToString();
                row[1] = valor[1].ToString();
                row[2] = valor[2].ToString();
                table.Rows.Add(row);
            }
            GridView1.DataSource = table;
            GridView1.DataBind();
        }
    }
}
