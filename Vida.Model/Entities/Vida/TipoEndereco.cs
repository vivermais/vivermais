using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class TipoEndereco
    {
        string codigo;

        public virtual string Codigo
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

        public TipoEndereco()
        {

        }
    }
}
