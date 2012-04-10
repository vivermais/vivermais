using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude.Misc;

namespace ViverMais.DAO.EstabelecimentoSaude
{
    public class DesativacaoEstabelecimentoDAO : ViverMaisServiceFacadeDAO, IDesativacaoEstabelecimento
    {
        #region IDesativacaoEstabelecimento Members

        public DesativacaoEstabelecimentoDAO()
        {
        }

        T IDesativacaoEstabelecimento.PesquisarPorMotivo<T>(string ds_motivo) 
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.DesativacaoEstabelecimento AS de WHERE de.Descricao = '" + ds_motivo + "'";
            return this.Session.CreateQuery(hql).UniqueResult<T>();
        }
        
        #endregion

        //#region IServiceFacade Members

        //    T ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.BuscarPorCodigo<T>(object codigo)
        //    {
        //        return FindByPrimaryKey<T>(codigo);
        //    }

        //    void ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.Salvar(object obj)
        //    {
        //        this.Save(obj);
        //    }

        //    IList<T> ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.ListarTodos<T>()
        //    {
        //        return FindAll<T>();
        //    }

        //#endregion
    }
}
