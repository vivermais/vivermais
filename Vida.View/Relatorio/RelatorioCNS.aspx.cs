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
using Vida.BLL;
using Vida.Model;

namespace Vida.View.Relatorio
{
    public partial class RelatorioCNS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlUnidades.DataSource = EstabelecimentoSaudeBLL.ListarTodos();
                ddlUnidades.DataBind();
                ddlUnidades.Items.Insert(0, new ListItem("Selecione...", "-1"));
                ddlUnidades.SelectedIndex = 0;
            }
        }

        protected void ddlUnidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUnidades.SelectedValue != "-1")
            {
                Vida.Model.EstabelecimentoSaude unidade = new Vida.Model.EstabelecimentoSaude(); 
                unidade.CNES = ddlUnidades.SelectedValue;
                ddlUsuario.DataSource = UsuarioBLL.ListarPorUnidade(unidade);
                ddlUsuario.DataBind();
                ddlUsuario.Items.Insert(0, new ListItem("Selecione...", "-1"));
                ddlUsuario.SelectedIndex = 0;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {            
            Usuario usuario = new Usuario();
            usuario.Codigo = Convert.ToInt32(ddlUsuario.SelectedValue);
            
            DateTime dataInicio = new DateTime();
            DateTime dataFim = new DateTime();
            
            string[] data1 = txtDataInicio.Text.Split('/');
            string[] data2 = txtDataFim.Text.Split('/');

            dataInicio = new DateTime(Convert.ToInt32(data1[2]),Convert.ToInt32(data1[1]),Convert.ToInt32(data1[0]));
            dataFim = new DateTime(Convert.ToInt32(data2[2]),Convert.ToInt32(data2[1]),Convert.ToInt32(data2[0]));

            int quantidade = LogVidaBLL.QuantidadeCartoesPorUsuario(usuario, dataInicio, dataFim);

            lblQuantidadeCartoes.Text = quantidade.ToString();
        }
    }
}
