using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class EventoFarmacia
    {
        public static readonly int ENCERRAR_INVENTARIO = 11;
        public static readonly int EXCLUIR_UNIDADE_MEDIDA = 7;
        public static readonly int MANTER_FABRICANTE = 1;
        public static readonly int MANTER_FARMACIA = 2;
        public static readonly int MANTER_FORNECEDOR = 13;
        public static readonly int CADASTRAR_RECEITA = 24;
        public static readonly int EDITAR_RECEITA = 32;
        public static readonly int ABERTURA_INVENTARIO = 8;
        public static readonly int ALTERAR_ITEM_INVENTARIO = 9;
        public static readonly int INSERIR_ITEM_INVENTARIO = 10;
        public static readonly int INSERIR_ITEM_NOTA_FISCAL = 17;
        public static readonly int ENCERRAR_NOTA_FISCAL = 20;
        public static readonly int ALTERAR_ITEM_NOTA_FISCAL = 18;
        public static readonly int EXCLUIR_ITEM_NOTA_FISCAL = 19;
        public static readonly int CONFIRMAR_RECEBIMENTO_ITEM_REMANEJAMENTO = 22;
        public static readonly int FINALIZAR_REMANEJAMENTO = 23;
        public static readonly int ALTERAR_ITEM_REQUISICAO_MEDICAMENTO = 28;
        public static readonly int EXCLUIR_ITEM_REQUISICAO_MEDICAMENTO = 29;
        public static readonly int INCLUIR_ITEM_REQUISICAO_MEDICAMENTO = 27;
        public static readonly int ENVIAR_REQUISICAO_DISTRITO = 26;
        public static readonly int MANTER_LOTE_MEDICAMENTO = 12;
        public static readonly int MANTER_MEDICAMENTO = 3;
        public static readonly int SALVAR_MOVIMENTACAO = 21;
        public static readonly int CADASTRAR_NOTA_FISCAL = 15;
        public static readonly int ALTERAR_NOTA_FISCAL = 16;
        public static readonly int MANTER_RESPONSAVEL_ATESTO = 14;
        public static readonly int ABRIR_REQUISICAO_MEDICAMENTO = 25;
        public static readonly int MANTER_SETOR = 4;
        public static readonly int MANTER_UNIDADE_MEDIDA = 6;
        public static readonly int VINCULAR_UNIDADE_DISTRITO = 30;
        public static readonly int EXCLUIR_VINCULAR_UNIDADE_DISTRITO = 31;
        public static readonly int ASSOCIAR_SETOR_UNIDADE = 5;

        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string nome;

        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public EventoFarmacia()
        {
        }
    }
}
