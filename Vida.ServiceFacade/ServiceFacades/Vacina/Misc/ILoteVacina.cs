using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc
{
    public interface ILoteVacina: IVacinaServiceFacade
    {
        bool ValidaCadastroLote<T>(string identificacao, DateTime datavalidade, int co_itemvacina, long co_lote);
        bool ValidaCadastroLote<T>(string identificacao, long co_lote);

        IList<T> BuscarLote<T>(string lote, DateTime datavalidade, int co_vacina, int co_fabricante, int numeroaplicacoes);
        IList<T> BuscarLotesValidos<T>(int co_vacina, DateTime data);
        IList<T> BuscarLotesValidos<T>(DateTime data);
        IList<T> BuscarLotesValidos<T>(string lote, DateTime datavalidade, int co_vacina, int co_fabricante, int numeroaplicacoes, DateTime aberturainventario);
        IList<T> BuscarLotesQuantidadeDisponivel<T>(int co_sala);
        IList<T> BuscarLotesQuantidadeDisponivel<T>(int co_sala, string lote, DateTime datavalidade, int co_vacina, int co_fabricante, int qtdaplicacao);
    }
}
