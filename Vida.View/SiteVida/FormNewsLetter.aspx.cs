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
using System.Collections.Generic;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.SiteViverMais;
using System.Text.RegularExpressions;

namespace ViverMais.View.SiteViverMais
{
    public partial class FormNewsLetter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                tbxNome.Attributes.Add("onfocus", "javascript:FxApagar(this);");
                tbxNome.Attributes.Add("onblur", "javascript:FxPreencher(this,'nome');");
                tbxEmail.Attributes.Add("onfocus", "javascript:FxApagar(this);");
                tbxEmail.Attributes.Add("onblur", "javascript:FxPreencher(this,'e-mail');");
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            //String com as regras que validam o E-mail
            string ValidaEmail =
                 @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
          + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
          + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
          + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

            if (tbxNome.Text == "Digite o seu nome")
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Informe seu nome!')</script>");
                return;
            }

            if (tbxEmail.Text == "Digite o seu e-mail")
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Informe seu e-mail!')</script>");
                return;
            }

            ViverMais.Model.NewsLetter spam = new ViverMais.Model.NewsLetter();

            if (!Regex.IsMatch(tbxEmail.Text, ValidaEmail))
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Email Invalido!')</script>");
                return;
            }
            else
            {
                try
                {
                    if (Factory.GetInstance<INewsLetterServiceFacade>().BuscarEmailUsuario<NewsLetter>(tbxEmail.Text.ToLower()) != null)
                    {
                        ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Email já cadastrado!')</script>");
                        return;
                    }
                    spam.Email = tbxEmail.Text.ToLower();
                    spam.Nome = tbxNome.Text;
                    spam.DataCriacao = DateTime.Now;
                    Factory.GetInstance<IViverMaisServiceFacade>().Salvar(spam);

                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Email cadastrado com sucesso!')</script>");
                }
                catch (Exception a)
                {
                    throw a;
                }
            }
        }
    }
}
