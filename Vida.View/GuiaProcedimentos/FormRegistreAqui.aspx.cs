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
using System.Net.Mail;
using System.Net;

namespace ViverMais.View.GuiaProcedimentos
{
    public partial class FormRegistreAqui : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {

            string[] destinatario = { "guia.saude@salvador.ba.gov.br" };
            string subject = "ViverMais - " + ddlTipoRegistro.SelectedItem.Value + " - Módulo Catálogo";
            
            string body = tbxMensagem.Text + "\n";
            body += "Nome: " + tbxNome.Text + "\n";
            body += "Email: " + tbxEmail.Text + "\n";
            body += "Telefone: " + tbxTelefone.Text + "\n";
            body += "Local de Origem: " + tbxLocalOrigem.Text + "\n";

            EnviarEmail(destinatario, subject, body);

            ScriptManager.RegisterClientScriptBlock(this, typeof(String), "ok", "alert('Email enviado com sucesso!');", true);
        }

        public static bool EnviarEmail(string[] destinatarios, string assunto, string conteudo)
        {
            MailMessage email = new MailMessage();

            email.Subject = assunto;
            email.Body = conteudo;
            email.IsBodyHtml = true;

            for (int i = 0; i < destinatarios.Count(); i++)
                email.To.Add(destinatarios[i]);

            SmtpClient smtp = new SmtpClient();

            try
            {
                smtp.Send(email);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
