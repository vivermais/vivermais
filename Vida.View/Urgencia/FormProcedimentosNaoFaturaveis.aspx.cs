using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Urgencia
{
    public partial class FormProcedimentosNaoFaturaveis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CADASTRAR_PROCEDIMENTO_NAO_FATURAVEL", Modulo.URGENCIA))
                {
                    if (Request["co_procedimento"] != null)
                    {
                        ViewState["co_proc"] = Request["co_procedimento"].ToString();
                        ProcedimentoNaoFaturavel proc = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ProcedimentoNaoFaturavel>(int.Parse(ViewState["co_proc"].ToString()));
                        TextBox_NomeProcedimento.Text = proc.Nome;

                        if (proc.Status == 'I')
                        {
                            RadioButton_Inativo.Checked = true;
                            RadioButton_Ativo.Checked = false;
                        }
                        else
                        {
                            RadioButton_Inativo.Checked = false;
                            RadioButton_Ativo.Checked = true;
                        }
                    }
                }else
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        /// <summary>
        /// Salva o procedimento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_SalvarProcedimento(object sender, EventArgs e)
        {
            try
            {
                IProcedimentoNaoFaturavel iUrgencia = Factory.GetInstance<IProcedimentoNaoFaturavel>();
                ProcedimentoNaoFaturavel procedimento = ViewState["co_proc"] == null ? new ProcedimentoNaoFaturavel() : iUrgencia.BuscarPorCodigo<ProcedimentoNaoFaturavel>(int.Parse(ViewState["co_proc"].ToString()));
                procedimento.Nome = TextBox_NomeProcedimento.Text;
                procedimento.Status = RadioButton_Ativo.Checked ? 'A' : 'I';
                //procedimento.CodigoProcedimento = long.Parse(TextBox_Codigo.Text);

                //if (iUrgencia.ValidarCadastroProcedimento(procedimento.CodigoProcedimento, procedimento.Codigo))
                //{
                iUrgencia.Salvar(procedimento);
                iUrgencia.Inserir(new LogUrgencia(DateTime.Now, ((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, 18, "ID:" + procedimento.Codigo));

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Procedimento salvo com sucesso.');location='FormExibeProcedimentosNaoFaturaveis.aspx';", true);
                //}else
                //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe um procedimento com o código informado! Por favor, digite outro código.');", true);
            }
            catch (Exception f)
            {
                throw f;
            }
        }
    }
}
