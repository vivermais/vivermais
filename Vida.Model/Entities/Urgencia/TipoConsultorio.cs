using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.Model
{
    public class TipoConsultorio
    {
        int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        string descricao;

        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        string cor;

        public virtual string Cor
        {
            get { return cor; }
            set { cor = value; }
        }

        public TipoConsultorio()
        {
        }
    }
}
