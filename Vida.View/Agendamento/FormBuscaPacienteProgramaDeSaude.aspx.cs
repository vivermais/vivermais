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
using System.Collections.Generic;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;

namespace ViverMais.View.Agendamento
{
    public partial class FormBuscaPacienteProgramaDeSaude : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ViverMais.ServiceFacade.ServiceFacades.Seguranca.ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "INATIVAR PACIENTE DO PROGRAMA", Modulo.AGENDAMENTO) && Factory.GetInstance<ViverMais.ServiceFacade.ServiceFacades.Seguranca.ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "REATIVAR PACIENTE DO PROGRAMA", Modulo.AGENDAMENTO))
                {
                    CarregaProgramasDeSaude();
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

        protected void GridViewVinculosAtivos_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewVinculosAtivos.DataSource = Session["PacientesAtivos"];
            GridViewVinculosAtivos.PageIndex = e.NewPageIndex;
            GridViewVinculosAtivos.DataBind();
        }

        protected void GridViewVinculosAtivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string co_paciente = e.CommandArgument.ToString();
            if (e.CommandName == "Inativar")
            {
                ProgramaDeSaudePaciente programaPaciente = Factory.GetInstance<IProgramaDeSaude>().BuscarProgramaDeSaudePaciente<ProgramaDeSaudePaciente>(int.Parse(ddlProgramaDeSaude.SelectedValue), co_paciente);
                if (programaPaciente != null)
                {
                    programaPaciente.Ativo = false;
                    Factory.GetInstance<IProgramaDeSaude>().Salvar(programaPaciente);
                    Factory.GetInstance<IProgramaDeSaude>().Salvar(new LogAgendamento(DateTime.Now, ((Usuario)(Session["Usuario"])).Codigo, 61, (programaPaciente.ProgramaDeSaude.Codigo.ToString() + "/" + programaPaciente.Paciente.Codigo)));
                    CarregaVinculosAtivos(programaPaciente.ProgramaDeSaude);
                    CarregaVinculosInativos(programaPaciente.ProgramaDeSaude);
                }
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok2", "alert('Paciente inativado com sucesso!');", true);
            }
        }

        protected void GridViewVinculosInativos_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewVinculosInativos.DataSource = Session["PacientesInativos"];
            GridViewVinculosInativos.PageIndex = e.NewPageIndex;
            GridViewVinculosInativos.DataBind();
        }

        protected void GridViewVinculosInativos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Reativar")
            {
                string co_paciente = e.CommandArgument.ToString();
                if (e.CommandName == "Inativar")
                {
                    ProgramaDeSaudePaciente programaPaciente = Factory.GetInstance<IProgramaDeSaude>().BuscarProgramaDeSaudePaciente<ProgramaDeSaudePaciente>(int.Parse(ddlProgramaDeSaude.SelectedValue), co_paciente);
                    if (programaPaciente != null)
                    {
                        programaPaciente.Ativo = true;
                        Factory.GetInstance<IProgramaDeSaude>().Salvar(programaPaciente);
                        Factory.GetInstance<IProgramaDeSaude>().Salvar(new LogAgendamento(DateTime.Now, ((Usuario)(Session["Usuario"])).Codigo, 61, (programaPaciente.ProgramaDeSaude.Codigo.ToString() + "/" + programaPaciente.Paciente.Codigo)));
                        CarregaVinculosAtivos(programaPaciente.ProgramaDeSaude);
                        CarregaVinculosInativos(programaPaciente.ProgramaDeSaude);
                    }
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok2", "alert('Paciente inativado com sucesso!');", true);
                }
            }
        }

        void CarregaVinculosAtivos(ProgramaDeSaude programa)
        {
            IList<ViverMais.Model.Paciente> pacientes = Factory.GetInstance<IProgramaDeSaude>().ListarPacientesPorPrograma<ViverMais.Model.Paciente>(programa.Codigo, true);
            GridViewVinculosAtivos.DataSource = pacientes;
            GridViewVinculosAtivos.DataBind();
            Session["PacientesAtivos"] = pacientes;
        }

        void CarregaVinculosInativos(ProgramaDeSaude programa)
        {
            IList<ViverMais.Model.Paciente> pacientes = Factory.GetInstance<IProgramaDeSaude>().ListarPacientesPorPrograma<ViverMais.Model.Paciente>(programa.Codigo, false);
            GridViewVinculosAtivos.DataSource = pacientes;
            GridViewVinculosAtivos.DataBind();
            Session["PacientesInativos"] = pacientes;
        }
    }
}
