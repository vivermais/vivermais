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
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.BPA;

namespace ViverMais.View.EnvioBPA
{
    public partial class FormCompetencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id_competencia"] != null)
                {
                    int codigo = int.Parse(Request.QueryString["id_competencia"]);
                    CompetenciaBPA competenciaBPA = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<CompetenciaBPA>(codigo);
                    tbxAno.Text = competenciaBPA.Ano.ToString();
                    ddlMes.SelectedValue = competenciaBPA.Mes.ToString();
                    tbxDataInicio.Text = competenciaBPA.DataInicial.ToString("dd/MM/yyyy");
                    tbxDataFim.Text = competenciaBPA.DataFinal.ToString("dd/MM/yyyy");
                }
            }
        }

        protected void imgBtnSalvar_Click(object sender, EventArgs e)
        {
            CompetenciaBPA competenciaBPA = null;
            if (Request.QueryString["id_competencia"] != null)
            {
                int codigo = int.Parse(Request.QueryString["id_competencia"]);
                competenciaBPA = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<CompetenciaBPA>(codigo);
            }
            else
            {
                competenciaBPA = Factory.GetInstance<IEnviarBPA>().BuscarCompetencia<CompetenciaBPA>(int.Parse(tbxAno.Text), int.Parse(ddlMes.SelectedValue));
                if (competenciaBPA != null)
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('A Competência informada já existe no sistema');</script>");
                    return;
                }
                competenciaBPA = new CompetenciaBPA();
            }
            competenciaBPA.Ano = int.Parse(tbxAno.Text);
            competenciaBPA.Mes = int.Parse(ddlMes.SelectedValue);
            competenciaBPA.DataInicial = DateTime.Parse(tbxDataInicio.Text);
            competenciaBPA.DataFinal = DateTime.Parse(tbxDataFim.Text);
            if (Request.QueryString["id_competencia"] != null)
                Factory.GetInstance<IViverMaisServiceFacade>().Salvar(competenciaBPA);
            else
                Factory.GetInstance<IViverMaisServiceFacade>().Inserir(competenciaBPA);
            ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Dados salvos com sucesso!');</script>");
        }
    }
}
