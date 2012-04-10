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
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Laboratorio;

namespace ViverMais.View.Laboratorio
{
    public partial class FormRecepcaoPaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                IList<UnidadeLaboratorio> unidades = Factory.GetInstance<ILaboratorioServiceFacade>().ListarTodos<UnidadeLaboratorio>().OrderBy(x => x.Nome).ToList();

                ddlUnidadeColeta.DataSource = unidades;
                ddlUnidadeColeta.DataBind();
                ddlUnidadeColeta.Items.Insert(0, new ListItem("Selecione uma unidade", "0"));

                ddlLocalEntrega.DataSource = unidades;
                ddlLocalEntrega.DataBind();
                ddlLocalEntrega.Items.Insert(0, new ListItem("Selecione uma unidade", "0"));
            }
        }


        protected void txtRG_TextChanged(object sender, EventArgs e)
        {
            PacienteLaboratorio paciente = Factory.GetInstance<IPacienteLaboratorio>().BuscaPorRG<PacienteLaboratorio>(txtRG.Text);
            if (paciente != null)
            {
                txtCartaoSUS.Text = paciente.CartaoSus;
                txtNome.Text = paciente.Nome;
                txtIdade.Text = paciente.Idade.ToString();
            }
            else
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alerta", "alert('Paciente não encontrado.');", true);
        }

        protected void txtCartaoSUS_TextChanged(object sender, EventArgs e)
        {
            PacienteLaboratorio paciente = Factory.GetInstance<IPacienteLaboratorio>().BuscaPorCartaoSus<PacienteLaboratorio>(txtCartaoSUS.Text);
            if (paciente != null)
            {
                txtRG.Text = paciente.Rg;
                txtNome.Text = paciente.Nome;
                txtIdade.Text = paciente.Idade.ToString();
                
            }
            else
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alerta", "alert('Paciente não encontrado.');", true);

        }

        protected void txtNomeUsual_TextChanged(object sender, EventArgs e)
        {
            if (txtNomeUsual.Text.Trim() != String.Empty)
            {
                List<ExameLaboratorio> exames = Factory.GetInstance<IExameLaboratorio>().BuscarPorNomeUsual<ExameLaboratorio>(txtNomeUsual.Text);
                lbxExamesEncontrados.DataSource = exames;
                lbxExamesEncontrados.DataBind();
            }
            txtMnemonico.Text = String.Empty;
        }

        protected void txtMnemonico_TextChanged(object sender, EventArgs e)
        {
            if (txtMnemonico.Text.Trim() != String.Empty)
            {
                List<ExameLaboratorio> exames = Factory.GetInstance<IExameLaboratorio>().BuscarPorSg<ExameLaboratorio>(txtMnemonico.Text);
                lbxExamesEncontrados.DataSource = exames;
                lbxExamesEncontrados.DataBind();
            }
            txtMnemonico.Text = String.Empty;
        }

        protected void btnVai_Click(object sender, EventArgs e)
        {
            repassaExame(ref lbxExamesEncontrados, ref lbxExamesSelecionados);
        }

        protected void btnVolta_Click(object sender, EventArgs e)
        {
            repassaExame(ref lbxExamesSelecionados, ref lbxExamesEncontrados);
        }

        protected void repassaExame(ref ListBox origem, ref ListBox destino)
        {
            if (origem.SelectedIndex > -1)
            {
                ListItem item = origem.SelectedItem;
                destino.Items.Add(item);
                destino.SelectedIndex = -1;
                origem.Items.Remove(item);
                OrdenarLista(ref destino);
            }
        }

        private void OrdenarLista(ref ListBox listBox)
        {
            SortedList itens = new SortedList();

            foreach (ListItem item in listBox.Items)
            {
                itens.Add(item.Text, item);
            }

            listBox.Items.Clear();

            for (int cont = 0; cont < itens.Count; cont++)
            {
                listBox.Items.Add((ListItem)itens[itens.GetKey(cont)]);
            }
        }

        protected void imgButtonVolta_Click(object sender, EventArgs e)
        {
            repassaExame(ref lbxExamesSelecionados, ref lbxExamesEncontrados);
        }

        protected void imgButtonVai_Click(object sender, EventArgs e)
        {
            repassaExame(ref lbxExamesEncontrados, ref lbxExamesSelecionados);
        }

        protected void txtNumeroConselhoSolicitante_TextChanged(object sender, EventArgs e)
        {
            ddlSolicitante.Items.Clear();
            SolicitanteExameLaboratorio solicitante = Factory.GetInstance<ISolicitanteExameLaboratorio>().BuscaPorNumeroConselho<SolicitanteExameLaboratorio>(Convert.ToInt32(txtNumeroConselhoSolicitante.Text));
            if (solicitante != null)
                ddlSolicitante.Items.Add(new ListItem(solicitante.Nome, solicitante.Codigo.ToString(), true));
            else
            {
                txtNumeroConselhoSolicitante.Text = string.Empty;
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alerta", "alert('Não foi encontrado solcitante com o numero informado.');", true);
            }

        }

        protected void txtPesquisaNomeSolicitante_TextChanged(object sender, EventArgs e)
        {
            gdvSolicitantes.DataSource = Factory.GetInstance<ISolicitanteExameLaboratorio>().BuscaPorNome<SolicitanteExameLaboratorio>(txtPesquisaNomeSolicitante.Text).OrderBy(x => x.Nome).ToList();
            gdvSolicitantes.DataBind();
        }
    }
}
