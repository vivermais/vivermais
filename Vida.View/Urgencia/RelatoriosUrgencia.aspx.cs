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
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using AjaxControlToolkit;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;

namespace ViverMais.View.Urgencia
{
    public partial class RelatoriosUrgencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuario = ((Usuario)Session["Usuario"]);
                IRelatorioUrgencia iRelatorio = Factory.GetInstance<IRelatorioUrgencia>();

                ISeguranca iSeguranca = Factory.GetInstance<ISeguranca>();
                foreach (AccordionPane ap in Accordion1.Panes)
                {
                    if (!iSeguranca.VerificarPermissao(usuario.Codigo, ap.ID, Modulo.URGENCIA))
                        ap.Visible = false;
                }

                IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
                IUrgenciaServiceFacade iurgencia = Factory.GetInstance<IUrgenciaServiceFacade>();

                IList<ViverMais.Model.Distrito> distritos = iRelatorio.DistritosAtivos<Distrito>();
                    //iViverMais.ListarTodos<ViverMais.Model.Distrito>().Where(p => p.Nome.ToUpper() != "NAO SE APLICA" && p.Nome.ToUpper() != "NÃO SE APLICA").ToList();
                ddlDistritoSituacao.DataTextField = "Nome";
                ddlDistritoSituacao.DataValueField = "Codigo";
                ddlDistritoSituacao.DataSource = distritos;
                ddlDistritoSituacao.DataBind();
                ddlDistritoSituacao.Items.Insert(0, new ListItem("Selecione...", "-1"));

                IList<string> codcids = Factory.GetInstance<ICid>().ListarGrupos();
                ddlGrupoCid.Items.Add(new ListItem("Selecione...", "0"));
                foreach (string c in codcids)
                {
                    ddlGrupoCid.Items.Add(new ListItem(c, c));
                    DropDownList_GrupoCIDProcedencia.Items.Add(new ListItem(c, c));
                }

                IList<ViverMais.Model.EstabelecimentoSaude> unidades = iRelatorio.EstabelecimentosAtivos<ViverMais.Model.EstabelecimentoSaude>();
                
                //IList<ViverMais.Model.EstabelecimentoSaude> unidades = iViverMais.ListarTodos<ViverMais.Model.EstabelecimentoSaude>().Where(un => un.RazaoSocial.ToUpper() != "NAO SE APLICA").OrderBy(un => un.NomeFantasia).ToList();
                this.PopulaDropDownUnidades(unidades, ddlUnidadeLeitos);
                this.PopulaDropDownUnidades(unidades, ddlUnidadePermanecia);
                this.PopulaDropDownUnidades(unidades, DropDownList_EstabelecimentoProcedimentoFPO);
                this.PopulaDropDownUnidades(unidades, DropDownList_UnidadeCIDProcedencia);
                this.PopulaDropDownUnidades(unidades, DropDownList_UnidadeFaixaEtaria);
                this.PopulaDropDownUnidades(unidades, DropDownList_UnidadeCID);
                this.PopulaDropDownUnidades(unidades, DropDownList_UnidadeEvasao);

                ddlUnidadePermanecia.Items.Insert(0, new ListItem("Selecione...", "0"));

                IList<ViverMais.Model.SituacaoAtendimento> situacoes = iurgencia.ListarTodos<ViverMais.Model.SituacaoAtendimento>();
                ddlSituacao.DataTextField = "Nome";
                ddlSituacao.DataValueField = "Codigo";

                ddlSituacao.DataSource = situacoes;
                ddlSituacao.DataBind();
                ddlSituacao.Items.Insert(0, new ListItem("Selecione...", "-1"));

                for (int i = 1; i < 13; i++)
                    DropDownList_MesProcedimento.Items.Add(new ListItem(i.ToString("00"),i.ToString("00")));

                this.CarregaTituloDropDown(ddlUnidadeSituacao);
                this.CarregaTituloDropDown(DropDownList_UnidadeCID);
                this.CarregaTituloDropDown(DropDownList_UnidadeEvasao);
                this.CarregaTituloDropDown(DropDownList_UnidadeFaixaEtaria);
                this.CarregaTituloDropDown(DropDownList_UnidadeCIDProcedencia);
                this.CarregaTituloDropDown(DropDownList_EstabelecimentoProcedimentoFPO);
                this.CarregaTituloDropDown(ddlCid);
                this.CarregaTituloDropDown(DropDownList_ProcedimentoSIGTAPFPO);
                this.CarregaTituloDropDown(DropDownList_CIDProcedencia);
            }
        }

        private void CarregaTituloDropDown(DropDownList dropdown)
        {
            dropdown.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
        }

        private void PopulaDropDownUnidades(IList<ViverMais.Model.EstabelecimentoSaude> unidades, DropDownList dropdown)
        {
            dropdown.DataTextField = "NomeFantasia";
            dropdown.DataValueField = "CNES";
            dropdown.DataSource = unidades;
            dropdown.DataBind();

            dropdown.Items.Insert(0, new ListItem("Selecione...", "-1"));
        }

        protected void ddlDistritoSituacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();
            IList<ViverMais.Model.EstabelecimentoSaude> unidades = iEstabelecimento.BuscarUnidadeDistrito<ViverMais.Model.EstabelecimentoSaude>(int.Parse(ddlDistritoSituacao.SelectedValue));
            string[] pasativos = Factory.GetInstance<IRelatorioUrgencia>().EstabelecimentosAtivos<ViverMais.Model.EstabelecimentoSaude>().Select(p=>p.CNES).ToArray();

            ddlUnidadeSituacao.DataTextField = "NomeFantasia";
            ddlUnidadeSituacao.DataValueField = "CNES";

            ddlUnidadeSituacao.DataSource = (from unidade in unidades orderby unidade.NomeFantasia where pasativos.Contains(unidade.CNES) select unidade);
            ddlUnidadeSituacao.DataBind();

            ddlUnidadeSituacao.Items.Insert(0, new ListItem("Selecione...", "-1"));
        }

        public void Redirect(string url, string target, string windowFeatures)
        {
            HttpContext context = HttpContext.Current;
            if ((String.IsNullOrEmpty(target) || target.Equals("_self", StringComparison.OrdinalIgnoreCase))
                && String.IsNullOrEmpty(windowFeatures))
            {
                context.Response.Redirect(url);
            }
            else
            {
                Page page = (Page)context.Handler;
                if (page == null)
                {
                    throw new InvalidOperationException("Cannot redirect to new window outside Page context.");
                }
                url = page.ResolveClientUrl(url);
                string script = string.Empty;
                if (String.IsNullOrEmpty(windowFeatures))
                {
                    script = @"window.open(""{0}"", ""{1}"");";
                }

                script = String.Format(script, url, target, windowFeatures);
                ScriptManager.RegisterStartupScript(page, typeof(Page), "Redirect", script, true);
            }
        }

        protected void btnPesquisarSituacao_Click1(object sender, ImageClickEventArgs e)
        {
            Redirect("RelatorioSituacao.aspx?id_situacao=" + ddlSituacao.SelectedValue, "_blank", "");
        }

        protected void btnPesquisar_Click(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid)
                return;

            DateTime temp;

            if (!ddlSituacao.SelectedValue.Equals("-1"))
                Redirect("RelatorioSituacao.aspx?id_situacao=" + ddlSituacao.SelectedValue + "&id_unidade=" + ddlUnidadeSituacao.SelectedValue, "_blank", "");
            if (ddlCid.SelectedValue != "0" && DateTime.TryParse(tbxDataInicialCid.Text, out temp) && DateTime.TryParse(tbxDataFinalCid.Text, out temp))
                Redirect("RelatorioAtendimentoCID.aspx?cid=" + ddlCid.SelectedValue + "&data_inicio=" + tbxDataInicialCid.Text + "&data_fim=" + tbxDataFinalCid.Text, "_blank", "");
            else
                if (CheckBox_TodosOsCids.Checked && DateTime.TryParse(tbxDataInicialCid.Text, out temp) && DateTime.TryParse(tbxDataFinalCid.Text, out temp))
                    Redirect("RelatorioAtendimentoCID.aspx?cid=&data_inicio=" + tbxDataInicialCid.Text + "&data_fim=" + tbxDataFinalCid.Text, "_blank", "");
            if ((!string.IsNullOrEmpty(tbxPeriodoInicial.Text)) && (!string.IsNullOrEmpty(tbxPeriodoFinal.Text)) && DateTime.TryParse(tbxPeriodoInicial.Text, out temp) && DateTime.TryParse(tbxPeriodoFinal.Text, out temp))
                Redirect("RelatorioAbsenteismo.aspx?data_inicio=" + tbxPeriodoInicial.Text + "&data_fim=" + tbxPeriodoFinal.Text, "_blank", "");
            if ((!string.IsNullOrEmpty(tbxDataInicial.Text)) && (!string.IsNullOrEmpty(tbxDataFinal.Text)) && DateTime.TryParse(tbxDataInicial.Text, out temp) && DateTime.TryParse(tbxDataFinal.Text, out temp))
                Redirect("RelatorioAtendimentoFaixa.aspx?sexo=" + rbtSexo.SelectedValue + "&data_inicio=" + DateTime.Parse(tbxDataInicial.Text) + "&data_fim=" + DateTime.Parse(tbxDataFinal.Text), "_blank", "");
            if (!ddlUnidadeLeitos.SelectedValue.Equals("-1"))
                Redirect("RelatoriosLeitosPorFaixaEtaria.aspx?id_unidade=" + ddlUnidadeLeitos.SelectedValue, "_blank", "");
            if (!ddlUnidadePermanecia.SelectedValue.Equals("-1") && DateTime.TryParse(tbxDataInicioPermanencia.Text, out temp) && DateTime.TryParse(tbxDataFinalPermanencia.Text,out temp))
                Redirect("RelatorioTempoPermanencia.aspx?data_inicial=" + DateTime.Parse(tbxDataInicioPermanencia.Text).ToString("dd/MM/yyyy") + "&data_final=" + DateTime.Parse(tbxDataFinalPermanencia.Text).ToString("dd/MM/yyyy") + "&id_unidade=" + ddlUnidadePermanecia.SelectedValue, "_blank", "");
            ddlDistritoSituacao.SelectedValue = "-1";
            ddlSituacao.SelectedValue = "-1";
            ddlCid.SelectedValue = "0";
            CheckBox_TodosOsCids.Checked = false;
            tbxPeriodoInicial.Text = string.Empty;
            tbxDataInicial.Text = string.Empty;
            ddlUnidadeLeitos.SelectedValue = "-1";

        }

        protected void OnClick_RelSituacao(object sender, EventArgs e)
        {
            if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "GERAR_RELATORIO_SITUACAO", Modulo.URGENCIA))
                Redirect("RelatorioSituacao.aspx?id_situacao=" + ddlSituacao.SelectedValue + "&id_unidade=" + ddlUnidadeSituacao.SelectedValue, "_blank", "");
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para gerar este relatório! Por favor, entre em contato com a administração.');", true);
        }

        protected void OnClick_RelCid(object sender, EventArgs e)
        {
            if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "GERAR_RELATORIO_ATENDIMENTO_CID", Modulo.URGENCIA))
            {
                if (CustomValidator_RelCid.IsValid)
                {
                    if (ddlCid.SelectedValue != "0")
                        Redirect("RelatorioAtendimentoCID.aspx?cid=" + ddlCid.SelectedValue + "&data_inicio=" + tbxDataInicialCid.Text + "&data_fim=" + tbxDataFinalCid.Text + "&co_unidade=" + DropDownList_UnidadeCID.SelectedValue, "_blank", "");
                    else
                        Redirect("RelatorioAtendimentoCID.aspx?cid=&data_inicio=" + tbxDataInicialCid.Text + "&data_fim=" + tbxDataFinalCid.Text + "&co_unidade=" + DropDownList_UnidadeCID.SelectedValue, "_blank", "");
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + CustomValidator_RelCid.ErrorMessage + "');", true);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para gerar este relatório! Por favor, entre em contato com a administração.');", true);
        }

        protected void OnServerValidate_RelCid(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = true;

            if (ddlCid.SelectedValue == "0" && !CheckBox_TodosOsCids.Checked)
            {
                CustomValidator_RelCid.ErrorMessage = "Selecione um Cid ou marque a opção de todos.";
                e.IsValid = false;
            }
        }

        protected void OnClick_RelEvasao(object sender, EventArgs e)
        {
            if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "GERAR_RELATORIO_EVASAO", Modulo.URGENCIA))
                Redirect("RelatorioAbsenteismo.aspx?data_inicio=" + tbxPeriodoInicial.Text + "&data_fim=" + tbxPeriodoFinal.Text + "&co_unidade=" + DropDownList_UnidadeEvasao.SelectedValue, "_blank", "");
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para gerar este relatório! Por favor, entre em contato com a administração.');", true);
        }

        protected void OnClick_RelAtendimentoFaixa(object sender, EventArgs e)
        {
            if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "GERAR_RELATORIO_ATENDIMENTO_FAIXA_ETARIA", Modulo.URGENCIA))
                Redirect("RelatorioAtendimentoFaixa.aspx?sexo=" + rbtSexo.SelectedValue + "&data_inicio=" + tbxDataInicial.Text + "&data_fim=" + tbxDataFinal.Text + "&co_unidade=" + DropDownList_UnidadeFaixaEtaria.SelectedValue, "_blank", "");
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para gerar este relatório! Por favor, entre em contato com a administração.');", true);
        }

        protected void OnClick_RelLeitosFaixa(object sender, EventArgs e)
        {
            if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "GERAR_RELATORIO_LEITOS_FAIXA_ETARIA", Modulo.URGENCIA))
                Redirect("RelatoriosLeitosPorFaixaEtaria.aspx?id_unidade=" + ddlUnidadeLeitos.SelectedValue, "_blank", "");
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para gerar este relatório! Por favor, entre em contato com a administração.');", true);
        }

        protected void OnClick_RelPermanencia(object sender, EventArgs e)
        {
            if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "GERAR_RELATORIO_TEMPO_PERMANENCIA", Modulo.URGENCIA))
                Redirect("RelatorioTempoPermanencia.aspx?data_inicial=" + tbxDataInicioPermanencia.Text + "&data_final=" + tbxDataFinalPermanencia.Text + "&id_unidade=" + ddlUnidadePermanecia.SelectedValue, "_blank", "");
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para gerar este relatório! Por favor, entre em contato com a administração.');", true);
        }

        protected void OnClick_RelProcedimentoFPO(object sender, EventArgs e)
        {
            if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "GERAR_RELATORIO_PROCEDIMENTO",Modulo.URGENCIA))
            {
                if (DropDownList_ProcedimentoNaoFaturavelFPO.SelectedValue == "-1" && DropDownList_ProcedimentoSIGTAPFPO.SelectedValue == "-1")
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Selecione um dos procedimentos disponíveis.');", true);
                else
                    Redirect("RelatorioProcedimentosFPO.aspx?co_unidade=" + DropDownList_EstabelecimentoProcedimentoFPO.SelectedValue + "&competencia=" + TextBox_AnoProcedimento.Text + DropDownList_MesProcedimento.SelectedValue + "&co_procedimento=" + DropDownList_ProcedimentoSIGTAPFPO.SelectedValue + "&co_procedimentonaofaturavel=" + DropDownList_ProcedimentoNaoFaturavelFPO.SelectedValue, "_blank", "");
            }else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para gerar este relatório! Por favor, entre em contato com a administração.');", true);
        }

        protected void OnClick_RelCIDProcedencia(object sender, EventArgs e)
        {
            if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "GERAR_RELATORIO_PROCEDENCIA", Modulo.URGENCIA))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "window.open('RelatorioProcedencia.aspx?co_unidade=" + DropDownList_UnidadeCIDProcedencia.SelectedValue + "&co_cid=" + DropDownList_CIDProcedencia.SelectedValue + "&datainicio=" + TextBox_DataInicialCIDProcedencia.Text + "&datafim=" + TextBox_DataFinalCIDProcedencia .Text + "');", true);
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para gerar este relatório! Por favor, entre em contato com a administração.');", true);
        }

        /// <summary>
        /// Busca os CID's de acordo com seu código
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlGrupoCid_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList grupocid = (DropDownList)sender;
            IList<ViverMais.Model.Cid> cids = Factory.GetInstance<ICid>().BuscarPorGrupo<ViverMais.Model.Cid>(grupocid.SelectedValue.ToString());

            if (grupocid.ID == "DropDownList_GrupoCIDProcedencia")
            {
                DropDownList_CIDProcedencia.DataTextField = "Nome";
                DropDownList_CIDProcedencia.DataValueField = "Codigo";

                DropDownList_CIDProcedencia.DataSource = cids;
                DropDownList_CIDProcedencia.DataBind();

                DropDownList_CIDProcedencia.Items.Insert(0, new ListItem("Selecione...", "-1"));
            }
            else
            {
                ddlCid.DataTextField = "Nome";
                ddlCid.DataValueField = "Codigo";

                ddlCid.DataSource = cids;
                ddlCid.DataBind();

                ddlCid.Items.Insert(0, new ListItem("Selecione...", "0"));
            }
        }

        protected void OnClick_PesquisarProcedimentoSIGTAP(object sender, EventArgs e)
        {
            DropDownList_ProcedimentoSIGTAPFPO.DataTextField = "Nome";
            DropDownList_ProcedimentoSIGTAPFPO.DataValueField = "Codigo";

            IList<Procedimento> procedimentos = Factory.GetInstance<IProcedimento>().BuscarPorNome<Procedimento>(TextBox_BuscarSIGTAP.Text);
            DropDownList_ProcedimentoSIGTAPFPO.DataSource = procedimentos;
            DropDownList_ProcedimentoSIGTAPFPO.DataBind();

            DropDownList_ProcedimentoSIGTAPFPO.Items.Insert(0, new ListItem("Selecione...", "-1"));
            DropDownList_ProcedimentoSIGTAPFPO.Items.Insert(1, new ListItem("TODOS", "0"));
        }

        protected void OnClick_PesquisarProcedimentoNaoFaturavel(object sender, EventArgs e)
        {
            DropDownList_ProcedimentoNaoFaturavelFPO.DataTextField = "Nome";
            DropDownList_ProcedimentoNaoFaturavelFPO.DataValueField = "Codigo";

            IList<ProcedimentoNaoFaturavel> procedimentos = Factory.GetInstance<IProcedimentoNaoFaturavel>().BuscarPorNome<ProcedimentoNaoFaturavel>(TextBox_BuscarNaoFaturavel.Text);
            DropDownList_ProcedimentoNaoFaturavelFPO.DataSource = procedimentos;
            DropDownList_ProcedimentoNaoFaturavelFPO.DataBind();

            DropDownList_ProcedimentoNaoFaturavelFPO.Items.Insert(0, new ListItem("Selecione...", "-1"));
            DropDownList_ProcedimentoNaoFaturavelFPO.Items.Insert(1, new ListItem("TODOS", "0"));
        }
    }
}
