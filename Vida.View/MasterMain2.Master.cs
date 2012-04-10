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
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.DAO;
using System.Collections.Generic;
using System.Xml;
using ViverMais.Model;

namespace ViverMais.View
{
    public partial class MasterMain2 : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //Resolve o problema de inclusão de arquivos pós renderização
            HtmlGenericControl js = new HtmlGenericControl("script");
            js.Attributes["type"] = "text/javascript";
            js.Attributes["src"] = Request.Url.ToString().Replace(Request.RawUrl, "") + Request.ApplicationPath + "JavaScript/util.js";
            this.Page.Header.Controls.Add(js);

            js = new HtmlGenericControl("script");
            js.Attributes["type"] = "text/javascript";
            js.Attributes["src"] = Request.Url.ToString().Replace(Request.RawUrl, "") + Request.ApplicationPath + "JavaScript/jquery-1.4.2.js";
            this.Page.Header.Controls.Add(js);

            js = new HtmlGenericControl("script");
            js.Attributes["type"] = "text/javascript";
            js.Attributes["src"] = Request.Url.ToString().Replace(Request.RawUrl, "") + Request.ApplicationPath + "JavaScript/MascarasGerais.js";
            this.Page.Header.Controls.Add(js);

            js = new HtmlGenericControl("script");
            js.Attributes["type"] = "text/javascript";
            js.Attributes["src"] = Request.Url.ToString().Replace(Request.RawUrl, "") + Request.ApplicationPath + "JavaScript/script.js";
            this.Page.Header.Controls.Add(js);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["Usuario"];

            if (usuario != null)
            {
                if (!IsPostBack)
                    this.CarregaMenuPrincipal(usuario.Codigo);
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        private string RetornaNavegador(string url, string texto)
        {
            string direcional = string.Empty;
            direcional += @"<li class='botoes_ViverMais'><a href='" + url + "'>" + texto + "</a></li>";
            direcional += "<li class='separator'></li>";

            return direcional;
        }

        private string RetornaNavegador(string url, string texto, string img)
        {
            string direcional = string.Empty;
            direcional += @"<li class='botoes_ViverMais'><a href='" + url + "'>" + texto + "</a></li>";
            direcional += "<li style='float:left'></li>";

            return direcional;
        }

        private void CarregaMenuPrincipal(int co_usuario)
        {
            string menu = string.Empty;
            string links = string.Empty;
            int tamanho_div = 75, tamanho_max_div = 678, valor_div = 0;
            string urlpart = Request.Url.ToString().Replace(Request.RawUrl, "") + Request.ApplicationPath;

            links += this.RetornaNavegador(urlpart + "Home.aspx", "Principal");
            valor_div += tamanho_div;

            links += this.RetornaNavegador(urlpart + "Home.aspx", "Regulação");
            valor_div += tamanho_div;

            links += this.RetornaNavegador(urlpart + "Home.aspx", "EAS");
            valor_div += tamanho_div;

            links += this.RetornaNavegador(urlpart + "Home.aspx", "Farmácia");
            valor_div += tamanho_div;

            links += this.RetornaNavegador(urlpart + "Home.aspx", "Ouvidoria");
            valor_div += tamanho_div;

            links += this.RetornaNavegador(urlpart + "Home.aspx", "Pacientes");
            valor_div += tamanho_div;

            links += this.RetornaNavegador(urlpart + "Home.aspx", "Profissionais");
            valor_div += tamanho_div;

            links += this.RetornaNavegador(urlpart + "Home.aspx", "Urgência");
            valor_div += tamanho_div;

            links += this.RetornaNavegador(urlpart + "Home.aspx", "Vacina");
            valor_div += tamanho_div;

            links += this.RetornaNavegador(urlpart + "Home.aspx", "Segurança");
            valor_div += tamanho_div;

            links += this.RetornaNavegador(urlpart + "Home.aspx", "Fale Conosco");
            valor_div += tamanho_div;

            links += this.RetornaNavegador(urlpart + "Home.aspx", "Sair");
            valor_div += tamanho_div;


            if (valor_div > tamanho_max_div)
                valor_div = tamanho_max_div;

          

            menu += "<div id='barra_informativo'>";
            menu += "<div id='fotos' class='sc_menu' style='width:" + valor_div + "px;'>";
            menu += "<ul id='fotos1' class='sc_menu'>";
            menu += links;
            menu += "</ul>";
            menu += "</div>";
            menu += "<li class='sombra_dir'></li>";
            menu += "</div>";



            Literal_Menu.Text = menu;
            Literal_Menu.DataBind();
            //fotos.Style.Add("width", tamanho_total_div.ToString());
            //fotos.DataBind();
        }

        protected void MenuViverMais_Load(object sender, EventArgs e)
        {

        }

        public void RemovePermissaoMenu(MenuItemCollection items)
        {
            ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
            IList<int> indices = new List<int>();
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i].ChildItems.Count > 0)
                {
                    RemovePermissaoMenu(items[i].ChildItems);
                }

                if (items[i].Value != "EMPTY" && !iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, items[i].Value.Split(':')[0].ToString(), int.Parse(items[i].Value.Split(':')[1].ToString())))
                {
                    indices.Add(i);
                }
            }
            foreach (int i in indices)
            {
                items.RemoveAt(i);
            }
        }

        protected void MenuViverMais_MenuItemClick(object sender, MenuEventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("~/Home.aspx");
        }

        protected void LoginName1_DataBinding(object sender, EventArgs e)
        {
        }

        protected override void AddedControl(Control control, int index)
        {
            if (Request.ServerVariables["http_user_agent"].IndexOf("Chrome", StringComparison.CurrentCultureIgnoreCase) != -1)
                this.Page.ClientTarget = "uplevel";

            base.AddedControl(control, index);
        }
    }
}
