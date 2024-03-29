﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface ISubGrupoProcedimentoCbo : IAgendamentoServiceFacade
    {
        IList<T> ListarSubGrupoProcedimentoCbo<T>(bool status);
        T VerificaSeExisteAtivo<T>(int co_subGrupo, string co_procedimento, string co_ocupacao);
        IList<T> BuscarSubGrupoProcedimentoCbo<T>(int co_subGrupo, string co_procedimento, string co_ocupacao);
        IList<T> ListarSubGrupoPorProcedimentoECbo<T>(string co_procedimento, string co_ocupacao, bool ativo);
    }
}
