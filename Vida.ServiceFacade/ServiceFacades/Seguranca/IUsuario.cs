using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ViverMais.ServiceFacade.ServiceFacades.Seguranca
{
    public interface IUsuario : IServiceFacade
    {
        IList<T> BuscarPorModulo<T>(int co_modulo);
        IList<T> BuscarPorModulo<T>(int co_modulo, string cnesunidade);
        IList<T> BuscarPorModulo<T,M>(IList<M> _modulos, string cnesunidade);
        IList<T> BuscarPorModulo<T, M>(IList<M> _modulos, string cnesunidade, string datanascimento, string nome, string cartaosus, string co_municipio);
        IList<T> BuscarPorCartaoSUS<T>(string cartaoSUS);
        IList<T> BuscarUsuariosPorCNES<T>(string cnes);
        IList<T> BuscarUsuarioPorNomeDataNascimento<T>(string nome, DateTime nascimento);
        IList<T> ListarPerfisUsuario<T>(int co_usuario);
        IList<T> ListarUsuariosPorPerfil<T>(int id_perfil);

        void SalvarUsuario<U>(U usuario, int co_usuario, int operacao);
        void AlterarUsuario<T>(T usuario);
    }
}
