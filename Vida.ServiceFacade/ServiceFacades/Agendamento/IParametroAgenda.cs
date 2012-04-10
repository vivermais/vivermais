using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IParametroAgenda : IServiceFacade
    {
        IList<T> ListaEstabelecimentosComParametroDistrital<T>(int id_distrito);
        //IList<T> BuscarDuplicidade<T>(string unidade, string procedimento);
        //IList<T> BuscarParametros<T>(string id_unidade, string tipo_configuracao, string co_procedimento, string co_cbo, int co_subgrupo);
        IList<T> BuscarParametros<T>(string id_unidade, string tipo_configuracao, string co_procedimento, string co_cbo, string co_subgrupo);
        T BuscarAgenda<T>(string unidade, DateTime data, string turno, string procedimento, int cmp, string profissional, string cbo);
        T BuscarParametrosPorTipo<T>(string cnes, int tipoagenda, string tipo_configuracao);
        IList<T> BuscarUnidadesEspecificas<T>(string cnes, int tipoagenda);
        //T BuscarParametroAgendaUnidade(int co_parametroAgenda);
    }
}
