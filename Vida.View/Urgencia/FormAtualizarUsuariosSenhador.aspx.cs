﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using System.Data;

namespace ViverMais.View.Urgencia
{
    public partial class FormAtualizarUsuariosSenhador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "ATUALIZAR_USUARIOS_SENHADOR", Modulo.URGENCIA))
                {
                    Usuario usuario = (Usuario)Session["Usuario"];
                    this.Label_Unidade.Text = usuario.Unidade.NomeFantasia;
                }else
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
            }
        }

        protected void OnClick_AtualizarUsuarios(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["Usuario"];

            IUsuarioProfissionalUrgence iUsuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>();
            DataTable usuariosNaoAtualizados = iUsuarioProfissional.AtualizarUsuariosSenhador(usuario.Unidade.CNES);

             if (usuariosNaoAtualizados.Rows.Count != 0)
                 this.lbAtualizacaoIncompleta.Visible = true;

             this.GridView_UsuariosNaoAtualizados.DataSource = usuariosNaoAtualizados;
             this.GridView_UsuariosNaoAtualizados.DataBind();
        }
    }
}
