using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Unidade
    {
        private string cnes;

        public virtual string CNES
        {
            get { return cnes; }
            set { cnes = value; }
        }

        private bool intoleranteFeriado;

        public virtual bool IntoleranteFeriado
        {
            get { return intoleranteFeriado; }
            set { intoleranteFeriado = value; }
        }


        /// <summary>
        /// 
        /// </summary>
   

        public Unidade()
        {

        }
    }
}
