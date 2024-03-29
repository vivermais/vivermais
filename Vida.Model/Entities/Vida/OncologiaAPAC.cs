﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class OncologiaAPAC : ParteVariavelProcedimento
    {
        public OncologiaAPAC()
        {

        }

        public const string co_subGrupo = "0304";
        private Cid cidTopografia;
        /// <summary>
        /// CID 10 Topografia
        /// </summary>
        public Cid CidTopografia
        {
            get { return cidTopografia; }
            set { cidTopografia = value; }
        }

        private char linfonodosRegionaisInvadidos;
        /// <summary>
        /// Linfonodos regionais invadidos (S = Sim; N = Não; 3 = Não Avaliáveis)
        /// </summary>
        public char LinfonodosRegionaisInvadidos
        {
            get { return linfonodosRegionaisInvadidos; }
            set { linfonodosRegionaisInvadidos = value; }
        }

        private int estadioUICC;
        /// <summary>
        /// Estádio – UICC (0;1;2;3;4)
        /// </summary>
        public int EstadioUICC
        {
            get { return estadioUICC; }
            set { estadioUICC = value; }
        }

        private int grauHistopatologico;
        /// <summary>
        /// Grau Histopatológico
        /// </summary>
        public int GrauHistopatologico
        {
            get { return grauHistopatologico; }
            set { grauHistopatologico = value; }
        }

        private DateTime dataIdentificacaoPatologica;
        /// <summary>
        /// Data da identificação patológica do caso
        /// </summary>
        public DateTime DataIdentificacaoPatologica
        {
            get { return dataIdentificacaoPatologica; }
            set { dataIdentificacaoPatologica = value; }
        }

        private char tratamentosAnteriores;
        /// <summary>
        /// Tratamentos anteriores (S=Sim; N=Não)
        /// </summary>
        public char TratamentosAnteriores
        {
            get { return tratamentosAnteriores; }
            set { tratamentosAnteriores = value; }
        }

        private DateTime dataInicioPrimeiroTratamentoAnterior;
        /// <summary>
        /// Data de inicio 1º tratamento anterior
        /// </summary>
        public DateTime DataInicioPrimeiroTratamentoAnterior
        {
            get { return dataInicioPrimeiroTratamentoAnterior; }
            set { dataInicioPrimeiroTratamentoAnterior = value; }
        }

        private Cid cidPrimeiroTratamentoAnterior;
        /// <summary>
        /// CID 1º Tratamento anterior
        /// </summary>
        public Cid CidPrimeiroTratamentoAnterior
        {
            get { return cidPrimeiroTratamentoAnterior; }
            set { cidPrimeiroTratamentoAnterior = value; }
        }


        private Cid cidSegundoTratamentoAnterior;
        /// <summary>
        /// CID 2º Tratamento anterior
        /// </summary>
        public Cid CidSegundoTratamentoAnterior
        {
            get { return cidSegundoTratamentoAnterior; }
            set { cidSegundoTratamentoAnterior = value; }
        }

        private DateTime dataInicioSegundoTratamentoAnterior;
        /// <summary>
        /// Data de inicio 2º tratamento anterior
        /// </summary>
        public DateTime DataInicioSegundoTratamentoAnterior
        {
            get { return dataInicioSegundoTratamentoAnterior; }
            set { dataInicioSegundoTratamentoAnterior = value; }
        }

        private Cid cidTerceiroTratamentoAnterior;
        /// <summary>
        /// CID 3º Tratamento anterior
        /// </summary>
        public Cid CidTerceiroTratamentoAnterior
        {
            get { return cidTerceiroTratamentoAnterior; }
            set { cidTerceiroTratamentoAnterior = value; }
        }

        private DateTime dataInicioTerceiroTratamentoAnterior;
        /// <summary>
        /// Data de inicio 3º tratamento anterior
        /// </summary>
        public DateTime DataInicioTerceiroTratamentoAnterior
        {
            get { return dataInicioTerceiroTratamentoAnterior; }
            set { dataInicioTerceiroTratamentoAnterior = value; }
        }

        private char continuidadeTratamento;
        /// <summary>
        /// Continuidade do tratamento (S=Sim; N=Não)
        /// </summary>
        public char ContinuidadeTratamento
        {
            get { return continuidadeTratamento; }
            set { continuidadeTratamento = value; }
        }

        private DateTime dataSolicitacaoTratamento;
        /// <summary>
        /// Data de inicio do tratamento solicitado
        /// </summary>
        public DateTime DataSolicitacaoTratamento
        {
            get { return dataSolicitacaoTratamento; }
            set { dataSolicitacaoTratamento = value; }
        }
    }
}
