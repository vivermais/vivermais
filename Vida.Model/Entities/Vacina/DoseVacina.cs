﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class DoseVacina
    {
        public static int ULTIMA_DOSE_NORMAL = 5;

        //VALORES ASSOCIADOS AO BANCO DE DADOS
        public static int PRIMEIRA = 3;
        public static int SEGUNDA = 4;
        public static int TERCEIRA = 5;
        public static int QUARTA = 42;
        public static int QUINTA = 43;
        public static int UNICA = 44;
        public static int REFORCO = 41;
        public static int PRIMEIRO_REFORCO = 6;
        public static int SEGUNDO_REFORCO = 21;

        int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        string descricao;
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        string abreviacao;
        public virtual string Abreviacao
        {
            get { return abreviacao; }
            set { abreviacao = value; }
        }

        int numeracaodose;
        public virtual int NumeracaoDose
        {
            get { return numeracaodose; }
            set { numeracaodose = value; }
        }

        public override string ToString()
        {
            if (this.Codigo == 3 || this.Codigo == 4 || this.Codigo == 5)
                return this.Descricao + " dose";
            else
                return this.Descricao;

        }

        public DoseVacina()
        {

        }

        public virtual int RetornarPesoDose()
        {
            int valor = -1;

            if (this.Codigo == DoseVacina.PRIMEIRA)
                valor = 1;
            if (this.Codigo == DoseVacina.SEGUNDA)
                valor = 2;
            if (this.Codigo == DoseVacina.TERCEIRA)
                valor = 3;
            if (this.Codigo == DoseVacina.QUARTA)
                valor = 4;
            if (this.Codigo == DoseVacina.QUINTA)
                valor = 5;
            if (this.Codigo == DoseVacina.UNICA)
                valor = 6;
            if (this.Codigo == DoseVacina.PRIMEIRO_REFORCO)
                valor = 7;
            if (this.Codigo == DoseVacina.SEGUNDO_REFORCO)
                valor = 8;
            if (this.Codigo == DoseVacina.REFORCO)
                valor = 9;

            return valor;
        }

        //public override bool Equals(object obj)
        //{
        //    return this.Codigo == ((DoseVacina)obj).Codigo ? true : false;
        //}

    }
}
