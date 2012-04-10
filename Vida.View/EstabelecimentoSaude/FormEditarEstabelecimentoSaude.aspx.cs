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
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.EstabelecimentoSaude
{
    public partial class FormEditarEstabelecimentoSaude : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_ESTABELECIMENTO_SAUDE", Modulo.ESTABELECIMENTO_SAUDE))
                {
                    long temp;

                    if (Request["unidade"] != null && long.TryParse(Request["unidade"].ToString(), out temp))
                    {
                        ViewState["unidade"] = Request["unidade"].ToString();
                        CarregaTiposEstabelecimentos();
                        CarregaMunicipios();
                        //CarregaDistritos();
                        CarregaDadosUnidade(Request["unidade"].ToString());

                        //Logradouro logradouro = new Logradouro();
                        //logradouro.
                    }
                }
                else
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, procure a administração.');location='Default.aspx';", true);
            }
        }

        /// <summary>
        /// Buscar endereço
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_BuscarEndereco(object sender, EventArgs e)
        {
            Logradouro logradouro = Factory.GetInstance<ILogradouro>().BuscarPorCEP<Logradouro>(long.Parse(tbxCEP.Text));

            if (logradouro != null)
            {
                tbxLogradouro.Text = logradouro.NomeLogradouro;
                ddlMunicipioGestor.SelectedValue = ddlMunicipioGestor.Items.FindByText(logradouro.Cidade.Trim()) != null ? ddlMunicipioGestor.Items.FindByText(logradouro.Cidade.Trim()).Value : "0";
                OnSelectedIndexChanged_CarregaDistritos(new object(), new EventArgs());

                IList<Bairro> bairros = Factory.GetInstance<IBairro>().BuscarPorNome<Bairro>(logradouro.Bairro);
                Bairro b = null;

                foreach (Bairro bairro in bairros)
                {
                    if (bairro.Distrito != null && bairro.Distrito.Municipio != null && bairro.Distrito.Municipio.Codigo == ddlMunicipioGestor.SelectedValue)
                    {
                        b = bairro;
                        break;
                    }
                }

                if (b != null)
                {
                    ddlDistrito.SelectedValue = b.Distrito.Codigo.ToString();
                    OnSelectedIndexChanged_CarregaBairro(new object(), new EventArgs());
                    DropDownList_Bairro.SelectedValue = b.Codigo.ToString();
                }
                //if (bairro.Distrito != null && bairro.Distrito.Municipio.Codigo == ddlMunicipioGestor.SelectedValue)
                //{
                //}
                //tbxBairro.Text = logradouro.Bairro;
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Endereço não encontrado.');", true);
        }

        /// <summary>
        /// Carrega os tipos de estabelecimentos de saúde existentes
        /// </summary>
        private void CarregaTiposEstabelecimentos()
        {
            IList<ViverMais.Model.TipoEstabelecimento> lte = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<ViverMais.Model.TipoEstabelecimento>().OrderBy(te => te.Descricao).ToList();

            ddltipoestabelecimento.DataSource = lte;
            ddltipoestabelecimento.DataBind();
            ddltipoestabelecimento.Items.Insert(0, new ListItem("SELECIONE...", "0"));
            //foreach (ViverMais.Model.TipoEstabelecimento t in lte)
            //    ddltipoestabelecimento.Items.Add(new ListItem(t.Descricao, t.Codigo.ToString()));
        }

        /// <summary>
        /// Carrega todos os munícipios cadastrados
        /// </summary>
        private void CarregaMunicipios()
        {
            IList<ViverMais.Model.Municipio> lm = Factory.GetInstance<IMunicipio>().ListarPorEstado<ViverMais.Model.Municipio>("BA").OrderBy(m => m.Nome).ToList();
            //char[] del = {'-', '-' };
            
            ddlMunicipioGestor.DataSource = lm;
            ddlMunicipioGestor.DataBind();
            ddlMunicipioGestor.Items.Insert(0, new ListItem("Selecione...", "0"));
            //foreach (ViverMais.Model.Municipio m in lm)
            //    ddlMunicipioGestor.Items.Add(new ListItem(m.Nome.Split('-')[0].Trim(), m.Codigo.ToString()));
        }

        /// <summary>
        /// Carrega os distritos cadastrados
        /// </summary>
        private void CarregaDistritos()
        {
            //IList<ViverMais.Model.Distrito> ld = Factory.GetInstance<IDistrito>().BuscarPorMunicipio<ViverMais.Model.Distrito>(ddlMunicipioGestor.SelectedValue).OrderBy(d=>d.Nome).ToList();

            //ddlDistrito.Items.Clear();
            //ddlDistrito.Items.Add(new ListItem("Selecione...", "0"));
            //foreach (ViverMais.Model.Distrito d in ld)
            //    ddlDistrito.Items.Add(new ListItem(d.Nome,d.Codigo.ToString()));
        }

        protected void OnSelectedIndexChanged_CarregaDistritos(object sender, EventArgs e)
        {
            IList<Distrito> distritos = new List<Distrito>();
            //ddlDistrito.Items.Clear();
            DropDownList_Bairro.Items.Clear();
            DropDownList_Bairro.Items.Add(new ListItem("Selecione...", "0"));

            if (ddlMunicipioGestor.SelectedValue != "0")
                distritos = Factory.GetInstance<IDistrito>().BuscarPorMunicipio<ViverMais.Model.Distrito>(ddlMunicipioGestor.SelectedValue).Where(p => p.Nome != "NAO SE APLICA" && p.Nome != "NÃO SE APLICA").OrderBy(d => d.Nome).ToList();

                //foreach (ViverMais.Model.Distrito d in ld)
                //    ddlDistrito.Items.Add(new ListItem(d.Nome, d.Codigo.ToString()));

            ddlDistrito.DataSource = distritos;
            ddlDistrito.DataBind();

            ddlDistrito.Items.Insert(0, new ListItem("Selecione...", "0"));
            ddlDistrito.Focus();
        }

        protected void OnSelectedIndexChanged_CarregaBairro(object sender, EventArgs e)
        {
            //DropDownList_Bairro.Items.Clear();
            IList<Bairro> bairros = new List<Bairro>();

            if (ddlDistrito.SelectedValue != "0")
                bairros = Factory.GetInstance<IBairro>().BuscarPorDistrito<Bairro>(int.Parse(ddlDistrito.SelectedValue));
                    //Factory.GetInstance<IBairro>().ListarTodos<Bairro>().Where(p => p.Distrito != null && p.Distrito.Codigo == int.Parse(ddlDistrito.SelectedValue)).OrderBy(p => p.Nome).ToList();

                //foreach (ViverMais.Model.Bairro bairro in bairros)
                //    DropDownList_Bairro.Items.Add(new ListItem(bairro.Nome, bairro.Codigo.ToString()));

            DropDownList_Bairro.DataSource = bairros;
            DropDownList_Bairro.DataBind();

            DropDownList_Bairro.Items.Insert(0, new ListItem("Selecione...", "0"));
            DropDownList_Bairro.Focus();
        }

        /// <summary>
        /// Carrega os dados do estabelecimento de saúde referenciado
        /// </summary>
        /// <param name="id_unidade">código do estabelecimento de saúde</param>
        private void CarregaDadosUnidade(string id_unidade)
        {
            ViverMais.Model.EstabelecimentoSaude estabelecimento = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(id_unidade);

            if (estabelecimento != null)
            {
                tbxRazaoSocial.Text  = estabelecimento.RazaoSocial;
                tbxNomeFantasia.Text = estabelecimento.NomeFantasia;
                tbxLogradouro.Text   = estabelecimento.Logradouro;
                //tbxBairro.Text       = estabelecimento.Bairro;
                tbxCEP.Text          = estabelecimento.CEP;
                Label_CNES.Text      = estabelecimento.CNES;
                //tbxCNES.Text         = estabelecimento.CNES;
                tbxSiglaEstabelecimento.Text = estabelecimento.SiglaEstabelecimento;
                ddlMunicipioGestor.SelectedValue = estabelecimento.MunicipioGestor.Codigo;
                OnSelectedIndexChanged_CarregaDistritos(new object(), new EventArgs());
                if (estabelecimento.Bairro != null)
                {
                    
                    //OnSelectedIndexChanged_CarregaDistritos(new object(), new EventArgs());
                    ddlDistrito.SelectedValue = estabelecimento.Bairro.Distrito.Codigo.ToString();
                    OnSelectedIndexChanged_CarregaBairro(new object(), new EventArgs());
                    DropDownList_Bairro.SelectedValue = estabelecimento.Bairro.Codigo.ToString();
                }

                //if (estabelecimento.MunicipioGestor != null && ddlMunicipioGestor.Items.FindByValue(estabelecimento.MunicipioGestor.Codigo.ToString()) != null)
                //    ddlMunicipioGestor.SelectedValue = estabelecimento.MunicipioGestor.Codigo.ToString();

                if (estabelecimento.TipoEstabelecimento != null && ddltipoestabelecimento.Items.FindByValue(estabelecimento.TipoEstabelecimento.Codigo.ToString()) != null ) 
                    ddltipoestabelecimento.SelectedValue = estabelecimento.TipoEstabelecimento.Codigo.ToString();

                //if (estabelecimento.Distrito != null && ddlDistrito.Items.FindByValue(estabelecimento.Distrito.Codigo.ToString()) != null)
                //    ddlDistrito.SelectedValue = estabelecimento.Distrito.Codigo.ToString();

                if (estabelecimento.StatusEstabelecimento == Convert.ToChar(ViverMais.Model.EstabelecimentoSaude.DescricaoStatus.Bloqueado).ToString())
                {
                    Panel_Ativar.Visible = true;
                    Panel_Desativar.Visible = false;
                }
                else
                {
                    Panel_Ativar.Visible = false;
                    Panel_Desativar.Visible = true;
                }
            }
        }

        /// <summary>
        /// Atualiza o estabelecimento de saúde de acordo com os dados informados
        /// </summary>
        /// <param name="sender">Objeto de envio</param>
        /// <param name="e">Evento do button</param>
        protected void btn_atualizar_Click(object sender, EventArgs e) 
        {
            ViverMais.Model.EstabelecimentoSaude estabelecimento = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(ViewState["unidade"].ToString());
            if (estabelecimento.MunicipioGestor.Codigo == "292740")//Quando o Município for Salvador, o CEP e o Bairro são obrigatório
            {
                if (ddlDistrito.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('- Distrito é Obrigatório!');", true);
                    return;
                }
                else if (DropDownList_Bairro.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('- Bairro é Obrigatório!');", true);
                    return;
                }
            }
            estabelecimento.RazaoSocial         = tbxRazaoSocial.Text;
            estabelecimento.NomeFantasia        = tbxNomeFantasia.Text;
            //estabelecimento.CNES                = tbxCNES.Text;
            estabelecimento.Logradouro          = tbxLogradouro.Text;
            //estabelecimento.Bairro              = tbxBairro.Text;
            estabelecimento.CEP                 = tbxCEP.Text;
            estabelecimento.SiglaEstabelecimento = tbxSiglaEstabelecimento.Text;
            estabelecimento.MunicipioGestor     = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.Municipio>(ddlMunicipioGestor.SelectedValue);
            //estabelecimento.Distrito            = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.Distrito>(int.Parse(ddlDistrito.SelectedValue));
            estabelecimento.TipoEstabelecimento = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.TipoEstabelecimento>(ddltipoestabelecimento.SelectedValue.Clone());
            estabelecimento.Bairro = Factory.GetInstance<IBairro>().BuscarPorCodigo<Bairro>(int.Parse(DropDownList_Bairro.SelectedValue));
            
            try
            {
                Factory.GetInstance<IViverMaisServiceFacade>().Atualizar(estabelecimento);
                Factory.GetInstance<IViverMaisServiceFacade>().Inserir(new LogViverMais(DateTime.Now,Factory.GetInstance<IUsuario>().BuscarPorCodigo<Usuario>(((Usuario)Session["Usuario"]).Codigo),8,"UNIDADE CNES:" + estabelecimento.CNES));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados atualizados com sucesso!');location='Default.aspx';", true);
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados atualizados com sucesso!');", true);
                //this.CarregaDadosUnidade(estabelecimento.CNES);
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Ativa/Desativa o estabelecimento de saúde
        /// </summary>
        /// <param name="sender">Objeto de envio</param>
        /// <param name="e">Evento do button</param>
        protected void btn_ativar_excluir_Click(object sender, EventArgs e) 
        {
            ViverMais.Model.EstabelecimentoSaude estabelecimento = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(ViewState["unidade"].ToString());
            
            string frase = string.Empty;

            if (((LinkButton)sender).CommandArgument == "ativar")
            {
                estabelecimento.StatusEstabelecimento = Convert.ToChar(ViverMais.Model.EstabelecimentoSaude.DescricaoStatus.Ativo).ToString();
                frase = "Estabelecimento desbloqueado com sucesso!";
            }
            else
            {
                estabelecimento.StatusEstabelecimento = Convert.ToChar(ViverMais.Model.EstabelecimentoSaude.DescricaoStatus.Bloqueado).ToString();
                frase = "Estabelecimento bloqueado com sucesso!";
            }

            try
            {
                Factory.GetInstance<IViverMaisServiceFacade>().Atualizar(estabelecimento);

                if (((LinkButton)sender).CommandArgument == "ativar")
                    Factory.GetInstance<IViverMaisServiceFacade>().Inserir(new LogViverMais(DateTime.Now, Factory.GetInstance<IUsuario>().BuscarPorCodigo<Usuario>(((Usuario)Session["Usuario"]).Codigo), 10, "UNIDADE CNES:" + estabelecimento.CNES));
                else
                    Factory.GetInstance<IViverMaisServiceFacade>().Inserir(new LogViverMais(DateTime.Now, Factory.GetInstance<IUsuario>().BuscarPorCodigo<Usuario>(((Usuario)Session["Usuario"]).Codigo), 9, "UNIDADE CNES:" + estabelecimento.CNES));

                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + frase + "');location='FormEditarEstabelecimentoSaude.aspx?unidade=" + ViewState["unidade"].ToString() + "';", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + frase + "');", true);

                if (estabelecimento.StatusEstabelecimento == Convert.ToChar(ViverMais.Model.EstabelecimentoSaude.DescricaoStatus.Bloqueado).ToString())
                {
                    Panel_Ativar.Visible = true;
                    Panel_Desativar.Visible = false;
                }
                else
                {
                    Panel_Ativar.Visible = false;
                    Panel_Desativar.Visible = true;
                }
            }
            catch (Exception f)
            {
                throw f;
            }
        }
    }
}
