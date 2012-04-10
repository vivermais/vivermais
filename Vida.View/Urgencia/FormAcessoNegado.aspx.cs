using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ViverMais.View.Urgencia
{
    public partial class FormAcessoNegado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.LiteralMensagem.Text = string.Empty;
                
                //Favor, não mudar os códigos de redirecionamento
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
                            this.LiteralMensagem.Text = @"<strong>Usuário, somente profissionais médicos têm acesso a página solicitada!</strong><br/>
                                Por favor, entre em contato com a administração.";
                            break;
                        case 3:
                            this.LiteralMensagem.Text = @"<strong>Usuário, somente profissionais de enfermagem têm acesso a página solicitada!</strong><br/>
                                Por favor, entre em contato com a administração.";
                            break;
                        case 4:
                            this.LiteralMensagem.Text = @"<strong>Usuário, o seu profissional identificado não possui vínculo com a unidade logada!</strong><br/>
                                Por favor, entre em contato com a administração.";
                            break;
                        case 5:
                            this.LiteralMensagem.Text = @"<strong>Profissional não identificado!</strong><br/>
                                Por favor, identifique o usuário junto à administração.";
                            break;
                        case 6:
                            this.LiteralMensagem.Text = @"<strong>Usuário, somente profissionais médicos e de enfermagem têm acesso a página solicitada!</strong><br/>
                                Por favor, entre em contato com a administração.";
                            break;
                        case 7:
                            this.LiteralMensagem.Text = @"<strong>Usuário, somente profissionais técnicos de enfermagem têm acesso a página solicitada!</strong><br/>
                                Por favor, entre em contato com a administração.";
                            break;
                    }
                }
                else
                {
                    if (Application["AcessoPagina"] != null)
                        this.LiteralMensagem.Text = Application["AcessoPagina"].ToString();
                }

                this.LiteralMensagem.DataBind();
            }
        }
    }
}
