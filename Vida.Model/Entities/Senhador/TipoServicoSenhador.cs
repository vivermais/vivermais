using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class TipoServicoSenhador
    {
        public static int NORMAL = 1;
        public static int AGENDADO = 2;

        public TipoServicoSenhador() 
        {
        }

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
    }
}
