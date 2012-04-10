using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace ViverMais.View
{
    public static class HelperRedirector
    {
        public static void Redirect(string url, string target, string windowFeatures)
        {
            HttpContext context = HttpContext.Current;
            if ((String.IsNullOrEmpty(target) || target.Equals("_self", StringComparison.OrdinalIgnoreCase))
                && String.IsNullOrEmpty(windowFeatures))
            {
                context.Response.Redirect(url);
            }
            else
            {
                Page page = (Page)context.Handler;
                if (page == null)
                {
                    throw new InvalidOperationException("Cannot redirect to new window outside Page context.");
                }
                url = page.ResolveClientUrl(url);
                string script;
                if (!String.IsNullOrEmpty(windowFeatures))
                {
                    script = @"window.open(""{0}"", ""{1}"", ""{2}"");";
                }
                else
                {
                    script = @"window.open(""{0}"", ""{1}"");";
                }
                script = String.Format(script, url, target, windowFeatures);
                ScriptManager.RegisterStartupScript(page, typeof(Page), "Redirect", script, true);
            }
        }

        public static void Voltar()
        {
            HttpContext context = HttpContext.Current;
            Stack<Uri> history = (Stack<Uri>)context.Session["History"];
            if (history != null)
            {
                history.Pop();
                context.Session["History"] = history;
                context.Session["FlagIndicaVoltar"] = true;
                context.Response.Redirect(history.Peek().ToString());
            }
        }

        public static string URLRelativaAplicacao()
        {
            HttpContext httpContext = HttpContext.Current;

            if (httpContext != null)
            {
                Page page = httpContext.Handler as Page;

                string app = ((page.Request.ApplicationPath.Equals("/") || string.IsNullOrEmpty(page.Request.ApplicationPath)) ? "/" : page.Request.ApplicationPath + "/");

                string urlpart = Regex.Replace(HttpUtility.UrlDecode(page.Request.Url.ToString()), @"\s", "").
                                Replace(Regex.Replace(HttpUtility.UrlDecode(page.Request.RawUrl), @"\s", ""), "") + app;

                return urlpart;
            }

            return string.Empty;
        }

        public static string EncodeURL(string url)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(url);

            return Uri.EscapeUriString(Convert.ToBase64String(toEncodeAsBytes));
        }

        public static string DecodeURL(string url)
        {
            try
            {
                byte[] encodedDataAsBytes = System.Convert.FromBase64String(url);

                if (url.Replace(" ", "").Length % 4 == 0)
                    return Uri.EscapeUriString(ASCIIEncoding.ASCII.GetString(encodedDataAsBytes));

                return url;
            }
            catch(FormatException)
            {
                return url;
            }
        }
    }
}
