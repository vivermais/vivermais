﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace ViverMais.View.Vacina
{
    public partial class FormDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["doc_download"] != null && Session["doc_download"] is MemoryStream && !string.IsNullOrEmpty(Request["filename"]) && Request["filename"].ToString().Contains(".xls"))
                {
                    //Response.ContentType = "application/vnd.ms-excel";
                    MemoryStream m = ((MemoryStream)Session["doc_download"]);
                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", Request["filename"].ToString()));
                    
                    Response.AddHeader("Content-Length", m.Length.ToString());
                    Response.BinaryWrite(m.GetBuffer());
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }
}