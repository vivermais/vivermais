using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Bairro:AModel
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

        Distrito distrito;

        public virtual Distrito Distrito
        {
            get { return distrito; }
            set { distrito = value; }
        }

        public Bairro()
        {

        }

        public override bool Persistido()
        {
            return this.codigo != 0;
        }
    }
}
