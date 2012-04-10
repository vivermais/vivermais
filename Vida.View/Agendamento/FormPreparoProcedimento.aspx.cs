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
using ViverMais.Model;
//.Entities.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.DAO;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.View.Agendamento.Helpers;

namespace ViverMais.View.Agendamento
{
    public partial class FormPreparoProcedimento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "PREPARO_PROCEDIMENTO", Modulo.AGENDAMENTO))
                {
                    //Verifica se é uma atualização
                    if (Request.QueryString["codigo"] != null)
                    {
                        Preparo preparo = Factory.GetInstance<IPreparo>().BuscaPreparo<Preparo>(int.Parse(Request.QueryString["codigo"]));
                        txtPreparo.Value = preparo.Descricao;
                        //tbxPreparo.Content = preparo.Descricao;
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);

            }
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
            
        //}

        //protected void btnFechar_Click(object sender, ImageClickEventArgs e)
        //{
        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "function",
        //        "Javascript:document.getElementById('cinza').style.display='none';javascript:document.getElementById('mensagem').style.display='none';void(0)", true);
        //}

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtPreparo.Value != string.Empty)
            //if (tbxPreparo.Content != "")
            {
                IPreparo iPreparo = Factory.GetInstance<IPreparo>();
                Preparo preparo = new Preparo();
                if (Request.QueryString["codigo"] != null)
                {
                    preparo = iPreparo.BuscaPreparo<Preparo>(int.Parse(Request.QueryString["codigo"]));
                    preparo.Descricao = txtPreparo.Value;
                    //preparo.Descricao = tbxPreparo.Content;
                    iPreparo.Salvar(preparo);
                    iPreparo.Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 22, preparo.Codigo.ToString()));
                    
                }
                else
                {
                    preparo.Descricao = txtPreparo.Value;
                    iPreparo.Salvar(preparo);
                    iPreparo.Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 3, preparo.Codigo.ToString()));
                    
                    //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Dados salvos com sucesso!');window.location='BuscaPreparoProcedimento.aspx';</script>");
                }
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados Salvos com Sucesso!');", true);
                Redirector.Redirect("BuscaPreparoProcedimento.aspx", "_parent", "");
                //preparo.Descricao = tbxPreparo.Content;
                //Factory.GetInstance<IPreparo>().Salvar(preparo);
                //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Dados salvos com sucesso!');window.location='BuscaPreparoProcedimento.aspx';</script>");
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Preencha a descrição do Preparo!');", true);
                return;
                //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Preencha a descrição do Preparo!');</script>");
            }
        }
    }
}
