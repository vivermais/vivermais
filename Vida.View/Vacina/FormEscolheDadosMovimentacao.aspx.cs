using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Movimentacao;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.View.Vacina
{
    public partial class FormEscolheDadosMovimentacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                ISeguranca iSeguranca = Factory.GetInstance<ISeguranca>();
                ISalaVacina iVacina = Factory.GetInstance<ISalaVacina>();

                bool permissao_doacao = iSeguranca.VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_DOACAO", Modulo.VACINA);
                bool permissao_devolucao = iSeguranca.VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_DEVOLUCAO", Modulo.VACINA);
                bool permissao_emprestimo = iSeguranca.VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_EMPRESTIMO", Modulo.VACINA);
                bool permissao_perda = iSeguranca.VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_PERDA", Modulo.VACINA);
                bool permissao_remanejamento = iSeguranca.VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_REMANEJAMENTO", Modulo.VACINA);
                bool permissao_acerto_balanco = iSeguranca.VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_ACERTO_BALANCO", Modulo.VACINA);

                if (permissao_doacao || permissao_devolucao ||
                     permissao_emprestimo || permissao_perda ||
                     permissao_remanejamento ||
                     permissao_acerto_balanco
                    )
                {
                    DropDownList_Sala.DataSource = iVacina.BuscarPorUsuario<ViverMais.Model.SalaVacina, Usuario>(usuario, true, true);
                    DropDownList_Sala.DataBind();

                    IList<TipoMovimentoVacina> tiposmovimento = iVacina.ListarTodos<TipoMovimentoVacina>().OrderBy(p => p.Nome).ToList();
                    foreach (TipoMovimentoVacina tipomovimento in tiposmovimento)
                    {
                        if ((tipomovimento.Codigo == TipoMovimentoVacina.DOACAO && permissao_doacao) || (tipomovimento.Codigo == TipoMovimentoVacina.DEVOLUCAO && permissao_devolucao) ||
                            (tipomovimento.Codigo == TipoMovimentoVacina.EMPRESTIMO && permissao_emprestimo) || (tipomovimento.Codigo == TipoMovimentoVacina.PERDA && permissao_perda) ||
                            (tipomovimento.Codigo == TipoMovimentoVacina.REMANEJAMENTO && permissao_remanejamento) ||
                            (tipomovimento.Codigo == TipoMovimentoVacina.ACERTO_BALANCO && permissao_acerto_balanco))

                            DropDownList_TipoMovimentacao.Items.Add(new ListItem(tipomovimento.Nome, tipomovimento.Codigo.ToString()));
                    }
                }
                else
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        protected void OnClick_Continuar(object sender, EventArgs e)
        {
            IList<InventarioVacina> inventariosAbertos = Factory.GetInstance<IInventarioVacina>().BuscarPorSituacao<InventarioVacina>(Convert.ToChar(InventarioVacina.DescricaoSituacao.Aberto), Convert.ToInt32(DropDownList_Sala.SelectedValue));
            if (inventariosAbertos.Count > 0)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, é necessário fechar o inventário aberto em " + inventariosAbertos[0].DataInventario.ToString("dd/MM/yyyy") + " antes de continuar com a nova movimentação.');", true);
                return;
            }

            string redirect = string.Empty;
            redirect = "FormMovimentacao.aspx?co_tipo=" + DropDownList_TipoMovimentacao.SelectedValue + "&co_sala=" + DropDownList_Sala.SelectedValue;

            if (Panel_Situacao.Visible)
                redirect += "&co_situacao=" + DropDownList_Situacao.SelectedValue;
            else
                redirect += "&co_situacao=" + OperacaoMovimentoVacina.RetornaSituacao(int.Parse(DropDownList_TipoMovimentacao.SelectedValue));

            Response.Redirect(redirect);
        }

        protected void OnSelectedIndexChanged_CarregaSituacao(object sender, EventArgs e)
        {
            IList<OperacaoMovimentoVacina> operacoesmovimento = null;
            ISalaVacina iVacina = Factory.GetInstance<ISalaVacina>();

            if (int.Parse(DropDownList_TipoMovimentacao.SelectedValue) == TipoMovimentoVacina.DOACAO)
            {
                SalaVacina sala = iVacina.BuscarPorCodigo<SalaVacina>(int.Parse(DropDownList_Sala.SelectedValue));
                operacoesmovimento = sala.PertenceCMADI ?
                    //iVacina.SalasPertencesCMADI().Contains(int.Parse(DropDownList_Sala.SelectedValue)) ?
                    iVacina.ListarTodos<OperacaoMovimentoVacina>().OrderBy(p => p.Nome).ToList() : iVacina.ListarTodos<OperacaoMovimentoVacina>().Where(p => p.Codigo != OperacaoMovimentoVacina.ENTRADA).OrderBy(p => p.Nome).ToList();
                this.PreencherOperacoesMovimento(operacoesmovimento);
            }
            else
            {
                if (int.Parse(DropDownList_TipoMovimentacao.SelectedValue) == TipoMovimentoVacina.EMPRESTIMO ||
                    int.Parse(DropDownList_TipoMovimentacao.SelectedValue) == TipoMovimentoVacina.ACERTO_BALANCO)
                {
                    operacoesmovimento = iVacina.ListarTodos<OperacaoMovimentoVacina>().OrderBy(p => p.Nome).ToList();
                    this.PreencherOperacoesMovimento(operacoesmovimento);
                }
                else
                {
                    Panel_Situacao.Visible = false;
                    CompareValidator_Situacao.Enabled = false;
                }
            }
        }

        private void PreencherOperacoesMovimento(IList<OperacaoMovimentoVacina> operacoesmovimento)
        {
            DropDownList_Situacao.DataSource = operacoesmovimento;
            DropDownList_Situacao.DataBind();
            DropDownList_Situacao.Items.Insert(0,new ListItem("Selecione...", "-1"));

            Panel_Situacao.Visible = true;
            CompareValidator_Situacao.Enabled = true;
        }

        protected void OnSelectedIndexChanged_DesabilitaSituacao(object sender, EventArgs e)
        {
            DropDownList_TipoMovimentacao.SelectedValue = "-1";
            Panel_Situacao.Visible = false;
            CompareValidator_Situacao.Enabled = false;
        }
    }
}
