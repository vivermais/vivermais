using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia
{
    public interface IFaturaBPAUrgencia : IServiceFacade
    {
        T BuscarPorCompetencia<T>(int competencia, string co_unidade, char tipo);
        T BuscarPrimeiraFatura<T>(string co_unidade, char tipo);
        T BuscarUltimaFatura<T>(string co_unidade, char tipo);

        IList<T> BuscarPorUnidade<T>(string co_unidade, char tipo);
    }
}
