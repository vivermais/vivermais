using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ItemMovimentacao
    {
        private Movimento movimento;

        public virtual Movimento Movimento
        {
            get { return movimento; }
            set { movimento = value; }
        }

        private LoteMedicamento lotemedicamento;

        public virtual LoteMedicamento LoteMedicamento
        {
            get { return lotemedicamento; }
            set { lotemedicamento = value; }
        }

        private int quantidade;

        public virtual int Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }


        public override int GetHashCode()
        {
            return base.GetHashCode() * 13;
        }

        public override bool Equals(object obj)
        {
            return this.LoteMedicamento.Equals(obj) &&
                   this.Movimento.Equals(obj);
        }

        public virtual string CodigoLote 
        {
            get { return LoteMedicamento.Codigo.ToString(); }
        }

        public virtual string NomeMedicamento 
        {
            get { return LoteMedicamento.Medicamento.Nome; }
        }

        public virtual string NomeFabricante 
        {
            get { return LoteMedicamento.Fabricante.Nome; }
        }

        public virtual string NomeLote 
        {
            get { return LoteMedicamento.Lote; }
        }

        public ItemMovimentacao()
        {
        }
    }
}
