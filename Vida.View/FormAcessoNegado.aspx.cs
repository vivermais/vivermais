﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ViverMais.View
{
    public partial class FormAcessoNegado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.LiteralMensagem.Text = string.Empty;

                if (Application["AcessoPagina"] != null)
                    this.LiteralMensagem.Text = Application["AcessoPagina"].ToString();
            }
        }
    }
}
