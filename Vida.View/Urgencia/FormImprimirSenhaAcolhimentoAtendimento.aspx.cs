using System;
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
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.DAO;

namespace ViverMais.View.Urgencia
{
    public partial class FormImprimirSenhaAcolhimentoAtendimento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Aqui será gerado ou impresso as senhas de acolhimento e atendimento para um
                //registro eletrônico. Ele pode ser gerado neste momento e depois impresso, caso aconteça algum problema
                //de comunicação entre o Urgência e o WebService nas suas respectativas funcionalidades.
                long co_prontuario;

                if (Request["co_prontuario"] != null && long.TryParse(Request["co_prontuario"].ToString(), out co_prontuario)
                    && Request["tipo_impressao"] != null && (Request["tipo_impressao"].ToString().Equals("acolhimento") || Request["tipo_impressao"].ToString().Equals("atendimento")))
                {
                    IProntuario iProntuario = Factory.GetInstance<IProntuario>();
                    string tipo_impressao = Request["tipo_impressao"].ToString();

                    if (tipo_impressao.Equals("acolhimento"))
                        this.Literal_Senha.Text = iProntuario.GerarSenhaAcolhimento(co_prontuario);
                    else
                        this.Literal_Senha.Text = iProntuario.GerarSenhaAtendimento(co_prontuario);

                    this.Literal_Senha.DataBind();
                }
            }
        }
    }
}
