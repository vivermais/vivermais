﻿using System;
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
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Agendamento
{
    public partial class FormCotaEAS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "PARAMETRIZACAO_COTA_ESTABELECIMENTO_SAUDE",Modulo.AGENDAMENTO))
                {
                    lbxProcedimento.Visible = false;
                    PanelPercentual.Visible = false;
                    IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
                    //LISTA TODOS OS DISTRITOS
                    IList<ViverMais.Model.Distrito> distritos = iViverMais.ListarTodos<ViverMais.Model.Distrito>();
                    ddlDistrito.Items.Add(new ListItem("Selecione...", "0"));
                    foreach (ViverMais.Model.Distrito distrito in distritos)
                    {
                        ddlDistrito.Items.Add(new ListItem(distrito.Nome, distrito.Codigo.ToString()));
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }


        }

        protected void tbxCodigo_TextChanged(object sender, EventArgs e)
        {
            
            IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();
            ViverMais.Model.Procedimento procedimentos = iProcedimento.BuscarPorCodigo<ViverMais.Model.Procedimento>(tbxCodigo.Text);
            if (procedimentos == null)
            {
                tbxProcedimento.Text = "PROCEDIMENTO NÃO EXISTE";
                return;
            }
            Session["procedimento"] = procedimentos;
            tbxCodigo.Text = procedimentos.Codigo;
            tbxProcedimento.Text = procedimentos.Nome;

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            //Limpa o ListBox
            lbxProcedimento.Items.Clear();
            IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();
            IList<ViverMais.Model.Procedimento> procedimentos = iProcedimento.BuscarPorNome<ViverMais.Model.Procedimento>(tbxProcedimento.Text);
            if (procedimentos.Count == 0)
            {
                tbxCodigo.Text = "";
                tbxProcedimento.Text = "PROCEDIMENTO NÃO EXISTE";
            }
            else
            {
                lbxProcedimento.Visible = true;

                foreach (ViverMais.Model.Procedimento procedimento in procedimentos)
                {
                    // preenche o ListBox
                    ListItem item = new ListItem();
                    item.Text = procedimento.Nome;
                    item.Value = procedimento.Codigo;
                    lbxProcedimento.Items.Add(item);
                }
            }

        }

        protected void lbxProcedimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbxCodigo.Text = lbxProcedimento.SelectedValue;
            tbxProcedimento.Text = lbxProcedimento.SelectedItem.Text;
            lbxProcedimento.Visible = false;

        }

        protected void ddlDistrito_SelectedIndexChanged(object sender, EventArgs e)
        {
            PanelPercentual.Visible = true;
            IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();
            IList<ViverMais.Model.EstabelecimentoSaude> estabelecimentos = iEstabelecimento.BuscarUnidadeDistrito<ViverMais.Model.EstabelecimentoSaude>(int.Parse(ddlDistrito.SelectedValue));

            DataTable table = new DataTable();
            DataColumn col0 = new DataColumn("Unidade");
            table.Columns.Add(col0);
            DataColumn col1 = new DataColumn("Percentual");
            table.Columns.Add(col1);
            DataColumn col2 = new DataColumn("Id_Unidade");
            table.Columns.Add(col2);

            foreach (ViverMais.Model.EstabelecimentoSaude estabelecimento in estabelecimentos)
            {
                DataRow row = table.NewRow();
                row[0] = estabelecimento.NomeFantasia;
                IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
                ViverMais.Model.CotasEAS cota = iAgendamento.BuscarPercentual<ViverMais.Model.CotasEAS>(estabelecimento.CNES, tbxCodigo.Text);
                if (cota != null)
                {
                    row[1] = cota.Percentual;
                    row[2] = estabelecimento.CNES;
                }
                else
                {
                    row[1] = "";
                    row[2] = estabelecimento.CNES; ;

                }
                table.Rows.Add(row);
            }

            GridViewUnidades.DataSource = table;
            GridViewUnidades.DataBind();
            Session["table"] = table;

        }

        protected void GridViewUnidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbxPercentual.Enabled = true;
            btnAtualizar.Enabled = true;
            int index = GridViewUnidades.SelectedIndex;
            GridViewRow row = GridViewUnidades.Rows[index];
            tbxPercentual.Text = Server.HtmlDecode(row.Cells[1].Text);

        }

        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (tbxProcedimento.Text == "PROCEDIMENTO NÃO EXISTE")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Informe um Procedimento Válido');", true);
                return;
            }
            int index = GridViewUnidades.SelectedIndex;
            
            DataTable table = (DataTable)Session["table"];
            DataRow row = table.Rows[index];
            IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
            ViverMais.Model.CotasEAS cota = new CotasEAS();
            IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();
            ViverMais.Model.EstabelecimentoSaude unidade = iEstabelecimento.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(row[2].ToString());
            cota.Id_EstabelecimentoSaude = unidade.CNES;
            IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();
            ViverMais.Model.Procedimento proced = iProcedimento.BuscarPorCodigo<ViverMais.Model.Procedimento>(tbxCodigo.Text);
            cota.Id_Procedimento = proced.Codigo;
            cota.Percentual = float.Parse(tbxPercentual.Text.Trim());

            ViverMais.Model.CotasEAS cotas = iAgendamento.BuscarPercentual<ViverMais.Model.CotasEAS>(unidade.CNES, proced.Codigo);
            if (cotas != null)
            {
                cotas.Percentual = float.Parse(tbxPercentual.Text.Trim());
                iAgendamento.Salvar(cotas);
                Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 12, "ID_EAS:" + cotas.Id_EstabelecimentoSaude +" ID_PROCEDIMENTO:"+ cotas.Id_Procedimento));
            }
            else
            {
                iAgendamento.Salvar(cota);
                Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 12, "ID_EAS:" + cota.Id_EstabelecimentoSaude + " ID_PROCEDIMENTO:" + cota.Id_Procedimento));
            }
            row[1] = tbxPercentual.Text;
            GridViewUnidades.DataSource = table;
            GridViewUnidades.DataBind();
        }

        protected void tbxCompetencia_TextChanged(object sender, EventArgs e)
        {
            lblQtdOfertada.Text  = Factory.GetInstance<IAmbulatorial>().BuscarQuantidadeOfertada<Int64>(tbxCodigo.Text, int.Parse(tbxCompetencia.Text)).ToString();
        }
    }
}
