using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ProcedimentoOcupacao
    {
        private Procedimento procedimento;

        public virtual Procedimento Procedimento 
        {
            get { return procedimento; }
            set { procedimento = value; }
        }

        private CBO cbo;

        public virtual CBO CBO 
        {
            get { return cbo; }
            set { cbo = value; }
        }

        private string datacompetencia;

        public virtual string DataCompetencia 
        {
            get { return datacompetencia; }
            set { datacompetencia = value; }
        }

        public ProcedimentoOcupacao() 
        {
        }

        public override bool Equals(object obj)
        {
            return this.Procedimento.Codigo.Equals(((ProcedimentoOcupacao)obj).Procedimento.Codigo) &&
                   this.CBO.Equals(((ProcedimentoOcupacao)obj).CBO.Codigo);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 51;
        }
    }
}
