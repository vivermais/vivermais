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
    public partial class MasterMainAntiga : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["Usuario"];

            if (usuario != null)
            {
                if (!IsPostBack)
                {
                    this.CarregaMenuPrincipal(usuario.Codigo);
                    //RemovePermissaoMenu(MenuViverMais.Items);
                }
            }
            else
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        private void CarregaMenuPrincipal(int co_usuario)
        {
            XElement xDoc = new XElement("Home");
            XElement xMenu = null;
            ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();

            #region PRINCIPAL
            xMenu = new XElement("Menu");
            xMenu.SetAttributeValue("Text", "Principal");
            xMenu.SetAttributeValue("Img", "~/img/separate.png");
            xMenu.SetAttributeValue("Url", "~/Home.aspx");
            xDoc.Add(xMenu);
            #endregion

            #region REGULAÇÃO

            if (iseguranca.VerificarPermissao(co_usuario, "AGENDAMENTO", Modulo.AGENDAMENTO))
            {
                xMenu = new XElement("Menu");
                xMenu.SetAttributeValue("Text", "Regulação");
                xMenu.SetAttributeValue("Img", "~/img/separate.png");
                xMenu.SetAttributeValue("Url", "~/Agendamento/Default.aspx");
                xDoc.Add(xMenu);
            }

            #endregion

            #region ESTABELECIMENTO DE SAUDE
            if (iseguranca.VerificarPermissao(co_usuario, "ESTABELECIMENTO_SAUDE", Modulo.ESTABELECIMENTO_SAUDE))
            {
                xMenu = new XElement("Menu");
                xMenu.SetAttributeValue("Text", "EAS");
                xMenu.SetAttributeValue("Img", "~/img/separate.png");
                xMenu.SetAttributeValue("Url", "~/EstabelecimentoSaude/FormEstabelecimentoDeSaude.aspx");
                xDoc.Add(xMenu);
            }
            #endregion

            #region FARMÁCIA
            if (iseguranca.VerificarPermissao(co_usuario, "FARMACIA", Modulo.FARMACIA))
            {
                xMenu = new XElement("Menu");
                xMenu.SetAttributeValue("Text", "Farmácia");
                xMenu.SetAttributeValue("Img", "~/img/separate.png");
                xMenu.SetAttributeValue("Url", "~/Farmacia/Default.aspx");
                xDoc.Add(xMenu);
            }
            #endregion

            #region OUVIDORIA
            if (iseguranca.VerificarPermissao(co_usuario, "OUVIDORIA", Modulo.OUVIDORIA))
            {
                xMenu = new XElement("Menu");
                xMenu.SetAttributeValue("Text", "Ouvidoria");
                xMenu.SetAttributeValue("Img", "~/img/separate.png");
                xMenu.SetAttributeValue("Url", "~/Ouvidoria/Default.aspx");
                xDoc.Add(xMenu);
            }
            #endregion

            #region PACIENTES
            if (iseguranca.VerificarPermissao(co_usuario, "PACIENTES", Modulo.CARTAO_SUS))
            {
                xMenu = new XElement("Menu");
                xMenu.SetAttributeValue("Text", "Cartão SUS");
                xMenu.SetAttributeValue("Img", "~/img/separate.png");
                xMenu.SetAttributeValue("Url", "~/Paciente/Default.aspx");
                xDoc.Add(xMenu);
            }
            #endregion

            #region PROFISSIONAIS
            if (iseguranca.VerificarPermissao(co_usuario, "PROFISSIONAIS", Modulo.ESTABELECIMENTO_SAUDE))
            {
                xMenu = new XElement("Menu");
                xMenu.SetAttributeValue("Text", "Profissionais");
                xMenu.SetAttributeValue("Img", "~/img/separate.png");
                xMenu.SetAttributeValue("Url", "~/Profissional/Default.aspx");
                xDoc.Add(xMenu);
            }
            #endregion

            #region URGÊNCIA
            if (iseguranca.VerificarPermissao(co_usuario, "URGENCIA", Modulo.URGENCIA))
            {
                xMenu = new XElement("Menu");
                xMenu.SetAttributeValue("Text", "Urgência");
                xMenu.SetAttributeValue("Img", "~/img/separate.png");
                xMenu.SetAttributeValue("Url", "~/Urgencia/Default.aspx");
                xDoc.Add(xMenu);
            }
            #endregion

            #region VACINA
            if (iseguranca.VerificarPermissao(co_usuario, "VACINA", Modulo.VACINA))
            {
                xMenu = new XElement("Menu");
                xMenu.SetAttributeValue("Text", "Vacina");
                xMenu.SetAttributeValue("Img", "~/img/separate.png");
                xMenu.SetAttributeValue("Url", "~/Vacina/Default.aspx");
                xDoc.Add(xMenu);
            }
            #endregion

            #region LABORATÓRIO
            //xMenu = new XElement("Menu");
            //xMenu.SetAttributeValue("Text", "Laboratório");
            //xMenu.SetAttributeValue("Img", "~/img/separate.png");
            //xMenu.SetAttributeValue("Url", "~/Laboratorio/Default.aspx");
            //xDoc.Add(xMenu);
            #endregion

            #region SEGURANCA
            if (iseguranca.VerificarPermissao(co_usuario, "SEGURANCA", Modulo.SEGURANCA))
            {
                xMenu = new XElement("Menu");
                xMenu.SetAttributeValue("Text", "Segurança");
                xMenu.SetAttributeValue("Img", "~/img/separate.png");
                xMenu.SetAttributeValue("Url", "~/Seguranca/Default.aspx");
                xDoc.Add(xMenu);
            }
            #endregion

            #region FALE CONOSCO
            xMenu = new XElement("Menu");
            xMenu.SetAttributeValue("Text", "Fale Conosco");
            xMenu.SetAttributeValue("Img", "~/img/separate.png");
            xMenu.SetAttributeValue("Url", "~/FaleConosco.aspx");
            xDoc.Add(xMenu);
            #endregion

            #region SAIR
            xMenu = new XElement("Menu");
            xMenu.SetAttributeValue("Text", "Sair");
            xMenu.SetAttributeValue("Img", "");
            xMenu.SetAttributeValue("Url", "");
            xDoc.Add(xMenu);
            #endregion

            MenuViverMais.Items.Clear();

            string xml = xDoc.ToString();
            XmlDataSource_MenuViverMais.Data = xml;
            XmlDataSource_MenuViverMais.DataBind();

            MenuViverMais.DataBind();
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
