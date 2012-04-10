using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class PactoAgregadoProcedCBO
    {
        public enum TipoDePacto {AGREGADO = 'A', PROCEDIMENTO='P',CBO = 'C'}
        public enum BloqueadoPorCota {SIM = 1, NAO = 0 }
        public enum PactoAtivo {SIM=1, NAO=0 }
        
        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private int ano;

        public virtual int Ano
        {
            get { return ano; }
            set { ano = value; }
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

        private IList<CBO> cbos;

        /// <summary>
        /// Lista de CBOs caso o Tipo de Pacto seja por CBO
        /// </summary>
        public virtual IList<CBO> Cbos
        {
            get { return cbos; }
            set { cbos = value; }
        }

        //private Decimal valorRestante;

        //public virtual Decimal ValorRestante
        //{
        //    get { return valorRestante; }
        //    set { valorRestante = value; }
        //}

        private int percentual;

        /// <summary>
        /// PErcentual que pode ser ultrapassado da cota
        /// </summary>
        public virtual int Percentual
        {
            get { return percentual; }
            set { percentual = value; }
        }

        private char tipoPacto;

        /// <summary>
        /// Identifica o Tipo de Pacto: 'A' - Por Agregado, 'P' - Procedimento, 'C' - CBO
        /// </summary>
        public virtual char TipoPacto
        {
            get { return tipoPacto; }
            set { tipoPacto = value; }
        }

        private bool ativo;

        /// <summary>
        /// Identifica se o pacto está ativo
        /// </summary>
        public virtual bool Ativo
        {
            get { return ativo; }
            set { ativo = value; }
        }

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

        private float valorPactuado;

        public virtual float ValorPactuado
        {
            get { return valorPactuado; }
            set { valorPactuado = value; }
        }

        private Decimal valorMensal;

        public virtual Decimal ValorMensal
        {
            get { return valorMensal; }
            set { valorMensal = value; }
        }

        private int percentualUrgenciaEmergencia;
        /// <summary>
        /// Guardo esta informação Justamente para Relatório, para saber posteriormente qual foi o percentual abatido no Valor do Pacto
        /// </summary>
        public virtual int PercentualUrgenciaEmergencia
        {
            get { return percentualUrgenciaEmergencia; }
            set { percentualUrgenciaEmergencia = value; }
        }

        public PactoAgregadoProcedCBO()
        {
            this.Cbos = new List<CBO>();
        }
    }
}
