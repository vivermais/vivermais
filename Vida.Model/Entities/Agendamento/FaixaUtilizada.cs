using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class FaixaUtilizada
    {

        private string faixa_Utilizada;

        public virtual string Faixa_Utilizada
        {
            get { return faixa_Utilizada; }
            set { faixa_Utilizada = value; }
        }

        private Faixa faixa;

        public virtual Faixa Faixa
        {
            get { return faixa; }
            set { faixa = value; }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 41;
        }

        public override bool Equals(object obj)
        {
            return this.Faixa_Utilizada.Equals(obj) && this.Faixa.Equals(obj);
        }

        public FaixaUtilizada()
        {

        }
    }
}
