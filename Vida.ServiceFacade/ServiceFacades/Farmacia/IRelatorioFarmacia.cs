﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ViverMais.ServiceFacade.ServiceFacades.Farmacia
{
    public interface IRelatorioFarmacia
    {
        IList ObterMovimentacaoDiaria(DateTime data, int codigoFarmacia);
        IList<T> ObterRelatorioPosicaoEstoqueLote<T>(int codigoFarmacia);
        IList<T> ObterRelatorioLotesAVencer<T>(int codigoFarmacia, DateTime data);
        IList ObterRelatorioConsumoMedioMensal(int codigoFarmacia, DateTime dataInicial, DateTime dataFinal);
        IList ObterRelatorioProducaoUsuario(int codigoUsuario, DateTime data);
        IList ObterRelatorioConsolidadoRM(int codigoDistrito, int mes, int ano);
        IList ObterRelatorioNotaFiscalLote(int codigoLoteMedicamento);
        IList ObterRelatorioValorUnitarioMedicamento(DateTime dataInicio, DateTime dataFim);
    }
}
