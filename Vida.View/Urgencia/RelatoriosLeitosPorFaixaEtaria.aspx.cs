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
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.DAO;
using ViverMais.View.Urgencia.RelatoriosCrystal;
using CrystalDecisions.CrystalReports.Engine;

namespace ViverMais.View.Urgencia
{
    public partial class RelatoriosLeitosPorFaixaEtaria : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            Usuario usuario = ((Usuario)Session["Usuario"]);
            //lblUnidade.Text = usuario.Unidade.NomeFantasia;
            //lblData.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");

            DataTable cab = new DataTable();
            cab.Columns.Add("Unidade", typeof(string));
            DataRow linha = cab.NewRow();
            linha["Unidade"] = usuario.Unidade.NomeFantasia;
            cab.Rows.Add(linha);

            DSCabecalhoLeitosFaixa dscabecalho = new DSCabecalhoLeitosFaixa();
            dscabecalho.Tables.Add(cab);

            //long id_unidade = long.Parse(Request.QueryString["id_unidade"]);

            IRelatorioUrgencia iRelatorio = Factory.GetInstance<IRelatorioUrgencia>();
            IList resultados = iRelatorio.ObterRelatorioLeitosFaixa(Request["id_unidade"].ToString());
            DSLeitosFaixa dsconteudo = new DSLeitosFaixa();

            DataTable table = new DataTable();
            DataColumn col1 = new DataColumn("FaixaEtaria");
            DataColumn col2 = new DataColumn("Quantidade");
            table.Columns.Add(col1);
            table.Columns.Add(col2);
            foreach (object obj in resultados)
            {
                DataRow row = table.NewRow();
                object[] valor = (object[])obj;
                row[0] = valor[0].ToString() + "-" + valor[1].ToString();
                row[1] = valor[2].ToString();
                table.Rows.Add(row);
            }

            dsconteudo.Tables.Add(table);

            ReportDocument doc = new ReportDocument();
            doc.Load(Server.MapPath("RelatoriosCrystal/RelLeitosFaixa.rpt"));
            doc.SetDataSource(dsconteudo.Tables[1]);
            doc.Subreports[0].SetDataSource(dscabecalho.Tables[1]);

            Session["documentoImpressaoUrgencia"] = doc;
            Response.Redirect("FormImprimirCrystalReports.aspx");

            //CrystalReportViewer_LeitosFaixa.ReportSource = doc;
            //CrystalReportViewer_LeitosFaixa.DataBind();

            //DataTable table = new DataTable();
            //DataColumn col1 = new DataColumn();
            //DataColumn col2 = new DataColumn();
            //DataColumn col3 = new DataColumn();
            //DataColumn col4 = new DataColumn();
            //DataColumn col5 = new DataColumn();
            //DataColumn col6 = new DataColumn();
            //DataColumn col7 = new DataColumn();
            
            //table.Columns.Add(col1);
            //table.Columns.Add(col2);
            //table.Columns.Add(col3);
            //table.Columns.Add(col4);
            //table.Columns.Add(col5);
            //table.Columns.Add(col6);
            //table.Columns.Add(col7);

            //DataRow hrow = table.NewRow();
            //hrow[0] = "Leito Masculino";
            //hrow[1] = "Leito Feminino";
            //hrow[2] = "Leito Infantil";
            //hrow[3] = "Masculinos Ocupados";
            //hrow[4] = "Feminios Ocupados";
            //hrow[5] = "Infantis Ocupados";
            //hrow[6] = "Consultorio";
            //table.Rows.Add(hrow);

            //foreach (object o in resultados)
            //{
            //    DataRow row = table.NewRow();
            //    row[0] = ((object[])o)[0].ToString();
            //    row[1] = ((object[])o)[1].ToString();
            //    row[2] = ((object[])o)[2].ToString();
            //    row[3] = ((object[])o)[3].ToString();
            //    row[4] = ((object[])o)[4].ToString();
            //    row[5] = ((object[])o)[5].ToString();
            //    row[6] = ((object[])o)[6].ToString();
            //    table.Rows.Add(row);
            //}            
            //GridViewLeitosFaixaEtaria.DataSource = table;
            //GridViewLeitosFaixaEtaria.DataBind();
        }
    }
}
