﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using System.Data;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.View.Urgencia.RelatoriosCrystal;
using System.Collections;

namespace ViverMais.View.Urgencia
{
    public partial class FormImprimirAtestadoReceita : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            long temp;
            if (Request["co_atestadoreceita"] != null && long.TryParse(Request["co_atestadoreceita"].ToString(), out temp))
            {
                //ViewState["co_atestadoreceita"] = temp;
                DataTable tab = Factory.GetInstance<IProntuario>().RetornaAtestadoReceita(long.Parse(Request["co_atestadoreceita"].ToString()));

                DataList_AtestadoReceita.DataSource = tab;
                DataList_AtestadoReceita.DataBind();
                //Repeater_AtestadoReceita.DataSource = tab;
                //Repeater_AtestadoReceita.DataBind();
                //Hashtable tab = Factory.GetInstance<IProntuario>().RetornaAtestadoReceita(long.Parse(Request["co_atestadoreceita"].ToString()));

                //DSRelAtestadoReceitaProntuario dsatestadoreceita = new DSRelAtestadoReceitaProntuario();
                //dsatestadoreceita.Tables.Add((DataTable)tab["atestadoreceita"]);
                //DSCabecalho dscabecalho = new DSCabecalho();
                //dscabecalho.Tables.Add((DataTable)tab["cabecalho"]);

                //ReportDocument doc = new ReportDocument();

                //doc.Load(Server.MapPath("RelatoriosCrystal/RelAtestadoReceitaProntuario.rpt"));
                //doc.Database.Tables[0].SetDataSource(dsatestadoreceita.Tables[1]);
                //doc.Subreports[0].SetDataSource(dscabecalho.Tables[1]);
                //CrystalReportViewer_AtestadoReceita.ReportSource = doc;
                //CrystalReportViewer_AtestadoReceita.DataBind();
            }
        }

        //protected void OnItemDataBound_ConfigurarItem(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item)
        //    {
        //        System.Web.UI.WebControls.Table tab = (System.Web.UI.WebControls.Table)e.Item.FindControl("Tabela_AtestadoReceita");
        //        AtestadoReceitaUrgence documento = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<AtestadoReceitaUrgence>(long.Parse(ViewState["co_atestadoreceita"].ToString()));
                
        //        switch (documento.TipoAtestadoReceita.Codigo)
        //        {
        //            case 1:
        //                tab.BackImageUrl = "~/Urgencia/img/topo_receita.jpg";
        //                break;
        //            case 2:
        //                tab.BackImageUrl = "~/Urgencia/img/topo_atestado.jpg";
        //                break;
        //            default:
        //                tab.BackImageUrl = "~/Urgencia/img/topo_comparecimento.jpg";
        //                break;
        //        }

        //        Label labeldata = (Label)tab.Rows[2].Cells[0].FindControl("Label_DataDocumento");
        //        labeldata.Text = "Salvador, " + DateTime.Now.ToString("dd/MM/yyyy");
        //    }
        //}

        protected void OnItemDataBound_ConfigurarDataList(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item)
            {
                System.Web.UI.WebControls.Table tab = (System.Web.UI.WebControls.Table)e.Item.FindControl("Tabela_AtestadoReceita");
                //AtestadoReceitaUrgence documento = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<AtestadoReceitaUrgence>(long.Parse(DataList_AtestadoReceita.DataKeys[e.Item.ItemIndex].ToString()));
                Label lbTipoDocumento = (Label)e.Item.FindControl("TipoDocumento");

                if (lbTipoDocumento.Text == "Receita")
                {
                    ((Image)this.FindControl(tab, "ImagemTopo")).ImageUrl = "~/Urgencia/img/topo_receita.jpg";
                    this.FindControl(tab, "LabelSiglaReceita").Visible = true;
                }
                else
                {
                    if (lbTipoDocumento.Text == "Atestado")
                        ((Image)this.FindControl(tab, "ImagemTopo")).ImageUrl = "~/Urgencia/img/topo_atestado.jpg";
                    else
                        ((Image)this.FindControl(tab, "ImagemTopo")).ImageUrl = "~/Urgencia/img/topo_comparecimento.jpg";
                }

                lbTipoDocumento.Visible = false;
                Label labeldata = (Label)this.FindControl(tab, "Label_DataDocumento");
                labeldata.Text = "Salvador, " + DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        private Control FindControl(System.Web.UI.WebControls.Table tab, string id_controle)
        {
            for (int linha = 0; linha < tab.Rows.Count; linha++)
            {
                TableRow row = tab.Rows[linha];

                for (int coluna = 0; coluna < row.Cells.Count; coluna++)
                {
                    Control control = row.Cells[coluna].FindControl(id_controle);

                    if (control != null)
                        return control;
                }
            }

            return null;
        }
    }
}
