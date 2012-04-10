using System;
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
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;

namespace ViverMais.View.Seguranca
{
    public partial class AlterarSenha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ccAlterarSenha.SalvarAlteracao.Click += new EventHandler(btnSalvar_Click);
            ccAlterarSenha.VoltarPagina.Click += new EventHandler(btnVoltar_Click);
            ccAlterarSenha.ImgVoltar.Attributes.Add("onmouseover", "this.src='img/btn_voltar2.png';");
            ccAlterarSenha.ImgVoltar.Attributes.Add("onmouseout", "this.src='img/btn_voltar1.png';");
            ccAlterarSenha.ImgSalvar.Attributes.Add("onmouseout", "this.src='img/btn_salvar_1.png';");
            ccAlterarSenha.ImgSalvar.Attributes.Add("onmouseover", "this.src='img/btn_salvar_2.png';");
            ccAlterarSenha.ImgSalvar.Src = "img/btn_salvar_1.png";
            ccAlterarSenha.ImgVoltar.Src = "img/btn_voltar1.png";

            ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
            if (!iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "ALTERAR_SENHA", Modulo.SEGURANCA))
                Response.Redirect("FormAcessoNegado.aspx?opcao=1");
            //{
                //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Você não tem permissão para acessar essa página. Em caso de dúViverMais, entre em contato.');window.location='Default.aspx';</script>");
            //}
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            if (Session["Usuario"] != null)
            {
                ViverMais.Model.Usuario usuario = iViverMais.BuscarPorCodigo<ViverMais.Model.Usuario>(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo);
                if (usuario.Senha != ccAlterarSenha.SenhaAtual.Text)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A senha atual não confere!');", true);
                    return;
                }

                usuario.Senha = ccAlterarSenha.NovaSenha.Text;
                iViverMais.Atualizar(usuario);
                Session["Usuario"] = usuario;
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Sua senha foi alterada com sucesso.');", true);
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}
