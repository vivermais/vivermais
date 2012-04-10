using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ViaAdministracao
    {
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

        public ViaAdministracao()
        {
        }
    }
}
