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
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using System.Collections.Generic;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.View.Farmacia
{
    public partial class FormBuscaRM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //protected void btnListarTodos_Click(object sender, EventArgs e)
        //{
        //    ViverMais.Model.Farmacia farmacia = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia>(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo);
        //    IList<ViverMais.Model.RequisicaoMedicamento> rms = Factory.GetInstance<IRequisicaoMedicamento>().BuscarPorFarmacia<ViverMais.Model.RequisicaoMedicamento>(farmacia.Codigo,-1);
        //    gridRM.DataSource = rms;
        //    gridRM.DataBind();
        //}

        //protected void btnPesquisar_Click(object sender, EventArgs e)
        //{
        //    ViverMais.Model.Farmacia farmacia = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia>(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo);
        //    ViverMais.Model.RequisicaoMedicamento rm = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.RequisicaoMedicamento>(int.Parse(tbxRequisicao.Text));
        //    IList<ViverMais.Model.RequisicaoMedicamento> rms = new List<ViverMais.Model.RequisicaoMedicamento>();
        //    //se existe e se o usuário tem permissão para visualizar a farmácia da RM pesquisada, exibe a RM
        //    if (rm != null && rm.Farmacia.Codigo == farmacia.Codigo)
        //        rms.Add(rm);
        //    gridRM.DataSource = rms;
        //    gridRM.DataBind();
        //}
    }
}
