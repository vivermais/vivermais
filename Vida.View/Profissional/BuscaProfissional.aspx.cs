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
using System.Collections.Generic;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.View.Profissional
{
    public partial class BuscaProfissional : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblSemPesquisa.Visible = false;
                lblSemRegistro.Visible = false;
                IList<ViverMais.Model.CategoriaOcupacao> categorias = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<ViverMais.Model.CategoriaOcupacao>();

                // Lista todas as ocupações
                foreach (ViverMais.Model.CategoriaOcupacao categoria in categorias)
                {
                    ddlCategoria.Items.Add(new ListItem(categoria.Nome, categoria.Codigo.ToString()));
                }
                ddlCategoria.SelectedValue = "1"; //Define Médico como Default
            }
        }


        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (tbxNumConselho.Text.Equals(String.Empty) && (tbxNomeProfissionalPesquisa.Text.Equals(String.Empty)))
            {
                lblSemPesquisa.Visible = true;
                return;
            }
            else
            {
                lblSemPesquisa.Visible = false;
                IList<ProfissionalSolicitante> profSolicitante = Factory.GetInstance<IProfissionalSolicitante>().BuscaProfissionalPorNumeroConselhoECategoria<ProfissionalSolicitante>(ddlCategoria.SelectedValue, tbxNumConselho.Text, tbxNomeProfissionalPesquisa.Text.ToUpper(),0);
                if (profSolicitante.Count != 0)
                {
                    lblSemRegistro.Visible = false;
                    Session["Profissionais"] = profSolicitante;
                    GridViewListaProfissionais.DataSource = profSolicitante;
                    GridViewListaProfissionais.DataBind();
                }
                else
                {
                    lblSemRegistro.Visible = true;
                    GridViewListaProfissionais.DataSource = new List<ProfissionalSolicitante>();
                    GridViewListaProfissionais.DataBind();
                    return;
                }
            }
        }
    }
}
