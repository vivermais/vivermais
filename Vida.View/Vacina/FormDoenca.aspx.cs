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
    public partial class FormDoenca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_DOENCA_VACINA", Modulo.VACINA))
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    if (Request.QueryString["co_doenca"] != null)
                    {
                        int codigo = int.Parse(Request.QueryString["co_doenca"]);
                        Doenca doenca = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<Doenca>(codigo);
                        tbxNome.Text = doenca.Nome;
                    }
                }
            }
        }

        /// <summary>
        /// Salva a doença
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Doenca doenca = Request.QueryString["co_doenca"] != null ? Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<Doenca>(int.Parse(Request.QueryString["co_doenca"])) : new Doenca();

            if (Factory.GetInstance<IDoencaVacina>().ValidarCadastroDoenca(doenca.Codigo, tbxNome.Text.Trim()))
            {
                doenca.Nome = tbxNome.Text.Trim().ToUpper();

                //if (doenca.Codigo != 0)
                //{
                Factory.GetInstance<IViverMaisServiceFacade>().Salvar(doenca);
                Factory.GetInstance<IVacinaServiceFacade>().Inserir(new LogVacina(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 3, "id doenca: " + doenca.Codigo));
                //}
                //else
                //{
                //    Factory.GetInstance<IViverMaisServiceFacade>().Inserir(doenca);
                //    Factory.GetInstance<IVacinaServiceFacade>().Inserir(new LogVacina(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 3, "id doenca: " + Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<Doenca>().Max(p=>p.Codigo).ToString()));
                //}

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados salvos com sucesso.');window.location='FormExibeDoenca.aspx';", true);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe uma doença cadastrada com o mesmo nome! Por favor, informe outro nome.');", true);
        }
    }
}
