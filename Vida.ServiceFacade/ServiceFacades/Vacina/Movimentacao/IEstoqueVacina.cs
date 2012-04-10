using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Vacina.Movimentacao
{
    public interface IEstoqueVacina: IServiceFacade
    {
        IList<T> BuscarPorSalaVacina<T>(int co_salavacina);
        IList<T> BuscarPorSalaVacinaValidadeLoteSuperior<T>(int co_salavacina, DateTime validadelote);

        int QuantidadeDisponivelEstoque(long co_lote, int co_sala);
        
        void SalvarMovimentacao<M, I>(M _movimento, IList<I> _itens, int co_usuario);
    }
}
