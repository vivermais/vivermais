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
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Paciente
{
    public partial class MasterPaciente : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "ViverMais - Módulo de Cadastro";
            if (!IsPostBack)
            {
                Usuario usuario = Session["Usuario"] as Usuario;
                Perfil perfil = usuario.Perfis.Where(p => p.Modulo.Codigo == Modulo.CARTAO_SUS).FirstOrDefault();

                if (perfil == null)
                {
                    Application["AcessoPagina"] = "<strong>Usuário, você não possui o perfil necessário para acessar o módulo Cartão SUS!</strong> <br/> Por favor, entre em contato com a administração.";
                    Response.Redirect("../FormAcessoNegado.aspx");
                }
                else
                {
                    IPerfil iPerfil = Factory.GetInstance<IPerfil>();
                    HorarioPerfil horario = iPerfil.BuscarHorario<HorarioPerfil>(perfil.Codigo, DateTime.Now.DayOfWeek);

                    if (horario == null)
                    {
                        Application["AcessoPagina"] = "<strong>Usuário, o seu perfil para o módulo Cartão SUS não possui um horário de utilização!</strong> <br/> Por favor, entre em contato com a administração.";
                        Response.Redirect("../FormAcessoNegado.aspx");
                    }
                    else
                    {
                        if (horario.Bloqueado)
                        {
                            Application["AcessoPagina"] = "<strong>Usuário, o horário para utilização do módulo Cartão SUS que consta em seu perfil no dia de hoje (" + horario.DiaEquivalente + ") está bloqueado!</strong> <br/> Por favor, entre em contato com a administração.";
                            Response.Redirect("../FormAcessoNegado.aspx");
                        }

                        if (!horario.PeriodoValido)
                        {
                            Application["AcessoPagina"] = "<strong>Usuário, o horário para utilização do módulo Cartão SUS que consta em seu perfil no dia de hoje (" + horario.DiaEquivalente + ") é de " +
                                horario.HoraInicial.Substring(0, 2) + ":" + horario.HoraInicial.Substring(2, 2) + "h as " + horario.HoraFinal.Substring(0, 2) + ":" + horario.HoraFinal.Substring(2, 2) + "h.</strong> <br/> Por favor, entre em contato com a administração.";
                            Response.Redirect("../FormAcessoNegado.aspx");
                        }
                    }
                }

                HyperLink navegador = (HyperLink)((MasterMain)Master).FindControl("HyperLink_Modulo");
                navegador.Text = "Cartão SUS";
                navegador.NavigateUrl = "~/Paciente/Default.aspx"; //Página default do módulo
            }
        }
    }
}
