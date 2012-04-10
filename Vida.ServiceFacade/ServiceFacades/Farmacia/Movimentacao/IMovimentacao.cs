﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace ViverMais.ServiceFacade.ServiceFacades.Farmacia.Movimentacao
{
    public interface IMovimentacao : IServiceFacade
    {
        IList<T> BuscarPorTipoMovimento<T>(int co_tipomovimento);
        IList<T> BuscarItensPorMovimento<T>(int co_movimento);
        IList<T> BuscarPorFarmacia<T>(int co_farmacia);
        IList<T> BuscarItensRemanejamentoPorRemanejamento<T>(int co_remanejamento);
        IList<T> BuscarRemanejamentosPorFarmacia<T>(int co_farmacia);
        void ConfirmarRecebimentoMedicamento<T>(T medicamento);
        Hashtable RetornaHashMovimentacaoRemanejamento<T>(int co_tipo, T lista);
    }
}