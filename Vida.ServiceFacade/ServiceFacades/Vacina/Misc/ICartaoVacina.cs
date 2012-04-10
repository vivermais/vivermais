using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc
{
    public interface ICartaoVacina : IServiceFacade
    {
        IList<T> BuscarPorPaciente<T>(string co_paciente);
        IList<T> BuscarPorDoseVacina<T>(int co_vacina, int co_dose, string co_paciente);
        IList<T> BuscarPorPacienteLoteVacina<T>(int co_paciente, int co_lote, int co_vacina);

        Hashtable RetornaCartoesPaciente(string co_paciente);
        
        bool ValidarAtualizacaoCartaoVacina(string co_usuario, DateTime dataaplicacao, int co_vacina, int co_dose);
        
        T BuscarPorItemDispensacao<T>(long co_item);
    }
}
