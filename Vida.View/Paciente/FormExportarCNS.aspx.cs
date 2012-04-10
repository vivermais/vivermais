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
using System.Xml;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades;
using System.IO;
using System.Text;

namespace ViverMais.View.Paciente
{
    public partial class FormExportarCNS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
                if (!iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "EXPORTAR_CNS", Modulo.CARTAO_SUS))
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Você não tem permissão para acessar essa página. Em caso de dúViverMais, entre em contato.');window.location='../Home.aspx';</script>");
                }
            }
        }

        protected void lnkBtnDownload_Click(object sender, EventArgs e)
        {
            IList<ControlePaciente> cps = Factory.GetInstance<IPaciente>().BuscarPorModificacao<ControlePaciente>(
                DateTime.Parse(tbxDataInicial.Text), DateTime.Parse(tbxDataFinal.Text));

            List<String> municipios = new List<string>();

            XmlDocument doc = new XmlDocument();
            XmlNode cabecalho = doc.CreateXmlDeclaration("1.0", "ISO-8859-1", string.Empty);
            doc.AppendChild(cabecalho);
            
            XmlElement root = doc.CreateElement("ROOT");
            doc.AppendChild(root);
            XmlElement dados = doc.CreateElement("DADOS");
            dados.SetAttribute("TOTAL_ENDERECOS", cps.Count.ToString());
            root.AppendChild(dados);
            XmlElement segundaVia = doc.CreateElement("SEGUNDA_VIA");
            dados.AppendChild(segundaVia);
            XmlElement cadastros = doc.CreateElement("CADASTROS");
            dados.AppendChild(cadastros);
            foreach (var item in cps)
            {
                Endereco enderecoPaciente = Factory.GetInstance<IEndereco>().BuscarPorPaciente<Endereco>(item.Codigo);
                ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(item.Codigo);

                if (enderecoPaciente != null)
                {
                    XmlElement endereco = doc.CreateElement("ENDERECO");
                    endereco.SetAttribute("CO_ENDERECO", enderecoPaciente.Codigo);
                    endereco.SetAttribute("CO_MUNICIPIO", enderecoPaciente.Municipio.Codigo);
                    if (!municipios.Contains(enderecoPaciente.Municipio.Codigo))//Se não existir na Lista o municipio Informado
                        municipios.Add(enderecoPaciente.Municipio.Codigo);//Adiciono o Municipio dos Pacientes numa Lista que será utilizada no final do documento
                    endereco.SetAttribute("CO_TIPO_LOGRADOURO", enderecoPaciente.TipoLogradouro.Codigo);
                    endereco.SetAttribute("NO_LOGRADOURO", enderecoPaciente.Logradouro);
                    endereco.SetAttribute("NU_LOGRADOURO", enderecoPaciente.Numero);
                    endereco.SetAttribute("NO_COMPL_LOGRADOURO", enderecoPaciente.Complemento);
                    endereco.SetAttribute("NO_BAIRRO", enderecoPaciente.Bairro);
                    endereco.SetAttribute("CO_CEP", enderecoPaciente.CEP);
                    endereco.SetAttribute("NU_DDD", enderecoPaciente.DDD);
                    endereco.SetAttribute("NU_TELEFONE", enderecoPaciente.Telefone);
                    endereco.SetAttribute("DS_USO_MUNICIPAL", string.Empty);
                    endereco.SetAttribute("NU_DDD_FAX", string.Empty);
                    endereco.SetAttribute("NU_FAX", string.Empty);
                    ControleEndereco control = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ControleEndereco>(enderecoPaciente.ControleEndereco.Codigo);
                    endereco.SetAttribute("DT_OPERACAO", control.DT_OPERACAO.ToString("dd/MM/yyyy HH:mm:ss"));
                    endereco.SetAttribute("NU_VERSAO", "0");
                    endereco.SetAttribute("CO_ORIGEM", "004");
                    endereco.SetAttribute("ST_CONTROLE", "I");
                    endereco.SetAttribute("ST_EXCLUIDO", control.ST_EXCLUIDO == '\0' ? "0" : control.ST_EXCLUIDO.ToString());
                    endereco.SetAttribute("ST_ATIVO", control.ST_ATIVO == '\0' ? "1" : control.ST_ATIVO.ToString());
                    endereco.SetAttribute("FL_ERROS", "0");
                    endereco.SetAttribute("ST_JA_RETORNOU", "0");
                    cadastros.AppendChild(endereco);
                    XmlElement grupo_elos_end = doc.CreateElement("GRUPO_ELOS_END");
                    endereco.AppendChild(grupo_elos_end);

                    XmlElement elos_end = doc.CreateElement("ELOS_END");
                    elos_end.SetAttribute("CO_ENDERECO_INFORMADO", enderecoPaciente.Codigo);
                    grupo_elos_end.AppendChild(elos_end);

                    XmlElement grupo_elos_end_fed = doc.CreateElement("GRUPO_ELOS_END_FED");
                    endereco.AppendChild(grupo_elos_end_fed);

                    //XmlElement elos_end_fed = doc.CreateElement("ELOS_END_FED");
                    //grupo_elos_end_fed.AppendChild(elos_end_fed);


                    XmlElement usuarios = doc.CreateElement("USUARIOS");
                    endereco.AppendChild(usuarios);
                    XmlElement usuario = doc.CreateElement("USUARIO");
                    usuarios.AppendChild(usuario);
                    usuario.SetAttribute("CO_USUARIO", paciente.Codigo);
                    usuario.SetAttribute("NO_USUARIO", paciente.Nome);
                    usuario.SetAttribute("DT_NASCIMENTO", paciente.DataNascimento.ToString("dd/MM/yyyy"));
                    usuario.SetAttribute("CO_MUNICIPIO_NASC", paciente.MunicipioNascimento == null ? "" : paciente.MunicipioNascimento.Codigo);
                    usuario.SetAttribute("NO_MAE", paciente.NomeMae == null ? "IGNORADA" : paciente.NomeMae);
                    usuario.SetAttribute("NO_PAI", paciente.NomePai == null ? "IGNORADO" : paciente.NomePai);
                    usuario.SetAttribute("ST_PROFISSIONAL", "0");
                    usuario.SetAttribute("ST_FREQUENTA_ESCOLA", paciente.ST_FREQUENTA_ESCOLA == '\0' ? string.Empty : paciente.ST_FREQUENTA_ESCOLA.ToString());
                    usuario.SetAttribute("DS_EMAIL", paciente.Email);
                    usuario.SetAttribute("DS_USO_MUNICIPAL", string.Empty);
                    usuario.SetAttribute("CO_RACA", paciente.RacaCor.Codigo);
                    usuario.SetAttribute("CO_ESTADO_CIVIL", "9");
                    usuario.SetAttribute("CO_SITUACAO_FAMILIAR", string.Empty);
                    usuario.SetAttribute("CO_PAIS", paciente.Pais == null ? "010" : paciente.Pais.Codigo);
                    usuario.SetAttribute("CO_SGRP_CBO", string.Empty);
                    usuario.SetAttribute("CO_ESCOLARIDADE", string.Empty);
                    usuario.SetAttribute("NU_DDD", enderecoPaciente.DDD);
                    usuario.SetAttribute("NU_TELEFONE", enderecoPaciente.Telefone);
                    usuario.SetAttribute("NU_DDD_2", string.Empty);
                    usuario.SetAttribute("NU_TELEFONE_2", string.Empty);
                    usuario.SetAttribute("CO_SEXO", paciente.Sexo.ToString());
                    usuario.SetAttribute("DT_INCLUSAO", paciente.DataInclusaoRegistro.ToString("dd/MM/yyyy HH:mm:ss"));
                    usuario.SetAttribute("DT_PREENCHIMENTO_FORM", paciente.DataPreenchimentoFormulario.ToString("dd/MM/yyyy"));
                    usuario.SetAttribute("CO_MUNICIPIO_RESIDENCIA", paciente.MunicipioResidencia == null ? "292740" : paciente.MunicipioResidencia.Codigo);
                    usuario.SetAttribute("ST_SEM_DOCUMENTO", paciente.SemDocumento == '\0' ? "0" : paciente.SemDocumento.ToString());
                    usuario.SetAttribute("NU_USUARIO_NO_DOMICILIO", string.Empty);
                    usuario.SetAttribute("ST_CONTROLE", "I");
                    usuario.SetAttribute("ST_EXCLUIDO", item.Excluido == '\0' ? "0" : item.Excluido.ToString());
                    usuario.SetAttribute("DT_OPERACAO", item.DataOperacao.ToString("dd/MM/yyyy HH:mm:ss"));
                    usuario.SetAttribute("NU_VERSAO", item.NumeroVersao.ToString());
                    usuario.SetAttribute("CO_LOTE", string.Empty);
                    usuario.SetAttribute("CO_DOMICILIO", string.Empty);
                    usuario.SetAttribute("ST_VIVO", paciente.Vivo.ToString());
                    usuario.SetAttribute("FL_ERROS", "0");
                    usuario.SetAttribute("ST_CONFIRMACAO_HOMONIMO", "0");
                    usuario.SetAttribute("ST_ENVIO_CEF", "0");

                    XmlElement grupo_elos_usu = doc.CreateElement("GRUPO_ELOS_USU");
                    usuario.AppendChild(grupo_elos_usu);

                    XmlElement elos_usu = doc.CreateElement("ELOS_USU");
                    elos_usu.SetAttribute("CO_USUARIO_INFORMADO", paciente.Codigo);
                    grupo_elos_usu.AppendChild(elos_usu);

                    XmlElement grupo_elos_usu_fed = doc.CreateElement("GRUPO_ELOS_USU_FED");
                    usuario.AppendChild(grupo_elos_usu_fed);

                    XmlElement grupo_documento = doc.CreateElement("GRUPO_DOCUMENTO");
                    usuario.AppendChild(grupo_documento);

                    IList<Documento> documentos = Factory.GetInstance<IPaciente>().ListarDocumentos<Documento>(paciente.Codigo);

                    foreach (var docs in documentos)//Documentos do Usuario
                    {
                        XmlElement documento = doc.CreateElement("DOCUMENTO");
                        documento.SetAttribute("CO_USUARIO", paciente.Codigo);
                        documento.SetAttribute("CO_TIPO_DOCUMENTO", docs.ControleDocumento.TipoDocumento.Codigo);
                        documento.SetAttribute("CO_ORGAO_EMISSOR", docs.OrgaoEmissor == null ? string.Empty : docs.OrgaoEmissor.Codigo);
                        documento.SetAttribute("NO_CARTORIO", docs.NomeCartorio);
                        documento.SetAttribute("NU_LIVRO", docs.NumeroLivro);
                        documento.SetAttribute("NU_FOLHA", docs.NumeroFolha);
                        documento.SetAttribute("NU_TERMO", docs.NumeroTermo);
                        documento.SetAttribute("DT_EMISSAO", docs.DataEmissao == null ? string.Empty : docs.DataEmissao.Value.ToString("dd/MM/yyyy"));
                        documento.SetAttribute("DT_CHEGADA_BRASIL", docs.DataChegadaBrasil == null ? String.Empty : docs.DataChegadaBrasil.Value.ToString("dd/MM/yyyy"));
                        documento.SetAttribute("NU_PORTARIA", docs.NumeroPortaria);
                        documento.SetAttribute("DT_NATURALIZACAO", docs.DataNaturalizacao == null ? String.Empty : docs.DataNaturalizacao.Value.ToString("dd/MM/yyyy"));
                        documento.SetAttribute("NU_DOCUMENTO", docs.Numero);
                        documento.SetAttribute("NU_DOCUMENTO_COMPL", docs.Complemento);
                        documento.SetAttribute("NU_SERIE", docs.Serie);
                        documento.SetAttribute("NU_ZONA_ELEITORAL", docs.ZonaEleitoral);
                        documento.SetAttribute("NU_SECAO_ELEITORAL", docs.SecaoEleitoral);
                        documento.SetAttribute("SG_UF", docs.UF == null ? string.Empty : docs.UF.Sigla);
                        documento.SetAttribute("ST_CONTROLE","I");
                        documento.SetAttribute("ST_EXCLUIDO", docs.ControleDocumento.Excluido == '\0' ? "0" : docs.ControleDocumento.Excluido.ToString());
                        documento.SetAttribute("DT_OPERACAO", docs.ControleDocumento.DataOperacao.ToString("dd/MM/yyyy"));
                        documento.SetAttribute("NU_VERSAO", "0");
                        documento.SetAttribute("FL_ERROS", "0");
                        grupo_documento.AppendChild(documento);
                    }
                    XmlElement grupo_cns = doc.CreateElement("GRUPO_CNS");
                    usuario.AppendChild(grupo_cns);


                    IList<CartaoSUS> cartoes = Factory.GetInstance<IPaciente>().ListarCartoesSUS<CartaoSUS>(paciente.Codigo);
                    foreach (var cartao in cartoes)
                    {
                        XmlElement cns = doc.CreateElement("CNS");
                        grupo_cns.AppendChild(cns);
                        cns.SetAttribute("CO_NUMERO_CARTAO", cartao.Numero);
                        cns.SetAttribute("CO_USUARIO", cartao.Paciente.Codigo);
                        cns.SetAttribute("TP_CARTAO", cartao.Tipo.ToString());
                        cns.SetAttribute("DT_ATRIBUICAO", cartao.DataAtribuicao.Value.ToString("dd/MM/yyyy HH:mm:ss"));
                        cns.SetAttribute("ST_CONTROLE", "I");
                        cns.SetAttribute("ST_EXCLUIDO", cartao.Excluido == '\0' ? "0" : cartao.Excluido.ToString());
                        cns.SetAttribute("DT_OPERACAO", cartao.DataOperacao.ToString("dd/MM/yyyy HH:mm:ss"));
                        cns.SetAttribute("NU_VERSAO", "0");
                        cns.SetAttribute("FL_ERROS", "0");
                    }

                    XmlElement grupo_pis = doc.CreateElement("GRUPO_PIS");
                    usuario.AppendChild(grupo_pis);

                    XmlElement grupo_origem = doc.CreateElement("GRUPO_ORIGEM");
                    usuario.AppendChild(grupo_origem);

                    XmlElement grupo_motivo = doc.CreateElement("GRUPO_MOTIVO");
                    usuario.AppendChild(grupo_motivo);

                    //Campos não Obrigatórios
                    //MotivoCadastroPaciente motivoCadastroPaciente = Factory.GetInstance<IPaciente>().BuscarMotivo<MotivoCadastroPaciente>(paciente.Codigo);
                    //grupo_motivo.SetAttribute("CO_MOTIVO", motivoCadastroPaciente.Motivo.Codigo.ToString());
                    //grupo_motivo.SetAttribute("CO_USUARIO", motivoCadastroPaciente.Paciente.Codigo);
                    //grupo_motivo.SetAttribute("CO_CNES", motivoCadastroPaciente.Cnes);
                    //grupo_motivo.SetAttribute("CO_DOC_REF", motivoCadastroPaciente.CodigoDocumentoReferencia.ToString());
                    //grupo_motivo.SetAttribute("CO_NUMERO_CARTAO_MAE", motivoCadastroPaciente.CnsMae);
                    //grupo_motivo.SetAttribute("NU_DOCUMENTO", motivoCadastroPaciente.CodigoDocumentoReferencia.ToString());
                    //grupo_motivo.SetAttribute("DT_OPERACAO", motivoCadastroPaciente.DataOperacao.ToString("dd/MM/yyyy HH:mm:ss"));
                    //grupo_motivo.SetAttribute("ST_CONTROLE", motivoCadastroPaciente.Controle);
                    //grupo_motivo.SetAttribute("ST_EXCLUIDO", motivoCadastroPaciente.Excluido.ToString());
                    //grupo_motivo.SetAttribute("NU_VERSAO", "0");
                    //grupo_motivo.SetAttribute("FL_ERROS", "0");

                    XmlElement grupo_rlendereco = doc.CreateElement("GRUPO_RLENDERECO");
                    usuario.AppendChild(grupo_rlendereco);

                    XmlElement rlendereco = doc.CreateElement("RLENDERECO");
                    rlendereco.SetAttribute("CO_ENDERECO", enderecoPaciente.Codigo);
                    rlendereco.SetAttribute("CO_USUARIO", paciente.Codigo);
                    rlendereco.SetAttribute("CO_BANCO", "ORACLE");
                    rlendereco.SetAttribute("CO_TIPO_ENDERECO", "01");
                    rlendereco.SetAttribute("ST_CONTROLE", "I");
                    rlendereco.SetAttribute("ST_EXCLUIDO", enderecoPaciente.ControleEndereco.ST_EXCLUIDO == '\0' ? "0" : enderecoPaciente.ControleEndereco.ST_EXCLUIDO.ToString());
                    rlendereco.SetAttribute("DT_OPERACAO", enderecoPaciente.ControleEndereco.DT_OPERACAO.ToString("dd/MM/yyyy HH:mm:ss"));
                    rlendereco.SetAttribute("NU_VERSAO", "0");
                    rlendereco.SetAttribute("ST_VINCULO", "1");
                    grupo_rlendereco.AppendChild(rlendereco);
                }
            }
            XmlElement sumario = doc.CreateElement("SUMARIO");
            root.AppendChild(sumario);

            XmlElement totais = doc.CreateElement("TOTAIS");
            sumario.AppendChild(totais);

            totais.SetAttribute("REGISTROS", (cps.Count * 2).ToString());//Somatorio de endereços, domicilios e usuarios
            totais.SetAttribute("ENDERECOS", cps.Count.ToString()); //Definido com Maurílio, pois no ViverMais o paciente possui um endereço. Relação 1x1
            totais.SetAttribute("DOMICILIOS", "0");
            totais.SetAttribute("USUARIOS", cps.Count.ToString());

            XmlElement identificacao = doc.CreateElement("IDENTIFICACAO");
            sumario.AppendChild(identificacao);
            identificacao.SetAttribute("CO_BANCO", "ORACLE");
            identificacao.SetAttribute("ORIGEM", "004");
            identificacao.SetAttribute("DATA", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            identificacao.SetAttribute("ARQUIVO", "I292740" + DateTime.Now.ToString("yyyyMMddHHmm") + "12345SISTEMAS-TERCEIROS.xml");

            XmlElement grupo_origem_banco = doc.CreateElement("GRUPO_ORIGEM_BANCO");
            sumario.AppendChild(grupo_origem_banco);

            XmlElement origem_banco = doc.CreateElement("ORIGEM_BANCO");
            grupo_origem_banco.AppendChild(origem_banco);

            origem_banco.SetAttribute("VALOR", "004");

            XmlElement grupo_ibge = doc.CreateElement("GRUPO_IBGE");
            sumario.AppendChild(grupo_ibge);


            foreach (var municipio in municipios)
            {
                XmlElement ibge = doc.CreateElement("IBGE");
                grupo_ibge.AppendChild(ibge);
                ibge.SetAttribute("VALOR", municipio);
            }

            XmlElement grupo_origem2 = doc.CreateElement("GRUPO_ORIGEM");
            sumario.AppendChild(grupo_origem2);

            XmlElement origens = doc.CreateElement("ORIGENS");
            grupo_origem2.AppendChild(origens);
            origens.SetAttribute("VALOR", "004");

            XmlElement grupo_versao = doc.CreateElement("GRUPO_VERSAO");
            sumario.AppendChild(grupo_versao);
            //doc.Save(DateTime.Now.ToString("yyyyMMddHHmm")+"EXPORTA_CNS_SMS-BA");


            using(MemoryStream memoryStream = new MemoryStream())
            {
                Byte[] arrayArquivo = Encoding.Default.GetBytes(doc.OuterXml);
                memoryStream.Write(arrayArquivo,0,doc.OuterXml.Length);

                memoryStream.Close();
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=I292740" + DateTime.Now.ToString("yyyyMMddHHmm") + "12345SISTEMAS-TERCEIROS.xml");
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(memoryStream.ToArray());
                Response.End();
            }
        }
    }
}
