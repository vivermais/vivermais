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
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;

namespace ViverMais.View.Agendamento
{
    public partial class FormVinculoProcedimentoProgramaDeSaude : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "VINCULAR_PROGRAMASAUDE_PROCEDIMENTO", Modulo.AGENDAMENTO))
                {
                    CarregaProgramasDeSaude();
                    CarregaProcedimentos();
                }
            }
        }

        void CarregaProgramasDeSaude()
        {
            ddlProgramaDeSaude.DataSource = Factory.GetInstance<ViverMais.ServiceFacade.ServiceFacades.IViverMaisServiceFacade>().ListarTodos<ProgramaDeSaude>("Nome", true).Where(programa => programa.Ativo).ToList();
            ddlProgramaDeSaude.DataBind();
            ddlProgramaDeSaude.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlProgramaDeSaude.Focus();
        }
        
        protected void ddlProgramaDeSaude_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProgramaDeSaude.SelectedValue != "0")
            {
                ProgramaDeSaude programaDeSaude = Factory.GetInstance<IProgramaDeSaude>().BuscarPorCodigo<ProgramaDeSaude>(int.Parse(ddlProgramaDeSaude.SelectedValue));
                if (programaDeSaude != null)
                {
                    CarregaVinculosAtivos(programaDeSaude);
                    CarregaVinculosInativos(programaDeSaude);
                }
            }
        }

        void CarregaVinculosAtivos(ProgramaDeSaude programa)
        {
            IList<ProgramaDeSaudeProcedimentoCBO> programasDeSaudeProcedimento = Factory.GetInstance<IProgramaDeSaudeProcedimentoCBO>().ListarPorPrograma<ProgramaDeSaudeProcedimentoCBO>(int.Parse(ddlProgramaDeSaude.SelectedValue), true);
            GridViewVinculosAtivos.DataSource = programasDeSaudeProcedimento;
            GridViewVinculosAtivos.DataBind();
            Session["VinculosAtivos"] = programasDeSaudeProcedimento;
        }

        protected void GridViewVinculosAtivos_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewVinculosAtivos.DataSource = Session["VinculosAtivos"];
            GridViewVinculosAtivos.PageIndex = e.NewPageIndex;
            GridViewVinculosAtivos.DataBind();
        }

        protected void GridViewVinculosAtivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id_ProgramaSaudeProcedimento = int.Parse(e.CommandArgument.ToString());
            ProgramaDeSaudeProcedimentoCBO programa = Factory.GetInstance<IProgramaDeSaudeProcedimentoCBO>().BuscarPorCodigo<ProgramaDeSaudeProcedimentoCBO>(id_ProgramaSaudeProcedimento);
            if (programa != null)
            {
                if (e.CommandName == "Inativar")
                {
                    programa.Ativo = false;
                    Factory.GetInstance<IProgramaDeSaudeProcedimentoCBO>().Salvar<ProgramaDeSaudeProcedimentoCBO>(ref programa);
                    Factory.GetInstance<IProgramaDeSaudeProcedimentoCBO>().Salvar(new LogAgendamento(DateTime.Now, ((Usuario)(Session["Usuario"])).Codigo, 59, programa.Codigo.ToString()));
                    CarregaVinculosAtivos(programa.ProgramaDeSaude);
                    CarregaVinculosInativos(programa.ProgramaDeSaude);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok2", "alert('Vínculo inativado com sucesso!');", true);
                }
                else if (e.CommandName == "VisualizaCBO")
                {
                    PanelCbos.Visible = true;
                    lblNomeProcedimento.Text = programa.Procedimento.Nome.ToUpper();
                    ViewState["CodigoProgramaProcedimento"] = id_ProgramaSaudeProcedimento.ToString();
                    IList<CBO> cbos = Factory.GetInstance<ICBO>().ListarCBOsPorProcedimento<ViverMais.Model.CBO>(programa.Procedimento.Codigo);
                    GridviewVinculoCBO.DataSource = cbos;
                    Session["CbosProcedimento"] = cbos;
                    GridviewVinculoCBO.DataBind();

                }
            }
        }

        protected void OnClick_btnFechar(object sender, EventArgs e)
        {
            PanelCbos.Visible = false;
            HiddenSelectedValuesCBO.Value = string.Empty;
            Session.Remove("CbosProcedimento");
            int co_ProgramaProcedimento = int.Parse(ViewState["CodigoProgramaProcedimento"].ToString());
            ProgramaDeSaudeProcedimentoCBO programaProcedimento = Factory.GetInstance<ViverMais.ServiceFacade.ServiceFacades.IViverMaisServiceFacade>().BuscarPorCodigo<ProgramaDeSaudeProcedimentoCBO>(co_ProgramaProcedimento);
            CarregaVinculosAtivos(programaProcedimento.ProgramaDeSaude);
            CarregaVinculosInativos(programaProcedimento.ProgramaDeSaude);
            ViewState.Remove("CodigoProgramaProcedimento");
        }

        protected void btnSalvarVinculoCbo_Click(object sender, EventArgs e)
        {
            if (HiddenSelectedValuesCBO.Value == string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "okCBO", "alert('Você deve selecionar, pelo menos, um CBO!');", true);
                return;
            }
            else
            {
                IList<CBO> cbos = new List<CBO>();
                int co_ProgramaProcedimento = int.Parse(ViewState["CodigoProgramaProcedimento"].ToString());
                ViverMais.ServiceFacade.ServiceFacades.IViverMaisServiceFacade iViverMais = Factory.GetInstance<ViverMais.ServiceFacade.ServiceFacades.IViverMaisServiceFacade>();

                string[] co_cbos = HiddenSelectedValuesCBO.Value.Split('|');
                co_cbos = co_cbos.Where(p => p != string.Empty).Distinct().ToArray();
                for (int i = 0; i < co_cbos.Length; i++)
                {
                    string co_cbo = co_cbos[i];
                    CBO cbo = iViverMais.BuscarPorCodigo<CBO>(co_cbo);
                    if (cbo != null && !cbos.Contains(cbo))
                        cbos.Add(cbo);
                }
                ProgramaDeSaudeProcedimentoCBO programa = iViverMais.BuscarPorCodigo<ProgramaDeSaudeProcedimentoCBO>(co_ProgramaProcedimento);
                programa.Cbos = cbos;
                iViverMais.Salvar(programa);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "okCBO", "alert('CBOs alterados com Sucesso!');", true);
                PanelCbos.Visible = false;
                HiddenSelectedValuesCBO.Value = string.Empty;
                ViewState.Remove("CodigoProgramaProcedimento");
                Session.Remove("CbosProcedimento");
                CarregaVinculosAtivos(programa.ProgramaDeSaude);
                CarregaVinculosInativos(programa.ProgramaDeSaude);
            }
        }

        void CarregaVinculosInativos(ProgramaDeSaude programa)
        {
            IList<ProgramaDeSaudeProcedimentoCBO> programasDeSaudeProcedimento = Factory.GetInstance<IProgramaDeSaudeProcedimentoCBO>().ListarPorPrograma<ProgramaDeSaudeProcedimentoCBO>(int.Parse(ddlProgramaDeSaude.SelectedValue), false);
            GridViewVinculosInativos.DataSource = programasDeSaudeProcedimento;
            GridViewVinculosInativos.DataBind();
            Session["VinculosInativos"] = programasDeSaudeProcedimento;
        }

        protected void GridViewVinculosInativos_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewVinculosInativos.DataSource = Session["VinculosInativos"];
            GridViewVinculosInativos.PageIndex = e.NewPageIndex;
            GridViewVinculosInativos.DataBind();
        }

        protected void GridViewVinculosInativos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Reativar")
            {
                int id_ProgramaSaudeProcedimento = int.Parse(e.CommandArgument.ToString());
                ProgramaDeSaudeProcedimentoCBO programa = Factory.GetInstance<IProgramaDeSaudeProcedimentoCBO>().BuscarPorCodigo<ProgramaDeSaudeProcedimentoCBO>(id_ProgramaSaudeProcedimento);
                if (programa != null)
                {
                    programa.Ativo = true;
                    Factory.GetInstance<IProgramaDeSaudeProcedimentoCBO>().Salvar<ProgramaDeSaudeProcedimentoCBO>(ref programa);
                    Factory.GetInstance<IProgramaDeSaudeProcedimentoCBO>().Salvar(new LogAgendamento(DateTime.Now, ((Usuario)(Session["Usuario"])).Codigo, 60, programa.Codigo.ToString()));
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok2", "alert('Vínculo reativado com sucesso!');", true);
                    CarregaVinculosAtivos(programa.ProgramaDeSaude);
                    CarregaVinculosInativos(programa.ProgramaDeSaude);
                }
            }
        }

        void CarregaProcedimentos()
        {
            ASPxComboBox_Procedimento.DataSource = Factory.GetInstance<IProcedimento>().ListarTodos<Procedimento>("Nome", true);
            ASPxComboBox_Procedimento.DataBind();
        }

        protected void GridviewVinculoCBO_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int co_programaProcedimento = int.Parse(ViewState["CodigoProgramaProcedimento"].ToString());
            ProgramaDeSaudeProcedimentoCBO programa = Factory.GetInstance<IProgramaDeSaudeProcedimentoCBO>().BuscarPorCodigo<ProgramaDeSaudeProcedimentoCBO>(co_programaProcedimento);
            if (programa != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string co_cbo = ((GridView)sender).DataKeys[e.Row.RowIndex].Value.ToString();
                    CheckBox chkSelect = (CheckBox)e.Row.FindControl("RowLevelCheckBox");
                    if (programa.Cbos.Select(cbos => cbos.Codigo).ToArray().Contains(co_cbo))
                        HiddenSelectedValuesCBO.Value += co_cbo.Insert(co_cbo.Length, "|");

                }

                if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
                {
                    CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("RowLevelCheckBox");
                    CheckBox chkBxHeader = (CheckBox)this.GridviewVinculoCBO.HeaderRow.FindControl("HeaderLevelCheckBox");
                    HiddenField hdnFldId = (HiddenField)e.Row.Cells[1].FindControl("hdnFldId");
                    chkBxSelect.Attributes["onclick"] = string.Format("javascript:ChildClick(this,'{0}', '{1}');", chkBxHeader.ClientID, hdnFldId.Value.Trim());
                }
            }
        }

        protected void GridviewVinculoCBO_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridviewVinculoCBO.DataSource = Session["CbosProcedimento"];
            GridviewVinculoCBO.PageIndex = e.NewPageIndex;
            GridviewVinculoCBO.DataBind();
        }

        protected void btnAddVinculo_Click(object sender, EventArgs e)
        {
            if (ddlProgramaDeSaude.SelectedValue != "0")
            {
                ProgramaDeSaude programaDeSaude = Factory.GetInstance<IProgramaDeSaude>().BuscarPorCodigo<ProgramaDeSaude>(int.Parse(ddlProgramaDeSaude.SelectedValue));
                if (!String.IsNullOrEmpty(ASPxComboBox_Procedimento.Value.ToString()) && ASPxComboBox_Procedimento.Value.ToString() != "0")
                {
                    int co_procedimento;
                    int.TryParse(ASPxComboBox_Procedimento.Value.ToString(), out co_procedimento);
                    if (co_procedimento != 0)
                    {
                        ViverMais.Model.Procedimento procedimento = Factory.GetInstance<IAmbulatorial>().BuscarPorCodigo<ViverMais.Model.Procedimento>(co_procedimento.ToString("0000000000"));
                        if (procedimento != null)
                        {
                            ProgramaDeSaudeProcedimentoCBO programaSaudeProcedimento = Factory.GetInstance<IProgramaDeSaudeProcedimentoCBO>().BuscaVinculo<ProgramaDeSaudeProcedimentoCBO>(int.Parse(ddlProgramaDeSaude.SelectedValue), procedimento.Codigo);
                            if (programaSaudeProcedimento == null)
                            {
                                programaSaudeProcedimento = new ProgramaDeSaudeProcedimentoCBO();
                                programaSaudeProcedimento.Ativo = true;
                                programaSaudeProcedimento.Procedimento = procedimento;
                                programaSaudeProcedimento.ProgramaDeSaude = programaDeSaude;
                                Factory.GetInstance<IProgramaDeSaude>().Salvar(programaSaudeProcedimento);
                                Factory.GetInstance<IProgramaDeSaudeProcedimentoCBO>().Salvar(new LogAgendamento(DateTime.Now, ((Usuario)(Session["Usuario"])).Codigo, 58, programaSaudeProcedimento.Codigo.ToString()));
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Vínculo cadastrado com sucesso!');", true);
                            }
                            else if (programaSaudeProcedimento != null && programaSaudeProcedimento.Ativo)
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Vínculo já existe!');", true);
                                return;
                            }
                            else if (programaSaudeProcedimento != null && !programaSaudeProcedimento.Ativo)
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Vínculo já existe e está inativo. Para reativar, vá em Vínculos Inativos e clique em Reativar.');", true);
                                return;
                            }
                        }
                    }
                }
                CarregaVinculosAtivos(programaDeSaude);
                CarregaVinculosInativos(programaDeSaude);
            }


        }
    }
}
