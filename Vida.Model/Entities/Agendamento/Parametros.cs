﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Parametros
    {

        private int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private int min_dias;
        public virtual int Min_Dias
        {
            get { return min_dias; }
            set { min_dias = value; }
        }

        private int max_dias;
        public virtual int Max_Dias
        {
            get { return max_dias; }
            set { max_dias = value; }
        }

        //private int competencia;
        //public virtual int Competencia 
        //{
        //    get { return competencia; }
        //    set { competencia = value; }
        //}

        private int min_dias_espera;
        public virtual int Min_Dias_Espera
        {
            get { return min_dias_espera; }
            set { min_dias_espera = value; }
        }

        private int pct_vagas_espera;
        public virtual int Pct_Vagas_Espera
        {
            get { return pct_vagas_espera; }
            set { pct_vagas_espera = value; }
        }

        private int min_dias_cancela;
        public virtual int Min_Dias_Cancela
        {
            get { return min_dias_cancela; }
            set { min_dias_cancela = value; }
        }

        private int min_dias_reaproveita;
        public virtual int Min_Dias_Reaproveita
        {
            get { return min_dias_reaproveita; }
            set { min_dias_reaproveita = value; }
        }

        private int validade_codigo;
        public virtual int Validade_Codigo
        {
            get { return validade_codigo; }
            set { validade_codigo = value; }
        }

        public Parametros() 
        {
        }
    }
}
