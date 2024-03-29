﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Agregado
    {
        private string codigo;

        public virtual string Codigo 
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

        private SubGrupoAgregado subGrupoAgregado;

        public virtual SubGrupoAgregado SubGrupoAgregado
        {
            get { return subGrupoAgregado; }
            set { subGrupoAgregado = value; }
        }

        public Agregado() 
        {
        }
    }
}
