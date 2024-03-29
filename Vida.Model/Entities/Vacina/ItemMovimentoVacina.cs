﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class ItemMovimentoVacina
    {
        private long codigo;

        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        private MovimentoVacina movimento;

        public virtual MovimentoVacina Movimento
        {
            get { return movimento; }
            set { movimento = value; }
        }
        private LoteVacina lote;

        public virtual LoteVacina Lote
        {
            get { return lote; }
            set { lote = value; }
        }
        private int quantidade;

        public virtual int Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }

        public virtual string NomeVacina
        {
            get { return this.Lote.NomeVacina; }
        }

        public virtual string NomeFabricante
        {
            get { return this.Lote.NomeFabricante; }
        }

        public virtual int AplicacaoVacina
        {
            get { return this.Lote.AplicacaoVacina; }
        }

        public virtual string Identificacao
        {
            get { return this.Lote.Identificacao; }
        }

        public virtual DateTime DataValidade
        {
            get { return this.Lote.DataValidade; }
        }

        public virtual long CodigoLote
        {
            get { return this.Lote.Codigo; }
        }

        private bool editar = false;
        public virtual bool Editar
        {
            get { return editar; }
            set { editar = value; }
        }

        public ItemMovimentoVacina()
        {
        }

        public static int RetornaIndex(IList<ItemMovimentoVacina> itens, long co_lote)
        {
            var item = itens.Select((Item, index) => new { index, Item }).Where
               (p => p.Item.Lote.Codigo == co_lote).FirstOrDefault();

            return item.index;
        }

    }
}
