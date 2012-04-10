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
using Vida.Model;
using Vida.Model;
using Vida.DAO;
using Vida.ServiceFacade.ServiceFacades.Procedimento;
using Vida.ServiceFacade.ServiceFacades;
using System.Collections.Generic;
using Vida.ServiceFacade.ServiceFacades.GuiaProcedimentos;

namespace Vida.View.GuiaProcedimentos
{
    public partial class BuscaProcedimentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PanelProcedimentos.Visible = false;
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            string codigoProcedimento = tbxCodigoProcedimento.Text;
            string nomeProcedimento = tbxNomeProcedimento.Text;

            IList<Vida.Model.Procedimento> procedimentos = new List<Vida.Model.Procedimento>();
            Vida.Model.Procedimento procedimento = new Procedimento();

            if (!nomeProcedimento.Equals(""))
                procedimentos = Factory.GetInstance<IProcedimento>().BuscarPorNome<Vida.Model.Procedimento>(nomeProcedimento);

            if (!codigoProcedimento.Equals(""))
            {
                procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Vida.Model.Procedimento>(codigoProcedimento);
                if (procedimento != null)
                    procedimentos.Add(procedimento);
            }
            if (procedimentos.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(String), "critica", "alert('Não existe Procedimento com os dados passados!');", true);
                return;
            }
            IList<InfoProcedimento> infoProcedimentos = new List<InfoProcedimento>();
            infoProcedimentos = Factory.GetInstance<IInfoProcedimento>().BuscarPorProcedimento<InfoProcedimento>(procedimentos[0].Codigo);
            if (infoProcedimentos.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(String), "critica", "alert('Não existe informação para o procedimento informado!');", true);
                return;
            }
            PanelProcedimentos.Visible = true;
            GridViewProcedimentos.DataSource = infoProcedimentos;
            GridViewProcedimentos.DataBind();

        }
    }
}
