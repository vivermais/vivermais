using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc
{
    public interface ISalaVacina: IVacinaServiceFacade
    {
        IList<T> BuscarPorUnidadeSaude<T>(string co_unidade);
        IList<T> BuscarPorUsuario<T, U>(U _usuario, bool verificar_permissao_escolher_qualquer_sala_vacina, bool adicionar_sala_default);
        IList<T> ListarUsuariosPorSala<T,S>(S _sala);
        IList<T> BuscarPorNome<T>(string nome);
        IList<T> BuscarUnidadesPorDistritoSala<T>(int co_distrito);
        IList<T> BuscarPorUnidadesPesquisadas<T>(IList<T> unidades);

        IList<int> SalasPertencesCMADI();

        void SalvarSala<T, U>(T _sala, IList<U> _usuarios, int co_usuario);
    }
}
