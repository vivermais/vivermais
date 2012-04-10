﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;
using ViverMais.Model;

namespace ViverMais.DAO.Profissional.Misc
{
    public class EquipeDAO: ViverMaisServiceFacadeDAO, IEquipe
    {
        #region IEquipe

            public EquipeDAO()
            {
            }

            E IEquipe.BuscarPorCodigo<A, C, E>(A area_equipe, C codigo_equipe)
            {
                Area area = (Area)((object)area_equipe);
                string codigo = (string)((object)codigo_equipe);

                string hql = string.Empty;
                hql = "FROM ViverMais.Model.EquipeProfissional AS ep WHERE ep.Equipe.Area.Codigo = '" + area.Codigo + "' AND ep.Equipe.Area.Municipio.Codigo = '" + area.Municipio.Codigo + "' AND ep.Equipe.Codigo = '" + codigo + "'";
                return this.Session.CreateQuery(hql).UniqueResult<E>();
            }

            IList<T> IEquipe.BuscarPorProfissional<T>(string co_profissional)
            {
                string hql = string.Empty;
                hql = "FROM ViverMais.Model.EquipeProfissional AS ep WHERE ep.Profissional.CPF = '" + co_profissional + "'";
                return this.Session.CreateQuery(hql).List<T>();
            }

            IList<T> IEquipe.BuscarPorOcupacao<T>(string co_ocupacao) 
            {
                string hql = string.Empty;
                hql = "FROM ViverMais.Model.EquipeProfissional AS ep WHERE ep.CBO.Codigo = '" + co_ocupacao + "'";
                return this.Session.CreateQuery(hql).List<T>();
            }

            T IEquipe.BuscarProfissionalPorCategoriaNumeroConselho<T>(string co_categoria, string numeroconselho)
            {
                string hql = string.Empty;
                hql = "SELECT e.Profissional FROM ViverMais.Model.EquipeProfissional AS e WHERE e.CBO.CategoriaOcupacao.Codigo = '" + co_categoria + "'";
                hql += " AND e.Profissional.RegistroConselho = '" + numeroconselho + "'";
                return Session.CreateQuery(hql).List<T>().FirstOrDefault();
            }

        #endregion
    }
}
