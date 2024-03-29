﻿using System;
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
using ViverMais.ServiceFacade.ServiceFacades.Farmacia;
using ViverMais.DAO;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;

namespace ViverMais.View.Farmacia
{
    public partial class RelatoriosFarmacia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IFarmacia ifarmacia = Factory.GetInstance<IFarmacia>();
                IList<ViverMais.Model.Farmacia> farmacias = ifarmacia.ListarTodos<ViverMais.Model.Farmacia>("Nome", true);
                ddlFarmaciaMovimentacao.Items.Add(new ListItem("Selecione", "-1"));
                ddlFarmaciaPosicaoEstoqueLote.Items.Add(new ListItem("Selecione", "-1"));
                ddlFarmaciaLoteAVencer.Items.Add(new ListItem("Selecione", "-1"));
                ddlFarmaciaConsumoMedioMensal.Items.Add(new ListItem("Selecione", "-1"));
                ddlFarmaciaProducaoUsuario.Items.Add(new ListItem("Selecione", "-1"));
                foreach (ViverMais.Model.Farmacia farmacia in farmacias)
                {
                    ddlFarmaciaMovimentacao.Items.Add(new ListItem(farmacia.Nome, farmacia.Codigo.ToString()));
                    ddlFarmaciaPosicaoEstoqueLote.Items.Add(new ListItem(farmacia.Nome, farmacia.Codigo.ToString()));
                    ddlFarmaciaLoteAVencer.Items.Add(new ListItem(farmacia.Nome, farmacia.Codigo.ToString()));
                    ddlFarmaciaConsumoMedioMensal.Items.Add(new ListItem(farmacia.Nome, farmacia.Codigo.ToString()));
                    ddlFarmaciaProducaoUsuario.Items.Add(new ListItem(farmacia.Nome, farmacia.Codigo.ToString()));
                }
                IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
                IList<ViverMais.Model.Distrito> distritos = iViverMais.ListarTodos<ViverMais.Model.Distrito>();
                ddlDistritoConsolidadoRM.Items.Add(new ListItem("Selecione...", "-1"));
                foreach (ViverMais.Model.Distrito distrito in distritos)
                {
                    ddlDistritoConsolidadoRM.Items.Add(new ListItem(distrito.Nome, distrito.Codigo.ToString()));
                }
            }
            ddlFarmaciaProducaoUsuario.SelectedIndexChanged += new EventHandler(ddlFarmaciaProducaoUsuario_SelectedIndexChanged);
        }

        void ddlFarmaciaProducaoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException("Precisa filtar os usuários de uma determinada farmácia");
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

        protected void btnpesquisar_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbxDataMovimentacao.Text))
                Redirect("RelatorioMovimentacaoDiaria.aspx?id_farmacia=" + ddlFarmaciaMovimentacao.SelectedValue + "&data=" + tbxDataMovimentacao.Text, "_blank", "");
            if (ddlFarmaciaPosicaoEstoqueLote.SelectedValue != "-1")
                Redirect("RelatorioPosicaoEstoqueLote.aspx?id_farmacia=" + ddlFarmaciaPosicaoEstoqueLote.SelectedValue, "_blank", "");
            if (!string.IsNullOrEmpty(tbxDataValidadeAVencer.Text))
                Redirect("RelatorioLoteAVencer.aspx?id_farmacia=" + ddlFarmaciaLoteAVencer.SelectedValue + "&data=" + tbxDataValidadeAVencer.Text, "_blank", "");
            if (!string.IsNullOrEmpty(tbxDataInicialConsumoMedioMensal.Text))
                Redirect("RelatorioConsumoMedioMensal.aspx?id_farmacia=" + ddlFarmaciaConsumoMedioMensal.SelectedValue + "&data_inicial=" + tbxDataInicialConsumoMedioMensal.Text + "&data_final=" + tbxDataFinalConsumoMedioMensal.Text, "_blank", "");
            //if (ddlUsuarioProducaoUsuario.SelectedValue != "-1")
            //Redirect("RelatorioProducaoUsuario.aspx?id_usuario=" + ddlUsuarioProducaoUsuario.SelectedValue + "&data=" + tbxDataProducaoUsuario.Text, "_blank", "");
            if (ddlDistritoConsolidadoRM.SelectedValue != "-1")
                Redirect("RelatorioConsolidadoRM.aspx?id_distrito=" + ddlDistritoConsolidadoRM.SelectedValue + "&mes=" + tbxMesConsolidadoRM.Text + "&ano=" + tbxAnoConsolidadoRM.Text, "_blank", "");
            if (!string.IsNullOrEmpty(tbxNumeroLote.Text))
                Redirect("RelatorioNotaFiscalLote.aspx?id_lote=" + tbxNumeroLote.Text, "_blank", "");
            if (!string.IsNullOrEmpty(tbxDataInicialValorUnitMedicamento.Text))
                Redirect("RelatorioValorUnitarioMedicamento.aspx?data_inicial=" + tbxDataInicialValorUnitMedicamento.Text + "&data_final=" + tbxDataFinalValorUnitMedicamento.Text, "_blank", "");

            ddlFarmaciaMovimentacao.SelectedValue = "-1";
            tbxDataMovimentacao.Text = string.Empty;
            ddlFarmaciaPosicaoEstoqueLote.SelectedValue = "-1";
            ddlFarmaciaLoteAVencer.SelectedValue = "-1";
            tbxDataValidadeAVencer.Text = string.Empty;
            tbxDataInicialConsumoMedioMensal.Text = string.Empty;
            ddlFarmaciaConsumoMedioMensal.SelectedValue = "-1";
            tbxDataInicialConsumoMedioMensal.Text = string.Empty;
            tbxDataFinalConsumoMedioMensal.Text = string.Empty;
            ddlDistritoConsolidadoRM.SelectedValue = "-1";
            tbxMesConsolidadoRM.Text = string.Empty;
            tbxAnoConsolidadoRM.Text = string.Empty;
            tbxNumeroLote.Text = string.Empty;
            tbxDataInicialValorUnitMedicamento.Text = string.Empty;
            tbxDataFinalValorUnitMedicamento.Text = string.Empty;
        }
    }
}
