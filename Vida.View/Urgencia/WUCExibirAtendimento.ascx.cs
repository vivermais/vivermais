using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;

namespace ViverMais.View.Urgencia
{
    public partial class WUCExibirAtendimento : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public Prontuario Prontuario
        {
            get
            {
                return (Session["prontuarioSelecionado"] != null && Session["prontuarioSelecionado"] is Prontuario) ? (Prontuario)Session["prontuarioSelecionado"] : null;
            }
            set
            {
                Session["prontuarioSelecionado"] = value;

                if (value != null)
                {
                    //IList<Prontuario> prontuarios = new List<Prontuario>();
                    //prontuarios.Add(value);
                    //DataTable tabela = Factory.GetInstance<IProntuario>().getDataTablePronturario<IList<Prontuario>>(prontuarios);

                    //this.Label_Paciente.Text = tabela.Rows[0]["NomePaciente"].ToString();
                    //this.Label_Data.Text = tabela.Rows[0]["DataProntuario"].ToString();
                    //this.Label_Numero.Text = tabela.Rows[0]["NumeroProntuario"].ToString();
                    //this.Label_Situacao.Text = tabela.Rows[0]["SituacaoEntrada"].ToString();
                    //this.TextBox_Descricao.Text = tabela.Rows[0]["PacienteDescricao"].ToString();

                    this.Label_Paciente.Text = value.NomePacienteToString;
                    this.Label_Data.Text = value.Data.ToString("dd/MM/yyyy HH:mm:ss");
                    this.Label_Numero.Text = value.NumeroToString;
                    this.Label_Situacao.Text = value.SituacaoEntradaPaciente;
                    this.TextBox_Descricao.Text = value.PacienteDescricao;
                }
            }
        }
    }
}