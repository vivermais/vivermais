using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.DAO;
using System.Drawing;
using ViverMais.Model;

namespace ViverMais.View.Paciente
{
    public partial class WUCExibirPaciente : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public Panel WUC_PanelDadosPaciente
        {
            get
            {
                return this.Panel_DadosPacientes;
            }
        }

        public UpdatePanel WUC_UpdatePanelExibirPaciente
        {
            get { return this.UpdatePanel_ExibirPaciente; }
        }

        public ViverMais.Model.Paciente Paciente
        {
            get
            {
                return Session["WUCExibePaciente"] != null ? (ViverMais.Model.Paciente)Session["WUCExibePaciente"] : null;
            }
            set
            {
                Session["WUCExibePaciente"] = value;

                if (value != null)
                {
                    lblNomeWUC.Text = value.Nome;
                    lblNomeMaeWUC.Text = value.NomeMae;
                    lblDataNascimentoWUC.Text = value.DataNascimento.ToString("dd/MM/yyyy");
                    IList<CartaoSUS> cartoes = BLL.CartaoSUSBLL.ListarPorPaciente(value);
                    lblCartaoSUSWUC.Text = cartoes.Select(p => long.Parse(p.Numero)).Min().ToString();
                    this.Panel_DadosPacientes.Visible = true;
                    this.UpdatePanel_ExibirPaciente.Update();
                }
            }
        }


    }
}