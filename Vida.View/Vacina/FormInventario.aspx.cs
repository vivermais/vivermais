using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Movimentacao;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.View.Vacina
{
    public partial class FormInventario : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ISeguranca iSeguranca = Factory.GetInstance<ISeguranca>();
                if (!iSeguranca.VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "REALIZAR_INVENTARIO_VACINA", Modulo.VACINA) && !iSeguranca.VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "REALIZAR_ABERTURA_ENCERRAMENTO_INVENTARIO_VACINA", Modulo.VACINA))
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    Usuario usuario = (Usuario)Session["Usuario"];
                    //LabelUnidadeSaude.Text = usuario.Unidade.NomeFantasia;

                    //IList<SalaVacina> salasvacina = Factory.GetInstance<ISalaVacina>().BuscarPorUnidadeSaude<SalaVacina>(usuario.Unidade.CNES);
                    //DropDownList_SalaVacina.DataSource = salasvacina;
                    //DropDownList_SalaVacina.DataBind();

                    DropDownList_SalaVacina.DataSource = Factory.GetInstance<ISalaVacina>().BuscarPorUsuario<SalaVacina, Usuario>(usuario, true, true);
                    DropDownList_SalaVacina.DataBind();

                    TextBox_DataAbertura.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
        }

        /// <summary>
        /// Salva o inventário com o status aberto.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Salvar(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["Usuario"];

            if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "REALIZAR_ABERTURA_ENCERRAMENTO_INVENTARIO_VACINA",Modulo.VACINA))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para abrir o inventário! Por favor entre em contato com a administração.');", true);
                return;
            }

            InventarioVacina inventarioaberto = Factory.GetInstance<IInventarioVacina>().BuscarPorSituacao<InventarioVacina>(Convert.ToChar(InventarioVacina.DescricaoSituacao.Aberto), int.Parse(DropDownList_SalaVacina.SelectedValue)).FirstOrDefault();

            if (inventarioaberto != null)
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe um inventário em ABERTO para esta sala de vacina!');", true);
            else
            {
                try
                {
                    int co_inventario = Factory.GetInstance<IInventarioVacina>().AbrirInventario(DateTime.Parse(TextBox_DataAbertura.Text), int.Parse(DropDownList_SalaVacina.SelectedValue));
                    
                    Factory.GetInstance<IVacinaServiceFacade>().Inserir(new LogVacina(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 11, "id inventario: " + co_inventario));
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Inventário aberto com sucesso!');location='FormItensInventario.aspx?co_inventario=" + co_inventario + "';", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Busca os inventários a partir de uma farmácia escolhida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_CarregaInventarios(object sender, EventArgs e)
        {
            ViewState["sala_escolhida"] = DropDownList_SalaVacina.SelectedValue;
            CarregaInventarios(int.Parse(DropDownList_SalaVacina.SelectedValue));
        }

        /// <summary>
        /// Carrega os inventários no gridview
        /// </summary>
        /// <param name="co_salavacina">código da sala de vacina</param>
        private void CarregaInventarios(int co_salavacina)
        {
            SalaVacina sala = Factory.GetInstance<ISalaVacina>().BuscarPorCodigo<SalaVacina>(co_salavacina);
            IList<InventarioVacina> inventarios = Factory.GetInstance<IInventarioVacina>().BuscarPorSalaVacina<InventarioVacina>(co_salavacina);
            GridView_Inventario.DataSource = inventarios;
            GridView_Inventario.DataBind();
            
            Label_Unidade.Text = sala.EstabelecimentoSaude.NomeFantasia;
            Label_SalaVacina.Text = sala.Nome;

            Panel_InventariosSalaVacina.Visible = true;
        }

        /// <summary>
        /// Paginação dos inventários cadastrados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            CarregaInventarios(int.Parse(ViewState["sala_escolhida"].ToString()));
            GridView_Inventario.PageIndex = e.NewPageIndex;
            GridView_Inventario.DataBind();
        }

        /// <summary>
        /// Formata o gridview para o padrão adotado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormatarGridView(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = (LinkButton)e.Row.FindControl("LinkButton1");
                lb.Text = DateTime.Parse(lb.Text).ToString("dd/MM/yyyy");
                lb.PostBackUrl = "FormDadosInventario.aspx?co_inventario=" + lb.CommandArgument.ToString();

                if (Convert.ToChar(e.Row.Cells[1].Text) == Convert.ToChar(InventarioVacina.DescricaoSituacao.Aberto))
                    e.Row.Cells[1].Text = "ABERTO";
                else
                    e.Row.Cells[1].Text = "ENCERRADO";
            }
        }
    }
}
