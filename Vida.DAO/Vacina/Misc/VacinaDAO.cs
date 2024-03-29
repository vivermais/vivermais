﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.Model;
using NHibernate.Proxy;
using NHibernate.Persister.Entity;

namespace ViverMais.DAO.Vacina.Misc
{
    public class VacinaDAO : VacinaServiceFacadeDAO, IVacina
    {
        IList<T> IVacina.BuscarPorUnidadeMedida<T>(int co_unidademedida)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Vacina AS v WHERE v.UnidadeMedida.Codigo = " + co_unidademedida;
            return Session.CreateQuery(hql).List<T>();
        }

        /// <summary>
        /// Retorna as doses de uma vacina.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="co_vacina"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        IList<T> IVacina.BuscarDoses<T>(int co_vacina)
        {
            string hql = string.Empty;

            hql = "SELECT dose FROM ViverMais.Model.DoseVacina AS dose, ViverMais.Model.ItemDoseVacina AS item";
            hql += " WHERE item.Vacina.Codigo = " + co_vacina;
            hql += " AND dose.Codigo = item.DoseVacina.Codigo";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IVacina.BuscarItensDoseVacina<T>(int co_vacina)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ItemDoseVacina as item";
            hql += " WHERE item.Vacina.Codigo = " + co_vacina + " ORDER BY item.DoseVacina.Descricao";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IVacina.BuscarVacinasDoCalendario<T>()
        {
            string hql = "From ViverMais.Model.Vacina as vacina where vacina.PertenceAoCalendario = 1 and vacina.Ativo = 'S'";
            return Session.CreateQuery(hql).List<T>();
        }

        bool IVacina.ValidarCadastroVacina(string codigocmadi, int co_vacina)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Vacina AS v WHERE v.CodigoCMADI = '" + codigocmadi + "'";
            hql += " AND v.Codigo <> " + co_vacina;
            return Session.CreateQuery(hql).List<ViverMais.Model.Vacina>().Count > 0 ? false : true;
        }

        void IVacina.SalvarVacina<V, D, E, IV>(V _vacina, IList<D> _dosesvacina, IList<E> _estrategiasvacina, IList<IV> _itensvacina, int co_usuario)
        {
            using (Session.BeginTransaction())
            {
                ViverMais.Model.Vacina vacina = (ViverMais.Model.Vacina)(object)_vacina;
                IList<DoseVacina> dosesvacina = (IList<DoseVacina>)(object)_dosesvacina;
                IList<Estrategia> estrategiasvacina = (IList<Estrategia>)(object)_estrategiasvacina;
                IList<ItemVacina> itensvacina = (IList<ItemVacina>)(object)_itensvacina;

                try
                {
                    vacina = (ViverMais.Model.Vacina)Session.SaveOrUpdateCopy(vacina);

                    #region DOSES
                    IList<ItemDoseVacina> atuaisdosesvacinas = Factory.GetInstance<IVacina>().BuscarItensDoseVacina<ItemDoseVacina>(vacina.Codigo);
                    var dosesvacinaexcluir = from dose in atuaisdosesvacinas
                                             where !dosesvacina.Select(p => p.Codigo).ToList().Contains(dose.DoseVacina.Codigo)
                                             select dose;

                    foreach (var excluirdose in dosesvacinaexcluir)
                    {
                        IList<ParametrizacaoVacina> parametros = Factory.GetInstance<IParametrizacaoVacina>().BuscarPorDoseVacina<ParametrizacaoVacina>(excluirdose.DoseVacina.Codigo, excluirdose.Vacina.Codigo);

                        foreach (ParametrizacaoVacina p in parametros)
                            Session.Delete(p);

                        Session.Delete(Session.Merge(excluirdose));
                    }

                    var dosesvacinaatualizacao = from dose in atuaisdosesvacinas
                                                 where dosesvacina.Select(p => p.Codigo).ToList().Contains(dose.DoseVacina.Codigo)
                                                 && !dosesvacinaexcluir.Select(p => p.DoseVacina.Codigo).Contains(dose.DoseVacina.Codigo)
                                                 select dose;

                    var dosesvacinaincluir = from dose in dosesvacina
                                             where !dosesvacinaexcluir.Select(p => p.DoseVacina.Codigo).Contains(dose.Codigo)
                                             && !dosesvacinaatualizacao.Select(p => p.DoseVacina.Codigo).Contains(dose.Codigo)
                                             select dose;

                    foreach (var dose in dosesvacinaincluir)
                    {
                        ItemDoseVacina itemdose = new ItemDoseVacina();
                        itemdose.DoseVacina = dose;
                        itemdose.Vacina = vacina;
                        Session.Save(itemdose);
                    }
                    #endregion

                    #region ESTRATÉGIAS
                    IList<Estrategia> estrategiasatuais = Factory.GetInstance<IEstrategiaImunobiologico>().BuscarPorVacina<Estrategia>(vacina.Codigo);
                    var estrategiasexcluir = from estrategia in estrategiasatuais
                                             where !estrategiasvacina.Select(p => p.Codigo).ToList().Contains(estrategia.Codigo)
                                             select estrategia;

                    foreach (Estrategia estrategia in estrategiasexcluir)
                    {
                        estrategia.Vacinas.Remove(vacina);
                        Session.Update(Session.Merge(estrategia));
                    }

                    var estrategiasatualizacao = from estrategia in estrategiasatuais
                                                 where estrategiasvacina.Select(p => p.Codigo).ToList().Contains(estrategia.Codigo)
                                                 && !estrategiasexcluir.Select(p => p.Codigo).ToList().Contains(estrategia.Codigo)
                                                 select estrategia;

                    var estrategiasincluir = from estrategia in estrategiasvacina
                                             where !estrategiasexcluir.Select(p => p.Codigo).ToList().Contains(estrategia.Codigo)
                                             && !estrategiasatualizacao.Select(p => p.Codigo).ToList().Contains(estrategia.Codigo)
                                             select estrategia;

                    foreach (Estrategia estrategia in estrategiasincluir)
                    {
                        estrategia.Vacinas.Add(vacina);
                        Session.Update(Session.Merge(estrategia));
                    }
                    #endregion

                    #region ITEM VACINA
                        IList<ItemVacina> itensatuais = Factory.GetInstance<IItemVacina>().ListarPorVacina<ItemVacina>(vacina.Codigo);

                        var itensexcluir = from excluiritem in itensatuais
                                           where !itensvacina.Select(p=>p.Codigo).ToList().Contains(excluiritem.Codigo)
                                           select excluiritem;

                        foreach (ItemVacina item in itensexcluir)
                            Session.Delete(Session.Merge(item));

                        foreach (ItemVacina item in itensvacina)
                        {
                            item.Vacina = vacina;
                            Session.SaveOrUpdate(Session.Merge(item));
                        }

                    #endregion

                    Session.Save(new LogVacina(DateTime.Now, co_usuario, 18, "id vacina: " + vacina.Codigo.ToString()));

                    Session.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    Session.Transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
