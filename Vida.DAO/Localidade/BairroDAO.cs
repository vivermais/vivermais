using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using ViverMais.Model;

namespace ViverMais.DAO.Localidade
{
    public class BairroDAO : ViverMaisServiceFacadeDAO, IBairro
    {
        public BairroDAO()
        {
        }

        #region IBairro Members

            IList<T> IBairro.ListarPorCidade<T>(string co_municipio)
            {
                string hql = string.Empty;
                hql  = "FROM ViverMais.Model.Bairro AS b WHERE b.Distrito.Municipio.Codigo = '" + co_municipio + "'";
                hql += " ORDER BY b.Nome";
                return Session.CreateQuery(hql).List<T>();
            }

            IList<T> IBairro.BuscarPorNome<T>(string nome)
            {
                string hql = string.Empty;
                hql = "FROM ViverMais.Model.Bairro AS b WHERE b.Nome = '" + nome + "'";
                return Session.CreateQuery(hql).List<T>();
            }

            IList<T> IBairro.BuscarPorDistrito<T>(int co_distrito)
            {
                string hql = string.Empty;
                hql = "FROM ViverMais.Model.Bairro AS b WHERE b.Distrito IS NOT NULL AND b.Distrito.Codigo=" + co_distrito;
                hql += " AND b.Nome <> 'NAO SE APLICA' AND b.Nome <> 'NÃO SE APLICA'";
                hql += " ORDER BY b.Nome";
                return Session.CreateQuery(hql).List<T>();
            }

        #endregion

        //#region IServiceFacade Members

        //T ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.BuscarPorCodigo<T>(object codigo)
        //{
        //    throw new NotImplementedException();
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
