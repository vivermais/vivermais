using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;

namespace ViverMais.View.Urgencia
{
    public partial class FormProntuariosObservacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CarregaProntuarios();
        }

        /// <summary>
        /// Carrega os prontuarios da unidade do usuário logado e que estejam em observação
        /// </summary>
        private void CarregaProntuarios()
        {
            GridView_Prontuarios.DataSource = Factory.GetInstance<IProntuario>().getDataTablePronturario<IList<ViverMais.Model.Prontuario>>(Factory.GetInstance<IProntuario>().ListarTodos<ViverMais.Model.Prontuario>().Where(p => p.CodigoUnidade == ((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.Codigo && p.Situacao.Codigo == 2).OrderBy(p=>p.ClassificacaoRisco.Codigo).ToList());
            GridView_Prontuarios.DataBind();
        }

        /// <summary>
        /// Formata o gridview de acordo com a classificação de risco do paciente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGrid(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Image img = (Image)e.Row.FindControl("Image_Classificacao");
                img.ImageUrl = "~/Urgencia/img/" + Factory.GetInstance<IClassificacaoRisco>().BuscarPorCodigo<ViverMais.Model.ClassificacaoRisco>(int.Parse(img.ImageUrl)).Imagem;
            }
        }

        /// <summary>
        /// Paginação do GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanging_Paginacao(object sender, GridViewPageEventArgs e) 
        {
            CarregaProntuarios();
            GridView_Prontuarios.PageIndex = e.NewPageIndex;
            GridView_Prontuarios.DataBind();
        }
    }
}
