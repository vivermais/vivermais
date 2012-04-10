using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Vacina;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using ViverMais.View.Vacina.RelatoriosCrystal;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;

namespace ViverMais.View.Vacina
{
    public partial class FormRelatorioInventario : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CONSULTAR_INVENTARIO_VACINA",Modulo.VACINA))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            else
            {
                int temp;
                string[] procura = { "conferencia", "final" };
                if (Request["co_inventario"] != null && int.TryParse(Request["co_inventario"].ToString(), out temp)
                    && Request["tipo"] != null && procura.Contains(Request["tipo"].ToString()))
                {
                    ReportDocument doc = new ReportDocument();
                    Hashtable hash = null;

                    if (Request["tipo"].ToString().Equals("conferencia"))
                    {
                        hash = Factory.GetInstance<IRelatorioVacina>().RelatorioConferenciaInventario(temp);
                        doc.Load(Server.MapPath("RelatoriosCrystal/RelInventarioConferenciaVacina.rpt"));

                        DSCabecalhoInventarioVacina dscabecalho = new DSCabecalhoInventarioVacina();
                        dscabecalho.Tables.Add((DataTable)hash["cabecalho"]);

                        DSRelInventarioConferenciaVacina dscorpo = new DSRelInventarioConferenciaVacina();
                        dscorpo.Tables.Add((DataTable)hash["corpo"]);

                        doc.SetDataSource(dscorpo.Tables[1]);
                        doc.Subreports[0].SetDataSource(dscabecalho.Tables[1]);
                    }
                    else
                    {
                        hash = Factory.GetInstance<IRelatorioVacina>().RelatorioFinalInventario(temp);
                        doc.Load(Server.MapPath("RelatoriosCrystal/RelInventarioFinalVacina.rpt"));

                        DSCabecalhoInventarioVacina dscabecalho = new DSCabecalhoInventarioVacina();
                        dscabecalho.Tables.Add((DataTable)hash["cabecalho"]);

                        DSRelInventarioFinal dscorpo = new DSRelInventarioFinal();
                        dscorpo.Tables.Add((DataTable)hash["corpo"]);

                        doc.SetDataSource(dscorpo.Tables[1]);
                        doc.Subreports[0].SetDataSource(dscabecalho.Tables[1]);
                    }

                    CrystalReportViewer_RelatorioInventario.ReportSource = doc;
                    CrystalReportViewer_RelatorioInventario.DataBind();
                }
            }
        }
    }
}
