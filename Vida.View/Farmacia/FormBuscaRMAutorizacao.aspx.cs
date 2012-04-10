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
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;

namespace ViverMais.View.Farmacia
{
    public partial class FormBuscaRMAutorizacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaCampos();
            }
        }

        protected void CarregaCampos() 
        {
            int co_distrito = Factory.GetInstance<IUsuarioDistritoFarmacia>().BuscarDistritoPorUsuario<int>(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo);
            IList<ViverMais.Model.EstabelecimentoSaude> estabelecimentos = Factory.GetInstance<IEstabelecimentoSaude>().BuscarUnidadeDistrito<ViverMais.Model.EstabelecimentoSaude>(co_distrito);     

            //constroi a lista de todas as farmacias dos estabelecimentos do distrito do usuário logado
            IList<ViverMais.Model.Farmacia> farmacias = new List<ViverMais.Model.Farmacia>();
            foreach ( ViverMais.Model.EstabelecimentoSaude es in estabelecimentos)
            {
                IList<ViverMais.Model.Farmacia> lf = Factory.GetInstance<IFarmacia>().BuscarPorEstabelecimentoSaude<ViverMais.Model.Farmacia>(es.CNES);
                foreach (ViverMais.Model.Farmacia f in lf) 
                    farmacias.Add(f);
            }
            ddlFarmacia.Items.Add(new ListItem("Selecione...", "0"));
            foreach (ViverMais.Model.Farmacia f in farmacias)
                ddlFarmacia.Items.Add(new ListItem(f.Nome, f.Codigo.ToString()));
        }

        protected void btnListarTodos_Click(object sender, EventArgs e)
        {
            int co_distrito = Factory.GetInstance<IUsuarioDistritoFarmacia>().BuscarDistritoPorUsuario<int>(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo);
            
            IList<ViverMais.Model.RequisicaoMedicamento> rms = Factory.GetInstance<IRequisicaoMedicamento>().BuscarPorDistrito<ViverMais.Model.RequisicaoMedicamento>(co_distrito);
            
            //busca as unidades do distrito do usuário logado =D
            //IList<ViverMais.Model.EstabelecimentoSaude> estabelecimentos = Factory.GetInstance<IEstabelecimentoSaude>().BuscarUnidadeDistrito<ViverMais.Model.EstabelecimentoSaude>(co_distrito);

            //IList<ViverMais.Model.RequisicaoMedicamento> requisicoes = new List<ViverMais.Model.RequisicaoMedicamento>();
            //foreach (ViverMais.Model.EstabelecimentoSaude es in estabelecimentos) 
            //{
            //    //busca as farmacias de cada unidade do distrito XD
            //    IList<ViverMais.Model.Farmacia> farmacias = Factory.GetInstance<IFarmacia>().BuscarPorEstabelecimentoSaude<ViverMais.Model.Farmacia
            //    if (farmacias != null)
            //        foreach (ViverMais.Model.Farmacia f in farmacias)
            //        {
            //            IList<ViverMais.Model.RequisicaoMedicamento> rms = Factory.GetInstance<IRequisicaoMedicamento>().BuscarPorFarmacia<ViverMais.Model.RequisicaoMedicamento>(f.Codigo);

            //        }
            //        //IList<ViverMais.Model.RequisicaoMedicamento> rms = Factory.GetInstance<IRequisicaoMedicamento>().
            //}
            //ViverMais.Model.Farmacia farmacia = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia>(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo);
            //IList<ViverMais.Model.RequisicaoMedicamento> rms = Factory.GetInstance<IRequisicaoMedicamento>().BuscarPorFarmacia<ViverMais.Model.RequisicaoMedicamento>(farmacia.Codigo);
            gridRM.DataSource = rms;
            gridRM.DataBind();
            Panel_ResultadoBusca.Visible = true;
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            //ViverMais.Model.Farmacia farmacia = Factory.GetInstance<IFarmacia>().BuscarPorDistrito<ViverMais.Model.Farmacia .BuscarPorUsuario<ViverMais.Model.Farmacia>(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo);
            //ViverMais.Model.RequisicaoMedicamento rm = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.RequisicaoMedicamento>(int.Parse(tbxRequisicao.Text));
            //IList<ViverMais.Model.RequisicaoMedicamento> rms = new List<ViverMais.Model.RequisicaoMedicamento>();
            ////se existe e se o usuário tem permissão para visualizar a farmácia da RM pesquisada, exibe a RM
            //if (rm != null && rm.Farmacia.Codigo == farmacia.Codigo)
            //    rms.Add(rm);
            //gridRM.DataSource = rms;
            //gridRM.DataBind();


            IList<ViverMais.Model.RequisicaoMedicamento> rms = Factory.GetInstance<IRequisicaoMedicamento>().BuscarPorFarmacia<ViverMais.Model.RequisicaoMedicamento>(int.Parse(ddlFarmacia.SelectedValue),-1);
            //ViverMais.Model.RequisicaoMedicamento rm = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.RequisicaoMedicamento>(int.Parse(tbxRequisicao.Text));
            //IList<ViverMais.Model.RequisicaoMedicamento> rms = new List<ViverMais.Model.RequisicaoMedicamento>();
            ////se existe e se o usuário tem permissão para visualizar a farmácia da RM pesquisada, exibe a RM
            //if (rms != null && rms.Count > 0)
            //{
            gridRM.DataSource = rms;
            gridRM.DataBind();
            Panel_ResultadoBusca.Visible = true;
        }
    }
}
