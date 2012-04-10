using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{ [Serializable]
    public class ItemInventario
    {      
        Inventario inventario;
        public virtual Inventario Inventario
        {
            get { return inventario; }
            set { inventario = value; }
        }

        LoteMedicamento lotemedicamento;
        public virtual LoteMedicamento LoteMedicamento
        {
            get { return lotemedicamento; }
            set { lotemedicamento = value; }
        }

        int qtdestoque;
        public virtual int QtdEstoque
        {
            get { return qtdestoque; }
            set { qtdestoque = value; }
        }

        int qtdcontada;
        public virtual int QtdContada
        {
            get { return qtdcontada; }
            set { qtdcontada = value; }
        }

        //==================================================================//
        public virtual string Medicamento 
        {
            get { return LoteMedicamento.Medicamento.Nome; }
        }

        public virtual string Lote 
        {
            get { return lotemedicamento.Lote; }
        }

        public virtual string Fabricante 
        {
            get { return LoteMedicamento.Fabricante.Nome; }
        }

        public virtual string UnidadeMedicamento 
        {
            get { return LoteMedicamento.Medicamento.UnidadeMedida.Sigla; }
        }

        public virtual string CodigoLote 
        {
            get { return LoteMedicamento.Codigo.ToString(); }
        }

        public virtual DateTime ValidadeLote 
        {
            get { return LoteMedicamento.Validade; }
        }
        //==================================================================//

        public ItemInventario()
        {

        }

        public override bool Equals(object obj)
        {
            return this.Inventario.Codigo == ((ItemInventario)obj).Inventario.Codigo &&
                   this.LoteMedicamento.Codigo == ((ItemInventario)obj).LoteMedicamento.Codigo;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 47;
        }
    }
}
