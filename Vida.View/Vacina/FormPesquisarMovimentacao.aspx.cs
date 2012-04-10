using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.DAO;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Vacina;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Movimentacao;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;

namespace ViverMais.View.Vacina
{
    public partial class FormPesquisarMovimentacao : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ISeguranca iSeguranca = Factory.GetInstance<ISeguranca>();
                ISalaVacina iVacina = Factory.GetInstance<ISalaVacina>();

                Usuario usuario = (Usuario)Session["Usuario"];

                if (iSeguranca.VerificarPermissao(usuario.Codigo, "PESQUISAR_MOVIMENTACAO", Modulo.VACINA))
                {
                    DropDownList_Sala.DataSource = iVacina.BuscarPorUsuario<ViverMais.Model.SalaVacina, Usuario>(usuario, true, true);
                    DropDownList_Sala.DataBind();
                    DropDownList_Sala.Attributes.Add("onmouseover", "javascript:showTooltip(this);");

                    DropDownList_TipoMovimento.DataSource = iVacina.ListarTodos<TipoMovimentoVacina>("Nome", true);
                    DropDownList_TipoMovimento.DataBind();
                    DropDownList_TipoMovimento.Items.Insert(0, new ListItem("Selecione...", "-1"));
                    DropDownList_TipoMovimento.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
                }
                else
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        protected void OnClick_Pesquisar(object sender, EventArgs e)
        {
            if (!this.CustomValidatorPeriodoMovimentacao.IsValid)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + this.CustomValidatorPeriodoMovimentacao.ErrorMessage + "');", true);
                return;
            }

            IMovimentoVacina iMovimento = Factory.GetInstance<IMovimentoVacina>();
            Panel_ResultadoPesquisa.Visible = true;
            int tipomovimento = int.Parse(DropDownList_TipoMovimento.SelectedValue);
            ViewState["tipomovimento"] = tipomovimento;
            IList<MovimentoVacina> movimentos = new List<MovimentoVacina>();

            if (string.IsNullOrEmpty(this.TextBox_DataInicio.Text) && int.Parse(this.DropDownList_SalaDestino.SelectedValue) == -1) //Por tipo
                movimentos = iMovimento.BuscarMovimentacao<MovimentoVacina>(int.Parse(DropDownList_Sala.SelectedValue), tipomovimento);
            else
                if (!string.IsNullOrEmpty(this.TextBox_DataInicio.Text) && int.Parse(this.DropDownList_SalaDestino.SelectedValue) == -1) //Mais período
                    movimentos = iMovimento.BuscarMovimentacao<MovimentoVacina>(int.Parse(DropDownList_Sala.SelectedValue), tipomovimento, DateTime.Parse(this.TextBox_DataInicio.Text), DateTime.Parse(this.TextBox_DataFim.Text));
                else
                    if (string.IsNullOrEmpty(this.TextBox_DataInicio.Text) && int.Parse(this.DropDownList_SalaDestino.SelectedValue) != -1) //Mais sala de destino
                        movimentos = iMovimento.BuscarMovimentacao<MovimentoVacina>(int.Parse(DropDownList_Sala.SelectedValue), tipomovimento, int.Parse(this.DropDownList_SalaDestino.SelectedValue));
                    else //Mais período e sala de destino
                        movimentos = iMovimento.BuscarMovimentacao<MovimentoVacina>(int.Parse(DropDownList_Sala.SelectedValue), tipomovimento, int.Parse(this.DropDownList_SalaDestino.SelectedValue), DateTime.Parse(this.TextBox_DataInicio.Text), DateTime.Parse(this.TextBox_DataFim.Text));

            Session["movimentosVacina"] = movimentos;
            this.CarregaMovimentos(movimentos);
        }

        protected void OnSelectedIndexChanged_CarregaSalasDestino(object sender, EventArgs e)
        {
            if (this.DropDownList_Sala.SelectedValue != "-1" &&
                int.Parse(this.DropDownList_TipoMovimento.SelectedValue)
                == TipoMovimentoVacina.REMANEJAMENTO)
            {
                ISalaVacina iSala = Factory.GetInstance<ISalaVacina>();
                IList<SalaVacina> salas = iSala.ListarTodos<SalaVacina>();

                this.DropDownList_SalaDestino.DataSource = salas.Except(salas.Where(p => p.Codigo == int.Parse(this.DropDownList_Sala.SelectedValue))).OrderBy(p=>p.Nome);
                this.DropDownList_SalaDestino.DataBind();
                this.DropDownList_SalaDestino.Items.Insert(0, new ListItem("SELECIONE...", "-1"));

                this.Panel_SalasDestino.Visible = true;
            }else
                this.Panel_SalasDestino.Visible = false;
        }

        protected void OnServerValidate_CompararDatas(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = true;
            this.CustomValidatorPeriodoMovimentacao.ErrorMessage = string.Empty;

            if (!string.IsNullOrEmpty(this.TextBox_DataFim.Text)
                && string.IsNullOrEmpty(this.TextBox_DataInicio.Text))
            {
                this.CustomValidatorPeriodoMovimentacao.ErrorMessage += " - Data Início é Obrigatório.";
                e.IsValid = false;
            }

            if (!string.IsNullOrEmpty(this.TextBox_DataInicio.Text)
                && string.IsNullOrEmpty(this.TextBox_DataFim.Text))
            {
                this.CustomValidatorPeriodoMovimentacao.ErrorMessage += " - Data Final é Obrigatório.";
                e.IsValid = false;
            }
        }

        private void CarregaMovimentos(IList<MovimentoVacina> movimentos)
        {
            GridView_ResultadoPesquisa.DataSource = movimentos;
            GridView_ResultadoPesquisa.DataBind();
        }

        protected void OnSelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            long co_movimentacao = long.Parse(this.GridView_ResultadoPesquisa.DataKeys[e.NewSelectedIndex]["Codigo"].ToString());
            ReportDocument doc = new ReportDocument();
            doc = Factory.GetInstance<IRelatorioVacina>().ObterRelatorioMovimento(co_movimentacao);

            //Hashtable hash = Factory.GetInstance<IRelatorioVacina>().ObterRelatorioMovimento(co_movimentacao);

            //doc.Load(Server.MapPath("RelatoriosCrystal/RelMovimento.rpt"));

            //doc.Database.Tables["corpo"].SetDataSource((DataTable)hash["corpo"]);
            //doc.Database.Tables["cabecalho"].SetDataSource((DataTable)hash["cabecalho"]);
            //doc.Subreports["RelHistoricoItemMovimentacao.rpt"].SetDataSource((DataTable)hash["historicoitens"]);

            Session["documentoImpressaoVacina"] = doc;
            ////Response.Redirect("FormMostrarRelatorioCrystalImpressao.aspx", true);
            //System.Text.StringBuilder script = new System.Text.StringBuilder();
            //script.Append("\n<script language='javascript'>\n");
            //script.Append("function download()");
            //script.Append("{\n");
            //script.Append(" window.open('FormMostrarRelatorioCrystalImpressao.aspx', 'Download', 'toolbar=no, location=no, directories=no, status=yes, menubar=no, scrollbars=no, resizable=no, titlebar=no, copyhistory=no, width=100, height=100, left=0, top=0');\n");
            //script.Append("}\n");
            //script.Append("window.onload = download;\n");
            //script.Append("</script>");
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "download", script.ToString(),false);
            //RegisterClientScriptBlock("download", s.ToString());
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "open", "<script>window.open('FormMostrarRelatorioCrystalImpressao.aspx?nome_arquivo=movimento.pdf');</script>", false);
            //Response.Redirect("FormMostrarRelatorioCrystalImpressao.aspx");
        }

        protected void OnPageIndexChanging_Movimentos(object sender, GridViewPageEventArgs e)
        {
            this.CarregaMovimentos((IList<MovimentoVacina>)Session["movimentosVacina"]);

            GridView_ResultadoPesquisa.PageIndex = e.NewPageIndex;
            GridView_ResultadoPesquisa.DataBind();
        }

        protected void OnRowDataBound_Movimento(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (int.Parse(ViewState["tipomovimento"].ToString()) != TipoMovimentoVacina.REMANEJAMENTO)
                {
                    this.GridView_ResultadoPesquisa.Columns[2].Visible = false;
                    this.GridView_ResultadoPesquisa.Columns[3].Visible = false;
                }
                else
                {
                    this.GridView_ResultadoPesquisa.Columns[2].Visible = true;
                    this.GridView_ResultadoPesquisa.Columns[3].Visible = true;
                    //IMovimentoVacina iVacina = Factory.GetInstance<IMovimentoVacina>();
                    long co_movimento = long.Parse(this.GridView_ResultadoPesquisa.DataKeys[e.Row.RowIndex]["Codigo"].ToString());
                    //RemanejamentoVacina remanejamento = iVacina.BuscarRemanejamentoPorMovimentacao<RemanejamentoVacina>(co_movimento);
                    IList<MovimentoVacina> movimentos = (IList<MovimentoVacina>)Session["movimentosVacina"];
                    MovimentoVacina movimento = movimentos.Where(p => p.Codigo == co_movimento).FirstOrDefault();
                    HyperLink navegador = (HyperLink)e.Row.FindControl("HyperLinkAlterarQuantidade");

                    if (navegador != null && movimento != null)
                    {
                        if (!movimento.Editar)
                        //if (remanejamento.Status == RemanejamentoVacina.FECHADO
                        //    || iVacina.VerificarConfirmacaoAlteracaoItensMovimento(remanejamento.Movimento.Codigo))
                        {
                            navegador.NavigateUrl = "#";
                            navegador.Enabled = false;
                            navegador.Text = "<img src=\"img/btn_editar-not.jpg\" border=\"0\" alt=\"Alteração indisponível.\" />";
                        }
                        else
                            navegador.NavigateUrl = "~/Vacina/FormMovimentacao.aspx?co_sala=" + movimento.Sala.Codigo + "&co_tipo=" + TipoMovimentoVacina.REMANEJAMENTO + "&co_situacao=" + OperacaoMovimentoVacina.RetornaSituacao(TipoMovimentoVacina.REMANEJAMENTO) + "&co_movimento=" + movimento.Codigo;
                    }
                }
            }
        }

        protected void OnClick_PesquisarPorNumero(object sender, EventArgs e)
        {
            IList<MovimentoVacina> movimentos = new List<MovimentoVacina>();

            IMovimentoVacina iMovimento = Factory.GetInstance<IMovimentoVacina>();
            MovimentoVacina movimento = iMovimento.BuscarPorNumero<MovimentoVacina, Usuario>(long.Parse(this.TextBox_Numero.Text), (Usuario)Session["Usuario"]);
            ViewState["tipomovimento"] = -1;

            if (movimento != null)
            {
                movimentos.Add(movimento);
                ViewState["tipomovimento"] = movimento.TipoMovimento.Codigo;
            }

            Session["movimentosVacina"] = movimentos;
            this.GridView_ResultadoPesquisa.DataSource = movimentos;
            this.GridView_ResultadoPesquisa.DataBind();

            this.Panel_ResultadoPesquisa.Visible = true;
        }
    }
}
