﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Inventario;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Farmacia
{
    public partial class FormItensRemanejamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "RECEBER_REMANEJAMENTO", Modulo.FARMACIA))
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                    return;
                }
                else
                {
                    int temp;

                    if (Request["co_remanejamento"] != null && int.TryParse(Request["co_remanejamento"].ToString(), out temp))
                    {
                        ViewState["co_remanejamento"] = Request["co_remanejamento"].ToString();
                        RemanejamentoMedicamento r = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<RemanejamentoMedicamento>(int.Parse(ViewState["co_remanejamento"].ToString()));

                        if (r.Status == RemanejamentoMedicamento.FECHADO)
                            Button_Finalizar.Visible = false;

                        Label_DataMovimento.Text = r.Movimento.Data.ToString("dd/MM/yyyy");
                        Label_FarmaciaMovimento.Text = r.Movimento.Farmacia.Nome;
                        Label_DataEnvioMovimento.Text = r.Movimento.Data_Envio.Value.ToString("dd/MM/yyyy");
                        Label_DataRecebMovimento.Text = r.Movimento.Data_Recebimento.Value.ToString("dd/MM/yyyy");
                        Label_RespEnvMovimento.Text = r.Movimento.Responsavel_Envio;
                        Label_RespRecebMovimento.Text = r.Movimento.Responsavel_Recebimento;

                        CarregaItemRemanejamento(int.Parse(Request["co_remanejamento"].ToString()));
                    }
                }
            }
        }

        /// <summary>
        /// Carrega os itens do remanejamento
        /// </summary>
        /// <param name="co_remanejamento"></param>
        private void CarregaItemRemanejamento(int co_remanejamento)
        {
            IList<ItemRemanejamento> lir = Factory.GetInstance<IMovimentacao>().BuscarItensRemanejamentoPorRemanejamento<ItemRemanejamento>(co_remanejamento);
            GridView_ItensRemanejamento.DataSource = lir;
            GridView_ItensRemanejamento.DataBind();

            if (QuantidadeRegistrosNaoConfirmados(co_remanejamento) == 0)
                Button_Finalizar.OnClientClick = "javascript:return confirm('Tem certeza que deseja finalizar este remanejamento ?');";
            else
                Button_Finalizar.OnClientClick = "javascript:alert('Usuário, ainda existe itens não confirmados! Por favor, confirme todos itens antes de finalizar o remanejamento.');return false;";
        }

        /// <summary>
        /// Retorna a quantidade de registros não confirmados o recebimento
        /// </summary>
        /// <param name="co_remanejamento"></param>
        /// <returns></returns>
        private int QuantidadeRegistrosNaoConfirmados(int co_remanejamento)
        {
            return Factory.GetInstance<IMovimentacao>().BuscarItensRemanejamentoPorRemanejamento<ItemRemanejamento>(co_remanejamento).Where(p => !p.DataConfirmacao.HasValue).ToList().Count;
        }

        /// <summary>
        /// Paginação do gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            CarregaItemRemanejamento(int.Parse(ViewState["co_remanejamento"].ToString()));
            GridView_ItensRemanejamento.PageIndex = e.NewPageIndex;
            GridView_ItensRemanejamento.DataBind();
        }

        /// <summary>
        /// Habilita a edição do item do remanejamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowEditing_EditarItem(object sender, GridViewEditEventArgs e)
        {
            GridView_ItensRemanejamento.EditIndex = e.NewEditIndex;
            CarregaItemRemanejamento(int.Parse(ViewState["co_remanejamento"].ToString()));
        }

        /// <summary>
        /// Cancela a edição do item do remanejamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCancelingEdit_CancelarEdicaoItem(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_ItensRemanejamento.EditIndex = -1;
            CarregaItemRemanejamento(int.Parse(ViewState["co_remanejamento"].ToString()));
        }

        /// <summary>
        /// Recebe e atualiza estoque da farmácia com item do remanejamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowUpdating_AlterarItem(object sender, GridViewUpdateEventArgs e)
        {
            if(InventarioAberto())
                return;

            IMovimentacao iMovimentacao = Factory.GetInstance<IMovimentacao>();
            ItemRemanejamento itemRemanejamento = iMovimentacao.BuscarItensRemanejamentoPorRemanejamento<ItemRemanejamento>(int.Parse(ViewState["co_remanejamento"].ToString())).Where(p => p.LoteMedicamento.Codigo == int.Parse(GridView_ItensRemanejamento.DataKeys[e.RowIndex]["CodigoLote"].ToString())).First();

            //if (Factory.GetInstance<IInventario>().BuscarPorSituacao<Inventario>('A', ir.Remanejamento.Movimento.Farmacia_Destino.Codigo).Count != 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A confirmação de recebimento deste item não pode ser concluída, pois existe um inventário ABERTO para a farmácia deste remanejamento que deve ser encerrado.');", true);
            //    return;
            //}

            GridViewRow rowGrid = GridView_ItensRemanejamento.Rows[e.RowIndex];
            TextBox tbxQuantidadeRecebida = (TextBox)rowGrid.FindControl("TextBox_QuantidadeRecebida");

            //if (ir.DataConfirmacao.HasValue) //Alteração
            //{
            //    ir.DataAlteracao = DateTime.Now;
            //    ir.QuantidadeAlterada = int.Parse(tbx.Text);
            //}
            //else
            //{
                itemRemanejamento.QuantidadeRecebida = int.Parse(tbxQuantidadeRecebida.Text);
                itemRemanejamento.DataConfirmacao = DateTime.Now;
            //}

            try
            {
                iMovimentacao.ConfirmarRecebimentoMedicamento<ItemRemanejamento>(itemRemanejamento);
                iMovimentacao.Salvar(new LogFarmacia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, EventoFarmacia.CONFIRMAR_RECEBIMENTO_ITEM_REMANEJAMENTO,
                    "id remanejamento: " + itemRemanejamento.Remanejamento.Codigo + " id lote medicamento: " + itemRemanejamento.LoteMedicamento.Codigo));

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Confirmação de medicamento recebido! Estoque da farmácia atualizado com sucesso.');", true);
                GridView_ItensRemanejamento.EditIndex = -1;
                CarregaItemRemanejamento(int.Parse(ViewState["co_remanejamento"].ToString()));
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Finaliza o recebimento do remanejamento especificado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_FinalizarRecebimento(object sender, EventArgs e)
        {
            if (InventarioAberto())
                return;
            try
            {
                IFarmaciaServiceFacade iFarmacia = Factory.GetInstance<IFarmaciaServiceFacade>();

                RemanejamentoMedicamento remanejamento = iFarmacia.BuscarPorCodigo<RemanejamentoMedicamento>(int.Parse(ViewState["co_remanejamento"].ToString()));

                //if (Factory.GetInstance<IInventario>().BuscarPorSituacao<Inventario>('A', r.Movimento.Farmacia_Destino.Codigo) != null)
                //{
                //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A finalização deste remanejamento não pode ser concluído, pois existe um inventário ABERTO para a farmácia deste remanejamento que deve ser encerrado.');", true);
                //    return;
                //}

                remanejamento.Status = RemanejamentoMedicamento.FECHADO;
                remanejamento.DataFechamento = DateTime.Now;

                iFarmacia.Salvar(remanejamento);
                iFarmacia.Salvar(new LogFarmacia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, EventoFarmacia.FINALIZAR_REMANEJAMENTO, "id remanejamento: " + remanejamento.Codigo));

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Remanejamento finalizado com sucesso.');location='Default.aspx';", true);
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Formata o gridview de acordo com o padrão especificado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridView(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            //if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
            {
                RemanejamentoMedicamento r = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<RemanejamentoMedicamento>(int.Parse(ViewState["co_remanejamento"].ToString()));
                ItemRemanejamento ir = Factory.GetInstance<IMovimentacao>().BuscarItensRemanejamentoPorRemanejamento<ItemRemanejamento>(r.Codigo).Where(p => p.LoteMedicamento.Codigo == int.Parse(GridView_ItensRemanejamento.DataKeys[e.Row.RowIndex]["CodigoLote"].ToString())).First();
                Image img = (Image)e.Row.FindControl("ImageStatus");
                LinkButton lb = ((LinkButton)e.Row.FindControl("LinkButton1"));

                //if (r.Status == 'F')
                //    lb.Enabled = false;

                if (ir.DataConfirmacao.HasValue || r.Status == RemanejamentoMedicamento.FECHADO)
                {
                    if (lb != null)
                        lb.Enabled = false;
                    //lb.Text = "Alterar";
                    //lb.CommandArgument = "A";
                    img.ImageUrl = "img/remanejamentoconfirmado.gif";
                }else
                    img.ImageUrl = "img/remanejamentonaoconfirmado.gif";
            }
        }

        private bool InventarioAberto()
        {
            RemanejamentoMedicamento r = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<RemanejamentoMedicamento>(int.Parse(ViewState["co_remanejamento"].ToString()));

            if (Factory.GetInstance<IInventario>().BuscarPorSituacao<Inventario>(Inventario.ABERTO,   r.Movimento.Farmacia_Destino.Codigo).Count != 0)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A confirmação de recebimento deste item não pode ser concluída, pois existe um inventário ABERTO para a farmácia deste remanejamento que deve ser encerrado.');", true);
                return true;
            }
            return false;
        }
    }
}
