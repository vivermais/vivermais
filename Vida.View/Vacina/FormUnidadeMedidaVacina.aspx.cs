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
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Vacina
{
    public partial class FormUnidadeMedidaVacina : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_UNIDADE_MEDIDA_VACINA",Modulo.VACINA))
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    int temp;

                    if (Request["co_unidademedida"] != null && int.TryParse(Request["co_unidademedida"].ToString(), out temp))
                    {
                        try
                        {
                            ViewState["co_unidademedida"] = Request["co_unidademedida"].ToString();
                            UnidadeMedidaVacina unidademedida = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<UnidadeMedidaVacina>(int.Parse(Request["co_unidademedida"].ToString()));
                            TextBox_Nome.Text = unidademedida.Nome;
                            TextBox_Sigla.Text = unidademedida.Sigla;
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
        /// Salva a unidade de medida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Salvar(object sender, EventArgs e)
        {
            try
            {
                UnidadeMedidaVacina unidademedida = ViewState["co_unidademedida"] != null ? Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<UnidadeMedidaVacina>(int.Parse(ViewState["co_unidademedida"].ToString())) : new UnidadeMedidaVacina();
                unidademedida.Nome = TextBox_Nome.Text.Trim().ToUpper();
                unidademedida.Sigla = TextBox_Sigla.Text.Trim().ToUpper();

                if (Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<UnidadeMedidaVacina>().Where(p => p.Codigo != unidademedida.Codigo && p.SiglaSemCaracterEspecial().Trim() == unidademedida.SiglaSemCaracterEspecial().Trim()).FirstOrDefault() == null)
                {
                    Factory.GetInstance<IVacinaServiceFacade>().Salvar(unidademedida);
                    Factory.GetInstance<IVacinaServiceFacade>().Inserir(new LogVacina(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 6, "id unidade medida: " + unidademedida.Codigo));

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Unidade de Medida salvo com sucesso!');location='FormExibeUnidadeMedidaVacina.aspx';", true);
                }else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe uma unidade de medida com a sigla informada. Por favor, digite outra sigla!');", true);
            }
            catch (Exception f)
            {
                throw f;
            }
        }
    }
}
