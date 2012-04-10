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
using System.Reflection;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Relatorio;
using ViverMais.Model.Entities;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Relatorio
{
    public partial class RelatorioGenerico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "RELATORIOS"))
                //    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Você não tem permissão para acessar essa página. Em caso de dúViverMais, entre em contato.');window.location='../Home.aspx';</script>");

                Type[] types = Assembly.GetAssembly(typeof(ViverMais.Model.Paciente)).GetTypes().OrderBy(t => t.Name).ToArray<Type>();
                ddlEntidade.Items.Add(new ListItem("Selecione", "-1"));
                foreach (var item in types)
                {
                    Attribute att = Attribute.GetCustomAttribute(item, typeof(RelatorioAttribute));
                    if (att != null)
                    {
                        if (((RelatorioAttribute)att).IsCriteria)
                            ddlEntidade.Items.Add(new ListItem(item.Name, item.AssemblyQualifiedName));
                    }
                }
            }
        }

        protected void ddlEntidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEntidade.SelectedValue != "-1")
            {
                Type type = Type.GetType(ddlEntidade.SelectedValue);
                PropertyInfo[] properties = type.GetProperties().OrderBy(p => p.Name).ToArray<PropertyInfo>();
                ddlPropriedade.Items.Clear();
                ddlPropriedade.Items.Add(new ListItem("Selecione", "-1"));
                lblTipo.Text = "-";
                foreach (var item in properties)
                {
                    Attribute att = Attribute.GetCustomAttribute(item, typeof(RelatorioAttribute));
                    if (att != null)
                    {
                        if (((RelatorioAttribute)att).IsCriteria)
                            ddlPropriedade.Items.Add(new ListItem(item.Name));
                    }
                }
                tbxValor.Text = string.Empty;
                lbxExpressoes.Items.Clear();
            }
        }

        protected void ddlPropriedade_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTipo.Text = Type.GetType(ddlEntidade.SelectedValue).GetProperty(ddlPropriedade.SelectedValue).PropertyType.Name;
            tbxValor.Text = string.Empty;
        }

        protected void imgBtnEnviar_Click(object sender, ImageClickEventArgs e)
        {
            //if (ddlPropriedade.SelectedValue != "-1")
            if(lbxExpressoes.Items.Count > 0)
            {
                //DataTable table = Factory.GetInstance<IRelatorioGenerico>()
                //    .ObterRelatorio(ddlEntidade.SelectedValue, ddlPropriedade.SelectedValue, int.Parse(ddlExpressao.SelectedValue), tbxValor.Text);
                IList<RelatorioExpression> exp = (IList<RelatorioExpression>)ViewState["Expressoes"];
                DataTable table = Factory.GetInstance<IRelatorioGenerico>()
                    .ObterRelatorio(ddlEntidade.SelectedValue, exp.ToArray());
                lblTotalResultados.Text = table.Rows.Count.ToString();
                GridViewRelatorio.DataSource = table;
                GridViewRelatorio.DataBind(); 
            }
        }

        protected void lnkAddExpressao_Click(object sender, EventArgs e)
        {
            if(ViewState["Expressoes"] == null)
                ViewState["Expressoes"] = new List<RelatorioExpression>();
            IList<RelatorioExpression> exp = (IList<RelatorioExpression>)ViewState["Expressoes"];
            exp.Add(new RelatorioExpression(ddlPropriedade.SelectedValue, int.Parse(ddlExpressao.SelectedValue), tbxValor.Text));
            lbxExpressoes.DataSource = exp;
            lbxExpressoes.DataBind();
            ViewState["Expressoes"] = exp;
        }

        protected void lnkDelExpressao_Click(object sender, EventArgs e)
        {
            if (lbxExpressoes.SelectedValue != null) 
            {
                IList<RelatorioExpression> exp = (IList<RelatorioExpression>)ViewState["Expressoes"];
                exp.RemoveAt(lbxExpressoes.SelectedIndex);
                lbxExpressoes.DataSource = exp;
                lbxExpressoes.DataBind();
                ViewState["Expressoes"] = exp;
            }
        }
    }
}
