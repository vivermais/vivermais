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
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.DAO;
using CrystalDecisions.CrystalReports.Engine;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;
using ViverMais.View.Agendamento.RelatoriosCrystal;


namespace ViverMais.View.Agendamento
{
    public partial class RelatorioAgendaPrestador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "RELATORIO_AGENDA_PRESTADOR", Modulo.AGENDAMENTO))
                {
                    CriaPDF();
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        protected void CriaPDF()
        {
            Hashtable hash = (Hashtable)Session["HashAgendaPrestador"];
            
            //CrystalReportViewer_AgendaPrestador.DisplayToolbar = true;
            //CrystalReportViewer_AgendaPrestador.EnableDatabaseLogonPrompt = false;
            
            DSCabecalhoAgendaPrestador cabecalho = new DSCabecalhoAgendaPrestador();
            cabecalho.Tables.Add((DataTable)hash["cabecalho"]);
            
            DSRelatorioAgendaPrestador conteudo = new DSRelatorioAgendaPrestador();
            conteudo.Tables.Add((DataTable)hash["corpo"]);

            ReportDocument repDoc = new ReportDocument();
            repDoc.Load(Server.MapPath("RelatoriosCrystal/CrystalReportViewer_AgendaPrestador.rpt"));
            repDoc.SetDataSource(conteudo.Tables[1]);
            repDoc.Subreports["CrystalReportViewer_CabecalhoAgendaPrestador.rpt"].SetDataSource(cabecalho.Tables[1]);
            
            
            //repDoc.Database.Tables["CabecalhoAgendaPrestador"].SetDataSource((DataTable)hash["cabecalho"]);
            //repDoc.Database.Tables["RelatorioAgendaPrestador"].SetDataSource((DataTable)hash["corpo"]);

            System.IO.Stream s = repDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "applicattion/octect-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=RelatorioAgendaPrestador.pdf");
            Response.AddHeader("Content-Length", s.Length.ToString());
            Response.BinaryWrite(((System.IO.MemoryStream)s).ToArray());
            Response.End();
            Session.Remove("HashAgendaPrestador");
        }
    }
}
