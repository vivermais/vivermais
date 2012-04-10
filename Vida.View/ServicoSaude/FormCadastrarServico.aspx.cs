﻿using System;
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
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.Model;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Misc;

namespace ViverMais.View.ServicoSaude
{
    public partial class FormCadastrarServico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridViewServico.PageIndexChanging += new GridViewPageEventHandler(GridViewServico_PageIndexChanging);
                IList<ViverMais.Model.ServicoSaude> servicos = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<ViverMais.Model.ServicoSaude>();
                if (servicos.Count != 0)
                {
                    Session["Servicos"] = servicos;
                    GridViewServico.DataSource = servicos;
                    GridViewServico.DataBind();
                }
                if (Request.QueryString["id_servico"] != null)
                {
                    ViverMais.Model.ServicoSaude servico = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.ServicoSaude>(int.Parse(Request.QueryString["id_servico"]));
                    tbxNomeServico.Text = servico.Nome;
                }
            }
        }

        protected void lknSalvar_Click(object sender, EventArgs e)
        {
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            ViverMais.Model.ServicoSaude servicosaude = new ViverMais.Model.ServicoSaude();
            //Caso seja uma edição
            if (Request.QueryString["id_servico"] != null)
            {
                servicosaude = iViverMais.BuscarPorCodigo<ViverMais.Model.ServicoSaude>(int.Parse(Request.QueryString["id_servico"]));
                servicosaude.Nome = tbxNomeServico.Text;
                iViverMais.Atualizar(servicosaude);

            }
            else
            {
                servicosaude.Nome = tbxNomeServico.Text;
                iViverMais.Inserir(servicosaude);
            }
            ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Dados Salvos com Sucesso!'); window.location='FormCadastrarServico.aspx'</script>");

        }

        protected void GridViewServico_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable table = (DataTable)Session["Servicos"];
            GridViewServico.DataSource = table;
            GridViewServico.PageIndex = e.NewPageIndex;
            GridViewServico.DataBind();

        }

        protected void GridViewServico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string servico = ((GridViewRow)((DataControlFieldCell)((LinkButton)e.CommandSource).Parent).Parent).Cells[0].Text;
            ViverMais.Model.ServicoSaude serv = Factory.GetInstance<IServicoSaude>().BuscarPorCodigo<ViverMais.Model.ServicoSaude>(int.Parse(servico));           
            Factory.GetInstance<IServicoSaude>().Deletar(serv);
            AtualizarGridView();

        }
        void AtualizarGridView()
        {
            IList<ViverMais.Model.ServicoSaude> servicos = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<ViverMais.Model.ServicoSaude>();
            GridViewServico.DataSource = servicos;
            GridViewServico.DataBind();
        }

    }
}
