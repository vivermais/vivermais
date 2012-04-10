﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class TipoOperacaoMovimento
    {
        public static int ENTRADA = 1;
        public static int SAIDA = 2;

        private int codigo;
        public virtual int Codigo 
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

        public TipoOperacaoMovimento() 
        {
        }
    }
}
