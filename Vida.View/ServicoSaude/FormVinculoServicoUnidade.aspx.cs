using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Misc;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;

namespace ViverMais.View.ServicoSaude
{
    public partial class FormVinculoServicoUnidade : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                IList<ViverMais.Model.ServicoSaude> servicos = Factory.GetInstance<IServicoSaude>().ListarTodos<ViverMais.Model.ServicoSaude>();
                ddlServico.Items.Add(new ListItem("Selecione...", "-1"));
                foreach (var item in servicos)
                {
                    ddlServico.Items.Add(new ListItem(item.Nome, item.Codigo.ToString()));
                }

                IList<ViverMais.Model.EstabelecimentoSaude> unidades = Factory.GetInstance<IEstabelecimentoSaude>().ListarTodos<ViverMais.Model.EstabelecimentoSaude>().Where(p=>p.Bairro != null && p.NomeFantasia.ToUpper() != "NAO SE APLICA" && p.NomeFantasia.ToUpper() != "NÃO SE APLICA").OrderBy(p=>p.NomeFantasia).ToList();
                ddlUnidade.Items.Add(new ListItem("Selecione...", "-1"));
                foreach (var item in unidades)
                {
                    ddlUnidade.Items.Add(new ListItem(item.NomeFantasia, item.CNES));
                }
            }
        }

        void AtualizarGridView() 
        {
            ViverMais.Model.ServicoSaude servico = Factory.GetInstance<IServicoSaude>().BuscarPorCodigo<ViverMais.Model.ServicoSaude>(int.Parse(ddlServico.SelectedValue));
            GridViewUnidades.DataSource = servico.Unidades;
            GridViewUnidades.DataBind();
        }

        protected void lknVincular_Click(object sender, EventArgs e)
        {
            ViverMais.Model.ServicoSaude servico = Factory.GetInstance<IServicoSaude>().BuscarPorCodigo<ViverMais.Model.ServicoSaude>(int.Parse(ddlServico.SelectedValue));
            ViverMais.Model.EstabelecimentoSaude unidade = Factory.GetInstance<IEstabelecimentoSaude>().BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(ddlUnidade.SelectedValue);
            servico.Unidades.Add(unidade);
            Factory.GetInstance<IServicoSaude>().Salvar(servico);
            AtualizarGridView();
        }

        protected void ddlServico_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarGridView();
        }

        protected void GridViewUnidades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string cnes = ((GridViewRow)((DataControlFieldCell)((LinkButton)e.CommandSource).Parent).Parent).Cells[0].Text;
            ViverMais.Model.EstabelecimentoSaude unidade = Factory.GetInstance<IEstabelecimentoSaude>().BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(cnes);
            ViverMais.Model.ServicoSaude servico = Factory.GetInstance<IServicoSaude>().BuscarPorCodigo<ViverMais.Model.ServicoSaude>(int.Parse(ddlServico.SelectedValue));
            servico.Unidades.Remove(unidade);
            Factory.GetInstance<IServicoSaude>().Salvar(servico);
            AtualizarGridView();
        }
    }
}
