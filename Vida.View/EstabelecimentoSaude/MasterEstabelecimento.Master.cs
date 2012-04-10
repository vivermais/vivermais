using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Xml;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.DAO;
using ViverMais.Model;
using System.Text.RegularExpressions;

namespace ViverMais.View.EstabelecimentoSaude
{
    public partial class MasterEstabelecimento : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuario = Session["Usuario"] as Usuario;
                Perfil perfil = usuario.Perfis.Where(p => p.Modulo.Codigo == Modulo.ESTABELECIMENTO_SAUDE).FirstOrDefault();

                if (perfil == null)
                {
                    Application["AcessoPagina"] = "<strong>Usuário, você não possui o perfil necessário para acessar o módulo Estabelecimento de Saúde!</strong> <br/> Por favor, entre em contato com a administração.";
                    Response.Redirect("../FormAcessoNegado.aspx");
                }
                else
                {
                    IPerfil iPerfil = Factory.GetInstance<IPerfil>();
                    HorarioPerfil horario = iPerfil.BuscarHorario<HorarioPerfil>(perfil.Codigo, DateTime.Now.DayOfWeek);

                    if (horario == null)
                    {
                        Application["AcessoPagina"] = "<strong>Usuário, o seu perfil para o módulo Estabelecimento de Saúde não possui um horário de utilização!</strong> <br/> Por favor, entre em contato com a administração.";
                        Response.Redirect("../FormAcessoNegado.aspx");
                    }
                    else
                    {
                        if (horario.Bloqueado)
                        {
                            Application["AcessoPagina"] = "<strong>Usuário, o horário para utilização do módulo Estabelecimento de Saúde que consta em seu perfil no dia de hoje (" + horario.DiaEquivalente + ") está bloqueado!</strong> <br/> Por favor, entre em contato com a administração.";
                            Response.Redirect("../FormAcessoNegado.aspx");
                        }

                        if (!horario.PeriodoValido)
                        {
                            Application["AcessoPagina"] = "<strong>Usuário, o horário para utilização do módulo Estabelecimento de Saúde que consta em seu perfil no dia de hoje (" + horario.DiaEquivalente + ") é de " +
                                horario.HoraInicial.Substring(0, 2) + ":" + horario.HoraInicial.Substring(2, 2) + "h as " + horario.HoraFinal.Substring(0, 2) + ":" + horario.HoraFinal.Substring(2, 2) + "h.</strong> <br/> Por favor, entre em contato com a administração.";
                            Response.Redirect("../FormAcessoNegado.aspx");
                        }
                    }
                }

                this.Page.Title = "ViverMais - Módulo EAS";
                this.CarregaMenuModulo(usuario);
            }
        }

        private void CarregaMenuModulo(Usuario usuario)
        {
            Literal menumodulo = (Literal)((MasterMain)Master).FindControl("barramodulo");
            StringBuilder textomenu = new StringBuilder();
            //string app = ((Request.ApplicationPath.Equals("/") || string.IsNullOrEmpty(Request.ApplicationPath)) ? "/" : Request.ApplicationPath + "/");
            //string urlpart = Regex.Replace(HttpUtility.UrlDecode(Request.Url.ToString()), @"\s", "").
            //                Replace(Regex.Replace(HttpUtility.UrlDecode(Request.RawUrl), @"\s", ""), "") + app;

            //string urlpart = Request.Url.ToString().Replace(Request.RawUrl, "") + app;
            //string suburlpart = Request.RawUrl.Split('/')[1];

            HyperLink navegador = (HyperLink)((MasterMain)Master).FindControl("HyperLink_Modulo");
            navegador.Text = "Estabelecimento Saúde";
            navegador.NavigateUrl = "~/EstabelecimentoSaude/Default.aspx"; //Página default do módulo

            //textomenu.Append("<div class=\"menu-barra-module\" style=\"background: url('" + urlpart + "EstabelecimentoSaude/img/barramenu.png' ) no-repeat;\">");
            //textomenu.Append(this.DivMenu.InnerText);
            //textomenu.Append("</div>");
            XmlDocument xml = new XmlDocument();
            xml.Load(Server.MapPath("~/EstabelecimentoSaude/Menu.xml"));
            menumodulo.Text = Factory.GetInstance<ISeguranca>().RetornaMenuModulo(xml, HelperRedirector.URLRelativaAplicacao(), usuario.Codigo, Modulo.ESTABELECIMENTO_SAUDE);
            
            //menumodulo.Text = textomenu.ToString();
            menumodulo.DataBind();
        }
    }
}
