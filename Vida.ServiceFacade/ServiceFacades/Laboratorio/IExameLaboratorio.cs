using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Laboratorio
{
    public interface IExameLaboratorio:ILaboratorioServiceFacade
    {
        List<T> BuscarPorNomeUsual<T>(string nomeUsual);
        List<T> BuscarPorSg<T>(string mnemonico);
    }
}
