using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class FaixaEtaria
    {
        int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        int inicial;
        public virtual int Inicial
        {
            get { return inicial; }
            set { inicial = value; }
        }

        int final;
        public virtual int Final
        {
            get { return final; }
            set { final = value; }
        }

        public FaixaEtaria()
        {
        }
        public override string ToString()
        {
            return this.Inicial.ToString() + "-" + this.Final.ToString();
        }
    }
}
