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
using System.Drawing.Printing;
using CrystalDecisions.Shared;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace ViverMais.View.Vacina
{
    public partial class FormImprimirReciboDispensacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int co_dispensacao;

            if (!IsPostBack)
            {
                if (Request["co_dispensacao"] != null && int.TryParse(Request["co_dispensacao"].ToString(), out co_dispensacao))
                {
                    ReportDocument doc = Factory.GetInstance<IRelatorioVacina>().ObterRelatorioDispensacao(co_dispensacao);

                    //Hashtable hash = Factory.GetInstance<IRelatorioVacina>().ObterRelatorioDispensacao(temp);
                    
                    //DataRow row = ((DataTable)hash["cabecalho"]).Rows[0];
                    //this.Label_UnidadeSaude.Text = row["UnidadeSaude"].ToString();
                    //this.Label_Paciente.Text = row["Nome"].ToString();
                    //this.Label_CartaoSUS.Text = row["CartaoSUS"].ToString();
                    //this.Label_Data.Text = row["Data"].ToString();

                    ////this.DataListCabecalho.DataSource = (DataTable)hash["cabecalho"];
                    ////this.DataListCabecalho.DataBind();

                    //Session["corpodispensacao"] = (DataTable)hash["corpo"];
                    //this.DataListCorpo.DataSource = (DataTable)hash["corpo"];
                    //this.DataListCorpo.DataBind();
                    ////Label label = new Label();
                    ////label.Font.Bold = true;

                    ////TableRow r0 = new TableRow();
                    ////TableCell c0 = new TableCell();
                    ////c0.Font.Bold = true;
                    ////c0.Font.Size = FontUnit.Point(10);
                    ////c0.Text = "Prefeitura do Salvador";
                    ////c0.HorizontalAlign = HorizontalAlign.Center;
                    ////r0.Cells.Add(c0);
                    ////Table1.Rows.Add(r0);
                    ////r0 = new TableRow();
                    ////c0 = new TableCell();
                    ////c0.Font.Bold = true;
                    ////c0.Font.Size = FontUnit.Point(10);
                    ////c0.Text = "Secretaria Municial de Saúde <br/>";
                    ////c0.HorizontalAlign = HorizontalAlign.Center;
                    ////r0.Cells.Add(c0); Table1.Rows.Add(r0);
                    ////r0 = new TableRow();
                    ////c0 = new TableCell();
                    ////c0.Font.Bold = true;
                    ////c0.Font.Size = FontUnit.Point(9);
                    ////c0.Text = "ViverMais - Sistema Integrado em Saúde Pública<br/><br/>";
                    ////c0.HorizontalAlign = HorizontalAlign.Center;
                    ////r0.Cells.Add(c0);
                    ////Table1.Rows.Add(r0);
                    ////DataTable tableHead = (DataTable)hash["cabecalho"];
                    ////foreach (var item in tableHead.Rows.OfType<DataRow>())
                    ////{
                    ////    r0 = new TableRow();
                    ////    c0 = new TableCell();
                    ////    c0.Font.Bold = true;
                    ////    c0.Font.Size = FontUnit.Point(9);
                    ////    c0.Text = item[0].ToString() + "<br /><br />";
                    ////    c0.Text = item[0].ToString();
                    ////    c0.HorizontalAlign = HorizontalAlign.Center;
                    ////    r0.Cells.Add(c0);

                    ////    TableRow r1 = new TableRow();
                    ////    TableCell c1 = new TableCell();
                    ////    c1.Font.Size = FontUnit.Point(8); label.Text = "Paciente: ";
                    ////    c1.Font.Bold = true;
                    ////    c1.Text = label.Text + item[1].ToString();
                    ////    r1.Cells.Add(c1);

                    ////    TableRow r2 = new TableRow();
                    ////    TableCell c2 = new TableCell();
                    ////    c2.Font.Size = FontUnit.Point(8); label.Text = "CNS: ";
                    ////    c2.Text = label.Text + item[2].ToString();
                    ////    r2.Cells.Add(c2);

                    ////    TableRow r3 = new TableRow();
                    ////    TableCell c3 = new TableCell();
                    ////    c3.Font.Size = FontUnit.Point(8); label.Text = "Data: ";
                    ////    c3.Text = label.Text + item[3].ToString();
                    ////    r3.Cells.Add(c3);

                    ////    TableRow rX = new TableRow();
                    ////    TableCell cX = new TableCell();
                    ////    cX.Font.Size = FontUnit.Point(8);
                    ////    cX.Text = "<br/>";
                    ////    rX.Cells.Add(cX);

                    ////    Table1.Rows.Add(r0);
                    ////    Table1.Rows.Add(r1);
                    ////    Table1.Rows.Add(r2);
                    ////    Table1.Rows.Add(r3);
                    ////    Table1.Rows.Add(rX);
                    ////}

                    ////DataTable tableBody = (DataTable)hash["corpo"];
                    ////foreach (var item in tableBody.Rows.OfType<DataRow>())
                    ////{
                    ////    r0 = new TableRow();
                    ////    c0 = new TableCell();
                    ////    c0.Font.Bold = true;
                    ////    c0.Font.Size = FontUnit.Point(8);
                    ////    c0.Text = "VACINA: " + item[0].ToString();
                    ////    r0.Cells.Add(c0);

                    ////    TableRow r1 = new TableRow();
                    ////    TableCell c1 = new TableCell();
                    ////    c1.Font.Size = FontUnit.Point(8); label.Text = "LOTE: ";
                    ////    c1.Text = label.Text + item[1].ToString();
                    ////    r1.Cells.Add(c1);

                    ////    TableRow r4 = new TableRow();
                    ////    TableCell c4 = new TableCell();
                    ////    c4.Font.Size = FontUnit.Point(8); label.Text = "FABRICANTE: ";
                    ////    c4.Text = label.Text + item[2].ToString();
                    ////    r4.Cells.Add(c4);

                    ////    TableRow r5 = new TableRow();
                    ////    TableCell c5 = new TableCell();
                    ////    c5.Font.Size = FontUnit.Point(8); label.Text = "VALIDADE: ";
                    ////    c5.Text = label.Text + item[3].ToString();
                    ////    r5.Cells.Add(c5);

                    ////    TableRow r2 = new TableRow();
                    ////    TableCell c2 = new TableCell();
                    ////    c2.Font.Size = FontUnit.Point(8); label.Text = "DOSE: ";
                    ////    c2.Text = label.Text + item[4].ToString();
                    ////    r2.Cells.Add(c2);

                    ////    TableRow r3 = new TableRow();
                    ////    if (!item[5].ToString().Equals("NoRegisters"))
                    ////    {
                    ////        TableCell c3 = new TableCell();
                    ////        label.Text = "PRÓXIMA: ";
                    ////        c3.Text = label.Text + item[5].ToString();
                    ////        c3.Font.Size = FontUnit.Point(8);
                    ////        r3.Cells.Add(c3);
                    ////    }
                    ////    TableRow rX = new TableRow();
                    ////    TableCell cX = new TableCell();
                    ////    cX.Font.Size = FontUnit.Point(8); cX.Text = "<br />";
                    ////    rX.Cells.Add(cX);

                    ////    Table1.Rows.Add(r0);
                    ////    Table1.Rows.Add(r2);
                    ////    if (!item[5].ToString().Equals("NoRegisters"))
                    ////        Table1.Rows.Add(r3);

                    ////    Table1.Rows.Add(r1);
                    ////    Table1.Rows.Add(r4);
                    ////    Table1.Rows.Add(r5);

                    ////    Table1.Rows.Add(rX);
                    ////}

                    ////r0 = new TableRow();
                    ////c0 = new TableCell();
                    ////c0.Font.Size = FontUnit.Point(8);
                    ////c0.Text = "Acesse seu Cartão de Vacina pelo site:";
                    ////r0.Cells.Add(c0);
                    ////Table1.Rows.Add(r0);

                    ////r0 = new TableRow();
                    ////c0 = new TableCell();
                    ////c0.Font.Size = FontUnit.Point(8);
                    ////c0.Text = "www.saude.salvador.ba.gov.br/ViverMais";
                    ////r0.Cells.Add(c0);
                    ////Table1.Rows.Add(r0);

                    ////int count = 0;
                    ////while (count < (tableBody.Rows.Count * 2))
                    ////{
                    ////    TableRow r = new TableRow();
                    ////    TableCell c = new TableCell();
                    ////    c.Font.Size = FontUnit.Point(8); c.Text = "<br/>";
                    ////    r.Cells.Add(c);

                    ////    Table1.Rows.Add(r);
                    ////    count++;
                    ////}
                    //DESCOMENTAR DAQUI PARA CIMA

                    //DSCabecalhoDispensacaoVacina cabecalho = new DSCabecalhoDispensacaoVacina();
                    //DSCorpoDispensacaoVacina corpo = new DSCorpoDispensacaoVacina();

                    //cabecalho.Tables.Add((DataTable)hash["cabecalho"]);
                    //corpo.Tables.Add((DataTable)hash["corpo"]);

                    //ReportDocument doc = new ReportDocument();
                    //doc.Load(Server.MapPath("RelatoriosCrystal/RelDispensacaoVacina.rpt"));
                    //doc.SetDataSource(cabecalho.Tables[1]);
                    //doc.Subreports[0].SetDataSource(corpo.Tables[1]);

                    Session["documentoImpressaoVacina"] = doc;
                    Response.Redirect("FormMostrarRelatorioCrystalImpressao.aspx?tipo=etiqueta&nome_arquivo=dispensacao.pdf");
                }
            }
        }

        protected void OnItemDataBound_CarregaItensDispensacao(object sender, DataListItemEventArgs e)
        {
            DataTable corpo = (DataTable)Session["corpodispensacao"];
            if (!corpo.Rows[e.Item.ItemIndex]["ProximaVacina"].ToString().Equals("NoRegisters"))
                e.Item.FindControl("Panel_ProximaDose").Visible = true;
        }
    }
}
