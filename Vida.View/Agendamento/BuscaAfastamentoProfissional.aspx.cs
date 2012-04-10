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
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using System.Collections.Generic;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Agendamento
{
    public partial class BuscaAfastamentoProfissional : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MOVIMENTACAO_AFASTAR_PROFISSIONAL",Modulo.AGENDAMENTO))
                {
                    PanelListaAfastamentos.Visible = false;
                    PanelDadosAfastamento.Visible = false;
                    // Lista todas as Categorias
                    IList<ViverMais.Model.CategoriaOcupacao> categorias = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<ViverMais.Model.CategoriaOcupacao>();
                    ddlCategoria.DataSource = categorias;
                    ddlCategoria.DataBind();
                    ddlCategoria.SelectedValue = "1";
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        protected void imgPesquisar_Click(object sender, EventArgs e)
        {
            ViverMais.Model.EstabelecimentoSaude eas = Factory.GetInstance<IEstabelecimentoSaude>().BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(tbxcnes.Text.Trim());
            if (eas != null)
            {
                PanelListaAfastamentos.Visible = true;
                VinculoProfissional vinculoProfissional = Factory.GetInstance<IVinculo>().BuscarVinculoProfissionalPorCnes<ViverMais.Model.VinculoProfissional>(eas.CNES, tbxNumConselho.Text.Trim(), ddlCategoria.SelectedValue).FirstOrDefault();
                if (vinculoProfissional != null)
                {
                    IList<AfastamentoProfissional> afastamentos = Factory.GetInstance<IAfastamentoProfissional>().ListaAfastamentoProfissional<AfastamentoProfissional>(eas.CNES, vinculoProfissional.Profissional.CPF);
                    GridViewListaAfastamentos.DataSource = afastamentos;
                    GridViewListaAfastamentos.DataBind();
                    Session["Afastamentos"] = afastamentos;
                    DesabilitaBotaoDeAcordoComAfastamento();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Nenhum Vínculo localizado com as informações inseridas!');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Estabelecimento de Saúde não Localizado!');", true);
                return;
            }
        }

        protected void DesabilitaBotaoDeAcordoComAfastamento()
        {
            IList<AfastamentoProfissional> afastamentos = (IList<AfastamentoProfissional>)Session["Afastamentos"];
            int index = 0;
            if (afastamentos != null && afastamentos.Count != 0)
            {
                for (int i = GridViewListaAfastamentos.PageIndex * GridViewListaAfastamentos.PageSize; i < (10 * (GridViewListaAfastamentos.PageIndex + 1)); i++)
                {
                    if (index == 10)
                        index = 0;
                    if (i < afastamentos.Count)
                    {
                        if (afastamentos[i].Data_Final != null && afastamentos[i].Data_Final != DateTime.MinValue && afastamentos[i].Data_Final != DateTime.MaxValue)
                        {
                            LinkButton botao = (LinkButton)GridViewListaAfastamentos.Rows[index].Cells[6].FindControl("cmdSelect");
                            if (botao != null)
                                botao.Visible = false;
                        }
                    }
                    else
                        break;
                    index++;
                }
            }
        }

        protected void GridViewListaAfastamentos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewListaAfastamentos.PageIndex = e.NewPageIndex;
            if (Session["Afastamentos"] != null)
            {
                //DataTable table = (DataTable)Session["Afastamentos"];
                GridViewListaAfastamentos.DataSource = Session["Afastamentos"];
                GridViewListaAfastamentos.DataBind();
            }
        }

        protected void GridViewListaAfastamentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id_afastamento = Convert.ToInt32(e.CommandArgument);
            AfastamentoProfissional afastamento = Factory.GetInstance<IAfastamentoProfissional>().BuscarPorCodigo<AfastamentoProfissional>(id_afastamento);
            tbxCodigoAfastamento.Text = id_afastamento.ToString();
            lblEstabelecimento.Text = afastamento.Unidade.NomeFantasia;
            lblProfissional.Text = afastamento.Profissional.Nome;
            lblDataInicial.Text = afastamento.Data_Inicial.ToString("dd/MM/yyyy");
            tbxDataRetorno.Text = afastamento.Data_Final.ToString("dd/MM/yyyy");
            tbxMotivo.Text = afastamento.Motivo;
            tbxObs.Text = afastamento.Obs;
            PanelDadosAfastamento.Visible = true;
        }

        protected void btnFechar_Click(object sender, EventArgs e)
        {
            PanelDadosAfastamento.Visible = false;
        }

        protected void btnSalvaAfastamento_Click(object sender, EventArgs e)
        {
            AfastamentoProfissional afastamento = Factory.GetInstance<IAfastamentoProfissional>().BuscarPorCodigo<AfastamentoProfissional>(int.Parse(tbxCodigoAfastamento.Text));
            if (afastamento != null)
            {
                if (DateTime.Parse(tbxDataRetorno.Text) > afastamento.Data_Final)
                {
                    afastamento.Data_Final = DateTime.Parse(tbxDataRetorno.Text);
                    afastamento.Obs = tbxObs.Text;
                    Factory.GetInstance<IAfastamentoProfissional>().Salvar(afastamento);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('DADOS SALVOS COM SUCESSO!');", true);
                    imgPesquisar_Click(new object(), new EventArgs());
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Data de Retorno Inferior a Data Inicial!');</script>");
                    return;
                }
            }

        }
    }
}
