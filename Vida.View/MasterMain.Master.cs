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
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.DAO;
using System.Collections.Generic;
using System.Xml;
using ViverMais.Model;
using System.Text.RegularExpressions;

namespace ViverMais.View
{
    public partial class MasterMain : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            string app = ((Request.ApplicationPath.Equals("/") || string.IsNullOrEmpty(Request.ApplicationPath)) ? "/" : Request.ApplicationPath + "/");
            string url = Regex.Replace(HttpUtility.UrlDecode(Request.Url.ToString()), @"\s", "");
            string urlraw = Regex.Replace(HttpUtility.UrlDecode(Request.RawUrl), @"\s", "");

            //Resolve o problema de inclusão de arquivos pós renderização
            HtmlGenericControl js = new HtmlGenericControl("script");
            js.Attributes["type"] = "text/javascript";
            js.Attributes["src"] = url.Replace(urlraw, "") + app + "JavaScript/util.js";
            this.Page.Header.Controls.Add(js);

            js = new HtmlGenericControl("script");
            js.Attributes["type"] = "text/javascript";
            js.Attributes["src"] = url.Replace(urlraw, "") + app + "JavaScript/jquery-1.4.2.js";
            this.Page.Header.Controls.Add(js);

            js = new HtmlGenericControl("script");
            js.Attributes["type"] = "text/javascript";
            js.Attributes["src"] = url.Replace(urlraw, "") + app + "JavaScript/jquery.tooltip.js";
            this.Page.Header.Controls.Add(js);

            js = new HtmlGenericControl("script");
            js.Attributes["type"] = "text/javascript";
            js.Attributes["src"] = url.Replace(urlraw, "") + app + "JavaScript/MascarasGerais.js";
            this.Page.Header.Controls.Add(js);

            js = new HtmlGenericControl("script");
            js.Attributes["type"] = "text/javascript";
            js.Attributes["src"] = url.Replace(urlraw, "") + app + "JavaScript/script.js";
            this.Page.Header.Controls.Add(js);

            js = new HtmlGenericControl("script");
            js.Attributes["type"] = "text/javascript";
            js.Attributes["src"] = url.Replace(urlraw, "") + app + "JavaScript/jquery-ui-1.8.6.custom.min.js";
            this.Page.Header.Controls.Add(js);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["Usuario"];

            if (usuario != null)
            {
                if (!IsPostBack)
                {
                    //this.LoginNameUsuario.Attributes["title"] = usuario.Unidade.NomeFantasia;
                    //this.user.Attributes["title"] = usuario.Unidade.NomeFantasia;
                    this.CarregaMenuPrincipal(usuario.Codigo);
                }
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
            direcional += "<li class=\"botoes_ViverMais\"><a href=\"" + url + "\">" + texto + "</a></li>";
            direcional += "<li class=\"separator\"></li>";

            return direcional;
        }

        private void CarregaMenuPrincipal(int co_usuario)
        {
            ISeguranca iSeguranca = Factory.GetInstance<ISeguranca>();
            //string menu = string.Empty;
            //string links = string.Empty;
            //int tamanho_div = 75, tamanho_max_div = 678, valor_div = 0;
            string app = ((Request.ApplicationPath.Equals("/") || string.IsNullOrEmpty(Request.ApplicationPath)) ? "/" : Request.ApplicationPath + "/");
            string urlpart = Request.Url.ToString().Replace(Request.RawUrl, "") + app;

            HtmlImage imgcentral = this.img_ViverMaisassin;
            img_ViverMaisassin.Attributes.Add("onmouseover", "this.src='" + urlpart + "img/marca-ViverMais-2.jpg';");
            img_ViverMaisassin.Attributes.Add("onmouseout", "this.src='" + urlpart + "img/marca-ViverMais-1.jpg';");

            //#region PRINCIPAL
            //links += this.RetornaNavegador(urlpart + "Home.aspx", "Principal");
            //valor_div += tamanho_div;
            //#endregion

            //#region ESTABELECIMENTO DE SAUDE
            //if (iSeguranca.VerificarPermissao(co_usuario, "ESTABELECIMENTO_SAUDE", Modulo.ESTABELECIMENTO_SAUDE))
            //{
            //    links += this.RetornaNavegador(urlpart + "EstabelecimentoSaude/Default.aspx", "EAS");
            //    valor_div += tamanho_div;
            //}
            //#endregion

            //#region FARMÁCIA
            //if (iSeguranca.VerificarPermissao(co_usuario, "FARMACIA", Modulo.FARMACIA))
            //{
            //    links += this.RetornaNavegador(urlpart + "Farmacia/Default.aspx", "Farmácia");
            //    valor_div += tamanho_div;
            //}
            //#endregion

            //#region LABORATORIO
            //links += this.RetornaNavegador(urlpart + "Laboratorio/Default.aspx", "Laboratório");
            //valor_div += tamanho_div;
            //#endregion

            //#region OUVIDORIA
            //if (iSeguranca.VerificarPermissao(co_usuario, "OUVIDORIA", Modulo.OUVIDORIA))
            //{
            //    links += this.RetornaNavegador(urlpart + "Ouvidoria/Default.aspx", "Ouvidoria");
            //    valor_div += tamanho_div;
            //}
            //#endregion

            //#region PACIENTES
            //if (iSeguranca.VerificarPermissao(co_usuario, "PACIENTES", Modulo.CARTAO_SUS))
            //{
            //    links += this.RetornaNavegador(urlpart + "Paciente/Default.aspx", "Pacientes");
            //    valor_div += tamanho_div;
            //}
            //#endregion

            //#region PROFISSIONAIS
            //if (iSeguranca.VerificarPermissao(co_usuario, "PROFISSIONAL", Modulo.PROFISSIONAL))
            //{
            //    links += this.RetornaNavegador(urlpart + "Profissional/Default.aspx", "Profissionais");
            //    valor_div += tamanho_div;
            //}
            //#endregion

            //#region REGULAÇÃO
            //if (iSeguranca.VerificarPermissao(co_usuario, "AGENDAMENTO", Modulo.AGENDAMENTO))
            //{
            //    links += this.RetornaNavegador(urlpart + "Agendamento/Default.aspx", "Regulação");
            //    valor_div += tamanho_div;
            //}
            //#endregion

            //#region SEGURANCA
            //if (iSeguranca.VerificarPermissao(co_usuario, "SEGURANCA", Modulo.SEGURANCA))
            //{
            //    links += this.RetornaNavegador(urlpart + "Seguranca/Default.aspx", "Segurança");
            //    valor_div += tamanho_div;
            //}
            //#endregion

            //#region URGÊNCIA
            //if (iSeguranca.VerificarPermissao(co_usuario, "URGENCIA", Modulo.URGENCIA))
            //{
            //    links += this.RetornaNavegador(urlpart + "Urgencia/Default.aspx", "Urgência");
            //    valor_div += tamanho_div;
            //}
            //#endregion

            //#region VACINA
            //if (iSeguranca.VerificarPermissao(co_usuario, "VACINA", Modulo.VACINA))
            //{
            //    links += this.RetornaNavegador(urlpart + "Vacina/Default.aspx", "Vacina");
            //    valor_div += tamanho_div;
            //}
            //#endregion

            //#region RELATORIOS
            //links += this.RetornaNavegador(urlpart + "Relatorio/Home.aspx", "Relatórios");
            //valor_div += tamanho_div;
            //#endregion

            //#region FALE CONOSCO
            //links += this.RetornaNavegador(urlpart + "FaleConosco.aspx", "Fale Conosco");
            //valor_div += tamanho_div;
            //#endregion

            //#region SAIR
            //links += this.RetornaNavegador(urlpart + "Logout.aspx", "Sair");
            //valor_div += tamanho_div;
            //#endregion

            //if (valor_div > tamanho_max_div)
            //    valor_div = tamanho_max_div;

            //menu += "<div id=\"barra_informativo\">";
            //menu += "<div id=\"fotos\" class=\"sc_menu\" style=\"width:" + valor_div + "px;\">";
            //menu += "<ul id=\"fotos1\" class=\"sc_menu\">";
            //menu += links;
            //menu += "</ul>";
            //menu += "</div>";
            //menu += "<li class=\"sombra_dir\"></li>";
            //menu += "</div>";

            XmlDocument xml = new XmlDocument();
            xml.Load(Server.MapPath("~/Menu.xml"));
            Literal_Menu.Text = iSeguranca.RetornaMenuPrincipal(xml, urlpart, co_usuario);

            //Literal_Menu.Text = menu;
            Literal_Menu.DataBind();
        }

        public void BloquearPagina(Panel panelconteudo)
        {
            //Panel_FimPagina.Style["display"] = "block";
            //Panel_FimPagina.DataBind();
            //this.Literal_FimPagina.Text = "<div id=\"black_overlay\" style=\"display:block\" />";
            //this.Literal_FimPagina.DataBind();
            //this.Panel_FimPagina.Visible = true;
            panelconteudo.Visible = true;
        }

        public void DesbloquearPagina(Panel panelconteudo)
        {
            //this.Literal_FimPagina.Text = "<div id=\"black_overlay\" style=\"display:none\" />";
            //this.Literal_FimPagina.DataBind();
            //this.Panel_FimPagina.Visible = false;
            panelconteudo.Visible = false;
        }

        public void AdicionarTriggerFimPagina(Control control, string nomeevento)
        {
            AsyncPostBackTrigger trig = new AsyncPostBackTrigger();
            trig.ControlID = control.UniqueID;
            trig.EventName = nomeevento;
            //this.UpdatePanel_FimPagina.Triggers.Add(trig);
        }

        //public void RemovePermissaoMenu(MenuItemCollection items)
        //{
        //    ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
        //    IList<int> indices = new List<int>();
        //    for (int i = items.Count - 1; i >= 0; i--)
        //    {
        //        if (items[i].ChildItems.Count > 0)
        //        {
        //            RemovePermissaoMenu(items[i].ChildItems);
        //        }

        //        if (items[i].Value != "EMPTY" && !iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, items[i].Value.Split(':')[0].ToString(), int.Parse(items[i].Value.Split(':')[1].ToString())))
        //        {
        //            indices.Add(i);
        //        }
        //    }
        //    foreach (int i in indices)
        //    {
        //        items.RemoveAt(i);
        //    }
        //}

        //protected void MenuViverMais_MenuItemClick(object sender, MenuEventArgs e)
        //{
        //    FormsAuthentication.SignOut();
        //    Session.Clear();
        //    Response.Redirect("~/Home.aspx");
        //}

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
