using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vida.DAO;
using Vida.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using Vida.Model;
using Vida.ServiceFacade.ServiceFacades.Farmacia.Misc;
using Vida.ServiceFacade.ServiceFacades;

namespace Vida.View.Farmacia
{
    public partial class FormAssociarSetorUnidade : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CarregaUnidades();
        }

        /// <summary>
        /// Carrega as unidades de saúde que sejam da rede municipal
        /// </summary>
        private void CarregaUnidades()
        {
            IList<Vida.Model.EstabelecimentoSaude> les = Factory.GetInstance<IEstabelecimentoSaude>().BuscarEstabelecimentoPorNaturezaOrganizacao<Vida.Model.EstabelecimentoSaude>("1");
            GridView_Unidade.DataSource = les;
            GridView_Unidade.DataBind();
        }

        /// <summary>
        /// Cancela a ação de associar o setor com a unidade de saúde escolhida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Cancelar(object sender, EventArgs e) 
        {
            Panel_SetoresUnidade.Visible = false;
            ViewState.Remove("co_unidade");
            ListBox_SetoresDisponiveis.Items.Clear();
            ListBox_SetoresDisponiveis.Items.Clear();
        }

        /// <summary>
        /// Verifica se associação dos setores e a unidade é válida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnServerValidate_ValidaAssociacao(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = ListBox_SetoresAlocados.Items.Count > 0 ? true : false;
        }

        /// <summary>
        /// Verifica se a ação de adicionar um setor é válida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnServerValidate_ValidaAdicaoSetor(object sender, ServerValidateEventArgs e) 
        {
            e.IsValid = false;
            foreach(ListItem i in ListBox_SetoresDisponiveis.Items)
            {
                if (i.Selected)
                {
                    e.IsValid = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Verifica se a ação de remover um setor é válida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnServerValidate_ValidaRemocaoSetor(object sender, ServerValidateEventArgs e) 
        {
            e.IsValid = false;
            foreach (ListItem i in ListBox_SetoresAlocados.Items)
            {
                if (i.Selected)
                {
                    e.IsValid = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Paginação do gridview de unidades
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            CarregaUnidades();
            GridView_Unidade.PageIndex = e.NewPageIndex;
            GridView_Unidade.DataBind();
        }

        /// <summary>
        /// Verifica se a ação escohida é para editar os setores diponíveis da unidade
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCommand_Acao(object sender, GridViewCommandEventArgs e) 
        {
            if (e.CommandName == "CommandName_Editar")
            {
                ListBox_SetoresDisponiveis.Items.Clear();
                ListBox_SetoresAlocados.Items.Clear();
                Panel_SetoresUnidade.Visible = true;

                string co_unidade       = GridView_Unidade.DataKeys[int.Parse(e.CommandArgument.ToString())]["Codigo"].ToString();
                ViewState["co_unidade"] = co_unidade;

                IList<Setor> lst  = Factory.GetInstance<ISetor>().ListarTodos<Setor>().OrderBy(p => p.Nome).ToList(); //Todos os setores
                IList<Setor> lsu  = Factory.GetInstance<ISetor>().BuscarPorEstabelecimento<Setor>(co_unidade); //Setores associados a esta unidade

                foreach (Setor s in lsu)
                    lst.Remove(s);

                foreach (Setor s in lst)
                    ListBox_SetoresDisponiveis.Items.Add(new ListItem(s.Nome, s.Codigo.ToString()));

                if (lsu != null && lsu.Count() > 0)
                {
                    Button_Salvar.Text = "Alterar";
                    Button_Salvar.OnClientClick = "javascript:return confirm('Tem certeza que deseja alterar o registro desta unidade ?');";
                }

                foreach (Setor s in lsu)
                    ListBox_SetoresAlocados.Items.Add(new ListItem(s.Nome, s.Codigo.ToString()));
            }
        }

        /// <summary>
        /// Salva a associação dos setores com a unidade de saúde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Salvar(object sender, EventArgs e) 
        {
            if (CustomValidator_Associar.IsValid)
            {
                try
                {
                    IList<SetorUnidade> las = Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<SetorUnidade>().Where(p => p.CodigoUnidade == ViewState["co_unidade"].ToString()).ToList();

                    IList<SetorUnidade> lns = new List<SetorUnidade>();
                    foreach(ListItem i in ListBox_SetoresAlocados.Items)
                        lns.Add(new SetorUnidade(ViewState["co_unidade"].ToString(),Factory.GetInstance<ISetor>().BuscarPorCodigo<Setor>(int.Parse(i.Value))));

                    Factory.GetInstance<ISetor>().AssociarSetorUnidade<IList<SetorUnidade>, IList<SetorUnidade>>(las, lns);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Setores associados com sucesso!');location='Default.aspx';", true);
                }
                catch (Exception f)
                {
                    throw f;
                }
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A unidade de saúde deve conter pelo menos um setor associado.');", true);
        }

        /// <summary>
        /// Adiciona o setor a unidade selecionada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_AdicionaSetor(object sender, ImageClickEventArgs e)
        {
            if (CustomValidator_Adicionar.IsValid)
            {
                ListItemCollection ltemp = new ListItemCollection();
                
                foreach (ListItem i in ListBox_SetoresDisponiveis.Items)
                {
                    if (i.Selected)
                        ltemp.Add(i);
                }

                foreach (ListItem i in ltemp)
                {
                    ListBox_SetoresDisponiveis.Items.Remove(i);
                    ListBox_SetoresAlocados.Items.Add(i);
                }

                ListBox_SetoresDisponiveis.DataBind();
                ListBox_SetoresAlocados.DataBind();
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Para adicionar um ou mais setores à unidade deve-se selecionar os registros na lista de setores disponíves.');", true);
        }

        /// <summary>
        /// Retira o setor da unidade selecionada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_RetiraSetor(object sender, ImageClickEventArgs e) 
        {
            if (CustomValidator_Retirar.IsValid)
            {
                ListItemCollection ltemp = new ListItemCollection();

                foreach (ListItem i in ListBox_SetoresAlocados.Items) 
                {
                    if (i.Selected)
                        ltemp.Add(i);
                }

                foreach (ListItem i in ltemp) 
                {
                    ListBox_SetoresAlocados.Items.Remove(i);
                    ListBox_SetoresDisponiveis.Items.Add(i);
                }

                ListBox_SetoresAlocados.DataBind();
                ListBox_SetoresDisponiveis.DataBind();
            }else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Para remover um ou mais setores da unidade deve-se selecionar os registros na lista de setores incluídos.');", true);
        }
    }
}
