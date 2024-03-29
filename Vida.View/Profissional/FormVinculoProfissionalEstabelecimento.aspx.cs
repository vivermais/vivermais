﻿using System;
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
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.BLL;
using ViverMais.ServiceFacade.ServiceFacades.Misc;

namespace ViverMais.View.Profissional
{
    public partial class FormVinculoProfissionalEstabelecimento : System.Web.UI.Page
    {

        void CarregaDropDownRacaCor()
        {
            //Populo o DDL de Raca/Cor
            IList<RacaCor> racaCor = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<RacaCor>();
            ddlRacaCor.Items.Add(new ListItem("Selecione...", "0"));
            foreach (RacaCor raca in racaCor)
                ddlRacaCor.Items.Add(new ListItem(raca.Descricao, raca.Codigo));
        }

        void CarregaVinculacao()
        {
            //Lista Todas os Tipos de Vinculacao
            IList<Vinculacao> vinculacoes = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<Vinculacao>();
            ddlVinculacao.Items.Add(new ListItem("Selecione", "0"));
            foreach (Vinculacao vinculacao in vinculacoes)
                ddlVinculacao.Items.Add(new ListItem(vinculacao.Descricao, vinculacao.Codigo));
        }

        void CarregaBairros()
        {
            //Lista todos os Bairros
            IList<Bairro> bairro = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<Bairro>();
            ddlBairro.Items.Add(new ListItem("Selecione", "0"));
            foreach (Bairro b in bairro)
            {
                ddlBairro.Items.Add(new ListItem(b.Nome, b.Codigo.ToString()));
            }
        }

        void CarregaTipoLogradouro()
        {
            //Lista todos os Tipos de Logradouro
            IList<TipoLogradouro> tipoLogradouro = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<TipoLogradouro>("Descricao", true);
            ddlTipoLogradouro.Items.Add(new ListItem("Selecione...", "0"));
            foreach (TipoLogradouro tp in tipoLogradouro)
            {
                ddlTipoLogradouro.Items.Add(new ListItem(tp.Descricao, tp.Codigo));
            }
        }

        void CarregaUFs()
        {
            //Lista todas as UF
            IList<UF> uf = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<UF>("Sigla", true);
            ddlUFProf.Items.Add(new ListItem("Selecione...", "0"));
            foreach (UF estado in uf)
            {
                ddlUFProf.Items.Add(new ListItem(estado.Sigla, estado.Sigla));
            }
        }

        void CarregaBanco()
        {
            //Lista todos os Bancos
            IList<Banco> banco = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<Banco>("Descricao", true);
            ddlBanco.Items.Add(new ListItem("Selecione...", "0"));
            foreach (Banco banc in banco)
            {
                ddlBanco.Items.Add(new ListItem(banc.Descricao, banc.Codigo));
            }
        }

        void CarregaOrgaoEmissor()
        {
            //Lista todos os Orgãos emissores
            IList<OrgaoEmissor> orgaoEmissor = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<OrgaoEmissor>("Nome", true);
            ddlOrgaoEmissorRegistroConselho.Items.Add(new ListItem("Selecione...", "0"));
            ddlOrgaoEmissorRG.Items.Add(new ListItem("Selecione...", "0"));
            foreach (OrgaoEmissor org in orgaoEmissor)
            {
                ddlOrgaoEmissorRegistroConselho.Items.Add(new ListItem(org.Nome, org.Codigo));
                ddlOrgaoEmissorRG.Items.Add(new ListItem(org.Nome, org.Codigo));
            }
        }

        void ListaVinculacoes()
        {
            //Lista todas as Vinculações
            IList<Vinculacao> vinculacao = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<Vinculacao>();
            ddlVinculacao.Items.Add(new ListItem("Selecione...", "0"));
            foreach (Vinculacao vinc in vinculacao)
            {
                ddlVinculacao.Items.Add(new ListItem(vinc.Descricao, vinc.Codigo));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_VINCULO_PROFISSIONAL", Modulo.PROFISSIONAL))
                {
                    Wizard1.ActiveStepIndex = 0;
                    Session["VinculosProfissional"] = null;
                    PanelVinculosAtivos.Visible = false;
                    PanelGridviewListaProfissionais.Visible = false;
                    PanelEstrangeiroSelecionado.Visible = false;
                    lblSemRegistro.Visible = false;
                    CarregaBairros();
                    CarregaBanco();
                    CarregaDropDownRacaCor();
                    CarregaOrgaoEmissor();
                    CarregaTipoLogradouro();
                    CarregaUFs();
                    CarregaVinculacao();
                    CriaTabelaVinculos();
                    lblSemVinculos.Visible = false;
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        private DataTable CriaTabelaVinculos()
        {
            DataTable table = new DataTable();
            table.Columns.Add("CodigoEstabelecimento");
            table.Columns.Add("Nome Fantasia");
            table.Columns.Add("CodigoCBO");
            table.Columns.Add("CBO");
            table.Columns.Add("CodigoVinculacao");
            table.Columns.Add("Vinculacao");
            table.Columns.Add("CodigoTipoVinculacao");
            table.Columns.Add("Tipo");
            table.Columns.Add("CodigoSubTipoVinculacao");
            table.Columns.Add("SubTipo");
            table.Columns.Add("SUS");
            table.Columns.Add("CargaHorariaAmbulatorial");
            table.Columns.Add("CargaHorariaHospitalar");
            table.Columns.Add("CargaHorariaOutros");
            table.Columns.Add("NumeroRegistro");
            table.Columns.Add("CodigoOrgaoEmissor");
            table.Columns.Add("OrgaoEmissor");
            table.Columns.Add("Status", typeof(bool));
            Session["VinculosProfissional"] = table;
            return table;
        }

        protected void btnBuscarPacienteViverMais_OnClick(object sender, EventArgs e)
        {
            ViverMais.Model.Paciente paciente;
            ViverMais.Model.Profissional profissional = (ViverMais.Model.Profissional)Session["Profissional"];
            //Se a Busca não for pelo Texto digitado, ele pegará pelo profissional selecionado
            if (String.IsNullOrEmpty(tbxCartaoSUS.Text))
            {
                if (profissional != null)
                {
                    paciente = Factory.GetInstance<IPaciente>().PesquisarPacientePorCNS<ViverMais.Model.Paciente>(profissional.CartaoSUS);
                    if (paciente == null)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Não foi Localizado nenhum paciente com o Cartão SUS Informado no Cadastro do Profissional.\\nPor favor, Verifique o Cartão SUS: " + tbxCartaoSUS.Text + ", pois o Profissional deve ser um Paciente SUS!');", true);
                        return;
                    }
                    else
                        CarregaDadosDoProfissional(profissional);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Desculpa, o servidor se comportou de maneira inesperada.\\nPor favor, Pesquise o Profissional novamente!');", true);
                    return;
                }
            }
            else
            {
                string cartaoSUS = tbxCartaoSUS.Text.Trim();
                paciente = Factory.GetInstance<IPaciente>().PesquisarPacientePorCNS<ViverMais.Model.Paciente>(cartaoSUS);
                if (paciente == null)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Não foi Localizado nenhum paciente com o Cartão SUS Informado no Cadastro do Profissional.\\nPor favor, Verifique o Cartão SUS: " + tbxCartaoSUS.Text + ", pois o Profissional deve ser um Paciente SUS!');", true);
                    tbxCartaoSUS.Text = string.Empty;
                    return;
                }
                else
                {
                    if (profissional != null)
                        CarregaDadosDoProfissional(profissional);
                    else
                    {
                        CarregaDadosDoProfissionalComoPacienteViverMais(paciente);
                    }
                }

            }
        }

        void CarregaDadosDoProfissionalComoPacienteViverMais(ViverMais.Model.Paciente paciente)
        {
            //Dados de Identificação
            tbxCartaoSUS.Text = Factory.GetInstance<IPaciente>().ListarCartoesSUS<ViverMais.Model.CartaoSUS>(paciente.Codigo).LastOrDefault().Numero;
            tbxNomeProfissional.Text = paciente.Nome;
            rbtSexo.SelectedValue = paciente.Sexo.ToString();
            tbxNomeMae.Text = paciente.NomeMae;
            tbxNomePai.Text = paciente.NomePai;
            tbxDataNascimento.Text = paciente.DataNascimento.ToString("dd/MM/yyyy");
            rbtNacionalidade.SelectedValue = paciente.Pais.Codigo == Pais.BRASIL || paciente.Pais.Codigo == Pais.NATURALIZADO ? "1" : "2";
            if (paciente.RacaCor != null)
                ddlRacaCor.SelectedValue = paciente.RacaCor.Codigo;
            tbxCPF.Enabled = false;
            tbxDataAtualizacao.Enabled = false;

            //Carrega os Documentos
            List<ViverMais.Model.Documento> documentos = DocumentoBLL.PesqusiarPorPaciente(paciente);
            if (documentos.Count > 0)
            {
                foreach (ViverMais.Model.Documento documento in documentos)
                {
                    if (documento.ControleDocumento != null && documento.ControleDocumento.TipoDocumento != null)
                    {
                        switch (documento.ControleDocumento.TipoDocumento.Codigo)
                        {
                            case "03":
                                tbxTituloEleitor.Text = documento.Numero;
                                tbxSecaoEleitoral.Text = documento.SecaoEleitoral;
                                tbxZonaEleitoral.Text = documento.ZonaEleitoral;
                                break;
                            case "04":
                                tbxCTPS.Text = documento.Numero;
                                tbxSerieCTPS.Text = documento.Serie;
                                break;
                            case "10":
                                tbxRG.Text = documento.Numero + documento.Complemento;
                                tbxDataEmissaoRG.Text = documento.DataEmissao.Value.ToString("dd/MM/yyyy");
                                ddlOrgaoEmissorRG.SelectedValue = documento.OrgaoEmissor == null ? "0" : documento.OrgaoEmissor.Codigo;
                                break;
                        }
                    }
                }
            }

            //Carrega o Endereço
            ViverMais.Model.Endereco endereco = EnderecoBLL.PesquisarCompletoPorPaciente(paciente);
            if (endereco != null)
            {
                MunicipioBLL.CompletarMunicipio(endereco.Municipio);
                tbxLogradouro.Text = endereco.Logradouro;
                tbxNumeroEndereco.Text = endereco.Numero;
                tbxComplemento.Text = endereco.Complemento;
                ddlBairro.ClearSelection();
                ddlTipoLogradouro.SelectedValue = endereco.TipoLogradouro != null ? endereco.TipoLogradouro.Codigo : "0";
                tbxCEP.Text = endereco.CEP;
                ddlBairro.SelectedIndex = ddlBairro.Items.IndexOf(ddlBairro.Items.FindByText(endereco.Bairro));
                tbxMunicipio.Text = endereco.Municipio.NomeSemUF;
                ddlUFProf.SelectedValue = endereco.Municipio.UF != null ? endereco.Municipio.UF.Sigla : "0";
                tbxTelefone.Text = endereco.DDD + endereco.Telefone;
            }
        }

        protected void btnBuscarProfissional_Click(object sender, EventArgs e)
        {
            IVinculo iVinculo = Factory.GetInstance<IVinculo>();
            string cpf = tbxCPFPesquisa.Text;
            bool cpfValido = HelperCPFCNPJValidator.ValidaCPF(cpf);
            string valor = cpf.Replace(".", "").Replace("-", "");

            if (!cpfValido)
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('CPF Inválido!');", true);
            else
            {
                IList<ViverMais.Model.VinculoProfissional> vinculosProfissional = iVinculo.BuscarProfissionalPorCPF<ViverMais.Model.VinculoProfissional>(valor);
                //ViverMais.Model.Profissional profissional = Factory.GetInstance<IProfissional>().BuscarPorCodigo<ViverMais.Model.Profissional>(vinculosProfissional[0].Profissional.Codigo);

                if (vinculosProfissional.Count == 0)
                {
                    panelConfirmacao.Visible = true;
                    lblConfirmacao.Text = "Não existe vínculo com o CPF informado. <br><br> Deseja vincular este profissional?";
                }
                else
                {
                    DataTable table = new DataTable();
                    DataColumn c1 = new DataColumn("Codigo");
                    DataColumn c2 = new DataColumn("Nome");
                    DataColumn c3 = new DataColumn("Categoria");
                    DataColumn c4 = new DataColumn("RegistroConselho");
                    table.Columns.Add(c1);
                    table.Columns.Add(c2);
                    table.Columns.Add(c3);
                    table.Columns.Add(c4);
                    DataRow row = table.NewRow();
                    row[0] = vinculosProfissional[0].Profissional.CPF;
                    row[1] = vinculosProfissional[0].Profissional.Nome;
                    row[2] = vinculosProfissional[0].OrgaoEmissorRegistroConselho != null && vinculosProfissional[0].OrgaoEmissorRegistroConselho.CategoriaOcupacao != null ? vinculosProfissional[0].OrgaoEmissorRegistroConselho.CategoriaOcupacao.Nome : String.Empty;
                    row[3] = vinculosProfissional[0].RegistroConselho;

                    table.Rows.Add(row);

                    GridViewListaProfissionais.DataSource = table;
                    GridViewListaProfissionais.DataBind();
                    PanelGridviewListaProfissionais.Visible = true;
                }
            }
        }

        protected void tbxCNES_TextChanged(object sender, EventArgs e)
        {
            IEstabelecimentoSaude iEstabelecimentoSaude = Factory.GetInstance<IEstabelecimentoSaude>();
            ViverMais.Model.EstabelecimentoSaude estabelecimentoSaude = iEstabelecimentoSaude.BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(tbxCNES.Text);
            if ((estabelecimentoSaude != null) && (estabelecimentoSaude.CNES != "000000"))//Codigo Para Não Se Aplica
                tbxNomeEstabelecimento.Text = estabelecimentoSaude.NomeFantasia;
            else
            {
                tbxNomeEstabelecimento.Text = "Estabelecimento não Localizado";
                tbxCNES.Text = String.Empty;
            }
        }

        protected void btnAddVinculo_Click(object sender, EventArgs e)
        {
            int cont = 0;
            int somaTotalCargaHoraria = 0;

            DataTable table = (DataTable)Session["VinculosProfissional"];
            IViverMaisServiceFacade iViverMaisServiceFacade = Factory.GetInstance<IViverMaisServiceFacade>();
            ITipoVinculacao iTipoVinculacao = Factory.GetInstance<ITipoVinculacao>();
            ISubTipoVinculacao iSubTipoVinculacao = Factory.GetInstance<ISubTipoVinculacao>();
            ViverMais.Model.Profissional profissional = (ViverMais.Model.Profissional)Session["Profissional"];
            ViverMais.Model.EstabelecimentoSaude estabelecimentoSaude = Factory.GetInstance<IEstabelecimentoSaude>().BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(tbxCNES.Text);
            CBO cbo = iViverMaisServiceFacade.BuscarPorCodigo<CBO>(tbxCBO.Text);
            string identifVinculo = (ddlVinculacao.SelectedValue + ddlTipoVinculacao.SelectedValue + ddlSubTipoVinculacao.SelectedValue);
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row1 in table.Rows)
                    {
                        int somaCargaHoraria = int.Parse(row1[11].ToString()) + int.Parse(row1[12].ToString()) + int.Parse(row1[13].ToString());
                        somaTotalCargaHoraria = somaTotalCargaHoraria + somaCargaHoraria;
                        cont++;
                    }
                    //Verifica se a carga horária do profissional somada é superior a 66 horas
                    somaTotalCargaHoraria = (somaTotalCargaHoraria + int.Parse(tbxCargaAmbulatorial.Text) + int.Parse(tbxCargaHospitalar.Text) + int.Parse(tbxCargaOutros.Text));

                    if (somaTotalCargaHoraria > 66)
                    {
                        ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Carga Horária Excedida');</script>");
                        return;
                    }
                    else
                    {
                        //Verifico se já existe Vinculo com os dados informados
                        VinculoProfissional listVinculoProfissional = Factory.GetInstance<IVinculo>().BuscarVinculoPorChavePrimaria<VinculoProfissional>(estabelecimentoSaude.CNES, tbxCPF.Text.Replace(".", ""), cbo.Codigo, identifVinculo, rbtCadastramento.SelectedValue);
                        if (listVinculoProfissional != null)
                        {
                            ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Já Existe Vínculo com os Dados Informados');</script>");
                            return;
                        }
                    }
                }
            }

            DataRow row = table.NewRow();
            row[0] = estabelecimentoSaude.CNES;
            row[1] = estabelecimentoSaude.NomeFantasia;
            row[2] = cbo.Codigo;
            row[3] = cbo.Nome;
            string identificacaoVinculo = ddlVinculacao.SelectedValue;
            //A identificação do Vínculo se dá da seguinte Forma:
            //XXYYZZ - XX: Vinculacao, YY: TipoVinculacao e ZZ: SubTipoVInculação
            Vinculacao vinculacao = iViverMaisServiceFacade.BuscarPorCodigo<Vinculacao>(identificacaoVinculo);
            row[4] = vinculacao.Codigo;
            row[5] = vinculacao.Descricao;
            TipoVinculacao tipoVinculacao = iTipoVinculacao.BuscaPorVinculacao<TipoVinculacao>(ddlVinculacao.SelectedValue, ddlTipoVinculacao.SelectedValue);
            row[6] = tipoVinculacao.Codigo;
            row[7] = tipoVinculacao.DescricaoVinculo;
            if (tipoVinculacao.Codigo != "00")
            {
                SubTipoVinculacao subTipoVinculacao = iSubTipoVinculacao.BuscaPorTipoVinculacao<SubTipoVinculacao>(ddlVinculacao.SelectedValue, ddlTipoVinculacao.SelectedValue, ddlSubTipoVinculacao.SelectedValue);
                row[8] = subTipoVinculacao.Codigo;
                row[9] = subTipoVinculacao.DescricaoSubVinculo;
            }
            else
            {
                IList<SubTipoVinculacao> subTipoVinculacao = iSubTipoVinculacao.BuscaPorTipoVinculacao<SubTipoVinculacao>("00");
                row[8] = subTipoVinculacao[0].Codigo;
                row[9] = subTipoVinculacao[0].DescricaoSubVinculo;
            }

            row[10] = rbtCadastramento.SelectedValue;
            row[11] = tbxCargaAmbulatorial.Text;
            row[12] = tbxCargaHospitalar.Text;
            row[13] = tbxCargaOutros.Text.Trim();
            row[14] = tbxRegistroConselho.Text;
            OrgaoEmissor orgaoEmissor = Factory.GetInstance<IOrgaoEmissor>().BuscarPorCodigo<OrgaoEmissor>(ddlOrgaoEmissorRegistroConselho.SelectedValue);
            row[15] = orgaoEmissor.Codigo;
            row[16] = orgaoEmissor.Nome;
            row[17] = true;
            table.Rows.Add(row);
            Session["VinculosProfissional"] = table;
            GridviewVinculosAtivos.DataSource = table;
            GridviewVinculosAtivos.DataBind();
            PanelVinculosAtivos.Visible = true;
        }

        protected void tbxCodIBGE_TextChanged(object sender, EventArgs e)
        {
            IMunicipio iMunicipio = Factory.GetInstance<IMunicipio>();
            Municipio municipio = iMunicipio.BuscarPorCodigo<Municipio>(tbxCodIBGE.Text);
            if (municipio != null)
            {
                tbxMunicipio.Text = municipio.Nome;
                ddlUFProf.SelectedValue = municipio.UF.Sigla;
            }
            else
            {
                tbxMunicipio.Text = "Município Não Localizado.";
                tbxCodIBGE.Text = String.Empty;
                return;
            }
        }

        protected void ddlVinculacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            ITipoVinculacao iTipoVinculacao = Factory.GetInstance<ITipoVinculacao>();
            ddlTipoVinculacao.Items.Clear();
            string id_vinculo = ddlVinculacao.SelectedValue;
            IList<TipoVinculacao> tipoViculacao = iTipoVinculacao.BuscaPorVinculacao<TipoVinculacao>(id_vinculo);
            if (tipoViculacao.Count == 0)
                RequiredFieldValidator30.Enabled = false;
            ddlTipoVinculacao.Items.Add(new ListItem("Selecione...", "0"));
            foreach (TipoVinculacao tv in tipoViculacao)
            {
                ddlTipoVinculacao.Items.Add(new ListItem(tv.DescricaoVinculo, tv.Codigo));
            }
        }

        protected void ddlTipoVinculacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            ISubTipoVinculacao iSubTipoVinculacao = Factory.GetInstance<ISubTipoVinculacao>();
            ddlSubTipoVinculacao.Items.Clear();
            IList<SubTipoVinculacao> subTipoVinculacao = iSubTipoVinculacao.BuscaPorTipoVinculacao<SubTipoVinculacao>(ddlVinculacao.SelectedValue, ddlTipoVinculacao.SelectedValue);
            if (subTipoVinculacao.Count == 0)
                RequiredFieldValidator31.Enabled = false; //Desabilita
            ddlSubTipoVinculacao.Items.Add(new ListItem("Selecione...", "0"));
            foreach (SubTipoVinculacao subTipo in subTipoVinculacao)
            {
                ddlSubTipoVinculacao.Items.Add(new ListItem(subTipo.DescricaoSubVinculo, subTipo.Codigo));
            }
        }

        protected void tbxCBO_TextChanged(object sender, EventArgs e)
        {
            IViverMaisServiceFacade iViverMaisServiceFacade = Factory.GetInstance<IViverMaisServiceFacade>();
            CBO cbo = iViverMaisServiceFacade.BuscarPorCodigo<CBO>(tbxCBO.Text.Trim());
            if (cbo != null)
                tbxNomeCBO.Text = cbo.Nome;
            else
            {
                tbxNomeCBO.Text = "Ocupação não localizada";
                return;
            }
        }

        /// <summary>
        /// Método Responsavel por Editar o Gridview de Vínculos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridviewVinculosAtivos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Pega o Objeto GridViewRow que representa a linha a ser editada 
            //a partir da Coleção de Linhas do Gridview
            GridviewVinculosAtivos.EditIndex = e.NewEditIndex;
            GridviewVinculosAtivos.DataSource = Session["VinculosProfissional"];
            GridviewVinculosAtivos.DataBind();
        }

        /// <summary>
        /// Método Responsavel por Atualizar as informações do Gridview de Vínculos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridviewVinculosAtivos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow linha = GridviewVinculosAtivos.Rows[e.RowIndex];

            //Pega a tabela que está na Sessão Atualizar o Status
            DataTable table = (DataTable)Session["VinculosProfissional"];

            //Se estiver selecionado ou não, ele irá atualizar a tabela que está na Sessão
            bool checkeed = ((CheckBox)linha.FindControl("chkBoxStatus")).Checked;

            //Verifica se estava Vinculado e agora irá desvíncular da Unidade
            if (bool.Parse(table.Rows[e.RowIndex][17].ToString()) == true)
            {
                if (checkeed == false)
                {
                    //Se Ele Irá desvincular o Profissional, Antes ele irá Verificar se ele está vinculado a Alguma Equipe
                    IList<Equipe> equipes = Factory.GetInstance<IEquipe>().BuscarPorProfissional<Equipe>(((ViverMais.Model.Profissional)Session["Profissional"]).CPF);
                    if (equipes.Count != 0) // Se estiver vinculado a Alguma Equipe
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Antes de Desvincular o Profissional, Você Deve excluir ele das Equipes!');", true);
                        return;
                    }
                }
            }

            table.Rows[e.RowIndex][17] = checkeed;
            Session["VinculosProfissional"] = table;
            GridviewVinculosAtivos.EditIndex = -1;
            GridviewVinculosAtivos.DataSource = table;
            GridviewVinculosAtivos.DataBind();
        }

        /// <summary>
        /// Método Responsavel por Cancelar as edições feitas no Gridview de Vínculos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void GridviewVinculosAtivos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridviewVinculosAtivos.EditIndex = -1;
            DataTable table = (DataTable)Session["VinculosProfissional"];
            GridviewVinculosAtivos.DataSource = table;
            GridviewVinculosAtivos.DataBind();
        }

        void CarregaDadosDoProfissional(ViverMais.Model.Profissional profissional)
        {
            //Dados de Identificação
            tbxNomeMae.Text = profissional.NomeMae;
            tbxNomePai.Text = profissional.NomePai;
            tbxNomeProfissional.Text = profissional.Nome;
            tbxRG.Text = profissional.RG;
            tbxDataEmissaoRG.Text = profissional.DataEmissaoRG == DateTime.MinValue ? String.Empty : profissional.DataEmissaoRG.ToString("dd/MM/yyyy");
            tbxDataNascimento.Text = profissional.DataNascimento.ToString("dd/MM/yyyy");
            tbxCPF.Text = profissional.CPF;
            tbxCartaoSUS.Text = String.IsNullOrEmpty(profissional.CartaoSUS) == true ? String.Empty : profissional.CartaoSUS;
            tbxCPF.Enabled = false;
            rbtSexo.SelectedValue = profissional.Sexo.ToString();
            if (profissional.RacaCor != null)
                ddlRacaCor.SelectedValue = profissional.RacaCor.Codigo;
            rbtNacionalidade.SelectedValue = profissional.Nacionalidade;
            tbxTituloEleitor.Text = profissional.TituloEleitor;
            tbxZonaEleitoral.Text = profissional.ZonaEleitoral;
            tbxSecaoEleitoral.Text = profissional.SecaoEleitoral;
            tbxPisPASEP.Text = profissional.PISPASEP;
            tbxCTPS.Text = profissional.NumeroCarteiraTrabalho;
            tbxSerieCTPS.Text = profissional.SerieCarteiraTrabalho;
            //tbxCodigoPacienteViverMais.Text = profissional.CodigoPacienteViverMais;
            if (profissional.UfResidencia != null)
                ddlUFProf.SelectedValue = profissional.UfResidencia.Sigla;
            if (profissional.OrgaoEmissorRG != null)
                ddlOrgaoEmissorRG.SelectedValue = profissional.OrgaoEmissorRG.Codigo;

            rbtCadastramento.SelectedValue = profissional.VinculoSUS.ToString();
            rbtCadastramento_SelectedIndexChanged(new object(), new EventArgs());

            //Dados Residenciais e bancários
            ddlTipoLogradouro.SelectedValue = profissional.TipoLogradouro == null ? "0" : profissional.TipoLogradouro.Codigo;
            ddlStatusProfissional.SelectedValue = profissional.StatusProfissional.ToString();
            tbxLogradouro.Text = profissional.Logradouro;
            tbxNumeroEndereco.Text = profissional.Numero;
            tbxComplemento.Text = profissional.Complemento;
            tbxCEP.Text = profissional.CEP;
            if (profissional.Municipio != null)
            {
                tbxCodIBGE.Text = profissional.Municipio.Codigo;
                tbxMunicipio.Text = profissional.Municipio.Nome;
                ddlUFProf.SelectedValue = profissional.Municipio.UF.Sigla;
            }
            if (profissional.Bairro != null)
                ddlBairro.SelectedValue = profissional.Bairro.Codigo.ToString();
            if (!String.IsNullOrEmpty(profissional.Telefone))
            {
                string telefone = profissional.Telefone.Replace("-", "").Replace("/","").Replace("(", "").Replace(")", "").Replace(" ","");
                tbxTelefone.Text = telefone;
            }
            tbxDataAtualizacao.Text = profissional.DataAtualizacao.ToString("dd/MM/yyyy");
            tbxDataAtualizacao.Enabled = false;
            tbxUltimoUsuario.Text = profissional.UsuarioResponsavelAtualizacao;
            tbxUltimoUsuario.Enabled = false;
            if (profissional.Banco != null)
                ddlBanco.SelectedValue = profissional.Banco.Codigo;
            tbxAgencia.Text = profissional.NumeroAgencia;
            tbxContaCorrente.Text = profissional.ContaCorrente;
        }

        protected void GridViewListaProfissionais_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string id_profissional = Convert.ToString(e.CommandArgument);
            IViverMaisServiceFacade iViverMaisServiceFacade = Factory.GetInstance<IViverMaisServiceFacade>();

            //Quando é selecionado o profissional, Coloco o Wizard para ir para a página 2 mostrando os dados
            //do profissional selecionado.            
            Wizard1.ActiveStepIndex = 1;

            ViverMais.Model.Profissional profissional = iViverMaisServiceFacade.BuscarPorCodigo<ViverMais.Model.Profissional>(id_profissional.ToString());
            Session["Profissional"] = profissional;

            if (!String.IsNullOrEmpty(profissional.CartaoSUS))
            {
                btnBuscarPacienteViverMais_OnClick(new object(), new EventArgs());
            }
            else
            {
                ViverMais.Model.Paciente pacienteViverMais = Factory.GetInstance<IPaciente>().BuscarPacientePorCPF<ViverMais.Model.Paciente>(profissional.CPF);
                if (pacienteViverMais != null)
                {
                    CartaoSUS cartao = CartaoSUSBLL.ListarPorPaciente(pacienteViverMais).First();
                    if (cartao != null)
                    {
                        profissional.CartaoSUS = cartao.Numero;
                        iViverMaisServiceFacade.Atualizar(profissional);
                        btnBuscarPacienteViverMais_OnClick(new object(), new EventArgs());
                    }
                    else
                    {
                        Wizard1.ActiveStepIndex = 0;
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('O Profissional selecionado não possui Cartão Sus. Por favor, Regularize o seu cadastro com o SUS, pois o Profissional deve ser um Paciente SUS!');", true);
                        return;
                    }
                }
                else
                {
                    Wizard1.ActiveStepIndex = 0;
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('O Profissional não é um Paciente Sus ou no seu cadastro, não foi informado o CPF. Por favor, Regularize o seu cadastro com o SUS, pois o Profissional deve ser um Paciente SUS!');", true);
                    return;
                }

                //Wizard1.ActiveStepIndex = 0;
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('O Profissional selecionado não possui Cartão Sus. Por favor, Regularize o seu cadastro com o SUS, pois o Profissional deve ser um Paciente SUS!');", true);
                //return;
            }

            //CarregaDadosDoProfissional(profissional);

            //Vínculos
            IList<VinculoProfissional> vinculos = Factory.GetInstance<IVinculo>().BuscarPorProfissional<VinculoProfissional>(profissional.CPF);
            if (vinculos.Count != 0)
            {
                lblSemVinculos.Visible = false;
                PanelVinculosAtivos.Visible = true;

                DataTable table = CriaTabelaVinculos();
                foreach (VinculoProfissional vp in vinculos)
                {
                    DataRow row = table.NewRow();
                    row[0] = vp.EstabelecimentoSaude.CNES;
                    row[1] = vp.EstabelecimentoSaude.NomeFantasia;
                    row[2] = vp.CBO.Codigo;
                    row[3] = vp.CBO.Nome;
                    string identificacaoVinculo = vp.IdentificacaoVinculo;
                    //A identificação do Vínculo se dá da seguinte Forma:
                    //XXYYZZ - XX: Vinculacao, YY: TipoVinculacao e ZZ: SubTipoVInculação
                    Vinculacao vinc = iViverMaisServiceFacade.BuscarPorCodigo<Vinculacao>(identificacaoVinculo.Substring(0, 2));
                    row[4] = vinc.Codigo;
                    row[5] = vinc.Descricao;
                    TipoVinculacao tipoVinculacao = Factory.GetInstance<ITipoVinculacao>().BuscaPorVinculacao<TipoVinculacao>(identificacaoVinculo.Substring(0, 2), identificacaoVinculo.Substring(2, 2));
                    row[6] = tipoVinculacao.Codigo;
                    row[7] = tipoVinculacao.DescricaoVinculo;
                    SubTipoVinculacao subTipoVinculacao = Factory.GetInstance<ISubTipoVinculacao>().BuscaPorTipoVinculacao<SubTipoVinculacao>(identificacaoVinculo.Substring(0, 2), identificacaoVinculo.Substring(2, 2), identificacaoVinculo.Substring(4));
                    row[8] = subTipoVinculacao.Codigo;
                    row[9] = subTipoVinculacao.DescricaoSubVinculo;
                    row[10] = vp.VinculoSUS;
                    row[11] = vp.CargaHorariaAmbulatorial;
                    row[12] = vp.CargaHorariaHospitalar;
                    row[13] = vp.CargaHorariaOutros;
                    row[14] = vp.RegistroConselho;
                    try
                    {
                        row[15] = vp.OrgaoEmissorRegistroConselho.Codigo;
                        row[16] = vp.OrgaoEmissorRegistroConselho.Nome;
                    }
                    catch (Exception f)
                    {
                        //row[15] = "Conselho Não identificado";
                        row[16] = "Conselho Não identificado";
                    }
                    if (vp.Status == "I")//Se estiver Inativo
                        row[17] = false;
                    else
                        row[17] = true;
                    table.Rows.Add(row);

                }
                Session["VinculosProfissional"] = table;
                DataTable table2 = table;
                GridviewVinculosAtivos.DataSource = table2;
                GridviewVinculosAtivos.DataBind();
            }
            else
            {
                lblSemVinculos.Visible = true;
            }
        }

        protected void rbtNacionalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtNacionalidade.SelectedValue == "2")
            {
                PanelEstrangeiroSelecionado.Visible = true;
                RequiredFieldValidator8.Enabled = true;
                RequiredFieldValidator9.Enabled = true;
                RequiredFieldValidator10.Enabled = true;
            }
            else
            {
                PanelEstrangeiroSelecionado.Visible = false;
                RequiredFieldValidator8.Enabled = false;
                RequiredFieldValidator9.Enabled = false;
                RequiredFieldValidator10.Enabled = false;
            }
        }

        protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            //Salva os dados cadastrais do profissional, Residenciais, Bancários e Vínculos
            ProfissionalEVinculos();
            ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Dados Salvos com Sucesso!');</script>");
            return;
        }

        private void ProfissionalEVinculos()
        {
            IViverMaisServiceFacade iViverMaisServiceFacade = Factory.GetInstance<IViverMaisServiceFacade>();

            ViverMais.Model.Profissional profissional = Factory.GetInstance<IProfissional>().BuscarPorCodigo<ViverMais.Model.Profissional>(tbxCPF.Text.Replace(".", "").Replace("-", ""));
            if (profissional == null)
            {
                profissional = new ViverMais.Model.Profissional();
                profissional.CPF = tbxCPF.Text.Replace(".", "").Replace("-", "");
            }

            ITipoVinculacao iTipoVinculacao = Factory.GetInstance<ITipoVinculacao>();
            ISubTipoVinculacao iSubTipoVinculacao = Factory.GetInstance<ISubTipoVinculacao>();
            IEstabelecimentoSaude iEstabelecimentoSaude = Factory.GetInstance<IEstabelecimentoSaude>();

            profissional.Nome = tbxNomeProfissional.Text;
            profissional.Nacionalidade = rbtNacionalidade.SelectedValue;
            ViverMais.Model.RacaCor racaCor = iViverMaisServiceFacade.BuscarPorCodigo<ViverMais.Model.RacaCor>(ddlRacaCor.SelectedValue);
            profissional.RacaCor = racaCor;
            profissional.RG = tbxRG.Text;
            OrgaoEmissor org = iViverMaisServiceFacade.BuscarPorCodigo<OrgaoEmissor>(ddlOrgaoEmissorRG.SelectedValue);
            if (org != null)
                profissional.OrgaoEmissorRG = org;
            if (tbxDataEmissaoRG.Text != "")
                profissional.DataEmissaoRG = DateTime.Parse(tbxDataEmissaoRG.Text);
            if (tbxDataNascimento.Text != "__/__/____")
                profissional.DataNascimento = DateTime.Parse(tbxDataNascimento.Text);
            profissional.Sexo = char.Parse(rbtSexo.SelectedValue);
            TipoLogradouro tipoLogradouro = iViverMaisServiceFacade.BuscarPorCodigo<TipoLogradouro>(ddlTipoLogradouro.SelectedValue);
            if (tipoLogradouro != null)
                profissional.TipoLogradouro = tipoLogradouro;
            Municipio municipio = iViverMaisServiceFacade.BuscarPorCodigo<Municipio>(tbxCodIBGE.Text.Trim());
            if (municipio != null)
                profissional.Municipio = municipio;

            //UF uf = iViverMaisServiceFacade.BuscarPorCodigo<UF>(ddlUFProf.SelectedValue);
            //if (uf != null)
            //    profissional.UfResidencia = uf;
            profissional.NomeMae = tbxNomeMae.Text;
            profissional.NomePai = tbxNomePai.Text;
            profissional.Telefone = tbxTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace("_", "");
            profissional.TituloEleitor = tbxTituloEleitor.Text;
            profissional.ZonaEleitoral = tbxZonaEleitoral.Text;
            profissional.SecaoEleitoral = tbxSecaoEleitoral.Text;
            //profissional.RegistroConselho = tbxRegistroConselho.Text;
            //OrgaoEmissor orgaoEmissor = iViverMaisServiceFacade.BuscarPorCodigo<OrgaoEmissor>(ddlOrgaoEmissorRegistroConselho.SelectedValue);
            //profissional.OrgaoEmissorRegistroConselho = orgaoEmissor;
            profissional.PISPASEP = tbxPisPASEP.Text;
            profissional.NumeroCarteiraTrabalho = tbxCTPS.Text;
            profissional.SerieCarteiraTrabalho = tbxSerieCTPS.Text;
            profissional.Logradouro = tbxLogradouro.Text;
            profissional.Numero = tbxNumeroEndereco.Text;
            profissional.Complemento = tbxComplemento.Text;
            profissional.CEP = tbxCEP.Text.Replace(".", "").Replace("-", "");
            Bairro bairro = iViverMaisServiceFacade.BuscarPorCodigo<Bairro>(int.Parse(ddlBairro.SelectedValue));
            if (bairro != null)
                profissional.Bairro = bairro;
            profissional.StatusProfissional = char.Parse(ddlStatusProfissional.SelectedValue);
            profissional.DataAtualizacao = DateTime.Now;
            Usuario usuario = (Usuario)Session["Usuario"];
            profissional.UsuarioResponsavelAtualizacao = usuario.Login;
            //profissional.CodigoPacienteViverMais = tbxCodigoPacienteViverMais.Text;
            profissional.CartaoSUS = tbxCartaoSUS.Text.Trim();
            Banco banco = iViverMaisServiceFacade.BuscarPorCodigo<Banco>(ddlBanco.SelectedValue);
            if (banco != null)
                profissional.Banco = banco;
            profissional.NumeroAgencia = tbxAgencia.Text;
            profissional.ContaCorrente = tbxContaCorrente.Text;
            profissional.VinculoSUS = char.Parse(rbtCadastramento.SelectedValue);
            if (Factory.GetInstance<IProfissional>().BuscarPorCodigo<ViverMais.Model.Profissional>(profissional.CPF) != null)
                iViverMaisServiceFacade.Salvar(profissional);
            else
                iViverMaisServiceFacade.Inserir(profissional);

            //Factory.GetInstance<ILogEventos>().Salvar(profissional);
            //Salvar os Vínculos

            DataTable table = (DataTable)Session["VinculosProfissional"];
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    VinculoProfissional vinculoProfissional = Factory.GetInstance<IVinculo>().BuscarVinculoPorChavePrimaria<VinculoProfissional>(row["CodigoEstabelecimento"].ToString(), profissional.CPF, row["CodigoCBO"].ToString(), row["CodigoVinculacao"].ToString() + row["CodigoTipoVinculacao"].ToString() + row["CodigoSubTipoVinculacao"].ToString(), row["SUS"].ToString());

                    if (vinculoProfissional == null)
                    {
                        vinculoProfissional = new VinculoProfissional();
                        vinculoProfissional.EstabelecimentoSaude = iViverMaisServiceFacade.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(row["CodigoEstabelecimento"].ToString());
                        vinculoProfissional.Profissional = profissional;
                        vinculoProfissional.CBO = iViverMaisServiceFacade.BuscarPorCodigo<CBO>(row["CodigoCBO"].ToString());
                        vinculoProfissional.IdentificacaoVinculo = row["CodigoVinculacao"].ToString() + row["CodigoTipoVinculacao"].ToString() + row["CodigoSubTipoVinculacao"].ToString();
                        vinculoProfissional.VinculoSUS = char.Parse(row["SUS"].ToString());
                        vinculoProfissional.RegistroConselho = row["NumeroRegistro"].ToString();
                        OrgaoEmissor orgaoEmissor = iViverMaisServiceFacade.BuscarPorCodigo<OrgaoEmissor>(row["CodigoOrgaoEmissor"].ToString());
                        vinculoProfissional.OrgaoEmissorRegistroConselho = orgaoEmissor;
                        vinculoProfissional.CargaHorariaAmbulatorial = int.Parse(row["CargaHorariaAmbulatorial"].ToString());
                        vinculoProfissional.CargaHorariaHospitalar = int.Parse(row["CargaHorariaHospitalar"].ToString());
                        vinculoProfissional.CargaHorariaOutros = int.Parse(row["CargaHorariaOutros"].ToString());
                        vinculoProfissional.DataAtualizacao = DateTime.Now;
                        vinculoProfissional.UsuarioResponsavelAtualizacao = usuario.Nome;
                        vinculoProfissional.RegistroConselho = tbxRegistroConselho.Text.Trim();
                        vinculoProfissional.OrgaoEmissorRegistroConselho = iViverMaisServiceFacade.BuscarPorCodigo<OrgaoEmissor>(ddlOrgaoEmissorRegistroConselho.SelectedValue);
                        vinculoProfissional.Status = ((bool)(row["Status"])) == true ? "A" : "I";
                        iViverMaisServiceFacade.Salvar(vinculoProfissional);
                    }
                    else
                    {
                        vinculoProfissional.Status = ((bool)(row["Status"])) == true ? "A" : "I";
                        Factory.GetInstance<IVinculo>().AtualizaVinculo(vinculoProfissional);
                    }
                    //Factory.GetInstance<ILogEventos>().Salvar(vinculoProfissional);
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Vincule o Profissional a um Estabelecimento ao menos!');</script>");
                return;
            }
        }

        protected void btnNao_Click(object sender, EventArgs e)
        {
            panelConfirmacao.Visible = false;
            Wizard1.ActiveStepIndex = 0;
        }

        protected void btnSim_Click(object sender, EventArgs e)
        {
            tbxCPF.Text = tbxCPFPesquisa.Text;
            tbxCPF.Enabled = false;

            string numeroCPF = tbxCPF.Text.Replace(".", "").Replace("-", "");

            ViverMais.Model.Paciente paciente = PacienteBLL.PesquisarPorCPF(numeroCPF);
            if (paciente != null)
            {
                CarregaDadosDoProfissionalComoPacienteViverMais(paciente);
                Wizard1.ActiveStepIndex = 1;
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Não foi encontrado nenhum Paciente com o CPF informado. Por favor, verifique se o cadastro do Profissional, como Paciente, está com a documentação completa.');", true);
                Wizard1.ActiveStepIndex = 0;
                panelConfirmacao.Visible = false;
                return;
            }
        }

        protected void Wizard1_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (Wizard1.ActiveStepIndex == 1)
            {
                panelConfirmacao.Visible = false;
                tbxCPFPesquisa.Text = "";
            }
        }

        protected void rbtCadastramento_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Campos Obrigatórios para Profissional Sus 
            if (rbtCadastramento.SelectedValue.Equals("S"))
            {
                RequiredFieldValidator1.Enabled = true;//Nome do Profissional
                RequiredFieldValidator2.Enabled = true;//CPF
                RequiredFieldValidator3.Enabled = true;//Sexo
                RequiredFieldValidator4.Enabled = true;//Nome da Mãe
                RequiredFieldValidator6.Enabled = true;//Data de Nascimento
                RequiredFieldValidator7.Enabled = true;//RG
                RequiredFieldValidator5.Enabled = true;//UF do RG
                RequiredFieldValidator14.Enabled = true;//Data Emissão RG
                RequiredFieldValidator11.Enabled = true;//Codigo IBGE
                RequiredFieldValidator12.Enabled = true;//Municipo de Residencia
                RequiredFieldValidator13.Enabled = true;//Estado de Residência
                RequiredFieldValidator15.Enabled = true;//Nacionalidade
                RequiredFieldValidator17.Enabled = true;//Logradouro
                RequiredFieldValidator18.Enabled = true;//Número Residencial
                RequiredFieldValidator19.Enabled = true;//CEP
                RequiredFieldValidator20.Enabled = true;//Bairro
                RequiredFieldValidator21.Enabled = true;//Telefone
                RequiredFieldValidator22.Enabled = true;//Banco
                RequiredFieldValidator23.Enabled = true;//Agência Bancária
                RequiredFieldValidator24.Enabled = true;//Conta Corrente
                //RequiredFieldValidator25.Enabled = true;//Registro Conselho
                //RequiredFieldValidator26.Enabled = true;//Orgao Emissor Conselho
            }
            else //Campos Obrigatórios para Profissional Não Sus 
            {
                RequiredFieldValidator1.Enabled = true;//Nome do Profissional
                RequiredFieldValidator2.Enabled = true;//CPF
                RequiredFieldValidator11.Enabled = true;//Codigo IBGE
                RequiredFieldValidator12.Enabled = true;//Municipo de Residencia
                RequiredFieldValidator13.Enabled = true;//Estado de Residência
                RequiredFieldValidator3.Enabled = false;//Sexo
                RequiredFieldValidator4.Enabled = false;//Nome da Mãe
                RequiredFieldValidator6.Enabled = false;//Data de Nascimento
                RequiredFieldValidator7.Enabled = false;//RG
                RequiredFieldValidator5.Enabled = false;//UF do RG
                RequiredFieldValidator11.Enabled = true;//Codigo Ibge
                RequiredFieldValidator13.Enabled = true;//Estado de Residência
                RequiredFieldValidator14.Enabled = false;//Data Emissão RG
                RequiredFieldValidator15.Enabled = false;//Nacionalidade
                RequiredFieldValidator17.Enabled = false;//Logradouro
                RequiredFieldValidator18.Enabled = false;//Número Residencial
                RequiredFieldValidator19.Enabled = false;//CEP
                RequiredFieldValidator20.Enabled = false;//Bairro
                RequiredFieldValidator21.Enabled = false;//Telefone
                RequiredFieldValidator22.Enabled = false;//Banco
                RequiredFieldValidator23.Enabled = false;//Agência Bancária
                RequiredFieldValidator24.Enabled = false;//Conta Corrente
            }
        }
    }
}
