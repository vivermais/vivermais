using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class SetorUnidade
    {
        private string codigounidade;

        public virtual string CodigoUnidade
        {
            get { return codigounidade; }
            set { codigounidade = value; }
        }

        private Setor setor;

        public virtual Setor Setor
        {
            get { return setor; }
            set { setor = value; }
        }
        
        public override bool Equals(object obj)
        {
            return CodigoUnidade.Equals(((SetorUnidade)obj).CodigoUnidade) && Setor.Codigo == ((SetorUnidade)obj).Setor.Codigo;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 79;
        }

        public SetorUnidade()
        {
        }

        public SetorUnidade(string CodigoUnidade, Setor Setor) 
        {
            this.CodigoUnidade = CodigoUnidade;
            this.Setor = Setor;
        }
    }
}
