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
using System.Text;

namespace ViverMais.View.Laboratorio
{
    public partial class MasterLaboratorio : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
                this.CarregaMenuModulo();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void CarregaMenuModulo()
        {
            Literal menumodulo = (Literal)((MasterMain)Master).FindControl("barramodulo");
            StringBuilder textomenu = new StringBuilder();
            //string app = ((Request.ApplicationPath.Equals("/") || string.IsNullOrEmpty(Request.ApplicationPath)) ? "/" : Request.ApplicationPath + "/");
            //string urlpart = Request.Url.ToString().Replace(Request.RawUrl, "") + app;

            //string suburlpart = Request.RawUrl.Split('/')[1];

            HyperLink navegador = (HyperLink)((MasterMain)Master).FindControl("HyperLink_Modulo");
            navegador.Text = "Módulo de laboratório";
            navegador.NavigateUrl = "~/Laboratorio/Default.aspx"; //Página default do módulo

            textomenu.Append("<div class=\"menu-barra-module\" style=\"background: url('" + HelperRedirector.URLRelativaAplicacao() + "Laboratorio/img/barramenu.png' ) no-repeat;\">");
            textomenu.Append(this.DivMenu.InnerText);
            textomenu.Append("</div>");

            menumodulo.Text = textomenu.ToString();
            menumodulo.DataBind();
        }
    }
}
