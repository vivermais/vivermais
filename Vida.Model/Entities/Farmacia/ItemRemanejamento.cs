using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ItemRemanejamento
    {
        
        private RemanejamentoMedicamento remanejamento;

        public virtual RemanejamentoMedicamento Remanejamento
        {
            get { return remanejamento; }
            set { remanejamento = value; }
        }

        private LoteMedicamento lotemedicamento;

        public virtual LoteMedicamento LoteMedicamento
        {
            get { return lotemedicamento; }
            set { lotemedicamento = value; }
        }

        private int quantidaderegistrada;

        public virtual int QuantidadeRegistrada
        {
            get { return quantidaderegistrada; }
            set { quantidaderegistrada = value; }
        }

        private int ? quantidaderecebida;

        public virtual int ? QuantidadeRecebida
        {
            get { return quantidaderecebida; }
            set { quantidaderecebida = value; }
        }

        //private int? quantidadealterada;

        //public virtual int? QuantidadeAlterada
        //{
        //    get { return quantidadealterada; }
        //    set { quantidadealterada = value; }
        //}

        private DateTime ? dataconfirmacao;

        public virtual DateTime ? DataConfirmacao
        {
            get { return dataconfirmacao; }
            set { dataconfirmacao = value; }
        }

        private DateTime? dataalteracao;

        public virtual DateTime? DataAlteracao
        {
            get { return dataalteracao; }
            set { dataalteracao = value; }
        }

        public virtual string NomeLote
        {
            get { return LoteMedicamento.Lote; }
        }

        public virtual string NomeMedicamento 
        {
            get { return LoteMedicamento.Medicamento.Nome; }
        }

        public virtual int CodigoLote
        {
            get { return LoteMedicamento.Codigo; }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 31;
        }

        public override bool Equals(object obj)
        {
            return this.LoteMedicamento.Equals(obj) &&
                   this.Remanejamento.Equals(obj);
        }

        public ItemRemanejamento()
        {
        }
    }
}
