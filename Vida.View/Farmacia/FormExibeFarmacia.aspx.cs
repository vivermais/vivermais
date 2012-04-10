using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;

namespace ViverMais.View.Farmacia
{
    public partial class FormExibeFarmacia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_FARMACIA", Modulo.FARMACIA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    IList<ViverMais.Model.EstabelecimentoSaude> les = Factory.GetInstance<IEstabelecimentoSaude>().BuscarEstabelecimentoPorNaturezaOrganizacao<ViverMais.Model.EstabelecimentoSaude>("1").OrderBy(p => p.NomeFantasia).ToList();
                    foreach (ViverMais.Model.EstabelecimentoSaude es in les)
                        DropDownList_Unidade.Items.Add(new ListItem(es.NomeFantasia, es.CNES));
                }
                //Usuario u = (Usuario)Session["Usuario"];
                //Label_Unidade.Text = u.Unidade.NomeFantasia;
            }
        }

        /// <summary>
        /// Carrega as farmácias vinculadas ao estabelecimento de saúde selecionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_CarregaFarmacias(object sender, EventArgs e)
        {
            GridView_Farmacia.DataSource = Factory.GetInstance<IFarmacia>().BuscarPorEstabelecimentoSaude<ViverMais.Model.Farmacia>(DropDownList_Unidade.SelectedValue);
            GridView_Farmacia.DataBind();

            Panel_Farmacia.Visible = true;
        }
    }
}
