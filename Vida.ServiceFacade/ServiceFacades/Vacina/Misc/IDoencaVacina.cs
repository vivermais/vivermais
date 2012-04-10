using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc
{
    public interface IDoencaVacina: IVacinaServiceFacade
    {
        bool ValidarCadastroDoenca(int co_doenca, string nome);
    }
}
