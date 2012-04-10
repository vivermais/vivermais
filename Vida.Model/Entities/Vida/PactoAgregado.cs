using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.Model
{
    [Serializable]
    public class PactoAgregado
    {
        public enum TipoDePacto {AGREGADO = 'A', PROCEDIMENTO='P',CBO = 'C'}
        
        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private Agregado agregado;

        public virtual Agregado Agregado
        {
            get { return agregado; }
            set { agregado = value; }
        }

        private long valorRestante;

        public virtual long ValorRestante
        {
            get { return valorRestante; }
            set { valorRestante = value; }
        }

        private int percentual;

        public virtual int Percentual
        {
            get { return percentual; }
            set { percentual = value; }
        }

        private char tipoPacto;

        public virtual char TipoPacto
        {
            get { return tipoPacto; }
            set { tipoPacto = value; }
        }

        //private CBO cbo;

        //public CBO Cbo
        //{
        //    get { return cbo; }
        //    set { cbo = value; }
        //}

        //private Procedimento procedimento;

        //public virtual Procedimento Procedimento
        //{
        //    get { return procedimento; }
        //    set { procedimento = value; }
        //}

        //private ProcedimentoAgregado procedimentoAgregado;

        //public virtual ProcedimentoAgregado ProcedimentoAgregado
        //{
        //    get { return procedimentoAgregado; }
        //    set { procedimentoAgregado = value; }
        //}

        private Pacto pacto;

        public virtual Pacto Pacto
        {
            get { return pacto; }
            set { pacto = value; }
        }

        private int bloqueiaCota;

        /// <summary>
        /// Verifica se está bloqueada pela Cota Financeira. (0-Não, 1-Sim)
        /// </summary>
        public virtual int BloqueiaCota
        {
            get { return bloqueiaCota; }
            set { bloqueiaCota = value; }
        }

        private long valorPactuado;

        public virtual long ValorPactuado
        {
            get { return valorPactuado; }
            set { valorPactuado = value; }
        }
        
        //private string datacompetencia;
        //public virtual string DataCompetencia
        //{
        //    get { return datacompetencia; }
        //    set { datacompetencia = value; }
        //}

        public PactoAgregado()
        {

        }

        //public override bool Equals(object obj)
        //{
        //    return this.ProcedimentoAgregado.Codigo.Equals(((ProcedimentoAgregado)obj).Codigo) &&
        //           this.Pacto.Equals(((Pacto)obj).Codigo);
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode() * 77;
        //}
    }
}
