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
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;

namespace ViverMais.View.Urgencia
{
    public partial class MasterUrgencia : System.Web.UI.MasterPage
    {
        public bool MenuVisivel
        {
            set
            {
                if (value == false)
                    ((Panel)((MasterMain)Master).FindControl("Panel_Modulo")).Attributes.Add("onclick", "javascript:return false;");
            }
        }

        public bool AjaxLoading
        {
            set
            {
                this.UpDateProgressUrgence.Visible = value;

                //if (value)
                //    this.UpDateProgressUrgence.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<IRelatorioUrgencia>().PAAtivo(((Usuario)Session["Usuario"]).Unidade.CNES))
                    Response.Redirect("../FormIndisponivelUrgencia.aspx");
                else
                {
                    Usuario usuario = Session["Usuario"] as Usuario;
                    Perfil perfil = usuario.Perfis.Where(p => p.Modulo.Codigo == Modulo.URGENCIA).FirstOrDefault();

                    if (perfil == null)
                    {
                        Application["AcessoPagina"] = "<strong>Usuário, você não possui o perfil necessário para acessar o módulo Urgência!</strong> <br/> Por favor, entre em contato com a administração.";
                        Response.Redirect("../FormAcessoNegado.aspx");
                    }
                    else
                    {
                        IPerfil iPerfil = Factory.GetInstance<IPerfil>();
                        HorarioPerfil horario = iPerfil.BuscarHorario<HorarioPerfil>(perfil.Codigo, DateTime.Now.DayOfWeek);

                        if (horario == null)
                        {
                            Application["AcessoPagina"] = "<strong>Usuário, o seu perfil para o módulo Urgência não possui um horário de utilização!</strong> <br/> Por favor, entre em contato com a administração.";
                            Response.Redirect("../FormAcessoNegado.aspx");
                        }
                        else
                        {
                            if (horario.Bloqueado)
                            {
                                Application["AcessoPagina"] = "<strong>Usuário, o horário para utilização do módulo Urgência que consta em seu perfil no dia de hoje (" + horario.DiaEquivalente + ") está bloqueado!</strong> <br/> Por favor, entre em contato com a administração.";
                                Response.Redirect("../FormAcessoNegado.aspx");
                            }

                            if (!horario.PeriodoValido)
                            {
                                Application["AcessoPagina"] = "<strong>Usuário, o horário para utilização do módulo Urgência que consta em seu perfil no dia de hoje (" + horario.DiaEquivalente + ") é de " +
                                    horario.HoraInicial.Substring(0, 2) + ":" + horario.HoraInicial.Substring(2, 2) + "h as " + horario.HoraFinal.Substring(0, 2) + ":" + horario.HoraFinal.Substring(2, 2) + "h.</strong> <br/> Por favor, entre em contato com a administração.";
                                Response.Redirect("../FormAcessoNegado.aspx");
                            }
                        }
                    }

                    this.Page.Title = "ViverMais - Módulo Urgência";
                    this.CarregaMenuModulo(usuario);
                }
            }
        }

        private void CarregaMenuModulo(Usuario usuario)
        {
            Literal menumodulo = (Literal)((MasterMain)Master).FindControl("barramodulo");
            //StringBuilder textomenu = new StringBuilder();
            //string app = ((Request.ApplicationPath.Equals("/") || string.IsNullOrEmpty(Request.ApplicationPath)) ? "/" : Request.ApplicationPath + "/");
            //string urlpart = Regex.Replace(HttpUtility.UrlDecode(Request.Url.ToString()), @"\s", "").
            //                Replace(Regex.Replace(HttpUtility.UrlDecode(Request.RawUrl), @"\s", ""), "") + app;

            //Session["EnderecoWebAplicacao"] = urlpart;

            HyperLink navegador = (HyperLink)((MasterMain)Master).FindControl("HyperLink_Modulo");
            navegador.Text = "Urgência e Emergência";
            navegador.NavigateUrl = "~/Urgencia/Default.aspx"; //Página default do módulo

            //textomenu.Append("<div class=\"menu-barra-module\" style=\"background: url('" + urlpart + "Urgencia/img/barramenu.png' ) no-repeat;\">");
            
            //textomenu.Append(this.DivMenu.InnerText);
            //textomenu.Append("</div>");

            XmlDocument xml = new XmlDocument();
            xml.Load(Server.MapPath("~/Urgencia/Menu.xml"));
            menumodulo.Text = Factory.GetInstance<ISeguranca>().RetornaMenuModulo(xml, HelperRedirector.URLRelativaAplicacao(), usuario.Codigo, Modulo.URGENCIA);
            
            //menumodulo.Text = textomenu.ToString();
            menumodulo.DataBind();
        }

        public void AdicionarTriggerFimPagina(Control control, string nomeevento)
        {
            ((MasterMain)this.Master).AdicionarTriggerFimPagina(control, nomeevento);
        }

        public void BloquearPagina(Panel panelconteudo)
        {
            ((MasterMain)this.Master).BloquearPagina(panelconteudo);
        }

        public void DesbloquearPagina(Panel panelconteudo)
        {
            ((MasterMain)this.Master).DesbloquearPagina(panelconteudo);
        }

        protected void quadroVagas(object sender, EventArgs e)
        {
            lbQuadroVagas.Text = "QUADRO DE VAGAS: ";
            lbQuadroVagas.Text += ((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.NomeFantasia.ToUpper();

            GridView_Vagas.DataSource = Factory.GetInstance<IVagaUrgencia>().QuadroVagas(((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.CNES, true);
            GridView_Vagas.DataBind();

            panelQuadroVagas.Visible = true;
        }

        //protected void OnRowDataBound_Vagas(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        int vagastotais = int.Parse(((Label)e.Row.Controls[0].Controls[0]).Text);
        //        int vagasocupadas = int.Parse(((Label)e.Row.Controls[1].Controls[0]).Text);
        //        ((Label)e.Row.Controls[2].Controls[0]).Text = (vagastotais - vagasocupadas).ToString();
        //    }
        //}

        protected void fecharQuadroVagas(object sender, EventArgs e)
        {
            panelQuadroVagas.Visible = false;
        }
    }
}
