using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using NHibernate;
using NHibernate.Criterion;

namespace ViverMais.DAO.Localidade
{
    public class MunicipioDAO : ViverMaisServiceFacadeDAO, IMunicipio
    {
        public MunicipioDAO() //: base("ViverMais")
        {

        }

        #region IMunicipio Members

        IList<T> IMunicipio.ListarPorEstado<T>(string siglaEstado)
        {
            string hql = "from ViverMais.Model.Municipio municipio where municipio.UF.Sigla = '" + siglaEstado + "'";
            hql += " order by municipio.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IMunicipio.BuscarPorNomeDaBahia<T>(string nome)
        {
            string hql = "from ViverMais.Model.Municipio municipio where municipio.UF.Sigla = 'BA'";
            //hql += " and municipio.Nome like '"+ nome+"%'";
            hql += " AND TRANSLATE(UPPER(municipio.Nome),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";
            hql += " LIKE ";
            hql += " TRANSLATE(UPPER('" + nome + "%'),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";
            return Session.CreateQuery(hql).List<T>();
        }

        #endregion

        //#region IServiceFacade Members

        //T ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.BuscarPorCodigo<T>(object codigo)
        //{
        //    return this.FindByPrimaryKey<T>(codigo);
        //}

        //void ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.Salvar(object obj)
        //{
        //    this.Save(obj);
        //}

        //IList<T> ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.ListarTodos<T>()
        //{
        //    return this.FindAll<T>();
        //}

        //#endregion
    }
}
