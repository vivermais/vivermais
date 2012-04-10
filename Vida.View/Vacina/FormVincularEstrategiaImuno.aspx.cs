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
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Vacina
{
    public partial class FormVincularEstrategiaImuno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "VINCULAR_ESTRATEGIA_IMUNO", Modulo.VACINA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);                else                {
                    ddlEstrategias.Items.Add(new ListItem("Selecione", "0"));
                    IList<Estrategia> estrategias = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<Estrategia>();
                    foreach (Estrategia estrategia in estrategias)
                        ddlEstrategias.Items.Add(new ListItem(estrategia.Descricao, estrategia.Codigo.ToString()));
                    ddlEstrategias.DataBind();
                    ddlEstrategias.SelectedValue = "0";

                    //IList<ViverMais.Model.Vacina> vacinas = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<ViverMais.Model.Vacina>("Nome", true);
                    //ddlImunobiologicos.Items.Add(new ListItem("Selecione", "0"));
                    //foreach (ViverMais.Model.Vacina vacina in vacinas)
                    //    ddlImunobiologicos.Items.Add(new ListItem(vacina.Nome, vacina.Codigo.ToString()));
                    //ddlImunobiologicos.DataBind();
                    //ddlImunobiologicos.SelectedValue = "0";
                }
            }
        }

        void CarregaVacinas()
        {
            int id_estrategia = int.Parse(ddlEstrategias.SelectedValue);
            IList<ViverMais.Model.Vacina> vacinas = Factory.GetInstance<IEstrategiaImunobiologico>().BuscarPorCodigo<Estrategia>(id_estrategia).Vacinas;
            IList<ViverMais.Model.Vacina> listavacinas = Factory.GetInstance<IVacina>().ListarTodos<ViverMais.Model.Vacina>("Nome", true);
            if (vacinas.Count != 0)
            {
                Panel_Estrategias_Imuno.Visible = true;
                Gridview_Imunos.DataSource = vacinas;
                Gridview_Imunos.DataBind();
                Session["Vacinas"] = vacinas;
                
                foreach (ViverMais.Model.Vacina v in vacinas)//remove as vacinas já cadastradas
                    listavacinas.Remove(v);
            }
            else
            {
                Panel_Estrategias_Imuno.Visible = false;
            }
                ddlImunobiologicos.Items.Clear();
                ddlImunobiologicos.Items.Add(new ListItem("Selecione", "0"));
                foreach (ViverMais.Model.Vacina vacina in listavacinas)
                    ddlImunobiologicos.Items.Add(new ListItem(vacina.Nome, vacina.Codigo.ToString()));
                ddlImunobiologicos.DataBind();
                ddlImunobiologicos.SelectedValue = "0";
            
        }

        protected void ddlEstrategias_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Vacinas"] = null;
            if (ddlEstrategias.SelectedValue != "0")
            {
                CarregaVacinas();
            }
        }

        protected void btnAddImuno_Click(object sender, EventArgs e)
        {
            Estrategia estrategia = Factory.GetInstance<IEstrategiaImunobiologico>().BuscarPorCodigo<Estrategia>(int.Parse(ddlEstrategias.SelectedValue));
            ViverMais.Model.Vacina vacina = Factory.GetInstance<IVacina>().BuscarPorCodigo<ViverMais.Model.Vacina>(int.Parse(ddlImunobiologicos.SelectedValue));

            IList<ViverMais.Model.Vacina> vacinas;
            //Se não existir Vacina para aquela Estratégia, ele irá adicionar
            if (!estrategia.Vacinas.Contains(vacina))
            {
                if (Session["Vacinas"] != null)
                    vacinas = (IList<ViverMais.Model.Vacina>)Session["Vacinas"];
                else
                    vacinas = new List<ViverMais.Model.Vacina>();
                vacinas.Add(vacina);
                Session["Vacinas"] = vacinas;
                estrategia.Vacinas.Add(vacina);
                Factory.GetInstance<IEstrategiaImunobiologico>().Salvar(estrategia);
                Factory.GetInstance<IVacinaServiceFacade>().Inserir(new LogVacina(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 19, "id vacina: " + vacina.Codigo.ToString() + " id estrategia: " + estrategia.Codigo.ToString()));
                CarregaVacinas();

                //Gridview_Imunos.DataSource = estrategia.Vacinas;
                //Gridview_Imunos.DataBind();
                Panel_Estrategias_Imuno.Visible = true;
            }
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Esta Vacina já está vinculada à esta estratégia!');", true);
            //}


        }


        protected void Gridview_Imunos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                int id_vacina = Convert.ToInt32(e.CommandArgument);
                ViverMais.Model.Vacina vacina = Factory.GetInstance<IVacina>().BuscarPorCodigo<ViverMais.Model.Vacina>(id_vacina);
                Estrategia estrategia = Factory.GetInstance<IEstrategiaImunobiologico>().BuscarPorCodigo<Estrategia>(int.Parse(ddlEstrategias.SelectedValue));
                if (estrategia.Vacinas.Contains(vacina))
                {
                    estrategia.Vacinas.Remove(vacina);
                    Session["Vacinas"] = estrategia.Vacinas;
                    //Gridview_Imunos.DataSource = estrategia.Vacinas;
                    //Gridview_Imunos.DataBind();
                    Factory.GetInstance<IEstrategiaImunobiologico>().Salvar(estrategia);
                    Factory.GetInstance<IVacinaServiceFacade>().Inserir(new LogVacina(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 20, "id vacina: " + vacina.Codigo.ToString() + " id estrategia: " + estrategia.Codigo.ToString()));
                    CarregaVacinas();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Registro Excluído com Sucesso!');", true);
                }

            }
        }
    }
}
