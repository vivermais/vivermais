using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class MotivoDesligamento
    {
        string codigo;

        public virtual string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        string motivo;

        public virtual string Motivo
        {
            get { return motivo; }
            set { motivo = value; }
        }

        public MotivoDesligamento()
        {

        }
    }
}
