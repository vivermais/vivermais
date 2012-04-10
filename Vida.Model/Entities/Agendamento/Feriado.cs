﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Feriado
    {
        private int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private DateTime data;
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        private char tipo;
        public virtual char Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private string descricao;
        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        public virtual string DataFormatada
        {
            get { return this.Data.ToString("dd/MM/yyyy"); }
        }

        public virtual string Nome
        {
            get
            {
                if (this.Tipo == 'M')
                {
                    return "Municipal";
                }
                else if (this.Tipo == 'E')
                {
                    return "Estadual";
                }
                else
                {
                    return "Nacional";
                }

            }
        }


        public Feriado()
        {
        }
    }
}
