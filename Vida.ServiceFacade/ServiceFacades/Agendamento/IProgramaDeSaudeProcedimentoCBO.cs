﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IProgramaDeSaudeProcedimentoCBO : IAgendamentoServiceFacade
    {
        IList<T> ListarPorPrograma<T>(int id_programa, bool ativo);
        void DeletarProgramaDeSaudeProcedimentoCBOPorPrograma(int id_programa);
        T BuscaVinculo<T>(int id_programa, string id_procedimento);
        IList<T> RetornaCBOsDoPrograma_Procedimento<T>(int id_programa, string id_procedimento);
        void DeletarProgramaDeSaudeProcedimentoCBO(int id_programa, string id_procedimento);
        void DeletarCbo(int id_programa, string id_procedimento, string id_cbo);
    }
}
