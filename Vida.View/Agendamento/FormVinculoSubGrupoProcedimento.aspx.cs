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
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Agendamento
{
    public partial class FormVinculoSubGrupoProcedimento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WUCPesquisaProcedimento1.GridView.SelectedIndexChanging += new GridViewSelectEventHandler(GridViewProcedimento_SelectedIndexChanging);
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CADASTRO_VINCULO_PROCEDIMENTO_SUBGRUPO", Modulo.AGENDAMENTO))
                {
                    ddlSubGrupo.Items.Clear();
                    ddlSubGrupo.Items.Add(new ListItem("Selecione...", "0"));
                    ddlProcedimento.Items.Clear();
                    ddlProcedimento.Items.Add(new ListItem("Selecione...", "0"));
                    ddlCBO.Items.Clear();
                    ddlCBO.Items.Add(new ListItem("Selecione...", "0"));
                    IList<SubGrupo> subGrupos = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<SubGrupo>("NomeSubGrupo", true);
                    foreach (SubGrupo subGrupo in subGrupos)
                        ddlSubGrupo.Items.Add(new ListItem(subGrupo.NomeSubGrupo, subGrupo.Codigo.ToString()));

                    CarregaVinculosAtivos();
                    CarregaVinculosInativos();
                }
            }
        }

        protected void lnkBtnModificarProcedimento_Click(object sender, EventArgs e)
        {
            PanelPesquisaProcedimento.Visible = true;
            ddlProcedimento.Items.Clear();
            ddlProcedimento.Items.Add(new ListItem("Selecione...", "0"));
            ddlCBO.Items.Clear();
            ddlCBO.Items.Add(new ListItem("Selecione...", "0"));
        }

        void CarregaVinculosAtivos()
        {
            IList<SubGrupoProcedimentoCBO> subGrupos = Factory.GetInstance<ISubGrupoProcedimentoCbo>().ListarSubGrupoProcedimentoCbo<SubGrupoProcedimentoCBO>(true);
            GridViewVinculosAtivos.DataSource = subGrupos;
            GridViewVinculosAtivos.DataBind();
            Session["VinculosAtivos"] = subGrupos;
        }

        void CarregaVinculosInativos()
        {
            IList<SubGrupoProcedimentoCBO> subGruposInativos = Factory.GetInstance<ISubGrupoProcedimentoCbo>().ListarSubGrupoProcedimentoCbo<SubGrupoProcedimentoCBO>(false);
            GridViewVinculosInativos.DataSource = subGruposInativos;
            GridViewVinculosInativos.DataBind();
            Session["VinculosInativos"] = subGruposInativos;
        }

        protected void GridViewVinculosAtivos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            IList<SubGrupoProcedimentoCBO> subGruposAtivos = (IList<SubGrupoProcedimentoCBO>)Session["VinculosAtivos"];
            GridViewVinculosAtivos.DataSource = subGruposAtivos;
            GridViewVinculosAtivos.DataBind();
            GridViewVinculosAtivos.PageIndex = e.NewPageIndex;
        }

        protected void GridViewVinculosInativos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            IList<SubGrupoProcedimentoCBO> subGruposInativos = (IList<SubGrupoProcedimentoCBO>)Session["VinculosInativos"];
            GridViewVinculosInativos.DataSource = subGruposInativos;
            GridViewVinculosInativos.DataBind();
            GridViewVinculosInativos.PageIndex = e.NewPageIndex;
        }

        protected void GridViewProcedimento_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            ddlProcedimento.Items.Clear();
            Procedimento procedimento = WUCPesquisaProcedimento1.ProcedimentoSelecionado;
            ddlProcedimento.Items.Add(new ListItem(procedimento.Nome, procedimento.Codigo));
            PanelPesquisaProcedimento.Visible = false;

            //Carrego os CBOs vinculados ao Procedimento
            IList<ViverMais.Model.CBO> cbos = Factory.GetInstance<ICBO>().ListarCBOsPorProcedimento<ViverMais.Model.CBO>(procedimento.Codigo);

            ddlCBO.Items.Clear();
            ddlCBO.Items.Add(new ListItem("Selecione...", "0"));
            
            foreach (CBO cbo in cbos)
                ddlCBO.Items.Add(new ListItem(cbo.Nome, cbo.Codigo.ToString()));
            ddlCBO.Focus();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            //Tratamento para evitar duplicidade
            SubGrupoProcedimentoCBO subGrupoProcedimentoCBO = Factory.GetInstance<ISubGrupoProcedimentoCbo>().VerificaSeExisteAtivo<SubGrupoProcedimentoCBO>(int.Parse(ddlSubGrupo.SelectedValue), ddlProcedimento.SelectedValue, ddlCBO.SelectedValue);
            if (subGrupoProcedimentoCBO != null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Vínculo já existe!')", true);
                return;
            }

            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            Procedimento procedimento = (Procedimento)Session["ProcedimentoSelecionado"];
            if (procedimento == null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Você deve selecionar um Procedimento!')", true);
                return;
            }
            
            SubGrupo subGrupo = iViverMais.BuscarPorCodigo<SubGrupo>(int.Parse(ddlSubGrupo.SelectedValue));
            CBO cbo = iViverMais.BuscarPorCodigo<CBO>(ddlCBO.SelectedValue);

            subGrupoProcedimentoCBO = new SubGrupoProcedimentoCBO(procedimento,subGrupo,cbo);
            iViverMais.Salvar(subGrupoProcedimentoCBO);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Registro incluído com sucesso!'); window.location='FormVinculoSubGrupoProcedimento.aspx'", true);
            //CarregaVinculosAtivos();
        }

        protected void GridViewVinculosAtivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Inativar")
            {
                int co_subGrupoProcedimentoCbo = Convert.ToInt32(e.CommandArgument.ToString());
                SubGrupoProcedimentoCBO subGrupoProcedimentoCBO = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<SubGrupoProcedimentoCBO>(co_subGrupoProcedimentoCbo);
                if (subGrupoProcedimentoCBO != null)
                {
                    subGrupoProcedimentoCBO.Ativo = false;
                    Factory.GetInstance<IViverMaisServiceFacade>().Salvar(subGrupoProcedimentoCBO);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Registro inativado com sucesso!')", true);
                    CarregaVinculosAtivos();
                    CarregaVinculosInativos();
                }
            }
        }
    }
}
