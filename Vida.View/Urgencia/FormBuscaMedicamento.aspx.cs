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
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;

namespace ViverMais.View.Urgencia
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_MEDICAMENTO_PRESCRICAO", Modulo.URGENCIA))
                {
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                }
            }

        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            
            //IList<Medicamento> medicamentos = Factory.GetInstance<IMedicamento>().ListarTodos<Medicamento>();
            IList<Medicamento> medicamentos = Factory.GetInstance<IMedicamento>().BuscarPorDescricao<Medicamento>(-1,tbxMedicamento.Text, false);
            gridMedicamento.DataSource = medicamentos;
            gridMedicamento.DataBind();
            Panel_ResultadoBusca.Visible = true;
        }

        protected void gridMedicamento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if( e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].Text == "Sim") 
                {
                    ((LinkButton)e.Row.Cells[2].Controls[0]).Enabled = false;
                }
            }
        }

        protected void GridMedicamento_RowCommand(object sender, GridViewCommandEventArgs e) 
        {
            string co_medicamento = gridMedicamento.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString();
            Response.Redirect("FormMedicamento.aspx?codigo="+co_medicamento);
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormMedicamento.aspx");
        }
    }
}
