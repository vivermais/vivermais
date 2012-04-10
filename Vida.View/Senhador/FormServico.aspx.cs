using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.DAO;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Senhador;

namespace ViverMais.View.Senhador
{
    public partial class FormServico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LinkButton eas_pesquisarcnes = this.EAS.WUC_LinkButton_PesquisarCNES;
            LinkButton eas_pesquisarnome = this.EAS.WUC_LinkButton_PesquisarNomeFantasia;

            eas_pesquisarcnes.Click += new EventHandler(OnClick_PesquisarCNES);
            eas_pesquisarnome.Click += new EventHandler(OnClick_PesquisarNomeFantasiaUnidade);

            if (!IsPostBack)
            {
                ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
                if (!iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "MANTER_SERVICO", Modulo.SENHADOR))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    IList<ViverMais.Model.TipoServicoSenhador> tipos = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<ViverMais.Model.TipoServicoSenhador>().ToList();
                    ddlTipoServico.DataSource = tipos;
                    ddlTipoServico.DataBind();
                    ddlTipoServico.Items.Insert(0, new ListItem("Selecione...", "0"));

                    IList<ViverMais.Model.ServicoSenhador> servicos = Factory.GetInstance<IServicoSenhador>().ListarTodos<ViverMais.Model.ServicoSenhador>().ToList();
                    ddlServico.DataSource = servicos;
                    ddlServico.DataBind();
                    ddlServico.Items.Insert(0, new ListItem("Selecione...", "0"));

                    IList<ViverMais.Model.CategoriaOcupacao> categorias = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<ViverMais.Model.CategoriaOcupacao>().ToList();
                    ddlCategoria.DataSource = categorias;
                    ddlCategoria.DataBind();
                    ddlCategoria.Items.Insert(0, new ListItem("Selecione...", "0"));

                }
            }
        }
        protected void OnClick_PesquisarCNES(object sender, EventArgs e)
        {
            ViverMais.Model.EstabelecimentoSaude unidade = this.EAS.WUC_EstabelecimentosPesquisados.FirstOrDefault();

            if (unidade != null)
            {
                this.DropDownList_Unidade.Items.Clear();
                this.DropDownList_Unidade.Items.Add(new ListItem(unidade.NomeFantasia, unidade.CNES));
                this.DropDownList_Unidade.Items.Insert(0, new ListItem("SELECIONE...", "0"));
                this.DropDownList_Unidade.SelectedValue = unidade.CNES;
                this.DropDownList_Unidade.Focus();
                this.UpdatePanel_Unidade.Update();
            }
        }

        protected void OnClick_PesquisarNomeFantasiaUnidade(object sender, EventArgs e)
        {
            IList<ViverMais.Model.EstabelecimentoSaude> unidades = this.EAS.WUC_EstabelecimentosPesquisados;

            this.DropDownList_Unidade.DataSource = unidades;
            this.DropDownList_Unidade.DataBind();
            this.DropDownList_Unidade.Items.Insert(0, new ListItem("SELECIONE...", "0"));

            this.DropDownList_Unidade.Focus();
            this.UpdatePanel_Unidade.Update();

        }

        protected void btnAddEAS_Click(object sender, EventArgs e)
        {
            //Municipio municipio = Factory.GetInstance<IMunicipio>().BuscarPorCodigo<Municipio>(ddlMunicipios.SelectedValue);
            //GrupoAbrangencia grupo = Factory.GetInstance<IGrupoAbrangencia>().BuscarPorCodigo<GrupoAbrangencia>(ddlGrupoAbrangencia.SelectedValue);
            //bool existe = false;
            //foreach (Municipio mun in grupo.Municipios)
            //    if (mun.Codigo == municipio.Codigo)
            //        existe = true;
            //if (existe)//Se Existir, ele irá alertar
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Vínculo Já Existe! Por favor, verifique!.');", true);
            //    return;
            //}
            //else
            //{
            //    AdicionarMunicipioAoGrupo(grupo, municipio);
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Vínculo Inserido com Sucesso!');", true);
            //}
            //CarregaDadosDoGrupo(grupo);
        }
        protected void Incluir_Click(object sender, EventArgs e)
        { }


    }
}
