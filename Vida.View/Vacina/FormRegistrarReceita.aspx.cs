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
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;

namespace ViverMais.View.Vacina
{
    public partial class FormRegistrarReceita : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "REGISTRAR_RECEITA", Modulo.VACINA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {

                    IList<ViverMais.Model.Vacina> vacinas = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<ViverMais.Model.Vacina>("Nome", true);
                    ddlVacina.Items.Add(new ListItem("Selecione...", "-1"));
                    foreach (var item in vacinas)
                    {
                        ddlVacina.Items.Add(new ListItem(item.Nome, item.Codigo.ToString()));
                    }

                    IList<ViverMais.Model.DoseVacina> doses = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<ViverMais.Model.DoseVacina>();
                    ddlDose.Items.Add(new ListItem("Selecione...", "-1"));
                    foreach (var item in doses)
                    {
                        ddlDose.Items.Add(new ListItem(item.Descricao, item.Codigo.ToString()));
                    }
                }
            }
            this.WUCPesquisarPaciente1.GridView.SelectedIndexChanged += new EventHandler(GridView_SelectedIndexChanged);
        }

        void GridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            WUCExibirPaciente1.Paciente = WUCPesquisarPaciente1.Paciente;
        }

        protected void imgBtnSalvar_Click(object sender, ImageClickEventArgs e)
        {

        }


    }
}
