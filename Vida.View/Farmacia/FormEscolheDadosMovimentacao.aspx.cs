using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Inventario;

namespace ViverMais.View.Farmacia
{
    public partial class FormEscolheDadosMovimentacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                bool permissao_doacao = Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_DOACAO", Modulo.FARMACIA);
                bool permissao_devolucao_paciente = Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_DEVOLUCAO_PACIENTE", Modulo.FARMACIA);
                bool permissao_emprestimo = Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_EMPRESTIMO", Modulo.FARMACIA);
                bool permissao_perda = Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_PERDA", Modulo.FARMACIA);
                bool permissao_remanejamento = Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_REMANEJAMENTO", Modulo.FARMACIA);
                bool permissao_transferencia = Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_TRANSFERENCIA_INTERNA", Modulo.FARMACIA);
                bool permissao_acerto_balanco = Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_ACERTO_BALANCO", Modulo.FARMACIA);
                
                if (permissao_doacao || permissao_devolucao_paciente ||
                     permissao_emprestimo || permissao_perda ||
                     permissao_remanejamento || permissao_transferencia ||
                     permissao_acerto_balanco
                    )
                {
                    DropDownList_Farmacia.DataSource = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia, Usuario>(usuario, true, true);
                    DropDownList_Farmacia.DataBind();

                    IList<TipoMovimento> ltm = Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<TipoMovimento>().OrderBy(p => p.Nome).ToList();
                    foreach (TipoMovimento tm in ltm)
                    {
                        if ((tm.Codigo == TipoMovimento.DOACAO && permissao_doacao) || (tm.Codigo == TipoMovimento.DEVOLUCAO_PACIENTE && permissao_devolucao_paciente) ||
                            (tm.Codigo == TipoMovimento.EMPRESTIMO && permissao_emprestimo) || (tm.Codigo == TipoMovimento.PERDA&& permissao_perda) ||
                            (tm.Codigo == TipoMovimento.REMANEJAMENTO && permissao_remanejamento) || (tm.Codigo == TipoMovimento.TRANSFERENCIA_INTERNA && permissao_transferencia) ||
                            (tm.Codigo == TipoMovimento.ACERTO_BALANCO && permissao_acerto_balanco))

                            DropDownList_TipoMovimentacao.Items.Add(new ListItem(tm.Nome, tm.Codigo.ToString()));
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                }
            }
        }

        protected void OnClick_Continuar(object sender, EventArgs e)
        {
            //Verifica se existe inventário em aberto para a farmácia
            //Caso exista não deixa realizar o procedimento
            IList<Inventario> inventariosAbertos = Factory.GetInstance<IInventario>().BuscarPorSituacao<Inventario>(Inventario.ABERTO, Convert.ToInt32(DropDownList_Farmacia.SelectedValue));
            if (inventariosAbertos.Count > 0)
            {
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "mensagem", "<script language='javascript'>alert('Necessário fechar o inventário aberto em " + inventariosAbertos[0].DataInventario.ToString("dd/MM/yyyy") + " antes de continuar com a nova movimentação.');</script>");
                return;
            }

            string redirect = string.Empty;
            redirect = "FormMovimentacao.aspx?tipo=" + DropDownList_TipoMovimentacao.SelectedValue + "&co_farmacia=" + DropDownList_Farmacia.SelectedValue;

            if (Panel_Situacao.Visible)
                redirect += "&co_situacao=" + DropDownList_Situacao.SelectedValue;

            Response.Redirect(redirect);
        }

        protected void OnSelectedIndexChanged_CarregaSituacao(object sender, EventArgs e)
        {
            DropDownList_Situacao.Items.Clear();
            DropDownList_Situacao.Items.Add(new ListItem("Selecione...", "-1"));
            IList<TipoOperacaoMovimento> ltom = null;

            if (int.Parse(DropDownList_TipoMovimentacao.SelectedValue) == TipoMovimento.DOACAO)
            {
                ltom = int.Parse(DropDownList_Farmacia.SelectedValue) == ViverMais.Model.Farmacia.ALMOXARIFADO ?
                    Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<TipoOperacaoMovimento>().OrderBy(p => p.Descricao).ToList() : 
                    Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<TipoOperacaoMovimento>().Where(p => p.Codigo != TipoOperacaoMovimento.ENTRADA).OrderBy(p => p.Descricao).ToList();

                foreach (TipoOperacaoMovimento tom in ltom)
                    DropDownList_Situacao.Items.Add(new ListItem(tom.Descricao, tom.Codigo.ToString()));

                Panel_Situacao.Visible = true;
                CompareValidator_Situacao.Enabled = true;
            }
            else
            {
                if (int.Parse(DropDownList_TipoMovimentacao.SelectedValue) == TipoMovimento.EMPRESTIMO ||
                    int.Parse(DropDownList_TipoMovimentacao.SelectedValue) == TipoMovimento.ACERTO_BALANCO)
                {
                    ltom = Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<TipoOperacaoMovimento>().OrderBy(p => p.Descricao).ToList();
                    foreach (TipoOperacaoMovimento tom in ltom)
                        DropDownList_Situacao.Items.Add(new ListItem(tom.Descricao, tom.Codigo.ToString()));

                    Panel_Situacao.Visible = true;
                    CompareValidator_Situacao.Enabled = true;
                }
                else
                {
                    Panel_Situacao.Visible = false;
                    CompareValidator_Situacao.Enabled = false;
                }
            }
        }

        protected void OnSelectedIndexChanged_DesabilitaSituacao(object sender, EventArgs e)
        {
            DropDownList_TipoMovimentacao.SelectedValue = "-1";
            Panel_Situacao.Visible = false;
            CompareValidator_Situacao.Enabled = false;
        }
    }
}
