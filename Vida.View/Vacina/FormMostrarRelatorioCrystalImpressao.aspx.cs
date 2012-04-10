using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using System.IO;
using System.Drawing.Printing;
using CrystalDecisions.CrystalReports.Engine;

namespace ViverMais.View.Vacina
{
    public partial class FormMostrarRelatorioCrystalImpressao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ClearHeaders();
            Response.ClearContent();
            Response.Charset = "";
            Response.Clear();
            //Response.BufferOutput = true;
            //Response.AddHeader("Cache-Control", "Private");
            //Response.CacheControl = "public";
            //Response.AppendHeader("Content-Transfer-Encoding", "binary");
            //Response.AppendHeader("Accept-Ranges", "none");
            //Response.Cache.SetCacheability(HttpCacheability.Private);

            //if (!IsPostBack)
            //{
            ReportDocument doc = (ReportDocument)Session["documentoImpressaoVacina"];

            if (Request["tipo"] != null && Request["tipo"].ToString().Equals("etiqueta"))
            {
                PrintDocument pDoc = new PrintDocument();
                
                int i = 1;
                bool etiquetaEncontrada = false;

                foreach (string printname in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    pDoc.PrinterSettings.PrinterName = printname;

                    foreach (System.Drawing.Printing.PaperSize p in pDoc.PrinterSettings.PaperSizes)
                    {
                        if (p.PaperName == "ETIQUETA BEMATECH")
                        {
                            i = p.RawKind;
                            break;
                        }
                    }

                    if (etiquetaEncontrada)
                        break;
                }

                //PageMargins margs = new PageMargins();
                //margs.bottomMargin = 0;
                //margs.leftMargin = 0;
                //margs.rightMargin = 0;
                //margs.topMargin = 0;
 
                //.ApplyPageMargins(margs);

                //System.Drawing.Printing.PaperSize paper = new System.Drawing.Printing.PaperSize("", 8, 3);

                
            }
            
            doc.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)new System.Drawing.Printing.PaperSize("teste", 8, 3).RawKind;

            string nome_arquivo = "RelatorioVacina.pdf";

            if (Request["nome_arquivo"] != null)
                nome_arquivo = Request["nome_arquivo"].ToString();

            System.IO.Stream s = doc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //Response.Buffer = true;
            //Response.ClearContent();
            //Response.ClearHeaders();

            //Response.ContentType = "application/octet-stream";
            //Response.AddHeader("Content-Type", "application/x-download");
            Response.AddHeader("Content-Type", "application/octet-stream");
            //Response.AddHeader("Content-Type", "application/download");
            //Response.AddHeader("Content-Description", "File Transfer");
            Response.AddHeader("Content-Disposition", "attachment;filename=" + nome_arquivo);
            Response.AddHeader("Content-Length", s.Length.ToString());

            //Response.OutputStream.Write(((MemoryStream)s).ToArray(), 0, ((MemoryStream)s).ToArray().Length);

            Response.BinaryWrite(((MemoryStream)s).ToArray());
            Response.Flush();
            Response.End();

            HttpContext.Current.ApplicationInstance.CompleteRequest();
            //Response.Close();
        }
        //}
    }
}
