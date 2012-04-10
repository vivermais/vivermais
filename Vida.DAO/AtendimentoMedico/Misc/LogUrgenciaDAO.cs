﻿using System;
using NHibernate;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc;

namespace ViverMais.DAO.AtendimentoMedico.Misc
{
    public class LogUrgenciaDAO: UrgenciaServiceFacadeDAO, ILogUrgencia
    {
        #region ILogUrgencia
        
        IList<T> ILogUrgencia.BuscarPorUsuario<T>(int co_usuario) 
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.LogUrgencia AS lu WHERE lu.CodigoUsuario = " + co_usuario;
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ILogUrgencia.BuscarPorEvento<T>(int co_evento) 
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.LogUrgencia AS lu WHERE lu.Evento = " + co_evento;
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ILogUrgencia.BuscarPorData<T>(DateTime data)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.LogUrgencia AS lu WHERE TO_CHAR(lu.Data,'DD/MM/YYYY') = '" + data.ToString("dd/MM/yyyy") + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        #endregion
    }
}