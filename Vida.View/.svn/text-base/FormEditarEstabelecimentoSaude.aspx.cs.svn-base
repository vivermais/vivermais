using System;
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
using Vida.ServiceFacade.ServiceFacades.EstabelecimentoDeSaude;
using Vida.ServiceFacade.ServiceFacades.Localidade;
using Vida.ServiceFacade.ServiceFacades.Paciente;

namespace Vida.View
{
    public partial class FormEditarEstabelecimentoSaude : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                long temp;

                if (Request["codigo"] != null && long.TryParse(Request["codigo"].ToString(), out temp))
                {
                    CarregaMunicipios();
                    CarregaDadosUnidade(Request["codigo"].ToString());
                }
            }
        }

        /// <summary>
        /// Carrega todos os munícipios cadastrados
        /// </summary>
        private void CarregaMunicipios()
        {
            //IMunicipio iMunicipio = Factory.GetInstance<IMunicipio>();
            //IList<TB_MS_MUNICIPIO> lm = iMunicipio.ListarTodos<TB_MS_MUNICIPIO>();

            //ddlMunicipioGestor.Items.Add(new ListItem("Selecione...", "0"));

            //foreach (TB_MS_MUNICIPIO m in lm)
            //    ddlMunicipioGestor.Items.Add(new ListItem(m.DS_MUNICIPIO, m.CO_MUNICIPIO));
        }

        /// <summary>
        /// Carrega os dados do estabelecimento de saúde referenciado
        /// </summary>
        /// <param name="id_unidade">código do estabelecimento de saúde</param>
        private void CarregaDadosUnidade(string id_unidade)
        {
            //IEstabelecimentoDeSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoDeSaude>();
            //EstabelecimentoDeSaude estabelecimento   = iEstabelecimento.BuscarPorCodigo<EstabelecimentoDeSaude>(Request["codigo"].Clone());

            //if (estabelecimento != null)
            //{
            //    tbxRazaoSocial.Text                      = estabelecimento.R_SOCIAL;
            //    tbxNomeFantasia.Text                     = estabelecimento.NOME_FANTA;
            //    tbxLogradouro.Text                       = estabelecimento.LOGRADOURO;
            //    //ddlMunicipioGestor.SelectedValue=
            //    tbxBairro.Text                           = estabelecimento.BAIRRO;
            //    tbxCEP.Text                              = estabelecimento.COD_CEP;

            //    if (estabelecimento.STATUSMOV == "B")
            //    {
            //        btn_ativar.Visible  = true;
            //        btn_excluir.Visible = false;
            //    }
            //}
        }

        /// <summary>
        /// Atualiza o estabelecimento de saúde de acordo com os dados informados
        /// </summary>
        /// <param name="sender">Objeto de envio</param>
        /// <param name="e">Evento do button</param>
        protected void btn_atualizar_Click(object sender, EventArgs e) 
        {
            //IEstabelecimentoDeSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoDeSaude>();
            //EstabelecimentoDeSaude estabelecimento   = iEstabelecimento.BuscarPorCodigo<EstabelecimentoDeSaude>(Request["codigo"].Clone());

            //estabelecimento.R_SOCIAL   = tbxRazaoSocial.Text;
            //estabelecimento.NOME_FANTA = tbxNomeFantasia.Text;
            //estabelecimento.LOGRADOURO = tbxLogradouro.Text;
            //estabelecimento.BAIRRO     = tbxBairro.Text;
            //estabelecimento.COD_CEP    = tbxCEP.Text;

            //try
            //{
            //    iEstabelecimento.Salvar(estabelecimento);
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados atualizados com sucesso!');", true);
            //}catch(Exception f)
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert(Erro:" + f.Message + ");", true);
            //    throw f;
            //}
        }

        /// <summary>
        /// Exclui o estabelecimento de saúde
        /// </summary>
        /// <param name="sender">Objeto de envio</param>
        /// <param name="e">Evento do button</param>
        protected void btn_excluir_Click(object sender, EventArgs e) 
        {
            //IEstabelecimentoDeSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoDeSaude>();
            //EstabelecimentoDeSaude estabelecimento   = iEstabelecimento.BuscarPorCodigo<EstabelecimentoDeSaude>(Request["codigo"].Clone());

            //estabelecimento.STATUSMOV = "B";

            //try
            //{
            //    iEstabelecimento.Salvar(estabelecimento);
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Estabelecimento excluído com sucesso!');", true);
            //}
            //catch (Exception f) 
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert(Erro:" + f.Message + ");", true);
            //    throw f;
            //}
        }

        /// <summary>
        /// Ativa o estabelecimento de saúde
        /// </summary>
        /// <param name="sender">Objeto de envio</param>
        /// <param name="e">Evento do button</param>
        protected void btn_ativar_Click(object sender, EventArgs e) 
        {
            //IEstabelecimentoDeSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoDeSaude>();
            //EstabelecimentoDeSaude estabelecimento   = iEstabelecimento.BuscarPorCodigo<EstabelecimentoDeSaude>(Request["codigo"].Clone());
  
            //estabelecimento.STATUSMOV = "U";

            //try
            //{
            //    iEstabelecimento.Salvar(estabelecimento);
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Estabelecimento ativado com sucesso!');", true);
            //}
            //catch (Exception f) 
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert(Erro:" + f.Message + ");", true);
            //    throw f;
            //}
        }
    }
}
