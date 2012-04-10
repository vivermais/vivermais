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
using System.Text;
using System.Xml;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.DAO;
using ViverMais.Model;
using System.Text.RegularExpressions;

namespace ViverMais.View.Vacina
{
    public partial class MasterVacina : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuario = Session["Usuario"] as Usuario;
                Perfil perfil = usuario.Perfis.Where(p => p.Modulo.Codigo == Modulo.VACINA).FirstOrDefault();

                if (perfil == null)
                {
                    Application["AcessoPagina"] = "<strong>Usuário, você não possui o perfil necessário para acessar o módulo Vacina!</strong> <br/> Por favor, entre em contato com a administração.";
                    Response.Redirect("../FormAcessoNegado.aspx");
                }
                else
                {
                    IPerfil iPerfil = Factory.GetInstance<IPerfil>();
                    HorarioPerfil horario = iPerfil.BuscarHorario<HorarioPerfil>(perfil.Codigo, DateTime.Now.DayOfWeek);

                    if (horario == null)
                    {
                        Application["AcessoPagina"] = "<strong>Usuário, o seu perfil para o módulo Vacina não possui um horário de utilização!</strong> <br/> Por favor, entre em contato com a administração.";
                        Response.Redirect("../FormAcessoNegado.aspx");
                    }
                    else
                    {
                        if (horario.Bloqueado)
                        {
                            Application["AcessoPagina"] = "<strong>Usuário, o horário para utilização do módulo Vacina que consta em seu perfil no dia de hoje (" + horario.DiaEquivalente + ") está bloqueado!</strong> <br/> Por favor, entre em contato com a administração.";
                            Response.Redirect("../FormAcessoNegado.aspx");
                        }

                        if (!horario.PeriodoValido)
                        {
                            Application["AcessoPagina"] = "<strong>Usuário, o horário para utilização do módulo Vacina que consta em seu perfil no dia de hoje (" + horario.DiaEquivalente + ") é de " +
                                horario.HoraInicial.Substring(0, 2) + ":" + horario.HoraInicial.Substring(2, 2) + "h as " + horario.HoraFinal.Substring(0, 2) + ":" + horario.HoraFinal.Substring(2, 2) + "h.</strong> <br/> Por favor, entre em contato com a administração.";
                            Response.Redirect("../FormAcessoNegado.aspx");
                        }
                    }
                }

                this.CarregaMenuModulo(usuario);
            }
        }

        private void CarregaMenuModulo(Usuario usuario)
        {
            Literal menumodulo = (Literal)((MasterMain)Master).FindControl("barramodulo");
            //StringBuilder textomenu = new StringBuilder();
            //string app = ((Request.ApplicationPath.Equals("/") || string.IsNullOrEmpty(Request.ApplicationPath)) ? "/" : Request.ApplicationPath + "/");

            //string urlpart = Regex.Replace(HttpUtility.UrlDecode(Request.Url.ToString()), @"\s", "").
            //                Replace(Regex.Replace(HttpUtility.UrlDecode(Request.RawUrl), @"\s", ""), "") + app;

            //string urlpart = Request.Url.ToString().Replace(Request.RawUrl, "") + app;
            //string suburlpart = Request.RawUrl.Split('/')[1];

            HyperLink navegador = (HyperLink)((MasterMain)Master).FindControl("HyperLink_Modulo");
            navegador.Text = "Vacina";
            navegador.NavigateUrl = "~/Vacina/Default.aspx"; //Página default do módulo
            //Usuario usuario = (Usuario)Session["Usuario"];
            ISeguranca iSeguranca = Factory.GetInstance<ISeguranca>();

            XmlDocument xml = new XmlDocument();
            xml.Load(Server.MapPath("~/Vacina/Menu.xml"));

            //bool permissao_doacao = iSeguranca.VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_DOACAO", Modulo.VACINA);
            //bool permissao_devolucao = iSeguranca.VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_DEVOLUCAO", Modulo.VACINA);
            //bool permissao_emprestimo = iSeguranca.VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_EMPRESTIMO", Modulo.VACINA);
            //bool permissao_perda = iSeguranca.VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_PERDA", Modulo.VACINA);
            //bool permissao_remanejamento = iSeguranca.VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_REMANEJAMENTO", Modulo.VACINA);
            //bool permissao_acerto_balanco = iSeguranca.VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_ACERTO_BALANCO", Modulo.VACINA);

            //XmlNode nodeEspecial = xml.SelectSingleNode("/Menu/div/div/ul/li/ul/li/ul/PermissaoEspecial[@Nome='NOVAMOVIMENTACAO']");
            //XmlNode parent = nodeEspecial.ParentNode;

            //if (permissao_doacao || permissao_devolucao ||
            //     permissao_emprestimo || permissao_perda ||
            //     permissao_remanejamento ||
            //     permissao_acerto_balanco
            //    )
            //    this.SubstituirXMLNode(xml, nodeEspecial, parent);
            //else
            //    parent.RemoveChild(nodeEspecial);

            //nodeEspecial = xml.SelectSingleNode("/Menu/div/div/ul/li/ul/PermissaoEspecial[@Nome='INVENTARIO']");
            //parent = nodeEspecial.ParentNode;

            //if (!iSeguranca.VerificarPermissao(usuario.Codigo, "REALIZAR_INVENTARIO_VACINA", Modulo.VACINA)
            //    && !iSeguranca.VerificarPermissao(usuario.Codigo, "REALIZAR_ABERTURA_ENCERRAMENTO_INVENTARIO_VACINA", Modulo.VACINA))
            //    parent.RemoveChild(nodeEspecial);
            //else
            //    this.SubstituirXMLNode(xml, nodeEspecial, parent);

            menumodulo.Text = iSeguranca.RetornaMenuModulo(xml, HelperRedirector.URLRelativaAplicacao(), usuario.Codigo, Modulo.VACINA);
          
            //textomenu.Append("<div class=\"menu-barra-module\" style=\"background: url('" + urlpart + "Vacina/img/barramenu.png' ) no-repeat;\">");
            //textomenu.Append(this.DivMenu.InnerText);
            //textomenu.Append("</div>");

            //menumodulo.Text = textomenu.ToString();
            menumodulo.DataBind();
        }

        //private void SubstituirXMLNode(XmlDocument xml, XmlNode noAntigo, XmlNode parent)
        //{
        //    XmlElement element = (XmlElement)noAntigo.FirstChild;
        //    XmlElement no = xml.CreateElement(element.Name);
        //    no.InnerXml = element.InnerXml;
        //    parent.ReplaceChild(no, noAntigo);
        //}
    }
}
