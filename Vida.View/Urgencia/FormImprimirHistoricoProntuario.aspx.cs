﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using ViverMais.View.Urgencia.RelatoriosCrystal;
using System.Collections;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;

namespace ViverMais.View.Urgencia
{
    public partial class FormImprimirHistoricoProntuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                long co_prontuario;
                if (Request["co_prontuario"] != null && long.TryParse(Request["co_prontuario"].ToString(), out co_prontuario) && Request["tipo"] != null)
                {
                    //#region Caminho absoluto utilizado como link das imagens do datalist desta página. Foi implementado desta maneira, pois quando esta página é executada com a função "HttpContext.Current.Server.Execute" as imagens devem possuir caminho absoluto.
                    //string app = ((Request.ApplicationPath.Equals("/") || string.IsNullOrEmpty(Request.ApplicationPath)) ? "/" : Request.ApplicationPath + "/");
                    //string urlpart = string.Empty;

                    //if (Request.Url.ToString().Contains("FormImprimirHistoricoProntuario"))
                    //    urlpart = Regex.Replace(HttpUtility.UrlDecode(Request.Url.ToString()), @"\s", "").
                    //        Replace(Regex.Replace(HttpUtility.UrlDecode(Request.RawUrl), @"\s", ""), "") + app;
                    //else
                    //{
                    //    string secondparturl = HttpUtility.UrlDecode(Request.FilePath) + (!string.IsNullOrEmpty(Request.QueryString.ToString()) ? ("?" + Request.QueryString.ToString()) : string.Empty);
                    //    urlpart = Regex.Replace(HttpUtility.UrlDecode(Request.Url.ToString()), @"\s", "").
                    //        Replace(Regex.Replace((Request.Url.ToString().Contains(secondparturl) ? secondparturl : Request.RawUrl), @"\s", ""), "") + app;
                    //}

                    //ViewState["url_part"] = urlpart;
                    //#endregion

                    //Hashtable tab = null;
                    //DataTable cabecalho = null;
                    //ReportDocument doc = new ReportDocument();

                    //DSCabecalho dsc = new DSCabecalho();
                    //DSCids dscids = new DSCids();
                    //DataTable acolhimento = null;
                    //DataTable cids = null;
                    //IProntuario iProntuario = Factory.GetInstance<IProntuario>();

                    switch (Request["tipo"].ToString())
                    {
                        case "acolhimento":
                            //tab = Factory.GetInstance<IProntuario>().RetornaHashTableAcolhimento(long.Parse(Request["co_prontuario"].ToString()));
                            //cabecalho = (DataTable)tab["cabecalho"];
                            //acolhimento = (DataTable)tab["acolhimento"];

                            //DSRelAcolhimentoProntuario dsa = new DSRelAcolhimentoProntuario();
                            //dsa.Tables.Add(acolhimento);
                            //dsc.Tables.Add(cabecalho);

                            //doc.Load(Server.MapPath("RelatoriosCrystal/RelAcolhimentoProntuario.rpt"));
                            //doc.SetDataSource(dsa.Tables[1]);
                            //doc.Subreports[0].SetDataSource(dsc.Tables[1]);

                            //Session["documentoImpressaoUrgencia"] = doc;

                            Session["documentoImpressaoUrgencia"] = Factory.GetInstance<IRelatorioUrgencia>().ObterRelatorioAcolhimento(co_prontuario);
                            Response.Redirect("FormImprimirCrystalReports.aspx?nomearquivo=acolhimento.pdf");
                            break;
                        case "aprazamentos":
                            Session["documentoImpressaoUrgencia"] = Factory.GetInstance<IRelatorioUrgencia>().ObterRelatorioAprazados(co_prontuario);
                            Response.Redirect("FormImprimirCrystalReports.aspx?nomearquivo=aprazamentos.pdf");
                            break;
                        case "consultamedica":
                            //tab = Factory.GetInstance<IProntuario>().RetornaHashtableConsultaMedica(long.Parse(Request["co_prontuario"].ToString()));
                            //cabecalho = (DataTable)tab["cabecalho"];
                            //DataTable anamnese = (DataTable)tab["anamnese"];
                            //cids = (DataTable)tab["cids"];

                            //DSRelConsultaMedica dsm = new DSRelConsultaMedica();
                            //dsm.Tables.Add(anamnese);
                            //dsc.Tables.Add(cabecalho);
                            //dscids.Tables.Add(cids);

                            //doc.Load(Server.MapPath("RelatoriosCrystal/RelConsultaMedicaProntuario.rpt"));
                            //doc.SetDataSource(dsm.Tables[1]);
                            //doc.Subreports[0].SetDataSource(dsc.Tables[1]);
                            //doc.Subreports[1].SetDataSource(dscids.Tables[1]);

                            //Session["documentoImpressaoUrgencia"] = doc;
                            Session["documentoImpressaoUrgencia"] = Factory.GetInstance<IRelatorioUrgencia>().ObterRelatorioConsultaMedica(co_prontuario);
                            Response.Redirect("FormImprimirCrystalReports.aspx?nomearquivo=consultamedica.pdf");
                            break;
                        case "evolucoesmedica":
                            //tab = Factory.GetInstance<IProntuario>().RetornaHashtableEvolucaoMedica(long.Parse(Request["co_prontuario"].ToString()));
                            //cabecalho = (DataTable)tab["cabecalho"];
                            //DataTable evolucaomedica = (DataTable)tab["evolucaomedica"];
                            //cids = (DataTable)tab["cids"];

                            //DSEvolucaoMedica dsevolucaomedica = new DSEvolucaoMedica();
                            //dsevolucaomedica.Tables.Add(evolucaomedica);
                            //dsc.Tables.Add(cabecalho);
                            //dscids.Tables.Add(cids);

                            //doc.Load(Server.MapPath("RelatoriosCrystal/RelEvolucoesMedica.rpt"));
                            //doc.SetDataSource(dsevolucaomedica.Tables[1]);
                            //doc.Subreports["RelCabecalho.rpt"].SetDataSource(dsc.Tables[1]);
                            //doc.Subreports["RelCids.rpt"].SetDataSource(dscids.Tables[1]);

                            //Session["documentoImpressaoUrgencia"] = doc;
                            Session["documentoImpressaoUrgencia"] = Factory.GetInstance<IRelatorioUrgencia>().ObterRelatorioEvolucoesMedicas(co_prontuario);
                            Response.Redirect("FormImprimirCrystalReports.aspx?nomearquivo=evolucoesmedicas.pdf");
                            break;
                        case "evolucoesenfermagem":
                            //tab = Factory.GetInstance<IProntuario>().RetornaHashtableEvolucaoEnfermagem(long.Parse(Request["co_prontuario"].ToString()));
                            //cabecalho = (DataTable)tab["cabecalho"];
                            //DataTable evolucaoenfermagem = (DataTable)tab["evolucaoenfermagem"];

                            //DSEvolucaoEnfermagem def = new DSEvolucaoEnfermagem();
                            //dsc.Tables.Add(cabecalho);
                            //def.Tables.Add(evolucaoenfermagem);

                            //doc.Load(Server.MapPath("RelatoriosCrystal/RelEvolucoesEnfermagem.rpt"));
                            //doc.SetDataSource(def.Tables[1]);
                            //doc.Subreports[0].SetDataSource(dsc.Tables[1]);

                            //Session["documentoImpressaoUrgencia"] = doc;
                            Session["documentoImpressaoUrgencia"] = Factory.GetInstance<IRelatorioUrgencia>().ObterRelatorioEvolucoesEnfermagem(co_prontuario);
                            Response.Redirect("FormImprimirCrystalReports.aspx?nomearquivo=evolucoesenfermagem.pdf");
                            break;
                        case "atestadosreceitas":
                            DataList_AtestadoReceita.DataSource = Factory.GetInstance<IProntuario>().RetornaDataTableAtestadosReceitas(co_prontuario);
                            DataList_AtestadoReceita.DataBind();
                            break;
                        case "exames":
                            //tab = Factory.GetInstance<IProntuario>().RetornaHashtableExames(long.Parse(Request["co_prontuario"].ToString()));
                            //cabecalho = (DataTable)tab["cabecalho"];
                            //DataTable exames = (DataTable)tab["exames"];
                            //DSExames dsexames = new DSExames();
                            //dsexames.Tables.Add(exames);
                            //dsc.Tables.Add(cabecalho);

                            //doc.Load(Server.MapPath("RelatoriosCrystal/RelExamesProntuario.rpt"));
                            //doc.SetDataSource(dsexames.Tables[1]);
                            //doc.Subreports[0].SetDataSource(dsc.Tables[1]);

                            //Session["documentoImpressaoUrgencia"] = doc;
                            Session["documentoImpressaoUrgencia"] = Factory.GetInstance<IRelatorioUrgencia>().ObterRelatorioExamesInternos(co_prontuario);
                            Response.Redirect("FormImprimirCrystalReports.aspx?nomearquivo=examesinternos.pdf");
                            break;
                        case "exameseletivos":
                            DataList_ExameEletivo.DataSource = Factory.GetInstance<IProntuario>().RetornaDataTableExamesEletivos(co_prontuario);
                            DataList_ExameEletivo.DataBind();
                            break;
                        case "prescricoes":
                            //tab = iProntuario.RetornaHashtablePrescricoes(long.Parse(Request["co_prontuario"].ToString()));
                            //cabecalho = (DataTable)tab["cabecalho"];
                            //DataTable prescricoes = (DataTable)tab["prescricao"];

                            //DSPrescricao dsprescricoes = new DSPrescricao();
                            //dsprescricoes.Tables.Add(prescricoes);
                            //dsc.Tables.Add(cabecalho);

                            //DSPrescricaoMedicamento dsmedicamentos = new DSPrescricaoMedicamento();
                            //dsmedicamentos.Tables.Add((DataTable)tab["medicamento"]);

                            //DSPrescricaoProcedimento dsprocedimentos = new DSPrescricaoProcedimento();
                            //dsprocedimentos.Tables.Add((DataTable)tab["procedimento"]);

                            //DSPrescricaoProcedimentoNaoFaturavel dsprocnf = new DSPrescricaoProcedimentoNaoFaturavel();
                            //dsprocnf.Tables.Add((DataTable)tab["procedimentonaofaturavel"]);

                            //doc.Load(Server.MapPath("RelatoriosCrystal/RelPrescricoesProntuario.rpt"));
                            //doc.SetDataSource(dsprescricoes.Tables[1]);
                            //doc.Subreports[0].SetDataSource(dsc.Tables[1]);
                            //doc.Subreports[1].SetDataSource(dsmedicamentos.Tables[1]);
                            //doc.Subreports[2].SetDataSource(dsprocedimentos.Tables[1]);
                            //doc.Subreports[3].SetDataSource(dsprocnf.Tables[1]);

                            //Session["documentoImpressaoUrgencia"] = doc;
                            Session["documentoImpressaoUrgencia"] = Factory.GetInstance<IRelatorioUrgencia>().ObterRelatorioPrescricoes(co_prontuario);
                            Response.Redirect("FormImprimirCrystalReports.aspx?nomearquivo=prescricoes.pdf");
                            break;
                        case "relatoriogeral":
                            //tab = iProntuario.RetornaHashTableAcolhimento(long.Parse(Request["co_prontuario"].ToString()));
                            //DSRelAcolhimentoProntuario dsacolhimento = new DSRelAcolhimentoProntuario();
                            //dsc.Tables.Add((DataTable)tab["cabecalho"]);
                            //dsacolhimento.Tables.Add((DataTable)tab["acolhimento"]);

                            //tab = iProntuario.RetornaHashtableEvolucaoEnfermagem(long.Parse(Request["co_prontuario"].ToString()));
                            //def = new DSEvolucaoEnfermagem();
                            //def.Tables.Add((DataTable)tab["evolucaoenfermagem"]);

                            //tab = iProntuario.RetornaHashtableExames(long.Parse(Request["co_prontuario"].ToString()));
                            //dsexames = new DSExames();
                            //dsexames.Tables.Add((DataTable)tab["exames"]);

                            //doc.Load(Server.MapPath("RelatoriosCrystal/RelRelatorioGeralProntuario.rpt"));

                            //tab = iProntuario.RetornaHashtablePrescricoes(long.Parse(Request["co_prontuario"].ToString()));
                            //doc.Database.Tables[0].SetDataSource((DataTable)tab["prescricao"]);

                            //doc.Subreports["RelPrescricaoMedicamento.rpt"].SetDataSource((DataTable)tab["medicamento"]);
                            //doc.Subreports["RelPrescricaoProcedimento.rpt"].SetDataSource((DataTable)tab["procedimento"]);
                            //doc.Subreports["RelPrescricaoProcedimentoNaoFaturavel.rpt"].SetDataSource((DataTable)tab["procedimentonaofaturavel"]);

                            //doc.Subreports["RelCabecalho.rpt"].SetDataSource(dsc.Tables[1]); //cabeçalho
                            //doc.Subreports["RelAcolhimentoProntuario.rpt"].SetDataSource(dsacolhimento.Tables[1]); //acolhimento
                            //doc.Subreports["RelClassificacoesRisco.rpt"].SetDataSource(iProntuario.RetornaClassificacoesRisco(long.Parse(Request["co_prontuario"].ToString()))); //classificacoes de risco

                            //tab = iProntuario.RetornaHashtableConsultaMedica(long.Parse(Request["co_prontuario"].ToString()));

                            //doc.Subreports["RelConsultaMedicaRelatorioGeral.rpt"].Database.Tables["consultamedica"].SetDataSource((DataTable)tab["anamnese"]); //consulta médica anamnese
                            //doc.Subreports["RelConsultaMedicaRelatorioGeral.rpt"].Database.Tables["cids"].SetDataSource((DataTable)tab["cids"]); //consulta médica cids

                            //doc.Subreports["RelEvolucoesEnfermagem.rpt"].SetDataSource(def.Tables[1]); //evolução enfermagem
                            //doc.Subreports["RelExamesProntuario.rpt"].SetDataSource(dsexames.Tables[1]); //exame prontuário

                            //tab = iProntuario.RetornaHashtableEvolucaoMedica(long.Parse(Request["co_prontuario"].ToString()));
                            //evolucaomedica = (DataTable)tab["evolucaomedica"];
                            //cids = (DataTable)tab["cids"];

                            //doc.Subreports["RelEvolucaoMedicaRelatorioGeral.rpt"].Database.Tables["evolucao"].SetDataSource(evolucaomedica);
                            //doc.Subreports["RelEvolucaoMedicaRelatorioGeral.rpt"].Database.Tables["cids"].SetDataSource(cids);

                            //UsuarioProfissionalUrgence usuario = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
                            //VinculoProfissional vinculo = Factory.GetInstance<IVinculo>().BuscarPorVinculoProfissional<VinculoProfissional>(usuario.UnidadeVinculo, usuario.Id_Profissional, usuario.CodigoCBO)[0];

                            //doc.SetParameterValue("@profissional", vinculo.Profissional.Nome);
                            //doc.SetParameterValue("@crmprofissional", string.IsNullOrEmpty(vinculo.RegistroConselho) ? "CRM não identificado" : vinculo.RegistroConselho);

                            //Session["documentoImpressaoUrgencia"] = doc;
                            Session["documentoImpressaoUrgencia"] = Factory.GetInstance<IRelatorioUrgencia>().ObterRelatorioGeral(co_prontuario, ((Usuario)Session["Usuario"]).Codigo);
                            Response.Redirect("FormImprimirCrystalReports.aspx?nomearquivo=relatoriogeral.pdf");
                            break;
                    }
                }
            }
        }

        protected void OnItemDataBound_ConfigurarDataListAtestados(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Table tab = (System.Web.UI.WebControls.Table)e.Item.FindControl("Tabela_AtestadoReceita");
                Label lbTipoDocumento = (Label)e.Item.FindControl("TipoDocumento");

                if (lbTipoDocumento.Text == "Receita")
                {
                    ((Image)this.FindControl(tab, "topo")).ImageUrl = "~/Urgencia/img/topo_receita.jpg";
                    this.FindControl(tab, "LabelSiglaReceita").Visible = true;
                }
                else
                {
                    if (lbTipoDocumento.Text == "Atestado")
                        ((Image)this.FindControl(tab, "topo")).ImageUrl = "~/Urgencia/img/topo_atestado.jpg";
                    else
                        ((Image)this.FindControl(tab, "topo")).ImageUrl = "~/Urgencia/img/topo_comparecimento.jpg";
                }

                lbTipoDocumento.Visible = false;
                Label labeldata = (Label)this.FindControl(tab, "Label_DataDocumento");
                labeldata.Text = "Salvador, " + DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        protected void OnItemDataBound_ConfigurarDataListExames(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Table tab = (System.Web.UI.WebControls.Table)e.Item.FindControl("Tabela_Exames");

                Label labeldata = (Label)this.FindControl(tab, "Label_DataDocumento");
                labeldata.Text = "Salvador, " + DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        private Control FindControl(System.Web.UI.WebControls.Table tab, string id_controle)
        {
            for (int linha = 0; linha < tab.Rows.Count; linha ++)
            {
                TableRow row = tab.Rows[linha];

                for (int coluna = 0; coluna < row.Cells.Count; coluna ++)
                {
                    Control control = row.Cells[coluna].FindControl(id_controle);

                    if (control != null)
                        return control;
                }
            }

            return null;
        }
    }
}
