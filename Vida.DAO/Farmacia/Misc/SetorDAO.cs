﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using ViverMais.Model;

namespace ViverMais.DAO.Farmacia.Misc
{
    public class SetorDAO : FarmaciaServiceFacadeDAO, ISetor
    {
        IList<T> ISetor.BuscarPorDescricao<T>(string nome) 
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Setor AS s WHERE s.Nome LIKE '" + nome + "%'";
            hql += " ORDER BY s.Nome";

            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ISetor.BuscarPorEstabelecimento<T>(string co_unidade) 
        {
            string hql = string.Empty;
            hql = "SELECT s.Setor FROM ViverMais.Model.SetorUnidade AS s WHERE s.CodigoUnidade = '" + co_unidade + "'";

            return Session.CreateQuery(hql).List<T>();
        }

        void ISetor.AssociarSetorUnidade<S>(IList<S> _setoresatuais, IList<S> _setoresnovos)
        {
            IList<SetorUnidade> setoresatuais = (IList<SetorUnidade>)(object)_setoresatuais;
            IList<SetorUnidade> setoresnovos = (IList<SetorUnidade>)(object)_setoresnovos;

            using (Session.BeginTransaction())
            {
                try
                {
                    foreach (SetorUnidade s in setoresnovos)
                    {
                        if (setoresatuais.Where(p => p.Setor.Codigo == s.Setor.Codigo).FirstOrDefault() == null) //Se o elemento não existir na nova lista inseri-lo
                            Session.Save(s);
                    }

                    foreach (SetorUnidade s in setoresatuais)
                    {
                        if (setoresnovos.Where(p => p.Setor.Codigo == s.Setor.Codigo).FirstOrDefault() == null) //Se um elemento anterior não existir na nova lista remova-o
                            Session.Delete(s);
                    }

                    Session.Transaction.Commit();
                    //Session.Flush();
                }
                catch (Exception f)
                {
                    Session.Transaction.Rollback();
                    throw f;
                }
            }
        }
    }
}
