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

namespace ViverMais.View.Agendamento
{
    public partial class RelatorioVagasAgendadasBasico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CriaPDF();
        }

        protected void CriaPDF()
        {
            System.IO.Stream s = (System.IO.Stream)Session["StreamRelatorioVagas"];
            if (s != null)
            {
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "applicattion/octect-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=RelatorioVagasAgendadoBasico.pdf");
                Response.AddHeader("Content-Length", s.Length.ToString());
                Response.BinaryWrite(((System.IO.MemoryStream)s).ToArray());
                Response.End();
            }
        }
    }
}
