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
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.DAO;
using System.Collections.Generic;
using System.Drawing;
using ViverMais.Model;

namespace ViverMais.View.Vacina
{
    public partial class FormPesquisaCartaoVacina : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // WUCPesquisarPaciente1.GridView.SelectedIndexChanged += new EventHandler(GridView_SelectedIndexChanged);
            if (!IsPostBack)
            {
                // WUCExibirPaciente1.Visible = false;
                PanelImprimeCartao.Visible = false;
            }
        }

        protected void LnkPesquisarClick(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;
            IPaciente ipaciente = Factory.GetInstance<IPaciente>();
            IList<ViverMais.Model.Paciente> pacientes = new List<ViverMais.Model.Paciente>();

            if (!string.IsNullOrEmpty(TextBox_CartaoSUS.Text))
            {
                ViverMais.Model.Paciente paciente = ipaciente.PesquisarPacientePorCNS<ViverMais.Model.Paciente>(TextBox_CartaoSUS.Text);
                if (paciente != null)
                {
                    if (paciente.DataNascimento != DateTime.Parse(TextBox_DataNascimento.Text))
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Nenhum resultado foi encontrado para os dados informados! Verifique se estes estão corretos.');", true);
                        return;
                    }
                    else
                    pacientes.Add(paciente);
                }
            }


            //if (pacientes.Count == 0)
            //    lblMensagem.Visible = true;

            Session["WUCPacientes"] = pacientes;
            GridViewPacientes.DataSource = pacientes;
            GridViewPacientes.DataBind();
            TextBox_CartaoSUS.Text = string.Empty;
        }

        protected void GridViewPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["WUCPacientes"] == null)
                return;
            int index = GridViewPacientes.SelectedIndex;
            IList<ViverMais.Model.Paciente> pacientes = (IList<ViverMais.Model.Paciente>)Session["WUCPacientes"];
            ViverMais.Model.Paciente paciente = pacientes[index];
            Session["pacienteSelecionado"] = paciente;
            IList<CartaoSUS> cartoes = Factory.GetInstance<IPaciente>().ListarCartoesSUS<CartaoSUS>(paciente.Codigo);
            lblCartaoSUS.Text = (from c in cartoes select long.Parse(c.Numero)).Min().ToString();//cartoes[cartoes.Count - 1].Numero;
            lblDataNascimento.Text = paciente.DataNascimento.ToString("dd/MM/yyyy");
            lblNome.Text = paciente.Nome;
            lblNomeMae.Text = paciente.NomeMae;
            GridViewPacientes.SelectedRowStyle.BackColor = Color.LightGray;
            PanelImprimeCartao.Visible = true;
        }


        //void GridView_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ViverMais.Model.Paciente paciente = WUCPesquisarPaciente1.Paciente;
        //    Session["pacienteSelecionado"] = paciente;
        //    WUCExibirPaciente1.Paciente = paciente;
        //    WUCExibirPaciente1.Visible = true;
        //    PanelImprimeCartao.Visible = true;
        //}

        protected void btnCartaoVacina_Click(object sender, EventArgs e)
        {
            if (Session["pacienteSelecionado"] != null)
                Response.Redirect("FormExibeCartaoVacina.aspx");

        }

        public void Redirect(string url, string target, string windowFeatures)
        {
            HttpContext context = HttpContext.Current;
            if ((String.IsNullOrEmpty(target) || target.Equals("_self", StringComparison.OrdinalIgnoreCase))
            && String.IsNullOrEmpty(windowFeatures))
            {
                context.Response.Redirect(url);
            }
            else
            {
                Page page = (Page)context.Handler;
                if (page == null)
                {
                    throw new InvalidOperationException("Cannot redirect to new window outside Page context.");
                }
                url = page.ResolveClientUrl(url);
                string script = string.Empty;
                if (String.IsNullOrEmpty(windowFeatures))
                {
                    script = @"window.open(""{0}"", ""{1}"");";
                }

                script = String.Format(script, url, target, windowFeatures);
                ScriptManager.RegisterStartupScript(page, typeof(Page), "Redirect", script, true);
            }
        }
    }
}
