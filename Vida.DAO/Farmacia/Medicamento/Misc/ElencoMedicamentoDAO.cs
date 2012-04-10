using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento.Misc;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;

namespace ViverMais.DAO.Farmacia.Medicamento.Misc
{
    public class ElencoMedicamentoDAO : FarmaciaServiceFacadeDAO, IElencoMedicamento
    {
        #region IElencoMedicamento Members

        /// <summary>
        /// Busca todos os medicamentos que não constam no elenco especificado
        /// </summary>
        IList<T> IElencoMedicamento.BuscarMedicamentosNaoContidosNoElenco<T>(int id_elenco)
        {
            string hql = string.Empty;
            hql = "SELECT m FROM ViverMais.Model.Medicamento AS m,ViverMais.Model.ElencoMedicamento AS e ";
            hql = hql + " WHERE e.Codigo = '" + id_elenco + "'";
            hql = hql + " AND m.PertenceARede = 'Y' ";
            hql = hql + " AND m not in elements(e.Medicamentos) ";
            return Session.CreateQuery(hql).List<T>();
        }

        /// <summary>
        /// Busca todos os sub-elencos que não constam no elenco especificado
        /// </summary>
        IList<T> IElencoMedicamento.BuscarSubElencosNaoContidosNoElenco<T>(int id_elenco)
        {
            string hql = string.Empty;
            hql = "SELECT se FROM ViverMais.Model.SubElencoMedicamento AS se,ViverMais.Model.ElencoMedicamento AS e ";
            hql = hql + " WHERE e.Codigo = '" + id_elenco + "'";
            hql = hql + " AND se not in elements(e.SubElencos) ";
            return Session.CreateQuery(hql).List<T>();
        }

        /// <summary>
        /// Busca todos os elencos de uma farmácia especificada
        /// Este método não está funcionando
        /// </summary>
        IList<T> IElencoMedicamento.BuscarElencosPorFarmacia<T>(int id_farmacia)
        {
            string hql = string.Empty;
            hql = "SELECT e FROM ViverMais.Model.ElencoMedicamento AS e,ViverMais.Model.Farmacia AS e ";
            hql = hql + " WHERE e.Codigo = '" + id_farmacia + "'";
            hql = hql + " AND se not in elements(e.SubElencos) ";
            return Session.CreateQuery(hql).List<T>();
        }

        //IList<T> IElencoMedicamento.BuscarPorNome<T>(string nome)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.ElencoMedicamento elenco WHERE TRANSLATE(UPPER(elenco.Nome),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc') ";
        //    hql += " LIKE TRANSLATE(UPPER('" + nome + "%'),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc') ORDER BY elenco.Nome";
        //    return Session.CreateQuery(hql).List<T>();
        //}

        IList<T> IElencoMedicamento.BuscarPorDescricao<T>(string nome)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ElencoMedicamento elenco ";

            if (!string.IsNullOrEmpty(nome))
            {
                hql += " WHERE TRANSLATE(UPPER(elenco.Nome),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc') ";
                hql += " LIKE TRANSLATE(UPPER('" + nome + "%'),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc') ";
            }

            hql += " ORDER BY elenco.Nome";

            return Session.CreateQuery(hql).List<T>();
        }

        string IElencoMedicamento.ExcluirElencoMedicamento(int co_elenco)
        {
            //using (Session.BeginTransaction())
            //{
            //    try
            //    {
                    string msg = string.Empty;
                    int i = 1;
                    ElencoMedicamento elenco = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ElencoMedicamento>(co_elenco);

                    //IList<ViverMais.Model.Farmacia> farmacias = Factory.GetInstance<IFarmacia>().ListarTodos<ViverMais.Model.Farmacia>().Where(p => p.Elencos.Select(p2 => p2.Codigo).Contains(elenco.Codigo)).ToList();
                    IList<ViverMais.Model.Farmacia> farmacias = Factory.GetInstance<IFarmacia>().BuscarPorElenco<ViverMais.Model.Farmacia>(co_elenco);
                    if (farmacias.Count() > 0)
                    {
                        msg = "O elenco não pode ser excluído, pois a(s) seguinte(s) farmácia(s) está(ão) vinculada(s) a este registro: \\n\\n";
                        foreach (ViverMais.Model.Farmacia farmacia in farmacias)
                        {
                            msg += i + " - " + farmacia.Nome + "\\n";
                            i++;
                        }

                        return msg;
                    }

                    if (elenco.SubElencos != null && elenco.SubElencos.Count() > 0)
                    {
                        msg = "O elenco não pode ser excluído, pois o(s) seguinte(s) sub-elenco(s) está(ão) vinculado(s) a este registro: \\n\\n";
                        foreach (SubElencoMedicamento subelenco in elenco.SubElencos)
                        {
                            msg += i + " - " + subelenco.Nome + "\\n";
                            i++;
                        }

                        return msg;
                    }

                    //IList<ViverMais.Model.Medicamento> medicamentos = Factory.GetInstance<IFarmacia>().ListarTodos<ViverMais.Model.Medicamento>().Where(p => p.Elencos.Select(p2 => p2.Codigo).Contains(elenco.Codigo)).ToList();
                    //IList<ViverMais.Model.Medicamento> medicamentos = Factory.GetInstance<IMedicamento>().BuscarPorElenco<ViverMais.Model.Medicamento>(co_elenco);

                    //foreach (ViverMais.Model.Medicamento medicamento in medicamentos)
                    //{
                    //    var elencomedicamento = medicamento.Elencos.Select((Elenco, IndexElenco) => new { IndexElenco, Elenco }).Where(p => p.Elenco.Codigo == elenco.Codigo).First();
                    //    medicamento.Elencos.RemoveAt(elencomedicamento.IndexElenco);
                    //    Session.SaveOrUpdateCopy(medicamento);
                    //}
                    
                    Factory.GetInstance<IViverMaisServiceFacade>().Deletar(elenco);
                    //Session.Delete(elenco);

                   // Session.Transaction.Commit();

                    return "ok";
                //}
                //catch (Exception ex)
                //{
                //    Session.Transaction.Rollback();
                //    throw ex;
                //}
            //}
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
