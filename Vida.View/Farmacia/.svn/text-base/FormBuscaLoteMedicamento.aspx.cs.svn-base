using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vida.Model;
using Vida.DAO;
using Vida.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using Vida.ServiceFacade.ServiceFacades;
using System.Text.RegularExpressions;

namespace Vida.View.Farmacia
{
    public partial class FormBuscaLoteMedicamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaLotes();
                //IList<Medicamento> lm = Factory.GetInstance<IMedicamento>().ListarTodos<Medicamento>().OrderBy(p => p.Nome).ToList();
                //foreach (Medicamento m in lm)
                //    DropDownList_Medicamento.Items.Add(new ListItem(m.Nome, m.Codigo.ToString()));

                //IList<FabricanteMedicamento> lf = Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<FabricanteMedicamento>().OrderBy(p => p.Nome).ToList();
                //foreach(FabricanteMedicamento f in lf)
                //    DropDownList_Fabricante.Items.Add(new ListItem(f.Nome,f.Codigo.ToString()));
            }
        }

        /// <summary>
        /// Pesquisa os lotes de medicamento com as informações registradas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Pesquisar(object sender, EventArgs e) 
        {
            //Button bt = (Button)sender;

            //if(bt.CommandArgument != "todos")
            //{
            //    if (!CustomValidator_Pesquisa.IsValid)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + ViewState["msgValidation"].ToString() + "');", true);
            //        return;
            //    }
            //}

            //ViewState["tipo_pesquisa"] = bt.CommandArgument;
            //ViewState["lote"] = TextBox_Lote.Text;
            //ViewState["co_medicamento"] = DropDownList_Medicamento.SelectedValue;
            //ViewState["co_fabricante"] = DropDownList_Fabricante.SelectedValue;
            //ViewState["validade"] = !string.IsNullOrEmpty(TextBox_Validade.Text) ? DateTime.Parse(TextBox_Validade.Text).ToString() : DateTime.MinValue.ToString();

            //CarregaLotes();
        }

        /// <summary>
        /// Carrega os lotes pesquisados
        /// </summary>
        private void CarregaLotes()
        {
            GridView_Lotes.DataSource = Factory.GetInstance<ILoteMedicamento>().ListarTodos<LoteMedicamento>().OrderBy(p => p.Medicamento.Nome).ToList();
            GridView_Lotes.DataBind();

            //IList<LoteMedicamento> llm = new List<LoteMedicamento>();

            //if (ViewState["tipo_pesquisa"].ToString() == "todos")
            //    llm = Factory.GetInstance<ILoteMedicamento>().ListarTodos<LoteMedicamento>().OrderBy(p => p.Medicamento.Nome).ToList();
            //else
            //    llm = Factory.GetInstance<ILoteMedicamento>().BuscarPorDescricao<LoteMedicamento>(ViewState["lote"].ToString(), DateTime.Parse(ViewState["validade"].ToString()), int.Parse(ViewState["co_medicamento"].ToString()), int.Parse(ViewState["co_fabricante"].ToString()));

            //GridView_Lotes.DataSource = llm;
            //GridView_Lotes.DataBind();
            //Panel_Resultado.Visible = true;
        }

        /// <summary>
        /// Paginação do gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e) 
        {
            CarregaLotes();
            GridView_Lotes.PageIndex = e.NewPageIndex;
            GridView_Lotes.DataBind();
        }

        /// <summary>
        /// Verifica se a pesquisa possui os parâmetros válidos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnServerValidate_ValidaPesquisa(object sender, ServerValidateEventArgs e) 
        {
            //e.IsValid = true;
            //Regex regex = new Regex(@"^[\S]{3}[\w\W]*$");

            //if (string.IsNullOrEmpty(TextBox_Lote.Text) && string.IsNullOrEmpty(TextBox_Validade.Text)
            //    && DropDownList_Fabricante.SelectedValue == "-1" && DropDownList_Medicamento.SelectedValue == "-1")
            //{
            //    ViewState["msgValidation"] = "Informe um ou mais dos seguintes campos para realizar a pesquisa: \\n 1 - Medicamento \\n 2 - Fabricante \\n 3 - Lote \\n 4 - Data de Validade";
            //    e.IsValid = false;
            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(TextBox_Lote.Text) && !regex.IsMatch(TextBox_Lote.Text))
            //    {
            //        ViewState["msgValidation"] = "Lote deve iniciar com pelo menos três caracteres.";
            //        e.IsValid = false;
            //    }
            //}
        }
    }
}
