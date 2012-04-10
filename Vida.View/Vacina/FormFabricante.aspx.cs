﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;

namespace ViverMais.View.Vacina
{
    public partial class FormFabricante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_FABRICANTE"))
                //{
                //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                //    return;
                //}

                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_FABRICANTE_VACINA",Modulo.VACINA))
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {

                    int temp;

                    if (Request["co_fabricante"] != null && int.TryParse(Request["co_fabricante"].ToString(), out temp))
                    {
                        try
                        {
                            FabricanteVacina fabricante = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<FabricanteVacina>(int.Parse(Request["co_fabricante"].ToString()));
                            TextBox_Nome.Text = fabricante.Nome;

                            if (fabricante.Situacao == FabricanteVacina.INATIVO)
                            {
                                RadioButton_Ativo.Checked = false;
                                RadioButton_Inativo.Checked = true;
                            }

                            TextBox_CNPJ.Text = fabricante.CNPJ;
                        }
                        catch (Exception f)
                        {
                            throw f;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Salva o fabricante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Salvar(object sender, EventArgs e)
        {
            try
            {
                FabricanteVacina fabricante = Request["co_fabricante"] != null ? Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<FabricanteVacina>(int.Parse(Request["co_fabricante"].ToString())) : new FabricanteVacina();
                fabricante.Nome = TextBox_Nome.Text.Trim().ToUpper();
                fabricante.Situacao = RadioButton_Ativo.Checked ? FabricanteVacina.ATIVO : FabricanteVacina.INATIVO;
                fabricante.CNPJ = TextBox_CNPJ.Text;

                if (fabricante.CNPJValido())
                {
                    bool cadastrar = Factory.GetInstance<IFabricanteVacina>().ValidarCadastro<FabricanteVacina>(fabricante.Codigo, TextBox_CNPJ.Text);

                    if (cadastrar)
                    {
                        Factory.GetInstance<IFabricanteVacina>().Salvar(fabricante);
                        Factory.GetInstance<IVacinaServiceFacade>().Inserir(new LogVacina(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 15, "id fabricante: " + fabricante.Codigo));

                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Fabricante salvo com sucesso!');location='FormExibeFabricante.aspx';", true);
                    }
                    else
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe um fabricante cadastrado com o CNPJ informado! Por favor, digite outro CNPJ.');", true);
                }else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('CNPJ inválido! Por favor, informe outro CNPJ.');", true);
            }
            catch (Exception f)
            {
                throw f;
            }
        }
    }
}
