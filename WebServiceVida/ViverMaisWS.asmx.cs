using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using System.Xml.Serialization;
using ViverMais.ServiceFacade.ServiceFacades;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using Oracle.DataAccess.Client;
using System.Text;

namespace WebServiceViverMais
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://localhost/pastateste/ViverMaisWS")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    

    public class ViverMaisWS : System.Web.Services.WebService
    {
        private enum LocalConexao
        {
            ISIS,
            PRODUCAO
        }

        private readonly LocalConexao localConexao = LocalConexao.PRODUCAO;
        //[WebMethod]
        //public string HelloWorld()
        //{
        //    return "Hello World";
        //}
        
        private string GetStringConexao()
        {
            switch (localConexao)
            {
                case LocalConexao.ISIS:
                    return "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.22.6.20)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ViverMais)));User Id=ngi;Password=salvador;";
                case LocalConexao.PRODUCAO:
                    return "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.20.12.44)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ngi)));User Id=ngi;Password=#Ng1s@3De$;";
                default:
                    return "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.22.6.20)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ViverMais)));User Id=ngi;Password=salvador;";
            }
        }

        [WebMethod]        
        public Paciente PesquisarPorCNES(string numeroCNES)
        {
            Paciente paciente = Factory.GetInstance<IPaciente>().PesquisarPacientePorCNS<Paciente>(numeroCNES);
            Paciente retorno;
            
            retorno = new Paciente();
            MontarPaciente(paciente, retorno);    
            
            return retorno;
        }

        [WebMethod]
        public Paciente PesquisarPorRG(string numeroRG)
        {
            Paciente paciente = Factory.GetInstance<IPaciente>().PesquisarPacientePorRG<Paciente>(numeroRG);
            Paciente retorno;

            retorno = new Paciente();
            MontarPaciente(paciente, retorno);

            return retorno;
        }

        [WebMethod]
        public Endereco PesquisarEnderecoPorPaciente(string numeroCNES)
        {
            Paciente paciente = PesquisarPorCNES(numeroCNES);
            Endereco endereco = Factory.GetInstance<IEndereco>().BuscarPorPaciente<Endereco>(paciente.Codigo);
            ControleEndereco controle = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ControleEndereco>(endereco.Codigo);
            endereco.ControleEndereco = controle;

            Endereco retorno;

            retorno = new Endereco();
            MontarEndereco(endereco, retorno);

            return retorno;
        }

        [WebMethod]
        public Documento[] PesquisarDocumentoPorPaciente(string numeroCNES)
        {
            OracleConnection connection = new OracleConnection();
            connection.ConnectionString = GetStringConexao();
            OracleCommand command = connection.CreateCommand();
            StringBuilder sql = new StringBuilder();
            sql.Append("select * ");
            sql.Append("from rl_ms_usuario_documentos rd ");
            sql.Append("inner join tb_ms_usuario_cns_elos ce on ce.co_usuario = rd.co_usuario ");
            sql.Append("inner join tb_ms_usuario u on u.co_usuario = ce.co_usuario ");
            sql.Append("inner join tb_ms_tipo_documento td on td.co_tipo_documento = rd.co_tipo_documento ");
            sql.Append("where ce.co_numero_cartao = :CNES");
                        

            
            OracleDataReader reader = null;
            List<Documento> documentos = new List<Documento>();
            try
            {
                connection.Open();
                command.CommandText = sql.ToString();
                command.Parameters.Add("CNES", OracleDbType.Varchar2).Value = numeroCNES;
                command.Prepare();

                reader = command.ExecuteReader();

                Documento documento;
                bool primeiro = true;
                Paciente paciente = null;
                while (reader.Read())
                {
                    //Armengue
                    if (primeiro)
                    {
                        paciente = PesquisarPorCodigo(Convert.ToString(reader["co_usuario"]));
                    }
                    documento = new Documento();
                    MontarDocumento(documento, reader, paciente);
                    documentos.Add(documento);
                }
            }
            catch (Exception ex)
            {
                string oi = ex.Message;
            }
            finally
            {
                if (!reader.IsClosed)
                    reader.Close();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return documentos.ToArray();
        }        

        [WebMethod]
        public Paciente[] Pesquisar(string nome, string nomeMae, DateTime dataNascimento)
        {
            List<Paciente> pacientes = Factory.GetInstance<IPaciente>().PesquisarPaciente<Paciente>(nome, nomeMae, dataNascimento).ToList();
            List<Paciente> retornos = new List<Paciente>();
            Paciente pa;
            foreach (Paciente p in pacientes)
            {
                pa = new Paciente();
                try
                {
                    MontarPaciente(p, pa);
                    retornos.Add(pa);
                }
                catch (Exception)
                {
                    break;
                }
            }
            return retornos.ToArray();
        }

        [WebMethod]
        public string[] PesquisarCartoesAtualizados()
        {
            DateTime dataInicial = DateTime.Now;
            //DateTime dataFinal = DateTime.Now.AddDays(1);
            
            IList<string> controles = Factory.GetInstance<IPaciente>().BuscarCodigoUsuarioPorModificacaoDoDia<string>(dataInicial);

            return controles.ToArray<string>();
        }

        private Paciente PesquisarPorCodigo(string codigo)
        {
            Paciente paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<Paciente>(codigo);
            Paciente retorno;

            retorno = new Paciente();
            MontarPaciente(paciente, retorno);

            return retorno;
        }

        private void MontarPaciente(Paciente pacientePesquisado, Paciente pacienteRetorno)
        {
            try
            {
                pacienteRetorno.Nome = pacientePesquisado.Nome;
                pacienteRetorno.Codigo = pacientePesquisado.Codigo;
                pacienteRetorno.DataInclusaoRegistro = pacientePesquisado.DataInclusaoRegistro;
                pacienteRetorno.DataNascimento = pacientePesquisado.DataNascimento;
                pacienteRetorno.DataPreenchimentoFormulario = pacientePesquisado.DataPreenchimentoFormulario;
                pacienteRetorno.Email = pacientePesquisado.Email;
                pacienteRetorno.FrequentaEscola = pacientePesquisado.FrequentaEscola;
                pacienteRetorno.SemDocumento = pacientePesquisado.SemDocumento;
                pacienteRetorno.Sexo = pacientePesquisado.Sexo;
                pacienteRetorno.Vivo = pacientePesquisado.Vivo;
                pacienteRetorno.NomeMae = pacientePesquisado.NomeMae;
                pacienteRetorno.NomePai = pacientePesquisado.NomePai;
                

                pacienteRetorno.RacaCor = new RacaCor();
                pacienteRetorno.RacaCor.Codigo = pacientePesquisado.RacaCor.Codigo;
                pacienteRetorno.RacaCor.Descricao = pacientePesquisado.RacaCor.Descricao;

                pacienteRetorno.Pais = new Pais();
                pacienteRetorno.Pais.Codigo = pacientePesquisado.Pais.Codigo;
                pacienteRetorno.Pais.Nome = pacientePesquisado.Pais.Nome;

                pacienteRetorno.MunicipioNascimento = new Municipio();
                pacienteRetorno.MunicipioNascimento.Codigo = pacientePesquisado.MunicipioNascimento.Codigo;
                pacienteRetorno.MunicipioNascimento.Nome = pacientePesquisado.MunicipioNascimento.Nome;

                pacienteRetorno.MunicipioNascimento.UF = new UF();
                pacienteRetorno.MunicipioNascimento.UF.Codigo = pacientePesquisado.MunicipioNascimento.UF.Codigo;
                pacienteRetorno.MunicipioNascimento.UF.Nome = pacientePesquisado.MunicipioNascimento.UF.Nome;
                pacienteRetorno.MunicipioNascimento.UF.Sigla = pacientePesquisado.MunicipioNascimento.UF.Sigla;

                pacienteRetorno.EstadoCivil = new EstadoCivil();
                pacienteRetorno.EstadoCivil.Codigo = pacientePesquisado.EstadoCivil.Codigo;
                pacienteRetorno.EstadoCivil.DataAtivacaoDesativacao = pacientePesquisado.EstadoCivil.DataAtivacaoDesativacao;
                pacienteRetorno.EstadoCivil.Descricao = pacientePesquisado.EstadoCivil.Descricao;
                pacienteRetorno.EstadoCivil.SituacaoAtivacao = pacientePesquisado.EstadoCivil.SituacaoAtivacao;

                pacienteRetorno.MunicipioResidencia = new Municipio();
                pacienteRetorno.MunicipioResidencia.Codigo = pacientePesquisado.MunicipioResidencia.Codigo;
                pacienteRetorno.MunicipioResidencia.Nome = pacientePesquisado.MunicipioResidencia.Nome;

                pacienteRetorno.MunicipioResidencia.UF = new UF();
                pacienteRetorno.MunicipioResidencia.UF.Codigo = pacientePesquisado.MunicipioResidencia.UF.Codigo;
                pacienteRetorno.MunicipioResidencia.UF.Nome = pacientePesquisado.MunicipioResidencia.UF.Nome;
                pacienteRetorno.MunicipioResidencia.UF.Sigla = pacientePesquisado.MunicipioResidencia.UF.Sigla;
            }
            catch (NullReferenceException)
            {
                pacienteRetorno = null;
            }
        }

        private void MontarEndereco(Endereco endereco, Endereco retorno)
        {
            try
            {
                retorno.Bairro = endereco.Bairro;
                retorno.CEP = endereco.CEP;                
                retorno.Complemento = endereco.Complemento;

                retorno.ControleEndereco = new ControleEndereco();
                retorno.ControleEndereco.Codigo = endereco.ControleEndereco.Codigo;
                retorno.ControleEndereco.CO_ORIGEM = endereco.ControleEndereco.CO_ORIGEM;
                retorno.ControleEndereco.CO_TIPO_ENDERECO = endereco.ControleEndereco.CO_TIPO_ENDERECO;
                retorno.ControleEndereco.DT_OPERACAO = endereco.ControleEndereco.DT_OPERACAO;
                retorno.ControleEndereco.FL_ERROS = endereco.ControleEndereco.FL_ERROS;
                retorno.ControleEndereco.NU_VERSAO = endereco.ControleEndereco.NU_VERSAO;
                retorno.ControleEndereco.ST_ATIVO = endereco.ControleEndereco.ST_ATIVO;
                retorno.ControleEndereco.ST_CONTROLE = endereco.ControleEndereco.ST_CONTROLE;
                retorno.ControleEndereco.ST_EXCLUIDO = endereco.ControleEndereco.ST_EXCLUIDO;
                retorno.ControleEndereco.ST_JA_RETORNOU = endereco.ControleEndereco.ST_JA_RETORNOU;
                retorno.ControleEndereco.ST_VINCULO = endereco.ControleEndereco.ST_VINCULO;

                retorno.DDD = endereco.DDD;
                retorno.Logradouro = endereco.Logradouro;
                
                retorno.Municipio = new Municipio();
                retorno.Municipio.Codigo = endereco.Municipio.Codigo;
                retorno.Municipio.Nome = retorno.Municipio.Nome;                
                retorno.Municipio.UF = new UF();
                retorno.Municipio.UF.Codigo = endereco.Municipio.UF.Codigo;
                retorno.Municipio.UF.Nome = endereco.Municipio.UF.Nome;
                retorno.Municipio.UF.Sigla = endereco.Municipio.UF.Sigla;

                retorno.Numero = endereco.Numero;
                retorno.Telefone = endereco.Telefone;
                retorno.TipoLogradouro = new TipoLogradouro();
                retorno.TipoLogradouro.Codigo = endereco.TipoLogradouro.Codigo;
                retorno.TipoLogradouro.Descricao = endereco.TipoLogradouro.Descricao;
                retorno.TipoLogradouro.Abreviatura = endereco.TipoLogradouro.Abreviatura;                    
            }
            catch (Exception)
            {
                retorno = null;
            }
        }

        private void MontarDocumento(Documento documento, OracleDataReader reader, Paciente paciente)
        {
            try
            {
                //Certidao
                documento.NumeroLivro = Convert.ToString(reader["NU_LIVRO"]);
                documento.NumeroFolha = Convert.ToString(reader["NU_FOLHA"]);
                documento.NumeroTermo = Convert.ToString(reader["NU_TERMO"]);
                try
                {
                    documento.DataEmissao = Convert.ToDateTime(reader["DT_EMISSAO"]);
                }
                catch (InvalidCastException)
                {
                    documento.DataEmissao = DateTime.MinValue;
                }
                documento.NomeCartorio = Convert.ToString(reader["NO_CARTORIO"]);

                //Estrangeiro
                try
                {
                    documento.DataChegadaBrasil = Convert.ToDateTime(reader["DT_CHEGADA_BRASIL"]);
                }
                catch (InvalidCastException)
                {
                    documento.DataChegadaBrasil = DateTime.MinValue;
                }
                documento.NumeroPortaria = Convert.ToString(reader["NU_PORTARIA"]);
                try
                {
                    documento.DataNaturalizacao = Convert.ToDateTime(reader["DT_NATURALIZACAO"]);
                }
                catch (InvalidCastException)
                {
                    documento.DataNaturalizacao = DateTime.MinValue;
                }





                //Comum
                documento.Numero = Convert.ToString(reader["NU_DOCUMENTO"]);
                documento.Complemento = Convert.ToString(reader["NU_DOCUMENTO_COMPL"]);



                documento.OrgaoEmissor = new OrgaoEmissor();
                //documento.OrgaoEmissor.Codigo = documento.OrgaoEmissor.Codigo;
                //dodocumentoc.OrgaoEmissor.Nome = documento.OrgaoEmissor.Nome;
                //documento.OrgaoEmissor.CategoriaOcupacao = new CategoriaOcupacao();
                //documento.OrgaoEmissor.CategoriaOcupacao.Codigo = documento.OrgaoEmissor.CategoriaOcupacao.Codigo;
                //documento.OrgaoEmissor.CategoriaOcupacao.Nome = documento.OrgaoEmissor.CategoriaOcupacao.Nome;


                //Titulo
                documento.Serie = Convert.ToString(reader["NU_SERIE"]);
                documento.ZonaEleitoral = Convert.ToString(reader["NU_ZONA_ELEITORAL"]);
                documento.SecaoEleitoral = Convert.ToString(reader["NU_SECAO_ELEITORAL"]);


                //UF documento
                documento.UF = new UF();
                //documento.UF.Codigo = documento.UF.Codigo;
                //documento.UF.Nome = documento.UF.Nome;
                documento.UF.Sigla = Convert.ToString(reader["SG_UF"]);


                //controle documento
                documento.ControleDocumento = new ControleDocumento();

                //Paciente
                documento.ControleDocumento.Paciente = paciente;

                //Tipo documento                
                documento.ControleDocumento.TipoDocumento = new TipoDocumento();
                documento.ControleDocumento.TipoDocumento.Codigo = Convert.ToString(reader["CO_TIPO_DOCUMENTO"]);
                documento.ControleDocumento.TipoDocumento.Descricao = Convert.ToString(reader["DS_TIPO_DOCUMENTO"]);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
