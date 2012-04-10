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
    public partial class FormSubElencoMedicamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_SUB_ELENCO_MEDICAMENTO", Modulo.FARMACIA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    Session.Remove("medicamentos");
                    Session.Remove("elencos");
                    Session.Remove("subelenco");
                    ViverMais.Model.SubElencoMedicamento subelenco = null;

                    if (Request.QueryString["codigo"] != null)
                    {
                        subelenco = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.SubElencoMedicamento>(int.Parse(Request.QueryString["codigo"].ToString()));
                        tbxSubElenco.Text = subelenco.Nome;
                        //LinkButton_Excluir.Visible = true;
                    }

                    gridMedicamentos.DataSource = subelenco != null ? subelenco.Medicamentos : new List<Medicamento>();
                    gridMedicamentos.DataBind();
                    //IList<ElencoMedicamento> elencos = subelenco != null ? Factory.GetInstance<ISubElencoMedicamento>().BuscarElencos<ElencoMedicamento>(subelenco.Codigo) : new List<ElencoMedicamento>();
                   // gridElenco.DataSource = elencos;
                   // gridElenco.DataBind();

                    IList<ViverMais.Model.Medicamento> medicamentos;
                    IList<ViverMais.Model.ElencoMedicamento> elencosNaoContidos;
                    if (subelenco != null)
                    {
                        //carrega no dropdownlist apenas os medicamentos e elencos que não constam no subelenco
                        medicamentos = Factory.GetInstance<ISubElencoMedicamento>().BuscarMedicamentosNaoContidosNoSubElenco<ViverMais.Model.Medicamento>(subelenco.Codigo).OrderBy(m => m.Nome).ToList();
                        elencosNaoContidos = Factory.GetInstance<ISubElencoMedicamento>().BuscarElencosNaoContidosNoSubElenco<ViverMais.Model.ElencoMedicamento>(subelenco.Codigo).OrderBy(el => el.Nome).ToList();
                        gridMedicamentos.DataSource = subelenco.Medicamentos;
                        gridMedicamentos.DataBind();
                        Session["medicamentos"] = subelenco.Medicamentos;

                       // gridElenco.DataSource = elencos;
                        //gridElenco.DataBind();
                       // Session["elencos"] = elencos;

                        Session["subelenco"] = subelenco;
                    }
                    else
                    {
                        //ou carrega todos caso seja um novo subelenco
                        medicamentos = Factory.GetInstance<IMedicamento>().ListarTodosMedicamentos<ViverMais.Model.Medicamento>().OrderBy(m => m.Nome).ToList();
                        elencosNaoContidos = Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<ViverMais.Model.ElencoMedicamento>().OrderBy(el => el.Nome).ToList();
                    }

                    ddlMedicamento.Items.Add(new ListItem("Selecione...", "0"));
                    foreach (ViverMais.Model.Medicamento m in medicamentos)
                        ddlMedicamento.Items.Add(new ListItem(m.Nome, m.Codigo.ToString()));
                    ddlMedicamento.DataBind();

                    ddlMedicamento.Attributes.Add("onmouseover", "javascript:showTooltip(this);");

                    //ddlElenco.Items.Add(new ListItem("Selecione...", "0"));
                    //foreach (ViverMais.Model.ElencoMedicamento em in elencosNaoContidos)
                    //    ddlElenco.Items.Add(new ListItem(em.Nome, em.Codigo.ToString()));
                    //ddlElenco.DataBind();
                }
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            IList<ViverMais.Model.Medicamento> medicamentos = Session["medicamentos"] != null ? (IList<ViverMais.Model.Medicamento>)Session["medicamentos"] : new List<ViverMais.Model.Medicamento>();
            ViverMais.Model.SubElencoMedicamento subelenco;

            //if (medicamentos.Count() <= 0)
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('É necessário incluir pelo menos um medicamento neste sub-elenco.');", true);
            //else
            //{
                if (Session["subelenco"] != null)
                    subelenco = (ViverMais.Model.SubElencoMedicamento)Session["subelenco"];
                else
                    subelenco = new ViverMais.Model.SubElencoMedicamento();

                subelenco.Nome = tbxSubElenco.Text;
                subelenco.Medicamentos = new List<ViverMais.Model.Medicamento>();
                if (medicamentos != null)
                    foreach (ViverMais.Model.Medicamento m in medicamentos)
                    {
                        subelenco.Medicamentos.Add(m);
                    }

                //subelenco.Elencos = new List<ViverMais.Model.ElencoMedicamento>();
                //IList<ViverMais.Model.ElencoMedicamento> elencos = Session["elencos"] != null ? (IList<ViverMais.Model.ElencoMedicamento>)Session["elencos"] : new List<ElencoMedicamento>();
                //if (elencos != null)
                //    foreach (ViverMais.Model.ElencoMedicamento el in elencos)
                //    {
                //        subelenco.Elencos.Add(el);
                //    }

                if (subelenco.Codigo == 0)
                { //salva um novo
                   // subelenco.Elencos = null;
                    subelenco.Medicamentos = null;
                    Factory.GetInstance<IFarmaciaServiceFacade>().Salvar(subelenco);
                   // subelenco.Elencos = subelenco.Elencos != null ? subelenco.Elencos.Concat(elencos).ToList() : elencos;
                    subelenco.Medicamentos = subelenco.Medicamentos != null ? subelenco.Medicamentos.Concat(medicamentos).ToList() : medicamentos;
                    Factory.GetInstance<IFarmaciaServiceFacade>().Atualizar(subelenco);
                }
                else //atualiza
                    Factory.GetInstance<IFarmaciaServiceFacade>().Atualizar(subelenco);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Sub-Elenco de Medicamento salvo com sucesso!');location='FormExibeSubElencoMedicamento.aspx';", true);
            //}
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

        //void RecarregaElencos()
        //{
        //    IList<ViverMais.Model.ElencoMedicamento> elencos = Session["elencos"] != null ? (IList<ViverMais.Model.ElencoMedicamento>)Session["elencos"] : new List<ViverMais.Model.ElencoMedicamento>();
        //    IList<ViverMais.Model.ElencoMedicamento> el = Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<ViverMais.Model.ElencoMedicamento>();
        //    var result = from l in el where !elencos.Contains(l) select l;
        //    ddlElenco.Items.Clear();
        //    ddlElenco.Items.Add(new ListItem("Selecione...", "0"));
        //    foreach (ViverMais.Model.ElencoMedicamento elenco in result.ToList<ViverMais.Model.ElencoMedicamento>().OrderBy(p => p.Nome).ToList())
        //    {
        //        ddlElenco.Items.Add(new ListItem(elenco.Nome, elenco.Codigo.ToString()));
        //    }
        //    ddlElenco.DataBind();


        //}

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            //pega o elenco atual da sessao

            IList<ViverMais.Model.Medicamento> medicamentos = Session["medicamentos"] != null ? (IList<ViverMais.Model.Medicamento>)Session["medicamentos"] : new List<ViverMais.Model.Medicamento>();
            //pega o medicamento selecionado no dropdownlist
            ViverMais.Model.Medicamento medicamento = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.Medicamento>(int.Parse(ddlMedicamento.SelectedValue));
            medicamentos.Add(medicamento);
            //remove o item para evitar insercao duplicada
            ddlMedicamento.Items.Remove(ddlMedicamento.Items.FindByValue(medicamento.Codigo.ToString()));
            gridMedicamentos.DataSource = medicamentos;
            gridMedicamentos.DataBind();
            //IList<ViverMais.Model.Medicamento> temp = new List<ViverMais.Model.Medicamento>();
            //temp.Add(medicamento);
            //temp.OrderBy(p => p.Nome);
            //foreach (ViverMais.Model.Medicamento med in temp)
            //    ddlMedicamento.Items.Add(new ListItem(med.Nome, med.Codigo.ToString()));
            //ddlMedicamento.DataBind();
            Session["medicamentos"] = medicamentos;
            RecarregaMedicamentos();
        }

        //protected void btnAdicionar2_Click(object sender, EventArgs e)
        //{
        //    IList<ViverMais.Model.ElencoMedicamento> elencos = Session["elencos"] != null ? (IList<ViverMais.Model.ElencoMedicamento>)Session["elencos"] : new List<ViverMais.Model.ElencoMedicamento>();
        //    //pega o elenco selecionado no dropdownlist
        //    ViverMais.Model.ElencoMedicamento elenco = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.ElencoMedicamento>(int.Parse(ddlElenco.SelectedValue));
        //    elencos.Add(elenco);
        //    //remove o item para evitar insercao duplicada
        //    ddlElenco.Items.Remove(ddlElenco.Items.FindByValue(elenco.Codigo.ToString()));
        //    gridElenco.DataSource = elencos;
        //    gridElenco.DataBind();
        //    Session["elencos"] = elencos;
        //    RecarregaElencos();
        //}

        protected void gridMedicamentos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            IList<ViverMais.Model.Medicamento> medicamentos = (IList<ViverMais.Model.Medicamento>)Session["medicamentos"];
            int co_medicamento = int.Parse(gridMedicamentos.DataKeys[e.RowIndex]["Codigo"].ToString());
            //ViverMais.Model.Medicamento m = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.Medicamento>(co_medicamento);
            int co_medicamentoremove = medicamentos.Select((Med, Index) => new { Index, Med }).Where(p => p.Med.Codigo == co_medicamento).First().Index;
            medicamentos.RemoveAt(co_medicamentoremove);
            gridMedicamentos.DataSource = medicamentos;
            gridMedicamentos.DataBind();
            Session["medicamentos"] = medicamentos;
            RecarregaMedicamentos();

        }

        //protected void gridElenco_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    //pega o elenco atual da sessao
        //    IList<ViverMais.Model.ElencoMedicamento> elencos = (IList<ViverMais.Model.ElencoMedicamento>)Session["elencos"];
        //    int co_elenco = int.Parse(gridElenco.Rows[e.RowIndex].Cells[0].Text);
        //    ViverMais.Model.ElencoMedicamento el = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.ElencoMedicamento>(co_elenco);
        //    elencos.Remove(el);
        //    gridElenco.DataSource = elencos;
        //    gridElenco.DataBind();
        //    Session["elencos"] = elencos;
        //    RecarregaElencos();
        //}

        ///// <summary>
        ///// Exclui o sub-elenco do módulo farmácia
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void OnClick_ExcluirSubElenco(object sender, EventArgs e)
        //{
        //    int co_subelenco;
        //    if (int.TryParse(Request["codigo"].ToString(), out co_subelenco))
        //    {
        //        string retorno = Factory.GetInstance<IMedicamento>().ExcluirSubElencoMedicamento(co_subelenco);

        //        if (retorno.Equals("ok"))
        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Sub-Elenco excluído com sucesso.');location='FormExibeSubElencoMedicamento.aspx';", true);
        //        else
        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + retorno + "');", true);
        //    }
        //}
    }
}
