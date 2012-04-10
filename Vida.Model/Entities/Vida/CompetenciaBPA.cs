﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class CompetenciaBPA
    {
        int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        int ano;

        public virtual int Ano
        {
            get { return ano; }
            set { ano = value; }
        }

        int mes;

        public virtual int Mes
        {
            get { return mes; }
            set { mes = value; }
        }

        DateTime dataInicial;

        public virtual DateTime DataInicial
        {
            get { return dataInicial; }
            set { dataInicial = value; }
        }

        DateTime dataFinal;

        public virtual DateTime DataFinal
        {
            get { return dataFinal; }
            set { dataFinal = value; }
        }

        public CompetenciaBPA()
        {

        }

        public override string ToString()
        {
            return this.Ano + "" + this.Mes.ToString("00");
        }
    }
}
