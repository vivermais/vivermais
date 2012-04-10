using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class ItemRemanejamentoVacina
    {
        private long codigo;

        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        private RemanejamentoVacina remanejamento;

        public virtual RemanejamentoVacina Remanejamento
        {
            get { return remanejamento; }
            set { remanejamento = value; }
        }
        private LoteVacina lote;

        public virtual LoteVacina Lote
        {
            get { return lote; }
            set { lote = value; }
        }
        private int quantidaderegistrada;

        public virtual int QuantidadeRegistrada
        {
            get { return quantidaderegistrada; }
            set { quantidaderegistrada = value; }
        }
        private int quantidadeconfirmada;

        public virtual int QuantidadeConfirmada
        {
            get { return quantidadeconfirmada; }
            set { quantidadeconfirmada = value; }
        }
        private DateTime? dataconfirmacao;

        public virtual DateTime? DataConfirmacao
        {
            get { return dataconfirmacao; }
            set { dataconfirmacao = value; }
        }
        private Usuario usuarioconfirmacao;

        public virtual Usuario UsuarioConfirmacao
        {
            get { return usuarioconfirmacao; }
            set { usuarioconfirmacao = value; }
        }

        public ItemRemanejamentoVacina()
        {
        }

        public static int RetornaIndex(IList<ItemRemanejamentoVacina> itens, long co_item)
        {
            var item = itens.Select((Item, index) => new { index, Item }).Where
               (p => p.Item.Codigo  == co_item).FirstOrDefault();

            return item.index;
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

        public virtual DateTime DataValidadeLote
        {
            get { return this.Lote.DataValidade; }
        }

        public virtual string IdentificacaoLote
        {
            get { return this.Lote.Identificacao; }
        }

        public virtual bool RecebimentoConfirmado
        {
            get
            {
                return this.DataConfirmacao.HasValue;
            }
        }
    }
}
