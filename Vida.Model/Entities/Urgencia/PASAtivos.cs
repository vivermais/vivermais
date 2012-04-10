using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.Model
{
    [Serializable]
    public class PASAtivos
    {
        //public static string[] unidades = { "0004154" };

        private string codigo;
        public virtual string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public PASAtivos()
        {
        }
    }
}
