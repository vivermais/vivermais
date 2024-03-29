﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Urgencia
{
    public partial class FormItemPA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CADASTRAR_ITEM_PA", Modulo.URGENCIA))
                {
                    if (Request["co_item"] != null)
                    {
                        ViewState["co_itemPA"] = Request["co_item"].ToString();

                        ItemPA pa = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ItemPA>(int.Parse(ViewState["co_itemPA"].ToString()));
                        tbxNome.Text = pa.Nome;
                        tbxCodigoSIGTAP.Text = pa.CodigoSIGTAP;

                        if (pa.Status == ItemPA.INATIVO)
                        {
                            RadioButton_Inativo.Checked = true;
                            RadioButton_Ativo.Checked = false;
                        }
                        else
                        {
                            RadioButton_Inativo.Checked = false;
                            RadioButton_Ativo.Checked = true;
                        }
                    }
                }
                else
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        ///// <summary>
        ///// Atualiza a lista de itens do pronto atendimento
        ///// </summary>
        //void AtualizaItens()
        //{
        //    IList<ViverMais.Model.ItemPA> itens = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ViverMais.Model.ItemPA>();
        //    gridItens.DataSource = itens;
        //    gridItens.DataBind();
        //}

        /// <summary>
        /// Cadastra/Atualiza um item do PA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            IItemPA iUrgencia = Factory.GetInstance<IItemPA>();
            ViverMais.Model.ItemPA item = ViewState["co_itemPA"] != null ? iUrgencia.BuscarPorCodigo<ViverMais.Model.ItemPA>(int.Parse(ViewState["co_itemPA"].ToString())) : new ViverMais.Model.ItemPA();

            item.CodigoSIGTAP = tbxCodigoSIGTAP.Text;
            item.Nome = tbxNome.Text;
            
            item.Status = RadioButton_Ativo.Checked ? ItemPA.ATIVO : ItemPA.INATIVO;

            if (iUrgencia.ValidaCadastroPorCodigoSIGTAP<ItemPA>(tbxCodigoSIGTAP.Text, item.Codigo))
            {
                try
                {
                    iUrgencia.Salvar(item);
                    iUrgencia.Inserir(new LogUrgencia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 14, "id item:" + item.Codigo));

                    //OnClick_Cancelar(sender, e);
                    //AtualizaItens();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Item salvo com sucesso!');location='FormExibeItem.aspx';", true);
                }
                catch (Exception f)
                {
                    throw f;
                }
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe um item com o código informado! Por favor, utilize outro código.');", true);
        }

        ///// <summary>
        ///// Paginação do gridview
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        //{
        //    AtualizaItens();
        //    gridItens.PageIndex = e.NewPageIndex;
        //    gridItens.DataBind();
        //}

        ///// <summary>
        ///// Cancela o cadastro/edição do item do PA
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void OnClick_Cancelar(object sender, EventArgs e)
        //{
        //    ViewState.Remove("co_itemPA");
        //    tbxNome.Text = "";
        //    tbxCodigoSIGTAP.Text = "";
        //    RadioButton_Ativo.Checked = true;
        //    RadioButton_Inativo.Checked = false;
        //}

        ///// <summary>
        ///// Seleciona as informações necessárias do item para edição
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void gridItens_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ItemPA pa = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ItemPA>(int.Parse(gridItens.DataKeys[gridItens.SelectedIndex]["Codigo"].ToString()));
        //    tbxNome.Text = pa.Nome;
        //    tbxCodigoSIGTAP.Text = pa.CodigoSIGTAP;

        //    if (pa.Status == ItemPA.INATIVO)
        //    {
        //        RadioButton_Inativo.Checked = true;
        //        RadioButton_Ativo.Checked = false;
        //    }
        //    else
        //    {
        //        RadioButton_Inativo.Checked = false;
        //        RadioButton_Ativo.Checked = true;
        //    }

        //    ViewState["co_itemPA"] = int.Parse(gridItens.DataKeys[gridItens.SelectedIndex]["Codigo"].ToString());
        //}
    }
}
