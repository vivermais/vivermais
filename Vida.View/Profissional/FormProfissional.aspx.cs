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
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.Model.Entities.ViverMais;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;


namespace ViverMais.View.Profissional
{
    public partial class FormProfissional : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CADASTRAR_PROFISSIONAL_SOLICITANTE", Modulo.PROFISSIONAL))
                {
                    IList<ViverMais.Model.CategoriaOcupacao> categorias = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<ViverMais.Model.CategoriaOcupacao>();

                    // Lista todas as ocupações
                    foreach (ViverMais.Model.CategoriaOcupacao categoria in categorias)
                    {
                        ddlCategoria.Items.Add(new ListItem(categoria.Nome, categoria.Codigo.ToString()));
                    }

                    ddlCategoria.SelectedValue = "1"; //Define Médico como Default
                    //IList<UF> ufs = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<UF>();
                    //foreach (UF uf in ufs)
                    //{
                    //    ddlEstado.Items.Add(new ListItem(uf.Nome, uf.Codigo));
                    //}

                    //Caso seja uma edição
                    if (Request.QueryString["codigo"] != null)
                    {
                        int id_profissional = int.Parse(Request.QueryString["codigo"].ToString());
                        ProfissionalSolicitante profissional = Factory.GetInstance<IProfissionalSolicitante>().BuscarPorCodigo<ProfissionalSolicitante>(id_profissional);
                        if (profissional != null)
                        {
                            tbxNomeProfissional.Text = profissional.Nome;
                            tbxNumConselho.Text = profissional.NumeroRegistro;
                            ddlCategoria.SelectedValue = profissional.OrgaoEmissorRegistro.CategoriaOcupacao.Codigo;
                            //ddlEstado.SelectedValue = profissional.UfProfssional.Codigo;
                        }
                    }
                }
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            //Salva o Profissional
            ProfissionalSolicitante profissional = new ProfissionalSolicitante();
            
            //Caso seja uma edição
            if (Request.QueryString["codigo"] != null)
            {
                int id_profissional = int.Parse(Request.QueryString["codigo"].ToString());
                IList<ProfissionalSolicitante> profissionais = Factory.GetInstance<IProfissionalSolicitante>().BuscaProfissionalPorNumeroConselhoECategoria<ProfissionalSolicitante>(ddlCategoria.SelectedValue, tbxNumConselho.Text.Trim(), "", id_profissional);
                if (profissionais.Count != 0)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe Profissional com os Dados Informados!');", true);
                    return;
                }
                profissional = Factory.GetInstance<IProfissionalSolicitante>().BuscarPorCodigo<ProfissionalSolicitante>(id_profissional);
                profissional.Nome = tbxNomeProfissional.Text.ToUpper();
                profissional.NumeroRegistro = tbxNumConselho.Text.Trim();
                OrgaoEmissor org = Factory.GetInstance<IOrgaoEmissor>().BuscarOrgaoEmissorPorCategoria<OrgaoEmissor>(ddlCategoria.SelectedValue);
                profissional.OrgaoEmissorRegistro = org;
                //profissional.UfProfssional = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<UF>(ddlEstado.SelectedValue);
                profissional.Status = "ATIVO";
                Factory.GetInstance<IViverMaisServiceFacade>().Salvar(profissional);
                Factory.GetInstance<IViverMaisServiceFacade>().Inserir(new LogViverMais(DateTime.Now,Factory.GetInstance<IUsuario>().BuscarPorCodigo<Usuario>(((Usuario)Session["Usuario"]).Codigo),12,"NUM CONSELHO:" + profissional.NumeroRegistro + " NOME:" + profissional.Nome));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados Salvos com Sucesso!');", true);
                return;
            }
            else
            {
                //Verifica se Já existe Profissional com os Dados informado
                IList<ProfissionalSolicitante> profissionais = Factory.GetInstance<IProfissionalSolicitante>().BuscaProfissionalPorNumeroConselhoECategoria<ProfissionalSolicitante>(ddlCategoria.SelectedValue, tbxNumConselho.Text.Trim(), "", 0);
                if (profissionais.Count != 0)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe Profissional com os Dados Informados!');", true);
                    return;
                }
                profissional.Nome = tbxNomeProfissional.Text.ToUpper();
                profissional.NumeroRegistro = tbxNumConselho.Text;
                OrgaoEmissor org = Factory.GetInstance<IOrgaoEmissor>().BuscarOrgaoEmissorPorCategoria<OrgaoEmissor>(ddlCategoria.SelectedValue);
                profissional.OrgaoEmissorRegistro = org;
                //profissional.UfProfssional = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<UF>(ddlEstado.SelectedValue);
                profissional.Status = "ATIVO";
                Factory.GetInstance<IViverMaisServiceFacade>().Inserir(profissional);
                int codigo = Factory.GetInstance<IProfissionalSolicitante>().BuscaUltimoRegistroIncluido<ProfissionalSolicitante>().Codigo;
                Factory.GetInstance<IViverMaisServiceFacade>().Inserir(new LogViverMais(DateTime.Now, Factory.GetInstance<IUsuario>().BuscarPorCodigo<Usuario>(((Usuario)Session["Usuario"]).Codigo), 11, "PROF ID:" + codigo));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados Salvos com Sucesso!');", true);
                return;
            }
        }
    }
}
