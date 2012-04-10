using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Farmacia.Dispensacao
{
    public interface IReceitaDispensacao : IFarmaciaServiceFacade
    {
        IList<T> BuscarReceitaPorPaciente<T>(string codigoPaciente);
        IList<T> BuscarMedicamentos<T>(long codigoReceita);        

        //RETIRADOS DE IDISPENSACAO
        T ValidarCadastroReceita<T>(string co_paciente, string co_profissional, DateTime datareceita);
    }
}
