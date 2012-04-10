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
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using System.Collections.Generic;


namespace ViverMais.View.Agendamento
{
    public partial class FormCadastrarFaixa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CADASTRAR_FAIXA", Modulo.AGENDAMENTO))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                GridViewListaFaixas.PageIndexChanging += new GridViewPageEventHandler(GridViewListaFaixas_PageIndexChanging);
                CarregaFaixas();

            }
        }
        void CarregaFaixas()
        {
            IFaixa iFaixa = Factory.GetInstance<IFaixa>();
            IList<Faixa> faixas = iFaixa.ListarTodos<Faixa>();
            //IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
            //ViverMais.Model.Faixa faixa = Factory.GetInstance<IFaixa>().BuscarFaixaAPAC<ViverMais.Model.Faixa>().FirstOrDefault();
            //for (int i = int.Parse(faixa.FaixaInicial); i < int.Parse(faixa.FaixaFinal); i++)//Pra cada arquivo de laudo
            //{
            //    Solicitacao solicitacao = Factory.GetInstance<ISolicitacao>().BuscaFaixaporIdentificador<Solicitacao>(i.ToString("0000000"));
            //    if (solicitacao != null)
            //    {
            //        if (solicitacao.Identificador.Substring(0, 2) == "29")
            //        {
            //            ViverMais.Model.FaixaUtilizada faixaUtilizada = new FaixaUtilizada();
            //            faixaUtilizada.Faixa = faixa;
            //            faixaUtilizada.Faixa_Utilizada = i.ToString("0000000");
            //            iAgendamento.Inserir(faixaUtilizada);
            //        }
            //    }
            //}
            Session["Faixas"] = faixas;
            GridViewListaFaixas.DataSource = faixas;
            GridViewListaFaixas.DataBind();
        }


        protected void GridViewListaFaixas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Retorna a propriedade Feriados que está definida na sessão
            IList<Faixa> faixas = (IList<Faixa>)Session["Faixas"];
            GridViewListaFaixas.DataSource = faixas;
            GridViewListaFaixas.PageIndex = e.NewPageIndex;
            GridViewListaFaixas.DataBind();
        }
        protected void Incluir_Click(object sender, EventArgs e)
        {
            IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
            ViverMais.Model.Faixa faixa = new Faixa();
            //VERIFICA SE A FAIXA FINAL É MENOR QUE A FAIXA INICIAL
            if (tbxFaixaInicial.Text.Length < 7)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A Faixa Inicial não pode ser menor que 7 digitos!');", true);
                return;
            }
            if (tbxFaixaFinal.Text.Length < 7)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A Faixa Final não pode ser menor que 7 digitos!');", true);
                return;
            }
            if (long.Parse(tbxFaixaFinal.Text) < long.Parse(tbxFaixaInicial.Text))
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('A Faixa Final não pode ser menor que a Faixa Incial');window.location='FormCadastrarFaixa.aspx'</script>");
                return;

            }
            faixa.FaixaInicial = tbxFaixaInicial.Text;
            faixa.FaixaFinal = tbxFaixaFinal.Text;
            faixa.Quantitativo = long.Parse(lblQtd.Text);
            faixa.Quantidade_utilizada = 0;
            faixa.Tipo = char.Parse(ddlTipo.SelectedValue);
            faixa.Ano_vigencia = tbxAnoVigencia.Text;
            faixa.DataInclusao = DateTime.Now;
            iAgendamento.Salvar(faixa);
            Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 10, "ID_FAIXAETARIA:" + faixa.Codigo));
            ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Faixa Cadastrada com Sucesso' );window.location='FormCadastrarFaixa.aspx'</script>");
            return;

        }

        protected void tbxFaixaFinal_TextChanged(object sender, EventArgs e)
        {
            lblQtd.Text = (int.Parse(tbxFaixaFinal.Text) - int.Parse(tbxFaixaInicial.Text) + 1).ToString();
        }



    }
}
