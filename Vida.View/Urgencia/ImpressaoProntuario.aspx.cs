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
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using System.Collections.Generic;

namespace ViverMais.View.Urgencia
{
    public partial class ImpressaoProntuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                int codigo = int.Parse(Request.QueryString["codigo"].ToString());
                ViverMais.Model.Prontuario prontuario = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.Prontuario>(codigo);
                Label_Prontuario.Text = prontuario.NumeroToString;
                Label_DataProntuario.Text = prontuario.Data.ToShortDateString();
                if (prontuario.Paciente != null && !string.IsNullOrEmpty(prontuario.Paciente.CodigoViverMais))
                {
                     ViverMais.Model.Paciente pacienteViverMais = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.Paciente>(prontuario.Paciente.CodigoViverMais);

                     Label_Nome.Text = pacienteViverMais.Nome;
                     Label_DataNascimento.Text = pacienteViverMais.DataNascimento.ToShortDateString();
                     Label_Sexo.Text = pacienteViverMais.Sexo.ToString();
                }
                else
                {
                    //tbxNomePaciente.Visible = true;
                    Label_Nome.Text = string.IsNullOrEmpty(prontuario.Paciente.Nome) ? "Não Identificado" : prontuario.Paciente.Nome;
                    //tbxIdade.Text = prontuario.Paciente.Idade.ToString();

                    if (prontuario.Paciente.Sexo != '\0')
                    {
                            Label_Sexo.Text = prontuario.Paciente.Sexo.ToString();
                    }
                }
                Label_SituacaoAtual.Text = prontuario.Situacao.Nome;
                Label_Idade.Text = prontuario.Idade.ToString();
                Label_FreqCardiaca.Text = prontuario.FrequenciaCardiaca;
                Label_FreqRespiratoria.Text = prontuario.FrequenciaRespiratoria;
                Label_TensaoArterial.Text = prontuario.TensaoArterial;
                Label_Temperatura.Text = prontuario.Temperatura;
                IProntuarioMedico iProntuario = Factory.GetInstance<IProntuarioMedico>();
                //IList<ViverMais.Model.ProntuarioMedico> prontuarioMedico = Factory.GetInstance<IProntuarioMedico>().
                IList<ViverMais.Model.ProntuarioMedico> pms = iProntuario.buscarPorProntuario<ViverMais.Model.ProntuarioMedico>(prontuario.Codigo);
                foreach (ViverMais.Model.ProntuarioMedico pm in pms)
                {
                    //Control ctl = LoadControl("~/Urgencia/WUCExibeDadosMedicos.ascx"); 
                    //WUCExibeDadosMedicos wuc = new WUCExibeDadosMedicos();
                    //Label lblObs = new Label();
                    //lblObs.Text = pm.Observacao;
                    //wuc.Observacao = lblObs;
                    //GridView gridCids = new GridView();
                    //gridCids.DataSource = pm.Cids;
                    //wuc.Cids = new GridView();
                    //wuc.DataSource = pm.Cids;
                    ////wuc.Cids.DataBind();
                    ////Label lblAprazamento = new Label();
                    ////lblAprazamento.Text = pm.Aprazamento;
                    ////wuc.Aprazamento = lblAprazamento;
                    //Panel_ProntuarioMedico.Controls.Add(wuc);
                    Label observacao = new Label();
                    observacao.Text = pm.Observacao;
                    GridView gridCids = new GridView();
                    gridCids.DataSource = pm.Cids;
                    gridCids.DataBind();
                    Panel_ProntuarioMedico.Controls.Add(observacao);
                    Panel_ProntuarioMedico.Controls.Add(gridCids);
                   }
            }
        }
    }
}
