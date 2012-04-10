using System;
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
using ViverMais.Model;
using System.Collections.Generic;

namespace ViverMais.View.Vacina
{
    public partial class Default : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Sistema em manutenção. Previsão de volta: 22/12/2010.');location='Default.aspx';", true);
            //Session.RemoveAll();
            //Label lb = new Label();
            //lb.Text = ((Usuario)Session["Usuario"]).Codigo.ToString();
        }

        //protected void teste(object sender, EventArgs e)
        //{
        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "javascript:GB_showFullScreen('Receita','../FormConsultarEstoque.aspx');",true); ;
        //}
    }
}
