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

namespace ViverMais.View
{
    public partial class WUC_MensagemIE : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpBrowserCapabilities browser = Request.Browser;

                if (browser.Browser == "IE")
                {
                    double version = (float)(browser.MajorVersion + browser.MinorVersion);

                    if (version >= 7.0)
                        this.PanelMensagem.Visible = true;
                }
            }
        }
    }
}