﻿using System;
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
using System.Collections.Generic;
using ViverMais.DAO;

namespace ViverMais.View.Farmacia
{
    public partial class RelatorioPosicaoEstoqueLote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int codigoFarmacia = int.Parse(Request.QueryString["id_farmacia"]);
            IRelatorioFarmacia relatorio = Factory.GetInstance<IRelatorioFarmacia>();
            DataTable table = new DataTable();
            DataColumn c0 = new DataColumn("Medicamento");
            DataColumn c1 = new DataColumn("Sigla");
            DataColumn c2 = new DataColumn("Lote");
            DataColumn c3 = new DataColumn("Fabricante");
            DataColumn c4 = new DataColumn("QuantidadeEstoque");
            DataColumn c5 = new DataColumn("Validade");
            table.Columns.Add(c0);
            table.Columns.Add(c1);
            table.Columns.Add(c2);
            table.Columns.Add(c3);
            table.Columns.Add(c4);
            table.Columns.Add(c5);
            IList<ViverMais.Model.Estoque> result = relatorio.ObterRelatorioPosicaoEstoqueLote<ViverMais.Model.Estoque>(codigoFarmacia);
            foreach (ViverMais.Model.Estoque item in result)
            {
                DataRow row = table.NewRow();
                row[0] = item.LoteMedicamento.Medicamento.Nome;
                row[1] = item.LoteMedicamento.Medicamento.UnidadeMedida.Sigla;
                row[2] = item.LoteMedicamento.Lote;
                row[3] = item.LoteMedicamento.Fabricante.Nome;
                row[4] = item.QuantidadeEstoque.ToString();
                row[5] = item.LoteMedicamento.Validade.ToString("dd/MM/yyyy");
                table.Rows.Add(row);
            }
            GridView1.DataSource = table;
            GridView1.DataBind();
        }
    }
}
