﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface ILaudo : IServiceFacade
    {
        //IList<T> BuscarFPO<T>(string id_unidade, int competencia);
        //T BuscarFpoCompetencia<T>(string unidade, string procedimento, int competencia);
        //T BuscarCompetencia<T>(string unidade);
        IList<T> BuscaPorSolicitacao<T>(string id_solicitacao);
        T VerificaDuplicidade<T>(string nomeArquivo);
    }
}
