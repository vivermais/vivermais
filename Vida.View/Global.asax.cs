using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;
using Vida.ServiceFacade;
using Vida.DAO;
using System.IO;
using System.Text;
using System.Web.UI;

namespace Vida.View
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            TextReader reader = new StringReader(Vida.View.Properties.Resources.Mapping);
            Factory.Load(reader);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //if (Context.User != null)
            //{
            //    HttpCookie cookie = Request.Cookies.Get(FormsAuthentication.FormsCookieName);
            //    if (cookie != null)
            //    {
            //        FormsAuthenticationTicket ticket =
            //        FormsAuthentication.Decrypt(cookie.Value);

            //        if (ticket == null || ticket.Expired)
            //        {
            //            FormsAuthentication.SignOut();
            //            string pagina = HttpUtility.UrlEncode(Request.RawUrl);
            //            Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + pagina, true);
            //        }
            //    }
            //}
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Application["ErroAplicacao"] = new Vida.Model.ErroVida(Server.GetLastError().GetBaseException(), Request.Url.ToString());
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            //if (Session != null && Session.IsNewSession && Request.IsAuthenticated)
            //{
            //    FormsAuthentication.SignOut();
            //    string pagina = HttpUtility.UrlEncode(Request.RawUrl);
            //    Response.Redirect(FormsAuthentication.LoginUrl);
            //}
            //HttpCookie cookie = Request.Cookies.Get(FormsAuthentication.FormsCookieName);
            //if (cookie != null)
            //{
            //    FormsAuthenticationTicket ticket =
            //    FormsAuthentication.Decrypt(cookie.Value);

            //    if (ticket.Expired)
            //    {
            //        FormsAuthentication.SignOut();
            //        //string pagina = HttpUtility.UrlEncode(Request.RawUrl);
            //        Response.Redirect(FormsAuthentication.LoginUrl);
            //    }
            //}
        }

        protected void Application_PreRequestHandlerExecute(Object sender, EventArgs e)
        {
            //if (Context.Handler is IRequiresSessionState || Context.Handler is IReadOnlySessionState)
            //{
            //    if ((HttpContext.Current.Session == null || HttpContext.Current.Session.Count == 0) && HttpContext.Current.User.Identity.IsAuthenticated)
            //    {
            //        string pagina = HttpUtility.UrlEncode(Request.RawUrl);
            //        FormsAuthentication.SignOut();
            //        Response.Redirect(FormsAuthentication.LoginUrl + "?ReturnUrl=" + pagina, true);
            //    }
            //}
        }
    }
}