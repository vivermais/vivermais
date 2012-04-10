﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Inventario;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using System.Collections;
using ViverMais.View.Farmacia.RelatoriosCrystal;

namespace ViverMais.View
{
    public partial class FormDadosInventario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBox_DataFechamento.Text = DateTime.Now.ToString("dd/MM/yyyy");

                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "REALIZAR_INVENTARIO",Modulo.FARMACIA) && !Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "REALIZAR_ABERTURA_ENCERRAMENTO_INVENTARIO",Modulo.FARMACIA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    int temp;

                    if (Request["co_inventario"] != null && int.TryParse(Request["co_inventario"].ToString(), out temp))
                        CarregaDadosInventario(int.Parse(Request["co_inventario"].ToString()));
                }
            }
        }

        /// <summary>
        /// Carrega os dados do inventário
        /// </summary>
        /// <param name="inventario">Código do inventário</param>
        private void CarregaDadosInventario(int co_inventario)
        {
            ViverMais.Model.Inventario iv = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.Inventario>(co_inventario);

            Label_Farmacia.Text     = iv.Farmacia.Nome;
            Label_DataAbertura.Text = iv.DataInventario.ToString("dd/MM/yyyy");

            if (iv.Situacao == Inventario.ENCERRADO)
            {
                TextBox_DataFechamento.Visible = false;
                Label_DataFechamento.Visible = true;
                Label_DataFechamento.Text = iv.DataFechamento.Value.ToString("dd/MM/yyyy");
                Label_Situacao.Text = "FECHADO";
                Button_FecharInventario.Visible = false;
                Button_MedicamentosInventario.Visible = false;
            }
            else
                Label_Situacao.Text = "ABERTO";
        }

        /// <summary>
        /// Função que fecha o inventário e atualiza o estoque da farmácia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_FecharInventario(object sender, EventArgs e) 
        {
            if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "REALIZAR_ABERTURA_ENCERRAMENTO_INVENTARIO", Modulo.FARMACIA))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para encerrar o inventário! Por favor entre em contato com a administração.');", true);
                return;
            }

            IInventario iInventario = Factory.GetInstance<IInventario>();

            ViverMais.Model.Inventario inventario = iInventario.BuscarPorCodigo<ViverMais.Model.Inventario>(int.Parse(Request["co_inventario"].ToString()));

            if (DateTime.Parse(TextBox_DataFechamento.Text).CompareTo(DateTime.Parse(Label_DataAbertura.Text)) >= 0)
            {
                inventario.DataFechamento = DateTime.Parse(TextBox_DataFechamento.Text);
                inventario.Situacao = Inventario.ENCERRADO;

                try
                {
                    iInventario.EncerrarInventario<Inventario>(inventario);
                    iInventario.Salvar(new LogFarmacia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, EventoFarmacia.ENCERRAR_INVENTARIO, "id inventario: " + inventario.Codigo));

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Inventário encerrado com sucesso! Estoque atualizado.');location='Default.aspx';", true);
                }
                catch (Exception f)
                {
                    throw f;
                }
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A Data de Fechamento deve ser igual ou maior que a Data de Abertura.');", true);
        }

        /// <summary>
        /// Redireciona o usuário para a página de medicamentos do inventário
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_ItensInventario(object sender, EventArgs e) 
        {
            Response.Redirect("FormItensInventario.aspx?co_inventario=" + Request["co_inventario"].ToString());
        }

        /// <summary>
        /// Gera o relatório escolhido para o inventário corrente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_GerarRelatorio(object sender, EventArgs e)
        {
            ReportDocument doc = new ReportDocument();
            Hashtable hash = null;

            //DataSet ds = new DataSet();
            //SqlDataAdapter da = null;

            if (RadioButton_Conferencia.Checked)
            {
                doc.Load(Server.MapPath("RelatoriosCrystal/RelInventarioConferencia.rpt"));
                hash = Factory.GetInstance<IInventario>().RelatorioInventario(int.Parse(Request["co_inventario"].ToString()), (int)Inventario.TipoRelatorio.CONFERENCIA);
                //da = new SqlDataAdapter("EXEC Proc_GeraRelatorioConferenciaInventario " + Request["co_inventario"].ToString(), ConfigurationManager.ConnectionStrings["ConnectionStringModuloFarmacia"].ConnectionString);
            }
            else
            {
                doc.Load(Server.MapPath("RelatoriosCrystal/RelInventarioFinal.rpt"));
                hash = Factory.GetInstance<IInventario>().RelatorioInventario(int.Parse(Request["co_inventario"].ToString()), (int)Inventario.TipoRelatorio.FINAL);
                //da = new SqlDataAdapter("EXEC Proc_GeraRelatorioFinalInventario " + Request["co_inventario"].ToString(), ConfigurationManager.ConnectionStrings["ConnectionStringModuloFarmacia"].ConnectionString);
            }

            doc.Database.Tables["Cabecalho"].SetDataSource((DataTable)hash["cabecalho"]);
            doc.Database.Tables["Corpo"].SetDataSource((DataTable)hash["corpo"]);

            //DSRelInventarioConferenciaFarmacia dsrelatorio = new DSRelInventarioConferenciaFarmacia();
            //dsrelatorio.Cabecalho = (DataTable)hash[""];
            //da.Fill(ds, "RelInventario");
            //doc.Database.Tables[0].SetDataSource(ds.Tables[0]);

            //doc.SetParameterValue("@FARMACIA", Label_Farmacia.Text);
            Session["relatoriofarmacia"] = doc;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "abrir", "window.open('FormRelatoriosCrystal.aspx');", true);
        }
    }
}