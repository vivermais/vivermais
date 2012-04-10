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
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;

namespace ViverMais.View.Farmacia
{
    public partial class FormElencoMedicamentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_ELENCO_MEDICAMENTO", Modulo.FARMACIA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    Session.Remove("medicamentos");
                    Session.Remove("subelencos");
                    Session.Remove("elenco");
                    ViverMais.Model.ElencoMedicamento elenco = null;

                    if (Request.QueryString["codigo"] != null)
                    {
                        elenco = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.ElencoMedicamento>(int.Parse(Request.QueryString["codigo"].ToString()));
                        tbxElenco.Text = elenco.Nome;
                        gridMedicamentos.DataSource = elenco.Medicamentos;
                        gridMedicamentos.DataBind();
                        gridSubElenco.DataSource = elenco.SubElencos;
                        gridSubElenco.DataBind();
                        //LinkButton_Excluir.Visible = true;
                    }

                    IList<ViverMais.Model.Medicamento> medicamentos;
                    IList<ViverMais.Model.SubElencoMedicamento> subelencos;
                    if (elenco != null)
                    {
                        //carrega no dropdownlist apenas os medicamentos e sub-grupos que não constam no elenco
                        medicamentos = Factory.GetInstance<IElencoMedicamento>().BuscarMedicamentosNaoContidosNoElenco<ViverMais.Model.Medicamento>(elenco.Codigo).OrderBy(m => m.Nome).ToList();
                        subelencos = Factory.GetInstance<IElencoMedicamento>().BuscarSubElencosNaoContidosNoElenco<ViverMais.Model.SubElencoMedicamento>(elenco.Codigo).OrderBy(se => se.Nome).ToList();
                        //gridMedicamentos.DataSource = elenco.Medicamentos;
                        //gridMedicamentos.DataBind();
                        Session["medicamentos"] = elenco.Medicamentos;

                        //gridSubElenco.DataSource = elenco.SubElencos;
                        //gridSubElenco.DataBind();
                        Session["subelencos"] = elenco.SubElencos;

                        Session["elenco"] = elenco;
                    }
                    else
                    {
                        //ou carrega todos caso seja um novo elenco
                        medicamentos = Factory.GetInstance<IMedicamento>().ListarTodosMedicamentos<ViverMais.Model.Medicamento>().OrderBy(m => m.Nome).ToList();
                        subelencos = Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<ViverMais.Model.SubElencoMedicamento>().OrderBy(se => se.Nome).ToList();
                    }

                    gridMedicamentos.DataSource = elenco != null ? elenco.Medicamentos : new List<Medicamento>();
                    gridMedicamentos.DataBind();
                    gridSubElenco.DataSource = elenco != null ? elenco.SubElencos : new List<SubElencoMedicamento>();
                    gridSubElenco.DataBind();

                    ddlMedicamento.Items.Add(new ListItem("Selecione...", "0"));
                    foreach (ViverMais.Model.Medicamento m in medicamentos)
                        ddlMedicamento.Items.Add(new ListItem(m.Nome, m.Codigo.ToString()));
                    ddlMedicamento.DataBind();

                    ddlSubElenco.Items.Add(new ListItem("Selecione...", "0"));
                    foreach (ViverMais.Model.SubElencoMedicamento s in subelencos)
                        ddlSubElenco.Items.Add(new ListItem(s.Nome, s.Codigo.ToString()));
                    ddlSubElenco.DataBind();

                    ddlMedicamento.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
                }
            }
        }

        void RecarregaMedicamentos()
        {
            IList<ViverMais.Model.Medicamento> medicamentos = Session["medicamentos"] != null ? (IList<ViverMais.Model.Medicamento>)Session["medicamentos"] : new List<ViverMais.Model.Medicamento>();
            IList<ViverMais.Model.Medicamento> m = Factory.GetInstance<IMedicamento>().ListarTodosMedicamentos<ViverMais.Model.Medicamento>();
            var result = from l in m where !medicamentos.Contains(l) select l;
            ddlMedicamento.Items.Clear();
            ddlMedicamento.Items.Add(new ListItem("Selecione...", "0"));
            foreach (ViverMais.Model.Medicamento med in result.ToList<ViverMais.Model.Medicamento>().OrderBy(p => p.Nome).ToList())
            {
                ddlMedicamento.Items.Add(new ListItem(med.Nome, med.Codigo.ToString()));
            }
            ddlMedicamento.DataBind();

        }

        void RecarregaSubElencos()
        {
            IList<ViverMais.Model.SubElencoMedicamento> subelencos = Session["subelencos"] != null ? (IList<ViverMais.Model.SubElencoMedicamento>)Session["subelencos"] : new List<ViverMais.Model.SubElencoMedicamento>();
            IList<ViverMais.Model.SubElencoMedicamento> subel = Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<ViverMais.Model.SubElencoMedicamento>();
            var result = from l in subel where !subelencos.Contains(l) select l;
            ddlSubElenco.Items.Clear();
            ddlSubElenco.Items.Add(new ListItem("Selecione...", "0"));
            foreach (ViverMais.Model.SubElencoMedicamento subelenco in result.ToList<ViverMais.Model.SubElencoMedicamento>().OrderBy(p => p.Nome).ToList())
            {
                ddlSubElenco.Items.Add(new ListItem(subelenco.Nome, subelenco.Codigo.ToString()));
            }
            ddlSubElenco.DataBind();
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            //pega o elenco atual da sessao
            IList<ViverMais.Model.Medicamento> medicamentos = Session["medicamentos"] != null ? (IList<ViverMais.Model.Medicamento>)Session["medicamentos"] : new List<ViverMais.Model.Medicamento>();
            //pega o medicamento selecionado no dropdownlist
            ViverMais.Model.Medicamento medicamento = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.Medicamento>(int.Parse(ddlMedicamento.SelectedValue));
            medicamentos.Add(medicamento);
            //remove o item para evitar insercao duplicada
            gridMedicamentos.DataSource = medicamentos;
            gridMedicamentos.DataBind();
            Session["medicamentos"] = medicamentos;
            RecarregaMedicamentos();

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            IList<ViverMais.Model.Medicamento> medicamentos = Session["medicamentos"] != null ? (IList<ViverMais.Model.Medicamento>)Session["medicamentos"] : new List<ViverMais.Model.Medicamento>();

            //if (medicamentos.Count() <= 0)
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('É necessário incluir pelo menos um medicamento neste elenco.');", true);
            // else
            //{
            ViverMais.Model.ElencoMedicamento elenco = Session["elenco"] != null ? (ViverMais.Model.ElencoMedicamento)Session["elenco"] : new ViverMais.Model.ElencoMedicamento();

            elenco.Nome = tbxElenco.Text;
            elenco.Medicamentos = new List<ViverMais.Model.Medicamento>();

            //IList<ViverMais.Model.Medicamento> medicamentos = Session["medicamentos"] != null ? (IList<ViverMais.Model.Medicamento>)Session["medicamentos"] : new List<ViverMais.Model.Medicamento>();
            foreach (ViverMais.Model.Medicamento m in medicamentos)
                elenco.Medicamentos.Add(m);

            elenco.SubElencos = new List<ViverMais.Model.SubElencoMedicamento>();
            IList<ViverMais.Model.SubElencoMedicamento> subelencos = Session["subelencos"] != null ? (IList<ViverMais.Model.SubElencoMedicamento>)Session["subelencos"] : new List<SubElencoMedicamento>();
            foreach (ViverMais.Model.SubElencoMedicamento se in subelencos)
                elenco.SubElencos.Add(se);

            if (elenco.Codigo == 0)//salva um novo
            {
                elenco.Medicamentos = new List<Medicamento>();
                elenco.SubElencos = new List<SubElencoMedicamento>();
                Factory.GetInstance<IFarmaciaServiceFacade>().Inserir(elenco);
                Factory.GetInstance<IFarmaciaServiceFacade>().Flush();
                elenco = Factory.GetInstance<IFarmaciaServiceFacade>().ReturnLastElementIncluded<ElencoMedicamento>("Codigo");
                elenco.Medicamentos = elenco.Medicamentos.Concat(medicamentos).ToList();
                elenco.SubElencos = elenco.SubElencos.Concat(subelencos).ToList();
                Factory.GetInstance<IFarmaciaServiceFacade>().Atualizar(elenco);
            }
            else //atualiza
                Factory.GetInstance<IFarmaciaServiceFacade>().Atualizar(elenco);

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Elenco de Medicamento salvo com sucesso!');location='FormExibeElenco.aspx';", true);
            //}
        }


        protected void gridMedicamentos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //pega o elenco atual da sessao
            IList<ViverMais.Model.Medicamento> medicamentos = (IList<ViverMais.Model.Medicamento>)Session["medicamentos"];
            int co_medicamento = int.Parse(gridMedicamentos.DataKeys[e.RowIndex]["Codigo"].ToString());
            ViverMais.Model.Medicamento m = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.Medicamento>(co_medicamento);
            int co_medicamentoremove = medicamentos.Select((Med, Index) => new { Index, Med }).Where(p => p.Med.Codigo == co_medicamento).First().Index;
            medicamentos.RemoveAt(co_medicamentoremove);
            //ddlMedicamento.Items.Add(new ListItem(m.Nome, m.Codigo.ToString()));
            gridMedicamentos.DataSource = medicamentos;
            gridMedicamentos.DataBind();
            Session["medicamentos"] = medicamentos;
            RecarregaMedicamentos();
        }

        protected void gridSubElenco_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //pega o elenco atual da sessao
            IList<ViverMais.Model.SubElencoMedicamento> subelencos = (IList<ViverMais.Model.SubElencoMedicamento>)Session["subelencos"];
            int co_subelenco = int.Parse(gridSubElenco.Rows[e.RowIndex].Cells[0].Text);
            ViverMais.Model.SubElencoMedicamento se = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.SubElencoMedicamento>(co_subelenco);
            subelencos.Remove(se);
            gridSubElenco.DataSource = subelencos;
            gridSubElenco.DataBind();
            Session["subelencos"] = subelencos;
            RecarregaSubElencos();
        }

        protected void btnAdicionar2_Click(object sender, EventArgs e)
        {
            //pega o elenco atual da sessao
            IList<ViverMais.Model.SubElencoMedicamento> subelencos = Session["subelencos"] != null ? (IList<ViverMais.Model.SubElencoMedicamento>)Session["subelencos"] : new List<ViverMais.Model.SubElencoMedicamento>();
            //pega o subelenco selecionado no dropdownlist
            ViverMais.Model.SubElencoMedicamento subelenco = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.SubElencoMedicamento>(int.Parse(ddlSubElenco.SelectedValue));
            subelencos.Add(subelenco);
            gridSubElenco.DataSource = subelencos;
            gridSubElenco.DataBind();
            Session["subelencos"] = subelencos;
            RecarregaSubElencos();
        }

        ///// <summary>
        ///// Exclui o elenco do módulo farmácia
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void OnClick_ExcluirElenco(object sender, EventArgs e)
        //{
        //    int co_elenco;
        //    if (int.TryParse(Request["codigo"].ToString(), out co_elenco))
        //    {
        //        string retorno = Factory.GetInstance<IMedicamento>().ExcluirElencoMedicamento(co_elenco);

        //        if (retorno.Equals("ok"))
        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Elenco excluído com sucesso.');location='FormExibeElenco.aspx';", true);
        //        else
        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + retorno + "');", true);
        //    }
        //}
    }
}
