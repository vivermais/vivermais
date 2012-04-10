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
using iTextSharp.text;
using System.Collections.Generic;

namespace ViverMais.View.Helpers
{
    public static class HelperRandomGenerator
    {
        static Random gen = new Random(DateTime.Now.Millisecond);
        

        public static long Next
        {
            get { return gen.Next(); }
        }

        public static long NextIdentificador
        {
            get { return gen.Next(99999); }
        }

    }
}
