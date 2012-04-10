using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class RadioTerapiaAPAC : OncologiaAPAC
    {
        public RadioTerapiaAPAC() { }

        public const string co_FormaOrganizacao = "030401";
        /// <summary>
        /// Finalidade do tratamento (1=RADICAL; 2=ADJUVANTE; 3=ANTIÁLGICA; 4=PALIATIVA; 5=PRÉVIA; 6=ANTIHEMORRÁGICA)
        /// </summary>
        private int finalidadeTratamento;
        public int FinalidadeTratamento
        {
            get { return finalidadeTratamento; }
            set { finalidadeTratamento = value; }
        }


        private Cid primeiroCidTopografico;
        public Cid PrimeiroCidTopografico
        {
            get { return primeiroCidTopografico; }
            set { primeiroCidTopografico = value; }
        }

        private Cid segundoCidTopografico;
        public Cid SegundoCidTopografico
        {
            get { return segundoCidTopografico; }
            set { segundoCidTopografico = value; }
        }

        private Cid terceiroCidTopografico;
        public Cid TerceiroCidTopografico
        {
            get { return terceiroCidTopografico; }
            set { terceiroCidTopografico = value; }
        }

        /// <summary>
        /// 1º Nº Campo/Incerções 
        /// </summary>
        private int numeroPrimeiraIncercoes;
        public int NumeroPrimeiraIncercoes
        {
            get { return numeroPrimeiraIncercoes; }
            set { numeroPrimeiraIncercoes = value; }
        }

        /// <summary>
        /// 2º Nº Campo/Incerções 
        /// </summary>
        private int numeroSegundaIncercao;
        public int NumeroSegundaIncercao
        {
            get { return numeroSegundaIncercao; }
            set { numeroSegundaIncercao = value; }
        }

        /// <summary>
        /// 3º Nº Campo/Incerções 
        /// </summary>
        private int numeroTerceiraIncercao;
        public int NumeroTerceiraIncercao
        {
            get { return numeroTerceiraIncercao; }
            set { numeroTerceiraIncercao = value; }
        }

        /// <summary>
        /// Data de inicio 1º
        /// </summary>
        private DateTime primeiraDataInicio;
        public DateTime PrimeiraDataInicio
        {
            get { return primeiraDataInicio; }
            set { primeiraDataInicio = value; }
        }

        /// <summary>
        /// Data de inicio 2º
        /// </summary>
        private DateTime segundaDataInicio;
        public DateTime SegundaDataInicio
        {
            get { return segundaDataInicio; }
            set { segundaDataInicio = value; }
        }

        /// <summary>
        /// Data de inicio 3º
        /// </summary>
        private DateTime terceiraDataInicio;
        public DateTime TerceiraDataInicio
        {
            get { return terceiraDataInicio; }
            set { terceiraDataInicio = value; }
        }

        /// <summary>
        /// Data de fim 1º
        /// </summary>
        private DateTime primeiraDataFim;
        public DateTime PrimeiraDataFim
        {
            get { return primeiraDataFim; }
            set { primeiraDataFim = value; }
        }

        /// <summary>
        /// Data de fim 2º
        /// </summary>
        private DateTime segundaDataFim;
        public DateTime SegundaDataFim
        {
            get { return segundaDataFim; }
            set { segundaDataFim = value; }
        }

        /// <summary>
        /// Data de fim 3º
        /// </summary>
        private DateTime terceiraDataFim;
        public DateTime TerceiraDataFim
        {
            get { return terceiraDataFim; }
            set { terceiraDataFim = value; }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
