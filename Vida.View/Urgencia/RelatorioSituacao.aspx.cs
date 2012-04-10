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
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using CrystalDecisions.CrystalReports.Engine;
using ViverMais.View.Urgencia.RelatoriosCrystal;

namespace ViverMais.View.Urgencia
{
    public partial class RelatorioSituacao : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            IUrgenciaServiceFacade iurgencia = Factory.GetInstance<IUrgenciaServiceFacade>();
            SituacaoAtendimento situacao = iurgencia.BuscarPorCodigo<SituacaoAtendimento>(int.Parse(Request.QueryString["id_situacao"]));
            
            DataTable cab = new DataTable();
            cab.Columns.Add("Situacao", typeof(string));
            cab.Columns.Add("Unidade", typeof(string));
            cab.Columns.Add("Distrito", typeof(string));

            Usuario usuario = (Usuario)Session["usuario"];

            DataRow linhacabecalho = cab.NewRow();
            linhacabecalho["Unidade"] = usuario.Unidade.NomeFantasia;
            linhacabecalho["Distrito"] = usuario.Unidade.Bairro.Distrito.Nome;
            linhacabecalho["Situacao"] = situacao.Nome;

            cab.Rows.Add(linhacabecalho);
            //lblSituacao.Text = situacao.Nome;
            //Unidade unidade = new Unidade();
            //unidade = (Unidade)Session["unidade"];
            
            ViverMais.Model.EstabelecimentoSaude unidade = iViverMais.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(Request.QueryString["id_unidade"]);
            
            //lblDistrito.Text = unidade.Distrito.Nome;//usuario.Unidade.Distrito.Nome;
            //lblUnidade.Text = unidade.NomeFantasia;//usuario.Unidade.NomeFantasia;

            //Cid cid = iurgencia.BuscarPorCodigo<Cid>(Request.QueryString["codigo"]);

            //lblCid.Text = cid.Nome;
            //lblData.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");

            IRelatorioUrgencia irelatorio = Factory.GetInstance<IRelatorioUrgencia>();
            IList<Prontuario> prontuarios = irelatorio.ObterRelatorioPorSituacao<Prontuario>(unidade.CNES, situacao.Codigo);
            //DataTable dataTable = new RelatoriosBsn().ObterRelatorioPorSituacao(unidade, cid, situacao);
            DataTable table = new DataTable();
            DataColumn col1 = new DataColumn("Nome");
            DataColumn col4 = new DataColumn("Atendimento");
            DataColumn col2 = new DataColumn("Data");
            DataColumn col3 = new DataColumn("Dias");
            table.Columns.Add(col1);
            table.Columns.Add(col4);
            table.Columns.Add(col2);
            table.Columns.Add(col3);

            foreach (Prontuario pront in prontuarios)
            {
                DataRow row = table.NewRow();

                if (!string.IsNullOrEmpty(pront.Paciente.CodigoViverMais))
                    row[0] = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(pront.Paciente.CodigoViverMais).Nome;
                else
                    row[0] = pront.Paciente.Nome; //iurgencia.BuscarPorCodigo<ViverMais.Model.PacienteUrgence>(pront.Paciente.Codigo).Descricao;

                row[1] = pront.NumeroToString;
                row[2] = pront.Data.ToString("dd/MM/yyyy");
                //obtém a quantidade de dias entre a atualizacao do prontuario e a data atual
                //ou seja, o numero de dias que o paciente esta esperando
                int dias = DateTime.Now.Subtract(pront.Data).Days;
                row[3] = dias.ToString();
                table.Rows.Add(row);
            }

            DSCabecalhoRelSituacao dscabecalho = new DSCabecalhoRelSituacao();
            DSRelatorioSituacao dssituacao = new DSRelatorioSituacao();

            dscabecalho.Tables.Add(cab);
            dssituacao.Tables.Add(table);

            ReportDocument doc = new ReportDocument();
            doc.Load(Server.MapPath("RelatoriosCrystal/RelSituacao.rpt"));
            doc.SetDataSource(dssituacao.Tables[1]);
            doc.Subreports[0].SetDataSource(dscabecalho.Tables[1]);

            Session["documentoImpressaoUrgencia"] = doc;
            Response.Redirect("FormImprimirCrystalReports.aspx");

            //CrystalReportViewer_Situacao.ReportSource = doc;
            //CrystalReportViewer_Situacao.DataBind();

            //GridView1.DataSource = table;
            //GridView1.DataBind();
        }
    }
}
