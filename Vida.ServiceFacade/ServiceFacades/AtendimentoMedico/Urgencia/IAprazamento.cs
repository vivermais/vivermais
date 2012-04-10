﻿using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Collections;

namespace ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia
{
    public interface IAprazamento : IUrgenciaServiceFacade
    {
        T BuscarAprazamentoMedicamento<T>(long co_prescricao,int co_medicamento,DateTime horario);
        T BuscarAprazamentoProcedimento<T>(long co_prescricao, string co_procedimento, DateTime horario);
        T BuscarAprazamentoProcedimentoNaoFaturavel<T>(long co_prescricao, int co_procedimentonaofaturavel, DateTime horario);

        bool AprazamentoMedicamentoAnteriorNaoExecutado(long co_prescricao, int co_medicamento, DateTime horario);
        bool AprazamentoProcedimentoAnteriorNaoExecutado(long co_prescricao, string co_procedimento, DateTime horario);
        bool AprazamentoProcedimentoNaoFaturavelAnteriorNaoExecutado(long co_prescricao, int co_procedimento, DateTime horario);

        void AprazarMedicamento<T>(T _aprazamento, bool aprazarautomatico, int co_usuario);
        void AprazarProcedimento<T>(T _aprazamento, bool aprazarautomatico, int co_usuario);
        void AprazarProcedimentoNaoFaturavel<T>(T _aprazamento, bool aprazarautomatico, int co_usuario);
        void ExcluirAprazamentosNaoExecutadosMedicamento<T>(long co_prescricao, int co_medicamento);
        void ExcluirAprazamentosNaoExecutadosProcedimentoNaoFaturavel<T>(long co_prescricao, int co_procedimento);
        void ExcluirAprazamentosNaoExecutadosProcedimento<T>(long co_prescricao, string co_procedimento);

        //Hashtable RetornaTabelaAprazamento(long co_prescricao, DateTime data);

        IList<T> BuscarAprazamentosBPAI<T>(string co_unidade, int competencia, DateTime datainicio, DateTime datalimite);
        IList<T> BuscarAprazamentosBPAC<T>(string co_unidade, int competencia, DateTime datainicio, DateTime datalimite);
        IList<T> BuscarAprazamentoMedicamento<T>(long co_prescricao);
        IList<T> BuscarAprazamentoProcedimento<T>(long co_prescricao);
        IList<T> BuscarAprazamentoProcedimentoNaoFaturavel<T>(long co_prescricao);
        IList<T> BuscarAprazamentoMedicamento<T>(long co_prescricao, DateTime data);
        IList<T> BuscarAprazamentoProcedimento<T>(long co_prescricao, DateTime data);
        IList<T> BuscarAprazamentoProcedimentoNaoFaturavel<T>(long co_prescricao, DateTime data);
        IList<T> BuscarAprazamentoMedicamento<T>(long co_prescricao, int co_medicamento);
        IList<T> BuscarAprazamentoProcedimento<T>(long co_prescricao, string co_procedimento);
        IList<T> BuscarAprazamentoProcedimentoNaoFaturavel<T>(long co_prescricao, int co_procedimento);
        IList<T> BuscarAprazamentoMedicamento<T>(long co_prescricao, char status);
        IList<T> BuscarAprazamentoProcedimento<T>(long co_prescricao, char status);
        IList<T> BuscarAprazamentoProcedimentoNaoFaturavel<T>(long co_prescricao, char status);
    }
}
