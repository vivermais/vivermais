using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.Model
{
    public class SenhaProfissionalSenhador
    {
        public SenhaProfissionalSenhador() 
        {
        }

        private long codigo;

        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private Profissional profissional;

        public virtual Profissional Profissional
        {
            get { return profissional; }
            set { profissional = value; }
        }

        private SenhaSenhador senha;

        public virtual SenhaSenhador Senha
        {
            get { return senha; }
            set { senha = value; }
        }
    }
}
