﻿using System;
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

namespace ViverMais.View
{
    public partial class FaleConosco : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkEnviar_Click(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage(tbxEmail.Text, "ViverMais.saude@salvador.ba.gov.br");
            mail.IsBodyHtml = true;
            mail.Subject = tbxAssunto.Text;
            mail.Body += "<html>";
            mail.Body += "<body>";
            mail.Body += "<p>De: " + tbxNome.Text + "</p>";
            mail.Body += "<p>" + tbxMensagem.Text + "</p>";
            mail.Body += "</body>";
            mail.Body += "</html>";
            new SmtpClient().Send(mail);
            ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Sua Mensagem foi Enviada!');</script>");
            tbxAssunto.Text = string.Empty;
            tbxEmail.Text = string.Empty;
            tbxMensagem.Text = string.Empty;
            tbxNome.Text = string.Empty;
        }
    }
}
