using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;

namespace ViverMais.DAO.Localidade
{
    public class DistritoDAO : ViverMaisServiceFacadeDAO, IDistrito
    {
        public DistritoDAO() //: base("ViverMais")
        {

        }

        IList<T> IDistrito.BuscarPorMunicipio<T>(string co_municipio)
        {
            return Session.CreateQuery("FROM ViverMais.Model.Distrito AS d WHERE d.Municipio.Codigo = '" + co_municipio + "' AND d.Nome <> 'NÃO SE APLICA' ORDER BY d.Nome").List<T>();
        }

        //#region IServiceFacade Members

        //T ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.BuscarPorCodigo<T>(object codigo)
        //{
        //    return FindByPrimaryKey<T>(codigo);
        //}

        //void ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.Salvar(object obj)
        //{
        //    throw new NotImplementedException();
        //}

        //IList<T> ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.ListarTodos<T>()
        //{
        //    return this.FindAll<T>();
        //}

        //#endregion
    }
}
