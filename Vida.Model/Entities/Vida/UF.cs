﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class UF:AModel
    {
        string codigo;

        public virtual string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        string sigla;

        public virtual string Sigla
        {
            get { return sigla; }
            set { sigla = value; }
        }

        string nome;

        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public UF()
        {

        }

        public override bool Persistido()
        {
            return this.codigo != string.Empty;
        }
    }
}
