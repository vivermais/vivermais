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
using System.Collections.Generic;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;

namespace ViverMais.View.Agendamento
{
    public partial class FormParametroEspecifica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ParametroAgenda parametroagenda = (ParametroAgenda)Session["parametroagenda"];
            int id_parametroagenda = parametroagenda.Codigo;

            if (!IsPostBack)
            {
                tbxCnes.Text = Session["Cnes"].ToString();
                lblCnes.Text = Session["Unidade"].ToString();

                if (Request.QueryString["id_parametroagendaespecifica"] != null)
                {
                    int id_parametroagendaespecifica = int.Parse(Request.QueryString["id_parametroagendaespecifica"]);

                    IAgendamentoServiceFacade iagendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
                    ParametroAgendaEspecifica parametroagendaespecifica = iagendamento.BuscarPorCodigo<ParametroAgendaEspecifica>(id_parametroagendaespecifica);
                    string id_unidade = parametroagendaespecifica.ID_Unidade;

                    // Busca CNES em ViverMais
                    IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();
                    ViverMais.Model.EstabelecimentoSaude estabelecimento = iEstabelecimento.BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(tbxCnes.Text);
                    if (estabelecimento == null)
                    {
                        lblCnesEspecifica.Text = "CNES não cadastrado";
                        lknSalvar.Enabled = false;
                        lknExcluir.Enabled = false;
                        tbxCnesEspecifica.Focus();
                        return;
                    }
                    else
                    {
                        id_unidade = estabelecimento.CNES;
                        tbxCnesEspecifica.Text = estabelecimento.CNES;
                        lblCnesEspecifica.Text = estabelecimento.NomeFantasia;
                        Session["id_unidadeespecifica"] = estabelecimento.CNES;
                    }

                    tbxCnes.Text = Session["Cnes"].ToString();
                    lblCnes.Text = Session["Unidade"].ToString();

                    lknExcluir.Enabled = true;
                }
                else
                {
                    lknExcluir.Enabled = false;
                }
            }

            IParametroAgendaEspecifica iAgendamento = Factory.GetInstance<IParametroAgendaEspecifica>();
            IList<ViverMais.Model.ParametroAgendaEspecifica> parametros = iAgendamento.BuscarParametros<ViverMais.Model.ParametroAgendaEspecifica>(id_parametroagenda);

            DataTable table = new DataTable(); ;
            DataColumn c0 = new DataColumn("Codigo");
            DataColumn c1 = new DataColumn("Unidade");
            table.Columns.Add(c0);
            table.Columns.Add(c1);

            foreach (ParametroAgendaEspecifica ag in parametros)
            {
                DataRow row = table.NewRow();
                row[0] = ag.Codigo;

                string id_unidade = ag.ID_Unidade;

                IViverMaisServiceFacade iUnidade = Factory.GetInstance<IViverMaisServiceFacade>();
                ViverMais.Model.EstabelecimentoSaude estabelecimento = iUnidade.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(id_unidade);
                
                row[1] = estabelecimento.NomeFantasia;

                table.Rows.Add(row);
            }

            gridAgenda.DataSource = table;
            gridAgenda.DataBind();
        }

        protected void Salvar_Click(object sender, EventArgs e)
        {
            ParametroAgenda parametroagenda = (ParametroAgenda)Session["parametroagenda"];
            int id_parametroagenda = parametroagenda.Codigo;
            string id_unidade = Session["id_unidadeespecifica"].ToString();
            IParametroAgendaEspecifica iParametroAgendaEspecifica = Factory.GetInstance<IParametroAgendaEspecifica>();
            ParametroAgendaEspecifica parametroagendaespecifica = null;
            if (Request.QueryString["id_parametroagendaespecifica"] != null)
            {
                int id_parametroagendaespecifica = int.Parse(Request.QueryString["id_parametroagendaespecifica"]);
                parametroagendaespecifica = iParametroAgendaEspecifica.BuscarPorCodigo<ParametroAgendaEspecifica>(id_parametroagendaespecifica);
            }
            else
            {
                // Criticar se é a mesma CNES
                if (tbxCnesEspecifica.Text == tbxCnes.Text)
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Utilização de mesma CNES. Operação Inválida !');</script>");
                    return;
                }

                // Criticar se já existe este registro                 
                parametroagendaespecifica = iParametroAgendaEspecifica.BuscarDuplicidade<ViverMais.Model.ParametroAgendaEspecifica>(id_parametroagenda, id_unidade);
                if (parametroagendaespecifica != null)
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Registro já cadastrado !');</script>");
                    return;
                }
                parametroagendaespecifica = new ParametroAgendaEspecifica();
            }
            parametroagendaespecifica.ID_Unidade = id_unidade;
            parametroagendaespecifica.ParametroAgenda = (ParametroAgenda)Session["parametroagenda"];
            iParametroAgendaEspecifica.Salvar(parametroagendaespecifica);
            if (Request.QueryString["id_parametroagendaespecifica"] != null)
                iParametroAgendaEspecifica.Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 18, parametroagendaespecifica.Codigo.ToString()));
            else
                iParametroAgendaEspecifica.Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 17, parametroagendaespecifica.Codigo.ToString()));
            
            ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Dados salvos com sucesso!');window.location='FormParametroEspecifica.aspx?id_parametroagendaespecifica=" + parametroagendaespecifica.Codigo + "'</script>");
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            ParametroAgenda parametroagenda = (ParametroAgenda)Session["parametroagenda"];
            int id_parametroagenda = parametroagenda.Codigo;
            Response.Redirect("FormParametroAgenda.aspx?id_parametroagenda=" + id_parametroagenda);
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            IParametroAgendaEspecifica iParametroAgendaEspecifica = Factory.GetInstance<IParametroAgendaEspecifica>();
            if (Request.QueryString["id_parametroagendaespecifica"] != null)
            {
                ParametroAgendaEspecifica parametroagendaespecifica = null;
                int id_parametroagendaespecifica = int.Parse(Request.QueryString["id_parametroagendaespecifica"]);
                parametroagendaespecifica = iParametroAgendaEspecifica.BuscarPorCodigo<ParametroAgendaEspecifica>(id_parametroagendaespecifica);
                int id_parametroagenda = parametroagendaespecifica.ParametroAgenda.Codigo;

                iParametroAgendaEspecifica.Deletar(parametroagendaespecifica);
                iParametroAgendaEspecifica.Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 19, "ID_PROGRAMA:"+parametroagendaespecifica.ID_Programa+" ID_UNIDADE:"+parametroagendaespecifica.ID_Unidade+" PARAMETRO_AGENDA:"+parametroagendaespecifica.ParametroAgenda.Codigo));
                
                IParametroAgendaEspecifica iAgendamento = Factory.GetInstance<IParametroAgendaEspecifica>();
                IList<ViverMais.Model.ParametroAgendaEspecifica> parametros = iAgendamento.BuscarParametros<ViverMais.Model.ParametroAgendaEspecifica>(id_parametroagenda);

                DataTable table = new DataTable(); ;
                DataColumn c0 = new DataColumn("Codigo");
                DataColumn c1 = new DataColumn("Distrito");
                table.Columns.Add(c0);
                table.Columns.Add(c1);

                foreach (ParametroAgendaEspecifica ag in parametros)
                {
                    DataRow row = table.NewRow();
                    row[0] = ag.Codigo;

                    string id_unidade = ag.ID_Unidade;

                    IViverMaisServiceFacade iUnidade = Factory.GetInstance<IViverMaisServiceFacade>();
                    ViverMais.Model.EstabelecimentoSaude estabelecimento = iUnidade.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(id_unidade);

                    row[1] = estabelecimento.NomeFantasia;

                    table.Rows.Add(row);
                }

                gridAgenda.DataSource = table;
                gridAgenda.DataBind();
            }
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            ParametroAgenda parametroagenda = (ParametroAgenda)Session["parametroagenda"];
            int id_parametroagenda = parametroagenda.Codigo;
            Response.Redirect("FormParametroEspecifica.aspx?id_parametroagenda=" + parametroagenda.Codigo);
        }

        protected void tbxCnesEspecifica_TextChanged(object sender, EventArgs e)
        {
            if (tbxCnesEspecifica.Text != "")
            {
                string id_unidade = "";
                IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();
                ViverMais.Model.EstabelecimentoSaude estabelecimento = iEstabelecimento.BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(tbxCnes.Text);
                if (estabelecimento == null)
                {
                    lblCnesEspecifica.Text = "CNES não cadastrado";
                    lknSalvar.Enabled = false;
                    tbxCnesEspecifica.Focus();
                    return;
                }
                else
                {
                    id_unidade = estabelecimento.CNES;
                    tbxCnesEspecifica.Text = estabelecimento.CNES;
                    lblCnesEspecifica.Text = estabelecimento.NomeFantasia;
                    Session["id_unidadeespecifica"] = estabelecimento.CNES;
                    lknSalvar.Enabled = true;
                }
            }
        }
    }
}
