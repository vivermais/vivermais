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
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.DAO;
using ViverMais.Model;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;

namespace ViverMais.View.Vacina
{
    public partial class FormParametrizacaoVacina : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_PARAMETRIZACAO_VACINA", Modulo.VACINA))
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {

                    CarregaDados();
                    //int temp;

                    //if (Request["co_salavacina"] != null && int.TryParse(Request["co_salavacina"].ToString(), out temp))
                    //{
                    //    try
                    //    {
                    //        SalaVacina salavacina = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<SalaVacina>(int.Parse(Request["co_salavacina"].ToString()));
                    //        TextBox_Responsavel.Text = salavacina.Responsavel;

                    //        if (salavacina.Ativo == Convert.ToChar(SalaVacina.DescricaoSituacao.Sim))
                    //        {
                    //            RadioButton_Ativo.Checked = false;
                    //            RadioButton_Inativo.Checked = true;
                    //        }
                    //    }
                    //    catch (Exception f)
                    //    {
                    //        throw f;
                    //    }
                    //}
                }
            }
        }

        void CarregaDados()
        {
            IList<ViverMais.Model.Vacina> vacinas = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<ViverMais.Model.Vacina>().OrderBy(p => p.Nome).ToList();
            DropDown_Vacina.Items.Add(new ListItem("Selecione...", "0"));
            foreach (ViverMais.Model.Vacina v in vacinas)
                DropDown_Vacina.Items.Add(new ListItem(v.Nome, v.Codigo.ToString()));
            DropDown_Vacina.DataBind();

            IList<ViverMais.Model.DoseVacina> doses = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<ViverMais.Model.DoseVacina>().OrderBy(p => p.Descricao).ToList();
            DropDown_Dose.Items.Add(new ListItem("Selecione...", "0"));
            foreach (ViverMais.Model.DoseVacina d in doses)
                DropDown_Dose.Items.Add(new ListItem(d.Descricao, d.Codigo.ToString()));
            DropDown_Dose.DataBind();
        }

        void CarregaGridParametrizacao() 
        {
            int co_vacina = int.Parse(DropDown_Vacina.SelectedValue.ToString());
            IList<ParametrizacaoVacina> listaparametrizacao = Factory.GetInstance<IParametrizacaoVacina>().BuscarPorVacina<ParametrizacaoVacina>(co_vacina);
            GridView_Parametrizacao.DataSource = listaparametrizacao;
            GridView_Parametrizacao.DataBind();
        }

        protected void OnSelectedIndexChanged_DropDownParametrizacao(object sender, EventArgs e) 
        { 
            int co_vacina = int.Parse(DropDown_Vacina.SelectedValue.ToString());
            IList<ParametrizacaoVacina> listaparametrizacao = Factory.GetInstance<IParametrizacaoVacina>().BuscarPorVacina<ParametrizacaoVacina>(co_vacina);
            GridView_Parametrizacao.DataSource = listaparametrizacao;
            GridView_Parametrizacao.DataBind();
        }

        protected void OnClick_Adicionar(object sender, EventArgs e) 
        {
            ParametrizacaoVacina parametrizacao = new ParametrizacaoVacina();
            //parametrizacao.Vacina = Factory.GetInstance<IVacina>().BuscarPorCodigo<ViverMais.Model.Vacina>(int.Parse(DropDown_Vacina.SelectedValue.ToString()));
            //parametrizacao.FaixaEtariaInicial = float.Parse(TextBox_FaixaEtariaInicial.Text);
            //parametrizacao.FaixaEtariaFinal = float.Parse(TextBox_FaixaEtariaFinal.Text);
            parametrizacao.ItemDoseVacina = Factory.GetInstance<IItemDoseVacina>().BuscarPorCodigo<ViverMais.Model.ItemDoseVacina>(int.Parse(DropDown_Dose.SelectedValue.ToString()));
            //if (RadioButton_Masculino.Checked)
            //    parametrizacao.Sexo = 'M';
            //else
            //    if (RadioButton_Feminino.Checked)
            //        parametrizacao.Sexo = 'F';
            //    else
            //        parametrizacao.Sexo = 'A';
            Factory.GetInstance<IVacinaServiceFacade>().Inserir(parametrizacao);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Parametrização salva com sucesso!');", true);
            CarregaGridParametrizacao(); 
        }

        protected void OnRowCommand_GridViewParametrizacao(object sender, GridViewCommandEventArgs e) 
        {
            int codigo = int.Parse(GridView_Parametrizacao.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());
            ParametrizacaoVacina parametrizacao = Factory.GetInstance<IVacina>().BuscarPorCodigo<ParametrizacaoVacina>(codigo);
            Factory.GetInstance<IVacina>().Deletar(parametrizacao);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Parametrização excluída com sucesso!');", true);
            CarregaGridParametrizacao(); 
        }

        protected void OnRowDeleting_GridViewParametrizacao(object sender, GridViewDeleteEventArgs e) 
        {
            
        }
    }
}
