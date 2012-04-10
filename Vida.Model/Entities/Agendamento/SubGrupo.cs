using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class SubGrupo
    {
        public SubGrupo() { }
        public SubGrupo(string nomeSubGrupo)
        {
            this.NomeSubGrupo = nomeSubGrupo;
        }

        public override string ToString()
        {
            return this.NomeSubGrupo;
        }

        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string nomeSubGrupo;

        public virtual string NomeSubGrupo
        {
            get { return nomeSubGrupo; }
            set { nomeSubGrupo = value; }
        }
    }
}
