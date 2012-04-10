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
using ViverMais.DAO;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.SiteViverMais;

namespace ViverMais.View.SiteViverMais
{
    public partial class FormNewsLetterExclusao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {

            ViverMais.Model.NewsLetter Email = new ViverMais.Model.NewsLetter();
            Email = Factory.GetInstance<INewsLetterServiceFacade>().BuscarEmailUsuario<NewsLetter>(tbxEmail.Text);
             if (Email == null)
             {
               ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Email não existe.!')</script>");
             }
             else 
             {   
                 Factory.GetInstance<IViverMaisServiceFacade>().Deletar(Email);
             }
            
            
            
                 
       }
    }
}
