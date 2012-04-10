﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.NotaFiscal;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Inventario;

namespace ViverMais.View.Farmacia
{
    public partial class FormNotaFiscal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Verifica se existem inventarios abertos para o almoxarifado
                IList<Inventario> inventariosAbertos = Factory.GetInstance<IInventario>().BuscarPorSituacao<Inventario>('A', 118);
                if (inventariosAbertos.Count > 0)
                {
                    //ClientScript.RegisterClientScriptBlock(Page.GetType(), "mensagem", "<script language='javascript'>alert('Necessário fechar o inventário aberto em " + inventariosAbertos[0].DataInventario.ToString("dd/MM/yyyy") + " antes de realizar esta operação.');document.location.href='FormGerarReceitaDispensacao.aspx';</script>");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "mensagem", "<script language='javascript'>alert('Necessário fechar o inventário aberto em " + inventariosAbertos[0].DataInventario.ToString("dd/MM/yyyy") + " antes de realizar esta operação.');window.location.href='Default.aspx';</script>", false);
                    return;
                }

                //ViverMais.Model.Farmacia farm = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia>(((Usuario)Session["Usuario"]).Codigo);

                //if (farm == null || (farm != null && farm.Codigo != Convert.ToInt32(ViverMais.Model.Farmacia.QualFarmacia.Almoxarifado)))
                //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Atenção usuário, você não tem permissão para acessar esta página.');location='Default.aspx';", true);

                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "REGISTRAR_NOTA_FISCAL", Modulo.FARMACIA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    CarregaResponsaveisCadastrados();
                    CarregaResponsaveis();

                    IList<FornecedorMedicamento> fornecedores = Factory.GetInstance<IFornecedorMedicamento>().ListarTodos<FornecedorMedicamento>().Where(p => p.Situacao == FornecedorMedicamento.ATIVO).OrderBy(p => p.NomeFantasia).ToList();
                    foreach (FornecedorMedicamento f in fornecedores)
                        DropDownList_Fornecedor.Items.Add(new ListItem(f.NomeFantasia, f.Codigo.ToString()));

                    int temp;

                    if (Request["co_nota"] != null && int.TryParse(Request["co_nota"].ToString(), out temp))
                    {
                        try
                        {
                            NotaFiscal notaFiscal = Factory.GetInstance<INotaFiscal>().BuscarPorCodigo<NotaFiscal>(int.Parse(Request["co_nota"].ToString()));

                            if (notaFiscal.Status == NotaFiscal.ENCERRADA)
                            {
                                Panel_Visualizacao.Visible = true;
                                Label_NumeroNota.Text = notaFiscal.NumeroNota;
                                Label_DataRecebimento.Text = notaFiscal.DataRecebimento.ToString("dd/MM/yyyy");
                                Label_DataAtesto.Text = notaFiscal.DataAtesto.ToString("dd/MM/yyyy");
                                Label_ResponsavelAtesto.Text = notaFiscal.Responsavel.Nome;
                                Label_Fornecedor.Text = notaFiscal.Fornecedor.NomeFantasia;
                                Label_ProcessoOrigem.Text = notaFiscal.ProcessoOrigem;
                                Label_Empenho.Text = notaFiscal.Empenho;
                                Label_AFM.Text = notaFiscal.Afm;
                            }
                            else
                            {
                                Panel_Cadastro.Visible = true;
                                Button_Salvar.Text = "Atualizar";

                                TextBox_NumeroNota.Text = notaFiscal.NumeroNota;
                                TextBox_DataRecebimento.Text = notaFiscal.DataRecebimento.ToString("dd/MM/yyyy");
                                TextBox_DataAtesto.Text = notaFiscal.DataAtesto.ToString("dd/MM/yyyy");
                                DropDownList_Responsavel.SelectedValue = notaFiscal.Responsavel.Codigo.ToString();

                                if (DropDownList_Fornecedor.Items.FindByValue(notaFiscal.Fornecedor.Codigo.ToString()) == null)
                                    DropDownList_Fornecedor.Items.Add(new ListItem(notaFiscal.Fornecedor.NomeFantasia, notaFiscal.Fornecedor.Codigo.ToString()));
                                //else

                                DropDownList_Fornecedor.SelectedValue = notaFiscal.Fornecedor.Codigo.ToString();
                                TextBox_ProcessoOrigem.Text = notaFiscal.ProcessoOrigem;
                                TextBox_Empenho.Text = notaFiscal.Empenho;
                                TextBox_AFM.Text = notaFiscal.Afm;
                            }
                        }
                        catch (Exception f)
                        {
                            throw f;
                        }
                    }
                    else
                        Panel_Cadastro.Visible = true;
                }
            }
        }

        /// <summary>
        /// Carrega os responsáveis cadastrados no módulo farmácia
        /// </summary>
        private void CarregaResponsaveis()
        {
            DropDownList_Responsavel.Items.Clear();
            DropDownList_Responsavel.Items.Add(new ListItem("Selecione...", "-1"));

            IList<ResponsavelAtesto> responsaveisAtesto = Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<ResponsavelAtesto>().OrderBy(p => p.Nome).ToList();
            foreach (ResponsavelAtesto r in responsaveisAtesto)
                DropDownList_Responsavel.Items.Add(new ListItem(r.Nome, r.Codigo.ToString()));
        }

        /// <summary>
        /// Chamada para atualização da lista de responsáveis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_CarregaResponsaveis(object sender, EventArgs e) 
        {
            CarregaResponsaveis();
        }

        /// <summary>
        /// Salva a nota fiscal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Salvar(object sender, EventArgs e) 
        {
            try
            {
                INotaFiscal iNotaFiscal = Factory.GetInstance<INotaFiscal>();
                IFarmaciaServiceFacade iFarmacia = Factory.GetInstance<IFarmaciaServiceFacade>();
                NotaFiscal notafiscal = Request["co_nota"] != null ? iNotaFiscal.BuscarPorCodigo<NotaFiscal>(int.Parse(Request["co_nota"].ToString())) : new NotaFiscal();

                if (iNotaFiscal.ValidarCadastroNotaFiscal<NotaFiscal>(int.Parse(DropDownList_Fornecedor.SelectedValue), TextBox_NumeroNota.Text, notafiscal.Codigo))
                {
                    notafiscal.NumeroNota = TextBox_NumeroNota.Text;
                    notafiscal.DataRecebimento = DateTime.Parse(TextBox_DataRecebimento.Text);
                    notafiscal.DataAtesto = DateTime.Parse(TextBox_DataAtesto.Text);
                    notafiscal.Responsavel = iFarmacia.BuscarPorCodigo<ResponsavelAtesto>(int.Parse(DropDownList_Responsavel.SelectedValue));
                    notafiscal.Fornecedor = iFarmacia.BuscarPorCodigo<FornecedorMedicamento>(int.Parse(DropDownList_Fornecedor.SelectedValue));
                    notafiscal.ProcessoOrigem = TextBox_ProcessoOrigem.Text;
                    notafiscal.Empenho = TextBox_Empenho.Text;
                    notafiscal.Afm = TextBox_AFM.Text;
                    notafiscal.Status = NotaFiscal.ABERTA;
                    notafiscal.Farmacia = Factory.GetInstance<IFarmacia>().BuscarPorCodigo<ViverMais.Model.Farmacia>(ViverMais.Model.Farmacia.ALMOXARIFADO);

                    iNotaFiscal.Salvar(notafiscal);
                    iNotaFiscal.Salvar(new LogFarmacia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo,
                        Request["co_nota"] != null ? EventoFarmacia.ALTERAR_NOTA_FISCAL : EventoFarmacia.CADASTRAR_NOTA_FISCAL, "id nota fiscal: " + notafiscal.Codigo));

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Nota Fiscal salva com sucesso!');location='FormItensNotaFiscal.aspx?co_nota=" + notafiscal.Codigo + "';", true);
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe uma nota fiscal cadastrada com o mesmo número e fornecedor! Por favor, informe outros dados.');", true);
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Salva o responsável pelo atesto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_SalvarResponsavel(object sender, EventArgs e)
        {
            try
            {
                IFarmaciaServiceFacade iFarmacia = Factory.GetInstance<IFarmaciaServiceFacade>();

                ResponsavelAtesto responsavelAtesto = new ResponsavelAtesto();
                responsavelAtesto.Nome = TextBox_Nome.Text;

                iFarmacia.Salvar(responsavelAtesto);
                iFarmacia.Salvar(new LogFarmacia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, EventoFarmacia.MANTER_RESPONSAVEL_ATESTO, "id responsavel atesto: " + responsavelAtesto.Codigo));

                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Responsável salvo com sucesso!');parent.parent.GB_hide();", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Responsável salvo com sucesso!');", true);
                TextBox_Nome.Text = "";
                //SelecionaTabDadosNotaFiscal();
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Seleciona o primeiro TabContainer
        /// </summary>
        private void SelecionaTabDadosNotaFiscal()
        {
            OnClick_CancelarCadastroResponsavel(new object(), new EventArgs());
            CarregaResponsaveisCadastrados();
            string temp_resp = DropDownList_Responsavel.SelectedValue;
            CarregaResponsaveis();
            DropDownList_Responsavel.SelectedValue = temp_resp;
            TabContainer_NotaFiscal.ActiveTabIndex = 0;
        }

        /// <summary>
        /// Cancela o cadastro do responsável
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_CancelarCadastroResponsavel(object sender, EventArgs e)
        {
            TextBox_Nome.Text = "";
        }

        /// <summary>
        /// Cancela a edição do registro de responsável
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCancelingEdit_CancelarEdicaoResponsavel(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_Responsavel.EditIndex = -1;
            CarregaResponsaveisCadastrados();
        }

        /// <summary>
        /// Edita o registro do responsável
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowEditing_EditarResponsavel(object sender, GridViewEditEventArgs e) 
        {
            GridView_Responsavel.EditIndex = e.NewEditIndex;
            CarregaResponsaveisCadastrados();
        }

        /// <summary>
        /// Atualiza o registro do responsável
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowUpdating_AtualizarResponsavel(object sender, GridViewUpdateEventArgs e) 
        {
            try
            {
                GridViewRow r = GridView_Responsavel.Rows[e.RowIndex];
                TextBox tbx = (TextBox)r.FindControl("TextBox_NomeResponsavel");
                ResponsavelAtesto resp = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ResponsavelAtesto>(int.Parse(GridView_Responsavel.DataKeys[e.RowIndex]["Codigo"].ToString()));
                resp.Nome = tbx.Text;
                Factory.GetInstance<IFarmaciaServiceFacade>().Salvar(resp);

                GridView_Responsavel.EditIndex = -1;
                CarregaResponsaveisCadastrados();
                CarregaResponsaveis();
                //SelecionaTabDadosNotaFiscal();
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Carrega os Responsáveis Cadastrados
        /// </summary>
        private void CarregaResponsaveisCadastrados()
        {
            GridView_Responsavel.DataSource = Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<ResponsavelAtesto>().OrderBy(p => p.Nome).ToList();
            GridView_Responsavel.DataBind();
        }

        /// <summary>
        /// Paginação do gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            CarregaResponsaveisCadastrados();
            GridView_Responsavel.PageIndex = e.NewPageIndex;
        }
    }
}
