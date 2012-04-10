using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

namespace ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia
{
    public interface IRelatorioUrgencia
    {
        DataTable ObterQuantitativoAtendimentoCID(object id_unidade, object cid, DateTime datainicial, DateTime datafinal);
        DataTable ObterRelatorioAtendimentoFaixa(string id_unidade, string sexo, DateTime dataInicio, DateTime dataFim);

        IList<T> ObterRelatorioPorSituacao<T>(object id_unidade, object id_situacao);
        IList<T> EstabelecimentosAtivos<T>();
        IList<T> DistritosAtivos<T>();
        IList ObterRelatorioAbsenteismo(string id_unidade, DateTime data_inicio, DateTime data_fim);
        IList ObterRelatorioAtendimentoPorFaixa(long id_unidade, string sexo, DateTime dataInicio, DateTime dataFim);
        IList ObterRelatorioLeitosFaixa(string id_unidade);

        Hashtable ObterRelatorioTempoPermanencia(DateTime dataInicial, DateTime dataFinal, string id_unidade);
        Hashtable RetornaHashTableProcedencia(string co_unidade, string co_cid, DateTime datainicio, DateTime datafim);
        //Hashtable RetornarHashTableAprazamento(long co_prescricao, DateTime data);

        bool PAAtivo(string cnes);

        string GraficoFilaAtendimentoUnidade(string cnes);

        T GerarBPAI<T>(string co_unidade, int competencia, DateTime datainicio, DateTime datalimite);
        T GerarBPAC<T>(string co_unidade, int competencia, DateTime datainicio, DateTime datalimite);

        ReportDocument ObterRelatorioAcolhimento(long co_prontuario);
        ReportDocument ObterRelatorioConsultaMedica(long co_prontuario);
        ReportDocument ObterRelatorioEvolucoesMedicas(long co_prontuario);
        ReportDocument ObterRelatorioEvolucoesEnfermagem(long co_prontuario);
        ReportDocument ObterRelatorioExamesInternos(long co_prontuario);
        ReportDocument ObterRelatorioPrescricoes(long co_prontuario);
        ReportDocument ObterRelatorioGeral(long co_prontuario, int co_usuario);
        ReportDocument ObterRelatorioFichaAtendimento(long numeroprontuario);
        ReportDocument ObterRelatorioAprazados(long co_prontuario);
        ReportDocument ObterRelatorioAprazados(long co_prescricao, DateTime data);
    }
}
