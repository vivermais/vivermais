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
using ViverMais.Model;
using ViverMais.DAO;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Vacina
{
    public partial class FormVincularVacinaFabricante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "VINCULAR_VACINA_FABRICANTE", Modulo.VACINA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    CarregaCampos();
                }
            }
        }

        void CarregaCampos()
        {
            IList<ViverMais.Model.Vacina> listavacina = Factory.GetInstance<IVacina>().ListarTodos<ViverMais.Model.Vacina>().Where(p => p.Ativo == 'S').OrderBy(p => p.Nome).ToList();
            DropDown_Vacina.Items.Add(new ListItem("Selecione...", "0"));
            foreach (ViverMais.Model.Vacina v in listavacina)
            {
                DropDown_Vacina.Items.Add(new ListItem(v.Nome, v.Codigo.ToString()));
            }
            DropDown_Vacina.DataBind();
            DropDown_Vacina.SelectedValue = "0";

        }

        void CarregaFabricantes()
        {
            IList<ItemVacina> listafabricante = Factory.GetInstance<IItemVacina>().ListarPorVacina<ItemVacina>(int.Parse(DropDown_Vacina.SelectedValue));
            IList<FabricanteVacina> fabricantes = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<FabricanteVacina>(); //para carregar no dropdown
            if (listafabricante.Count > 0)
            {
                GridView_Fabricante.DataSource = listafabricante;
                GridView_Fabricante.DataBind();
                Panel_FabricantesVinculados.Visible = true;
                foreach (ItemVacina i in listafabricante)
                {
                    fabricantes.Remove(i.FabricanteVacina);
                }
                DropDown_Fabricante.Items.Clear();
                DropDown_Fabricante.Items.Add(new ListItem("Selecione...", "0"));
                foreach (FabricanteVacina f in fabricantes)
                {
                    DropDown_Fabricante.Items.Add(new ListItem(f.Nome, f.Codigo.ToString()));
                }
                DropDown_Fabricante.DataBind();
                DropDown_Fabricante.SelectedValue = "0";
                Panel_FabricantesVinculados.Visible = true;
            }
            else
            {
                DropDown_Fabricante.Items.Clear();
                DropDown_Fabricante.Items.Add(new ListItem("Selecione...", "0"));
                foreach (FabricanteVacina f in fabricantes)
                {
                    DropDown_Fabricante.Items.Add(new ListItem(f.Nome, f.Codigo.ToString()));
                }
                DropDown_Fabricante.DataBind();
                DropDown_Fabricante.SelectedValue = "0";
                Panel_FabricantesVinculados.Visible = false;
            }

        }

        protected void DropDown_Vacina_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDown_Vacina.SelectedValue != "0")
                CarregaFabricantes();
        }

        protected void OnClick_btnAdicionar(object sender, EventArgs e)
        {
            FabricanteVacina f = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<FabricanteVacina>(int.Parse(DropDown_Fabricante.SelectedValue));
            ViverMais.Model.Vacina v = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<ViverMais.Model.Vacina>(int.Parse(DropDown_Vacina.SelectedValue));
            ItemVacina i = new ItemVacina();
            i.FabricanteVacina = f;
            i.Vacina = v;
            i.Aplicacao = int.Parse(TextBox_Aplicacao.Text);
  
            //i.Codigo = Factory.GetInstance<IItemVacina>().BuscarProximoRegistro();
            Factory.GetInstance<IVacinaServiceFacade>().Inserir(i);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Item cadastrado com sucesso!');", true);
            Factory.GetInstance<IVacinaServiceFacade>().Inserir(new LogVacina(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 21, "id itemvacina: " + i.Codigo.ToString()));
            CarregaFabricantes();

        }

        protected void OnClick_btnCancelar(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void OnRowDeleting_GridView_Fabricante(object sender, GridViewDeleteEventArgs e)
        {
            //Verifica se o item existe
            int co_item = int.Parse(GridView_Fabricante.DataKeys[e.RowIndex].Value.ToString());
            if (!Factory.GetInstance<IItemVacina>().PermiteExclusao(co_item))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não é possível excluir este registro pois há lote ligado a este!');", true);
                return;
            }
            IList<ItemVacina> itens = (IList<ItemVacina>)Session["Fabricantes"];
            ItemVacina i = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<ItemVacina>(co_item);
            Factory.GetInstance<IVacinaServiceFacade>().Deletar(i);
            CarregaFabricantes();

        }

        protected void OnRowEditing_GridView_Fabricante(object sender, GridViewEditEventArgs e)
        {
            ViewState["CodigoItem"] = GridView_Fabricante.DataKeys[e.NewEditIndex][0].ToString();
            Panel_Atualizar.Visible = true;
        }

        protected void OnClick_SalvarAlteracao(object sender, EventArgs e)
        {
            int codigo = int.Parse(ViewState["CodigoItem"].ToString());
            ItemVacina item = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<ItemVacina>(codigo);
            item.Aplicacao = int.Parse(TextBox_AplicacaoAlterar.Text);
            Factory.GetInstance<IVacinaServiceFacade>().Atualizar(item);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Item atualizado com sucesso!');", true);
            Panel_Atualizar.Visible = false;
            TextBox_AplicacaoAlterar.Text = "";
            ViewState["CodigoItem"] = null;
            IList<ItemVacina> itens = Factory.GetInstance<IItemVacina>().ListarPorVacina<ItemVacina>(item.Vacina.Codigo);
            Session["Fabricantes"] = itens;
            GridView_Fabricante.DataSource = itens;
            GridView_Fabricante.DataBind();

        }

        protected void OnClick_CancelarAlteracao(object sender, EventArgs e)
        {
            Panel_Atualizar.Visible = false;
            TextBox_AplicacaoAlterar.Text = "";
            ViewState["CodigoItem"] = null;
        }

    }
}
