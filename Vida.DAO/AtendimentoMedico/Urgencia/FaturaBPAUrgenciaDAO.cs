﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.Model;

namespace ViverMais.DAO.AtendimentoMedico.Urgencia
{
    public class FaturaBPAUrgenciaDAO : UrgenciaServiceFacadeDAO, IFaturaBPAUrgencia
    {
        T IFaturaBPAUrgencia.BuscarPorCompetencia<T>(int competencia, string co_unidade, char tipo)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.FaturaBPAUrgencia f WHERE f.Competencia=" + competencia + " AND f.Unidade.CNES='" + co_unidade+ "'";
            hql += " AND f.Tipo='" + tipo + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        IList<T> IFaturaBPAUrgencia.BuscarPorUnidade<T>(string co_unidade, char tipo)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.FaturaBPAUrgencia f WHERE f.Unidade.CNES='" + co_unidade + "' AND f.Tipo='" + tipo + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        T IFaturaBPAUrgencia.BuscarPrimeiraFatura<T>(string co_unidade, char tipo)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.FaturaBPAUrgencia f WHERE f.Unidade.CNES='" + co_unidade + "' AND f.Tipo='" + tipo + "'";
            hql += " ORDER BY f.Data";
            return (T)(object)((IList<FaturaBPAUrgencia>)Session.CreateQuery(hql).List<FaturaBPAUrgencia>()).FirstOrDefault();
        }

        T IFaturaBPAUrgencia.BuscarUltimaFatura<T>(string co_unidade, char tipo)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.FaturaBPAUrgencia f WHERE f.Unidade.CNES='" + co_unidade + "' AND f.Tipo='" + tipo + "'";
            hql += " ORDER BY f.Data";
            return (T)(object)((IList<FaturaBPAUrgencia>)Session.CreateQuery(hql).List<FaturaBPAUrgencia>()).LastOrDefault();
        }

        //T IFaturaBPAUrgencia.BuscarFaturaAnteriorUltima<T>(string co_unidade, char tipo)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.FaturaBPAUrgencia f WHERE f.Unidade.CNES='" + co_unidade + "' AND f.Tipo='" + tipo + "'";
        //    hql += " ORDER BY f.Data";

        //    IList<FaturaBPAUrgencia> lista = (IList<FaturaBPAUrgencia>)Session.CreateQuery(hql).List<FaturaBPAUrgencia>();

        //    if (lista.Count() > 0 && lista
        //}

        public FaturaBPAUrgenciaDAO()
        {
        }
    }
}
