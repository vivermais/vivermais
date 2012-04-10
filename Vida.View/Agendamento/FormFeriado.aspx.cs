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
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;


namespace ViverMais.View.Agendamento
{
    public partial class FormFeriado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CADASTRAR_FERIADO",Modulo.AGENDAMENTO))
                {
                    lblCriticaData.Visible = false;
                    lblCriticaDescrição.Visible = false;
                    lblCriticaTipo.Visible = false;

                    //Caso seja uma edição
                    if (Request.QueryString["codigo"] != null)
                    {
                        IFeriado iFeriado = Factory.GetInstance<IFeriado>();
                        Feriado feriado = iFeriado.BuscarPorCodigo<Feriado>(int.Parse(Request.QueryString["codigo"]));
                        if (feriado != null)
                        {
                            tbxData.Text = feriado.Data.ToString("dd/MM/yyyy");
                            tbxDescricao.Text = feriado.Descricao;
                            ddlFeriado.SelectedValue = feriado.Tipo.ToString();
                        }
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
            
        }

        protected void Incluir_Click(object sender, EventArgs e)
        {
            IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
            IFeriado iFeriado = Factory.GetInstance<IFeriado>();
            ViverMais.Model.Feriado feriado = new Feriado();

            //Caso seja uma edição
            if (Request.QueryString["codigo"] != null)
            {
                feriado = iFeriado.BuscarPorCodigo<Feriado>(int.Parse(Request.QueryString["codigo"]));
            }
            else
            {
                //Verifica se ja existe um feriado cadastrado com essa data
                if (iFeriado.VerificaData(DateTime.Parse(tbxData.Text)))
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Já Existe um Feriado Cadastrado com Esta Data.');</script>");
                    return;
                }
            }
            feriado.Data = DateTime.Parse(tbxData.Text);
            feriado.Tipo = char.Parse(ddlFeriado.SelectedValue);
            feriado.Descricao = tbxDescricao.Text;
            iAgendamento.Salvar(feriado);
            Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 1, "ID:"+ feriado.Codigo+" Nome Feriado:" + feriado.Descricao));
            ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Dados Salvos com Sucesso!'); window.location='FormFeriado.aspx'</script>");
        }
    }
}
