﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Farmacia
{
    public partial class FormUnidadeRecebimentoRM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "VINCULAR_UNIDADE_DISTRITO_RM", Modulo.FARMACIA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    //this.CarregaDropDownUnidade(this.CarregaUnidadeDistrito());

                    DropDownList_Distrito.DataSource = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<Distrito>().Where(p => p.Nome != "NÃO SE APLICA").OrderBy(p => p.Nome).ToList();
                    DropDownList_Distrito.DataBind();
                    DropDownList_Distrito.Items.Insert(0, new ListItem("Selecione...", "-1"));
                }
            }
        }

        /// <summary>
        /// Carrega os dropdown de unidades somente para aquelas unidades que não possuam ainda vínculo
        /// </summary>
        /// <param name="unidades"></param>
        //private void CarregaDropDownUnidade(IList<UnidadeDistritoRM> unidades)
        //{
        //    IList<ViverMais.Model.EstabelecimentoSaude> eas = Factory.GetInstance<IEstabelecimentoSaude>().ListarTodos<ViverMais.Model.EstabelecimentoSaude>().Where(p => p.NomeFantasia != "NÃO SE APLICA" && p.NomeFantasia != "NAO SE APLICA").ToList();
        //    IList<ViverMais.Model.EstabelecimentoSaude> unidadesdropdown = eas.Where(p => !unidades.Select(p2 => p2.CNESUnidade).ToList().Contains(p.CNES)).ToList().OrderBy(p => p.NomeFantasia).ToList();

        //    DropDownList_Unidade.DataSource = unidadesdropdown;
        //    DropDownList_Unidade.DataBind();
        //    DropDownList_Unidade.Items.Insert(0, new ListItem("Selecione...", "-1"));
        //}

        /// <summary>
        /// Carrega todas as relações entre unidade e distrito
        /// </summary>
        /// <returns>Lista de vínculos</returns>
        //private IList<UnidadeDistritoRM> CarregaUnidadeDistrito()
        //{
        //    IList<UnidadeDistritoRM> unidades = Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<UnidadeDistritoRM>().OrderBy(p => p.NomeUnidade).ToList();
        //    GridView_UnidadeDistrito.DataSource = unidades;
        //    GridView_UnidadeDistrito.DataBind();

        //    return unidades;
        //}

        protected void OnSelectedIndexChanged_DropDownList_Distrito(object sender, EventArgs e)
        {
            IList<ViverMais.Model.EstabelecimentoSaude> eas = Factory.GetInstance<IEstabelecimentoSaude>().ListarTodos<ViverMais.Model.EstabelecimentoSaude>()
                .Where(p => p.NomeFantasia != "NÃO SE APLICA" && p.NomeFantasia != "NAO SE APLICA"
                    && p.Bairro != null && p.Bairro.Distrito != null && p.Bairro.Distrito.Codigo.ToString() == DropDownList_Distrito.SelectedValue)
                    .OrderBy(p=>p.NomeFantasia).ToList();

            //IList<UnidadeDistritoRM> unidades = Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<UnidadeDistritoRM>()
            //    .Where(p=>p.Distrito. .OrderBy(p => p.NomeUnidade).ToList();
            GridView_UnidadeDistrito.DataSource = eas;
            GridView_UnidadeDistrito.DataBind();
            Panel_Resultado.Visible = true;
        }


        /// <summary>
        /// Paginação do gridview para as relações entre unidade e distrito
        /// no que diz respeito ao recebimento das rm's solicitadas pelas farmácias
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void OnPageIndexChanging_UnidadeDistrito(object sender, GridViewPageEventArgs e)
        //{
        //    this.CarregaUnidadeDistrito();
        //    GridView_UnidadeDistrito.PageIndex = e.NewPageIndex;
        //    GridView_UnidadeDistrito.DataBind();
        //}

        /// <summary>
        /// Exclui a relação entre a unidade e o distrito para recebimento da RM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void OnRowDeleting_UnidadeDistrito(object sender, GridViewDeleteEventArgs e)
        //{
        //    IFarmaciaServiceFacade iFarmacia = Factory.GetInstance<IFarmaciaServiceFacade>();

        //    int codigo = int.Parse(GridView_UnidadeDistrito.DataKeys[e.RowIndex]["Codigo"].ToString());
        //    UnidadeDistritoRM unidadedistrito = Factory.GetInstance<IFarmacia>().BuscarPorCodigo<ViverMais.Model.UnidadeDistritoRM>(codigo);
        //    iFarmacia.Deletar(unidadedistrito);
        //    iFarmacia.Salvar(new LogFarmacia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, EventoFarmacia.EXCLUIR_VINCULAR_UNIDADE_DISTRITO,
        //        "unidade: " + unidadedistrito.Unidade.NomeFantasia + ", distrito: " + unidadedistrito.Distrito.Nome));

        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Vínculo excluído com sucesso.');", true);
        //    this.CarregaDropDownUnidade(this.CarregaUnidadeDistrito());
        //}

        /// <summary>
        /// Realiza o vínculo entre a unidade e distrito
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void OnClick_VincularUnidadeDistrito(object sender, EventArgs e)
        //{
        //    IFarmaciaServiceFacade iFarmacia = Factory.GetInstance<IFarmaciaServiceFacade>();
        //    UnidadeDistritoRM unidadedistrito = new UnidadeDistritoRM();
        //    unidadedistrito.Unidade = Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(DropDownList_Unidade.SelectedValue);
        //    unidadedistrito.Distrito = Factory.GetInstance<IDistrito>().BuscarPorCodigo<Distrito>(int.Parse(DropDownList_Distrito.SelectedValue));

        //    iFarmacia.Salvar(unidadedistrito);
        //    iFarmacia.Salvar(new LogFarmacia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, EventoFarmacia.VINCULAR_UNIDADE_DISTRITO,
        //        "unidade: " + unidadedistrito.Unidade.NomeFantasia + ", distrito: " + unidadedistrito.Distrito.Nome));
        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Vínculo realizado com sucesso.');", true);
        //    this.CarregaDropDownUnidade(this.CarregaUnidadeDistrito());
        //    DropDownList_Distrito.SelectedValue = "-1";
        //}

        /// <summary>
        /// Escolha default do distrito vinculado a unidade escolhida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void OnSelectedIndexChanged_Distrito(object sender, EventArgs e)
        //{
        //    if (DropDownList_Unidade.SelectedValue != "-1")
        //    {
        //        ViverMais.Model.EstabelecimentoSaude unidade = Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(DropDownList_Unidade.SelectedValue);
        //        DropDownList_Distrito.SelectedValue = unidade.Bairro != null ? unidade.Bairro.Distrito.Codigo.ToString() : "-1";
        //    }
        //}

    }
}