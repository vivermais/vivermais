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
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.BPA;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.EnvioBPA
{
    public partial class ExibeProtocoloEnvio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
            if (!iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "ENVIAR_BPA",Modulo.ENVIO_BPA))
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Você não tem permissão para acessar essa página. Em caso de dúViverMais, entre em contato.');window.location='../Home.aspx';</script>");
            }

            if (Session["protocolo"] != null) 
            {
                ProtocoloEnvioBPA protocolo = (ProtocoloEnvioBPA)Session["protocolo"];
                protocolo = Factory.GetInstance<IEnviarBPA>().BuscarProtocoloEnvio<ProtocoloEnvioBPA>(protocolo.Usuario, protocolo.EstabelecimentoSaude, protocolo.DataEnvio);
                this.lblLogin.Text = protocolo.Usuario.Login;
                this.lblCNES.Text = protocolo.EstabelecimentoSaude.CNES;
                this.lblDataEnvio.Text = protocolo.DataEnvio.ToString("dd/MM/yyyy hh:mm:ss");
                this.lblCompetencia.Text = protocolo.Competencia.ToString();
                this.lblArquivoEnviado.Text = protocolo.Arquivo;
                this.lblTamanhoArquivo.Text = protocolo.TamanhoArquivo + " bytes";
                this.lblNumeroControle.Text = protocolo.NumeroControle;
                this.lblNumeroProtocolo.Text = protocolo.Codigo.ToString();
            }
        }
    }
}
