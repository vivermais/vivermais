using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;

namespace ViverMais.View.Urgencia
{
    public partial class FormMostrarHistoricoProntuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViverMais.Model.Prontuario prontuario = Factory.GetInstance<IProntuario>().BuscarPorCodigo<ViverMais.Model.Prontuario>(long.Parse(Request["codigo"].ToString()));

                if (prontuario != null)
                {
                    lblNumero.Text = prontuario.NumeroToString;
                    lblData.Text = prontuario.Data.ToString("dd/MM/yyyy");
                    lblPaciente.Text = prontuario.NomePacienteToString;
                }
            }
        }
    }
}
