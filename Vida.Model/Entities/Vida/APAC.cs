﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ViverMais.Model
{
    public class APAC
    {
        public APAC() 
        {
            this.DescritivoProcedimentosRealizados = new List<DescritivoProcedimentosRealizados>(10); //Quantitativo Máximo de Procedimentos Por Paciente
        }

        public String RetornaNomeFormatado(string nome, int tamanhoMax)
        {
            string nomeFormatado = nome;
            if (nomeFormatado.Length < tamanhoMax)
            {
                while (nomeFormatado.Length < tamanhoMax)
                    nomeFormatado += " ";
            }
            else if (nomeFormatado.Length == tamanhoMax)
                return nomeFormatado;
            else
                nomeFormatado.Remove(tamanhoMax);
            return nomeFormatado;
        }

        public enum TipoDeAPAC { INICIAL = 1, CONTINUIDADE = 2, UNICA = 3 }
        public enum MoticoDeSaida { ALTA_CURADO = 11, ALTA_MELHORADO = 12, ALTA_DA_PERPETUA_E_PERMANENCIA = 13, ALTA_A_PEDIDO = 14, ALTA_COM_PREVISAO_DE_RETORNO_PARA_ACOMPANHAMENTO = 15, ALTA_POR_EVASAO = 16, ALTA_DA_PERPETUA_E_RECEM_NASCIDO = 17, ALTA_POR_OUTROS_MOTIVO = 18}
        public enum CaraterDoAtendimento { ELETIVO = 1, URGENCIA = 2, ACIDENTE_NO_TRABALHO = 3, ACIDENTE_TRAJETO_TRABALHO = 4, OUTROS_TIPOS_ACIDENTE_TRANSITO = 5, OUTRO_TIPO_LESAO_ENVENENAMENTO = 6}
        
        /// <summary>
        /// Código da Unidade da Federação (IBGE)
        /// </summary>
        private UF uf;
        public UF Uf
        {
            get { return uf; }
            set { uf = value; }
        }

        /// <summary>
        /// Código da Unidade Prestadora de Serviços  (c/ dígito verificador) - Unidade Executante
        /// </summary>
        private EstabelecimentoSaude unidadePrestadora;
        public virtual EstabelecimentoSaude UnidadePrestadora
        {
            get { return unidadePrestadora; }
            set { unidadePrestadora = value; }
        }

        private Paciente paciente;
        public virtual Paciente Paciente
        {
            get { return paciente; }
            set { paciente = value; }
        }

        private CartaoSUS cartaoSUSPaciente;
        public CartaoSUS CartaoSUSPaciente
        {
            get { return cartaoSUSPaciente; }
            set { cartaoSUSPaciente = value; }
        }

        private int indicativoContinuacaoAPAC;
        /// <summary>
        /// Indicador de continuação da APAC (vide observação)
        /// </summary>
        public virtual int IndicativoContinuacaoAPAC
        {
            get { return indicativoContinuacaoAPAC; }
            set { indicativoContinuacaoAPAC = value; }
        }

        /// <summary>
        /// Número da APAC (12 dígitos para sequencia e 1 para dígito verificador)
        /// </summary>
        private string numeroAPAC;
        public virtual string NumeroAPAC
        {
            get { return numeroAPAC; }
            set { numeroAPAC = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private DateTime dataProcessamentoAPAC;
        public virtual DateTime DataProcessamentoAPAC
        {
            get { return dataProcessamentoAPAC; }
            set { dataProcessamentoAPAC = value; }
        }

        private DateTime dataValidadeInicial;
        public virtual DateTime DataValidadeInicial
        {
            get { return dataValidadeInicial; }
            set { dataValidadeInicial = value; }
        }

        private DateTime dataValidadeFinal;
        public virtual DateTime DataValidadeFinal
        {
            get { return dataValidadeFinal; }
            set { dataValidadeFinal = value; }
        }

        private string tipoAtendimento;
        public virtual string TipoAtendimento
        {
            get { return tipoAtendimento; }
            set { tipoAtendimento = value; }
        }

        private int tipoAPAC;
        public virtual int TipoAPAC
        {
            get { return tipoAPAC; }
            set { tipoAPAC = value; }
        }

        private ParteVariavelProcedimento parteVariavelProcedimento;

        public ParteVariavelProcedimento ParteVariavelProcedimento
        {
            get { return parteVariavelProcedimento; }
            set { parteVariavelProcedimento = value; }
        }

        private Endereco enderecoPaciente;
        public virtual Endereco EnderecoPaciente
        {
            get { return enderecoPaciente; }
            set { enderecoPaciente = value; }
        }

        public string LogradouroPaciente()
        {
            string logradouro = this.EnderecoPaciente.Logradouro;

            if (logradouro.Length > 30)
                logradouro = logradouro.Remove(30);
            else
                while (logradouro.Length < 30)
                    logradouro += " ";
            return logradouro;
        }

        public string NumeroRuaPaciente()
        {
            string numero = this.EnderecoPaciente.Numero;

            if (numero.Length > 5)
                numero = numero.Remove(5);
            else
                while (numero.Length < 5)
                    numero += " ";
            return numero;
        }

        public string ComplementoEnderecoPaciente()
        {
            string complemento = this.EnderecoPaciente.Complemento;

            if (complemento.Length > 10)
                complemento = complemento.Remove(10);
            else
                while (complemento.Length < 10)
                    complemento += " ";
            return complemento;
        }

        public string CodigoDoMunicipio()
        {
            string codigo = this.EnderecoPaciente.Municipio.Codigo;

            if (codigo.Length > 7)
                codigo = codigo.Remove(7);
            else
                while (codigo.Length < 7)
                    codigo += " ";
            return codigo;
        }

        //private string detalhamentoProcedimento;
        //public virtual string DetalhamentoProcedimento
        //{
        //    get { return detalhamentoProcedimento; }
        //    set { detalhamentoProcedimento = value; }
        //}

        private Profissional medicoExecutante;
        public virtual Profissional MedicoExecutante
        {
            get { return medicoExecutante; }
            set { medicoExecutante = value; }
        }

        //private DescritivoProcedimentosRealizados[] descritivoProcedimentosRealizados;
        ///// <summary>
        ///// Procedimentos, Cbo, Cids e Quantidade. Por Padrão, Deve-se definir o procedimento pricipal na posição zero
        ///// </summary>
        //public virtual DescritivoProcedimentosRealizados[] DescritivoProcedimentosRealizados
        //{
        //    get { return descritivoProcedimentosRealizados;  }
        //    set { descritivoProcedimentosRealizados = value; }
        //}

        private List<DescritivoProcedimentosRealizados> descritivoProcedimentosRealizados;

        public virtual List<DescritivoProcedimentosRealizados> DescritivoProcedimentosRealizados
        {
            get { return descritivoProcedimentosRealizados; }
            set { descritivoProcedimentosRealizados = value; }
        }        


        //public virtual ArrayList<DescritivoProcedimentosRealizados> DescritivoProcedimentosRealizados
        //{
        //    get { return descritivoProcedimentosRealizados; }
        //    set { descritivoProcedimentosRealizados = value; }
        //}

        private String cpfProfissionalAutorizador;
        public virtual String CpfProfissionalAutorizador
        {
            get { return cpfProfissionalAutorizador; }
            set { cpfProfissionalAutorizador = value; }
        }

        private String cpfProfissionalExecutante;
        public virtual String CpfProfissionalExecutante
        {
            get { return cpfProfissionalExecutante; }
            set { cpfProfissionalExecutante = value; }
        }

        private string nomeProfissionalExecutante;
        public virtual string NomeProfissionalExecutante
        {
            get { return nomeProfissionalExecutante; }
            set { nomeProfissionalExecutante = value; }
        }
        
        private String nomeProfissionalAutorizador;
        public virtual String NomeProfissionalAutorizador
        {
            get 
            {
                string nomeProf = nomeProfissionalAutorizador;
                while (nomeProf.Length < 30)
                    nomeProf = nomeProf + " ";
                if (nomeProf.Length > 30)
                    nomeProf = nomeProf.Remove(30);
                return nomeProf;
            }
            set { nomeProfissionalAutorizador = value; }
        }

        private string cnsPaciente;
        public string CnsPaciente
        {
            get { return cnsPaciente; }
            set { cnsPaciente = value; }
        }

        private string cnsMedicoResponsavel;
        public virtual string CnsMedicoResponsavel
        {
            get { return cnsMedicoResponsavel; }
            set { cnsMedicoResponsavel = value; }
        }
        
        private string cnsAutorizadorResponsavel;
        /// <summary>
        /// Cartão sus do Médico Regulador
        /// </summary>
        public virtual string CnsAutorizadorResponsavel
        {
            get { return cnsAutorizadorResponsavel; }
            set { cnsAutorizadorResponsavel = value; }
        }

        private string caraterAtendimento;
        public virtual string CaraterAtendimento
        {
            get { return caraterAtendimento; }
            set { caraterAtendimento = value; }
        }

        private Cid cidCausasAssociadas;
        public Cid CidCausasAssociadas
        {
            get { return cidCausasAssociadas; }
            set { cidCausasAssociadas = value; }
        }
        
        private string numeroProntuario;
        public string NumeroProntuario
        {
            get { return numeroProntuario; }
            set { numeroProntuario = value; }
        }

        private string cnesUnidadeSolicitante;
        public string CnesUnidadeSolicitante
        {
            get { return cnesUnidadeSolicitante; }
            set { cnesUnidadeSolicitante = value; }
        }

        private DateTime dataSolicitacao;
        public DateTime DataSolicitacao
        {
            get { return dataSolicitacao; }
            set { dataSolicitacao = value; }
        }

        private DateTime dataAutorizacao;
        public DateTime DataAutorizacao
        {
            get { return dataAutorizacao; }
            set { dataAutorizacao = value; }
        }

        private const string codigoDoEmissor = "M292740801";
        public string CodigoDoEmissor
        {
            get { return codigoDoEmissor; }
        }

        private string numeroApacAnterior;
        public string NumeroApacAnterior
        {
            get { return numeroApacAnterior; }
            set { numeroApacAnterior = value; }
        }

        //private RacaCor racaCor;
        //public RacaCor RacaCor
        //{
        //    get { return racaCor; }
        //    set { racaCor = value; }
        //}

        private int motivoSaida;

        public int MotivoSaida
        {
            get { return motivoSaida; }
            set { motivoSaida = value; }
        }
        
        private string nomeResponsavel;
        /// <summary>
        /// Nome do Responsável pelo Paciente. Se o paciente for maior de Idade, pode conter o nome do paciente. Se menor de idade, pode inserir nome da Mãe ou outro responsável
        /// </summary>
        public string NomeResponsavel
        {
            get { return nomeResponsavel; }
            set { nomeResponsavel = value; }
        }

        //private Pais pais;
        //public virtual Pais Pais
        //{
        //    get { return pais; }
        //    set { pais = value; }
        //}

        private DateTime dataAltaOuObito;
        public virtual DateTime DataAltaOuObito
        {
            get { return dataAltaOuObito; }
            set { dataAltaOuObito = value; }
        }

        public string NomePaciente()
        {
            string nomepaciente = this.paciente.Nome;

            if (nomepaciente.Length > 30)
                nomepaciente = nomepaciente.Remove(30);
            else
            {
                while (nomepaciente.Length != 30)
                    nomepaciente += " ";
            }

            return nomepaciente;
        }

        public string NomeMaePaciente()
        {
            string nomeMaepaciente = this.paciente.NomeMae;

            if (nomeMaepaciente.Length > 30)
                nomeMaepaciente = nomeMaepaciente.Remove(30);
            else
            {
                while (nomeMaepaciente.Length != 30)
                    nomeMaepaciente += " ";
            }

            return nomeMaepaciente;
        }

        public int IdadePaciente()
        {
            return this.CalcularIdade(this.DataValidadeInicial, this.paciente.DataNascimento);
        }

        protected int CalcularIdade(DateTime dataatual, DateTime datanascimento)
        {
            int idade = dataatual.Year - datanascimento.Year;

            if (dataatual.Month < datanascimento.Month ||
                (dataatual.Month == datanascimento.Month &&
                    dataatual.Day < datanascimento.Day))
                idade--;

            return idade;
        }

        
        //private DescritivoProcedimentosRealizados detalhamentoParteVariavel;
        ///// <summary>
        ///// Detalhamento da parte variável,  determinada pelo procedimento principal
        ///// </summary>
        //public virtual DescritivoProcedimentosRealizados DetalhamentoParteVariavel
        //{
        //    get { return detalhamentoParteVariavel; }
        //    set { detalhamentoParteVariavel = value; }
        //}

    }
}
