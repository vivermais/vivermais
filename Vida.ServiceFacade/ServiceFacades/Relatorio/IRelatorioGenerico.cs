using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ViverMais.ServiceFacade.ServiceFacades.Relatorio
{
    public interface IRelatorioGenerico
    {
        DataTable ObterRelatorio(string tipo, string propriedade, int expressao, object valor);
        DataTable ObterRelatorio(string tipo, object[] expressions);
    }
}
