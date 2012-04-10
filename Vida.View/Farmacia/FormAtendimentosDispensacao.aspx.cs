﻿using System;
using System.Collections.Generic;
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
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Dispensacao;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.View.Farmacia
{
    public partial class FormAtendimentosDispensacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int temp;

                if (Request["receita"] != null && int.TryParse(Request["receita"].ToString(), out temp))
                    CarregaDadosReceita(int.Parse(Request["receita"].ToString()));
            }
        }

        private void CarregaDadosReceita(int receita)
        {
            //DispensacaoBsn dispensacaoBsn = new DispensacaoBsn();
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            IPaciente ipaciente = Factory.GetInstance<IPaciente>();
            IFarmacia ifarmacia = Factory.GetInstance<IFarmacia>();
            IDispensacao idispensacao = Factory.GetInstance<IDispensacao>();
            IProfissional iprofissional = Factory.GetInstance<IProfissional>();
            ViverMais.Model.ReceitaDispensacao dispensacao = ifarmacia.BuscarPorCodigo<ViverMais.Model.ReceitaDispensacao>(receita);//dispensacaoBsn.BuscarPorCodigo<Dispensacao>(receita);
            ViverMais.Model.Paciente paciente = ipaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(dispensacao.CodigoPaciente);
            ViverMais.Model.Profissional profissional = iprofissional.BuscarPorCodigo<ViverMais.Model.Profissional>(dispensacao.CodigoProfissional);
            ViverMais.Model.Municipio municipio = iViverMais.BuscarPorCodigo<ViverMais.Model.Municipio>(dispensacao.CodigoMunicipio);
            IList<CartaoSUS> cartoes = ipaciente.ListarCartoesSUS<CartaoSUS>(dispensacao.CodigoPaciente);
            lbReceita.Text = dispensacao.Codigo.ToString();
            lbDataReceita.Text = dispensacao.DataReceita.ToString("dd/MM/yyyy");
            lbCartaoSUS.Text = cartoes.Last().Numero;//dispensacao.Paciente.CartaoSus.ToString();
            lbPaciente.Text = paciente.Nome;//dispensacao.Paciente.Nome.ToString();
            lbProfissional.Text = profissional.Nome;//dispensacao.Profissional.Nome.ToString();

            if (dispensacao.CodigoUnidade != "")
            {
                ViverMais.Model.EstabelecimentoSaude unidade = iViverMais.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(dispensacao.CodigoUnidade);
                lbDistritoOrigem.Text = unidade.Bairro.Distrito.Nome;//dispensacao.Distrito.Nome.ToString();
            }

            lbMunicipioOrigem.Text = municipio.Nome;//dispensacao.Municipio.Nome.ToString();

            //ItensDispensacaoBsn itensDispensacaoBsn = new ItensDispensacaoBsn();
            string[] campos = { "DataAtendimento", "Farmacia.Nome", "Farmacia.Codigo" };
            DataTable itens = idispensacao.BuscarPorDispensacaoAgrupado(receita, campos);//itensDispensacaoBsn.BuscarPorDispensacaoAgrupado(receita, campos);
            GridView1.DataSource = itens;
            GridView1.DataBind();
        }

        protected void verificar_acao(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "c_visualizar")
                Response.Redirect("FormItensAtendimentoDispensacao.aspx?data=" + GridView1.Rows[int.Parse(e.CommandArgument.ToString())].Cells[1].Text.ToString() + "&receita=" + Request["receita"].ToString() + "&farmacia=" + GridView1.DataKeys[int.Parse(e.CommandArgument.ToString())]["CodigoFarmacia"].ToString());
        }
    }
}
