﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vida.ServiceFacade.ServiceFacades.EstabelecimentoSaude.Misc;

namespace Vida.DAO.EstabelecimentoSaude.Misc
{
    public class DesativacaoEstabelecimentoDAO : VidaServiceFacadeDAO, IDesativacaoEstabelecimento
    {
        #region IDesativacaoEstabelecimento Members

        public DesativacaoEstabelecimentoDAO()
        {
        }

        T IDesativacaoEstabelecimento.PesquisarPorMotivo<T>(string ds_motivo) 
        {
            string hql = string.Empty;
            hql = "FROM Vida.Model.DesativacaoEstabelecimento AS de WHERE de.Descricao = '" + ds_motivo + "'";
            return this.Session.CreateQuery(hql).UniqueResult<T>();
        }
        
        #endregion

        //#region IServiceFacade Members

        //    T Vida.ServiceFacade.ServiceFacades.IServiceFacade.BuscarPorCodigo<T>(object codigo)
        //    {
        //        return FindByPrimaryKey<T>(codigo);
        //    }

        //    void Vida.ServiceFacade.ServiceFacades.IServiceFacade.Salvar(object obj)
        //    {
        //        this.Save(obj);
        //    }

        //    IList<T> Vida.ServiceFacade.ServiceFacades.IServiceFacade.ListarTodos<T>()
        //    {
        //        return FindAll<T>();
        //    }

        //#endregion
    }
}