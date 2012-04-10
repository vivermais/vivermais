using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vida.ServiceFacade.ServiceFacades;
using Vida.DAO;
using Vida.Model;
using Vida.ServiceFacade.ServiceFacades.Paciente;
using Vida.ServiceFacade.ServiceFacades.Profissional;
using Vida.ServiceFacade.ServiceFacades.Farmacia.Misc;
using Vida.ServiceFacade.ServiceFacades.Farmacia.Dispensacao;

namespace Vida.View.Farmacia
{
    public partial class FormDispensacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CarregaDropsDown();
        }

        /// <summary>
        /// Carrega os dropsdowns de distrito, categorias, municipios da tela
        /// </summary>
        private void CarregaDropsDown() 
        {
            IVidaServiceFacade ivida = Factory.GetInstance<IVidaServiceFacade>();
            IList<Vida.Model.Distrito> distritos = ivida.ListarTodos<Distrito>();
            ddlDistrito.Items.Add(new ListItem("Selecione...", "0"));
            foreach (Distrito t in distritos)
                ddlDistrito.Items.Add(new ListItem(t.Nome, t.Codigo.ToString()));

            IList<CategoriaOcupacao> categorias = ivida.ListarTodos<CategoriaOcupacao>();//new GenericBsn<Categoria>().ListarTodos();
            ddlCategoria.Items.Add(new ListItem("Selecione...", "0"));
            foreach (CategoriaOcupacao c in categorias)
                ddlCategoria.Items.Add(new ListItem(c.Nome, c.Codigo.ToString()));

            IList<Municipio> municipios = ivida.ListarTodos<Municipio>();
            ddlMunicipio.Items.Add(new ListItem("Selecione...","0"));
            foreach (Municipio m in municipios)
                ddlMunicipio.Items.Add(new ListItem(m.Nome, m.Codigo.ToString()));
           
        }

        /// <summary>
        /// Função utilizada para pesquisar a existência de um paciente/profissional
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Pequisar_Click(object sender, EventArgs e)
        {
            ImageButton img = (ImageButton)sender;

            switch (img.CommandArgument)
            {
                case "paciente":
                    IPaciente ipaciente = Factory.GetInstance<IPaciente>();
                    //PacienteBsn pacienteBSn = new PacienteBsn();
                    Vida.Model.Paciente paciente = ipaciente.PesquisarPacientePorCNS<Vida.Model.Paciente>(tbxCartaoSUS.Text);//pacienteBSn.buscaSUS(tbxCartaoSUS.Text);

                    if (paciente != null)
                        tbxPaciente.Text = paciente.Nome;
                    else
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não foi econtrado paciente algum com o número do SUS informado!');", true);
                break;
                case "profissional":
                    //ProfissionalBsn profissionalBsn = new ProfissionalBsn();
                    IProfissional iprofissional = Factory.GetInstance<IProfissional>();
                    Vida.Model.Profissional profissional = iprofissional.BuscaProfissionalPorVinculo<Vida.Model.Profissional>(int.Parse(ddlCategoria.SelectedValue.ToString()), int.Parse(tbxNumeroConselho.Text));//profissionalBsn.Lista(int.Parse(tbxNumeroConselho.Text), "", int.Parse(ddlCategoria.SelectedValue.ToString()));
                    if (profissional != null)
                        tbxProfissional.Text = profissional.Nome;
                    else
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não foi encontrado profissional algum com os dados informados!');", true);
                break;
            }
        }

        /// <summary>
        /// Função que válida o cadastro da dispensação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Salvar_Click(object sender, EventArgs e)
        {
            //ProfissionalBsn profissionalBsn = new ProfissionalBsn();
            IVidaServiceFacade ivida = Factory.GetInstance<IVidaServiceFacade>();
            IPaciente ipaciente = Factory.GetInstance<IPaciente>();
            IProfissional iprofissional = Factory.GetInstance<IProfissional>();
            Vida.Model.Profissional profissional = iprofissional.BuscaProfissionalPorVinculo<Vida.Model.Profissional>(int.Parse(ddlCategoria.SelectedValue.ToString()), int.Parse(tbxNumeroConselho.Text));//profissionalBsn.Lista(int.Parse(tbxNumeroConselho.Text), "", int.Parse(ddlCategoria.SelectedValue.ToString()));
            //PacienteBsn pacienteBSn = new PacienteBsn();
            //Paciente paciente = pacienteBSn.buscaSUS(tbxCartaoSUS.Text);
            Vida.Model.Paciente paciente = ipaciente.PesquisarPacientePorCNS<Vida.Model.Paciente>(tbxCartaoSUS.Text);

            //MunicipioBsn municipioBsn = new MunicipioBsn();
            //Municipio municipio = municipioBsn.buscarPorCodigo(int.Parse(ddlMunicipio.SelectedValue.ToString()));
            Municipio municipio = ivida.BuscarPorCodigo<Municipio>(int.Parse(ddlMunicipio.SelectedValue));
            //DistritoBsn distritoBsn = new DistritoBsn();
            //Distrito distrito = distritoBsn.BuscarPorCodigo<Distrito>(int.Parse(ddlDistrito.SelectedValue.ToString()));
            Distrito distrito = ivida.BuscarPorCodigo<Distrito>(int.Parse(ddlDistrito.SelectedValue.ToString()));
            //DispensacaoBsn dispensacaBsn = new DispensacaoBsn();
            IDispensacao idispensacao = Factory.GetInstance<IDispensacao>();
            Dispensacao dispensacao;

            if (profissional == null)// || profissionais.Count <= 0)
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não foi encontrado profissional algum com os dados informados!');", true);
            else
            {
                if (paciente == null)
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não foi econtrado paciente algum com o número do SUS informado!');", true);
                else
                {
                    int resultado = CriticarInclusao(profissional, paciente, DateTime.Parse(tbxDataReceita.Text.ToString()));

                    if (resultado == 1)
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A data da receita já passou do prazo de 1 ano de validade!');", true);
                    else
                        if (resultado == 2)
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe uma receita cadastrada para esta data com os dados informados!');", true);
                        else
                            if (resultado == 3)
                            {
                                painel_dois.Visible = true;
                                painel_um.Enabled   = false;
                            }
                            else
                            {
                                salvarDispensacao(paciente, profissional, municipio, (Vida.Model.EstabelecimentoSaude)Session["unidade"], distrito, DateTime.Parse(tbxDataReceita.Text.ToString()));
                                dispensacao = idispensacao.BuscarPorProfissionalPaciente<Vida.Model.Dispensacao>(profissional.Codigo, paciente.Codigo, DateTime.Parse(tbxDataReceita.Text));//dispensacaBsn.buscarDispensacao(profissionais[0].Codigo, paciente.Codigo, DateTime.Parse(tbxDataReceita.Text.ToString()));
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Receita cadastrada com sucesso!');location='FormItensDispensacao.aspx?id_dispensacao=" + dispensacao.Codigo + "';", true);
                            }
                    }
              }
        }

        /// <summary>
        /// Salva a dispensação com as configurações indicadas
        /// </summary>
        /// <param name="paciente"></param>
        /// <param name="profissional"></param>
        /// <param name="municipio"></param>
        /// <param name="unidade"></param>
        /// <param name="distrito"></param>
        /// <param name="data"></param>
        private void salvarDispensacao(Vida.Model.Paciente paciente, Vida.Model.Profissional profissional, Municipio municipio, Vida.Model.EstabelecimentoSaude unidade, Distrito distrito, DateTime data) 
        {
            //DispensacaoBsn dispensacaoBsn = new DispensacaoBsn();
            Dispensacao dispensacao = new Dispensacao();
            dispensacao.CodigoPaciente = paciente.Codigo;
            dispensacao.CodigoProfissional = profissional.Codigo;
            dispensacao.CodigoUnidade = unidade.Codigo;
            dispensacao.CodigoMunicipio = municipio.Codigo;
            //dispensacao = distrito;
            dispensacao.DataReceita = data;
            Factory.GetInstance<IFarmacia>().Salvar(dispensacao);
            //dispensacaoBsn.Salvar(dispensacao);
        }

        /// <summary>
        /// Função que verifica se a data da receita ainda está válida ou não
        /// e que verifica se já foi cadastrada um receita com a data, paciente e profissional indicado
        /// </summary>
        /// <param name="profissional"></param>
        /// <param name="paciente"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        int CriticarInclusao(Vida.Model.Profissional profissional, Vida.Model.Paciente paciente, DateTime data)
        {
            TimeSpan diff = DateTime.Today.Subtract(data);

            if (diff.Days > 365) //Receita com validade maior que 1 ano
                return 1;
            else
                if (diff.Days > 182 && diff.Days <= 365) //Receita com validade entre 6 meses e 1 ano
                    return 3;

            //DispensacaoBsn dispensacaoBsn = new DispensacaoBsn();
            IDispensacao idispensacao = Factory.GetInstance<IDispensacao>();
            Dispensacao dispensacao = idispensacao.BuscarPorProfissionalPaciente<Vida.Model.Dispensacao>(profissional.Codigo, paciente.Codigo, data);//dispensacaoBsn.buscarDispensacao(profissional.Codigo, paciente.Codigo, data);

            if (dispensacao != null) //Receita já existente com esta data, paciente e profissional responsável
                return 2;

            return 0;
        }

        /// <summary>
        /// Função que confirma a liberação para a receita que possui a validade entre 6 meses e 1 ano.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_confirmar_liberacao_Click(object sender, EventArgs e) 
        {
            IProfissional iprofissional = Factory.GetInstance<IProfissional>();
            //ProfissionalBsn profissionalBsn = new ProfissionalBsn();
            Vida.Model.Profissional profissional = iprofissional.BuscaProfissionalPorVinculo<Vida.Model.Profissional>(int.Parse(ddlCategoria.SelectedValue.ToString()), int.Parse(tbxNumeroConselho.Text));//profissionalBsn.Lista(int.Parse(tbxNumeroConselho.Text), "", int.Parse(ddlCategoria.SelectedValue.ToString()));
            IPaciente ipaciente = Factory.GetInstance<IPaciente>();
            //PacienteBsn pacienteBSn = new PacienteBsn();
            Vida.Model.Paciente paciente = ipaciente.PesquisarPacientePorCNS<Vida.Model.Paciente>(tbxCartaoSUS.Text);//pacienteBSn.buscaSUS(tbxCartaoSUS.Text);
            //MunicipioBsn municipioBsn = new MunicipioBsn();
            IVidaServiceFacade ivida = Factory.GetInstance<IVidaServiceFacade>();
            Municipio municipio = ivida.BuscarPorCodigo<Vida.Model.Municipio>(int.Parse(ddlMunicipio.SelectedValue.ToString()));//municipioBsn.buscarPorCodigo(int.Parse(ddlMunicipio.SelectedValue.ToString()));
            //DistritoBsn distritoBsn = new DistritoBsn();
            Distrito distrito = ivida.BuscarPorCodigo<Vida.Model.Distrito>(int.Parse(ddlDistrito.SelectedValue.ToString()));//distritoBsn.BuscarPorCodigo<Distrito>(int.Parse(ddlDistrito.SelectedValue.ToString()));
            //DispensacaoBsn dispensacaBsn = new DispensacaoBsn();
            IDispensacao idispensacao = Factory.GetInstance<IDispensacao>();
            Dispensacao dispensacao;

            salvarDispensacao(paciente, profissional, municipio, (Vida.Model.EstabelecimentoSaude)Session["unidade"], distrito, DateTime.Parse(tbxDataReceita.Text.ToString()));
            dispensacao = idispensacao.BuscarPorProfissionalPaciente<Vida.Model.Dispensacao>(profissional.Codigo, paciente.Codigo, DateTime.Parse(tbxDataReceita.Text));//dispensacaBsn.buscarDispensacao(profissionais[0].Codigo, paciente.Codigo, DateTime.Parse(tbxDataReceita.Text.ToString()));
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dispensação cadastrada com sucesso!');location='FormItensDispensacao.aspx?id_dispensacao=" + dispensacao.Codigo + "';", true);
        }

        /// <summary>
        /// Função que cancela a ação de cadastro para a receita que possui validade entre 6 meses e 1 ano.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_cancelar_liberacao_Click(object sender, EventArgs e) 
        {
            painel_um.Enabled   = true;
            painel_dois.Visible = false;
        }

    }
}
