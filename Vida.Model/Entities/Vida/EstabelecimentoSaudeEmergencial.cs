﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class EstabelecimentoSaudeEmergencial
    {
        private int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string cnes;
        public virtual string CNES
        {
            get { return cnes; }
            set { cnes = value; }
        }

        private string nome;
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public EstabelecimentoSaudeEmergencial()
        {
        }
    }
}
