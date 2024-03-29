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
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.ServiceFacade.ServiceFacades.Atendimento;

namespace ViverMais.View.Atendimento
{
    public partial class FormImprimirSenhaRecepcao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Aqui será gerado ou impresso as senhas de acolhimento e atendimento para um
                //registro eletrônico. Ele pode ser gerado neste momento e depois impresso, caso aconteça algum problema
                //de comunicação entre o Urgência e o WebService nas suas respectativas funcionalidades.
                long co_registroEletronico;
                long co_servico;

                if (Request["co_registro_eletronico"] != null && long.TryParse(Request["co_registro_eletronico"].ToString(), out co_registroEletronico)
                    && Request["co_servico"] != null && long.TryParse(Request["co_servico"].ToString(), out co_servico))
                {
                    IRegistroEletronicoAtendimento iRegistroeletronico = Factory.GetInstance<IRegistroEletronicoAtendimento>();

                    this.Literal_Senha.Text = iRegistroeletronico.GerarSenhaAtendimento(co_registroEletronico);

                    this.Literal_Senha.DataBind();
                }
            }
        }
    }
}
