using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using AjaxControlToolkit;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using System.IO;

namespace ViverMais.View.Urgencia
{
    public partial class FormConfirmarBaixaExame : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CONFIRMAR_ENTREGA_EXAME", Modulo.URGENCIA))
                    CarregaExamesProntuario();
                else
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        /// <summary>
        /// Carrega os prontuários da unidade com resultado disponível
        /// </summary>
        private void CarregaExamesProntuario()
        {
            Usuario usuario = Session["Usuario"] as Usuario;
            IList<ProntuarioExame> exames = Factory.GetInstance<IExame>().ListarPorEstabelecimentoDisponivelEntrega<ProntuarioExame>(usuario.Unidade.CNES);
            //Factory.GetInstance<IExame>().ListarPorEstabelecimentoSaude<ProntuarioExame>(((Usuario)Session["Usuario"]).Unidade.CNES).Where(p=>p.DataResultado.HasValue && !p.DataConfirmacaoBaixa.HasValue).ToList();
            var consulta = from prontuario in exames
                           group prontuario by prontuario.Prontuario
                               into p
                               orderby p.Key.NumeroToString
                               //select new { CodigoProntuario = p.Key.Codigo, NumeroAtendimento = p.Key.NumeroToString, PacienteNome = "", PacienteDescricao = "" };
                               select new { CodigoProntuario = p.Key.Codigo, NumeroAtendimento = p.Key.NumeroToString, PacienteNome = !string.IsNullOrEmpty(p.Key.Paciente.CodigoViverMais) ? Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(p.Key.Paciente.CodigoViverMais).Nome : p.Key.Paciente.Nome, PacienteDescricao = string.IsNullOrEmpty(p.Key.Paciente.Descricao) ? " - " : p.Key.Paciente.Descricao };

            if (exames.Count < 1)
            {
                var stream = new FileStream(Server.MapPath("~/Urgencia/Documentos/EntregaExame/" + usuario.Unidade.CNES + ".txt"), FileMode.Truncate, FileAccess.Write, FileShare.Write);
                TextWriter escrita = new StreamWriter(stream);

                //stream.Seek(0, SeekOrigin.Begin);
                escrita.WriteLine('N');
                escrita.Flush();
                stream.Close();
            }

            GridView_Pacientes.DataSource = consulta;
            GridView_Pacientes.DataBind();
        }

        /// <summary>
        /// Formata o gridview com o padrão especificado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridView(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Accordion accordionexames = (Accordion)e.Row.FindControl("Accordion_Exames");
                GridView gridviewexames = (GridView)accordionexames.Panes[0].FindControl("GridView_Exames");

                long co_prontuario = long.Parse(GridView_Pacientes.DataKeys[e.Row.RowIndex]["CodigoProntuario"].ToString());
                IList<ProntuarioExame> examesconfirmados = Factory.GetInstance<IExame>().BuscarExamesPorProntuario<ProntuarioExame>(co_prontuario).Where(p => p.DataResultado.HasValue && !p.DataConfirmacaoBaixa.HasValue).ToList();

                var consulta = from prontuario in examesconfirmados
                               select new { DataSolicitacao = prontuario.Data.ToString("dd/MM/yyyy HH:mm"), Exame = prontuario.Exame.Descricao, CodigoExame = prontuario.Codigo, Resultado = prontuario.Resultado };

                gridviewexames.DataSource = consulta;
                gridviewexames.DataBind();
            }
        }

        /// <summary>   
        /// Paginação do gridview de exames
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Paginacao_Exames(object sender, GridViewPageEventArgs e)
        {
            CarregaExamesProntuario();
            GridView_Pacientes.PageIndex = e.NewPageIndex;
            GridView_Pacientes.DataBind();
        }

        /// <summary>
        /// Confirma a entrega do exame para o paciente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_ConfirmarEntregaExame(object sender, EventArgs e)
        {
            long co_exame = long.Parse(((LinkButton)sender).CommandArgument.ToString());

            try
            {
                ProntuarioExame prontuario = Factory.GetInstance<IExame>().BuscarPorCodigo<ProntuarioExame>(co_exame);
                prontuario.DataConfirmacaoBaixa = DateTime.Now;
                prontuario.UsuarioConfirmacaoBaixa = ((Usuario)Session["Usuario"]).Codigo;

                Factory.GetInstance<IExame>().Salvar(prontuario);

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Confirmação realizada com sucesso!');", true);
                CarregaExamesProntuario();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
