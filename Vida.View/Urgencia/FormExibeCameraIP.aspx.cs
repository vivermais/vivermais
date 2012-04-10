using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.DAO;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using AjaxControlToolkit;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc;

namespace ViverMais.View.Urgencia
{
    public partial class FormExibeCameraIP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_CAMERA_IP", Modulo.URGENCIA))
                {
                    this.CarregaCamerasUnidades();
                }
                else
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        /// <summary>
        /// Carrega as câmeras dos PA's ativos
        /// </summary>
        private void CarregaCamerasUnidades()
        {
            //IList<PASAtivos> pas = Factory.GetInstance<IRelatorioUrgencia>().RetornaPASAtivos<PASAtivos>();

            //if (pas.Count() > 0)
            //{
            GridView_Unidades.DataSource = Factory.GetInstance<IRelatorioUrgencia>().EstabelecimentosAtivos<ViverMais.Model.EstabelecimentoSaude>();
            //Factory.GetInstance<IEstabelecimentoSaude>().ListarTodos<ViverMais.Model.EstabelecimentoSaude>().Where(p => pas.Select(p2 => p2.Codigo).Contains(p.CNES)).OrderBy(p => p.NomeFantasia).ToList();
            GridView_Unidades.DataBind();
            //}
        }

        /// <summary>
        /// Configura o grid view de unidades com o padrão especificado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_ConfigurarGridViewUnidade(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Accordion ac = (Accordion)e.Row.FindControl("Accordion_Cameras");
                GridView gv = (GridView)ac.Panes[0].FindControl("GridView_Cameras");

                gv.DataSource = Factory.GetInstance<ICameraIP>().BuscarPorUnidade<CameraIP>(GridView_Unidades.DataKeys[e.Row.RowIndex]["CNES"].ToString());
                gv.DataBind();
            }
        }

        /// <summary>
        /// Exclui a câmera escolhida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_ExcluirCamera(object sender, EventArgs e)
        {
            ICameraIP iCamera = Factory.GetInstance<ICameraIP>();
            CameraIP camera = iCamera.BuscarPorCodigo<CameraIP>(int.Parse(((LinkButton)sender).CommandArgument.ToString()));

            try
            {
                iCamera.Deletar(camera);
                iCamera.Inserir(new LogUrgencia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 45, "ID CAMERA:" + camera.Codigo));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Câmera excluída com sucesso.');", true);
                this.CarregaCamerasUnidades();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
