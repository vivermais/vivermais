using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class PropriedadeVacina
    {
        int codigo;

        public virtual int Codigo
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

        string tipo;

        public virtual string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public PropriedadeVacina()
        {

        }

        public override string ToString()
        {
            return this.Nome;
        }
    }
}
