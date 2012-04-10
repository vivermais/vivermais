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

namespace Vida.View.Farmacia
{
    public partial class IncPesquisaLoteMedicamento : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IList<Medicamento> lm = Factory.GetInstance<IMedicamento>().ListarTodos<Medicamento>().OrderBy(p => p.Nome).ToList();
            foreach (Medicamento m in lm)
                DropDownList_Medicamento.Items.Add(new ListItem(m.Nome, m.Codigo.ToString()));

            IList<FabricanteMedicamento> lf = Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<FabricanteMedicamento>().OrderBy(p => p.Nome).ToList();
            foreach (FabricanteMedicamento f in lf)
                DropDownList_Fabricante.Items.Add(new ListItem(f.Nome, f.Codigo.ToString()));
        }

        protected void OnClick_Pesquisar(object sender, EventArgs e) 
        {
        }
    }
}