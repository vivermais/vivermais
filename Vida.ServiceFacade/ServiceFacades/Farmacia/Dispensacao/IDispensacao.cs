﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ViverMais.ServiceFacade.ServiceFacades.Farmacia.Dispensacao
{
    public interface IDispensacao : IFarmaciaServiceFacade
    {
        //Fer o que fica aqui e o que será transferido
        //para IReceita
        IList<T> BuscarItensDispensacaoPorPaciente<T>(string codigoPaciente);
        T BuscarPorItem<T>(long codigoReceita, int codigoLoteMedicamento, DateTime dataAtendimento);
        T BuscarPorProfissionalPaciente<T>(string id_profissional, string id_paciente, DateTime data);
        DataTable BuscarPorDispensacaoAgrupado(int id_dispensacao, string[] campos);
        IList<T> BuscarItensPorAtendimento<T>(DateTime dataAtendimento, long co_receita, int co_farmacia);
        IList<T> BuscarItensPorAtendimentoEmedicamento<T>(int id_dispensacao, int id_farmacia, int id_medicamento);
        T BuscarPrimeiroItemDispensadoPorReceita<T>(long co_receita, int co_medicamento);
        void SalvarItensDispensacao<T>(IList<T> itens);
        void SalvarItemDispensacao<T>(T item);
        void AlterarItemDispensacao<T>(T item, int qtdAnterior);
        void DeletarItemDispensacao<T>(T item);
        int QuantidadeDispensadaMedicamentoReceita(long co_receita, int co_medicamento);
        IList<T> BuscarDispensacaoPorReceita<T>(long co_receita);
        DateTime BuscarDataAtendimentoRecente(string codigoPaciente, int co_medicamento);
        DateTime BuscarDataAtendimentoRecente(long co_receita);
        object[] BuscarUltimoAtendimento(string codigoPaciente, int co_medicamento, DateTime dataAtendimento);

        //Novas
        T BuscarUltimaComMedicamento<T>(string codigoPaciente, int codigoMedicamento);//ACHO QUE N PRECISA MAIS
        List<T> BuscarItensDispensacao<T>(int codigoDispensacao);
        IList<T> BuscarDispensacoesPorReceita<T>(long co_receita);
    }
}
