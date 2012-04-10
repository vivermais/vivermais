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
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;

namespace ViverMais.View.Farmacia
{
    public partial class FormVincularElencoFarmacia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaCampos();
                CarregaArvore();

            }
        }

        void CarregaCampos()
        {
            IList<ViverMais.Model.Farmacia> farmacias = Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<ViverMais.Model.Farmacia>();
            IList<ViverMais.Model.ElencoMedicamento> elencos = Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<ViverMais.Model.ElencoMedicamento>();

            ddlFarmacia.Items.Add(new ListItem("Selecione...","0"));
            foreach (ViverMais.Model.Farmacia f in farmacias) 
            {
                ddlFarmacia.Items.Add(new ListItem(f.Nome,f.Codigo.ToString()));
            }

            ddlElenco.Items.Add(new ListItem("Selecione...","0"));
            foreach (ViverMais.Model.ElencoMedicamento e in elencos) 
            {
                ddlElenco.Items.Add(new ListItem(e.Nome,e.Codigo.ToString()));
            }
        }
        /// <summary>
        /// Carrega a arvore com as farmácias e os elencos
        /// </summary>
        void CarregaArvore(){

            IList<ViverMais.Model.Farmacia> farmacias = Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<ViverMais.Model.Farmacia>();
            
            TreeViewFarmacia.Nodes.Clear();
            foreach (ViverMais.Model.Farmacia f in farmacias)
            {
            TreeNode root = new TreeNode(f.Nome, f.Codigo.ToString());
            root.NavigateUrl = "#";             
                foreach (ViverMais.Model.ElencoMedicamento em in f.Elencos)
                {
                    TreeNode NodeElenco = new TreeNode(em.Nome, em.Codigo.ToString() );
                    root.ChildNodes.Add(NodeElenco);
                }
                TreeViewFarmacia.Nodes.Add(root);
                
            }
            TreeViewFarmacia.CollapseAll();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            int co_farmacia = int.Parse(ddlFarmacia.SelectedValue);
            ViverMais.Model.Farmacia farmacia = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.Farmacia>(co_farmacia);

            int co_elenco = int.Parse(ddlElenco.SelectedValue);
            ViverMais.Model.ElencoMedicamento elenco = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.ElencoMedicamento>(co_elenco);

            if (farmacia.Elencos.Contains(elenco))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Falha! Esta farmácia já contém este elenco!');", true);
                return;
            }

            farmacia.Elencos.Add(elenco);
            Factory.GetInstance<IFarmaciaServiceFacade>().Salvar(ref farmacia);
            CarregaArvore();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Elenco vinculado com sucesso!');", true);
            btnCancelar_Click(new object(), new EventArgs());
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ddlFarmacia.SelectedValue = "0";
            ddlElenco.SelectedValue = "0";
        }

        //Seleciona um elenco e uma farmácia para exclusão
        protected void TreeViewFarmacia_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode node = TreeViewFarmacia.SelectedNode;
            //caso seja selecionada uma farmacia, nao faz nada
            if (node.Parent == null)
            {
                ddlFarmacia.SelectedValue = "0";
                ddlElenco.SelectedValue = "0";
                Session["elenco"] = null;
                Session["farmacia"] = null;
                return;
            }
            
            ViverMais.Model.ElencoMedicamento elenco = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.ElencoMedicamento>(int.Parse(node.Value));
            ViverMais.Model.Farmacia farmacia = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.Farmacia>(int.Parse(node.Parent.Value));

            ddlFarmacia.SelectedValue = farmacia.Codigo.ToString();
            ddlElenco.SelectedValue = elenco.Codigo.ToString();
            Session["elenco"] = elenco;
            Session["farmacia"] = farmacia;
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            int co_farmacia = int.Parse(ddlFarmacia.SelectedValue);
            ViverMais.Model.Farmacia farmacia = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.Farmacia>(co_farmacia);

            int co_elenco = int.Parse(ddlElenco.SelectedValue);
            ViverMais.Model.ElencoMedicamento elenco = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.ElencoMedicamento>(co_elenco);

            if (!farmacia.Elencos.Contains(elenco))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Falha! Esta farmácia não contém este elenco!');", true);
                return;
            }
            farmacia.Elencos.Remove(elenco);
            Factory.GetInstance<IFarmaciaServiceFacade>().Salvar(ref farmacia);
            CarregaArvore();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Elenco desvinculado com sucesso!');", true);

        }
    }
}
