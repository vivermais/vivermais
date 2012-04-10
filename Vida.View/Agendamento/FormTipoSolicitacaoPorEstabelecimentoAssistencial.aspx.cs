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
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Agendamento
{
    public partial class FormTipoSolicitacaoPorEstabelecimentoAssistencial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "PARAMETRIZACAO_TIPO_SOLICITACAO_EAS", Modulo.AGENDAMENTO))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            IEstabelecimentoSaude iEstabelecimentoSaude = Factory.GetInstance<IEstabelecimentoSaude>();
            ViverMais.Model.EstabelecimentoSaude estabelecimento = new ViverMais.Model.EstabelecimentoSaude();
            string cnesEstabelecimento = tbxCNES.Text;
            if ((cnesEstabelecimento.Equals(String.Empty)) || (!cnesEstabelecimento.Equals("")))
            {
                estabelecimento = iEstabelecimentoSaude.BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(cnesEstabelecimento);
            }

            if (estabelecimento != null)
            {
                SaveCheckBox(chkbxTipoSolicitacao, estabelecimento);
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Dados Salvos com Sucesso!');window.location='FormTipoSolicitacaoPorEstabelecimentoAssistencial.aspx';</script>");
                return;
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Estabelecimento Inválido');</script>");
                return;
            }
        }

        private void SaveCheckBox(Control pai, ViverMais.Model.EstabelecimentoSaude estabelecimento)
        {
            IAgendamentoServiceFacade iAgendamentoServiceFacade = Factory.GetInstance<IAgendamentoServiceFacade>();
            ITipoSolicitacaoEstabelecimento iTipoSolicitacaoEstabelecimento = Factory.GetInstance<ITipoSolicitacaoEstabelecimento>();

            if (pai is CheckBoxList)
            {
                CheckBoxList chkbox = (CheckBoxList)pai;
                //Enquanto Existir ListItem
                foreach(ListItem list in chkbox.Items)
                {
                    //Busca para ver se já existe na base
                    ParametroTipoSolicitacaoEstabelecimento tipo = iTipoSolicitacaoEstabelecimento.BuscarTipoSolicitacaoPorEstabelecimento<ParametroTipoSolicitacaoEstabelecimento>(estabelecimento.CNES, list.Value);
                    if (tipo == null)//Se não existir
                    {
                        if (list.Selected)//Selecionou?
                        {
                            tipo = new ParametroTipoSolicitacaoEstabelecimento();
                            tipo.TipoSolicitacao = list.Value;
                            tipo.CodigoEstabelecimento = estabelecimento.CNES;
                            //parametro.EstabelecimentoSaude = estabelecimento;
                            iAgendamentoServiceFacade.Inserir(tipo);
                            Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now,((Usuario)Session["Usuario"]).Codigo,23,"Tipo Estabelecimento :"+tipo.CodigoEstabelecimento+" CodigoEstabelecimento:"+tipo.CodigoEstabelecimento));
                        }
                    }
                    else//Se já existir
                    {
                        if (!list.Selected)//Está Desmarcado?
                        {
                            Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 24, "Tipo Estabelecimento :" + tipo.CodigoEstabelecimento + " CodigoEstabelecimento:" + tipo.CodigoEstabelecimento));
                            iAgendamentoServiceFacade.Deletar(tipo);//Deleto da base
                        }
                    }
                }
            }
        }

        private void FillCheckBox(Control pai, ViverMais.Model.EstabelecimentoSaude estabelecimento)
        {
            ITipoSolicitacaoEstabelecimento iTipoSolicitacaoEstabelecimento = Factory.GetInstance<ITipoSolicitacaoEstabelecimento>();
            
            if (pai is CheckBoxList)
            {
                CheckBoxList chkbox = (CheckBoxList)pai;
                //Enquanto Existir ListItem
                foreach (ListItem list in chkbox.Items)
                {
                    ParametroTipoSolicitacaoEstabelecimento tipo = iTipoSolicitacaoEstabelecimento.BuscarTipoSolicitacaoPorEstabelecimento<ParametroTipoSolicitacaoEstabelecimento>(estabelecimento.CNES, list.Value);
                    if (tipo != null)
                        list.Selected = true;
                    else
                        list.Selected = false;
                }
            }
        }

        protected void tbxCNES_TextChanged(object sender, EventArgs e)
        {
            IEstabelecimentoSaude iEstabelecimentoSaude = Factory.GetInstance<IEstabelecimentoSaude>();
            ViverMais.Model.EstabelecimentoSaude estabelecimentoSaude = iEstabelecimentoSaude.BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(tbxCNES.Text);
            if (estabelecimentoSaude != null)
            {
                lblNomeFantasia.Text = estabelecimentoSaude.NomeFantasia;
                FillCheckBox(chkbxTipoSolicitacao, estabelecimentoSaude);
            }

            else
            {
                lblNomeFantasia.Text = "Estabelecimento não Localizado";
                tbxCNES.Text = string.Empty;
            }
                
            //Carrego os CheckBox Rerefentes

            //CheckBoxList chk = ;
            //foreach (ListItem list in chkbxTipoSolicitacao.Items)
            //{
            //    if (list.Selected)
            //        chkbxTipoSolicitacao.SelectedValue = true;

            //}
        }
    }
}
