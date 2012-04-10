using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ProcedimentoCid
    {
        private Procedimento procedimento;
        public virtual Procedimento Procedimento
        {
            get { return procedimento; }
            set { procedimento = value; }
        }

        private Cid cid;
        public virtual Cid Cid
        {
            get { return cid; }
            set { cid = value; }
        }

        private string datacompetencia;
        public virtual string DataCompetencia
        {
            get { return datacompetencia; }
            set { datacompetencia = value; }
        }

        private string cidprincipal;
        public virtual string CidPrincipal
        {
            get { return cidprincipal; }
            set { cidprincipal = value; }
        }

        public ProcedimentoCid()
        {
        }

        public override bool Equals(object obj)
        {
            return this.Procedimento.Codigo.Equals(((ProcedimentoCid)obj).Procedimento.Codigo) &&
                   this.Cid.Equals(((ProcedimentoCid)obj).Cid.Codigo);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 77;
        }
    }
}
