using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Vida.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc;

namespace Vida.DAO.AtendimentoMedico.Misc
{
    public class SituacaoAtendimentoDAO : UrgenciaServiceFacadeDAO, ISituacaoAtendimento
    {
        public SituacaoAtendimentoDAO()// : base("urgencia")
        {

        }


        //#region IServiceFacade Members

        //T Vida.ServiceFacade.ServiceFacades.IServiceFacade.BuscarPorCodigo<T>(object codigo)
        //{
        //    return FindByPrimaryKey<T>(codigo);
        //}

        //void Vida.ServiceFacade.ServiceFacades.IServiceFacade.Salvar(object obj)
        //{
        //    this.Save(obj);
        //}

        //IList<T> Vida.ServiceFacade.ServiceFacades.IServiceFacade.ListarTodos<T>()
        //{
        //    return FindAll<T>();
        //}

        //#endregion
    }
}
