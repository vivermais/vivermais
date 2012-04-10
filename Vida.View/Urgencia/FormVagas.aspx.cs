﻿using System;
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
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Urgencia
{
    public partial class FormVagas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CADASTRAR_NUMERO_VAGAS", Modulo.URGENCIA))
                    CarregaVagas(((Usuario)Session["Usuario"]).Unidade.CNES);
                else
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        /// <summary>
        /// Monta o quadro de vagas existentes na unidade
        /// </summary>
        /// <param name="co_unidade">código da unidade</param>
        private void CarregaVagas(string co_unidade)
        {
            DataTable tabela = new DataTable();
            tabela = Factory.GetInstance<IVagaUrgencia>().QuadroVagas(co_unidade, false);

            GridView_Vagas.DataSource = tabela;
            GridView_Vagas.DataBind();
        }

        /// <summary>
        /// Salva as vagas com as informações descritas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_SalvarVagas(object sender, EventArgs e)
        {
            IList<ViverMais.Model.VagaUrgencia> vagas = null;
            IVagaUrgencia iVaga = Factory.GetInstance<IVagaUrgencia>();
            IList<ViverMais.Model.VagaUrgencia> novasvagas = iVaga.Vagas<ViverMais.Model.VagaUrgencia>(int.Parse(TextBox_VagasFeminina.Text), int.Parse(TextBox_VagasMasculina.Text), int.Parse(TextBox_VagasInfantil.Text), ViewState["indexOfUnidade"].ToString());
            vagas = iVaga.BuscarPorUnidade<VagaUrgencia>(ViewState["indexOfUnidade"].ToString());

            try
            {
                iVaga.AtualizarVagas<ViverMais.Model.VagaUrgencia>(novasvagas, vagas, ViewState["indexOfUnidade"].ToString());
                //Colocar Log Aqui
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Vagas salvas com sucesso!');", true);
                CarregaVagas(ViewState["indexOfUnidade"].ToString());
                OnClick_Cancelar(sender, e);
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Cancela a ação de cadastro ou edição da vaga
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Cancelar(object sender, EventArgs e)
        {
            //ButtonSalvarVaga.CommandArgument = "salvar";
            ViewState.Remove("indexOfUnidade");
            //DropDownList_Unidade.SelectedValue = "0";
            TextBox_VagasInfantil.Text = "";
            TextBox_VagasMasculina.Text = "";
            TextBox_VagasFeminina.Text = "";
            Panel_Vagas.Visible = false;
        }

        /// <summary>
        /// Verifica se ação é de edição para a vaga
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCommand_Acao(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CommandName_editar")
            {
                string unidade = GridView_Vagas.DataKeys[int.Parse(e.CommandArgument.ToString())]["Unidade"].ToString();
                GridViewRow r = GridView_Vagas.Rows[int.Parse(e.CommandArgument.ToString())];

                //DropDownList_Unidade.SelectedValue = unidade;
                TextBox_VagasInfantil.Text = r.Cells[0].Text;
                TextBox_VagasMasculina.Text = r.Cells[1].Text;
                TextBox_VagasFeminina.Text = r.Cells[2].Text;
                ViewState["indexOfUnidade"] = unidade;
                Panel_Vagas.Visible = true;
                //ButtonSalvarVaga.CommandArgument = "editar";
            }
        }
    }
}
