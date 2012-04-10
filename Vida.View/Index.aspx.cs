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
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace ViverMais.View
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
                Response.Redirect("Home.aspx");

            if (!IsPostBack)
            {
                LimparCampo(this.ddlCNES);
                LimparCampo(this.ddlUsuario);

                int co_usuario;

                if (Request["co_usuario"] != null && int.TryParse(Request["co_usuario"].ToString(), out co_usuario))
                {
                    ViverMais.Model.Usuario usuario = Factory.GetInstance<IUsuario>().BuscarPorCodigo<ViverMais.Model.Usuario>(co_usuario);

                    if (usuario != null)
                    {
                        tbxCartaoSUS.Text = usuario.CartaoSUS;
                        imgBtnCartao_Click(new object(), new ImageClickEventArgs(0, 0));
                        ddlCNES.SelectedValue = usuario.Codigo.ToString();
                        ddlCNES_SelectedIndexChanged(new object(), new EventArgs());
                    }
                }
            }
        }

        protected void imgBtnLogin_Click(object sender, ImageClickEventArgs e)
        {
            ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
            ViverMais.Model.Usuario usuario = null;
            //Verifica se um usuário foi selecionado no combo
            if (ddlUsuario.SelectedValue != "" && ddlUsuario.SelectedValue != "-1")
            {
                usuario = Factory.GetInstance<IUsuario>().BuscarPorCodigo<Usuario>(int.Parse(ddlUsuario.SelectedValue));
                //Verifica se a senha digitada é igual à senha do usuário
                if (usuario.Senha == tbxSenha.Text)
                {
                    if (iseguranca.VerificarPermissao(usuario.Codigo, "EFETUAR_LOGIN", Modulo.SEGURANCA))
                    {
                        if (usuario.Senha.Equals("123456"))
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "javascript:GB_show('Trocar Senha','../Seguranca/FormTrocarSenha.aspx?co_usuario=" + usuario.Codigo + "',500,900);", true);

                            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "window.open('Seguranca/FormTrocarSenha.aspx?co_usuario=" + usuario.Codigo + "');", true);
                            //Response.Redirect("~/Seguranca/FormTrocarSenha.aspx?co_usuario=" + usuario.Codigo);
                        }
                        else
                        {
                            //if (usuario.CartaoSUS == "898000237276137")
                            //{
                            Session["Usuario"] = usuario;
                            Factory.GetInstance<IViverMaisServiceFacade>().Inserir(new LogViverMais(DateTime.Now, usuario, 4, usuario.Nome));

                            string nome = usuario.Nome.Substring(0, usuario.Nome.IndexOf(' '));

                            //FormsAuthenticationTicket tkt;
                            //string cookiestr;
                            //HttpCookie ck;
                            //tkt = new FormsAuthenticationTicket(1, nome, DateTime.Now,  DateTime.Now.AddMinutes(1), false,nome);
                            //cookiestr = FormsAuthentication.Encrypt(tkt);
                            //ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);

                            //ck.Path = FormsAuthentication.FormsCookiePath;
                            //Response.Cookies.Add(ck);
                            //FormsAuthentication.RedirectFromLoginPage("d", false);

                            FormsAuthentication.RedirectFromLoginPage(nome, false);
                            //}
                        }
                    }
                    else
                        ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Usuário você não tem a permissão necessária para acessar o sistema. Por favor, entre em contato com a administração.');</script>");
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Usuário ou Senha Incorretos.');</script>");
                }
            }
        }

        protected void ddlCNES_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCNES.SelectedValue != "-1")
            {
                LimparCampo(this.ddlUsuario);
                Usuario usuario = Factory.GetInstance<IUsuario>().BuscarPorCodigo<Usuario>(int.Parse(ddlCNES.SelectedValue));
                ddlUsuario.Items.Add(new ListItem(usuario.Nome, usuario.Codigo.ToString()));
                ddlUsuario.SelectedValue = usuario.Codigo.ToString();
                tbxSenha.Focus();
            }
            else
                ddlUsuario.SelectedValue = "-1";
        }

        protected void imgBtnCartao_Click(object sender, ImageClickEventArgs e)
        {
            IList<Usuario> usuarios = Factory.GetInstance<IUsuario>().BuscarPorCartaoSUS<Usuario>(tbxCartaoSUS.Text).ToList();
            ////Não existe nenhum usuário com o cartão sus informado
            if (usuarios.Count == 0)
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Não foi encontrado nenhum Usuário para esse Cartão SUS.');</script>");
                return;
            }
            ////Existe um usuário com o cartão sus informado
            else
            {
                LimparCampo(this.ddlCNES);

                if (usuarios.Count == 1)
                {
                    ddlCNES.Items.Add(new ListItem(usuarios.First().Unidade.NomeFantasia, usuarios.First().Codigo.ToString()));
                    ddlCNES.SelectedValue = usuarios.First().Codigo.ToString();
                    ddlCNES_SelectedIndexChanged(new object(), new EventArgs());
                    tbxSenha.Focus();
                }
                ////Existe mais de um usuário com o cartão sus informado
                else
                {
                    LimparCampo(this.ddlUsuario);

                    foreach (Usuario usuario in usuarios)
                        ddlCNES.Items.Add(new ListItem(usuario.Unidade.NomeFantasia, usuario.Codigo.ToString()));

                    ddlCNES.Focus();
                }
            }
        }

        protected void OnSelectedIndexChanged_SelecionaCNES(object sender, EventArgs e)
        {
            ddlCNES.SelectedValue = ddlUsuario.SelectedValue;
        }

        private void LimparCampo(DropDownList dropdown)
        {
            dropdown.Items.Clear();
            dropdown.Items.Add(new ListItem("Selecione...", "-1"));
            dropdown.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
        }

        protected void OnClick_LimparCampos(object sender, EventArgs e)
        {
            tbxCartaoSUS.Text = "";
            LimparCampo(this.ddlUsuario);
            LimparCampo(this.ddlCNES);
        }

        protected void OnClick_btnFechar(object sender, EventArgs e)
        {
            PanelMensagem.Visible = false;
        }
    }
}
