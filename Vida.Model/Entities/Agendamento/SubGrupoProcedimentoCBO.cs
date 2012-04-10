using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class SubGrupoProcedimentoCBO
    {
        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private Procedimento procedimento;

        public virtual Procedimento Procedimento
        {
            get { return procedimento; }
            set { procedimento = value; }
        }

        private SubGrupo subGrupo;

        public virtual SubGrupo SubGrupo
        {
            get { return subGrupo; }
            set { subGrupo = value; }
        }

        private CBO cbo;

        public virtual CBO Cbo
        {
            get { return cbo; }
            set { cbo = value; }
        }

        private bool ativo;

        public virtual bool Ativo
        {
            get { return ativo; }
            set { ativo = value; }
        }

        public SubGrupoProcedimentoCBO() { }
        
        public SubGrupoProcedimentoCBO(Procedimento proced, SubGrupo sub, CBO cbo)
        {
            this.SubGrupo = sub;
            this.Procedimento = proced;
            this.Cbo = cbo;
            this.Ativo = true;
        }
    }
}
