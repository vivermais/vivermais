using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc
{
    public interface ICameraIP: IServiceFacade
    {
        IList<T> BuscarPorUnidade<T>(string cnes);

        bool ValidarCadastro(string endereco, int codigo);
    }
}
