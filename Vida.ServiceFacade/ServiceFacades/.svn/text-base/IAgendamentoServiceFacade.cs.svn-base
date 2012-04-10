using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vida.ServiceFacade.ServiceFacades;
using System.Collections;

namespace Vida.ServiceFacade.ServiceFacades
{
    public interface IAgendamentoServiceFacade : IServiceFacade
    {
        int BuscarAfastamentoEAS(string id_unidade);
        
        T VerificaEASInativo<T>(string id_unidade, DateTime data);
        T VerificaProfissionalInativo<T>(string id_unidade, string id_profissional, DateTime data);
        //IList<T> BuscaSolicitacaoPorPaciente<T>(string id_paciente);
        T BuscarPercentual<T>(string unidade, string procedimento);
        IList<T> BuscarProcedimentoPorOcupacao<T>(string cbo);
        IList<T> ListarFaturas<T>(string id_unidade, int competencia);
        T BuscarFatura<T>(string id_unidade, int competencia, string tipo);
        IList<T> BuscarMovimento<T>(int id_fatura);  
        IList<T> ListarBPC<T>(string id_unidade, int competencia);
    }
}
