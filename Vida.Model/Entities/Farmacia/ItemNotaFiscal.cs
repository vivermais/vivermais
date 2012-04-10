using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ItemNotaFiscal
    {
        private int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        NotaFiscal notafiscal;
        public virtual NotaFiscal NotaFiscal
        {
            get { return notafiscal; }
            set { notafiscal = value; }
        }

        LoteMedicamento lotemedicamento;
        public virtual LoteMedicamento LoteMedicamento
        {
            get { return lotemedicamento; }
            set { lotemedicamento = value; }
        }

        int quantidade;
        public virtual int Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }

        decimal valorUnitario;
        public virtual decimal ValorUnitario
        {
            get { return valorUnitario; }
            set { valorUnitario = value; }
        }

        public virtual int CodigoLote
        {
            get { return this.LoteMedicamento.Codigo; }
        }

        public virtual string NomeLote 
        {
            get { return this.LoteMedicamento.Lote; }
        }

        public virtual string NomeMedicamento
        {
            get { return this.LoteMedicamento.Medicamento.Nome; }
        }

        public virtual string UnidadeMedidaMedicamento
        {
            get { return this.LoteMedicamento.Medicamento.UnidadeMedidaToString; }
        }

        public virtual string FabricanteMedicamento
        {
            get { return this.LoteMedicamento.Fabricante.Nome; }
        }

        public virtual string FabricanteMedicamentoCodigo 
        {
            get { return this.LoteMedicamento.Fabricante.Codigo.ToString(); }
        }

        public virtual DateTime ValidadeMedicamento
        {
            get { return this.LoteMedicamento.Validade; }
        }

        public ItemNotaFiscal()
        {
        }

        //public override bool Equals(object obj)
        //{
        //    return this.LoteMedicamento.Codigo == ((ItensNotaFiscal)obj).LoteMedicamento.Codigo &&
        //           this.NotaFiscal.Codigo == ((ItensNotaFiscal)obj).NotaFiscal.Codigo;
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode() * 47;
        //}
    }
}
