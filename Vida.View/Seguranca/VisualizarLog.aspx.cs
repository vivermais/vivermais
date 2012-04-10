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
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Misc;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.View.Seguranca
{
    public partial class VisualizarLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                tbxDataInicial.Text = DateTime.Today.AddDays(-5).ToString("dd/MM/yyyy");
                tbxDataFinal.Text = DateTime.Today.ToString("dd/MM/yyyy");
                ddlEvento.Items.Add(new ListItem("Todos", "0"));
                IList<ViverMais.Model.EventoViverMais> eventos = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<ViverMais.Model.EventoViverMais>();
                foreach (var item in eventos)
                {
                    ddlEvento.Items.Add(new ListItem(item.Descricao, item.Codigo.ToString()));
                }
            }
        }

        protected void ImgBtnEnviar1_Click(object sender, EventArgs e)
        {
            lblEvento.Text = ddlEvento.SelectedItem.Text;
            IList<ViverMais.Model.LogViverMais> logs = Factory.GetInstance<ILogEventos>().BuscarLog<ViverMais.Model.LogViverMais>(DateTime.Parse(tbxDataInicial.Text), DateTime.Parse(tbxDataFinal.Text + " 23:59:59"), int.Parse(ddlEvento.SelectedValue));
            DataTable table = new DataTable();
            DataColumn c0 = new DataColumn("Data");
            DataColumn c1 = new DataColumn("Usuario");
            //DataColumn c2 = new DataColumn("Evento");
            DataColumn c3 = new DataColumn("Valor");
            table.Columns.Add(c0);
            table.Columns.Add(c1);
            //table.Columns.Add(c2);
            table.Columns.Add(c3);
            //IViverMaisServiceFacade iViverMais = Factory.GetInstance
            foreach (ViverMais.Model.LogViverMais log in logs)
            {
                DataRow row = table.NewRow();
                row[0] = log.Data;
                row[1] = log.Usuario.Nome;
                //row[2] = log.Evento;
                row[2] = log.Valor;
                table.Rows.Add(row);
            }
            GridViewLog.DataSource = table;
            GridViewLog.DataBind();
        }
    }
}
