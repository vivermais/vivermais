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
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.DAO;
using System.Text.RegularExpressions;

namespace ViverMais.View.Profissional
{
    public partial class MasterProfissional : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuario = Session["Usuario"] as Usuario;
                Perfil perfil = usuario.Perfis.Where(p => p.Modulo.Codigo == Modulo.PROFISSIONAL).FirstOrDefault();

                if (perfil == null)
                {
                    Application["AcessoPagina"] = "<strong>Usuário, você não possui o perfil necessário para acessar o módulo Profissional!</strong> <br/> Por favor, entre em contato com a administração.";
                    Response.Redirect("../FormAcessoNegado.aspx");
                }
                else
                {
                    IPerfil iPerfil = Factory.GetInstance<IPerfil>();
                    HorarioPerfil horario = iPerfil.BuscarHorario<HorarioPerfil>(perfil.Codigo, DateTime.Now.DayOfWeek);

                    if (horario == null)
                    {
                        Application["AcessoPagina"] = "<strong>Usuário, o seu perfil para o módulo Profissional não possui um horário de utilização!</strong> <br/> Por favor, entre em contato com a administração.";
                        Response.Redirect("../FormAcessoNegado.aspx");
                    }
                    else
                    {
                        if (horario.Bloqueado)
                        {
                            Application["AcessoPagina"] = "<strong>Usuário, o horário para utilização do módulo Profissional que consta em seu perfil no dia de hoje (" + horario.DiaEquivalente + ") está bloqueado!</strong> <br/> Por favor, entre em contato com a administração.";
                            Response.Redirect("../FormAcessoNegado.aspx");
                        }

                        if (!horario.PeriodoValido)
                        {
                            Application["AcessoPagina"] = "<strong>Usuário, o horário para utilização do módulo Profissional que consta em seu perfil no dia de hoje (" + horario.DiaEquivalente + ") é de " +
                                horario.HoraInicial.Substring(0, 2) + ":" + horario.HoraInicial.Substring(2, 2) + "h as " + horario.HoraFinal.Substring(0, 2) + ":" + horario.HoraFinal.Substring(2, 2) + "h.</strong> <br/> Por favor, entre em contato com a administração.";
                            Response.Redirect("../FormAcessoNegado.aspx");
                        }
                    }
                }

                this.Page.Title = "ViverMais - Módulo de Profissional";
                CarregaMenuModulo(usuario);
            }
        }

        private void CarregaMenuModulo(Usuario usuario)
        {
            Literal menumodulo = (Literal)((MasterMain)Master).FindControl("barramodulo");
            StringBuilder textomenu = new StringBuilder();
            //string app = ((Request.ApplicationPath.Equals("/") || string.IsNullOrEmpty(Request.ApplicationPath)) ? "/" : Request.ApplicationPath + "/");
            ////string urlpart = Request.Url.ToString().Replace(Request.RawUrl, "") + app;
            //string urlpart = Regex.Replace(HttpUtility.UrlDecode(Request.Url.ToString()), @"\s", "").
            //                Replace(Regex.Replace(HttpUtility.UrlDecode(Request.RawUrl), @"\s", ""), "") + app;

            HyperLink navegador = (HyperLink)((MasterMain)Master).FindControl("HyperLink_Modulo");
            navegador.Text = "Profissional";
            navegador.NavigateUrl = "~/Profissional/Default.aspx"; //Página default do módulo

            textomenu.Append("<div class=\"menu-barra-module\" style=\"background: url('" + HelperRedirector.URLRelativaAplicacao() + "Profissional/img/barramenu.png' ) no-repeat;\">");
            textomenu.Append(this.DivMenu.InnerText);
            textomenu.Append("</div>");

            menumodulo.Text = textomenu.ToString();
            menumodulo.DataBind();
        }
    }
}
