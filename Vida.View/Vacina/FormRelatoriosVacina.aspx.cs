using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Vacina;
using AjaxControlToolkit;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using System.IO;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;

namespace ViverMais.View.Vacina
{
    public partial class FormRelatoriosVacina : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.EAS.WUC_LinkButton_PesquisarCNES.Click += new EventHandler(OnClick_PesquisarEstabelecimento);
            this.EAS.WUC_LinkButton_PesquisarNomeFantasia.Click += new EventHandler(OnClick_PesquisarEstabelecimento);

            this.InserirTrigger(this.EAS.WUC_LinkButton_PesquisarCNES.UniqueID, "Click", this.UpdatePanel_ProducaoDiaria);
            this.InserirTrigger(this.EAS.WUC_LinkButton_PesquisarNomeFantasia.UniqueID, "Click", this.UpdatePanel_ProducaoDiaria);

            if (!IsPostBack)
            {
                TextBox_DataInicioProducao.Text = DateTime.Today.ToString("dd/MM/yyyy");
                TextBox_DataTerminoProducao.Text = DateTime.Today.ToString("dd/MM/yyyy");
                ISeguranca iSeguranca = Factory.GetInstance<ISeguranca>();

                foreach (AccordionPane ap in Accordion1.Panes)
                {
                    if (!iSeguranca.VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, ap.ID, Modulo.VACINA))
                        ap.Visible = false;
                }

                IDistrito iDistrito = Factory.GetInstance<IDistrito>();
                this.DropDownList_Distrito.DataSource = iDistrito.BuscarPorMunicipio<Distrito>(Municipio.SALVADOR);
                this.DropDownList_Distrito.DataBind();
                this.DropDownList_Distrito.Items.Insert(0, new ListItem("SELECIONE...", "-1"));
            }
        }

        private void InserirTrigger(string idcontrole, string nomeevento, UpdatePanel updatepanel)
        {
            AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
            trigger.ControlID = idcontrole;
            trigger.EventName = nomeevento;
            updatepanel.Triggers.Add(trigger);
        }

        private void ZerarSelecao()
        {
            TextBox_DataInicioProducao.Text = DateTime.Today.ToString("dd/MM/yyyy");
            TextBox_DataTerminoProducao.Text = DateTime.Today.ToString("dd/MM/yyyy");
            this.CheckBox_SelecionaTodasUnidadesProducaoDiaria.Checked = false;
        }

        protected void OnClick_PesquisarEstabelecimento(object sender, EventArgs e)
        {
            this.ZerarSelecao();
            this.CheckBoxList_UnidadesProducaoDiaria.DataSource = //this.EAS.WUC_EstabelecimentosPesquisados;
                Factory.GetInstance<ISalaVacina>().BuscarPorUnidadesPesquisadas<ViverMais.Model.EstabelecimentoSaude>(this.EAS.WUC_EstabelecimentosPesquisados);
                //.Where(p=>p.Bairro != null && p.Bairro.Distrito != null && p.Bairro.Distrito.Municipio.Codigo == Municipio.SALVADOR).ToList());
            this.CheckBoxList_UnidadesProducaoDiaria.DataBind();
            this.Panel_Unidades.Visible = true;
            this.UpdatePanel_ProducaoDiaria.Update();
        }

        protected void OnSelectedIndexChanged_CarregaUnidades(object sender, EventArgs e)
        {
            if (this.DropDownList_Distrito.SelectedValue != "-1")
            {
                this.ZerarSelecao();
                this.CheckBoxList_UnidadesProducaoDiaria.DataSource = Factory.GetInstance<ISalaVacina>().BuscarUnidadesPorDistritoSala<ViverMais.Model.EstabelecimentoSaude>(int.Parse(this.DropDownList_Distrito.SelectedValue));
                this.CheckBoxList_UnidadesProducaoDiaria.DataBind();
                this.Panel_Unidades.Visible = true;
            }
            else
                this.Panel_Unidades.Visible = false;
        }


        /// <summary>
        /// Marca ou desmarca os estabelecimentos do distrito
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnCheckedChanged_SelecionaTodasUnidadesProducaoDiaria(object sender, EventArgs e)
        {
            if (CheckBox_SelecionaTodasUnidadesProducaoDiaria.Checked)
            {
                foreach (ListItem itemunidade in CheckBoxList_UnidadesProducaoDiaria.Items)
                    itemunidade.Selected = true;
            }
            else
            {
                foreach (ListItem itemunidade in CheckBoxList_UnidadesProducaoDiaria.Items)
                    itemunidade.Selected = false;
            }
        }

        /// <summary>
        /// Gera o relatório para as unidades escolhidas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_GerarRelatorioProducaoDiaria(object sender, EventArgs e)
        {
            List<ViverMais.Model.EstabelecimentoSaude> estabelecimentos = new List<ViverMais.Model.EstabelecimentoSaude>();
            IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();

            foreach (ListItem itemunidade in CheckBoxList_UnidadesProducaoDiaria.Items)
            {
                if (itemunidade.Selected)
                    estabelecimentos.Add(iEstabelecimento.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(itemunidade.Value));
            }

            if (estabelecimentos.Count() > 0)
            {
                MemoryStream ms = Factory.GetInstance<IRelatorioVacina>().ObterRelatorioProducao<ViverMais.Model.EstabelecimentoSaude>(estabelecimentos.OrderBy(p=>p.NomeFantasia).ToList(), DateTime.Parse(TextBox_DataInicioProducao.Text), DateTime.Parse(TextBox_DataTerminoProducao.Text));
                Session["doc_download"] = ms;
                Response.Redirect("FormDownload.aspx?filename=RelatorioProducao_" + TextBox_DataInicioProducao.Text.Replace('/', '-') + "_a_" + TextBox_DataTerminoProducao.Text.Replace('/', '-') + ".xls");
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "window.open('FormDownload.aspx?filename=RelatorioProducao_" + TextBox_DataInicioProducao.Text.Replace('/', '-') + "_a_" + TextBox_DataTerminoProducao.Text.Replace('/', '-') + ".xls');", true);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Selecione pelo menos um dos estabelecimentos de saúde disponíveis.');", true);
        }

        /// <summary>
        /// Cancela a geração do relatório de produção diária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_CancelarProducaoDiaria(object sender, EventArgs e)
        {
            this.Panel_Unidades.Visible = false;
        }
    }
}
