using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Movimentacao;

namespace ViverMais.View.Vacina
{
    public partial class FormConfirmarRecebimentoRemanejamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                ISeguranca iSeguranca = Factory.GetInstance<ISeguranca>();

                if (!iSeguranca.VerificarPermissao(usuario.Codigo, "RECEBER_REMANEJAMENTO", Modulo.VACINA))
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    long co_remanejamento;
                    if (Request["co_remanejamento"] != null && long.TryParse(Request["co_remanejamento"].ToString(), out co_remanejamento))
                    {
                        IMovimentoVacina iVacina = Factory.GetInstance<IMovimentoVacina>();
                        RemanejamentoVacina remanejamento = iVacina.BuscarPorCodigo<RemanejamentoVacina>(co_remanejamento);
                        Label_DataMovimento.Text = remanejamento.Data.ToString("dd/MM/yyyy");
                        Label_SalaMovimento.Text = remanejamento.SalaOrigem;
                        Label_SalaDestino.Text = remanejamento.Movimento.SalaDestino.Nome;
                        Label_DataEnvioMovimento.Text = remanejamento.Movimento.DataEnvio.Value.ToString("dd/MM/yyyy");
                        Label_DataRecebMovimento.Text = remanejamento.Movimento.DataRecebimento.Value.ToString("dd/MM/yyyy");
                        Label_RespEnvMovimento.Text = remanejamento.Movimento.ResponsavelEnvio;
                        Label_RespRecebMovimento.Text = remanejamento.Movimento.ResponsavelRecebimento;
                        ViewState["co_usuario"] = usuario.Codigo;

                        IList<ItemRemanejamentoVacina> itens = iVacina.BuscarItensRemanejamento<ItemRemanejamentoVacina>(remanejamento.Codigo);
                        Session["imunosRemanejamento"] = itens;
                        this.CarregaImunos(itens);

                        IList<InventarioVacina> inventariosAbertos = Factory.GetInstance<IInventarioVacina>().BuscarPorSituacao<InventarioVacina>(Convert.ToChar(InventarioVacina.DescricaoSituacao.Aberto), remanejamento.Movimento.SalaDestino.Codigo);
                        if (inventariosAbertos.Count > 0)
                        {
                            Application["AcessoPagina"] = "Usuário, é necessário fechar o inventário da sala " + inventariosAbertos[0].Sala.Nome + " aberto em " + inventariosAbertos[0].DataInventario.ToString("dd/MM/yyyy") + " antes de continuar com a confirmação de recebimento.";
                            Response.Redirect("FormAcessoNegado.aspx");
                            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, é necessário fechar o inventário aberto em " + inventariosAbertos[0].DataInventario.ToString("dd/MM/yyyy") + " antes de continuar com a confirmação de recebimento.');location='FormReceberRemanejamento.aspx';", true);
                            //return;
                        }
                    }
                }
            }
        }

        private void CarregaImunos(IList<ItemRemanejamentoVacina> itens)
        {
            GridView_Imunos.DataSource = itens;
            GridView_Imunos.DataBind();
        }

        protected void OnPageIndexChanging_Imunos(object sender, GridViewPageEventArgs e)
        {
            this.CarregaImunos((IList<ItemRemanejamentoVacina>)Session["imunosRemanejamento"]);
            GridView_Imunos.PageIndex = e.NewPageIndex;
            GridView_Imunos.DataBind();
        }

        protected void OnRowUpdating_Imunos(object sender, GridViewUpdateEventArgs e)
        {
            IMovimentoVacina iVacina = Factory.GetInstance<IMovimentoVacina>();
            IList<ItemRemanejamentoVacina> itens = (IList<ItemRemanejamentoVacina>)Session["imunosRemanejamento"];
            Session["imunosRemanejamento"] = itens;
            long co_item = long.Parse(GridView_Imunos.DataKeys[e.RowIndex]["Codigo"].ToString());
            ItemRemanejamentoVacina item = itens.Where(p => p.Codigo == co_item).First();
            int quantidadeconfirmada = int.Parse(((TextBox)GridView_Imunos.Rows[e.RowIndex].FindControl("TextBox_QuantidadeConfirmada")).Text);

            item.QuantidadeConfirmada = quantidadeconfirmada;
            item.DataConfirmacao = DateTime.Now;
            item.UsuarioConfirmacao = iVacina.BuscarPorCodigo<Usuario>(int.Parse(ViewState["co_usuario"].ToString()));

            int itensnaoconfirmados = itens.Where(p => !p.RecebimentoConfirmado).ToList().Count;
            bool fecharremanejamento = itensnaoconfirmados == 0 ? true : false;

            iVacina.ConfirmarRemanejamento(item, fecharremanejamento);
            itens[ItemRemanejamentoVacina.RetornaIndex(itens, item.Codigo)] = item;
            Session["imunosRemanejamento"] = itens;

            GridView_Imunos.EditIndex = -1;
            this.CarregaImunos(itens);

            if (fecharremanejamento)
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, este foi o último imunobiológico do remanejamento confirmado! O estoque foi atualizado e o remanejamento encerrado com sucesso.');location='FormReceberRemanejamento.aspx';", true);
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Quantidade confirmada com sucesso! Estoque atualizado.');", true);
        }

        protected void OnRowEditing_Imunos(object sender, GridViewEditEventArgs e)
        {
            GridView_Imunos.EditIndex = e.NewEditIndex;
            this.CarregaImunos((IList<ItemRemanejamentoVacina>)Session["imunosRemanejamento"]);
        }

        protected void OnRowCancelingEdit_Imunos(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_Imunos.EditIndex = -1;
            this.CarregaImunos((IList<ItemRemanejamentoVacina>)Session["imunosRemanejamento"]);
        }

        protected void OnRowDataBound_Imunos(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                IList<ItemRemanejamentoVacina> itens = (IList<ItemRemanejamentoVacina>)Session["imunosRemanejamento"];
                ItemRemanejamentoVacina item = itens.Where(p => p.Codigo == long.Parse(GridView_Imunos.DataKeys[e.Row.RowIndex]["Codigo"].ToString())).First();
                LinkButton lbconfirmar = (LinkButton)e.Row.Controls[7].FindControl("LinkButtonConfirmar");

                if (lbconfirmar != null)
                {
                    if (item.RecebimentoConfirmado)
                    {
                        lbconfirmar.Enabled = false;
                        lbconfirmar.Text = "<img src='img/yes.png' border='0' title='Recebimento Confirmado' />";
                    }
                    else
                        lbconfirmar.Text = "<img src='img/no.png' border='0' title='Recebimento não confirmado' />";
                }
            }
        }
    }
}
