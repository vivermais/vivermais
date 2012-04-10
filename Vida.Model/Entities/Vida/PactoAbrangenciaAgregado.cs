using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class PactoAbrangenciaAgregado
    {
        public enum TipoDePacto {AGREGADO = 'A', PROCEDIMENTO='P',CBO = 'C'}
        public enum BloqueadoPorCota { SIM = 1, NAO = 0 }
        public enum PactoAtivo { SIM = 1, NAO = 0 }

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

        private Procedimento procedimento;

        public virtual Procedimento Procedimento
        {
            get { return procedimento; }
            set { procedimento = value; }
        }

        private DateTime dataPacto;

        public virtual DateTime DataPacto
        {
            get { return dataPacto; }
            set { dataPacto = value; }
        }

        private DateTime dataUltimaOperacao;

        public virtual DateTime DataUltimaOperacao
        {
            get { return dataUltimaOperacao; }
            set { dataUltimaOperacao = value; }
        }
        private Usuario usuario;

        public virtual Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        private CBO cbo;

        public virtual CBO Cbo
        {
            get { return cbo; }
            set { cbo = value; }
        }

        //private float valorRestante;

        ///// <summary>
        ///// Não Mais usada
        ///// </summary>
        //public virtual float ValorRestante
        //{
        //    get { return valorRestante; }
        //    set { valorRestante = value; }
        //}

        //private int percentual;

        //public virtual int Percentual
        //{
        //    get { return percentual; }
        //    set { percentual = value; }
        //}

        private char tipoPacto;

        public virtual char TipoPacto
        {
            get { return tipoPacto; }
            set { tipoPacto = value; }
        }

        private bool ativo;

        public virtual bool Ativo
        {
            get { return ativo; }
            set { ativo = value; }
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

        private PactoAbrangencia pactoAbrangencia;

        public virtual PactoAbrangencia PactoAbrangencia
        {
          get { return pactoAbrangencia; }
          set { pactoAbrangencia = value; }
        }

        private int bloqueiaCota;

        ///// <summary>
        ///// Verifica se está bloqueada pela Cota Financeira. (0-Não, 1-Sim)
        ///// </summary>
        //public virtual int BloqueiaCota
        //{
        //    get { return bloqueiaCota; }
        //    set { bloqueiaCota = value; }
        //}

        private int ano;

        public virtual int Ano
        {
            get { return ano; }
            set { ano = value; }
        }

        private Decimal valorPactuado;

        public virtual Decimal ValorPactuado
        {
            get { return valorPactuado; }
            set { valorPactuado = value; }
        }


        private Decimal valorUtilizado;
        /// <summary>
        /// Identifica o Saldo restante para um Grupo de Abrangência Realizar em determinado Pacto Abrangencia
        /// </summary>
        public virtual Decimal ValorUtilizado
        {
            get { return valorUtilizado; }
            set { valorUtilizado = value; }
        }
        
        //private string datacompetencia;
        //public virtual string DataCompetencia
        //{
        //    get { return datacompetencia; }
        //    set { datacompetencia = value; }
        //}

        public PactoAbrangenciaAgregado()
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
