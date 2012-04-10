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

namespace ViverMais.View.Paciente
{
    public partial class WUCPesquisarPaciente : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //lblMensagem.Visible = false;
        }

        public string WUC_CartaoSUSPesquisado
        {
            get
            {
                return ViewState["cartaosus"].ToString();
            }
        }

        public string WUC_PacientePesquisado
        {
            get
            {
                return ViewState["paciente"].ToString();
            }
        }

        public string WUC_MaePesquisado
        {
            get
            {
                return ViewState["mae"].ToString();
            }
        }

        public string WUC_DataNascimentoPesquisado
        {
            get
            {
                return ViewState["datanascimento"].ToString();
            }
        }

        public ImageButton WUC_BotaoPesquisar
        {
            get
            {
                return this.btnPesquisar;
            }
        }

        public LinkButton WUC_BotaoBiometria
        {
            get
            {
                return this.lnkBiometria;
            }
        }

        public HtmlImage WUC_ImagemBiometria
        {
            get { return this.img_biometria; }
        }

        public UpdatePanel WUC_UpdatePanel_ResultadoPesquisa
        {
            get { return this.UpdatePanel_ResultadoPesquisa; }
        }

        public ViverMais.Model.Paciente Paciente
        {
            get
            {
                return Session["WUCPacienteSelecionado"] != null ? (ViverMais.Model.Paciente)Session["WUCPacienteSelecionado"] : null;
            }
            //set;
        }

        public GridView GridView
        {
            get
            {
                return this.GridViewPacientes;
            }
            //set
            //{
            //    this.GridViewPacientes = value;
            //}
        }

        protected void GridViewPacientes_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            if (Session["WUCPacientes"] == null)
                return;
            int index = e.NewSelectedIndex;
            IList<ViverMais.Model.Paciente> pacientes = (IList<ViverMais.Model.Paciente>)Session["WUCPacientes"];
            ViverMais.Model.Paciente paciente = pacientes[index];
            Session["WUCPacienteSelecionado"] = paciente;
            GridViewPacientes.SelectedRowStyle.BackColor = Color.LightGray;
        }

        protected void btnPesquisar_Click1(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid)
                return;
            IPaciente ipaciente = Factory.GetInstance<IPaciente>();
            IList<ViverMais.Model.Paciente> pacientes = new List<ViverMais.Model.Paciente>();

            if (!string.IsNullOrEmpty(tbxCartaoSUS.Text))
            {
                ViverMais.Model.Paciente paciente = ipaciente.PesquisarPacientePorCNS<ViverMais.Model.Paciente>(tbxCartaoSUS.Text);
                if (paciente != null)
                {
                    pacientes.Add(paciente);
                }
            }
            else
            {
                pacientes = ipaciente.PesquisarPaciente<ViverMais.Model.Paciente>(tbxNome.Text, tbxNomeMae.Text, string.IsNullOrEmpty(tbxDataNascimento.Text) ? DateTime.MinValue : DateTime.Parse(tbxDataNascimento.Text));
            }

            //if (pacientes.Count == 0)
            //    lblMensagem.Visible = true;

            GridViewPacientes.Visible = true;
            GridViewPacientes.SelectedIndex = -1;
            Session["WUCPacientes"] = pacientes;
            GridViewPacientes.DataSource = pacientes;
            GridViewPacientes.DataBind();

            ViewState["paciente"] = tbxNome.Text; tbxNome.Text = string.Empty;
            ViewState["mae"] = tbxNomeMae.Text; tbxNomeMae.Text = string.Empty;
            ViewState["datanascimento"] = tbxDataNascimento.Text; tbxDataNascimento.Text = string.Empty;
            ViewState["cartaosus"] = tbxCartaoSUS.Text; tbxCartaoSUS.Text = string.Empty;
        }

        protected void CustomValidatorNomeMae_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (!string.IsNullOrEmpty(tbxCartaoSUS.Text))
                return;
            if (tbxNomeMae.Text != string.Empty && tbxNomeMae.Text.Split(' ').Length < 2)
                args.IsValid = false;
        }

        protected void CustomValidatorNomePaciente_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (!string.IsNullOrEmpty(tbxCartaoSUS.Text))
                return;
            if (tbxNome.Text != string.Empty && tbxNome.Text.Split(' ').Length < 2)
                args.IsValid = false;
        }

        protected void CustomValidatorRequired_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (tbxNome.Text == string.Empty && tbxCartaoSUS.Text == string.Empty)
                args.IsValid = false;
        }

        protected void CustomValidatorCampos_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (!string.IsNullOrEmpty(tbxCartaoSUS.Text))
                return;
            if (tbxNome.Text != string.Empty && tbxNomeMae.Text == string.Empty && tbxDataNascimento.Text == string.Empty)
                args.IsValid = false;
        }
    }
}