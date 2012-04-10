﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using System.Text.RegularExpressions;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Farmacia
{
    public partial class FormBuscaMedicamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_MEDICAMENTO", Modulo.FARMACIA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                    CarregaMedicamentos();
                //IUnidadeMedidaMedicamento ium       = Factory.GetInstance<IUnidadeMedidaMedicamento>();
                //IList<UnidadeMedidaMedicamento> lum = ium.ListarTodos<UnidadeMedidaMedicamento>().OrderBy(p => p.Nome).ToList();

                //foreach (UnidadeMedidaMedicamento um in lum)
                //    DropDownList_UnidadeMedida.Items.Add(new ListItem(um.Nome, um.Codigo.ToString()));
            }
        }

        /// <summary>
        /// Carrega os medicamentos cadastrados
        /// </summary>
        private void CarregaMedicamentos()
        {
            GridView_Medicamento.DataSource = Factory.GetInstance<IMedicamento>().ListarTodosMedicamentos<Medicamento>().OrderBy(p => p.Nome).ToList();
            GridView_Medicamento.DataBind();
        }

        /// <summary>
        /// Pesquisa os medicamentos de acordo com a descrição solicitada pelo usuário
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Pesquisar(object sender, EventArgs e)
        {
            //Button bt       = (Button)sender;
            //bool ok         = true;
            //IMedicamento im = Factory.GetInstance<IMedicamento>();

            //IList<Medicamento> lm = new List<Medicamento>();

            //ViewState["tipo_pesquisa"] = bt.CommandArgument;

            //if (bt.CommandArgument == "todos")
            //    lm = im.ListarTodosMedicamentos<Medicamento>().OrderBy(p => p.Nome).ToList();
            //else
            //{
            //    if (!CustomValidator_Pesquisa.IsValid)
            //        ok = false;
            //    else
            //    {
            //        ViewState["co_unidademedida"] = DropDownList_UnidadeMedida.SelectedValue;
            //        ViewState["medicamento"]      = TextBox_Nome.Text;
            //        lm = im.BuscarPorDescricao<Medicamento>(int.Parse(DropDownList_UnidadeMedida.SelectedValue), TextBox_Nome.Text);
            //    }
            //}

            //if (ok)
            //{
            //    GridView_Medicamento.DataSource = lm;
            //    GridView_Medicamento.DataBind();

            //    Panel_Resultado.Visible = true;
            //}
            //else
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + ViewState["msgValidation"].ToString() + "');", true);
        }

        /// <summary>
        /// Formata o gridview de medicamentos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridView(object sender, GridViewRowEventArgs e) 
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Cells[3].Text = bool.Parse(e.Row.Cells[3].Text) ? "Sim" : "Não";
        }

        /// <summary>
        /// Paginação do gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e) 
        {
            //if (ViewState["tipo_pesquisa"].ToString() == "todos")
            //    GridView_Medicamento.DataSource = Factory.GetInstance<IMedicamento>().ListarTodos<Medicamento>().OrderBy(p => p.Nome).ToList();
            //else
            //    GridView_Medicamento.DataSource = Factory.GetInstance<IMedicamento>().BuscarPorDescricao<Medicamento>(int.Parse(ViewState["co_unidademedida"].ToString()), ViewState["medicamento"].ToString());
            CarregaMedicamentos();
            GridView_Medicamento.PageIndex = e.NewPageIndex;
            GridView_Medicamento.DataBind();
        }

        /// <summary>
        /// Valida a pesquisa de medicamentos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnServerValidate_ValidaPesquisa(object sender, ServerValidateEventArgs e) 
        {
            //Regex regex = new Regex(@"^[\S]{3}[\w\W]*$");
            //e.IsValid = true;

            //if (string.IsNullOrEmpty(TextBox_Nome.Text) && DropDownList_UnidadeMedida.SelectedValue == "-1")
            //{
            //    ViewState["msgValidation"] = "Informe o inicio do nome do medicamento e/ou a sua unidade de medida!";
            //    e.IsValid = false;
            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(TextBox_Nome.Text) && !regex.IsMatch(TextBox_Nome.Text))
            //    {
            //        ViewState["msgValidation"] = "O medicamento deve iniciar com pelo menos três caracteres!";
            //        e.IsValid = false;
            //    }
            //}
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<Medicamento> medicamentos;
            if (!string.IsNullOrEmpty(txtNomeMedicamento.Text.Trim()) || !string.IsNullOrEmpty(txtCodigoMedicamento.Text.Trim()))
            {
                medicamentos = Factory.GetInstance<IMedicamento>().BuscarPorDescricao<Medicamento>(txtCodigoMedicamento.Text, txtNomeMedicamento.Text).ToList();
                if (medicamentos != null || medicamentos.Count != 0)
                {                   
                    GridView_Medicamento.DataSource = medicamentos;
                    GridView_Medicamento.DataBind();
                }
                else
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alerta", "alert('Não foram encontrados medicamentos para os dados informados.');", true);
            }
            else
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alerta", "alert('Favor informar dados para a busca.');", true);


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            CarregaMedicamentos();
        }
    }
}
