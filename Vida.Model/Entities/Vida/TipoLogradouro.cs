using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class TipoLogradouro:AModel
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

        string abreviatura;

        public virtual string Abreviatura
        {
            get { return abreviatura; }
            set { abreviatura = value; }
        }

        public TipoLogradouro()
        {

        }

        public override bool Persistido()
        {
            return this.codigo != string.Empty;
        }
    }
}
