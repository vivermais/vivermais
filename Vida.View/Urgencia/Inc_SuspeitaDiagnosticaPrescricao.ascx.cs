﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;

namespace ViverMais.View.Urgencia
{
    public partial class Inc_SuspeitaDiagnosticaPrescricao : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                CarregaGridCid((IList<Cid>)Session["ListaCid"]);
                CarregaGridPrescricaoMedicamento(RetornaListaMedicamento());
                CarregaGridProcedimento(RetornaListaProcedimento());
                CarregaGridProcedimentoNaoFaturavel(RetornaListaProcedimentoNaoFaturavel());

                IList<string> codcids = Factory.GetInstance<ICid>().ListarGrupos();
                ddlGrupoCid.Items.Add(new ListItem("Selecione...", "0"));
                foreach (string c in codcids)
                    ddlGrupoCid.Items.Add(new ListItem(c, c));

                CarregaViasAdministracao();

                //Carrega os medicamentos do elenco do PA
                IList<Medicamento> lm = Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<Medicamento>().OrderBy(p => p.Nome).ToList();

                ddlMedicamentos.Items.Add(new ListItem("Selecione...", "0"));
                foreach (ViverMais.Model.Medicamento m in lm)
                {
                    ListItem item = new ListItem(m.Nome, m.Codigo.ToString());
                    ddlMedicamentos.Items.Add(item);
                }

                IList<ProcedimentoNaoFaturavel> lpnf = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ProcedimentoNaoFaturavel>().Where(p => p.Status == 'A').OrderBy(p => p.Nome).ToList();
                foreach (ProcedimentoNaoFaturavel pnf in lpnf)
                    DropDownList_ProcedimentosNaoFaturaveis.Items.Add(new ListItem(pnf.Nome, pnf.Codigo.ToString()));

                DropDownList_ProcedimentosNaoFaturaveis.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
                ddlMedicamentos.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
            }
        }

        /// <summary>
        /// Carrega os tipos de via de administração para medicamento
        /// </summary>
        private void CarregaViasAdministracao()
        {
            IList<ViaAdministracao> lva = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ViaAdministracao>().OrderBy(p => p.Nome).ToList();

            foreach (ViaAdministracao va in lva)
            {
                DropDownList_ViaAdministracaoMedicamento.Items.Add(new ListItem(va.Nome, va.Codigo.ToString()));
            }
        }

        /// <summary>
        /// Retorna a lista temporária de procedimentos
        /// </summary>
        /// <returns></returns>
        public IList<PrescricaoProcedimento> RetornaListaProcedimento()
        {
            return Session["ListaProcedimento"] != null ? (IList<PrescricaoProcedimento>)Session["ListaProcedimento"] : new List<PrescricaoProcedimento>();
        }

        /// <summary>
        /// Retorna a lista temporária de medicamentos
        /// </summary>
        /// <returns></returns>
        public IList<PrescricaoMedicamento> RetornaListaMedicamento()
        {
            return Session["ListaMedicamento"] != null ? (IList<PrescricaoMedicamento>)Session["ListaMedicamento"] : new List<PrescricaoMedicamento>();
        }

        /// <summary>
        /// Retorna a lista temporária de procedimentos não-faturáveis
        /// </summary>
        /// <returns></returns>
        public IList<PrescricaoProcedimentoNaoFaturavel> RetornaListaProcedimentoNaoFaturavel()
        {
            return Session["ListaProcedimentoNaoFaturavel"] != null ? (IList<PrescricaoProcedimentoNaoFaturavel>)Session["ListaProcedimentoNaoFaturavel"] : new List<PrescricaoProcedimentoNaoFaturavel>();
        }

        /// <summary>
        /// Carrega o grid de procedimento
        /// </summary>
        /// <param name="iList"></param>
        private void CarregaGridProcedimento(IList<PrescricaoProcedimento> iList)
        {
            GridView_Procedimento.DataSource = iList;
            GridView_Procedimento.DataBind();
        }

        /// <summary>
        /// Carrega o grid de medicamento para a prescrição corrente
        /// </summary>
        /// <param name="iList"></param>
        private void CarregaGridPrescricaoMedicamento(IList<PrescricaoMedicamento> iList)
        {
            gridMedicamentos.DataSource = iList;
            gridMedicamentos.DataBind();
        }

        /// <summary>
        /// Carrega o grid de suspeita diagnóstica
        /// </summary>
        /// <param name="iList"></param>
        private void CarregaGridCid(IList<Cid> iList)
        {
            gridCid.DataSource = iList;
            gridCid.DataBind();
        }

        /// <summary>
        /// Carrega a lista de procedimentos não-faturáveis para a prescrição válida
        /// </summary>
        /// <param name="p"></param>
        private void CarregaGridProcedimentoNaoFaturavel(IList<PrescricaoProcedimentoNaoFaturavel> lista)
        {
            GridView_ProcedimentosNaoFaturavel.DataSource = lista;
            GridView_ProcedimentosNaoFaturavel.DataBind();
        }

        /// <summary>
        /// Adiciona um CID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdicionarCid_Click(object sender, EventArgs e)
        {
            IList<Cid> lista = Session["ListaCid"] != null ? (IList<Cid>)Session["ListaCid"] : new List<Cid>();

            if (lista.Where(p => p.Codigo == ddlCid.SelectedValue).FirstOrDefault() == null)
                lista.Add(Factory.GetInstance<ICid>().BuscarPorCodigo<Cid>(ddlCid.SelectedValue));

            Session["ListaCid"] = lista;
            CarregaGridCid(lista);
        }

        /// <summary>
        /// Deleta uma linha específica do gridview de CID's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridCid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            IList<Cid> lista = Session["ListaCid"] != null ? (IList<Cid>)Session["ListaCid"] : new List<Cid>();

            lista.RemoveAt(e.RowIndex);

            Session["ListaCid"] = lista;
            CarregaGridCid(lista);
        }

        /// <summary>
        /// Busca os CID's de acordo com seu código
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_BuscarCids(object sender, EventArgs e)
        {
            ddlCid.Items.Clear();
            ddlCid.Items.Add(new ListItem("Selecione...", "0"));

            IList<ViverMais.Model.Cid> lc = Factory.GetInstance<ICid>().BuscarPorGrupo<ViverMais.Model.Cid>(ddlGrupoCid.SelectedValue.ToString());
            foreach (ViverMais.Model.Cid c in lc)
            {
                ListItem item = new ListItem(c.Nome, c.Codigo.ToString());
                ddlCid.Items.Add(item);
            }

            ddlCid.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
        }

        /// <summary>
        /// Busca os CID's de acordo com o código digitado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_BuscarCID(object sender, EventArgs e)
        {
            ddlCid.Items.Clear();
            ddlCid.Items.Add(new ListItem("Selecione...", "0"));
            TextBox_CID.Text = TextBox_CID.Text.ToUpper();

            Cid c = Factory.GetInstance<ICid>().BuscarPorCodigo<Cid>(TextBox_CID.Text);

            if (c != null)
            {
                ListItem item = new ListItem(c.Nome, c.Codigo.ToString());
                ddlCid.Items.Add(item);
            }

            ddlCid.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
        }

        /// <summary>
        /// Carrega os procedimentos para o 'Cid' escolhido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_CarregaProcedimentos(object sender, EventArgs e)
        {
            DropDownList_Procedimento.Items.Clear();
            DropDownList_Procedimento.Items.Add(new ListItem("Selecione...", "-1"));

            if (ddlCid.SelectedValue != "-1")
            {
                IList<ProcedimentoCid> lpc = Factory.GetInstance<IProcedimento>().BuscarPorCid<ProcedimentoCid>(ddlCid.SelectedValue).OrderBy(p => p.Procedimento.Nome).ToList();
                foreach (ProcedimentoCid pc in lpc)
                {
                    ListItem item = new ListItem(pc.Procedimento.Nome, pc.Procedimento.Codigo.ToString());
                    DropDownList_Procedimento.Items.Add(item);
                }
                DropDownList_Procedimento.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
            }
        }

        /// <summary>
        /// Adiciona o medicamento na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdicionarMedicamento_Click(object sender, EventArgs e)
        {
            IList<PrescricaoMedicamento> lista = Session["ListaMedicamento"] != null ? (IList<PrescricaoMedicamento>)Session["ListaMedicamento"] : new List<PrescricaoMedicamento>();

            if (lista.Where(p => p.Medicamento.ToString() == ddlMedicamentos.SelectedValue).FirstOrDefault() == null)
            {
                PrescricaoMedicamento pm = new PrescricaoMedicamento();
                pm.ObjetoMedicamento = Factory.GetInstance<IMedicamento>().BuscarPorCodigo<Medicamento>(int.Parse(ddlMedicamentos.SelectedValue));
                pm.Medicamento = pm.ObjetoMedicamento.Codigo;
                pm.Intervalo = tbxIntervaloMedicamento.Text;
                pm.Observacao = TextBox_ObservacaoPrescricaoMedicamento.Text;

                if (DropDownList_ViaAdministracaoMedicamento.SelectedValue != "-1")
                    pm.ViaAdministracao = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ViaAdministracao>(int.Parse(DropDownList_ViaAdministracaoMedicamento.SelectedValue));

                if (DropDownList_FormaAdministracaoMedicamento.SelectedValue != "-1")
                    pm.FormaAdministracao = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<FormaAdministracao>(int.Parse(DropDownList_FormaAdministracaoMedicamento.SelectedValue));

                lista.Add(pm);

                Session["ListaMedicamento"] = lista;
                CarregaGridPrescricaoMedicamento(lista);
                OnClick_CancelarMedicamento(sender, e);
            }
        }

        /// <summary>
        /// Adiciona o procedimento na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_AdicionarProcedimento(object sender, EventArgs e)
        {
            IList<PrescricaoProcedimento> lpp = RetornaListaProcedimento();

            if (lpp.Where(p => p.Procedimento.Codigo == DropDownList_Procedimento.SelectedValue).FirstOrDefault() == null)
            {
                PrescricaoProcedimento pp = new PrescricaoProcedimento();

                pp.CodigoProcedimento = DropDownList_Procedimento.SelectedValue;
                pp.Procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(DropDownList_Procedimento.SelectedValue);
                pp.Intervalo = TextBox_IntervaloProcedimento.Text;

                lpp.Add(pp);
                Session["ListaProcedimento"] = lpp;

                CarregaGridProcedimento(lpp);
                OnClick_CancelarProcedimento(sender, e);
            }
        }

        /// <summary>
        /// Adiciona um procedimento não-faturável na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_AdicionarProcedimentoNaoFaturavel(object sender, EventArgs e)
        {
            IList<PrescricaoProcedimentoNaoFaturavel> lista = RetornaListaProcedimentoNaoFaturavel();

            if (lista.Where(p => p.Procedimento.Codigo == int.Parse(DropDownList_ProcedimentosNaoFaturaveis.SelectedValue)).FirstOrDefault() == null)
            {
                PrescricaoProcedimentoNaoFaturavel ppnf = new PrescricaoProcedimentoNaoFaturavel();
                ppnf.Procedimento = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ProcedimentoNaoFaturavel>(int.Parse(DropDownList_ProcedimentosNaoFaturaveis.SelectedValue));
                ppnf.Intervalo = TextBox_IntervaloProcedimentoNaoFaturavel.Text;
                lista.Add(ppnf);

                Session["ListaProcedimentoNaoFaturavel"] = lista;
                CarregaGridProcedimentoNaoFaturavel(lista);

                DropDownList_ProcedimentosNaoFaturaveis.SelectedValue = "-1";
                TextBox_IntervaloProcedimentoNaoFaturavel.Text = "";
            }
        }

        /// <summary>
        /// Deleta o medicamento na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridMedicamentos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            IList<PrescricaoMedicamento> lista = Session["ListaMedicamento"] != null ? (IList<PrescricaoMedicamento>)Session["ListaMedicamento"] : new List<PrescricaoMedicamento>();
            lista.RemoveAt(e.RowIndex);
            Session["ListaMedicamento"] = lista;

            CarregaGridPrescricaoMedicamento(lista);
        }

        /// <summary>
        /// Deleta o procedimento adicionado na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDeleting_DeletarProcedimento(object sender, GridViewDeleteEventArgs e)
        {
            IList<PrescricaoProcedimento> lpp = RetornaListaProcedimento();
            lpp.RemoveAt(e.RowIndex);
            Session["ListaProcedimento"] = lpp;

            CarregaGridProcedimento(lpp);
        }

        /// <summary>
        /// Deleta o procedimento não-faturável adicionado na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDeleting_ExcluirProcedimentoNaoFaturavel(object sender, GridViewDeleteEventArgs e)
        {
            IList<PrescricaoProcedimentoNaoFaturavel> lista = RetornaListaProcedimentoNaoFaturavel();
            lista.RemoveAt(e.RowIndex);
            Session["ListaProcedimentoNaoFaturavel"] = lista;

            CarregaGridProcedimentoNaoFaturavel(lista);
        }

        /// <summary>
        /// Cancela a inserção do procedimento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_CancelarProcedimento(object sender, EventArgs e)
        {
            DropDownList_Procedimento.SelectedValue = "-1";
            TextBox_IntervaloProcedimento.Text = "";
        }

        /// <summary>
        /// Cancela a inserção do medicamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_CancelarMedicamento(object sender, EventArgs e)
        {
            ddlMedicamentos.SelectedValue = "0";
            TextBox_ObservacaoPrescricaoMedicamento.Text = "";
            tbxIntervaloMedicamento.Text = "";
            DropDownList_ViaAdministracaoMedicamento.SelectedValue = "-1";
            OnSelectedIndexChanged_CarregaFormaAdministracaoMedicamento(sender, e);
        }

        /// <summary>
        /// Carrega as formas de administração a partir da via escolhida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_CarregaFormaAdministracaoMedicamento(object sender, EventArgs e)
        {
            ViaAdministracao va = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ViaAdministracao>(int.Parse(DropDownList_ViaAdministracaoMedicamento.SelectedValue));
            DropDownList_FormaAdministracaoMedicamento.Items.Clear();
            DropDownList_FormaAdministracaoMedicamento.Items.Add(new ListItem("Selecione...", "-1"));

            if (va != null)
            {
                foreach (FormaAdministracao fa in va.FormasAdministracao)
                    DropDownList_FormaAdministracaoMedicamento.Items.Add(new ListItem(fa.Nome, fa.Codigo.ToString()));
            }
        }
    }
}