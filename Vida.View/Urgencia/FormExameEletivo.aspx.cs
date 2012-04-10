using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;

namespace ViverMais.View.Urgencia
{
    public partial class FormExameEletivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!Factory.GetInstance<IRelatorioUrgencia>().PAAtivo(((Usuario)Session["Usuario"]).Unidade.CNES))
                //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, o Módulo Urgência ainda não está disponível para sua unidade! Por favor, procure a administração.');location='../Home.aspx';", true);

                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CADASTRAR_EXAME", Modulo.URGENCIA))
                {
                    if (Request["co_exame"] != null)
                    {
                        ViewState["co_exame"] = Request["co_exame"].ToString();
                        ExameEletivo exame = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ExameEletivo>(int.Parse(ViewState["co_exame"].ToString()));

                        tbxNome.Text = exame.Descricao;

                        if (exame.Status == Convert.ToChar(ExameEletivo.DescricaoStatus.Inativo))
                        {
                            RadioButton_Inativo.Checked = true;
                            RadioButton_Ativo.Checked = false;
                        }
                        else
                        {
                            RadioButton_Inativo.Checked = false;
                            RadioButton_Ativo.Checked = true;
                        }
                    }

                    //AtualizaExames();
                    //ViverMais.Model.UsuarioProfissionalUrgence up = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigoViverMais<ViverMais.Model.UsuarioProfissionalUrgence>(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo);
                    //if (up == null)
                    //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Profissional não identificado! Por favor, identifique o usuário.');location='Default.aspx';", true);
                    //else
                }
                else
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        ///// <summary>
        ///// Atualiza a lista de exames
        ///// </summary>
        //void AtualizaExames()
        //{
        //    IList<ExameEletivo> exames = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ExameEletivo>().OrderBy(p=>p.Descricao).ToList();
        //    gridExames.DataSource = exames;
        //    gridExames.DataBind();
        //}

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            ExameEletivo exame;
            IUrgenciaServiceFacade iUrgencia = Factory.GetInstance<IUrgenciaServiceFacade>();

            if (ViewState["co_exame"] != null)
                exame = iUrgencia.BuscarPorCodigo<ExameEletivo>(int.Parse(ViewState["co_exame"].ToString()));
            else
                exame = new ExameEletivo();

            exame.Descricao = tbxNome.Text;
            exame.Status = RadioButton_Ativo.Checked ? Convert.ToChar(ExameEletivo.DescricaoStatus.Ativo) : Convert.ToChar(ExameEletivo.DescricaoStatus.Inativo);
            iUrgencia.Salvar(exame);
            iUrgencia.Inserir(new LogUrgencia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 17, "id exame:" + exame.Codigo));
            //OnClick_Cancelar(sender, e);

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Exame salvo com sucesso!');location='FormExibeExameEletivo.aspx';", true);
            //AtualizaExames();
        }

        ///// <summary>
        ///// Seleciona as informações necessárias do exame para edição
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void gridExames_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ViewState["co_exame"] = int.Parse(gridExames.DataKeys[gridExames.SelectedIndex]["Codigo"].ToString());
        //    ExameEletivo exame = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ExameEletivo>(int.Parse(ViewState["co_exame"].ToString()));

        //    tbxNome.Text = exame.Descricao;

        //    if (exame.Status == Convert.ToChar(ExameEletivo.DescricaoStatus.Inativo))
        //    {
        //        RadioButton_Inativo.Checked = true;
        //        RadioButton_Ativo.Checked = false;
        //    }
        //    else
        //    {
        //        RadioButton_Inativo.Checked = false;
        //        RadioButton_Ativo.Checked = true;
        //    }
        //}

        ///// <summary>
        ///// Cancela o cadastro/edição do exame
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void OnClick_Cancelar(object sender, EventArgs e)
        //{
        //    ViewState.Remove("co_exame");
        //    tbxNome.Text = "";
        //    RadioButton_Ativo.Checked = true;
        //    RadioButton_Inativo.Checked = false;
        //}

        ///// <summary>
        ///// Paginação do gridview
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        //{
        //    AtualizaExames();
        //    gridExames.PageIndex = e.NewPageIndex;
        //    gridExames.DataBind();
        //}
    }
}
