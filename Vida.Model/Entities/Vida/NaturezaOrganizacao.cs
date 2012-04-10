﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class NaturezaOrganizacao
    {
        public static int ADMINISTRACAO_DIRETA_SAUDE = 1;

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

        public NaturezaOrganizacao() 
        {
        }
    }
}