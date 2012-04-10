using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using AjaxControlToolkit;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;

namespace ViverMais.View.Urgencia
{
    public partial class FormCameraIP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_CAMERA_IP",Modulo.URGENCIA))
                {
                    //var consulta = from es in Factory.GetInstance<IEstabelecimentoSaude>().ListarTodos<ViverMais.Model.EstabelecimentoSaude>()
                    //               where Factory.GetInstance<IRelatorioUrgencia>().RetornaPASAtivos<PASAtivos>().Select(p2=>p2.Codigo).Contains(es.CNES)
                    //               orderby es.NomeFantasia
                    //               select es;

                    DropDownList_Unidade.DataSource = Factory.GetInstance<IRelatorioUrgencia>().EstabelecimentosAtivos<ViverMais.Model.EstabelecimentoSaude>();
                    DropDownList_Unidade.DataBind();
                    DropDownList_Unidade.Items.Insert(0, new ListItem("Selecione...", "-1"));

                    if (Request["co_camera"] != null)
                    {
                        ViewState["cameraeditar"] = Request["co_camera"].ToString();
                        CameraIP camera = Factory.GetInstance<ICameraIP>().BuscarPorCodigo<CameraIP>(int.Parse(ViewState["cameraeditar"].ToString()));
                        TextBox_Endereco.Text = camera.Endereco;
                        TextBox_Local.Text = camera.Local;
                        DropDownList_Unidade.SelectedValue = camera.CNESUnidade;
                    }
                    //CarregaCamerasUnidades();
                }
                else
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        /// <summary>
        /// Salva a câmera ip com os dados informados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Salvar(object sender, EventArgs e)
        {
            try
            {
                CameraIP camera = ViewState["cameraeditar"] != null ? Factory.GetInstance<ICameraIP>().BuscarPorCodigo<CameraIP>(int.Parse(ViewState["cameraeditar"].ToString())) : new CameraIP();
                ICameraIP iCamera = Factory.GetInstance<ICameraIP>();

                if (iCamera.ValidarCadastro(TextBox_Endereco.Text, camera.Codigo))
                {
                    camera.Local = TextBox_Local.Text;
                    camera.Endereco = TextBox_Endereco.Text;
                    camera.CNESUnidade = DropDownList_Unidade.SelectedValue;
                    iCamera.Salvar(camera);

                    if (ViewState["cameraeditar"] == null)
                        iCamera.Inserir(new LogUrgencia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 43, "ID CAMERA:" + camera.Codigo));
                    else
                        iCamera.Inserir(new LogUrgencia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 44, "ID CAMERA:" + camera.Codigo));

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Câmera salva com sucesso.');location='FormExibeCameraIP.aspx';", true);
                    //OnClick_Cancelar(new object(), new EventArgs());
                }else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe uma câmera cadastrada com este endereço. Por favor, informe outro endereço.');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///// <summary>
        ///// Cancela a ação de cadastrado da câmera
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void OnClick_Cancelar(object sender, EventArgs e)
        //{
        //    ViewState.Remove("cameraeditar");
        //    DropDownList_Unidade.SelectedValue = "-1";
        //    TextBox_Endereco.Text = "";
        //    TextBox_Local.Text = "";
        //    CarregaCamerasUnidades();
        //}

        ///// <summary>
        ///// Edita os dados da câmera
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void OnClick_EditarCamera(object sender, EventArgs e)
        //{
        //    ViewState["cameraeditar"] = ((LinkButton)sender).CommandArgument.ToString();
        //    CameraIP camera = Factory.GetInstance<ICameraIP>().BuscarPorCodigo<CameraIP>(int.Parse(ViewState["cameraeditar"].ToString()));
        //    TextBox_Endereco.Text = camera.Endereco;
        //    TextBox_Local.Text = camera.Local;
        //    DropDownList_Unidade.SelectedValue = camera.CNESUnidade;
        //}
    }
}
