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
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.DAO;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;
using ViverMais.BLL;

namespace ViverMais.View.Paciente
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
                bool Cadastrar_Cartao = iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "CADASTRAR_CARTAO_SUS", Modulo.CARTAO_SUS);
                if (!Cadastrar_Cartao && !iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "ALTERAR_CARTAO_SUS", Modulo.CARTAO_SUS))
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Você não tem permissão para acessar essa página. Em caso de dúViverMais, entre em contato.');window.location='../Home.aspx';</script>");
                }
                Session["PermissaoCadastrarCartao"] = Cadastrar_Cartao;

            }
            //lblMensagem.Visible = false;
        }

        protected void btnPesquisar_Click1(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid)
                return;
            Panel_Cadastrar_Cartao.Visible = false;
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
                if (tbxNome.Text.Trim() != string.Empty && (tbxNomeMae.Text.Trim() == string.Empty && tbxDataNascimento.Text.Trim() == string.Empty)
                  || tbxNomeMae.Text.Trim() != string.Empty && (tbxNome.Text.Trim() == string.Empty && tbxDataNascimento.Text.Trim() == string.Empty)
                  || tbxDataNascimento.Text.Trim() != string.Empty && (tbxNome.Text.Trim() == string.Empty && tbxNomeMae.Text.Trim() == string.Empty)
                )
                {
                    ClientScript.RegisterClientScriptBlock(Page.GetType(), "alerta", "alert('Favor informar ao menos dois campos para a busca.');", true);
                    return;
                }
                pacientes = ipaciente.PesquisarPaciente<ViverMais.Model.Paciente>(tbxNome.Text.ToUpper(), tbxNomeMae.Text.ToUpper(), string.IsNullOrEmpty(tbxDataNascimento.Text) ? DateTime.MinValue : DateTime.Parse(tbxDataNascimento.Text));
            }

            if (pacientes.Count == 0 && (bool)Session["PermissaoCadastrarCartao"])
                Panel_Cadastrar_Cartao.Visible = true;

            Session["WUCPacientes"] = pacientes;
            GridViewPacientes.DataSource = pacientes;
            GridViewPacientes.DataBind();
            tbxNome.Text = string.Empty;
            tbxNomeMae.Text = string.Empty;
            tbxDataNascimento.Text = string.Empty;
            tbxCartaoSUS.Text = string.Empty;
        }

        protected void OnRowDataBound_Pacientes(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                IList<ViverMais.Model.Paciente> pacientes = (IList<ViverMais.Model.Paciente>)Session["WUCPacientes"];
                ControlePaciente controlepaciente = ControlePacienteBLL.PesquisarPorPaciente(
                    pacientes.Where(p=>p.Codigo ==
                    this.GridViewPacientes.DataKeys[e.Row.RowIndex]["Codigo"].ToString()).First());

                ((Label)e.Row.FindControl("LabelUltimaAtualizacao")).Text = controlepaciente != null ? controlepaciente.DataOperacao.ToString("dd/MM/yyyy HH:mm")
                    : "Não Informado";
            }
        }

        protected void CustomValidatorNomePaciente_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (!string.IsNullOrEmpty(tbxCartaoSUS.Text))
                return;
            if (tbxNome.Text != string.Empty && tbxNome.Text.Split(' ').Length < 2)
            {
                args.IsValid = false;
            }
        }

        protected void CustomValidatorNomeMae_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (!string.IsNullOrEmpty(tbxCartaoSUS.Text))
                return;
            if (tbxNomeMae.Text != string.Empty && tbxNomeMae.Text.Split(' ').Length < 2)
            {
                args.IsValid = false;
            }
        }

        protected void CustomValidatorCampos_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (!string.IsNullOrEmpty(tbxCartaoSUS.Text))
                return;
            if (tbxNome.Text != string.Empty && tbxNomeMae.Text == string.Empty && tbxDataNascimento.Text == string.Empty)
            {
                args.IsValid = false;
            }
        }

        protected void CustomValidatorRequired_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (tbxNome.Text == string.Empty && tbxNomeMae.Text == string.Empty && tbxDataNascimento.Text == string.Empty && tbxCartaoSUS.Text == string.Empty)
                args.IsValid = false;
        }

        protected void Accordion1_ItemCommand(object sender, CommandEventArgs e)
        {
            tbxNome.Text = string.Empty;
            tbxNomeMae.Text = string.Empty;
            tbxDataNascimento.Text = string.Empty;
            tbxCartaoSUS.Text = string.Empty;
        }
    }
}
