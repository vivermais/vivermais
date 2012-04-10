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
using Vida.ServiceFacade.ServiceFacades.Seguranca;
using Vida.DAO;
using System.Collections.Generic;
using Vida.ServiceFacade.ServiceFacades;
using Vida.Model;
using Vida.ServiceFacade.ServiceFacades.Senhador;

namespace Vida.View.Senhador
{
    public partial class FormCadastrarServico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
                if (!iseguranca.VerificarPermissao(((Vida.Model.Usuario)Session["Usuario"]).Codigo, "MANTER_SERVICO", Modulo.SENHADOR))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);


            }

        }
        protected void Incluir_Click(object sender, EventArgs e)
        {
            ISenhador iSenhador = Factory.GetInstance<ISenhador>();
            IServicoSenhador iServicoSenhador = Factory.GetInstance<IServicoSenhador>();
            Vida.Model.ServicoSenhador servicoSenhador = new ServicoSenhador();
            servicoSenhador = iServicoSenhador.BuscarPorNome<ServicoSenhador>(tbxNomeServico.Text);
            
            //Caso seja uma edição
            if (servicoSenhador != null)
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Serviço já Cadastrado.');</script>");
                return;
            }
            servicoSenhador.Nome = tbxNomeServico.Text.Trim();
            iSenhador.Salvar(servicoSenhador);
            ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Dados Salvos com Sucesso!'); window.location='FormCadastrarServico.aspx'</script>");
        }
    }
}
