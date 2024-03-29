﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class SubGrupoAgregado
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

        private GrupoAgregado grupoAgregado;

        public virtual GrupoAgregado GrupoAgregado
        {
            get { return grupoAgregado; }
            set { grupoAgregado = value; }
        }

        public SubGrupoAgregado() 
        {
        }
    }
}
