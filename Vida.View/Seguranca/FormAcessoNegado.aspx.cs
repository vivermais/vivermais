﻿using System;
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

namespace ViverMais.View.Seguranca
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
                        case 2:
                            this.LiteralMensagem.Text = @"<strong>Usuário, você não tem privilégios suficientes para acessar esta página!</strong><br />
                                            Por favor, entre em contato com a administração.";
                            break;
                    }

                    this.LiteralMensagem.DataBind();
                }
            }
        }
    }
}