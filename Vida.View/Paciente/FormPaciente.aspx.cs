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
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades;
using System.Threading;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Misc;
using ViverMais.View.Agendamento.Helpers;
using System.Text.RegularExpressions;
using System.IO;
using iTextSharp.text.pdf;
using ViverMais.BLL;
using ViverMais.Model.Entities.ViverMais;

namespace ViverMais.View.Paciente
{
    public partial class FormPaciente : System.Web.UI.Page
    {
        //Comentado
        private bool CPFValido(string numero)
        {
            if (numero.Length != 11)
                return false;
            string expressao = "^0+$|^1+$|^2+$|^3+$|^4+$|^5+$|^6+$|^7+$|^8+$|^9+$";

            if (Regex.IsMatch(numero, expressao))
                return false;

            int numero1 = 0;
            int numero2 = 0;
            int multiplicador = 11;

            foreach (char n in numero.Substring(0, 9))
            {
                numero2 += int.Parse(n.ToString()) * multiplicador--;
                numero1 += int.Parse(n.ToString()) * multiplicador;
            }

            int digito1 = numero1 % 11;
            if (digito1 < 2)
                digito1 = 0;
            else
                digito1 = 11 - digito1;

            numero2 += digito1 * 2;

            int digito2 = numero2 % 11;
            if (digito2 < 2)
                digito2 = 0;
            else
                digito2 = 11 - digito2;

            if (digito1 != int.Parse(numero[9].ToString()))
                return false;
            if (digito2 != int.Parse(numero[10].ToString()))
                return false;
            return true;
        }

        private bool DataMaiorQueAtual(DateTime data)
        {
            return DateTime.Today.CompareTo(data) == 1;
        }

        /// <summary>
        /// Carrega os dados iniciais da pagina
        /// </summary>
        /// <param name="iViverMais">Service Facade do ViverMais</param>
        private void CarregarDadosIniciais()
        {
            //Preenche drop Motivo
            ddlMotivo.DataSource = MotivoCadastroBLL.ListarTodos();// iViverMais.ListarTodos<MotivoCadastro>();
            ddlMotivo.DataBind();
            ddlMotivo.Items.Insert(0, new ListItem("Selecione", "-1"));
            ddlMotivo.SelectedValue = "99";//Código da opção 'Outros'

            //Preenche drop RacaCor
            ddlRacaCor.DataSource = RacaCorBLL.ListarTodos(); //iViverMais.ListarTodos<ViverMais.Model.RacaCor>();
            ddlRacaCor.DataBind();
            ddlRacaCor.Items.Insert(0, new ListItem("Selecione", "-1"));
            ddlRacaCor.SelectedIndex = 0;

            ddlEtnia.DataSource = EtniaBLL.ListarTodos();
            ddlEtnia.DataBind();
            ddlEtnia.Items.Insert(0, new ListItem("Selecione...", "-1"));
            ddlEtnia.SelectedIndex = 0;

            //Preenche drops UF
            List<ViverMais.Model.UF> ufs = UFBLL.ListarTodos().OrderBy(x => x.Sigla).ToList(); //iViverMais.ListarTodos<ViverMais.Model.UF>("Sigla", true);
            ddlUFIdentidade.DataSource = ufs;
            ddlUFCTPS.DataSource = ufs;
            ddlUFNascimento.DataSource = ufs;
            ddlUFIdentidade.DataBind();
            ddlUFCTPS.DataBind();
            ddlUFNascimento.DataBind();
            ddlUFIdentidade.Items.Insert(0, new ListItem("Selecione...", "-1"));
            ddlUFCTPS.Items.Insert(0, new ListItem("Selecione...", "-1"));
            ddlUFNascimento.Items.Insert(0, new ListItem("Selecione...", "-1"));
            ddlUFIdentidade.SelectedIndex = 0;
            ddlUFCTPS.SelectedIndex = 0;
            ddlUFNascimento.SelectedValue = "BA";

            //Preenche municipios                
            IList<ViverMais.Model.Municipio> municipios = MunicipioBLL.PesquisarPorEstado("BA"); //Factory.GetInstance<IMunicipio>().ListarPorEstado<ViverMais.Model.Municipio>("BA").OrderBy(a => a.Nome).ToList();
            ddlMunicipios.DataSource = municipios;
            ddlMunicipioNascimento.DataSource = municipios;
            ddlMunicipios.DataBind();
            ddlMunicipioNascimento.DataBind();
            //foreach (ListItem item in ddlMunicipios.Items)
            //{
            //    item.Text = item.Text.Split('-')[0].Trim();
            //}
            //foreach (ListItem item in ddlMunicipioNascimento.Items)
            //{
            //    item.Text = item.Text.Split('-')[0].Trim();
            //}
            ddlMunicipios.Items.Insert(0, new ListItem("Selecione...", "-1"));
            ddlMunicipioNascimento.Items.Insert(0, new ListItem("Selecione...", "-1"));


            ddlMunicipios.SelectedValue = ((Usuario)Session["usuario"]).Unidade.MunicipioGestor.Codigo; //"292740";//Código de Salvador
            if (((Usuario)Session["usuario"]).Unidade.MunicipioGestor.Codigo != "292740")
            {
                ddlMunicipios.Enabled = false;
                ddlMunicipios.Attributes.Add("readonly", "readonly");
                ddlMunicipioNascimento.SelectedValue = "292740";//Código de Salvador
            }
            else
            {
                ddlMunicipioNascimento.SelectedValue = "292740";//Código de Salvador                
            }

            //Preenche drop bairros            
            List<Bairro> bairros = BairroBLL.PesquisarPorMunicipio(((Usuario)Session["usuario"]).Unidade.MunicipioGestor); //Factory.GetInstance<IBairro>().ListarPorCidade<ViverMais.Model.Bairro>(((Usuario)Session["usuario"]).Unidade.MunicipioGestor.Codigo);
            ddlBairro.DataSource = bairros.OrderBy(x => x.Nome).ToList();
            ddlBairro.DataBind();
            ddlBairro.Items.Insert(0, new ListItem("Selecione...", "-1"));
            ddlBairro.SelectedIndex = 0;

            //Preenche drop tipo logradouro
            ddlTipoLogradouro.DataSource = TipoLogradouroBLL.ListarTodos(); //Factory.GetInstance<IEndereco>().ListarTodos<ViverMais.Model.TipoLogradouro>();
            ddlTipoLogradouro.DataBind();
            ddlTipoLogradouro.Items.Insert(0, new ListItem("Selecione...", "-1"));

            //Preenche drop orgão expedidor
            ddlOrgaoExpedidor.DataSource = OrgaoEmissorBLL.ListarTodos(); //iViverMais.ListarTodos<ViverMais.Model.OrgaoEmissor>();
            ddlOrgaoExpedidor.DataBind();
            ddlOrgaoExpedidor.Items.Insert(0, new ListItem("Selecione...", "-1"));


            //Preenche drop pais de origem
            ddlPaisOrigem.DataSource = PaisBLL.ListarTodos();// iViverMais.ListarTodos<ViverMais.Model.Pais>().OrderBy(p => p.Nome).ToList();
            ddlPaisOrigem.DataBind();
            ddlPaisOrigem.Items.Insert(0, new ListItem("Selecione...", "-1"));
            //ddlPaisOrigem.Items.Remove(ddlPaisOrigem.Items.FindByValue("010")); 

            this.rbDeficiencia.Items.Add(new ListItem("Sim", "S"));
            this.rbDeficiencia.Items.Add(new ListItem("Não", "N"));
            this.rbOrtese.Items.Add(new ListItem("Sim", "S"));
            this.rbOrtese.Items.Add(new ListItem("Não", "N"));

            this.chckTipoDeficiencia.DataSource = DeficienciaBLL.ListarTodos().OrderBy(p => p.Nome).ToList();
            this.chckTipoDeficiencia.DataBind();

            this.chckOrigemDeficiencia.DataSource = OrigemDeficienciaBLL.ListarTodos().OrderBy(p => p.Nome).ToList();
            this.chckOrigemDeficiencia.DataBind();

            this.chckProtese.DataSource = ProteseDeficienciaBLL.ListarTodosOrdenados();
            this.chckProtese.DataBind();

            this.chckComunicacaoDeficiencia.DataSource = ComunicacaoDeficienciaBLL.ListarTodosOrdenados();
            this.chckComunicacaoDeficiencia.DataBind();

            this.chckLocomocaoDeficiencia.DataSource = LocomocaoDeficienciaBLL.ListarTodosOrdenados();
            this.chckLocomocaoDeficiencia.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Session.Clear();
            if (Session["Usuario"] == null)
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('-A sessão expirou por algum motivo.\n-Favor logar novamente.');window.location='../Home.aspx';</script>");
                return;
            }

            if (!IsPostBack)
            {
                Session.Remove("paciente");
                Session.Remove("endereco");

                //Verificar possibilidade de carregar no login todas as permissões do usuário
                //evitando assim varias chamadas ao banco.
                ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
                if ((!iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "CADASTRAR_CARTAO_SUS", Modulo.CARTAO_SUS)
                    && Request.QueryString["codigo"] == null)
                    || (!iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "ALTERAR_CARTAO_SUS", Modulo.CARTAO_SUS)
                    && Request.QueryString["codigo"] != null))
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Você não tem permissão para acessar essa página. Em caso de dúViverMais, entre em contato.');window.location='../Home.aspx';</script>");
                    return;
                }

                IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();

                CarregarDadosIniciais();

                //Caso seja edição
                if (Request.QueryString["codigo"] != null)
                {
                    IPaciente ipaciente = Factory.GetInstance<IPaciente>();
                    string codigoPaciente = Request.QueryString["codigo"];
                    ViverMais.Model.Paciente paciente = PacienteBLL.PesquisarCompleto(codigoPaciente); //ipaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(Request.QueryString["codigo"]);
                    //Adiciona o paciente à seção para o caso de edição

                    //ViewState["codigoPaciente"] = paciente.Codigo;
                    List<ViverMais.Model.CartaoSUS> cartoes = CartaoSUSBLL.ListarPorPaciente(paciente).OrderBy(x => x.Numero).ToList(); //ipaciente.ListarCartoesSUS<ViverMais.Model.CartaoSUS>(paciente.Codigo).OrderBy(x => x.Numero).ToList();
                    for (int i = 0; i < cartoes.Count; i++)
                    {
                        if (i == 0)
                            lblCNS.Text += cartoes[i].Numero;
                        else
                            lblCNS.Text += " / " + cartoes[i].Numero;
                    }

                    //Desabilitar Campos (Nome do Paciente, Nome da mãe, Data de Nascimento)
                    if (!iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "ALTERAR_INFORMACOES_BASICAS_PACIENTE", Modulo.CARTAO_SUS))
                    {
                        tbxNomePaciente.Enabled = false;
                        tbxNomeMae.Enabled = false;
                        tbxDataNascimento.Enabled = false;
                    }

                    List<ControleCartaoSUS> controleCartao = ControleCartaoSUSBLL.ListarPorCartao(cartoes.First()); //ipaciente.ListarControleCartaoSUS<ControleCartaoSUS>(cartoes.First().Numero);
                    if (controleCartao.Count > 0)
                        lblViaCartao.Text = (controleCartao.Count) + "ª";

                    //Biometria não vou tirar
                    ViverMais.Model.Biometria biometria = iViverMais.BuscarPorCodigo<ViverMais.Model.Biometria>(paciente.Codigo);
                    if (biometria != null)
                        lblBiometria.Text = "Cadastrada";

                    //Motivo cadastro não vou tirar
                    //ViverMais.Model.MotivoCadastroPaciente mcp = ipaciente.BuscarMotivo<MotivoCadastroPaciente>(paciente.Codigo, ((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.CNES);
                    ViverMais.Model.MotivoCadastroPaciente mcp = MotivocadastroPacienteBLL.PesquisarPorPaciente(paciente, ((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.CNES);

                    if (mcp != null && mcp.Motivo != null)
                        ddlMotivo.SelectedValue = mcp.Motivo.Codigo.ToString();

                    tbxNomePaciente.Text = paciente.Nome;
                    tbxNomeMae.Text = paciente.NomeMae;
                    if (tbxNomeMae.Text.Trim() == "IGNORADA")
                        chkMaeIgnorada.Checked = true;
                    tbxDataNascimento.Text = paciente.DataNascimento.ToString("dd/MM/yyyy");
                    tbxNomePai.Text = paciente.NomePai;
                    if (tbxNomePai.Text.Trim() == "IGNORADO")
                        chkPaiIgnorado.Checked = true;
                    rbtnListSexo.SelectedValue = paciente.Sexo.ToString();
                    try
                    {
                        ddlRacaCor.SelectedValue = paciente.RacaCor.Codigo;
                        if (paciente.RacaCor.Codigo == "5")
                        {
                            ddlEtnia.SelectedValue = paciente.Etnia.Codigo;
                            ViewEtnia.Visible = true;
                        }
                    }
                    catch (NullReferenceException)
                    {
                        RequiredFieldRacaCor.Validate();
                    }

                    rbtnListFreqEscola.SelectedValue = paciente.FrequentaEscola.ToString();
                    tbxEmail.Text = paciente.Email;
                    if (paciente.Pais != null)
                        ddlPaisOrigem.SelectedValue = paciente.Pais.Codigo;

                    if (paciente.MunicipioNascimento != null)
                    {
                        try
                        {
                            //if (ddlUFNascimento.SelectedValue != paciente.MunicipioNascimento.UF.Sigla)
                            //{
                            ddlUFNascimento.SelectedValue = paciente.MunicipioNascimento.UF.Sigla;
                            ddlMunicipioNascimento.Items.Clear();
                            ddlMunicipioNascimento.SelectedValue = paciente.MunicipioNascimento.Codigo;
                            ddlMunicipioNascimento.DataSource = MunicipioBLL.PesquisarPorEstado(paciente.MunicipioNascimento.UF.Sigla).OrderBy(x => x.Nome); //Factory.GetInstance<IMunicipio>().ListarPorEstado<ViverMais.Model.Municipio>(paciente.MunicipioNascimento.UF.Sigla);
                            ddlMunicipioNascimento.DataBind();
                            //foreach (ListItem item in ddlMunicipioNascimento.Items)
                            //    item.Text = item.Text.Split('-')[0].Trim();
                            ddlMunicipioNascimento.Items.Insert(0, new ListItem("Selecione...", "-1"));
                            ddlMunicipioNascimento.SelectedValue = paciente.MunicipioNascimento.Codigo;
                            //}
                        }
                        catch (NullReferenceException)
                        {
                            ddlMunicipioNascimento.Items.Clear();
                            ddlMunicipioNascimento.Items.Add(new ListItem("Selecione...", "-1"));
                            CustomValidatorMunicipioNasc.Validate();
                            CustomValidatorUFNasc.Validate();
                        }
                    }

                    cbxVivo.Checked = paciente.Vivo == '1';

                    ViverMais.Model.Endereco endereco = EnderecoBLL.PesquisarPorPaciente(paciente); // Factory.GetInstance<IEndereco>().BuscarPorPaciente<ViverMais.Model.Endereco>(paciente.Codigo);    
                    if (endereco != null)
                    {
                        //ViewState["codigoEndereco"] =  endereco.Codigo;
                        try
                        {
                            MunicipioBLL.CompletarMunicipio(endereco.Municipio);
                        }
                        catch (NullReferenceException)
                        {
                            RequiredFieldValidator5.Validate();
                        }

                        tbxNomeLogradouro.Text = endereco.Logradouro;
                        tbxNumero.Text = endereco.Numero;
                        tbxComplemento.Text = endereco.Complemento;
                        ddlBairro.ClearSelection();

                        if (((Usuario)Session["usuario"]).Unidade.MunicipioGestor.Codigo != "292740" && endereco.Municipio != null &&
                            endereco.Municipio.Codigo != ((Usuario)Session["usuario"]).Unidade.MunicipioGestor.Codigo)
                        {
                            lblBairro.Text = endereco.Bairro;
                            lblMunicipioResidencia.Text = endereco.Municipio.NomeSemUF;
                            ddlMunicipios.SelectedIndex = ddlMunicipios.Items.IndexOf(ddlMunicipios.Items.FindByValue(((Usuario)Session["usuario"]).Unidade.MunicipioGestor.Codigo));
                            btnSalvar.Attributes.Add("onClick", "return confirm('Deseja alterar o município de residência do paciente?');");
                        }
                        else
                        {
                            try
                            {
                                ddlMunicipios.SelectedValue = endereco.Municipio.Codigo;
                                ddlBairro.Items.Clear();
                                ddlBairro.DataSource = BairroBLL.PesquisarPorMunicipio(endereco.Municipio).OrderBy(x => x.Nome).ToList();
                                ddlBairro.DataBind();
                                ddlBairro.Items.Insert(0, new ListItem("Selecione...", "-1"));
                                ddlBairro.SelectedIndex = ddlBairro.Items.IndexOf(ddlBairro.Items.FindByText(endereco.Bairro));
                            }
                            catch (NullReferenceException)
                            {
                                RequiredFieldValidator5.Validate();
                            }
                        }

                        tbxCEP.Text = endereco.CEP;
                        tbxDDD.Text = endereco.DDD;
                        tbxTelefone.Text = endereco.Telefone;
                        try
                        {
                            ddlTipoLogradouro.SelectedValue = endereco.TipoLogradouro.Codigo;
                        }
                        catch (NullReferenceException)
                        {
                            RequiredFieldValidatorTipoLogradouro.Validate();
                        }
                    }

                    List<ViverMais.Model.Documento> documentos = DocumentoBLL.PesqusiarPorPaciente(paciente);
                    //DocumentoBLL.PesqusiarPorPaciente(paciente).Where(t => t.ControleDocumento != null).ToList<Documento>(); 
                    //ipaciente.ListarDocumentos<ViverMais.Model.Documento>(paciente.Codigo);

                    if (documentos.Count > 0)
                    {
                        foreach (ViverMais.Model.Documento documento in documentos)
                        {
                            if (documento.ControleDocumento != null && documento.ControleDocumento.TipoDocumento != null)
                            {
                                switch (documento.ControleDocumento.TipoDocumento.Codigo)
                                {
                                    case "02":
                                        tbxCPF.Text = documento.Numero;
                                        break;
                                    case "03":
                                        tbxTituloEleitor.Text = documento.Numero;
                                        tbxSecaoEleitoral.Text = documento.SecaoEleitoral;
                                        tbxZonaEleitoral.Text = documento.ZonaEleitoral;
                                        break;
                                    case "04":
                                        tbxCTPS.Text = documento.Numero;
                                        tbxSerieCTPS.Text = documento.Serie;
                                        tbxDataEmissaoCTPS.Text = documento.DataEmissao.Value.ToString("dd/MM/yyyy");
                                        ddlUFCTPS.SelectedValue = documento.UF == null ? ddlUFCTPS.SelectedValue = "-1" : documento.UF.Sigla;
                                        break;
                                    case "05":
                                        if (documento.DataChegadaBrasil.HasValue)
                                        {
                                            MultiView1.SetActiveView(ViewEstrangeiro);
                                            rbtnListNacionalidade.SelectedValue = "E";//Estrangeiro
                                            tbxDataEntradaBrasil.Text = documento.DataChegadaBrasil.Value.ToString("dd/MM/yyyy");
                                        }
                                        else if (documento.DataNaturalizacao.HasValue)
                                        {
                                            MultiView1.SetActiveView(ViewNaturalizado);
                                            rbtnListNacionalidade.SelectedValue = "N";//Naturalizado
                                            tbxDataNaturalizacao.Text = documento.DataNaturalizacao.Value.ToString("dd/MM/yyyy");
                                            tbxNaturalizacaoPortaria.Text = documento.NumeroPortaria;
                                        }
                                        break;
                                    case "10":
                                        tbxIdentidade.Text = documento.Numero;
                                        tbxComplementoIdentidade.Text = documento.Complemento;
                                        tbxDataEmissaoIdentidade.Text = documento.DataEmissao.Value.ToString("dd/MM/yyyy");
                                        try
                                        {

                                            ddlOrgaoExpedidor.SelectedValue = documento.OrgaoEmissor.Codigo;
                                        }
                                        catch (NullReferenceException)
                                        {
                                            CustomValidatorOrgaoEmissor.Validate();
                                        }
                                        try
                                        {
                                            ddlUFIdentidade.SelectedValue = documento.UF.Sigla;
                                        }
                                        catch (NullReferenceException)
                                        {
                                            CustomValidatorUFIdentidade.Validate();
                                        }
                                        break;
                                    case "91":
                                    case "92":
                                    case "93":
                                    case "95":
                                        ddlTipoCertidao.SelectedValue = documento.ControleDocumento.TipoDocumento.Codigo;
                                        tbxNomeCartorio.Text = documento.NomeCartorio;
                                        tbxLivro.Text = documento.NumeroLivro;
                                        tbxFolhas.Text = documento.NumeroFolha;
                                        tbxTermo.Text = documento.NumeroTermo;
                                        tbxDataEmissaoCertidao.Text = documento.DataEmissao.Value.ToString("dd/MM/yyyy");
                                        if (documento.Numero != null)
                                        {
                                            tbxNovaCertidao.Text = documento.Numero + documento.NumeroLivro + documento.NumeroFolha + documento.NumeroTermo + documento.Complemento;
                                        }
                                        break;
                                }
                            }
                        }
                    }


                    HiddenCodigoPaciente.Value = paciente.Codigo;
                    HiddenNomePaciente.Value = paciente.Nome;
                    HiddenDataNascimento.Value = paciente.DataNascimento.ToString("dd/MM/yyyy");

                    try
                    {
                        HiddenMunicipio.Value = endereco.Municipio.Nome;
                    }
                    catch (NullReferenceException)
                    {
                        HiddenMunicipio.Value = "";
                    }

                    string cartoesSelecionados = lblCNS.Text;
                    string[] arrayCartoes = cartoesSelecionados.Split('/');
                    long result = (from c in arrayCartoes select long.Parse(c)).Min();
                    HiddenNumeroCartao.Value = result.ToString();

                    //Editando a deficiência se houver
                    DeficienciaPaciente deficiencia = paciente.Deficiencia;

                    if (deficiencia != null)
                    {
                        this.rbDeficiencia.SelectedValue = deficiencia.Deficiente ? "S" : "N";
                        this.OnSelectedIndexChanged_Deficiencia(new object(), new EventArgs());

                        foreach (Deficiencia def in deficiencia.Deficiencias)
                        {
                            this.chckTipoDeficiencia.Items.FindByValue(def.Codigo.ToString()).Selected = true;
                            this.OnSelectedIndexChanged_TipoDeficiencia(new object(), new EventArgs());
                        }

                        foreach (OrigemDeficiencia origem in deficiencia.Origens)
                            this.chckOrigemDeficiencia.Items.FindByValue(origem.Codigo.ToString()).Selected = true;

                        foreach (LocomocaoDeficiencia locomocao in deficiencia.Locomocoes)
                        {
                            this.chckLocomocaoDeficiencia.Items.FindByValue(locomocao.Codigo.ToString()).Selected = true;
                            this.OnSelectedIndexChanged_Locomocao(new object(), new EventArgs());
                        }

                        foreach (ComunicacaoDeficiencia comunicacao in deficiencia.Comunicacoes)
                        {
                            this.chckComunicacaoDeficiencia.Items.FindByValue(comunicacao.Codigo.ToString()).Selected = true;
                            this.OnSelectedIndexChanged_Comunicacao(new object(), new EventArgs());
                        }

                        foreach (ProteseDeficiencia protese in deficiencia.Proteses)
                        {
                            this.chckProtese.Items.FindByValue(protese.Codigo.ToString()).Selected = true;
                            this.OnSelectedIndexChanged_Protese(new object(), new EventArgs());
                        }

                        this.rbOrtese.SelectedValue = deficiencia.UsaOrtese ? "S" : "N";
                    }
                    rbtnListNacionalidade_SelectedIndexChanged(new object(), new EventArgs());
                }
                else
                {
                    btnBiometria.Enabled = false;
                    //rbtnListNacionalidade.SelectedValue = "B";
                }

                //this.btnSalvar.Focus();
            }
        }

        private void PreencherObjetoEndereco(Endereco endereco)
        {
            endereco.Logradouro = tbxNomeLogradouro.Text.ToUpper();
            endereco.Numero = tbxNumero.Text.ToUpper().Replace(" ", "");
            endereco.Complemento = tbxComplemento.Text.ToUpper();
            endereco.Bairro = ddlBairro.SelectedItem.Text;
            endereco.CEP = tbxCEP.Text;
            endereco.DDD = tbxDDD.Text;
            endereco.Telefone = tbxTelefone.Text;
            endereco.TipoLogradouro = new TipoLogradouro();
            endereco.TipoLogradouro.Codigo = ddlTipoLogradouro.SelectedValue;
            endereco.Municipio = new Municipio();
            endereco.Municipio.Codigo = ddlMunicipios.SelectedValue;
            MunicipioBLL.CompletarMunicipio(endereco.Municipio);
        }

        protected void btnSalvar_Click(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid)
                return;

            //Validações preliminares
            DateTime nascimento;

            if (ddlRacaCor.SelectedValue == RacaCor.INDIGENA)
            {
                if (ddlEtnia.SelectedValue == "-1")
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Favor informar a etnia.');</script>");
                    return;
                }
            }

            try
            {
                nascimento = DateTime.Parse(tbxDataNascimento.Text);
            }
            catch (InvalidCastException)
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Data de nascimento inválida.');</script>");
                return;
            }

            if ((tbxTelefone.Text == "00000000") || (tbxTelefone.Text == "11111111") || (tbxTelefone.Text == "22222222") || (tbxTelefone.Text == "33333333") || (tbxTelefone.Text == "44444444")
                || (tbxTelefone.Text == "55555555") || (tbxTelefone.Text == "66666666") || (tbxTelefone.Text == "77777777") || (tbxTelefone.Text == "88888888") || (tbxTelefone.Text == "99999999")
                || (tbxTelefone.Text == "12345678"))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Telefone Inválido.');", true);
                return;
            }


            if (new DateTime(DateTime.Today.Subtract(nascimento).Ticks).Year - 1 > 130)
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('A idade do paciente não pode ser maior que 130 anos.');</script>");
                return;
            }
            if (new DateTime(DateTime.Today.Subtract(nascimento).Ticks).Year - 1 > 17 && tbxCPF.Text.Trim() == "")
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Paciente maior que 16 anos, favor preencher com um CPF válido.');</script>");
                return;
            }

            if (tbxDataNaturalizacao.Text.Trim() != string.Empty && nascimento.CompareTo(DateTime.Parse(tbxDataNaturalizacao.Text)) > 0)
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('A data de naturalização não pode ser anterior a data de nascimento.');</script>");
                return;
            }

            if (tbxDataEntradaBrasil.Text.Trim() != string.Empty && nascimento.CompareTo(DateTime.Parse(tbxDataEntradaBrasil.Text)) > 0)
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('A data de entrada no Brasil não pode ser anterior a data de nascimento.');</script>");
                return;
            }

            if (tbxCPF.Text.Trim() != "" && !CPFValido(tbxCPF.Text.ToString()))
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('CPF inválido.');</script>");
                return;
            }

            if (tbxDDD.Text.Trim() != "" && tbxTelefone.Text.Trim() == "")
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Favor preencher o campo telefone.');</script>");
                return;
            }

            if (tbxTituloEleitor.Text != "" && tbxSecaoEleitoral.Text.Trim() == "" && tbxZonaEleitoral.Text.Trim() == "")
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Favor preencher todos os campos referentes ao titulo de eleitor.');</script>");
                return;
            }

            if (tbxDataEmissaoCertidao.Text.Trim() != "")
            {
                if (nascimento.CompareTo(DateTime.Parse(tbxDataEmissaoCertidao.Text)) > 0)
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('A data de emissão da certidão não pode ser anterior a data de nascimento.');</script>");
                    return;
                }
            }

            if (tbxDataEmissaoCTPS.Text.Trim() != "")
            {
                if (nascimento.CompareTo(DateTime.Parse(tbxDataEmissaoCTPS.Text)) > 0)
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('A data de emissão da CTPS não pode ser anterior à data de nascimento.');</script>");
                    return;
                }
            }

            if (tbxDataEmissaoIdentidade.Text.Trim() != "")
            {
                if (nascimento.CompareTo(DateTime.Parse(tbxDataEmissaoIdentidade.Text)) > 0)
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('A data de emissão do RG não pode ser anterior à data de nascimento.');</script>");
                    return;
                }
            }

            if (tbxNomeCartorio.Text.Trim() != "")
            {
                Regex reg = new Regex(@"^([a-z]|\s)*$", RegexOptions.IgnoreCase);

                if (!reg.IsMatch(tbxNomeCartorio.Text))
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Nome do cartório deve contar apenas letras.');</script>");
                    return;
                }
            }
            //====================================================================


            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            IPaciente ipaciente = Factory.GetInstance<IPaciente>();
            //ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(codigoPaciente);
            ViverMais.Model.Paciente paciente = new ViverMais.Model.Paciente();
            ControlePaciente controlePaciente = null;

            //Edição de paciente
            if (Request.QueryString["codigo"] != null)
            //if (ViewState["codigoPaciente"] != null)
            {
                //string codigoPaciente = (string)ViewState["codigoPaciente"];
                string codigoPaciente = Request.QueryString["codigo"];
                paciente = PacienteBLL.PesquisarCompleto(codigoPaciente);

                controlePaciente = ControlePacienteBLL.PesquisarPorPaciente(paciente); //Factory.GetInstance<IPaciente>().BuscaControlePaciente<ControlePaciente>(paciente.Codigo);
                if (controlePaciente == null)
                    controlePaciente = new ControlePaciente();
                controlePaciente.Controle = 'A';
                controlePaciente.DataOperacao = DateTime.Now;

                paciente.Nome = tbxNomePaciente.Text.ToUpper();
                if (chkMaeIgnorada.Checked != true)
                {
                    paciente.NomeMae = tbxNomeMae.Text.ToUpper();
                }
                else
                {
                    paciente.NomeMae = "IGNORADA";
                }
                paciente.DataNascimento = DateTime.Parse(tbxDataNascimento.Text);
                if (chkPaiIgnorado.Checked != true)
                {
                    paciente.NomePai = tbxNomePai.Text.ToUpper();
                }
                else
                {
                    paciente.NomePai = "IGNORADO";
                }
                paciente.Sexo = char.Parse(rbtnListSexo.SelectedValue);
                paciente.RacaCor = new RacaCor();
                paciente.RacaCor.Codigo = ddlRacaCor.SelectedValue;
                if (paciente.RacaCor.Codigo == RacaCor.INDIGENA)
                {
                    paciente.Etnia = new Etnia();
                    paciente.Etnia.Codigo = ddlEtnia.SelectedValue;
                }
                paciente.FrequentaEscola = char.Parse(rbtnListFreqEscola.SelectedValue);
                paciente.Email = tbxEmail.Text;
                paciente.Vivo = cbxVivo.Checked ? '1' : '0';
                switch (rbtnListNacionalidade.SelectedValue)
                {
                    case "B":
                        {
                            if ((paciente.Pais != null) && (paciente.Pais.Codigo != "010"))//Se o paciente for brasileiro, não poderá alterar a nacionalidade
                            {
                                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Alteração de Nacionalidade Inválida!');</script>");
                                return;
                            }
                            paciente.Pais = new Pais();
                            paciente.Pais.Codigo = "010";
                            paciente.MunicipioNascimento = new Municipio();
                            paciente.MunicipioNascimento.Codigo = ddlMunicipioNascimento.SelectedValue;
                            break;
                        }
                    case "E": //Estrangeiro
                        {
                            if ((paciente.Pais == null) || (paciente.Pais != null && paciente.Pais.Codigo == "010"))
                            {
                                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Alteração de Nacionalidade Inválida!');</script>");
                                return;
                            }
                            paciente.Pais = new Pais();
                            paciente.Pais.Codigo = ddlPaisOrigem.SelectedValue;
                            break;

                        }
                    case "N":
                        {
                            if ((paciente.Pais != null) && (paciente.Pais.Codigo == "010"))
                            {
                                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Alteração de Nacionalidade Inválida!');</script>");
                                return;
                            }
                            paciente.Pais = null;
                            break;
                        }
                }

                //Coloquei as operações de banco ao final
                //pois se não for possível realizar a alteração devido às validações acima
                //estas não serão realizadas
                controlePaciente.Excluido = '0';
                ControlePacienteBLL.Inserir(controlePaciente);

                MotivoCadastroPaciente motivo = MotivocadastroPacienteBLL.PesquisarPorPaciente(paciente, ((Usuario)Session["Usuario"]).Unidade.CNES);

                if (motivo != null)
                {
                    motivo.Motivo = new MotivoCadastro();
                    motivo.Motivo.Codigo = int.Parse(ddlMotivo.SelectedValue);
                    MotivocadastroPacienteBLL.Atualizar(motivo);
                }
                else
                {
                    motivo = new MotivoCadastroPaciente();
                    motivo.Paciente = paciente;
                    motivo.Cnes = ((Usuario)Session["Usuario"]).Unidade.CNES;
                    motivo.Motivo = new MotivoCadastro();
                    motivo.Motivo.Codigo = int.Parse(ddlMotivo.SelectedValue);
                    MotivocadastroPacienteBLL.Cadastrar(motivo);
                }

                paciente.Deficiencia = this.RetornaDeficienciaPaciente();
                PacienteBLL.Atualizar(paciente);
                StartBackgroundThread(delegate { this.SalvarLog(new LogViverMais(DateTime.Now, (ViverMais.Model.Usuario)Session["Usuario"], 2, paciente.Codigo)); });
            }
            else //Cadastro de Paciente
            {
                paciente.Nome = tbxNomePaciente.Text.ToUpper();
                if (chkMaeIgnorada.Checked != true)
                {
                    paciente.NomeMae = tbxNomeMae.Text.ToUpper();
                }
                else
                {
                    paciente.NomeMae = "IGNORADO";
                }

                paciente.DataNascimento = DateTime.Parse(tbxDataNascimento.Text);

                //Verifica se já existe um paciente cadastrado com o mesmo nome, nome de mãe e data de nascimento                                
                IList<ViverMais.Model.Paciente> p = PacienteBLL.Pesquisar(tbxNomePaciente.Text.ToUpper(), tbxNomeMae.Text.ToUpper(), DateTime.Parse(tbxDataNascimento.Text));
                if (p.Count > 0)
                {
                    if (Request["url_retorno"] != null)
                        ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Dados salvos com sucesso!');window.location='" + HelperRedirector.DecodeURL(Request["url_retorno"].ToString()) + p[0].Codigo + "';</script>");
                    else
                        ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Já existe um Paciente cadastrado com os dados fornecidos. Confirme se os dados estão corretos e atualize se necessário.');window.location='FormPaciente.aspx?codigo=" + p[0].Codigo + "';</script>");
                    
                    return;
                }

                MotivoCadastroPaciente motivo = new MotivoCadastroPaciente();
                motivo.Cnes = ((Usuario)Session["Usuario"]).Unidade.CNES;
                motivo.Motivo = new MotivoCadastro();
                motivo.Motivo.Codigo = int.Parse(ddlMotivo.SelectedValue);

                if (chkPaiIgnorado.Checked != true)
                {
                    paciente.NomePai = tbxNomePai.Text.ToUpper();
                }
                else
                {
                    paciente.NomePai = "IGNORADO";
                }
                if (paciente.NomePai == "NAO EXISTE" || paciente.NomePai == "NÃO EXISTE")
                    paciente.NomePai = "IGNORADO";
                paciente.Sexo = char.Parse(rbtnListSexo.SelectedValue);
                paciente.RacaCor = new RacaCor();
                paciente.RacaCor.Codigo = ddlRacaCor.SelectedValue;
                if (paciente.RacaCor.Codigo == RacaCor.INDIGENA)
                {
                    paciente.Etnia = new Etnia();
                    paciente.Etnia.Codigo = ddlEtnia.SelectedValue;
                }
                paciente.FrequentaEscola = char.Parse(rbtnListFreqEscola.SelectedValue);
                paciente.Vivo = cbxVivo.Checked ? '1' : '0';
                paciente.Email = tbxEmail.Text;
                //Se brasileiro adiciona o município
                if (rbtnListNacionalidade.SelectedValue == "B")
                {
                    paciente.Pais = new Pais();
                    paciente.Pais.Codigo = "010";
                    paciente.MunicipioNascimento = new Municipio();
                    paciente.MunicipioNascimento.Codigo = ddlMunicipioNascimento.SelectedValue;
                }
                else if (rbtnListNacionalidade.SelectedValue == "E")
                {
                    paciente.Pais = new Pais();
                    paciente.Pais.Codigo = ddlPaisOrigem.SelectedValue;
                }


                try
                {
                    paciente.Deficiencia = this.RetornaDeficienciaPaciente();
                    PacienteBLL.Cadastrar(paciente);
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('" + ex.Message + "');</script>");
                    return;
                }

                motivo.Paciente = paciente;

                MotivocadastroPacienteBLL.Cadastrar(motivo);
                //ViewState["codigoPaciente"] = paciente.Codigo;
                StartBackgroundThread(delegate { this.SalvarLog(new LogViverMais(DateTime.Now, (ViverMais.Model.Usuario)Session["Usuario"], 1, paciente.Codigo)); });
            }
            Endereco endereco;
            //Passos comuns ao cadastro e edição
            endereco = EnderecoBLL.PesquisarPorPaciente(paciente);
            if (endereco != null)
            {
                //string codigoEndereco = (string)ViewState["codigoEndereco"];
                //endereco = EnderecoBLL.PesquisarCompletoPorPaciente(paciente);

                ViverMais.Model.Endereco novoEndereco = new Endereco();
                PreencherObjetoEndereco(novoEndereco);

                endereco.DDD = novoEndereco.DDD;
                endereco.Telefone = novoEndereco.Telefone;
                endereco.Complemento = novoEndereco.Complemento;

                if (endereco.Equals(novoEndereco))
                    EnderecoBLL.Atualizar(endereco);
                else
                {
                    try
                    {
                        EnderecoUsuarioBLL.RealizarMudanca(EnderecoUsuarioBLL.Pesquisar(paciente.Codigo, endereco.Codigo), novoEndereco);
                        //Guarda na variável para salvar na sessão depois
                        endereco = novoEndereco;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                if (((Usuario)Session["usuario"]).Unidade.MunicipioGestor.Codigo != "292740")
                {
                    lblBairro.Text = string.Empty;
                    lblMunicipioResidencia.Text = string.Empty;
                }
            }
            else
            {
                endereco = new Endereco();
                EnderecoUsuario enderecoUsuario = new EnderecoUsuario();
                PreencherObjetoEndereco(endereco);
                enderecoUsuario.Endereco = endereco;
                enderecoUsuario.CodigoPaciente = paciente.Codigo;
                enderecoUsuario.Excluido = '0';
                enderecoUsuario.Operacao = DateTime.Now;
                enderecoUsuario.TipoEndereco = new TipoEndereco();
                enderecoUsuario.TipoEndereco.Codigo = "01";
                enderecoUsuario.Endereco.ControleEndereco = new ControleEndereco();

                try
                {
                    EnderecoUsuarioBLL.Cadastrar(enderecoUsuario);
                    //Guarda na variável para salvar na sessão depois
                    endereco = enderecoUsuario.Endereco;
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('" + ex.Message + "');</script>");
                    return;
                }

                //StartBackgroundThread(delegate { this.CadastrarEndereco(endereco, paciente); });
            }

            //ViewState["codigoEndereco"] = endereco.Codigo;

            ViverMais.Model.Documento docNacionalidade = null;
            switch (rbtnListNacionalidade.SelectedValue)
            {
                case "E"://Futuramente colocar documentos na sessão
                    docNacionalidade = DocumentoBLL.PesqusiarPorPaciente("05", paciente);
                    if (docNacionalidade == null)
                    {
                        docNacionalidade = new Documento();
                        ViverMais.Model.ControleDocumento controle = new ViverMais.Model.ControleDocumento();
                        controle.Paciente = paciente;
                        controle.TipoDocumento = new TipoDocumento();
                        controle.TipoDocumento.Codigo = "05";
                        docNacionalidade.ControleDocumento = controle;
                        docNacionalidade.DataChegadaBrasil = DateTime.Parse(tbxDataEntradaBrasil.Text);
                        docNacionalidade.DataNaturalizacao = null;
                        DocumentoBLL.Cadastrar(docNacionalidade);
                    }
                    else
                    {
                        docNacionalidade.DataChegadaBrasil = DateTime.Parse(tbxDataEntradaBrasil.Text);
                        docNacionalidade.DataNaturalizacao = null;
                        DocumentoBLL.Atualizar(docNacionalidade);
                    }

                    break;
                case "N":
                    docNacionalidade = DocumentoBLL.PesqusiarPorPaciente("05", paciente);
                    if (docNacionalidade == null)
                    {
                        docNacionalidade = new Documento();
                        ViverMais.Model.ControleDocumento controle = new ViverMais.Model.ControleDocumento();
                        controle.Paciente = paciente;
                        controle.TipoDocumento = new TipoDocumento();
                        controle.TipoDocumento.Codigo = "05";
                        docNacionalidade.ControleDocumento = controle;
                        docNacionalidade.DataNaturalizacao = DateTime.Parse(tbxDataNaturalizacao.Text);
                        docNacionalidade.NumeroPortaria = tbxNaturalizacaoPortaria.Text;
                        docNacionalidade.DataChegadaBrasil = null;
                        DocumentoBLL.Cadastrar(docNacionalidade);
                    }
                    else
                    {
                        docNacionalidade.DataNaturalizacao = DateTime.Parse(tbxDataNaturalizacao.Text);
                        docNacionalidade.NumeroPortaria = tbxNaturalizacaoPortaria.Text;
                        docNacionalidade.DataChegadaBrasil = null;
                        DocumentoBLL.Atualizar(docNacionalidade);
                    }
                    break;
            }

            if (!string.IsNullOrEmpty(tbxCPF.Text))
            {
                ViverMais.Model.Documento doc = DocumentoBLL.PesqusiarPorPaciente("02", paciente);
                if (doc == null)
                {
                    doc = new ViverMais.Model.Documento();
                    doc.Numero = tbxCPF.Text;
                    ViverMais.Model.ControleDocumento controle = new ViverMais.Model.ControleDocumento();
                    controle.Paciente = paciente;
                    controle.TipoDocumento = new TipoDocumento();
                    controle.TipoDocumento.Codigo = "02";
                    doc.ControleDocumento = controle;
                    DocumentoBLL.Cadastrar(doc);
                }
                else
                {
                    doc.Numero = tbxCPF.Text;
                    DocumentoBLL.Atualizar(doc);
                }
            }

            if (!string.IsNullOrEmpty(tbxIdentidade.Text))
            {
                ViverMais.Model.Documento doc = DocumentoBLL.PesqusiarPorPaciente("10", paciente);
                if (doc == null)
                {
                    doc = new ViverMais.Model.Documento();
                    doc.Numero = tbxIdentidade.Text;
                    doc.Complemento = tbxComplementoIdentidade.Text;
                    doc.UF = new UF();
                    doc.UF.Sigla = ddlUFIdentidade.SelectedValue;
                    doc.DataEmissao = DateTime.Parse(tbxDataEmissaoIdentidade.Text);
                    doc.OrgaoEmissor = new OrgaoEmissor();
                    doc.OrgaoEmissor.Codigo = ddlOrgaoExpedidor.SelectedValue;
                    ViverMais.Model.ControleDocumento controle = new ViverMais.Model.ControleDocumento();
                    controle.Paciente = paciente;
                    controle.TipoDocumento = new TipoDocumento();
                    controle.TipoDocumento.Codigo = "10";
                    doc.ControleDocumento = controle;
                    DocumentoBLL.Cadastrar(doc);
                }
                else
                {
                    doc.Numero = tbxIdentidade.Text;
                    doc.Complemento = tbxComplementoIdentidade.Text;
                    doc.UF = new UF();
                    doc.UF.Sigla = ddlUFIdentidade.SelectedValue;
                    doc.DataEmissao = DateTime.Parse(tbxDataEmissaoIdentidade.Text);
                    doc.OrgaoEmissor = new OrgaoEmissor();
                    doc.OrgaoEmissor.Codigo = ddlOrgaoExpedidor.SelectedValue;
                    DocumentoBLL.Atualizar(doc);
                }
            }

            if (ddlTipoCertidao.SelectedValue != "-1")
            {
                ViverMais.Model.Documento doc = DocumentoBLL.PesqusiarPorPaciente(ddlTipoCertidao.SelectedValue, paciente);
                if (doc == null)
                {
                    doc = new ViverMais.Model.Documento();
                    if (!tbxNovaCertidao.Text.Contains("_"))
                    {
                        doc.Numero = tbxNovaCertidao.Text.Replace(".", "").Substring(0, 15);
                        doc.Complemento = tbxNovaCertidao.Text.Split('-')[1];
                    }
                    doc.NomeCartorio = tbxNomeCartorio.Text;
                    doc.NumeroLivro = tbxLivro.Text;
                    doc.NumeroFolha = tbxFolhas.Text;
                    doc.NumeroTermo = tbxTermo.Text;
                    doc.DataEmissao = DateTime.Parse(tbxDataEmissaoCertidao.Text);
                    ViverMais.Model.ControleDocumento controle = new ViverMais.Model.ControleDocumento();
                    controle.Paciente = paciente;
                    controle.TipoDocumento = new TipoDocumento();
                    controle.TipoDocumento.Codigo = ddlTipoCertidao.SelectedValue;
                    doc.ControleDocumento = controle;
                    DocumentoBLL.Cadastrar(doc);
                }
                else
                {
                    doc.NomeCartorio = tbxNomeCartorio.Text;
                    doc.NumeroLivro = tbxLivro.Text;
                    doc.NumeroFolha = tbxFolhas.Text;
                    doc.NumeroTermo = tbxTermo.Text;
                    doc.DataEmissao = DateTime.Parse(tbxDataEmissaoCertidao.Text);
                    DocumentoBLL.Atualizar(doc);
                }
            }

            if (!string.IsNullOrEmpty(tbxTituloEleitor.Text))
            {
                ViverMais.Model.Documento doc = DocumentoBLL.PesqusiarPorPaciente("03", paciente);
                if (doc == null)
                {
                    doc = new ViverMais.Model.Documento();
                    doc.Numero = tbxTituloEleitor.Text;
                    doc.SecaoEleitoral = tbxSecaoEleitoral.Text;
                    doc.ZonaEleitoral = tbxZonaEleitoral.Text;
                    ViverMais.Model.ControleDocumento controle = new ViverMais.Model.ControleDocumento();
                    controle.Paciente = paciente;
                    controle.TipoDocumento = new TipoDocumento();
                    controle.TipoDocumento.Codigo = "03";
                    doc.ControleDocumento = controle;
                    DocumentoBLL.Cadastrar(doc);
                }
                else
                {
                    doc.Numero = tbxTituloEleitor.Text;
                    doc.SecaoEleitoral = tbxSecaoEleitoral.Text;
                    doc.ZonaEleitoral = tbxZonaEleitoral.Text;
                    DocumentoBLL.Atualizar(doc);
                }
            }

            if (!string.IsNullOrEmpty(tbxCTPS.Text))
            {
                ViverMais.Model.Documento doc = DocumentoBLL.PesqusiarPorPaciente("04", paciente);
                if (doc == null)
                {
                    doc = new ViverMais.Model.Documento();
                    doc.Numero = tbxCTPS.Text;
                    doc.Serie = tbxSerieCTPS.Text;
                    doc.DataEmissao = DateTime.Parse(tbxDataEmissaoCTPS.Text);
                    doc.UF = new UF();
                    doc.UF.Sigla = ddlUFCTPS.SelectedValue;
                    ViverMais.Model.ControleDocumento controle = new ViverMais.Model.ControleDocumento();
                    controle.Paciente = paciente;
                    controle.TipoDocumento = new TipoDocumento();
                    controle.TipoDocumento.Codigo = "04";
                    doc.ControleDocumento = controle;
                    DocumentoBLL.Cadastrar(doc);
                }
                else
                {
                    doc.Numero = tbxCTPS.Text;
                    doc.Serie = tbxSerieCTPS.Text;
                    doc.DataEmissao = DateTime.Parse(tbxDataEmissaoCTPS.Text);
                    doc.UF = new UF();
                    doc.UF.Sigla = ddlUFCTPS.SelectedValue;
                    DocumentoBLL.Atualizar(doc);
                }
            }

            HiddenCodigoPaciente.Value = paciente.Codigo;
            HiddenCodigoPaciente.DataBind();
            HiddenNomePaciente.Value = paciente.Nome;
            HiddenNomePaciente.DataBind();
            HiddenDataNascimento.Value = paciente.DataNascimento.ToString("dd/MM/yyyy");
            HiddenDataNascimento.DataBind();
            HiddenMunicipio.Value = endereco.Municipio.Nome;
            HiddenMunicipio.DataBind();

            if (Request["codigo"] == null)
            {
                List<CartaoSUS> cartoes = CartaoSUSBLL.ListarPorPaciente(paciente);
                HiddenNumeroCartao.Value = cartoes[0].Numero;
                HiddenNumeroCartao.DataBind();
                lblCNS.Text = cartoes[0].Numero;
                lblCNS.DataBind();
            }

            UpdatePanel_CartaoSUS.Update();
            UpdatePanel_CamposEscondidos.Update();

            if (Request["url_retorno"] != null)
                //{
                //if (Request["encode"] != null && Request["encode"].ToString().Equals("sim"))
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Dados salvos com sucesso!');window.location='" + HelperRedirector.DecodeURL(Request["url_retorno"].ToString()) + paciente.Codigo + "';</script>");
            //else
            //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Dados salvos com sucesso!');window.location='" + HttpUtility.UrlDecode(Request["url_retorno"].ToString()) + paciente.Codigo + "';</script>");
            //}
            else
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Dados salvos com sucesso!');window.location='FormPaciente.aspx?codigo=" + paciente.Codigo + "';</script>");
        }

        protected void btnBiometria_Click(object sender, EventArgs e)
        {
            Response.Redirect("Biometria.aspx?codigo=" + Request.QueryString["codigo"]);
        }

        protected void CustomValidatorCartorio_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (string.IsNullOrEmpty(tbxNomeCartorio.Text) && ddlTipoCertidao.SelectedValue != "-1")
                args.IsValid = false;
        }

        protected void CustomValidatorLivro_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (string.IsNullOrEmpty(tbxLivro.Text) && ddlTipoCertidao.SelectedValue != "-1")
                args.IsValid = false;
        }

        protected void CustomValidatorFolhas_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (string.IsNullOrEmpty(tbxFolhas.Text) && ddlTipoCertidao.SelectedValue != "-1")
                args.IsValid = false;
        }

        protected void CustomValidatorTermo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (string.IsNullOrEmpty(tbxTermo.Text) && ddlTipoCertidao.SelectedValue != "-1")
                args.IsValid = false;

        }

        protected void CustomValidatorDataCertidao_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            DateTime result = new DateTime();

            if (ddlTipoCertidao.SelectedValue != "-1") //Caso seja selecionada uma certidão
            {
                if (!DateTime.TryParse(tbxDataEmissaoCertidao.Text, out result)) //Caso não seja valida a data informada
                    args.IsValid = false;
                else
                {
                    if (result < DateTime.Parse(tbxDataNascimento.Text)) //Caso a data informada seja menor que a data de nascimento
                        args.IsValid = false;
                    else if (DateTime.Today.CompareTo(result) == -1) //Caso a data informada seja maior que a data atual
                    {
                        args.IsValid = false;
                        CustomValidatorDataCertidao.Text = "Data da certidão de nascimento maior que a data atual";
                    }
                }
            }
        }

        protected void lnkCartaoSus_Click(object sender, EventArgs e)
        {
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            IPaciente ipaciente = Factory.GetInstance<IPaciente>();

            if (HiddenNumeroCartao.Value.Trim() != "")
            {
                long result = long.Parse(HiddenNumeroCartao.Value.Trim());
                IList<ControleCartaoSUS> _controlesCartao = ipaciente.ListarControleCartaoSUS<ControleCartaoSUS>(result.ToString());
                ControleCartaoSUS controleCartao = new ControleCartaoSUS();
                controleCartao.NumeroCartao = result.ToString();
                controleCartao.ViaCartao = (_controlesCartao.Count() > 0 ? _controlesCartao.Select(p => p.ViaCartao).Max() : 0) + 1;
                //_controlesCartao.Count + 1;

                //if (_controlesCartao.Count > 0)
                //    controleCartao.ViaCartao = _controlesCartao.Last().ViaCartao + 1;
                //else
                //    controleCartao.ViaCartao = 1;

                controleCartao.DataEmissao = DateTime.Now;
                controleCartao.Usuario = (Usuario)Session["Usuario"];
                iViverMais.Inserir(controleCartao);
                //lblViaCartao.Text = controleCartao.ViaCartao.ToString();
                //lblViaCartao.DataBind();
                //this.UpdatePanel_CartaoSUS.Update();
                iViverMais.Inserir(new LogViverMais(DateTime.Now, (ViverMais.Model.Usuario)Session["Usuario"], 5, result.ToString()));
                AddCartaoSUSCompleto(result);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "alert(Não foi possível localizar o numero do cartão SUS. Favor informar este problema ao suporte técnico.", true);
            }
        }


        //Acho que não é emais usado
        protected void lnkEtiqueta_Click(object sender, EventArgs e)
        {
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            IPaciente ipaciente = Factory.GetInstance<IPaciente>();

            if (HiddenNumeroCartao.Value.Trim() != "")
            {
                long result = long.Parse(HiddenNumeroCartao.Value.Trim());
                IList<ControleCartaoSUS> _controlesCartao = ipaciente.ListarControleCartaoSUS<ControleCartaoSUS>(result.ToString());
                ControleCartaoSUS controleCartao = new ControleCartaoSUS();
                controleCartao.NumeroCartao = result.ToString();
                controleCartao.ViaCartao = (_controlesCartao.Count() > 0 ? _controlesCartao.Select(p => p.ViaCartao).Max() : 0) + 1;
                //_controlesCartao.Count + 1;

                //if (_controlesCartao.Count > 0)
                //    controleCartao.ViaCartao = _controlesCartao.Last().ViaCartao + 1;
                //else
                //    controleCartao.ViaCartao = 1;

                controleCartao.DataEmissao = DateTime.Now;
                controleCartao.Usuario = (Usuario)Session["Usuario"];
                iViverMais.Inserir(controleCartao);
                //lblViaCartao.Text = controleCartao.ViaCartao.ToString();
                //lblViaCartao.DataBind();
                //this.UpdatePanel_CartaoSUS.Update();
                iViverMais.Inserir(new LogViverMais(DateTime.Now, (ViverMais.Model.Usuario)Session["Usuario"], 5, result.ToString()));
                AddCartaoAnexo(result);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "alert(Não foi possível localizar o numero do cartão SUS. Favor informar este problema ao suporte técnico.", true);
            }
        }

        private void AddCartaoSUSCompleto(long numeroCartaoSUS)
        {
            ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
            if (!iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "ALTERAR_CARTAO_SUS", Modulo.CARTAO_SUS))
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Você não tem permissão para acessar essa página. Em caso de dúViverMais, entre em contato.');window.location='../Home.aspx';</script>");
                return;
            }

            MemoryStream MStream = new MemoryStream();
            iTextSharp.text.Document doc = new iTextSharp.text.Document(new iTextSharp.text.Rectangle(295, 191));
            PdfWriter writer = PdfWriter.GetInstance(doc, MStream);

            //Monta o pdf
            doc.Open();
            iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
            p.IndentationLeft = -10;
            p.Font.Color = iTextSharp.text.Color.BLACK;
            iTextSharp.text.Phrase nome = new iTextSharp.text.Phrase(HiddenNomePaciente.Value + "\n");
            //paciente.Nome
            nome.Font.Size = 8;
            iTextSharp.text.Phrase nascimento = new iTextSharp.text.Phrase(HiddenDataNascimento.Value + "\t\t" + HiddenMunicipio.Value + "\n");
            nascimento.Font.Size = 8;
            iTextSharp.text.Phrase cartaosus = new iTextSharp.text.Phrase(numeroCartaoSUS + "\n");
            cartaosus.Font.Size = 12;
            PdfContentByte cb = writer.DirectContent;
            Barcode39 code39 = new Barcode39();
            code39.Code = numeroCartaoSUS.ToString();
            code39.StartStopText = true;
            code39.GenerateChecksum = false;
            code39.Extended = true;
            iTextSharp.text.Image imageEAN = code39.CreateImageWithBarcode(cb, null, null);

            iTextSharp.text.Image back = iTextSharp.text.Image.GetInstance(Server.MapPath("img/") + "back_card.JPG");
            back.SetAbsolutePosition(0, doc.PageSize.Height - back.Height);

            iTextSharp.text.Image front = iTextSharp.text.Image.GetInstance(Server.MapPath("img/") + "front_card.JPG");
            front.SetAbsolutePosition(0, doc.PageSize.Height - front.Height);

            iTextSharp.text.Phrase barcode = new iTextSharp.text.Phrase(new iTextSharp.text.Chunk(imageEAN, 36, -45));
            barcode.Font.Color = iTextSharp.text.Color.WHITE;

            p.SetLeading(1, 0.7f);
            p.Add(cartaosus);
            p.Add(nome);
            p.Add(nascimento);
            p.Add(barcode);
            doc.Add(p);

            doc.Add(back);
            doc.NewPage();
            doc.Add(front);

            doc.Close();
            //Fim monta pdf



            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=CartaoSUS.pdf");
            HttpContext.Current.Response.BinaryWrite(MStream.GetBuffer());
            HttpContext.Current.Response.End();
        }

        private void AddCartaoAnexo(long numeroCartaoSUS)
        {
            ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
            if (!iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "ALTERAR_CARTAO_SUS", Modulo.CARTAO_SUS))
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Você não tem permissão para acessar essa página. Em caso de dúViverMais, entre em contato.');</script>");
                return;
            }


            MemoryStream MStream = new MemoryStream();
            iTextSharp.text.Document doc = new iTextSharp.text.Document(new iTextSharp.text.Rectangle(405, 134), 25, 40, 10, 0);
            iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, MStream);

            //Monta o pdf
            doc.Open();
            iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
            iTextSharp.text.Phrase nome = new iTextSharp.text.Phrase(HiddenNomePaciente.Value + "\n");
            nome.Font.Size = 14;
            iTextSharp.text.Phrase nascimento = new iTextSharp.text.Phrase(HiddenDataNascimento.Value + "\t\t" + HiddenMunicipio.Value + "\n");
            nascimento.Font.Size = 14;
            iTextSharp.text.Phrase cartaosus = new iTextSharp.text.Phrase(numeroCartaoSUS + "\n");
            cartaosus.Font.Size = 16;
            PdfContentByte cb = writer.DirectContent;
            Barcode39 code39 = new Barcode39();
            code39.Code = numeroCartaoSUS.ToString();
            code39.StartStopText = true;
            code39.GenerateChecksum = false;
            code39.Extended = true;
            iTextSharp.text.Image imageEAN = code39.CreateImageWithBarcode(cb, null, null);
            imageEAN.ScalePercent(170);
            iTextSharp.text.Image back = iTextSharp.text.Image.GetInstance(Server.MapPath("img/") + "etiqueta.png");
            back.SetAbsolutePosition(0, doc.PageSize.Height - back.Height);
            iTextSharp.text.Phrase barcode = new iTextSharp.text.Phrase(new iTextSharp.text.Chunk(imageEAN, 0, -45));
            p.Add(cartaosus);
            p.Add(nome);
            p.Add(nascimento);
            p.Add(barcode);
            doc.Add(p);
            doc.Close();

            //fim monta pdf


            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=CartaoEtiqueta.pdf");
            HttpContext.Current.Response.BinaryWrite(MStream.GetBuffer());
            HttpContext.Current.Response.End();
        }



        protected void CustomValidatorIdentidadeCertidao_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (string.IsNullOrEmpty(tbxIdentidade.Text) && ddlTipoCertidao.SelectedValue == "-1")
                args.IsValid = false;
        }

        protected void CustomValidatorSerieCTPS_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (!string.IsNullOrEmpty(tbxCTPS.Text) && string.IsNullOrEmpty(tbxSerieCTPS.Text))
                args.IsValid = false;
        }

        protected void CustomValidatorUFCTPS_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (!string.IsNullOrEmpty(tbxCTPS.Text) && ddlUFCTPS.SelectedValue == "-1")
                args.IsValid = false;
        }

        protected void CustomValidatorDataCTPS_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            DateTime date = DateTime.Today;
            DateTime datanascimento = DateTime.MinValue;

            if (!string.IsNullOrEmpty(tbxCTPS.Text))
            {
                if (!DateTime.TryParse(tbxDataEmissaoCTPS.Text, out date))
                    args.IsValid = false;
                else if (DateTime.TryParse(this.tbxDataNascimento.Text, out datanascimento) && date.CompareTo(datanascimento) <= 0)
                {
                    args.IsValid = false;
                    CustomValidatorDataCTPS.Text = "A Data de Emissão da Carteira de Trabalho deve ser maior que a Data de Nascimento";
                }
                else if (DateTime.Today.CompareTo(date) == -1)
                {
                    args.IsValid = false;
                    CustomValidatorDataCTPS.Text = "A Data de Emissão da Carteira de Trabalho deve ser menor que a Data de Hoje";
                }
            }
        }

        protected void CustomValidatorZonaEleitoral_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (!string.IsNullOrEmpty(tbxTituloEleitor.Text) && string.IsNullOrEmpty(tbxZonaEleitoral.Text))
                args.IsValid = false;
        }

        protected void CustomValidatorSecaoEleitoral_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (!string.IsNullOrEmpty(tbxTituloEleitor.Text) && string.IsNullOrEmpty(tbxSecaoEleitoral.Text))
                args.IsValid = false;
        }

        protected void CustomValidatorOrgaoEmissor_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (!string.IsNullOrEmpty(tbxIdentidade.Text) && ddlOrgaoExpedidor.SelectedValue == "-1")
                args.IsValid = false;
        }

        protected void CVDataEmissaoIdentidade_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (!string.IsNullOrEmpty(this.tbxDataEmissaoIdentidade.Text) &&
                !string.IsNullOrEmpty(this.tbxDataNascimento.Text) && (DateTime.Parse(this.tbxDataEmissaoIdentidade.Text)
                .CompareTo(DateTime.Parse(this.tbxDataNascimento.Text)) <= 0
                || DateTime.Parse(this.tbxDataEmissaoIdentidade.Text).CompareTo(DateTime.Today) > 0))
                args.IsValid = false;

            //if (string.IsNullOrEmpty(tbxIdentidade.Text))
            //{
            //    args.IsValid = true;
            //}
            //else
            //{
            //    if (string.IsNullOrEmpty(tbxDataEmissaoIdentidade.Text))
            //    {
            //        args.IsValid = false;
            //    }
            //    else
            //    {
            //        try
            //        {
            //            if (DateTime.Parse(tbxDataNascimento.Text).CompareTo(DateTime.Parse(tbxDataEmissaoIdentidade.Text)) > 0)
            //            {
            //                args.IsValid = false;
            //            }
            //            else
            //            {
            //                if (DateTime.Today.CompareTo(DateTime.Parse(tbxDataEmissaoIdentidade.Text)) == -1)
            //                {
            //                    args.IsValid = false;
            //                    CVDataEmissaoIdentidade.Text = "Data de emissão da identidade maior que a data atual";
            //                }

            //                args.IsValid = true;
            //            }
            //        }
            //        catch (FormatException)
            //        {
            //            args.IsValid = false;
            //        }
            //    }
            //}
        }

        protected void OnServerValidate_Telefone(object sender, ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if ((this.tbxDDD.Text == string.Empty && this.tbxTelefone.Text != string.Empty) ||
                (this.tbxDDD.Text != string.Empty && this.tbxTelefone.Text == string.Empty) ||
                (this.tbxDDD.Text != string.Empty && this.tbxDDD.Text.Length != 2) ||
                (this.tbxTelefone.Text != string.Empty && this.tbxTelefone.Text.Length != 8))
                args.IsValid = false;
        }

        protected void CustomValidatorUFIdentidade_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (!string.IsNullOrEmpty(tbxIdentidade.Text) && ddlUFIdentidade.SelectedValue == "-1")
                args.IsValid = false;
        }

        protected void CustomValidatorDataNascimento_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (DateTime.Today.Subtract(DateTime.Parse(tbxDataNascimento.Text)).Days < 0)
                args.IsValid = false;
            if (DateTime.Today.CompareTo(DateTime.Parse(tbxDataNascimento.Text)) == -1)
            {
                args.IsValid = false;
                CustomValidatorDataNascimento.Text = "Data de nascimento maior que a data atual";
            }
        }

        protected void CustomValidatorUFNasc_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (rbtnListNacionalidade.SelectedValue.Equals("B") && ddlUFNascimento.SelectedValue == "-1")
                args.IsValid = false;
        }

        protected void CustomValidatorMunicipioNasc_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (rbtnListNacionalidade.SelectedValue.Equals("B") && (ddlMunicipioNascimento.SelectedValue == null || ddlMunicipioNascimento.SelectedValue == "-1"))
                args.IsValid = false;
        }

        protected void CustomValidatorPaisNasc_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (rbtnListNacionalidade.SelectedValue.Equals("E") && (ddlMunicipioNascimento.SelectedValue == null || ddlPaisOrigem.SelectedValue == "-1"))
                args.IsValid = false;
        }

        protected void CustomValidatorDataEntradaBrasil_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (rbtnListNacionalidade.SelectedValue.Equals("E") && string.IsNullOrEmpty(tbxDataEntradaBrasil.Text))
                args.IsValid = false;
            if (rbtnListNacionalidade.SelectedValue.Equals("E") && !string.IsNullOrEmpty(tbxDataEntradaBrasil.Text))
            {
                if (DateTime.Today.CompareTo(DateTime.Parse(tbxDataEntradaBrasil.Text)) == -1)
                {
                    args.IsValid = false;
                    CustomValidatorDataEntradaBrasil.Text = "Data de entrada no Brasil maior que a data atual";
                }
            }
        }

        protected void CustomValidatorDataNaturalizacao_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (rbtnListNacionalidade.SelectedValue.Equals("N") && string.IsNullOrEmpty(tbxDataNaturalizacao.Text))
                args.IsValid = false;

            if (rbtnListNacionalidade.SelectedValue.Equals("N") && !string.IsNullOrEmpty(tbxDataNaturalizacao.Text))
            {
                if (DateTime.Today.CompareTo(DateTime.Parse(tbxDataNaturalizacao.Text)) == -1)
                {
                    args.IsValid = false;
                    CustomValidatorDataNaturalizacao.Text = "Data de naturalização maior que a data atual";
                }
            }
        }

        protected void CustomValidatorNumPortaria_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (rbtnListNacionalidade.SelectedValue.Equals("N") && string.IsNullOrEmpty(tbxNaturalizacaoPortaria.Text))
                args.IsValid = false;
        }


        private void AtualizarMotivo(object motivo)
        {
            Factory.GetInstance<IPaciente>().AtualizarMotivo<ViverMais.Model.MotivoCadastroPaciente>((ViverMais.Model.MotivoCadastroPaciente)motivo);
        }

        private void SalvarMotivo(object motivo)
        {
            Factory.GetInstance<IPaciente>().SalvarMotivo<ViverMais.Model.MotivoCadastroPaciente>((ViverMais.Model.MotivoCadastroPaciente)motivo);
        }

        private void CadastrarEndereco(Endereco endereco, ViverMais.Model.Paciente paciente)
        {
            Factory.GetInstance<IEndereco>().CadastrarEndereco<ViverMais.Model.Endereco, ViverMais.Model.Paciente>(endereco, paciente);
        }

        private void SalvarLog(LogViverMais log)
        {
            Factory.GetInstance<ILogEventos>().Salvar(log);
        }

        protected void rbtnListNacionalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rbtnListNacionalidade.SelectedValue)
            {
                case "B":
                    MultiView1.SetActiveView(this.ViewBrasileiro);
                    CompareValidatorIdentidade.Enabled = true; //Habilita a validação do Campo Identidade para permitir somente Números
                    RegularExpressionValidator4.Enabled = false; //Desabilita a validação do Campo Identidade para permitir letras e números
                    CompareValidator1.Enabled = true;//Habilita a validação do Campo Complemento Identidade para permitir somente Números
                    RegularExpressionValidator8.Enabled = false; //Desabilita a validação do Campo Complemento Identidade para permitir letras e números
                    break;
                case "E":
                    MultiView1.SetActiveView(this.ViewEstrangeiro);
                    RegularExpressionValidator4.Enabled = true;//Habilita a validação do Campo Identidade para permitir letras e números
                    RegularExpressionValidator8.Enabled = true;//Habilita a validação do Campo Complemento Identidade para permitir letras e números
                    CompareValidatorIdentidade.Enabled = false;//Desabilita a validação do Campo Identidade para permitir somente Números
                    CompareValidator1.Enabled = false;//Desabilita a validação do Campo Complemento Identidade para permitir somente Números
                    break;
                case "N":
                    MultiView1.SetActiveView(this.ViewNaturalizado);
                    CompareValidatorIdentidade.Enabled = true; //Habilita a validação do Campo Identidade para permitir somente Números
                    RegularExpressionValidator4.Enabled = false; //Desabilita a validação do Campo Identidade para permitir letras e números
                    CompareValidator1.Enabled = true;//Habilita a validação do Campo Complemento Identidade para permitir somente Números
                    RegularExpressionValidator8.Enabled = false; //Desabilita a validação do Campo Complemento Identidade para permitir letras e números
                    break;
            }
        }

        protected void ddlUFNascimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            IList<ViverMais.Model.Municipio> municipios = MunicipioBLL.PesquisarPorEstado(ddlUFNascimento.SelectedValue);
            //ddlMunicipioNascimento.Items.Clear();
            ddlMunicipioNascimento.DataSource = municipios;
            ddlMunicipioNascimento.DataBind();
            ddlMunicipioNascimento.Items.Insert(0, new ListItem("Selecione...", "-1"));
            //foreach (ViverMais.Model.Municipio municipio in municipios)
            //    ddlMunicipioNascimento.Items.Add(new ListItem(municipio.Nome.Split('-')[0].Trim(), municipio.Codigo));

            this.ddlMunicipioNascimento.Focus();
        }

        public static void StartBackgroundThread(ThreadStart threadStart)
        {
            if (threadStart != null)
            {
                Thread thread = new Thread(threadStart);
                thread.IsBackground = true;
                thread.Start();
            }
        }

        protected void tbxNovaCertidao_TextChanged(object sender, EventArgs e)
        {
            if (!tbxNovaCertidao.Text.Contains("_"))
            {
                string[] split = tbxNovaCertidao.Text.Split('.');

                if (split.Count() >= 6)
                    tbxLivro.Text = split[5];
                else
                    tbxLivro.Text = "";

                if (split.Count() >= 7)
                    tbxFolhas.Text = split[6];
                else
                    tbxFolhas.Text = "";

                if (split.Count() >= 8)
                    tbxTermo.Text = split[7].Substring(0, 7);
                else
                    tbxTermo.Text = "";
            }
        }

        protected void imgBuscarCEP_Click(object sender, ImageClickEventArgs e)
        {
            if (tbxCEP.Text != string.Empty && Regex.IsMatch(tbxCEP.Text, "[0-9]{8}")
                && tbxCEP.Text != "40000000" && tbxCEP.Text.Length == 8)
            {
                ViverMais.Model.Logradouro log = LogradouroBLL.LocalizarPorCep(long.Parse(tbxCEP.Text));

                if (log != null)
                {
                    if (((Usuario)Session["usuario"]).Unidade.MunicipioGestor.Codigo != Municipio.SALVADOR && ((Usuario)Session["usuario"]).Unidade.MunicipioGestor.Nome.Trim().Split('-')[0].Trim() != log.Cidade.Trim())
                    {
                        ddlTipoLogradouro.SelectedValue = "-1";
                        tbxNomeLogradouro.Text = string.Empty;
                        tbxNumero.Text = string.Empty;
                        tbxComplemento.Text = string.Empty;
                        ddlBairro.SelectedValue = "-1";
                        ddlMunicipios.SelectedValue = ((Usuario)Session["usuario"]).Unidade.MunicipioGestor.Codigo;
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alerta", "alert('Este CEP não pertence ao seu município');", true);
                        return;
                    }
                    ddlTipoLogradouro.SelectedValue = ddlTipoLogradouro.Items.FindByText(log.Tipo) == null ? "-1" : ddlTipoLogradouro.Items.FindByText(log.Tipo).Value;
                    tbxNomeLogradouro.Text = log.NomeLogradouro;
                    ddlMunicipios.SelectedValue =
                        ddlMunicipios.Items.FindByText(log.Cidade.Trim()) == null ? null : ddlMunicipios.Items.FindByText(log.Cidade.Trim()).Value;
                    Municipio municipio = new Municipio();
                    municipio.Codigo = ddlMunicipios.SelectedValue;
                    ddlBairro.Items.Clear();
                    ddlBairro.DataSource = BairroBLL.PesquisarPorMunicipio(municipio).OrderBy(x => x.Nome).ToList();
                    ddlBairro.DataBind();
                    ddlBairro.Items.Insert(0, new ListItem("Selecione...", "-1"));
                    ddlBairro.SelectedValue = ddlBairro.Items.FindByText(log.Bairro) == null ? null : ddlBairro.Items.FindByText(log.Bairro).Value;
                }
                else
                {
                    ddlTipoLogradouro.SelectedValue = "-1";
                    tbxNomeLogradouro.Text = string.Empty;
                    tbxNumero.Text = string.Empty;
                    tbxComplemento.Text = string.Empty;
                    ddlBairro.SelectedValue = "-1";
                    ddlMunicipios.SelectedValue = ((Usuario)Session["usuario"]).Unidade.MunicipioGestor.Codigo;
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Logradouro não encontrado.');document.getElementById('ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_imgBuscarCEP').focus();", true);
                }
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Informe o CEP desejado contendo 8 dígitos e que seja diferente de 40000000.');", true);
            //SetFocus(imgBuscarCEP);
        }

        //Comentado
        protected void lnkBtnEtiquetaHTML_Click(object sender, EventArgs e)
        {
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            IPaciente ipaciente = Factory.GetInstance<IPaciente>();
            IList<CartaoSUS> cartoes = ipaciente.ListarCartoesSUS<ViverMais.Model.CartaoSUS>(Request.QueryString["codigo"]);

            if (cartoes.Count() > 0)
            {
                IList<ControleCartaoSUS> _controlesCartao = ipaciente.ListarControleCartaoSUS<ControleCartaoSUS>(cartoes.Last().Numero);
                ControleCartaoSUS controleCartao = new ControleCartaoSUS();
                controleCartao.NumeroCartao = cartoes.Last().Numero;
                if (_controlesCartao.Count > 0)
                    controleCartao.ViaCartao = _controlesCartao.Last().ViaCartao + 1;
                else
                    controleCartao.ViaCartao = 1;
                controleCartao.DataEmissao = DateTime.Today;
                controleCartao.Usuario = (Usuario)Session["Usuario"];
                iViverMais.Inserir(controleCartao);
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>window.open('ImpressaoEtiquetaHTML.aspx?codigo=" + Request.QueryString["codigo"] + "', '_blank');</script>");
            }
        }

        protected void ddlMunicipios_SelectedIndexChanged(object sender, EventArgs e)
        {
            Municipio municipio = new Municipio();
            municipio.Codigo = ddlMunicipios.SelectedValue;
            ddlBairro.DataSource = BairroBLL.PesquisarPorMunicipio(municipio).OrderBy(x => x.Nome).ToList();
            ddlBairro.DataBind();
            ddlBairro.Items.Insert(0, new ListItem("Selecione...", "-1"));
            ddlBairro.SelectedIndex = 0;
            //tbxCEP.Text = string.Empty;
            ddlTipoLogradouro.SelectedIndex = 0;
            tbxNomeLogradouro.Text = string.Empty;
            tbxNumero.Text = string.Empty;
            tbxComplemento.Text = string.Empty;
        }

        protected void ddlRacaCor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRacaCor.SelectedValue == RacaCor.INDIGENA)
                ViewEtnia.Visible = true;
            else
                ViewEtnia.Visible = false;
            ddlEtnia.SelectedIndex = 0;
        }

        protected void OnSelectedIndexChanged_Deficiencia(object sender, EventArgs e)
        {
            if (this.rbDeficiencia.SelectedValue == "S")
            {
                this.Panel_TipoDeficiencia.Visible = true;
                this.Panel_OrigemDeficiencia.Visible = true;
                this.Panel_OrteseDeficiencia.Visible = true;
                this.Panel_ProteseDeficiencia.Visible = true;
            }
            else
            {
                this.Panel_TipoDeficiencia.Visible = false;
                this.Panel_OrigemDeficiencia.Visible = false;
                this.Panel_OrteseDeficiencia.Visible = false;
                this.Panel_ProteseDeficiencia.Visible = false;

                this.Panel_MeioLocomocaoDeficiencia.Visible = false;
                this.Panel_MeioComunicacaoDeficiencia.Visible = false;
                this.ZerarSelecaoItens(this.chckOrigemDeficiencia.Items);
                this.ZerarSelecaoItens(this.chckTipoDeficiencia.Items);
                this.ZerarSelecaoItens(this.chckProtese.Items);
                this.HabilitaItens(this.chckProtese.Items);
                this.ZerarSelecaoItens(this.chckLocomocaoDeficiencia.Items);
                this.HabilitaItens(this.chckLocomocaoDeficiencia.Items);
                this.ZerarSelecaoItens(this.chckComunicacaoDeficiencia.Items);
                this.HabilitaItens(this.chckComunicacaoDeficiencia.Items);
                this.ZerarSelecaoItens(this.rbOrtese.Items);
            }
        }

        protected void OnSelectedIndexChanged_TipoDeficiencia(object sender, EventArgs e)
        {
            if (this.chckTipoDeficiencia.Items.FindByValue(Deficiencia.FISICA.ToString()).Selected
                || this.chckTipoDeficiencia.Items.FindByValue(Deficiencia.INTELECTUAL.ToString()).Selected
                || this.chckTipoDeficiencia.Items.FindByValue(Deficiencia.VISUAL.ToString()).Selected
                || this.chckTipoDeficiencia.Items.FindByValue(Deficiencia.MULTIPLA.ToString()).Selected)
                this.Panel_MeioLocomocaoDeficiencia.Visible = true;
            else
            {
                this.Panel_MeioLocomocaoDeficiencia.Visible = false;
                this.ZerarSelecaoItens(this.chckLocomocaoDeficiencia.Items);
                this.HabilitaItens(this.chckLocomocaoDeficiencia.Items);
            }

            if (this.chckTipoDeficiencia.Items.FindByValue(Deficiencia.VISUAL.ToString()).Selected
                 || this.chckTipoDeficiencia.Items.FindByValue(Deficiencia.AUDITIVA.ToString()).Selected)
                this.Panel_MeioComunicacaoDeficiencia.Visible = true;
            else
            {
                this.Panel_MeioComunicacaoDeficiencia.Visible = false;
                this.ZerarSelecaoItens(this.chckComunicacaoDeficiencia.Items);
                this.HabilitaItens(this.chckComunicacaoDeficiencia.Items);
            }
        }

        protected void OnSelectedIndexChanged_Locomocao(object sender, EventArgs e)
        {
            if (
                this.chckLocomocaoDeficiencia.Items.FindByValue(LocomocaoDeficiencia.NAO_UTILIZA.ToString()).Selected)
                this.DesabilitaItens(this.chckLocomocaoDeficiencia.Items, LocomocaoDeficiencia.NAO_UTILIZA);
            else
                this.HabilitaItens(this.chckLocomocaoDeficiencia.Items);
        }

        protected void OnSelectedIndexChanged_Comunicacao(object sender, EventArgs e)
        {
            if (
                this.chckComunicacaoDeficiencia.Items.FindByValue(ComunicacaoDeficiencia.NAO_UTILIZA.ToString()).Selected)
                this.DesabilitaItens(this.chckComunicacaoDeficiencia.Items, ComunicacaoDeficiencia.NAO_UTILIZA);
            else
                this.HabilitaItens(this.chckComunicacaoDeficiencia.Items);
        }

        protected void OnSelectedIndexChanged_Protese(object sender, EventArgs e)
        {
            if (
                this.chckProtese.Items.FindByValue(ProteseDeficiencia.NAO_UTILIZA.ToString()).Selected)
                this.DesabilitaItens(this.chckProtese.Items, ProteseDeficiencia.NAO_UTILIZA);
            else
                this.HabilitaItens(this.chckProtese.Items);
        }

        protected void OnServerValidate_TipoDeficiencia(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = this.ExisteItemSelecionado(this.chckTipoDeficiencia.Items);
        }

        protected void OnServerValidate_Protese(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = this.ExisteItemSelecionado(this.chckProtese.Items);
        }

        protected void OnServerValidate_ComunicacaoDeficiencia(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = this.ExisteItemSelecionado(this.chckComunicacaoDeficiencia.Items);
        }

        protected void OnServerValidate_OrigemDeficiencia(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = this.ExisteItemSelecionado(this.chckOrigemDeficiencia.Items);
        }

        protected void OnServerValidate_LocomocaoDeficiencia(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = this.ExisteItemSelecionado(this.chckLocomocaoDeficiencia.Items);
        }

        private bool ExisteItemSelecionado(ListItemCollection itens)
        {
            foreach (ListItem item in itens)
                if (item.Selected)
                    return true;

            return false;
        }

        private void DesabilitaItens(ListItemCollection itens, int naoincluir)
        {
            foreach (ListItem item in itens)
            {
                if (int.Parse(item.Value) != naoincluir)
                {
                    item.Selected = false;
                    item.Enabled = false;
                }
            }
        }

        private void HabilitaItens(ListItemCollection itens)
        {
            foreach (ListItem item in itens)
                item.Enabled = true;
        }

        private void ZerarSelecaoItens(ListItemCollection itens)
        {
            foreach (ListItem item in itens)
                item.Selected = false;
        }

        private DeficienciaPaciente RetornaDeficienciaPaciente()
        {
            DeficienciaPaciente deficiencia = new DeficienciaPaciente();

            if (rbDeficiencia.SelectedValue == "S")
            {
                List<Deficiencia> deficiencias = new List<Deficiencia>();
                List<OrigemDeficiencia> origens = new List<OrigemDeficiencia>();
                List<ComunicacaoDeficiencia> comunicacoes = new List<ComunicacaoDeficiencia>();
                List<LocomocaoDeficiencia> locomocoes = new List<LocomocaoDeficiencia>();
                List<ProteseDeficiencia> proteses = new List<ProteseDeficiencia>();

                deficiencia.Deficiente = true;
                if (this.rbOrtese.SelectedValue == "S")
                    deficiencia.UsaOrtese = true;
                else
                    deficiencia.UsaOrtese = false;

                foreach (ListItem item in this.chckTipoDeficiencia.Items)
                {
                    if (item.Selected)
                        deficiencias.Add(new Deficiencia(int.Parse(item.Value), item.Text));
                }

                foreach (ListItem item in this.chckOrigemDeficiencia.Items)
                {
                    if (item.Selected)
                        origens.Add(new OrigemDeficiencia(int.Parse(item.Value), item.Text));
                }

                if (this.Panel_MeioLocomocaoDeficiencia.Visible)
                {
                    foreach (ListItem item in this.chckLocomocaoDeficiencia.Items)
                    {
                        if (item.Selected)
                            locomocoes.Add(new LocomocaoDeficiencia(int.Parse(item.Value), item.Text));
                    }
                }

                if (this.Panel_MeioComunicacaoDeficiencia.Visible)
                {
                    foreach (ListItem item in this.chckComunicacaoDeficiencia.Items)
                    {
                        if (item.Selected)
                            comunicacoes.Add(new ComunicacaoDeficiencia(int.Parse(item.Value), item.Text));
                    }
                }

                foreach (ListItem item in this.chckProtese.Items)
                {
                    if (item.Selected)
                        proteses.Add(new ProteseDeficiencia(int.Parse(item.Value), item.Text));
                }

                deficiencia.Proteses = proteses;
                deficiencia.Locomocoes = locomocoes;
                deficiencia.Comunicacoes = comunicacoes;
                deficiencia.Origens = origens;
                deficiencia.Deficiencias = deficiencias;
            }

            return deficiencia;
        }

        protected void chkMaeIgnorada_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMaeIgnorada.Checked == true)
            {
                tbxNomeMae.Text = "IGNORADA";
                tbxNomeMae.Enabled = false;
            }
            else
            {
                tbxNomeMae.Text = "";
                tbxNomeMae.Enabled = true;
            }
        }
        protected void chkPaiIgnorado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPaiIgnorado.Checked == true)
            {
                tbxNomePai.Text = "IGNORADO";
                tbxNomePai.Enabled = false;
            }
            else
            {
                tbxNomePai.Text = "";
                tbxNomePai.Enabled = true;
            }
        }
        protected void chkNumero_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNumero.Checked == true)
            {
                tbxNumero.Text = "S/N";
                tbxNumero.Enabled = false;
            }
            else
            {
                tbxNumero.Text = "";
                tbxNumero.Enabled = true;
            }
        }

        //protected void btnFechar_Click(object sender, EventArgs e)
        //{
        //    panelInformação.Visible = false;
        //}

    }
}