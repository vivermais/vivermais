using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ItemInventarioVacina
    {
        InventarioVacina inventario;
        public virtual InventarioVacina Inventario
        {
            get { return inventario; }
            set { inventario = value; }
        }

        LoteVacina lotevacina;
        public virtual LoteVacina LoteVacina
        {
            get { return lotevacina; }
            set { lotevacina = value; }
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

        public ItemInventarioVacina()
        {
        }

        private long codigo;
        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        //public override bool Equals(object obj)
        //{
        //    return this.Inventario.Codigo == ((ItemInventarioVacina)obj).Inventario.Codigo &&
        //           this.LoteVacina.Codigo == ((ItemInventarioVacina)obj).LoteVacina.Codigo;
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode() * 13;
        //}

        public virtual string NomeVacina
        {
            get { return LoteVacina.ItemVacina.Vacina.Nome; }
        }

        public virtual string IdentificacaoLote
        {
            get { return LoteVacina.Identificacao; }
        }

        public virtual string NomeFabricanteVacina
        {
            get { return LoteVacina.ItemVacina.FabricanteVacina.Nome; }
        }

        public virtual string UnidadeMedidaVacina
        {
            get { return LoteVacina.ItemVacina.Vacina.UnidadeMedida.Sigla; }
        }

        public virtual int AplicacaoVacina
        {
            get { return LoteVacina.ItemVacina.Aplicacao; }
        }

        public virtual string CodigoLote
        {
            get { return LoteVacina.Codigo.ToString(); }
        }

        public virtual DateTime ValidadeLote
        {
            get { return LoteVacina.DataValidade; }
        }
    }
}
