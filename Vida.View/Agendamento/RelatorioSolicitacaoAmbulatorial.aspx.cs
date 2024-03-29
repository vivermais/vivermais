﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ViverMais.View.Agendamento.RelatoriosCrystal;
using CrystalDecisions.CrystalReports.Engine;

namespace ViverMais.View.Agendamento
{
    public partial class RelatorioSolicitacaoAmbulatorial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CriaPDF();
        }

        protected void CriaPDF()
        {
            Hashtable hash = (Hashtable)Session["HashSolicitacoes"];

            DSCabecalhoSolicitacaoAmbulatorial cabecalho = new DSCabecalhoSolicitacaoAmbulatorial();
            cabecalho.Tables.Add((DataTable)hash["cabecalho"]);


            DSRelatorioSolicitacaoAmbulatorial conteudo = new DSRelatorioSolicitacaoAmbulatorial();
            conteudo.Tables.Add((DataTable)hash["corpo"]);

            Session.Remove("HashSolicitacoes");
            ReportDocument repDoc = new ReportDocument();
            repDoc.Load(Server.MapPath("RelatoriosCrystal/CrystalReportViewer_SolicitacaoAmbulatorial.rpt"));
            repDoc.SetDataSource(conteudo.Tables[1]);
            repDoc.Subreports["CrystalReportViewer_CabecalhoSolicitacaoAmbulatorial.rpt"].SetDataSource(cabecalho.Tables[1]);

            System.IO.Stream s = repDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("content-disposition", "attachment; filename=RelatorioSolicitacaoAmbulatorial.pdf");
            Response.AddHeader("content-length", ((System.IO.MemoryStream)s).ToArray().Length.ToString());
            Response.BinaryWrite(((System.IO.MemoryStream)s).ToArray());
            Response.End();
            //Response.Flush();
            //Response.Close();

            
        }
    }
}
