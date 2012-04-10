﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IProgramaDeSaude : IAgendamentoServiceFacade
    {
        //<T>
        //bool VerificaData(DateTime data);
        IList<T> ListarPacientesPorPrograma<T>(int co_programa, bool ativo);
        T BuscarProgramaDeSaudePaciente<T>(int co_prograSaude, string co_paciente);
        T BuscarPorNome<T>(string nomePrograma);
    }
}