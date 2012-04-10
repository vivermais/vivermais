using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Laboratorio
{
    public interface ISolicitanteExameLaboratorio:ILaboratorioServiceFacade
    {
        T BuscaPorNumeroConselho<T>(int numeroConselho);
        List<T> BuscaPorNome<T>(string nome);
    }
}
