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
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;

namespace ViverMais.View.Urgencia
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                CarregaTiposConsultorios();
            }
        }

        /// <summary>
        /// Carrega os tipos de consultórios existentes
        /// </summary>
        private void CarregaTiposConsultorios()
        {
            IList<ViverMais.Model.TipoConsultorio> ltc = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ViverMais.Model.TipoConsultorio>().OrderBy(tc => tc.Cor).ToList();
            GridView_TipoConsultorio.DataSource = ltc;
            GridView_TipoConsultorio.DataBind();
        }

        /// <summary>
        /// Salva o tipo do consultório descrito
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_SalvarTipoConsultorio(object sender, EventArgs e) 
        {
            try 
            {
                bool ok = true;
                ViverMais.Model.TipoConsultorio tc = ButtonSalvarTipo.CommandArgument == "salvar" ? new ViverMais.Model.TipoConsultorio() : Factory.GetInstance<ITipoConsultorio>().BuscarPorCodigo<ViverMais.Model.TipoConsultorio>(int.Parse(ViewState["indexOfTipoConsultorio"].ToString()));

                tc.Cor       = TextBox_Cor.Text;
                tc.Descricao = TextBox_Descricao.Text;

                ViverMais.Model.TipoConsultorio tctemp = Factory.GetInstance<ITipoConsultorio>().BuscarPorCor<ViverMais.Model.TipoConsultorio>(tc.Cor);

                if (tctemp != null && tctemp.Codigo != tc.Codigo)
                    ok = false;

                if (ok)
                {
                    Factory.GetInstance<IUrgenciaServiceFacade>().Salvar(tc);
                    ButtonSalvarTipo.CommandArgument = "salvar";
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Tipo de Consultório salvo com sucesso!');", true);
                    CarregaTiposConsultorios();
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe um tipo de consultório com a cor informada! Informe outra cor.');", true);
            }catch(Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Verifica se está sendo feito a edição do tipo de consultório
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCommand_Acao(object sender, GridViewCommandEventArgs e) 
        {
            if (e.CommandName == "CommandName_Editar") 
            {
                int index = int.Parse(GridView_TipoConsultorio.DataKeys[int.Parse(e.CommandArgument.ToString())]["Codigo"].ToString());
                ViverMais.Model.TipoConsultorio tc = Factory.GetInstance<ITipoConsultorio>().BuscarPorCodigo<ViverMais.Model.TipoConsultorio>(index);

                if (tc != null)
                {
                    TextBox_Cor.Text = tc.Cor;
                    TextBox_Descricao.Text = tc.Descricao;
                    ViewState["indexOfTipoConsultorio"] = index.ToString();
                    ButtonSalvarTipo.CommandArgument = "editar";
                }
            }
        }

        /// <summary>
        /// Cancela a ação seja de editar ou cadastrar um novo tipo de consultório
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Cancelar(object sender, EventArgs e) 
        {
            ButtonSalvarTipo.CommandArgument = "salvar";
            ViewState.Remove("indexOfTipoConsultorio");
            TextBox_Cor.Text                 = "";
            TextBox_Descricao.Text           = "";
        }
    }
}
