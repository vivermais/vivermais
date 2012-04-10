using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using System.Collections;

namespace ViverMais.DAO.Farmacia.Medicamento
{
    /* Poderia ter sido utilizado sobrecarga ao invés de ficar verificando se o valor de um
     * parâmetro é nulo ou um determinado valor
     */

    public class MedicamentoDAO : FarmaciaServiceFacadeDAO, IMedicamento
    {
        #region IMedicamento Members
        IList<T> IMedicamento.BuscarMedicamentosPorUnidadeMedida<T>(int co_unidademedida)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Medicamento AS m WHERE m.UnidadeMedida.Codigo = " + co_unidademedida;
            hql += " and m.PertenceARede = 'Y'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IMedicamento.BuscarElencos<T>(int co_medicamento)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ElencoMedicamento AS elenco ";
            hql += " WHERE elenco.Medicamentos IN ELEMENTS(FROM ViverMais.Model.Medicamento AS m";
            hql += "  WHERE m.Codigo = " + co_medicamento + ")";
            
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IMedicamento.BuscarSubElencos<T>(int co_medicamento)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.SubElencoMedicamento AS subelenco ";
            hql += " WHERE subelenco.Medicamentos IN ELEMENTS(SELECT medicamento FROM ViverMais.Model.Medicamento AS medicmamento";
            hql += "  WHERE medicamento.Codigo = " + co_medicamento + ")";
            return Session.CreateQuery(hql).List<T>();
        }

        /// <summary>
        /// Busca um medicamento por sua descricao
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="co_unidademedida">Se -1, ignorada</param>
        /// <param name="nome">Parte do nome do medicamento</param>
        /// <param name="pertencearede">Se true, somente medicamentos do sisfarma. False, todos os medicamentos</param>
        /// <returns></returns>
        IList<T> IMedicamento.BuscarPorDescricao<T>(int co_unidademedida, string nome, bool pertencearede)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Medicamento AS m WHERE";

            if (co_unidademedida != -1)
                hql += " m.UnidadeMedida.Codigo = " + co_unidademedida;

            if (!string.IsNullOrEmpty(nome))
            {
                if (co_unidademedida != -1)
                {
                    hql += " AND TRANSLATE(UPPER(m.Nome),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";
                    hql += " LIKE ";
                    hql += " TRANSLATE(UPPER('" + nome + "%'),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";

                    //hql += " AND m.Nome LIKE '" + nome + "%'";
                }
                else
                {
                    hql += " TRANSLATE(UPPER(m.Nome),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";
                    hql += " LIKE ";
                    hql += " TRANSLATE(UPPER('" + nome + "%'),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";

                    //hql += " m.Nome LIKE '" + nome + "%'";
                }
                //if (co_unidademedida != -1)
                //    hql += " AND m.Nome LIKE '" + nome + "%'";
                //else
                //    hql += " m.Nome LIKE '" + nome + "%'";
            }

            if (pertencearede)
            {
                if (co_unidademedida != -1 || !string.IsNullOrEmpty(nome))
                    hql += " AND m.PertenceARede = 'Y'";
                else
                    hql += " m.PertenceARede = 'Y'";
            }

            hql += " ORDER BY m.Nome";

            return Session.CreateQuery(hql).List<T>();
        }

        /// <summary>
        /// Busca um medicamento por sua descricao
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="co_medicamento">Codigo do medicamento</param>
        /// <param name="nome">Parte do nome do medicamento</param>
        /// <returns></returns>
        IList<T> IMedicamento.BuscarPorDescricao<T>(string co_medicamento, string nome)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Medicamento AS m WHERE";

            if (!string.IsNullOrEmpty(co_medicamento))
                hql += " m.CodMedicamento LIKE '" + co_medicamento + "%'";

            if (!string.IsNullOrEmpty(nome))
            {
                if (!string.IsNullOrEmpty(co_medicamento))
                {
                    hql += " AND TRANSLATE(UPPER(m.Nome),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";
                    hql += " LIKE ";
                    hql += " TRANSLATE(UPPER('" + nome + "%'),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";

                    //hql += " AND m.Nome LIKE '" + nome.ToUpper() + "%'";
                }
                else
                {
                    hql += " TRANSLATE(UPPER(m.Nome),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";
                    hql += " LIKE ";
                    hql += " TRANSLATE(UPPER('" + nome + "%'),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";

                    //hql += " m.Nome LIKE '" + nome.ToUpper() + "%'";
                }
            }
            

            hql += " ORDER BY m.Nome";

            return Session.CreateQuery(hql).List<T>();
        }

        T IMedicamento.VerificaDuplicidadePorCodigo<T>(string codigosigm, int co_medicamento)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Medicamento AS m WHERE m.CodMedicamento = '" + codigosigm + "' AND m.Codigo <> " + co_medicamento;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IMedicamento.BuscarPorCodigoSIGM<T>(string codigosigm)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Medicamento AS m WHERE m.CodMedicamento = '" + codigosigm + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        //retorna próximo código do medicamento
        string IMedicamento.GerarCodigoMedicamentoUrgencia()
        {
            //string codigo = string.Empty;
            //hql = "SELECT MAX(m.Codigo) FROM ViverMais.Model.Medicamento AS m ";
            IList codigos = Session.CreateQuery("SELECT m.CodMedicamento FROM ViverMais.Model.Medicamento m WHERE m.CodMedicamento LIKE '90000%'").List();
                
            if (codigos.Count > 0)
                return (codigos.Cast<object>().Select(p => long.Parse(p.ToString())).Max() + 1).ToString();

            return "900000000";
            //return Session.CreateQuery(hql).UniqueResult<T>();
        }

        //retorna todos os medicamentos da rede básica (medicamentos do Farmácia)
        IList<T> IMedicamento.ListarTodosMedicamentos<T>()
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Medicamento AS m WHERE m.PertenceARede = 'Y'";
            return Session.CreateQuery(hql).List<T>();
        }

        //string IMedicamento.ExcluirElencoMedicamento(int co_elenco)
        //{
        //    using (Session.BeginTransaction())
        //    {
        //        try
        //        {
        //            string msg = string.Empty;
        //            int i = 1;
        //            ElencoMedicamento elenco = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ElencoMedicamento>(co_elenco);

        //            //IList<ViverMais.Model.Farmacia> farmacias = Factory.GetInstance<IFarmacia>().ListarTodos<ViverMais.Model.Farmacia>().Where(p => p.Elencos.Select(p2 => p2.Codigo).Contains(elenco.Codigo)).ToList();
        //            IList<ViverMais.Model.Farmacia> farmacias = Factory.GetInstance<IFarmacia>().BuscarPorElenco<ViverMais.Model.Farmacia>(co_elenco);
        //            if (farmacias.Count() > 0)
        //            {
        //                msg = "O elenco não pode ser excluído, pois a(s) seguinte(s) farmácia(s) está(ão) vinculada(s) a este registro: \\n\\n";
        //                foreach (ViverMais.Model.Farmacia farmacia in farmacias)
        //                {
        //                    msg += i + " - " + farmacia.Nome + "\\n";
        //                    i++;
        //                }

        //                return msg;
        //            }

        //            if (elenco.SubElencos != null && elenco.SubElencos.Count() > 0)
        //            {
        //                msg = "O elenco não pode ser excluído, pois o(s) seguinte(s) sub-elenco(s) está(ão) vinculado(s) a este registro: \\n\\n";
        //                foreach (SubElencoMedicamento subelenco in elenco.SubElencos)
        //                {
        //                    msg += i + " - " + subelenco.Nome + "\\n";
        //                    i++;
        //                }

        //                return msg;
        //            }

        //            //IList<ViverMais.Model.Medicamento> medicamentos = Factory.GetInstance<IFarmacia>().ListarTodos<ViverMais.Model.Medicamento>().Where(p => p.Elencos.Select(p2 => p2.Codigo).Contains(elenco.Codigo)).ToList();
        //            IList<ViverMais.Model.Medicamento> medicamentos = Factory.GetInstance<IMedicamento>().BuscarPorElenco<ViverMais.Model.Medicamento>(co_elenco);

        //            foreach (ViverMais.Model.Medicamento medicamento in medicamentos)
        //            {
        //                var elencomedicamento = medicamento.Elencos.Select((Elenco, IndexElenco) => new { IndexElenco, Elenco }).Where(p => p.Elenco.Codigo == elenco.Codigo).First();
        //                medicamento.Elencos.RemoveAt(elencomedicamento.IndexElenco);
        //                Session.Update(medicamento);
        //            }

        //            Session.Delete(elenco);

        //            Session.Transaction.Commit();

        //            return "ok";
        //        }
        //        catch (Exception ex)
        //        {
        //            Session.Transaction.Rollback();
        //            throw ex;
        //        }
        //    }
        //}

        //string IMedicamento.ExcluirSubElencoMedicamento(int co_subelenco)
        //{
        //    using (Session.BeginTransaction())
        //    {
        //        try
        //        {
        //            string msg = string.Empty;
        //            int i = 1;
        //            SubElencoMedicamento subelenco = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<SubElencoMedicamento>(co_subelenco);

        //            if (subelenco.Elencos != null && subelenco.Elencos.Count() > 0)
        //            {
        //                msg = "O sub-elenco não pode ser excluído, pois o(s) seguinte(s) elenco(s) está(ão) vinculado(s) a este registro: \\n\\n";
        //                foreach (ElencoMedicamento elenco in subelenco.Elencos)
        //                {
        //                    msg += i + " - " + elenco.Nome + "\\n";
        //                    i++;
        //                }

        //                return msg;
        //            }

        //            Session.Delete(subelenco);
        //            Session.Transaction.Commit();

        //            return "ok";
        //        }
        //        catch (Exception ex)
        //        {
        //            Session.Transaction.Rollback();
        //            throw ex;
        //        }
        //    }
        //}

        IList<T> IMedicamento.BuscarPorElenco<T>(int co_elenco)
        {
            string hql = string.Empty;
            hql = "SELECT medicamento FROM ViverMais.Model.Medicamento medicamento, ViverMais.Model.ElencoMedicamento elenco WHERE ";
            hql += " elenco.Codigo = " + co_elenco + " AND elenco IN ELEMENTS(medicamento.Elencos)";
            return Session.CreateQuery(hql).List<T>();
        }

        #endregion
    }
}
