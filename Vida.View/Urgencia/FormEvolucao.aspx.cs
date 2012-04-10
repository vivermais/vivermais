using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using System.Data;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;

namespace Urgence.View
{
    public partial class WebForm24 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int temp;
                if (Request["codigo"] != null && int.TryParse(Request["codigo"].ToString(), out temp))
                {
                    ViverMais.Model.Prontuario prontuario = Factory.GetInstance<IProntuario>().BuscarPorCodigo<ViverMais.Model.Prontuario>(int.Parse(Request["codigo"].ToString()));

                    if (prontuario != null)
                    {
                        lblNumero.Text = prontuario.NumeroToString;
                        lblData.Text = prontuario.Data.ToShortDateString();

                        lblPaciente.Text = string.IsNullOrEmpty(prontuario.Paciente.Nome) ? "Não Identificado" : prontuario.Paciente.Nome;

                        if (!string.IsNullOrEmpty(prontuario.Paciente.CodigoViverMais))
                        {
                            ViverMais.Model.Paciente pacienteViverMais = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(prontuario.Paciente.CodigoViverMais);

                            if (pacienteViverMais != null)
                                lblPaciente.Text = pacienteViverMais.Nome;
                        }

                        IList<ViverMais.Model.Evolucao> evolucoes = new List<ViverMais.Model.Evolucao>();
                        evolucoes = Factory.GetInstance<IEvolucao>().buscaPorProntuario<ViverMais.Model.Evolucao>(prontuario.Codigo);

                        foreach (ViverMais.Model.Evolucao ev in evolucoes)
                            ev.Profissional = Factory.GetInstance<IProfissional>().BuscarPorCodigo<ViverMais.Model.Profissional>(ev.CodigoProfissional.Trim());

                        Session["evolucoes"] = evolucoes;
                        gridEvolucao.DataSource = evolucoes;
                        gridEvolucao.DataBind();

                        IList<ViverMais.Model.ProntuarioProcedimento> lpp = Factory.GetInstance<IProntuarioProcedimento>().BuscarPorProntuario<ViverMais.Model.ProntuarioProcedimento>(prontuario.Codigo);
                        foreach (ViverMais.Model.ProntuarioProcedimento pp in lpp)
                            pp.Procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<ViverMais.Model.Procedimento>(pp.CodigoProcedimento);

                        gridProcedimento.DataSource = lpp;
                        gridProcedimento.DataBind();
                    }
                }
            }
        }

        protected void btnSalvar_Click1(object sender, EventArgs e)
        {
            ViverMais.Model.UsuarioProfissionalUrgence up = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigoIdentificacao<ViverMais.Model.UsuarioProfissionalUrgence>(TextBox_CodigoIdentificacao.Text);

            if (up != null)
            {
                ViverMais.Model.Prontuario prontuario = new ViverMais.Model.Prontuario();
                prontuario = Factory.GetInstance<IProntuario>().BuscarPorCodigo<ViverMais.Model.Prontuario>(int.Parse(Request["codigo"].ToString()));
                ViverMais.Model.Evolucao evolucao = new ViverMais.Model.Evolucao();
                IList<ViverMais.Model.Evolucao> evolucoes = new List<ViverMais.Model.Evolucao>();
                evolucoes = (List<ViverMais.Model.Evolucao>)Session["evolucoes"];

                evolucao.Prontuario = prontuario;
                evolucao.Observacao = tbxObservacao.Text;
                evolucao.Aprazamento = tbxAprazamento.Text;
                evolucao.Data = DateTime.Now;
                evolucao.CodigoProfissional = up.Id_Profissional;
                evolucao.Profissional = Factory.GetInstance<IProfissional>().BuscarPorCodigo<ViverMais.Model.Profissional>(up.Id_Profissional);

                try
                {
                    Factory.GetInstance<IEvolucao>().Salvar(evolucao);
                    evolucoes.Add(evolucao);
                    tbxObservacao.Text = "";
                    tbxAprazamento.Text = "";
                    TextBox_CodigoIdentificacao.Text = "";
                    Session["evolucoes"] = evolucoes;
                    gridEvolucao.DataSource = evolucoes;
                    gridEvolucao.DataBind();
                    //Registro de Log
                    Factory.GetInstance<IUrgenciaServiceFacade>().Inserir(new LogUrgencia(DateTime.Now, up.Id_Usuario, 1, "id evolucao:" + evolucao.Codigo));
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados salvos com sucesso!');", true);
                }
                catch (Exception f)
                {
                    throw f;
                }
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Profissional não identificado! Por favor, identifique o usuário.');", true);
        }
    }
}
