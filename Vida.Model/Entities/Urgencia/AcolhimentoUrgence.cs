﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class AcolhimentoUrgence
    {
        public static readonly float TEMPERATURA_PADRAO_FEBRE = 37;
        public static readonly char INFANTIL = 'I';
        public static readonly char ADULTO = 'A';

        public AcolhimentoUrgence()
        {
        }

        long codigo;
        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        Prontuario prontuario;
        public virtual Prontuario Prontuario 
        {
            get { return prontuario; }
            set { prontuario = value; }
        }

        DateTime data;
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        string queixa;
        public virtual string Queixa
        {
            get { return queixa; }
            set { queixa = value; }
        }

        string temperatura;
        public virtual string Temperatura
        {
            get { return temperatura; }
            set { temperatura = value; }
        }

        string hgt;
        public virtual string Hgt
        {
            get { return hgt; }
            set { hgt = value; }
        }

        bool acidente;
        public virtual bool Acidente
        {
            get { return acidente; }
            set { acidente = value; }
        }

        bool convulsao;
        public virtual bool Convulsao
        {
            get { return convulsao; }
            set { convulsao = value; }
        }

        bool fratura;
        public virtual bool Fratura
        {
            get { return fratura; }
            set { fratura = value; }
        }

        string frequenciacardiaca;
        public virtual string FrequenciaCardiaca
        {
            get { return frequenciacardiaca; }
            set { frequenciacardiaca = value; }
        }

        string frequenciarespiratoria;
        public virtual string FrequenciaRespiratoria
        {
            get { return frequenciarespiratoria; }
            set { frequenciarespiratoria = value; }
        }

        bool asma;
        public virtual bool Asma
        {
            get { return asma; }
            set { asma = value; }
        }

        bool dorintensa;
        public virtual bool DorIntensa
        {
            get { return dorintensa; }
            set { dorintensa = value; }
        }

        bool alergia;
        public virtual bool Alergia
        {
            get { return alergia; }
            set { alergia = value; }
        }

        bool diarreia;
        public virtual bool Diarreia
        {
            get { return diarreia; }
            set { diarreia = value; }
        }

        private bool dortoraxica;
        public virtual bool DorToraxica
        {
            get { return dortoraxica; }
            set { dortoraxica = value; }
        }

        private bool saturacaooxigenio;
        public virtual bool SaturacaoOxigenio
        {
            get { return saturacaooxigenio; }
            set { saturacaooxigenio = value; }
        }

        private string tensaoarterialinicio;
        public virtual string TensaoArterialInicio
        {
            get { return tensaoarterialinicio; }
            set { tensaoarterialinicio = value; }
        }

        private string tensaoarterialfim;
        public virtual string TensaoArterialFim
        {
            get { return tensaoarterialfim; }
            set { tensaoarterialfim = value; }
        }

        private decimal? peso;
        public virtual decimal? Peso
        {
            get { return peso; }
            set { peso = value; }
        }

        private ClassificacaoRisco classificacaorisco;
        public virtual ClassificacaoRisco ClassificacaoRisco
        {
            get { return classificacaorisco; }
            set { classificacaorisco = value; }
        }

        string codigoprofissional;
        public virtual string CodigoProfissional
        {
            get { return codigoprofissional; }
            set { codigoprofissional = value; }
        }

        private string cboprofissional;
        public virtual string CBOProfissional
        {
            get { return cboprofissional; }
            set { cboprofissional = value; }
        }

        //private char? tipoacolhimento;
        //public virtual char? TipoAcolhimento
        //{
        //    get { return tipoacolhimento; }
        //    set { tipoacolhimento = value; }
        //}

        ///// <summary>
        ///// Retorna o código do senhador para o tipo de acolhimento
        ///// </summary>
        //public virtual char ? CodigoTipoAcolhimentoSenhador
        //{
        //    get
        //    {
        //        if (this.tipoacolhimento.HasValue && this.tipoacolhimento.Value != '\0')
        //        {
        //            if (this.tipoacolhimento.Value == INFANTIL)
        //                return 'I';
        //            else
        //                return 'A';
        //        }

        //        return null; //inválido
        //    }
        //}
    }
}
