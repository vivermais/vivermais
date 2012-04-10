using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.Model
{
    [Serializable]
    public class Campanha
    {
        public enum DescricaoUnidadeFaixa { Meses = 'M' , Anos = 'A' };
        public enum DescricaoStatus { Ativa = 'A', Finalizada = 'F' };

        int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        
        string nome;
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        int ano;

        public virtual int Ano
        {
            get { return ano; }
            set { ano = value; }
        }

        int mes;

        public virtual int Mes
        {
            get { return mes; }
            set { mes = value; }
        }

        float faixaEtariaInicial;

        public virtual float FaixaEtariaInicial
        {
            get { return faixaEtariaInicial; }
            set { faixaEtariaInicial = value; }
        }

        float faixaEtariaFinal;

        public virtual float FaixaEtariaFinal
        {
            get { return faixaEtariaFinal; }
            set { faixaEtariaFinal = value; }
        }

        private char unidadefaixainicial;
        public virtual char UnidadeFaixaInicial
        {
            get { return unidadefaixainicial; }
            set { unidadefaixainicial = value; }
        }

        private char unidadefaixafinal;
        public virtual char UnidadeFaixaFinal
        {
            get { return unidadefaixafinal; }
            set { unidadefaixafinal = value; }
        }

        private DateTime datainicio;
        public virtual DateTime DataInicio
        {
            get { return datainicio; }
            set { datainicio = value; }
        }

        private DateTime datafim;
        public virtual DateTime DataFim
        {
            get { return datafim; }
            set { datafim = value; }
        }

        private int meta;
        public virtual int Meta
        {
            get { return meta; }
            set { meta = value; }
        }

        private char status;
        public virtual char Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// Representa a FaixaEtaria Inicial + "-" + FaixaEtaria Final
        /// </summary>
        public virtual string FaixaEtaria
        {
            get { return faixaEtariaInicial + "-" + FaixaEtariaFinal; }
        }

        int sexo;

        /// <summary>
        /// 1 - Representa Masculino
        /// 2 - Representa Feminino
        /// 3 - Representa Ambos
        /// </summary>
        public virtual int Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }

        public override string ToString()
        {
            return this.Nome;
        }

        public override bool Equals(object obj)
        {
            if (this.codigo == ((Campanha)obj).codigo)
                return true;
            else
                return false;
        }

        public Campanha()
        {
            
        }
    }
}
