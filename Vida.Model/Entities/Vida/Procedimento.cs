using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Procedimento
    {

        public static string CONSULTA_MEDICA_ATENCAO_ESPECIALIZADA = "0301010072";
        public static string CONSULTA_MEDICA_ATENCAO_BASICA = "0301010064";
        private string codigo;

        public virtual string Codigo 
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public virtual string CodigoNomeProcedimento
        {
            get { return this.Codigo + " - " + this.Nome; }
        }
        
        private string nome;
        public virtual string Nome 
        {
            get { return nome; }
            set { nome = value; }
        }

        private string complexidade;

        public virtual string Complexidade 
        {
            get { return complexidade; }
            set { complexidade = value; }
        }

        private string restricaosexo;

        public virtual string RestricaoSexo 
        {
            get { return restricaosexo; }
            set { restricaosexo = value; }
        }

        private int qtdmaximaexecucao;

        public virtual int QtdMaximaExecucao 
        {
            get { return qtdmaximaexecucao; }
            set { qtdmaximaexecucao = value; }
        }

        private int qtddiaspermanencia;

        public virtual int QtdDiasPermanencia 
        {
            get { return qtddiaspermanencia; }
            set { qtddiaspermanencia = value; }
        }

        private int qtdpontos;

        public virtual int QtdPontos 
        {
            get { return qtdpontos; }
            set { qtdpontos = value; }
        }

        private int idademinima;

        public virtual int IdadeMinima 
        {
            get { return idademinima; }
            set { idademinima = value; }
        }

        private int idademaxima;

        public virtual int IdadeMaxima 
        {
            get { return idademaxima; }
            set { idademaxima = value; }
        }

        private int vl_sa;

        public virtual int VL_SA 
        {
            get { return vl_sa; }
            set { vl_sa = value; }
        }

        public virtual Decimal ValorProcedimentoAmbulatorialFormatado
        {
            get { return Decimal.Parse(this.VL_SA.ToString().Insert(this.VL_SA.ToString("000").Length - 2, ",")); }
        }

        private int vl_sh;

        public virtual int VL_SH
        {
            get { return vl_sh; }
            set { vl_sh = value; }
        }

        private int vl_sp;

        public virtual int VL_SP
        {
            get { return vl_sp; }
            set { vl_sp = value; }
        }

        private FinanciamentoProcedimento financiamentoprocedimento;

        public virtual FinanciamentoProcedimento FinanciamentoProcedimento 
        {
            get { return financiamentoprocedimento; }
            set { financiamentoprocedimento = value; }
        }

        private RubricaProcedimento rubricaprocedimento;

        public virtual RubricaProcedimento RubricaProcedimento 
        {
            get { return rubricaprocedimento; }
            set { rubricaprocedimento = value; }
        }

        private string datacompetencia;

        public virtual string DataCompetencia 
        {
            get { return datacompetencia; }
            set { datacompetencia = value; }
        }

        public override string ToString()
        {
            return this.Nome;
        }

        public Procedimento() 
        {
        }
    }
}
