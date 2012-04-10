using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.Model
{
    public class Perfil
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

        public Perfil()
        {

        }
    }
}
