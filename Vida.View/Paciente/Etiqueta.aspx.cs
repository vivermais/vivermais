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
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.Model;
using System.Collections.Generic;
using ViverMais.DAO;
using iTextSharp.text.pdf;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Paciente
{
    public partial class Etiqueta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
                if (!iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "ALTERAR_CARTAO_SUS",Modulo.CARTAO_SUS))
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Você não tem permissão para acessar essa página. Em caso de dúViverMais, entre em contato.');window.location='../Home.aspx';</script>");
                }
            }

            if (Request.QueryString["codigo"] != null)
            {
                IPaciente ipaciente = Factory.GetInstance<IPaciente>();
                ViverMais.Model.Paciente paciente = ipaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(Request.QueryString["codigo"]);
                IList<CartaoSUS> cartoes = ipaciente.ListarCartoesSUS<ViverMais.Model.CartaoSUS>(paciente.Codigo);
                long result = (from c in cartoes select long.Parse(c.Numero)).Min();
                Barcode39 code39 = new Barcode39();
                code39.Code = result.ToString();
                code39.StartStopText = true;
                code39.GenerateChecksum = false;
                code39.Extended = true;
                System.Drawing.Image img = code39.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                img.Save (ms, System.Drawing.Imaging.ImageFormat.Gif);
                Response.ClearContent();
                Response.ContentType = "image/Gif";
                Response.BinaryWrite(ms.ToArray());
            }
        }
    }
}
