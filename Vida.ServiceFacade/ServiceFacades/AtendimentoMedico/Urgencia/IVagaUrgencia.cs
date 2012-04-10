﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia
{
    public interface IVagaUrgencia: IServiceFacade
    {
        DataTable QuadroVagas(string co_unidade, bool formato_linha);
        
        void AtualizarVagas<T>(IList<T> vagas, IList<T> vagasatuais, string co_unidade);

        T VerificaDisponibilidadeVaga<T>(char tipovaga, string co_unidade);
        T BuscarPorProntuario<T>(long co_prontuario);
        T BuscarConfiguracaoPaPorUnidade<T>(string co_unidade);

        IList<T> Vagas<T>(int qtd_feminina, int qtd_masculina, int qtd_infantil, string co_unidade);
        IList<T> BuscarPorUnidade<T>(string co_unidade);
        IList<T> BuscarEspecialidadesAtendimento<T>(string co_unidade);
    }
}
