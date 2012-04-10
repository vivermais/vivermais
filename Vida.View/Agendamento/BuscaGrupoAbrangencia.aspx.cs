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
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Agendamento
{
    public partial class BuscaGrupoAbrangencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "BUSCAR_GRUPO_AGRANGENCIA", Modulo.AGENDAMENTO))
                {
                    CarregaDropGruposAbrangencia();
                    CarregaDropMunicipios();
                    //CarregaArvoreDeGrupos();
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        protected void CarregaDropGruposAbrangencia()
        {
            IList<GrupoAbrangencia> gruposAbrangencia = Factory.GetInstance<IGrupoAbrangencia>().ListarGruposAtivos<GrupoAbrangencia>();
            ddlGrupoAbrangencia.Items.Add(new ListItem("Selecione...", "0"));
            foreach (GrupoAbrangencia grupoAbrangencia in gruposAbrangencia)
            {
                ddlGrupoAbrangencia.Items.Add(new ListItem(grupoAbrangencia.NomeGrupo, grupoAbrangencia.Codigo));
            }

            ddlGrupoAbrangencia.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
        }

        protected void CarregaDropMunicipios()
        {
            IList<Municipio> municipios = Factory.GetInstance<IMunicipio>().ListarPorEstado<Municipio>("BA");
            ddlMunicipios.Items.Add(new ListItem("Selecione...", "0"));
            foreach (Municipio municipio in municipios)
            {
                ddlMunicipios.Items.Add(new ListItem(municipio.Nome, municipio.Codigo));
            }
        }

        protected void ddlGrupoAbrangencia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GrupoAbrangencia grupo = Factory.GetInstance<IGrupoAbrangencia>().BuscarPorCodigo<GrupoAbrangencia>(ddlGrupoAbrangencia.SelectedValue);
            if (grupo != null)
            {
                CarregaDadosDoGrupo(grupo);
            }

        }

        protected void btnAddTodosMunicipios_Click(object sender, EventArgs e)
        {
            GrupoAbrangencia grupo = Factory.GetInstance<IGrupoAbrangencia>().BuscarPorCodigo<GrupoAbrangencia>(ddlGrupoAbrangencia.SelectedValue);
            if (grupo != null)
            {
                bool existe = false;
                foreach (ListItem item in ddlMunicipios.Items)
                {
                    if (item.Value != "0")
                    {
                        foreach (Municipio mun in grupo.Municipios)
                        {
                            if (mun.Codigo == item.Value)
                                existe = true;
                        }
                        if (!existe)
                            AdicionarMunicipioAoGrupo(grupo, Factory.GetInstance<IMunicipio>().BuscarPorCodigo<Municipio>(item.Value));
                    }
                    existe = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Selecione um Grupo de Abrangência!');", true);
                return;
            }
            CarregaDadosDoGrupo(grupo);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Registros Incluídos com Sucesso!');", true);
            return;
        }

        /// <summary>
        /// Método Responsável por trazer os Municípios Pertencentes ao Grupo
        /// </summary>
        protected void CarregaDadosDoGrupo(GrupoAbrangencia grupo)
        {
            TreeViewVinculoGrupoMunicipio.Nodes.Clear();
            TreeNode root = null;
            //IList<GrupoAbrangencia grupoAbrangencia;
            //if (Session["ListaGrupos"] != null)
            //gruposAbrangencia = Factory.GetInstance<IGrupoAbrangencia>().
            //    gruposAbrangencia = Factory.GetInstance<IGrupoAbrangencia>().ListarGruposAtivos<GrupoAbrangencia>();

            //foreach (GrupoAbrangencia grupoAbrangencia in gruposAbrangencia)
            //{
            root = new TreeNode("<span style='font-size: Small; font: Tahoma; color: #FFFFFF; background-color: #5d8bb0; line-height: 28px; float: left; height: 26px; width: 600px; font-weight: Bold;'>&nbsp; " + grupo.NomeGrupo + "</span>");
            //IList<Municipio> municipios = Factory.GetInstance<IGrupoAbrangencia>().ListarMunicipiosPorGrupoAbrangencia<Municipio>(grupoAbrangencia.Codigo);
            foreach (Municipio municipio in grupo.Municipios)
            {
                TreeNode n1 = new TreeNode("<span style='font-size: X-Small; font: Verdana; font-color: #778899; font-weight: Bold; width: 400px;'><li>"
                                            + municipio.Nome + "&nbsp;&nbsp;&nbsp;&nbsp;<img src='img/bt_del.png' border='0' alt='Excluir'></li></span>");
                n1.Value = grupo.Codigo + "/" + municipio.Codigo;
                root.ChildNodes.Add(n1);
            }
            TreeViewVinculoGrupoMunicipio.Nodes.Add(root);
            root.NavigateUrl = "#";
            root.Expanded = false;
            //root.SelectAction = TreeNodeSelectAction.Expand;
            //}
            TreeViewVinculoGrupoMunicipio.ExpandAll();
        }

        void AdicionarMunicipioAoGrupo(GrupoAbrangencia grupo, Municipio municipio)
        {
            grupo.Municipios.Add(municipio);
            Factory.GetInstance<IGrupoAbrangencia>().AddMunicipioAoGrupo(grupo.Codigo, municipio.Codigo);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Vínculo Inserido com Sucesso!');", true);

        }

        protected void btnAddVinculo_Click(object sender, EventArgs e)
        {
            Municipio municipio = Factory.GetInstance<IMunicipio>().BuscarPorCodigo<Municipio>(ddlMunicipios.SelectedValue);
            GrupoAbrangencia grupo = Factory.GetInstance<IGrupoAbrangencia>().BuscarPorCodigo<GrupoAbrangencia>(ddlGrupoAbrangencia.SelectedValue);
            bool existe = false;
            foreach (Municipio mun in grupo.Municipios)
                if (mun.Codigo == municipio.Codigo)
                    existe = true;
            if (existe)//Se Existir, ele irá alertar
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Vínculo Já Existe! Por favor, verifique!.');", true);
                return;
            }
            else
            {
                AdicionarMunicipioAoGrupo(grupo, municipio);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Vínculo Inserido com Sucesso!');", true);
            }
            CarregaDadosDoGrupo(grupo);
        }

        protected void TreeViewVinculoGrupoMunicipio_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode node = TreeViewVinculoGrupoMunicipio.SelectedNode;
            ViewState["VinculoGrupoMunicipio"] = node.Value;
            PanelExcluirVinculo.Visible = true;
            string id_grupoAbrangencia = ViewState["VinculoGrupoMunicipio"].ToString().Split('/')[0];
            string id_municipio = ViewState["VinculoGrupoMunicipio"].ToString().Split('/')[1];
            lblConfirmacao.Text = "Deseja excluir o município " + Factory.GetInstance<IMunicipio>().BuscarPorCodigo<Municipio>(id_municipio).Nome;
            lblConfirmacao.Text += " do Grupo " + Factory.GetInstance<IGrupoAbrangencia>().BuscarPorCodigo<GrupoAbrangencia>(id_grupoAbrangencia).NomeGrupo + "?";

        }

        public void btnSim_Click(object sender, EventArgs e)
        {
            string id_grupoAbrangencia = ViewState["VinculoGrupoMunicipio"].ToString().Split('/')[0];
            string id_municipio = ViewState["VinculoGrupoMunicipio"].ToString().Split('/')[1];

            Factory.GetInstance<IGrupoAbrangencia>().DeletarMunicipioDoGrupo(id_grupoAbrangencia, id_municipio);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Registro Deletado com Sucesso!');", true);
            PanelExcluirVinculo.Visible = false;
            GrupoAbrangencia grupo = Factory.GetInstance<IGrupoAbrangencia>().BuscarPorCodigo<GrupoAbrangencia>(ddlGrupoAbrangencia.SelectedValue);
            if (grupo != null)
            {
                CarregaDadosDoGrupo(grupo);
            }
            //CarregaArvoreDeGrupos();

        }

        public void btnNao_Click(object sender, EventArgs e)
        {
            ViewState["VinculoGrupoMunicipio"] = null;
            PanelExcluirVinculo.Visible = false;
        }
    }
}
