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
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Urgencia
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_MEDICAMENTO_PRESCRICAO", Modulo.URGENCIA))
                {
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                }
                else
                {
                    if (Request.QueryString["codigo"] != null)
                    {
                        int codigo = int.Parse(Request.QueryString["codigo"].ToString());
                        Medicamento medicamento = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<Medicamento>(codigo);
                        tbxMedicamento.Text = medicamento.Nome;
                        this.chckMedicamento.Checked = medicamento.EMedicamento;
                        ViewState["co_medicamento"] = medicamento.Codigo.ToString();
                    }
                }
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            Medicamento medicamento;
            IMedicamento iMedicamento = Factory.GetInstance<IMedicamento>();
            if (ViewState["co_medicamento"] != null)
                medicamento = iMedicamento.BuscarPorCodigo<Medicamento>(int.Parse(ViewState["co_medicamento"].ToString()));
            else
            {
                medicamento = new Medicamento();
                medicamento.PertenceARede = false;
                medicamento.Ind_Antibio = false;
                
                //90000
                medicamento.CodMedicamento = iMedicamento.GerarCodigoMedicamentoUrgencia();
                    //"URG" + (iMedicamento.GerarCodigoMedicamentoUrgencia<int>() + 1).ToString();
            }

            medicamento.Nome = tbxMedicamento.Text.ToUpper();
            medicamento.EMedicamento = this.chckMedicamento.Checked;

            if (medicamento.EMedicamento)
            {
                //usa uma unidade de medida qualquer porque esta não é relevante para o Urgência
                medicamento.UnidadeMedida = iMedicamento.BuscarPorCodigo<UnidadeMedidaMedicamento>(UnidadeMedidaMedicamento.UNIDADE);
                msg = "Medicamento";
            }
            else
            {
                medicamento.UnidadeMedida = iMedicamento.BuscarPorCodigo<UnidadeMedidaMedicamento>(UnidadeMedidaMedicamento.INDEFINIDA);
                msg = "Prescrição";
            }

            iMedicamento.Salvar(medicamento);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + msg + " salvo com sucesso!');location='FormBuscaMedicamento.aspx';", true);
            //Response.Redirect("FormBuscaMedicamento.aspx");
        }
    }
}
