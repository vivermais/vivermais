﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.Model;

namespace ViverMais.DAO.Procedimento.Misc
{
    public class RegistroDAO : ViverMaisServiceFacadeDAO, IRegistro
    {
        T IRegistro.BuscarPorCodigo<T>(int co_registro)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Registro AS r WHERE r.Codigo = '" + co_registro.ToString("00") + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        IList<T> IRegistro.BuscarPorProcedimento<T>(string co_procedimento)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ProcedimentoRegistro AS pr WHERE pr.Procedimento.Codigo = '" + co_procedimento + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IRegistro.BuscarPorProcedimento<T>(string co_procedimento,int tipo)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ProcedimentoRegistro AS pr WHERE pr.Procedimento.Codigo='"+co_procedimento + "'";
            hql += " and pr.Registro.Codigo='"+ tipo.ToString("00") +"'";
            return Session.CreateQuery(hql).List<T>();
        }

        bool IRegistro.ProcedimentoExigeCid(string co_procedimento)
        {
            string hql = string.Empty;
            hql += "SELECT p.Procedimento.Codigo FROM ViverMais.Model.ProcedimentoRegistro p WHERE p.Procedimento.Codigo='" + co_procedimento + "'";
            hql += " AND p.DataCompetencia IS NOT NULL AND p.DataCompetencia=(SELECT MAX(p2.DataCompetencia) FROM ViverMais.Model.ProcedimentoRegistro p2";
            hql += " WHERE p2.Procedimento.Codigo = '" + co_procedimento + "' AND (p2.Registro.Codigo='" + Registro.BPA_INDIVIDUALIZADO.ToString("00") + "' OR p2.Registro.Codigo='" + Registro.APAC_PROC_PRINCIPAL.ToString("00") + "'))";
            hql += " AND (p.Registro.Codigo='" + Registro.BPA_INDIVIDUALIZADO.ToString("00") + "' OR p.Registro.Codigo='" + Registro.APAC_PROC_PRINCIPAL.ToString("00") + "')";
            return Session.CreateQuery(hql).List().Count > 0 ? true : false;
        }
    }
}