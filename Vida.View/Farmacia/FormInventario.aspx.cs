using System;
using ViverMais.DAO;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Globalization;
using System.Threading;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Inventario;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View
{
    public partial class FormInventario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBox_DataAbertura.Text = DateTime.Today.ToString("dd/MM/yyyy");
                Usuario usuario = (Usuario)Session["Usuario"];

                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "REALIZAR_INVENTARIO",Modulo.FARMACIA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    DropDownList_Farmacia.DataSource = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia, Usuario>(usuario, true, true);
                    DropDownList_Farmacia.DataBind();
                    //if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "REALIZAR_ABERTURA_ENCERRAMENTO_INVENTARIO"))
                    //{
                    //    IList<ViverMais.Model.Farmacia> lf = Factory.GetInstance<IFarmacia>().ListarTodos<ViverMais.Model.Farmacia>().OrderBy(p => p.Nome).ToList();

                    //    foreach (ViverMais.Model.Farmacia f in lf)
                    //        DropDownList_Farmacia.Items.Add(new ListItem(f.Nome, f.Codigo.ToString()));
                    //}
                    //else
                    //{
                    //    ViverMais.Model.Farmacia f = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia>(((Usuario)Session["Usuario"]).Codigo);

                    //    if (f != null)
                    //        DropDownList_Farmacia.Items.Add(new ListItem(f.Nome, f.Codigo.ToString()));
                    //}
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
            if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "REALIZAR_ABERTURA_ENCERRAMENTO_INVENTARIO",Modulo.FARMACIA))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para abrir o inventário! Por favor entre em contato com a administração.');", true);
                return;
            }

            IInventario iInventario = Factory.GetInstance<IInventario>();
            ViverMais.Model.Inventario inventario = iInventario.BuscarPorSituacao<ViverMais.Model.Inventario>(Inventario.ABERTO, int.Parse(DropDownList_Farmacia.SelectedValue)).FirstOrDefault();
            ViverMais.Model.Farmacia f = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.Farmacia>(int.Parse(DropDownList_Farmacia.SelectedValue));

            if (inventario != null)
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe um inventário em ABERTO para esta farmácia!');", true);
            else 
            {
                ViverMais.Model.Inventario novo_inventario     = new ViverMais.Model.Inventario();
                novo_inventario.DataInventario = DateTime.Parse(TextBox_DataAbertura.Text);
                novo_inventario.Farmacia       = f;
                novo_inventario.Situacao       = Inventario.ABERTO;

                iInventario.AbrirInventario<Inventario>(int.Parse(DropDownList_Farmacia.SelectedValue), novo_inventario);
                iInventario.Salvar(new LogFarmacia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, EventoFarmacia.ABERTURA_INVENTARIO, "id inventario: " + novo_inventario.Codigo));

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Inventário aberto com sucesso!');location='FormItensInventario.aspx?co_inventario=" + novo_inventario.Codigo + "';", true);
            }
        }

        /// <summary>
        /// Busca os inventários a partir de uma farmácia escolhida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_CarregaInventarios(object sender, EventArgs e) 
        {
            ViewState["farmacia_escolhida"] = DropDownList_Farmacia.SelectedValue;
            CarregaInventarios(int.Parse(DropDownList_Farmacia.SelectedValue));
//            TextBox_DataAbertura.ReadOnly = false;
//            TextBox_DataAbertura.ReadOnly = true;
        }

        /// <summary>
        /// Carrega os inventários no gridview
        /// </summary>
        /// <param name="co_farmacia">farmácia escolhida</param>
        private void CarregaInventarios(int co_farmacia)
        {
            IList<ViverMais.Model.Inventario> li = Factory.GetInstance<IInventario>().BuscarPorFarmacia<ViverMais.Model.Inventario>(co_farmacia);
            GridView_Inventario.DataSource = li;
            GridView_Inventario.DataBind();
            Panel_InventariosFarmacia.Visible = true;
        }

        /// <summary>
        /// Paginação dos inventários cadastrados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e) 
        {
            CarregaInventarios(int.Parse(ViewState["farmacia_escolhida"].ToString()));
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

                if (e.Row.Cells[1].Text == Inventario.ABERTO.ToString())
                    e.Row.Cells[1].Text = "ABERTO";
                else
                    e.Row.Cells[1].Text = "ENCERRADO";
            }
        }
    }
}
