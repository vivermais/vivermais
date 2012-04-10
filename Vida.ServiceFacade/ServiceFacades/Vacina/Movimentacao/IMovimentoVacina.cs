﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Vacina.Movimentacao
{
    public interface IMovimentoVacina : IServiceFacade
    {
        IList<T> BuscarMovimentacao<T>(int co_sala, int co_tipo);
        IList<T> BuscarMovimentacao<T>(int co_sala, int co_tipo, DateTime datainicio, DateTime datafim);
        IList<T> BuscarMovimentacao<T>(int co_sala, int co_tipo, int co_saladestino);
        IList<T> BuscarMovimentacao<T>(int co_sala, int co_tipo, int co_saladestino, DateTime datainicio, DateTime datafim);
        IList<T> BuscarItensMovimentacao<T>(long co_movimentacao);
        IList<T> BuscarRemanejamentosSalaDestino<T>(int co_sala, char status);
        IList<T> BuscarRemanejamentosSalaDestino<T>(int co_sala);
        IList<T> BuscarItensRemanejamento<T>(long co_remanejamento);
        IList<T> BuscarRemanejamentoPorSala<T>(int co_sala);
        IList<T> BuscarRemanejamentoPorSala<T>(int co_sala, char status);
        IList<T> BuscarHistoricoAlteracaoItemMovimento<T>(long co_itemmovimento);

        T BuscarItemMovimento<T>(long co_movimento, long co_lote);
        T BuscarRemanejamentoPorMovimentacao<T>(long co_movimentacao);
        T BuscarItemRemanejamentoPorMovimentoLote<T>(long co_movimento, long co_lote);
        T BuscarPorNumero<T, U>(long numero, U _usuario);

        void ConfirmarRemanejamento<T>(T itemremanejamento, bool fecharremanejamento);

        bool AlterarQuantidadeItemMovimento<T,A,U>(ref T _itemmovimento, A _itemremanejamento, int novaquantidade, string motivo, U _usuario);
    }
}