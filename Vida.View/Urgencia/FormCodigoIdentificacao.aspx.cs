﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.Model;

namespace ViverMais.View.Urgencia
{
    public partial class FormCodigoIdentificacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlCNES.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
                ddlUsuario.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
                //base.SetFocus(this.ButtonValidarCodigo);
            }
        }

        /// <summary>
        /// Válida a identificação do usuário
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Validar(object sender, EventArgs e) 
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
                    ViverMais.Model.UsuarioProfissionalUrgence usuarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<ViverMais.Model.UsuarioProfissionalUrgence>(usuario.Codigo);

                    if (usuarioprofissional != null)
                    {
                        Panel_CodigoIdentificacao.Visible = true;
                        ViewState["indexOfProfissional"] = usuario.Codigo;
                        lbCodigo.Text = !string.IsNullOrEmpty(usuarioprofissional.Identificacao) ? usuarioprofissional.Identificacao : "Nenhum código encontrado.";
                        TextBox_Codigo.Attributes.Add("value", usuarioprofissional.Identificacao);
                        TextBox_ConfirmaCodigo.Attributes.Add("value", usuarioprofissional.Identificacao);
                    }
                    else
                    {
                        Panel_CodigoIdentificacao.Visible = false;
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Profissional não identificado!');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário ou Senha Incorretos!');", true);
                    //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Usuário ou Senha Incorretos');</script>");
                }
            }
        }

        /// <summary>
        /// Salva o código de identificação do profissional
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_SalvarCodigo(object sender, EventArgs e) 
        {
            IUsuarioProfissionalUrgence iUsuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>();
            bool codigovalido = iUsuarioProfissional.ValidaCodigoIdentificacao(TextBox_Codigo.Text, int.Parse(ViewState["indexOfProfissional"].ToString()), ((Usuario)Session["Usuario"]).Unidade.CNES);

            if (codigovalido)
            {
                ViverMais.Model.UsuarioProfissionalUrgence ususarioprofissional = iUsuarioProfissional.BuscarPorCodigo<ViverMais.Model.UsuarioProfissionalUrgence>(int.Parse(ViewState["indexOfProfissional"].ToString()));
                ususarioprofissional.Identificacao = TextBox_Codigo.Text;

                try
                {
                    iUsuarioProfissional.Atualizar(ususarioprofissional);
                    //Regitro de Log
                    iUsuarioProfissional.Inserir(new LogUrgencia(DateTime.Now, ususarioprofissional.Id_Usuario, 2, "id=" + ususarioprofissional.Identificacao));
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Código salvo com sucesso!');location='Default.aspx';", true);
                }
                catch (Exception f)
                {
                    throw f;
                }
            }else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, o nível de segurança deste codigo de identificação é muito baixo. Por favor, informe outro código!!');", true);
        }

        /// <summary>
        /// Cancela a mudança do código de identificação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_CancelarCodigo(object sender, EventArgs e) 
        {
            Panel_CodigoIdentificacao.Visible = false;
        }

        protected void ddlCNES_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlUsuario.Items.Clear();
            ddlUsuario.Items.Add(new ListItem("Selecione...", "-1"));

            if (ddlCNES.SelectedValue != "-1")
            {
                Usuario usuario = Factory.GetInstance<IUsuario>().BuscarPorCodigo<Usuario>(int.Parse(ddlCNES.SelectedValue));
                ddlUsuario.Items.Add(new ListItem(usuario.Nome, usuario.Codigo.ToString()));
            }
        }

        protected void ddlUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUsuario.SelectedValue != "-1")
            {
                Usuario usuario = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<Usuario>(int.Parse(ddlUsuario.SelectedValue));
                tbxCartaoSUS.Text = usuario.CartaoSUS;
            }
        }

        protected void imgBtnCartao_Click(object sender, ImageClickEventArgs e)
        {
            IList<Usuario> usuarios = Factory.GetInstance<IUsuario>().BuscarPorCartaoSUS<Usuario>(tbxCartaoSUS.Text);
            //Não existe nenhum usuário com o cartão sus informado
            if (usuarios.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não foi encontrado nenhum Usuário para esse Cartão SUS.');", true);
                //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Não foi encontrado nenhum Usuário para esse Cartão SUS');</script>");
                return;
            }
            //Existe um usuário com o cartão sus informado
            else if (usuarios.Count == 1)
            {
                tbxCNES.Text = usuarios.First().Unidade.CNES;
                ddlUsuario.Items.Clear();
                ddlUsuario.Items.Add(new ListItem("Selecione...", "-1"));
                ddlUsuario.Items.Add(new ListItem(usuarios.First().Nome, usuarios.First().Codigo.ToString()));
                tbxCNES.Visible = true;
                ddlCNES.Visible = false;
            }
            //Existe mais de um usuário com o cartão sus informado
            else
            {
                tbxCNES.Visible = false;
                ddlCNES.Visible = true;
                ddlCNES.Items.Clear();
                ddlCNES.Items.Add(new ListItem("Selecione...", "-1"));
                foreach (Usuario usuario in usuarios)
                {
                    ddlCNES.Items.Add(new ListItem(usuario.Unidade.NomeFantasia, usuario.Codigo.ToString()));
                }
            }
        }

        protected void imgBtnCNES_Click(object sender, ImageClickEventArgs e)
        {
            IList<Usuario> usuarios = Factory.GetInstance<IUsuario>().BuscarUsuariosPorCNES<Usuario>(tbxCNES.Text);
            ddlUsuario.Items.Clear();

            ddlUsuario.DataValueField = "Codigo";
            ddlUsuario.DataTextField = "Nome";

            ddlUsuario.DataSource = usuarios;
            ddlUsuario.DataBind();

            ddlUsuario.Items.Insert(0, new ListItem("Selecione...", "-1"));
        }
    }
}
