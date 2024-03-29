﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class OrgaoEmissor:AModel
    {
        string codigo;

        public virtual string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        string nome;

        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        private CategoriaOcupacao categoriaOcupacao;

        public virtual CategoriaOcupacao CategoriaOcupacao
        {
            get { return categoriaOcupacao; }
            set { categoriaOcupacao = value; }
        }

        public override string ToString()
        {
            return this.Nome;
        }

        public OrgaoEmissor()
        {

        }

        public override bool Persistido()
        {
            return this.codigo != string.Empty;
        }
    }
}
