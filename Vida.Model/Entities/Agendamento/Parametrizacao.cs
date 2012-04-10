﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Parametrizacao
    {
        private int codigo;
        public virtual int Codigo 
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string nome;
        public virtual string Nome 
        {
            get { return nome; }
            set { nome = value; }
        }

        private float valor;
        public virtual float Valor 
        {
            get { return valor; }
            set { valor = value; }
        }

        private char tipo;
        public virtual char Tipo 
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private string id_Estabelecimento;

        public virtual string Id_Estabelecimento
        {
            get { return id_Estabelecimento; }
            set { id_Estabelecimento = value; }
        }

        private string id_Procedimento;

        public virtual string Id_Procedimento
        {
            get { return id_Procedimento; }
            set { id_Procedimento = value; }
        }


        public Parametrizacao() 
        {
        }
    }
}
