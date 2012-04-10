using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class TipoAtestadoReceita
    {
        public static int Atestado = 2;
        public static int Receita = 1;
        public static int Comparecimento = 3;

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

        public TipoAtestadoReceita()
        {
        }
    }
}
