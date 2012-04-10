﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;

namespace ViverMais.View.Seguranca
{
    public partial class FormTrocarSenha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ccalterarsenha.SalvarAlteracao.Click += new EventHandler(SalvarAlteracao_Click);
            ccalterarsenha.VoltarPagina.Click += new EventHandler(VoltarPagina_Click);
            ccalterarsenha.ImgVoltar.Src = "~/Seguranca/img/btn-fechar.png";
            ccalterarsenha.ImgSalvar.Src = "~/Seguranca/img/btn-salvar.png";
            ccalterarsenha.ImgVoltar.Attributes.Remove("onmouseover");
            ccalterarsenha.ImgVoltar.Attributes.Remove("onmouseout");
            ccalterarsenha.ImgSalvar.Attributes.Remove("onmouseout");
            ccalterarsenha.ImgSalvar.Attributes.Remove("onmouseover");

            int temp;
            if (Request["co_usuario"] != null && int.TryParse(Request["co_usuario"].ToString(), out temp))
            {
                ViewState["co_usuario"] = Request["co_usuario"].ToString();
                Usuario usuario = Factory.GetInstance<IUsuario>().BuscarPorCodigo<ViverMais.Model.Usuario>(temp);
                ccalterarsenha.SetSenhaAtual(usuario.Senha);
                ccalterarsenha.Visible = true;
            }
        }

        protected void SalvarAlteracao_Click(object sender, EventArgs e)
        {
            Usuario usuario = Factory.GetInstance<IUsuario>().BuscarPorCodigo<ViverMais.Model.Usuario>(int.Parse(ViewState["co_usuario"].ToString()));

            if (usuario.Senha != ccalterarsenha.SenhaAtual.Text)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A senha atual não confere!');", true);
                return;
            }

            try
            {
                usuario.Senha = ccalterarsenha.NovaSenha.Text;
                Factory.GetInstance<IUsuario>().Atualizar(usuario);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, sua senha foi alterada com sucesso. Agora você pode acessar o sistema.');parent.parent.document.location.href='../Index.aspx?co_usuario=" + usuario.Codigo + "';parent.parent.GB_hide();", true);
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, sua senha foi alterada com sucesso. Agora você pode acessar o sistema.');window.opener.location.href='../Index.aspx?co_usuario=" + usuario.Codigo + "';self.close();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void VoltarPagina_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "parent.parent.GB_hide();", true);
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "self.close();", true);
            //Response.Redirect("~/Index.aspx");
        }
    }
}
