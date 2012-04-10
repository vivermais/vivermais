using System;
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
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace ViverMais.View.GuiaProcedimentos
{
    public partial class MasterCatalogo : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "ViverMais - Módulo de Catálogo";
            
            if (!IsPostBack)
            {
                Usuario usuario = Session["Usuario"] as Usuario;
                this.CarregaMenuModulo(usuario);
            }
        }

        private void CarregaMenuModulo(Usuario usuario)
        {
            Literal menumodulo = (Literal)((MasterMain)Master).FindControl("barramodulo");
            StringBuilder textomenu = new StringBuilder();
            //string app = ((Request.ApplicationPath.Equals("/") || string.IsNullOrEmpty(Request.ApplicationPath)) ? "/" : Request.ApplicationPath + "/");
            //string urlpart = Regex.Replace(HttpUtility.UrlDecode(Request.Url.ToString()), @"\s", "").
            //                Replace(Regex.Replace(HttpUtility.UrlDecode(Request.RawUrl), @"\s", ""), "") + app;
            //string urlpart = Request.Url.ToString().Replace(Request.RawUrl, "") + app;
            //string suburlpart = Request.RawUrl.Split('/')[1];

            HyperLink navegador = (HyperLink)((MasterMain)Master).FindControl("HyperLink_Modulo");
            navegador.Text = "Guia de Procedimentos";
            navegador.NavigateUrl = "~/GuiaProcedimentos/Default.aspx"; //Página default do módulo

            XmlDocument xml = new XmlDocument();
            xml.Load(Server.MapPath("~/GuiaProcedimentos/Menu.xml"));
            menumodulo.Text = Factory.GetInstance<ISeguranca>().RetornaMenuModulo(xml, HelperRedirector.URLRelativaAplicacao(), usuario.Codigo, Modulo.GUIAPROCEDIMENTOS);

            //menumodulo.Text = textomenu.ToString();
            menumodulo.DataBind();

            //textomenu.Append("<div class=\"menu-barra-module\" style=\"background: url('" + urlpart + "Agendamento/img/barramenu.png' ) no-repeat;\">");
            //textomenu.Append(this.DivMenu.InnerText);
            //textomenu.Append("</div>");

            //menumodulo.Text = textomenu.ToString();
            //menumodulo.DataBind();
        }
    }
}
