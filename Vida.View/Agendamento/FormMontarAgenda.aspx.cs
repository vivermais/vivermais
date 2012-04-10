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
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using System.Text.RegularExpressions;


namespace ViverMais.View.Agendamento
{
    public partial class FormMontarAgenda : System.Web.UI.Page
    {
        //protected void OnClick_btnFechar(object sender, EventArgs e)
        //{
        //    PanelMensagem.Visible = false;
        //    Session["LeuMsgAgenda"] = true;
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MONTAR_AGENDA", Modulo.AGENDAMENTO))
                {
                    //if(Session["LeuMsgAgenda"] == null)
                    //    PanelMensagem.Visible = true;
                    //else
                    //    PanelMensagem.Visible = false;

                    Session["Datas"] = new List<DateTime>();
                    Session["Excluidos"] = new List<DateTime>();

                    if (Request.QueryString["id_agenda"] != null)
                    {
                        IAgendamentoServiceFacade iagendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
                        int id_agenda = int.Parse(Request.QueryString["id_agenda"]);

                        Agenda agenda = iagendamento.BuscarPorCodigo<Agenda>(id_agenda);
                        tbxCompetencia.Text = agenda.Competencia.ToString();

                        Usuario usuario = (Usuario)Session["Usuario"];
                        string id_unidade = usuario.Unidade.CNES;
                        string id_procedimento = "";
                        string cnes = usuario.Unidade.CNES;



                        // Prepara lista de Procedimentos a partir do FPO

                        ddlProcedimento.Items.Clear();
                        IFPO ifpo = Factory.GetInstance<IFPO>();
                        IList<ViverMais.Model.FPO> fpo = ifpo.BuscarFPO<ViverMais.Model.FPO>(id_unidade, int.Parse(tbxCompetencia.Text));
                        ddlProcedimento.Items.Add(new ListItem("Selecione...", "0"));
                        foreach (ViverMais.Model.FPO f in fpo)
                        {
                            ddlProcedimento.Items.Add(new ListItem(f.Procedimento.Nome, f.Procedimento.Codigo));
                        }
                        ddlProcedimento.SelectedValue = agenda.Procedimento.Codigo;

                        string cod_cbo = agenda.Cbo.Codigo;
                        string id_profissional = "";
                        IVinculo ivinculo = Factory.GetInstance<IVinculo>();


                        // Monta lista de Profissionais ligados ao Vinculo do CNES
                        ddlProfissional.Items.Clear();
                        IList<ViverMais.Model.VinculoProfissional> prof = ivinculo.BuscarPorCNESCBO<ViverMais.Model.VinculoProfissional>(cnes, cod_cbo);
                        ddlProfissional.Items.Add(new ListItem("Selecione...", "0"));
                        foreach (ViverMais.Model.VinculoProfissional f in prof)
                        {
                            id_profissional = f.Profissional.CPF;
                            IViverMaisServiceFacade iProfissional = Factory.GetInstance<IViverMaisServiceFacade>();
                            ViverMais.Model.Profissional profissional = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(id_profissional);
                            if (profissional != null)
                            {
                                ddlProfissional.Items.Add(new ListItem(profissional.Nome, profissional.CPF));
                            }
                        }
                        ddlProfissional.SelectedValue = agenda.ID_Profissional.CPF;
                        tbxQuantidade.Text = agenda.Quantidade.ToString();
                        ddlTurno.SelectedValue = agenda.Turno;
                        tbxHora_Inicial.Text = agenda.Hora_Inicial;
                        tbxHora_Final.Text = agenda.Hora_Final;
                        ddlTurno.SelectedValue = agenda.Turno;
                        CarrregaDiaCalendario(DateTime.Parse(agenda.Data.ToString()));
                    }
                    btnSalvar.Attributes.Add("onclick", "javascript:return ValidaQuantidade('" + tbxQuantidade.ClientID + "')");
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        protected void CarrregaDiaCalendario(DateTime data)
        {
            //VERIFICA SE A DATA INFORMADA É UM FERIADO
            if (Factory.GetInstance<IFeriado>().VerificaData(Calendar1.SelectedDate))
            {
                //Verifico se o estabelecimento é Tolerante à feriado
                if (!Factory.GetInstance<IUnidade>().VerificaEstabelecimentoToleranteFeriado(((Usuario)Session["Usuario"]).Unidade.CNES) != true)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('A Data Selecionada é um Feriado. E o estabelecimento não é Tolerante à Feriado!');", true);
                    //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('A Data Selecionada é um Feriado. E o estabelecimento não é Tolerante à Feriado!')</script>");
                    return;
                }
            }

            Calendar1.SelectedDates.Clear();
            Calendar1.SelectedDate = data;


        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbxCompetencia.Text))
            {
                //Verifica se  a data está compatível com a Competencia
                if (verificaData(Calendar1.SelectedDate) == false)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Data Inválida. Incompatível com a Competência.');", true);
                    //ClientScript.RegisterClientScriptBlock(typeof(String), "erro", "<script>alert('Data Inválida. Incompatível com a Competência.');</script>");
                    return;
                }

                IList<DateTime> datas = (IList<DateTime>)Session["Datas"];
                IList<DateTime> excluidos = (IList<DateTime>)Session["Excluidos"];
                //VERIFICA SE A DATA INFORMADA É UM FERIADO
                if (Factory.GetInstance<IFeriado>().VerificaData(Calendar1.SelectedDate))
                {
                    //IList unidades = Factory.GetInstance<IUnidade>().VerificaEstabelecimentoToleranteFeriado(((Usuario)Session["Usuario"]).Unidade.CNES);
                    //Verifico se o estabelecimento é Tolerante à feriado
                    if (Factory.GetInstance<IUnidade>().VerificaEstabelecimentoToleranteFeriado(((Usuario)Session["Usuario"]).Unidade.CNES) != true)
                    {
                        ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('A Data Selecionada é um Feriado. E o estabelecimento não é Tolerante à Feriado!')</script>");
                        datas.Remove(Calendar1.SelectedDate);
                        Calendar1.SelectedDates.Remove(Calendar1.SelectedDate);
                        Session["Datas"] = datas;
                    }
                }
                if (Calendar1.SelectedDate > DateTime.Today)
                {

                    if (datas.Contains(Calendar1.SelectedDate))
                    {
                        datas.Remove(Calendar1.SelectedDate);
                        // Aqui busca qtd já agendada e verifica se pode excluir. Se existir não pode. > Msg de Erro. 
                        excluidos.Add(Calendar1.SelectedDate);
                    }
                    else
                    {
                        datas.Add(Calendar1.SelectedDate);
                        if (excluidos.Contains(Calendar1.SelectedDate))
                        {
                            excluidos.Remove(Calendar1.SelectedDate);
                        }
                    }
                }
                Calendar1.SelectedDates.Clear();
                foreach (DateTime data in datas)
                {
                    Calendar1.SelectedDates.Add(data);
                }
                Session["Datas"] = datas;
                Session["Excluidos"] = excluidos;
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Você deve informar inicialmente a competência');", true);
                Calendar1.SelectedDates.Remove(Calendar1.SelectedDate);
                //ClientScript.RegisterClientScriptBlock(typeof(String), "erro", "<script>alert('Data Inválida. Incompatível com a Competência.');</script>");
                return;
            }
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (Calendar1.SelectedDates.Contains(e.Day.Date))
            {
                if (e.Day.Date > DateTime.Today)
                {
                    e.Cell.BackColor = System.Drawing.Color.Blue;
                }
                else
                {
                    e.Cell.BackColor = System.Drawing.Color.White;
                    e.Cell.ForeColor = System.Drawing.Color.MediumAquamarine;
                }
            }

        }

        protected bool ValidaHora(string hora)
        {
            string expressao = "^([0-1][0-9]|[2][0-3]):[0-5][0-9]$";

            if (Regex.IsMatch(hora, expressao))
                return true;
            return false;
        }

        protected void btnSim_Click(object sender, EventArgs e)
        {
            //PanelMensagem.Visible = false;
            IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
            IList<ViverMais.Model.Agenda> ag = (IList<ViverMais.Model.Agenda>)Session["ListaAgenda"];
            foreach (Agenda agenda in ag)
            {
                if (!agenda.Publicada)
                {
                    agenda.Publicada = true;
                    iAgendamento.Salvar(agenda);
                    iAgendamento.Inserir(new LogAgendamento(DateTime.Now, ((Usuario)(Session["Usuario"])).Codigo, 50, agenda.Codigo.ToString()));
                }
            }
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Agendas Publicadas com Sucesso!');", true);
            btnPesquisar_Click(new object(), new EventArgs());
        }

        protected void btnNao_Click(object sender, EventArgs e)
        {
            //PanelMensagem.Visible = false;
        }

        protected void OnClick_btnFechar(object sender, EventArgs e)
        {
            //PanelMensagem.Visible = false;
        }

        protected void Salvar_Click(object sender, EventArgs e)
        {
            IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
            IFeriado iFeriado = Factory.GetInstance<IFeriado>();

            Usuario usuario = (Usuario)Session["Usuario"];
            string id_unidade = usuario.Unidade.CNES;
            if (!(ValidaHora(tbxHora_Inicial.Text) && (ValidaHora(tbxHora_Final.Text))))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Horário Inválido')", true);
                return;
            }
            if (tbxHora_Inicial.Text == "__:__" || tbxHora_Final.Text == "__:__")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Horário Inválido')", true);
                return;
            }
            if (String.IsNullOrEmpty(tbxHora_Inicial.Text) || String.IsNullOrEmpty(tbxHora_Final.Text) || ddlTurno.SelectedValue == "0" || String.IsNullOrEmpty(tbxQuantidade.Text))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Preencha todos os campos para montar a agenda')", true);
                return;
            }
            if (DateTime.Parse(tbxHora_Inicial.Text) > DateTime.Parse(tbxHora_Final.Text))
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Hora Inicial é Maior que a Hora Final!'); </script>");
                return;
            }

            if (!ValidaHorarioComTurno())
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Horário incompatível com o Turno.')", true);
                return;
            }

            // Criticar Quantidade de Vagas
            if ((tbxQuantidade.Text == "0") || (tbxQuantidade.Text == ""))
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Quantidade Inválida. !');</script>");
                return;
            }
            // Pegar todas as datas que estão na Lista Gravada e Gravar registro para cada um
            IList<DateTime> datas = (IList<DateTime>)Session["Datas"];
            // Criticar as datas . Se estiver com erro, não grava nada
            foreach (DateTime data in datas)
            {
                Agenda agenda = new Agenda();
                agenda.Data = data;

                // Verificar se o Estabelecimento está Afastado nesta data
                AfastamentoEAS afastamentoeas = Factory.GetInstance<IAfastamentoEAS>().VerificaAfastamentosNaData<ViverMais.Model.AfastamentoEAS>(id_unidade, data);
                if (afastamentoeas != null)
                {
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('O Profissional Estará Afastado na Data " + data.ToString("dd/MM/yyyy") + "!');", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('O Estabelecimento de Saúde Inativo Para a data " + data.ToString("dd/MM/yyyy") + "!');", true);
                    return;
                }
                // Verificar se o Profissional está Afastado nesta data
                AfastamentoProfissional afastamentoprofissional = Factory.GetInstance<IAfastamentoProfissional>().VerificaAfastamentosNaData<ViverMais.Model.AfastamentoProfissional>(id_unidade, data, ddlProfissional.SelectedValue);
                if (afastamentoprofissional != null)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('O Profissional Estará Afastado na Data " + data.ToString("dd/MM/yyyy") + "!');", true);
                    return;
                }
            }

            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            foreach (DateTime data in datas)
            {
                Agenda agenda = new Agenda();
                agenda.Data = data;
                agenda.Hora_Inicial = tbxHora_Inicial.Text;
                agenda.Hora_Final = tbxHora_Final.Text;
                agenda.Turno = ddlTurno.SelectedValue;
                agenda.Quantidade = int.Parse(tbxQuantidade.Text);
                agenda.ID_Profissional = iViverMais.BuscarPorCodigo<ViverMais.Model.Profissional>(ddlProfissional.SelectedValue);
                agenda.Estabelecimento = usuario.Unidade;
                //iViverMais.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(id_unidade);
                agenda.Cbo = iViverMais.BuscarPorCodigo<CBO>(ddlCBO.SelectedValue);
                agenda.Competencia = int.Parse(tbxCompetencia.Text);
                agenda.QuantidadeAgendada = 0;
                agenda.Publicada = false;
                agenda.Procedimento = iViverMais.BuscarPorCodigo<ViverMais.Model.Procedimento>(ddlProcedimento.SelectedValue);
                if (ddlSubGrupo.SelectedValue != "0")
                    agenda.SubGrupo = iViverMais.BuscarPorCodigo<SubGrupo>(int.Parse(ddlSubGrupo.SelectedValue));
                iAgendamento.Salvar(agenda);
                iAgendamento.Salvar(new LogAgendamento(DateTime.Now, ((Usuario)(Session["Usuario"])).Codigo, 28, "ID_AGENDA: " + agenda.Codigo));
            }

            // Pegar as Datas que foram excluidas e excluir os registros
            IList<DateTime> excluidos = (IList<DateTime>)Session["Excluidos"];
            foreach (DateTime data in excluidos)
            {
                //Busca o registro Agenda, para esta data e se existir , Exclui
                Agenda agenda = new Agenda();
                agenda = Factory.GetInstance<IAmbulatorial>().BuscaDuplicidade<ViverMais.Model.Agenda>(id_unidade, int.Parse(tbxCompetencia.Text), ddlProcedimento.SelectedValue.ToString(), ddlProfissional.SelectedValue.ToString(), data, ddlTurno.SelectedValue);
                if (agenda != null)
                {
                    iAgendamento.Deletar(agenda);
                }
            }
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Dados Salvos com sucesso!'); location='FormMontarAgenda.aspx';", true);
            //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Dados salvos com sucesso!');window.location='FormMontarAgenda.aspx'</script>");
        }

        bool ValidaHorarioComTurno()
        {
            if (ddlTurno.SelectedValue != "0")
            {
                if (ddlTurno.SelectedValue == "M")
                {
                    if ((int.Parse(tbxHora_Inicial.Text.Replace(":", string.Empty)) >= 600) && (int.Parse(tbxHora_Inicial.Text.Replace(":", string.Empty)) <= 1200) && (int.Parse(tbxHora_Final.Text.Replace(":", string.Empty)) <= 1200))
                        return true;
                }
                else if (ddlTurno.SelectedValue == "T")
                {
                    if ((int.Parse(tbxHora_Inicial.Text.Replace(":", string.Empty)) >= 1201) && (int.Parse(tbxHora_Inicial.Text.Replace(":", string.Empty)) <= 1859) && (int.Parse(tbxHora_Final.Text.Replace(":", string.Empty)) <= 1859))
                        return true;
                }
                else if (ddlTurno.SelectedValue == "N")
                {
                    if ((int.Parse(tbxHora_Inicial.Text.Replace(":", string.Empty)) >= 1900) && (int.Parse(tbxHora_Inicial.Text.Replace(":", string.Empty)) <= 2359) && (int.Parse(tbxHora_Final.Text.Replace(":", string.Empty)) <= 2359))
                        return true;
                }
            }
            return false;
            //tbxHora_Inicial.Text != "__:__" || tbxHora_Final.Text == "__:__")
        }

        protected bool verificaData(DateTime data)
        {
            int ano = int.Parse(tbxCompetencia.Text.Substring(0, 4));
            int mes = int.Parse(tbxCompetencia.Text.Substring(4, 2));

            int dia = DateTime.DaysInMonth(ano, mes);

            DateTime dataInicial = DateTime.Parse("01/" + mes + "/" + ano);
            DateTime dataFinal = DateTime.Parse(dia + "/" + mes + "/" + ano);

            if (data > dataFinal || data < dataInicial)
            {
                return false;
            }
            return true;
        }

        protected void tbxCompetencia_TextChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;

            Usuario usuario = (Usuario)Session["Usuario"];
            string id_unidade = usuario.Unidade.CNES;
            string id_procedimento = "";
            string cnes = usuario.Unidade.CNES;
            // Prepara lista de Procedimentos a partir do FPO

            ddlProcedimento.Items.Clear();
            IFPO ifpo = Factory.GetInstance<IFPO>();
            IList<ViverMais.Model.FPO> fpo = ifpo.BuscarFPO<ViverMais.Model.FPO>(id_unidade, int.Parse(tbxCompetencia.Text));
            ddlProcedimento.Items.Add(new ListItem("Selecione...", "0"));
            List<Procedimento> procedimentos = new List<Procedimento>();
            foreach (ViverMais.Model.FPO f in fpo)
                procedimentos.Add(f.Procedimento);
            
            procedimentos = procedimentos.OrderBy(p => p.Nome).ToList();
            foreach (Procedimento proced in procedimentos)
                ddlProcedimento.Items.Add(new ListItem(proced.Codigo + " - " + proced.Nome, proced.Codigo));
        }

        protected void ddlCBO_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Carrega os SubGrupos Vinculados a Especialidade e Procedimento
            ddlSubGrupo.Items.Clear();
            ddlSubGrupo.Items.Add(new ListItem("Selecione...", "0"));
            IList<SubGrupo> subGrupos = Factory.GetInstance<ISubGrupoProcedimentoCbo>().ListarSubGrupoPorProcedimentoECbo<SubGrupo>(ddlProcedimento.SelectedValue, ddlCBO.SelectedValue, true);
            foreach (SubGrupo subGrupo in subGrupos)
                ddlSubGrupo.Items.Add(new ListItem(subGrupo.NomeSubGrupo, subGrupo.Codigo.ToString()));

            Usuario usuario = (Usuario)Session["Usuario"];
            string cnes = usuario.Unidade.CNES;

            string id_profissional = "";
            // Monta lista de Profissionais ligados ao Vinculo do CNES
            ddlProfissional.Items.Clear();
            IVinculo ivinculo = Factory.GetInstance<IVinculo>();
            IList<ViverMais.Model.VinculoProfissional> vinculo = ivinculo.BuscarPorCNESCBO<ViverMais.Model.VinculoProfissional>(cnes, ddlCBO.SelectedValue.ToString()).Where(p => p.Status == Convert.ToChar(VinculoProfissional.DescricaStatus.Ativo).ToString()).ToList().Distinct().ToList();
            ddlProfissional.Items.Add(new ListItem("Selecione...", "0"));

            foreach (ViverMais.Model.VinculoProfissional f in vinculo)
            {
                id_profissional = f.Profissional.CPF;
                if (ddlProfissional.Items.FindByValue(id_profissional) == null)
                {
                    IViverMaisServiceFacade iProfissional = Factory.GetInstance<IViverMaisServiceFacade>();
                    ViverMais.Model.Profissional profissional = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(id_profissional);
                    if (profissional != null)
                        ddlProfissional.Items.Add(new ListItem(profissional.Nome, profissional.CPF));
                }
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["Usuario"];
            string id_unidade = usuario.Unidade.CNES;
            IList<ViverMais.Model.Agenda> agendas = Factory.GetInstance<IAmbulatorial>().BuscarAgendas<ViverMais.Model.Agenda>(id_unidade, int.Parse(tbxCompetencia.Text), ddlProcedimento.SelectedValue, ddlProfissional.SelectedValue, string.Empty, int.Parse(ddlSubGrupo.SelectedValue));
            Session["ListaAgenda"] = agendas;
            gridAgenda.DataSource = agendas;
            gridAgenda.DataBind();
            PanelAgendas.Visible = true;
            DesabilitaBotaoDeAcordoComAgenda();
            if (agendas.Where(agd => agd.Publicada == false).ToList().Count != 0)                
                lbkPublicar.Visible = true;
        }

        void ExibePanelLimiteFPO(int qtd_FPO, int qtdExcedida)
        {
            //PanelMensagem.Visible = true;
            //lblQtdFPO.Text = qtd_FPO.ToString();
            //lblQtdExcedida.Text = qtdExcedida.ToString();
        }

        protected void lbkPublicar_Click(object sender, EventArgs e)
        {
            IList<ViverMais.Model.Agenda> ag = (IList<ViverMais.Model.Agenda>)Session["ListaAgenda"];
            string msg = string.Empty;

            if ((Session["Usuario"] != null) && (Session["ListaAgenda"] != null))
            {
                //string cnes = ((Usuario)(Session["Usuario"])).Unidade.CNES;
                //Busca a Fpo referente a unidade, procedimento e competencia
                //FPO fpo = Factory.GetInstance<IFPO>().BuscarFpoCompetencia<ViverMais.Model.FPO>(cnes, ddlProcedimento.SelectedValue, int.Parse(tbxCompetencia.Text));
                //if (fpo != null)
                //{
                //    ////Pego as Agendas que já estão publicadas
                //    //IList<Agenda> agendasPublicadas = Factory.GetInstance<IAmbulatorial>().BuscarAgendas<Agenda>(cnes, int.Parse(tbxCompetencia.Text), ddlProcedimento.SelectedValue, string.Empty, string.Empty).Where(p => p.Publicada == true).ToList();
                //    //int somaVagasPublicadas = agendasPublicadas.Sum(p => p.Quantidade);

                //    ////Pego também as agendas que estão na sessão que serão publicada
                //    //IList<Agenda> agendas = ((IList<Agenda>)Session["ListaAgenda"]).Where(p => p.Publicada == false).ToList();
                //    //int somaVagas = agendas.Sum(p => p.Quantidade);

                //    //if ((somaVagas + somaVagasPublicadas) > fpo.QTD_Total)//Se exceder a FPO, é exibido o Panel para confirmação do usuário
                //    //{
                //    //    //ExibePanelLimiteFPO(fpo.QTD_Total, (somaVagas + somaVagasPublicadas) - fpo.QTD_Total);
                //    //    //return;
                //    //    ////Pendente de Regina e Vanina mandarem a Mensagem que irá aparecer
                //    //    //msg =  "alert('Quantidade de Vagas Excede FPO. \\nFPO: " + fpo.QTD_Total + "\\nAgendas Publicadas: " + somaVagasPublicadas + "\\nResta a Publicar: " + (fpo.QTD_Total - somaVagasPublicadas) + "');";
                //    //}
                //    //else
                //    //{
                //    //    msg = "alert('Agendas Publicadas com Sucesso!');";
                //    //    //PanelMensagem.Visible = false;
                //    //}
                //}

                foreach (Agenda agenda in ag)
                {
                    if (!agenda.Publicada)
                    {
                        agenda.Publicada = true;
                        Factory.GetInstance<IAgendamentoServiceFacade>().Salvar(agenda);
                        Factory.GetInstance<IAgendamentoServiceFacade>().Salvar(new LogAgendamento(DateTime.Now, ((Usuario)(Session["Usuario"])).Codigo, 50, agenda.Codigo.ToString()));
                    }
                }
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Agendas Publicadas com Sucesso!');", true);
                btnPesquisar_Click(new object(), new EventArgs());
            }
            Session.Remove("ListaAgenda");
            lbkPublicar.Enabled = false;
        }

        protected void gridAgenda_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[4].Text == "Publicada")
                {
                    e.Row.Cells[1].Enabled = false;
                }
                //if(e.Row.Cells[4].Text)
            }
        }

        protected void gridAgenda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                string id_agenda = e.CommandArgument.ToString();
                Agenda agenda = Factory.GetInstance<IAmbulatorial>().BuscarPorCodigo<Agenda>(int.Parse(id_agenda));
                IList<Agenda> agendasSession = (IList<Agenda>)Session["ListaAgenda"];
                agendasSession.Remove(agenda);
                Factory.GetInstance<IAmbulatorial>().Deletar(agenda);
                Factory.GetInstance<IAmbulatorial>().Salvar(new LogAgendamento(DateTime.Now, ((Usuario)(Session["Usuario"])).Codigo, 33, id_agenda));

                Session["ListaAgenda"] = agendasSession;
                gridAgenda.DataSource = Session["ListaAgenda"];
                gridAgenda.DataBind();
                UpdatePanel1.Update();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Agenda Excluída com Sucesso!');", true);
                DesabilitaBotaoDeAcordoComAgenda();
                btnPesquisar_Click(new object(), new EventArgs());
            }
        }

        protected void gridAgenda_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridAgenda.EditIndex = e.NewEditIndex;
            gridAgenda.DataSource = Session["ListaAgenda"];
            gridAgenda.DataBind();

        }

        protected void gridAgenda_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            GridViewRow linha = gridAgenda.Rows[e.RowIndex];
            string data = ((TextBox)linha.FindControl("tbxData")).Text;
            if (DateTime.Parse(data) < DateTime.Today)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(String), "ok", "<script>alert('A Data não pode ser anterior a data de hoje !'); </script>", false);
                return;

            }
            string quantidade = ((TextBox)linha.FindControl("tbxQuantidade")).Text;

            //IList<Agenda> agendas = (IList<Agenda>)Session["ListaAgenda"];
            string id_agenda = gridAgenda.DataKeys[e.RowIndex].Value.ToString();
            Agenda agenda = Factory.GetInstance<IAgendamentoServiceFacade>().BuscarPorCodigo<Agenda>(int.Parse(id_agenda));
            agenda.Data = DateTime.Parse(data);
            agenda.Quantidade = int.Parse(quantidade);
            IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
            iAgendamento.Salvar(agenda);
            iAgendamento.Salvar(new LogAgendamento(DateTime.Now, ((Usuario)(Session["Usuario"])).Codigo, 32, agenda.Codigo.ToString()));
            //DataRow dtRow = table.Rows.Find(id_agenda);
            //if (dtRow != null)
            //{
            //    table.Rows[e.RowIndex][2] = data;
            //    table.Rows[e.RowIndex][5] = quantidade;
            //}

            //Agenda agenda = iAgendamento.BuscarPorCodigo<Agenda>(int.Parse(id_agenda));
            //if (agenda != null)
            //{
            //agenda.Data = DateTime.Parse(data);
            //agenda.Quantidade = int.Parse(quantidade);

            //}
            this.btnPesquisar_Click(new object(), new EventArgs());
            Session["ListaAgenda"] = Factory.GetInstance<IAmbulatorial>().BuscarAgendas<ViverMais.Model.Agenda>(agenda.Estabelecimento.CNES, int.Parse(tbxCompetencia.Text), ddlProcedimento.SelectedValue, ddlProfissional.SelectedValue, string.Empty, int.Parse(ddlSubGrupo.SelectedValue));;
            gridAgenda.EditIndex = -1;
            gridAgenda.DataSource = Session["ListaAgenda"];
            gridAgenda.DataBind();
            DesabilitaBotaoDeAcordoComAgenda();
        }

        protected void gridAgenda_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridAgenda.EditIndex = -1;
            //DataTable table = (DataTable)Session["ListaAgenda"];
            gridAgenda.DataSource = Session["ListaAgenda"]; ;
            gridAgenda.DataBind();

        }

        protected void ddlProcedimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            IList<ViverMais.Model.CBO> cbosDoProcedimento = Factory.GetInstance<ICBO>().ListarCBOsPorProcedimento<ViverMais.Model.CBO>(ddlProcedimento.SelectedValue);
            Usuario usuario = (Usuario)Session["Usuario"];
            string cnes = usuario.Unidade.CNES;

            // Monta lista de CBO ligados ao Vinculo do CNES
            IList<CBO> cbosDoVinculo = Factory.GetInstance<IVinculo>().BuscarCbosDaUnidade<CBO>(cnes).Distinct().ToList();
            ddlCBO.Items.Clear();
            ddlCBO.Items.Add(new ListItem("Selecione...", "0"));

            var intersecao = from result in cbosDoVinculo
                             where
                                 cbosDoProcedimento.Select(p => p.Codigo).ToList().Contains(result.Codigo)
                             select result;
            foreach (CBO cbo in intersecao)
                ddlCBO.Items.Add(new ListItem(cbo.Nome, cbo.Codigo));
        }

        protected void gridAgenda_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridAgenda.EditIndex = -1;
            gridAgenda.DataSource = Session["ListaAgenda"];
            gridAgenda.PageIndex = e.NewPageIndex;
            gridAgenda.DataBind();
            DesabilitaBotaoDeAcordoComAgenda();
        }

        protected void DesabilitaBotaoDeAcordoComAgenda()
        {
            IList<Agenda> agendas = (IList<Agenda>)Session["ListaAgenda"];
            int index = 0;
            if (agendas != null && agendas.Count != 0)
            {
                for (int i = gridAgenda.PageIndex * gridAgenda.PageSize; i < (10 * (gridAgenda.PageIndex + 1)); i++)
                {
                    if (index == 10)
                        index = 0;
                    if (i < agendas.Count)
                    {
                        //Agenda agenda = ;
                        switch (agendas[i].Publicada)
                        {
                            case true: //Pendente
                                LinkButton botao = (LinkButton)gridAgenda.Rows[index].Cells[7].FindControl("LinkButton_ConfirmarExcecucao");
                                if (botao != null)
                                    botao.Visible = false;
                                LinkButton botao1 = (LinkButton)gridAgenda.Rows[index].Cells[8].FindControl("cmdDelete");
                                if (botao1 != null)
                                    botao1.Visible = false;
                                break;
                        }
                    }
                    else
                        break;
                    index++;
                }
            }
        }

    }
}
