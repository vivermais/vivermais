﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ViverMais.View.EstabelecimentoSaude
{
    public partial class FormAcessoNegado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int opcao;
                if (Request["opcao"] != null && int.TryParse(Request["opcao"].ToString(), out opcao))
                {
                    switch (opcao)
                    {
                        case 1:
                            this.LiteralMensagem.Text = @"<strong>Usuário, você não tem permissão para acessar a página solicitada!</strong><br />
                                            Por favor, entre em contato com a administração.";
                            break;
                    }

                    this.LiteralMensagem.DataBind();
                }
            }
        }
    }
}
