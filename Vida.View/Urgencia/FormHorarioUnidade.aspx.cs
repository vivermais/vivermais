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
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;

namespace ViverMais.View.Urgencia
{
    public partial class FormHorarioUnidade : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "ALTERAR_HORARIO_VIGENCIA_PRESCRICAO",Modulo.URGENCIA))
                {
                    ViverMais.Model.HorarioUnidade horariounidade = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.HorarioUnidade>(((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.CNES);
                    ViverMais.Model.EstabelecimentoSaude unidade = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.CNES);
                    if (horariounidade != null)
                        lblHorarioAtual.Text = horariounidade.Horario;
                    else
                        lblHorarioAtual.Text = "-";
                    lblUnidade.Text = unidade.NomeFantasia;
                    ViewState["co_unidade"] = unidade.CNES;
                }else
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime.Parse(tbxHorario.Text + ":00");
            }
            catch (Exception ex) 
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O horário informado não corresponde a um horário válido!');", true);
                return;
            }

            IUrgenciaServiceFacade iUrgencia = Factory.GetInstance<IUrgenciaServiceFacade>();
            ViverMais.Model.HorarioUnidade horario = new ViverMais.Model.HorarioUnidade();

            horario.CodigoUnidade = ViewState["co_unidade"].ToString();
            horario.Horario = tbxHorario.Text;

            if (lblHorarioAtual.Text == "-")//não existe horário cadastrado ainda
                iUrgencia.Inserir(horario);
            else
                iUrgencia.Atualizar(horario);

            iUrgencia.Inserir(new LogUrgencia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 28, "horario unidade:" + horario.CodigoUnidade));

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados salvos com sucesso.');", true);
            lblHorarioAtual.Text = horario.Horario;
                
        }
    }
}
