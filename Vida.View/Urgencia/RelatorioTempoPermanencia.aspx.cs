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
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using CrystalDecisions.CrystalReports.Engine;
using ViverMais.View.Urgencia.RelatoriosCrystal;

namespace ViverMais.View.Urgencia
{
    public partial class RelatorioTempoPermanencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dataInicial = DateTime.Parse(Request.QueryString["data_inicial"]);
            DateTime dataFinal = DateTime.Parse(Request.QueryString["data_final"]);
            string id_unidade = Request.QueryString["id_unidade"];
            IRelatorioUrgencia irelatorio = Factory.GetInstance<IRelatorioUrgencia>();
            Hashtable hash = irelatorio.ObterRelatorioTempoPermanencia(dataInicial, dataFinal, id_unidade);

            DSCabecalhoTempoPermanencia dscabecalho = new DSCabecalhoTempoPermanencia();
            dscabecalho.Tables.Add((DataTable)hash["cabecalho"]);
            DSCorpoTempoPermanencia dscorpo = new DSCorpoTempoPermanencia();
            dscorpo.Tables.Add((DataTable)hash["corpo"]);

            ReportDocument doc = new ReportDocument();
            doc.Load(Server.MapPath("RelatoriosCrystal/RelTempoPermanencia.rpt"));
            doc.SetDataSource(dscabecalho.Tables[1]);
            doc.Subreports[0].SetDataSource(dscorpo.Tables[1]);

            Session["documentoImpressaoUrgencia"] = doc;
            Response.Redirect("FormImprimirCrystalReports.aspx");

            //CrystalReportViewer_TempoPermanencia.ReportSource = doc;
            //CrystalReportViewer_TempoPermanencia.DataBind();

            //DataTable table = new DataTable();
            //DataColumn c0 = new DataColumn("Prontuario");
            //DataColumn c1 = new DataColumn("Paciente");
            //DataColumn c2 = new DataColumn("Permanencia");
            //table.Columns.Add(c0);
            //table.Columns.Add(c1);
            //table.Columns.Add(c2);

            //IList result = irelatorio.ObterRelatorioTempoPermanencia(dataInicial, dataFinal, id_unidade);
            //foreach (object obj in result)
            //{
            //    DataRow row = table.NewRow();
            //    object[] valor = (object[])obj;
            //    //int dias = 0;
            //    ViverMais.Model.Prontuario prontuario = Factory.GetInstance<IProntuario>().BuscarPorCodigo<ViverMais.Model.Prontuario>(long.Parse(valor[0].ToString()));

            //    row[0] = prontuario.NumeroToString;

            //    if (!string.IsNullOrEmpty(prontuario.Paciente.CodigoViverMais))
            //    {
            //        ViverMais.Model.Paciente pac = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(prontuario.Paciente.CodigoViverMais);
            //        row[1] = pac != null ? pac.Nome : "Não Identificado";
            //    }
            //    else 
            //    {
            //        if (string.IsNullOrEmpty(prontuario.Paciente.Nome))
            //            row[1] = "Não Identificado";
            //        else
            //            row[1] = prontuario.Paciente.Nome;
            //    }

            //    TimeSpan diferenca;

            //    if (valor[2] != null) //Saída menos entrada
            //    {
            //        diferenca = DateTime.Parse(valor[2].ToString()).Subtract(DateTime.Parse(valor[1].ToString()));
            //        row[2] = diferenca.Days == 0 ? "1" : diferenca.Days + " dias" + (diferenca.Hours % 24) + " e horas";
            //    }else //Alteração Enfermagem menos entrada
            //    {
            //        row[2] = " - ";
            //    }
                
            //    table.Rows.Add(row);
            //}

            //GridView_TempoPermanencia.DataSource = table;
            //GridView_TempoPermanencia.DataBind();
        }
    }
}
