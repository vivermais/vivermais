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
using System.Collections.Generic;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;

namespace ViverMais.View.Farmacia
{
    public partial class FormExibeSubElencoMedicamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_SUB_ELENCO_MEDICAMENTO", Modulo.FARMACIA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                    OnClick_ListarTodos(new object(), new EventArgs());
            }
        }

        /// <summary>
        /// Carrega os sub-elencos de acordo com o tipo de pesquisa
        /// </summary>
        /// <param name="nome"></param>
        private void CarregaSubElenco(string nome)
        {
            GridView_SubElenco.DataSource = Factory.GetInstance<ISubElencoMedicamento>().BuscarPorDescricao<SubElencoMedicamento>(nome);
            GridView_SubElenco.DataBind();
        }

        /// <summary>
        /// Pesquisa os sub-elencos de acordo com o nome informado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Pesquisa(object sender, EventArgs e)
        {
            ViewState["nomesubelenco"] = TextBox_NomeSubElenco.Text;
            this.CarregaSubElenco(ViewState["nomesubelenco"].ToString());
        }

        /// <summary>
        /// Lista todos sub-elencos cadastrados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_ListarTodos(object sender, EventArgs e)
        {
            ViewState["nomesubelenco"] = string.Empty;
            this.CarregaSubElenco(ViewState["nomesubelenco"].ToString());
        }


        /// <summary>
        /// Paginação para o gridview de sub-elencos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_SubElenco(object sender, GridViewPageEventArgs e)
        {
            this.CarregaSubElenco(ViewState["nomesubelenco"].ToString());
            GridView_SubElenco.PageIndex = e.NewPageIndex;
            GridView_SubElenco.DataBind();
        }

        /// <summary>
        /// Exclui o sub-elenco do módulo farmácia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDeleting_SubElenco(object sender, GridViewDeleteEventArgs e)
        {
            int co_subelenco;
            if (int.TryParse(GridView_SubElenco.DataKeys[e.RowIndex]["Codigo"].ToString(), out co_subelenco))
            {
                string retorno = Factory.GetInstance<ISubElencoMedicamento>().ExcluirSubElencoMedicamento(co_subelenco);

                if (retorno.Equals("ok"))
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Sub-Elenco excluído com sucesso.');", true);
                    this.CarregaSubElenco(ViewState["nomesubelenco"].ToString());
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + retorno + "');", true);
            }
        }
    }
}
