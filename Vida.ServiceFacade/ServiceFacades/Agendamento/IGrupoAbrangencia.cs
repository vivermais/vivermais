using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IGrupoAbrangencia : IViverMaisServiceFacade
    {
        IList<T> ListarMunicipiosPorGrupoAbrangencia<T>(string codigoGrupo);
        IList<T> ListarGruposAtivos<T>();
        IList<T> ListarGruposInativos<T>();
        IList<T> ListarGrupoPorMunicipio<T>(string codigoMunicipio);
        T BuscarGrupoPorNome<T>(string nome);
        void AddMunicipioAoGrupo(string id_grupo, string id_municipio);
        void DeletarMunicipioDoGrupo(string id_grupo, string id_municipio);
        void DeletarGrupoAbrangencia(string id_grupo);
    }
}
