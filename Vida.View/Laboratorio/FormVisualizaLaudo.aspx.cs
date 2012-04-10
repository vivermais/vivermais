using System;
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
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Laboratorio;
using System.Text;
using System.Net;
using System.IO;

namespace ViverMais.View.Laboratorio
{
    public partial class FormVisualizaLaudo : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            MemoryStream memoryStream = (MemoryStream)Session["memoryStreamLaudo"];

            Response.AddHeader("Content-Disposition", "inline;filename=Laudo.pdf");
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "applicattion/pdf";
            Response.AddHeader("Content-Disposition", "inline;filename=Laudo.pdf");

            Response.BinaryWrite(memoryStream.ToArray());
            Response.Flush();
            Response.End();
        }
    }
}
