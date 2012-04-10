﻿using System;
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
using iTextSharp.text.pdf;
using iTextSharp.text;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using ViverMais.Model;
using ViverMais.DAO;
using System.IO;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Paciente
{
    public partial class ImpressaoCartaoSUS : System.Web.UI.Page
    {
        /*
         * protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["codigo"] != null)
                {
                    ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
                    if (!iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "ALTERAR_CARTAO_SUS"))
                    {
                        ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Você não tem permissão para acessar essa página. Em caso de dúViverMais, entre em contato.');window.location='../Home.aspx';</script>");
                    }
                    IPaciente ipaciente = Factory.GetInstance<IPaciente>();
                    ViverMais.Model.Paciente paciente = ipaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(Request.QueryString["codigo"]);
                    IList<CartaoSUS> cartoes = ipaciente.ListarCartoesSUS<ViverMais.Model.CartaoSUS>(paciente.Codigo);
                    //CartaoSUS cartao = cartoes.Max<long>(x => long.Parse(x.Numero));
                    long result = (from c in cartoes select long.Parse(c.Numero)).Min();
                    IEndereco iendereco = Factory.GetInstance<IEndereco>();
                    Endereco endereco = iendereco.BuscarPorPaciente<Endereco>(Request.QueryString["codigo"]);
                    Document doc = new Document(new iTextSharp.text.Rectangle(295, 191));
                    Response.ContentType = "application/pdf";
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("temp/") + Request.QueryString["codigo"] + ".pdf", FileMode.Create));
                    doc.Open();
                    Paragraph p = new Paragraph();
                    p.IndentationLeft = -10;
                    p.Font.Color = iTextSharp.text.Color.BLACK;
                    Phrase nome = new Phrase(paciente.Nome + "\n");
                    nome.Font.Size = 8;
                    Phrase nascimento = new Phrase(paciente.DataNascimento.ToString("dd/MM/yyyy") + "\t\t" + endereco.Municipio.Nome + "\n");
                    nascimento.Font.Size = 8;
                    //Phrase cartaosus = new Phrase(cartoes.Last().Numero + "\n");
                    Phrase cartaosus = new Phrase(result + "\n");
                    cartaosus.Font.Size = 12;
                    PdfContentByte cb = writer.DirectContent;
                    Barcode39 code39 = new Barcode39();
                    code39.Code = result.ToString();
                    code39.StartStopText = true;
                    code39.GenerateChecksum = false;
                    code39.Extended = true;
                    iTextSharp.text.Image imageEAN = code39.CreateImageWithBarcode(cb, null, null);

                    iTextSharp.text.Image back = iTextSharp.text.Image.GetInstance(Server.MapPath("img/") + "back_card.JPG");
                    back.SetAbsolutePosition(0, doc.PageSize.Height - back.Height);

                    iTextSharp.text.Image front = iTextSharp.text.Image.GetInstance(Server.MapPath("img/") + "front_card.JPG");
                    front.SetAbsolutePosition(0, doc.PageSize.Height - front.Height);

                    Phrase barcode = new Phrase(new Chunk(imageEAN, 36, -45));
                    barcode.Font.Color = iTextSharp.text.Color.WHITE;

                    p.SetLeading(1, 0.7f);
                    p.Add(cartaosus);
                    p.Add(nome);
                    p.Add(nascimento);
                    p.Add(barcode);
                    doc.Add(p);

                    doc.Add(back);
                    doc.NewPage();
                    doc.Add(front);

                    doc.Close();
                    Response.Redirect("temp/" + Request.QueryString["codigo"] + ".pdf");
                }
            }
        }*/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["codigo"] != null)
                {
                    ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
                    if (!iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "MANTER_PACIENTE", Modulo.CARTAO_SUS))
                    {
                        ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Você não tem permissão para acessar essa página. Em caso de dúViverMais, entre em contato.');window.location='../Home.aspx';</script>");
                    }
                    IPaciente ipaciente = Factory.GetInstance<IPaciente>();
                    ViverMais.Model.Paciente paciente = ipaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(Request.QueryString["codigo"]);
                    IList<CartaoSUS> cartoes = ipaciente.ListarCartoesSUS<ViverMais.Model.CartaoSUS>(paciente.Codigo);
                    //CartaoSUS cartao = cartoes.Max<long>(x => long.Parse(x.Numero));
                    long result = (from c in cartoes select long.Parse(c.Numero)).Min();
                    IEndereco iendereco = Factory.GetInstance<IEndereco>();
                    Endereco endereco = iendereco.BuscarPorPaciente<Endereco>(Request.QueryString["codigo"]);


                    MemoryStream MStream = new MemoryStream();
                    Document doc = new Document(new iTextSharp.text.Rectangle(295, 191));
                    PdfWriter writer = PdfWriter.GetInstance(doc,MStream);

                    //Monta o pdf
                    doc.Open();
                    Paragraph p = new Paragraph();
                    p.IndentationLeft = -10;
                    p.Font.Color = iTextSharp.text.Color.BLACK;
                    Phrase nome = new Phrase(paciente.Nome + "\n");
                    nome.Font.Size = 8;
                    Phrase nascimento = new Phrase(paciente.DataNascimento.ToString("dd/MM/yyyy") + "\t\t" + endereco.Municipio.Nome + "\n");
                    nascimento.Font.Size = 8;
                    //Phrase cartaosus = new Phrase(cartoes.Last().Numero + "\n");
                    Phrase cartaosus = new Phrase(result + "\n");
                    cartaosus.Font.Size = 12;
                    PdfContentByte cb = writer.DirectContent;
                    Barcode39 code39 = new Barcode39();
                    code39.Code = result.ToString();
                    code39.StartStopText = true;
                    code39.GenerateChecksum = false;
                    code39.Extended = true;
                    iTextSharp.text.Image imageEAN = code39.CreateImageWithBarcode(cb, null, null);

                    iTextSharp.text.Image back = iTextSharp.text.Image.GetInstance(Server.MapPath("img/") + "back_card.JPG");
                    back.SetAbsolutePosition(0, doc.PageSize.Height - back.Height);

                    iTextSharp.text.Image front = iTextSharp.text.Image.GetInstance(Server.MapPath("img/") + "front_card.JPG");
                    front.SetAbsolutePosition(0, doc.PageSize.Height - front.Height);

                    Phrase barcode = new Phrase(new Chunk(imageEAN, 36, -45));
                    barcode.Font.Color = iTextSharp.text.Color.WHITE;

                    p.SetLeading(1, 0.7f);
                    p.Add(cartaosus);
                    p.Add(nome);
                    p.Add(nascimento);
                    p.Add(barcode);
                    doc.Add(p);

                    doc.Add(back);
                    doc.NewPage();
                    doc.Add(front);

                    doc.Close();
                    //Fim monta pdf

                    HttpContext.Current.Response.Buffer = true;
                    HttpContext.Current.Response.ClearContent();
                    HttpContext.Current.Response.ClearHeaders();
                    HttpContext.Current.Response.ContentType = "application/pdf";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=CartaoSUS.pdf");
                    HttpContext.Current.Response.BinaryWrite(MStream.GetBuffer());
                    HttpContext.Current.Response.End();
                }
            }
        }
    }
}
