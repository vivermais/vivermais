using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class TipoSenhaSenhador
    {
        public static int PRIORITARIA = 1;
        public static int NAO_PRIORITARIA = 2;

        public TipoSenhaSenhador() 
        {
        }

        private int codigo;
        public virtual int Codigo
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
    }
}
