﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Banco
    {
        private string codigo;

        public virtual string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string descricao;

        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        private DateTime dataAtualizacao;

        public virtual DateTime DataAtualizacao
        {
            get { return dataAtualizacao; }
            set { dataAtualizacao = value; }
        }

        public Banco() 
        {
            
        }
    }
}