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
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;

namespace ViverMais.View.Farmacia
{
    public partial class FormBuscaRMAtendimento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IList<ViverMais.Model.Farmacia> farmacias = Factory.GetInstance<IRequisicaoMedicamento>().ListarTodos<ViverMais.Model.Farmacia>();
                ddlFarmacia.Items.Add(new ListItem("Selecione...","0"));
                foreach (ViverMais.Model.Farmacia farmacia in farmacias)
                    ddlFarmacia.Items.Add(new ListItem(farmacia.Nome, farmacia.Codigo.ToString()));
            }
        }

        protected void brnPesquisar_Click(object sender, EventArgs e)
        {
            IList<ViverMais.Model.RequisicaoMedicamento> rms = Factory.GetInstance<IRequisicaoMedicamento>().ListarAutorizadas<ViverMais.Model.RequisicaoMedicamento>(int.Parse(ddlFarmacia.SelectedValue));
            gridRM.DataSource = rms;
            gridRM.DataBind();
            Panel_ResultadoBusca.Visible = true;
        }

        protected void btnListarTodos_Click(object sender, EventArgs e)
        {
            IList<ViverMais.Model.RequisicaoMedicamento> rms = Factory.GetInstance<IRequisicaoMedicamento>().ListarAutorizadas<ViverMais.Model.RequisicaoMedicamento>();
            gridRM.DataSource = rms;
            gridRM.DataBind();
            Panel_ResultadoBusca.Visible = true;
        }
    }
}
