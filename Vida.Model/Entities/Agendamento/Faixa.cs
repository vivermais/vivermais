using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Faixa
    {
        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private DateTime? dataInclusao;

        public virtual DateTime? DataInclusao
        {
            get { return dataInclusao; }
            set { dataInclusao = value; }
        }

        public virtual String Data
        {
            get { return this.DataInclusao.HasValue ? this.DataInclusao.Value.ToShortDateString() : String.Empty; }
        }

        public virtual long QuantidadeDisponivel
        {
            get { return this.Quantitativo - this.Quantidade_utilizada; }
        }

        
        private string faixaInicial;

        public virtual string FaixaInicial
        {
            get { return faixaInicial; }
            set { faixaInicial = value; }
        }

        private string faixaFinal;

        public virtual string FaixaFinal
        {
            get { return faixaFinal; }
            set { faixaFinal = value; }
        }

        private long quantitativo;

        public virtual long Quantitativo
        {
            get { return quantitativo; }
            set { quantitativo = value; }
        }

        private long quantidade_utilizada;

        public virtual long Quantidade_utilizada
        {
            get { return quantidade_utilizada; }
            set { quantidade_utilizada = value; }
        }

        private char tipo;

        public virtual char Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private string ano_vigencia;

        public virtual string Ano_vigencia
        {
            get { return ano_vigencia; }
            set { ano_vigencia = value; }
        }

        public Faixa()
        {
        }
    }
}
