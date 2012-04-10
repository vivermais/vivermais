using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.Model
{
    public class PactoProcedimentoCBO
    {
        public PactoProcedimentoCBO()
        {

        }

        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private Pacto pacto;

        public virtual Pacto Pacto
        {
            get { return pacto; }
            set { pacto = value; }
        }

        private Procedimento procedimento;

        public virtual Procedimento Procedimento
        {
            get { return procedimento; }
            set { procedimento = value; }
        }

        private CBO cbo;

        public virtual CBO Cbo
        {
            get { return cbo; }
            set { cbo = value; }
        }

        private long valorPactuado;

        public virtual long ValorPactuado
        {
            get { return valorPactuado; }
            set { valorPactuado = value; }
        }

        private long valorRestante;

        public virtual long ValorRestante
        {
            get { return valorRestante; }
            set { valorRestante = value; }
        }

        private bool ativo;

        public virtual bool Ativo
        {
            get { return ativo; }
            set { ativo = value; }
        }

        private int percentual;

        public virtual int Percentual
        {
            get { return percentual; }
            set { percentual = value; }
        }

    }
}
