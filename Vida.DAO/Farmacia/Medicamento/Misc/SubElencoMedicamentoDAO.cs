using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento.Misc;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using NHibernate;

namespace ViverMais.DAO.Farmacia.Medicamento.Misc
{
    public class SubElencoMedicamentoDAO : FarmaciaServiceFacadeDAO, ISubElencoMedicamento
    {
        #region ISubElencoMedicamento Members

        /// <summary>
        /// Busca todos os medicamentos que não constam no subelenco especificado
        /// </summary>
        IList<T> ISubElencoMedicamento.BuscarMedicamentosNaoContidosNoSubElenco<T>(int id_subelenco)
        {
            string hql = string.Empty;
            hql = "SELECT m FROM ViverMais.Model.Medicamento AS m,ViverMais.Model.SubElencoMedicamento AS sub ";
            hql = hql + " WHERE sub.Codigo = '" + id_subelenco + "'";
            hql = hql + " AND m.PertenceARede = 'Y' ";
            hql = hql + " AND m not in elements(sub.Medicamentos) ";
            return Session.CreateQuery(hql).List<T>();
        }

        /// <summary>
        /// Busca todos os elencos que não constam no sub-elenco especificado
        /// </summary>
        IList<T> ISubElencoMedicamento.BuscarElencosNaoContidosNoSubElenco<T>(int id_subelenco)
        {
            string hql = string.Empty;
            hql = "SELECT el FROM ViverMais.Model.ElencoMedicamento AS el,ViverMais.Model.SubElencoMedicamento AS se ";
            hql = hql + " WHERE se.Codigo = '" + id_subelenco + "'";
            hql = hql + " AND el not in elements(se.Elencos) ";
            return Session.CreateQuery(hql).List<T>();
        }

        /// <summary>
        /// Busca todos os elencos que constam no sub-elenco especificado
        /// </summary>
        IList<T> ISubElencoMedicamento.BuscarElencos<T>(int co_subelenco)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ElencoMedicamento AS elenco ";
            hql += " WHERE elenco.SubElencos IN ELEMENTS(SELECT subelenco FROM ViverMais.Model.SubElencoMedicamento AS subelenco";
            hql += "  WHERE subelenco.Codigo = " + co_subelenco + ")";
            return Session.CreateQuery(hql).List<T>();
        }

        //IList<T> ISubElencoMedicamento.BuscarPorNome<T>(string nome)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.SubElencoMedicamento subelenco WHERE TRANSLATE(UPPER(subelenco.Nome),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc') ";
        //    hql += " LIKE TRANSLATE(UPPER('" + nome + "%'),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc') ORDER BY subelenco.Nome";
        //    return Session.CreateQuery(hql).List<T>();
        //}

        IList<T> ISubElencoMedicamento.BuscarPorDescricao<T>(string nome)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.SubElencoMedicamento subelenco ";

            if (!string.IsNullOrEmpty(nome))
            {
                hql += " WHERE TRANSLATE(UPPER(subelenco.Nome),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc') ";
                hql += " LIKE TRANSLATE(UPPER('" + nome + "%'),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc') ";
            }

            hql += " ORDER BY subelenco.Nome";

            return Session.CreateQuery(hql).List<T>();
        }

        string ISubElencoMedicamento.ExcluirSubElencoMedicamento(int co_subelenco)
        {
            SubElencoMedicamento subelenco = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<SubElencoMedicamento>(co_subelenco);

            ISession iSession = this.Session;
            iSession.Clear();

            using (iSession.BeginTransaction())
            {
                try
                {
                    string msg = string.Empty;
                    int i = 1;

                    IList<ElencoMedicamento> elencos = Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<ElencoMedicamento>()
                        .Where(p=>p.SubElencos.Contains(Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<SubElencoMedicamento>(co_subelenco))).ToList();
                    if (elencos.Count() > 0)
                    {
                        msg = "O sub-elenco não pode ser excluído, pois o(s) seguinte(s) elenco(s) está(ão) vinculado(s) a este registro: \\n\\n";
                        foreach (ElencoMedicamento elenco in elencos)
                        {
                            msg += i + " - " + elenco.Nome + "\\n";
                            i++;
                        }

                        return msg;
                    }

                    iSession.Delete(iSession.Merge(subelenco));
                    //iSession.Flush();
                    iSession.Transaction.Commit();

                    return "ok";
                }
                catch (Exception ex)
                {
                    iSession.Transaction.Rollback();
                    throw ex;
                }
            }
        }

        #endregion

        #region IServiceFacade Members

        T ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.BuscarPorCodigo<T>(object codigo)
        {
            throw new NotImplementedException();
        }

        void ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.Atualizar(object obj)
        {
            throw new NotImplementedException();
        }

        void ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.Inserir(object obj)
        {
            throw new NotImplementedException();
        }

        void ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.Salvar(object obj)
        {
            throw new NotImplementedException();
        }

        void ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.Deletar(object obj)
        {
            throw new NotImplementedException();
        }

        IList<T> ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.ListarTodos<T>()
        {
            throw new NotImplementedException();
        }

        IList<T> ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.ListarTodos<T>(string orderField, bool asc)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
