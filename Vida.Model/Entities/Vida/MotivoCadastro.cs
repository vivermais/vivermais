using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class MotivoCadastro:AModel
    {
        int codigo;

        public virtual int Codigo
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

        public MotivoCadastro()
        {

        }

        public override bool Persistido()
        {
            return this.codigo != 0;
        }
    }
}
