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
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.View.Agendamento.RelatoriosCrystal;
using CrystalDecisions.CrystalReports.Engine;
using ViverMais.View.Agendamento.Helpers;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;

namespace ViverMais.View.Agendamento
{
    public partial class FormRelatorioProducaoMedicoRegulador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "RELATORIO_PRODUCAO_MEDICO_REGULADOR", Modulo.AGENDAMENTO))
                {
                    CarregaPerfisAgendamento();
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        protected void ddlPerfis_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMedicosReguladores(int.Parse(ddlPerfis.SelectedValue));
        }

        protected void btnGeraRelatorio_Click(object sender, EventArgs e)
        {
            DateTime data_inicial = DateTime.Parse(tbxData_Inicial.Text);
            DateTime data_final = DateTime.Parse(tbxData_Final.Text);
            int id_perfil = int.Parse(ddlPerfis.SelectedValue);
            Hashtable hash = Factory.GetInstance<IRelatorioAgendamento>().RelatorioProducaoMedicoRegulador(data_inicial, data_final, int.Parse(ddlMedicoRegulador.SelectedValue),id_perfil);

            if (hash.Count != 0)
            {
                Session["HashProducaoMedicoRegulador"] = hash;
                Redirector.Redirect("RelatorioProducaoMedicoRegulador.aspx", "_blank", "");
                Usuario usuario = (Usuario)(Session["Usuario"]);
                Factory.GetInstance<IAmbulatorial>().Salvar(new LogAgendamento(DateTime.Now, usuario.Codigo, 47, "NomeUsuario: " + usuario.Nome));
            }
        }

        /// <summary>
        /// Carrega Os Perfis do Módulo Agendamento
        /// </summary>
        protected void CarregaPerfisAgendamento()
        {
            IList<Perfil> perfisAgendamento = Factory.GetInstance<IPerfil>().BuscarPorModulo<Perfil>(Modulo.AGENDAMENTO);
            ddlPerfis.Items.Clear();
            ddlPerfis.Items.Add(new ListItem("Selecione...", "0"));
            foreach (Perfil perfil in perfisAgendamento)
            {
                ddlPerfis.Items.Add(new ListItem(perfil.Nome, perfil.Codigo.ToString()));
                if (perfil.Nome.ToUpper().IndexOf("REGULADOR") != -1)
                {
                    ddlPerfis.SelectedValue = perfil.Codigo.ToString();
                }
            }
            ddlPerfis_OnSelectedIndexChanged(new object(), new EventArgs());

            //ddlPerfis.DataSource = perfisAgendamento;
            //ddlPerfis.DataBind();
        }


        /// <summary>
        /// Carrega o DropDownList com os Usuários que possuem o Perfil Selecionado
        /// </summary>
        protected void CarregaMedicosReguladores(int id_perfil)
        {
            IList<Usuario> medicosReguladores = Factory.GetInstance<IUsuario>().ListarUsuariosPorPerfil<Usuario>(id_perfil);
            ddlMedicoRegulador.Items.Clear();
            ddlMedicoRegulador.Items.Add(new ListItem("Selecione...", "0"));
            foreach (Usuario usuario in medicosReguladores)
            {
                ddlMedicoRegulador.Items.Add(new ListItem(usuario.Nome, usuario.Codigo.ToString()));
            }
        }
    }
}
