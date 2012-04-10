using System;
using System.IO.Compression;
using System.Web;

namespace ViverMais.View.CustomPages
{
    public class CompressionHTTP : IHttpModule
    {
        void IHttpModule.Dispose() { }

        void IHttpModule.Init(HttpApplication httpapplication)
        {
            httpapplication.PostReleaseRequestState += new EventHandler(httpapplication_PostReleaseRequestState);
        }

        private const string GZIP = "gzip";
        private const string DEFLATE = "deflate";

        void httpapplication_PostReleaseRequestState(object sender, EventArgs e)
        {
            HttpApplication httpapplication = (HttpApplication)sender;

            if (httpapplication.Context.CurrentHandler is System.Web.UI.Page
                || httpapplication.Request.Path.Contains("css") || httpapplication.Request.Path.Contains("js"))
            {
                if (EncondingAceito(GZIP))
                {
                    httpapplication.Response.Filter = new GZipStream(httpapplication.Response.Filter, CompressionMode.Compress);
                    SetEncoding(GZIP);
                }
                else if (EncondingAceito(DEFLATE))
                {
                    httpapplication.Response.Filter = new DeflateStream(httpapplication.Response.Filter, CompressionMode.Compress);
                    SetEncoding(DEFLATE);
                }
            }
        }

        private static void SetEncoding(string encoding)
        {
            HttpContext.Current.Response.AppendHeader("Content-encoding", encoding);
        }

        private static bool EncondingAceito(string encoding)
        {
            return HttpContext.Current.Request.Headers["Accept-encoding"] != null && HttpContext.Current.Request.Headers["Accept-encoding"].Contains(encoding);
        }
    }
}