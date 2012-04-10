using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class GrupoCBO
    {
        private string codigo;

        public virtual string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string nomeGrupo;

        public virtual string NomeGrupo
        {
            get { return nomeGrupo; }
            set { nomeGrupo = value; }
        }

        public GrupoCBO() { }

    }
}
