﻿using System;
using NHibernate;
using ViverMais.Model;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;

namespace ViverMais.DAO.AtendimentoMedico.Urgencia
{
    public class KitPADAO : UrgenciaServiceFacadeDAO, IKitPA
    {
        IList<T> IKitPA.BuscarItemPA<T>(int co_kit)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.KitItemPA AS k WHERE k.KitPA.Codigo = " + co_kit + " ORDER BY k.ItemPA.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IKitPA.BuscarMedicamentoPA<T>(int co_kit)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.KitMedicamentoPA AS k WHERE k.KitPA.Codigo = " + co_kit;

            IMedicamento iMedicamento = Factory.GetInstance<IMedicamento>();
            IList<KitMedicamentoPA> kits = (IList<KitMedicamentoPA>)(object)Session.CreateQuery(hql).List<T>();
            foreach (KitMedicamentoPA kit in kits)
                kit.Medicamento = iMedicamento.BuscarPorCodigo<Medicamento>(kit.CodigoMedicamento);

            return (IList<T>)kits;
        }

        void IKitPA.SalvarKit<K, II, MI>(K _kit, IList<II> _itens, IList<MI> _medicamentos) 
        {
            IKitPA iKit = Factory.GetInstance<IKitPA>();
            KitPA kit = (KitPA)(object)_kit;
            IList<KitItemPA> itens = (IList<KitItemPA>)(object)_itens;
            IList<KitMedicamentoPA> medicamentos = (IList<KitMedicamentoPA>)(object)_medicamentos;
            IList<KitItemPA> itensAntigos = iKit.BuscarItemPA<KitItemPA>(kit.Codigo);
            IList<KitMedicamentoPA> medicamentosAntigos = iKit.BuscarMedicamentoPA<KitMedicamentoPA>(kit.Codigo);

            using (Session.BeginTransaction())
            {
                try
                {
                    kit = (KitPA)Session.SaveOrUpdateCopy(kit);

                    foreach (KitItemPA item in (from i in itensAntigos where !itens.Select(p=>p.CodigoItem).Contains(i.CodigoItem)
                                                    select i).ToList())
                        Session.Delete(Session.Merge(item));

                    foreach (KitMedicamentoPA item in (from i in medicamentosAntigos where
                                                           !medicamentos.Select(p=>p.CodigoMedicamento).Contains(i.CodigoMedicamento)
                                                           select i).ToList())
                        Session.Delete(Session.Merge(item));

                    #region Novos
                    foreach (KitItemPA item in (from i in itens
                                                where !itensAntigos.Select(p => p.CodigoItem).Contains(i.CodigoItem)
                                                select i).ToList())
                    {
                        item.KitPA = kit;
                        Session.Save(item);
                    }

                    foreach (KitMedicamentoPA item in (from i in medicamentos
                                                       where
                                                           !medicamentosAntigos.Select(p => p.CodigoMedicamento).Contains(i.CodigoMedicamento)
                                                       select i).ToList())
                    {
                        item.KitPA = kit;
                        Session.Save(item);
                    }
                    #endregion

                    #region Atualizações
                    foreach (KitItemPA item in (from i in itensAntigos
                                                where itens.Select(p => p.CodigoItem).Contains(i.CodigoItem)
                                                select i).ToList())
                    {
                        KitItemPA novo = itens.Where(p => p.CodigoItem == item.CodigoItem).First();
                        item.Quantidade = novo.Quantidade;
                        Session.Update(item);
                    }

                    foreach (KitMedicamentoPA item in (from i in medicamentosAntigos
                                                       where
                                                           medicamentos.Select(p => p.CodigoMedicamento).Contains(i.CodigoMedicamento)
                                                       select i).ToList())
                    {
                        KitMedicamentoPA novo = medicamentos.Where(p => p.CodigoMedicamento == item.CodigoMedicamento).First();
                        item.Quantidade = novo.Quantidade;
                        item.MedicamentoPrincipal = novo.MedicamentoPrincipal;

                        Session.Update(item);
                    }
                    #endregion

                    Session.Transaction.Commit();
                }
                catch (Exception f)
                {
                    Session.Transaction.Rollback();
                    throw f;
                }
            }
        }

        //void IKitPA.SalvarKit<K, I, M>(K kit, IList<I> _itensaseremexcluidos, IList<I> _itensaseremincluidos, IList<M> _medicamentosaseremexcluidos, IList<M> _medicamentosaseremincluidos) 
        //{
        //    using (Session.BeginTransaction())
        //    {
        //        KitPA Kit = (KitPA)(object)kit;
        //        IList<KitItemPA> itensaseremincluidos = (IList<KitItemPA>)(object)_itensaseremincluidos;
        //        IList<KitMedicamentoPA> medicamentosaseremincluidos = (IList<KitMedicamentoPA>)(object)_medicamentosaseremincluidos;
        //        IList<KitItemPA> itensaseremexcluidos = (IList<KitItemPA>)(object)_itensaseremexcluidos;
        //        IList<KitMedicamentoPA> medicamentosaseremexcluidos = (IList<KitMedicamentoPA>)(object)_medicamentosaseremexcluidos;

        //        try
        //        {
        //            Kit = (KitPA)Session.SaveOrUpdateCopy(Kit);

        //            foreach (KitItemPA item in itensaseremexcluidos)
        //                Session.Delete(item);

        //            foreach (KitMedicamentoPA item in medicamentosaseremexcluidos)
        //                Session.Delete(item);

        //            foreach (KitItemPA item in itensaseremincluidos)
        //            {
        //                item.KitPA = Kit;
        //                Session.SaveOrUpdateCopy(item);
        //            }

        //            foreach (KitMedicamentoPA item in medicamentosaseremincluidos)
        //            {
        //                item.KitPA = Kit;
        //                Session.SaveOrUpdateCopy(item);
        //            }

        //            Session.Transaction.Commit();
        //        }
        //        catch (Exception f)
        //        {
        //            Session.Transaction.Rollback();
        //            throw f;
        //        }
        //    }
        //}

        T IKitPA.BuscarKitPorMedicamentoPrincipal<T>(int co_medicamento) 
        {
            string hql = string.Empty;
            hql = "FROM KitMedicamentoPA AS km WHERE km.CodigoMedicamento = " + co_medicamento;
            hql += " AND km.MedicamentoPrincipal = 'Y'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        bool IKitPA.ValidaCadastroKit(int co_kit, int co_medicamentoprincipal)
        {
            string hql = string.Empty;
            hql = "SELECT km.KitPA FROM KitMedicamentoPA AS km WHERE km.CodigoMedicamento = " + co_medicamentoprincipal;
            hql +=" AND km.MedicamentoPrincipal = 'Y' AND km.KitPA.Codigo <> " + co_kit;
            return Session.CreateQuery(hql).List<KitPA>().Count <= 0;
        }
    }
}
