using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class CartaoBase:AModel
    {
        string numero;

        public virtual string Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        int atribuido;

        public virtual int Atribuido
        {
            get { return atribuido; }
            set { atribuido = value; }
        }

        public CartaoBase()
        {
        }

        public override bool Persistido()
        {
            return this.numero != null & this.numero != string.Empty;
        }
    }
}
