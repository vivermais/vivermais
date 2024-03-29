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
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc;
using System.Collections.Generic;
using ViverMais.View.Urgencia.RelatoriosCrystal;
using CrystalDecisions.CrystalReports.Engine;

namespace ViverMais.View.Urgencia
{
    public partial class RelatorioAtendimentoFaixa : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            Usuario usuario = ((Usuario)Session["Usuario"]);

            string sexo = Request.QueryString["sexo"];
            DateTime data_Inicio = DateTime.Parse(Request.QueryString["data_inicio"]);
            DateTime data_Fim = DateTime.Parse(Request.QueryString["data_fim"]);
            
            DataTable cab = new DataTable();
            cab.Columns.Add("Unidade", typeof(string));
            cab.Columns.Add("Sexo", typeof(string));
            cab.Columns.Add("Periodo", typeof(string));

            DataRow linha = cab.NewRow();
            linha["Unidade"] = usuario.Unidade.NomeFantasia;
            linha["Periodo"] = data_Inicio.ToString("dd/MM/yyyy") + " a " + data_Fim.ToString("dd/MM/yyyy");

            //lblUnidade.Text = usuario.Unidade.NomeFantasia;

            switch (sexo)
            {
                default:
                    linha["Sexo"] = "Ambos";
                    break;
                case "M":
                    linha["Sexo"] = "Masculino";
                    break;
                case "F":
                    linha["Sexo"] = "Feminino";
                    break;
            }

            cab.Rows.Add(linha);
            DSCabecalhoAtendimentoFaixa dscabecalho = new DSCabecalhoAtendimentoFaixa();
            dscabecalho.Tables.Add(cab);

            //lblPeriodo.Text = data_Inicio.ToString("dd/MM/yyyy") + " a " + data_Fim.ToString("dd/MM/yyyy");
            //lblData.Text = DateTime.Now.ToString() ;
            //long id_unidade = long.Parse(Request["co_unidade"].ToString());

            IRelatorioUrgencia iRelatorio = Factory.GetInstance<IRelatorioUrgencia>();
            //IList resultados = iRelatorio.ObterRelatorioAtendimentoPorFaixa(id_unidade, sexo, data_Inicio, data_Fim);
            
            //DataTable table = new DataTable();
            //DataColumn col1 = new DataColumn();
            //DataColumn col2 = new DataColumn();
            //table.Columns.Add(col1);
            //table.Columns.Add(col2);

            //DataRow hrow = table.NewRow();
            //hrow[0] = "Faixa Etária";
            //hrow[1] = "Qtd";
            //table.Rows.Add(hrow);

            //foreach (object o in resultados)
            //{
            //    DataRow row = table.NewRow();
            //    FaixaEtaria fe = new FaixaEtaria();

            //    fe = Factory.GetInstance<IFaixaEtaria>().BuscarPorCodigo<FaixaEtaria>(int.Parse(((object[])o)[0].ToString()));
            //    row[0] = fe.ToString();
            //    row[1] = ((object[])o)[1].ToString();
            //    table.Rows.Add(row);
            //}

            //GridViewAtendimentoFaixa.DataSource = table;
            DSAtendimentoFaixa dsconteudo = new DSAtendimentoFaixa();
            dsconteudo.Tables.Add(iRelatorio.ObterRelatorioAtendimentoFaixa(Request["co_unidade"].ToString(), sexo, data_Inicio, data_Fim));

            ReportDocument doc = new ReportDocument();
            doc.Load(Server.MapPath("RelatoriosCrystal/RelAtendimentoFaixa.rpt"));
            doc.SetDataSource(dsconteudo.Tables[1]);
            doc.Subreports[0].SetDataSource(dscabecalho.Tables[1]);

            Session["documentoImpressaoUrgencia"] = doc;
            Response.Redirect("FormImprimirCrystalReports.aspx");

            //CrystalReportViewer_AtendimentoFaixa.ReportSource = doc;
            //CrystalReportViewer_AtendimentoFaixa.DataBind();

            //GridViewAtendimentoFaixa.DataSource = iRelatorio.getRelatorioAtendimentoFaixa(id_unidade, sexo, data_Inicio, data_Fim);
            //GridViewAtendimentoFaixa.DataBind();
        }
    }
}
