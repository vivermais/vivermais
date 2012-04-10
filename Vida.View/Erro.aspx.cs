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

namespace ViverMais.View
{
    public partial class Erro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Application["ErroAplicacao"] != null)
            {
                ViverMais.Model.ErroViverMais erro = (ViverMais.Model.ErroViverMais)Application["ErroAplicacao"];
                Label_Erro.Text = erro.Exception.Message;
                Label_Pagina.Text = erro.Pagina;
                Label_StackTrace.Text = erro.Exception.StackTrace;
            }
            //Exception ex = Server.GetLastError();
            //if(ex != null)
                //lblMensagem.Text = Server.GetLastError().GetBaseException().Message.ToString() + " - " + Request.Url.ToString();
        }
    }
}
