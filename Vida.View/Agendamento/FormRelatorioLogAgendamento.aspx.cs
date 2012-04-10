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
using System.IO;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;

namespace ViverMais.View.Agendamento
{
    public partial class FormRelatorioLogAgendamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "RELATORIO_LOG", Modulo.AGENDAMENTO))
                //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        private void CriaArquivoExcel()
        {
            //MemoryStream ms = Factory.GetInstance<IRelatorioAgendamento>().RelatorioSolicitacoesDesmarcadas();
            MemoryStream ms = Factory.GetInstance<IRelatorioAgendamento>().Log(DateTime.Parse(tbxDataInicial.Text), DateTime.Parse(tbxDataFinal.Text), tbxCartaoSUS.Text);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", "RelatorioSolicitacoesDesmarcadas.xls"));
            Response.Clear();

            Response.BinaryWrite((ms).GetBuffer());
            Response.End();
        }

        protected void btnImprimir_OnClick(object sender, EventArgs e)
        {
            CriaArquivoExcel();
        }
    }
}
