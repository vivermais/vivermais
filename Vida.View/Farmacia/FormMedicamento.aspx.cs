﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento.Misc;
using System.Collections;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Farmacia
{
    public partial class FormMedicamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ( ! IsPostBack )
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_MEDICAMENTO",Modulo.FARMACIA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    CarregaUnidadesMedida();
                    CarregaElencoMedicamento(-1);
                    CarregaSubElencoMedicamento(-1);

                    int temp;
                    if (Request["co_medicamento"] != null && int.TryParse(Request["co_medicamento"].ToString(), out temp))
                    {
                        imgsalvar.Alt = "Alterar";
                        imgsalvar.Src = "img/btn/alterar1.png";
                        imgsalvar.Attributes.Add("onmouseover", "this.src='img/btn/alterar2.png';");
                        imgsalvar.Attributes.Add("onmouseout", "this.src='img/btn/alterar1.png';");

                        Button_Salvar.OnClientClick = "javascript:if (Page_ClientValidate()) return confirm('Tem certeza que deseja alterar o registro deste medicamento ?'); return false;";

                        try
                        {
                            IMedicamento im = Factory.GetInstance<IMedicamento>();
                            Medicamento m = im.BuscarPorCodigo<Medicamento>(int.Parse(Request["co_medicamento"].ToString()));

                            TextBox_Codigo.Text = m.CodMedicamento;
                            TextBox_Nome.Text = m.Nome;
                            DropDownList_UnidadeMedida.SelectedValue = m.UnidadeMedida.Codigo.ToString();

                            if (m.Ind_Antibio)
                            {
                                RadioButton_Antibiotico.Checked = true;
                                RadioButton_NaoAntibiotico.Checked = false;
                            }

                            CarregaElencoMedicamento(m.Codigo);
                            CarregaSubElencoMedicamento(m.Codigo);
                        }
                        catch (Exception f)
                        {
                            throw f;
                        }
                    }
                    else 
                    {
                        imgsalvar.Alt = "Salvar";
                        imgsalvar.Src = "img/btn/salvar1.png";
                        imgsalvar.Attributes.Add("onmouseover", "this.src='img/btn/salvar2.png';");
                        imgsalvar.Attributes.Add("onmouseout", "this.src='img/btn/salvar1.png';");
                    }
                }
            }
        }

        /// <summary>
        /// Carrega os elencos do medicamento
        /// </summary>
        /// <param name="co_medicamento">código do medicamento</param>
        private void CarregaElencoMedicamento(int co_medicamento)
        {
            if (co_medicamento != -1)
            {
                IMedicamento im = Factory.GetInstance<IMedicamento>();
                Medicamento m = im.BuscarPorCodigo<Medicamento>(co_medicamento);
                IList<ViverMais.Model.ElencoMedicamento> elencos = Factory.GetInstance<IElencoMedicamento>().ListarTodos<ElencoMedicamento>()
                    .Where(p=>p.Medicamentos.Contains(m)).ToList();
                GridView_ElencoMedicamento.DataSource = elencos;
            }
            else
                GridView_ElencoMedicamento.DataSource = new List<ElencoMedicamento>();

            GridView_ElencoMedicamento.DataBind();
        }

        /// <summary>
        /// Paginação dos elencos do medicamento escolhido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_PaginacaoElenco(object sender, GridViewPageEventArgs e)
        {
            CarregaElencoMedicamento(int.Parse(Request["co_medicamento"].ToString()));
            GridView_ElencoMedicamento.PageIndex = e.NewPageIndex;
            GridView_ElencoMedicamento.DataBind();
        }

        /// <summary>
        /// Carrega os sub-elencos do medicamento
        /// </summary>
        /// <param name="co_medicamento">código do medicamento</param>
        private void CarregaSubElencoMedicamento(int co_medicamento)
        {
            if (co_medicamento != -1)
            {
                IMedicamento im = Factory.GetInstance<IMedicamento>();
                Medicamento m = im.BuscarPorCodigo<Medicamento>(co_medicamento);
                IList<SubElencoMedicamento> subelencos = Factory.GetInstance<IMedicamento>().BuscarSubElencos<SubElencoMedicamento>(m.Codigo);
                GridView_SubElencoMedicamento.DataSource = subelencos;
            }
            else
                GridView_SubElencoMedicamento.DataSource = new List<SubElencoMedicamento>();

            GridView_SubElencoMedicamento.DataBind();
        }

        /// <summary>
        /// Paginação dos sub-elencos do medicamento escolhido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_PaginacaoSubElenco(object sender, GridViewPageEventArgs e)
        {
            CarregaSubElencoMedicamento(int.Parse(Request["co_medicamento"].ToString()));
            GridView_SubElencoMedicamento.PageIndex = e.NewPageIndex;
            GridView_SubElencoMedicamento.DataBind();
        }

        /// <summary>
        /// Carrega as unidades de medidas existentes
        /// </summary>
        private void CarregaUnidadesMedida()
        {
            IUnidadeMedidaMedicamento ium = Factory.GetInstance<IUnidadeMedidaMedicamento>();
            //IList<UnidadeMedidaMedicamento> lum = ium.ListarTodos<UnidadeMedidaMedicamento>().Where(p=>p.Ativo == true).OrderBy(p => p.Nome).ToList();
            IList<UnidadeMedidaMedicamento> lum = ium.ListarTodos<UnidadeMedidaMedicamento>().OrderBy(p => p.Nome).ToList();

            foreach (UnidadeMedidaMedicamento um in lum)
                DropDownList_UnidadeMedida.Items.Add(new ListItem(um.Nome, um.Codigo.ToString()));
        }

        /// <summary>
        /// Salva o medicamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Salvar(object sender, EventArgs e) 
        {
            try
            {
                IMedicamento iMedicamento = Factory.GetInstance<IMedicamento>();
                Medicamento medicamento   = Request["co_medicamento"] != null ? iMedicamento.BuscarPorCodigo<Medicamento>(int.Parse(Request["co_medicamento"].ToString())) : new Medicamento();

                if (iMedicamento.VerificaDuplicidadePorCodigo<Medicamento>(TextBox_Codigo.Text, medicamento.Codigo) != null)
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe um medicamento com o código informado! Por favor, informe outro código.');", true);
                else
                {
                    medicamento.CodMedicamento = TextBox_Codigo.Text;
                    medicamento.Nome = TextBox_Nome.Text;
                    medicamento.UnidadeMedida = Factory.GetInstance<IUnidadeMedidaMedicamento>().BuscarPorCodigo<UnidadeMedidaMedicamento>(int.Parse(DropDownList_UnidadeMedida.SelectedValue));
                    medicamento.Ind_Antibio = RadioButton_Antibiotico.Checked ? true : false;
                    medicamento.PertenceARede = true;

                    iMedicamento.Salvar(medicamento);

                    Factory.GetInstance<IFarmaciaServiceFacade>().Salvar(new LogFarmacia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, EventoFarmacia.MANTER_MEDICAMENTO,
                        "id medicamento:" + medicamento.Codigo));
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Medicamento salvo com sucesso!');location='Default.aspx';", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
            }
        }
    }
}
