using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Engine.Query;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.Collections;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Agregado;

namespace ViverMais.DAO.Agendamento
{
    public class PactoDAO : ViverMaisServiceFacadeDAO, IPacto
    {
        #region IPacto Members

        public PactoDAO()
        {

        }

        T IPacto.BuscarPactoPorMunicipio<T>(string id_municipio)
        {
            string hql = String.Empty;
            hql += "from ViverMais.Model.Pacto pacto";
            hql += " where pacto.Municipio.Codigo=" + id_municipio;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        //void IPacto.SalvarPacto<T,U,V>(T pactoParameter, U pactoAgregadoProcedCBOParameter, IList<V> pactosReferenciaSaldoParameter)
        //{
        //    ISession sessao = NHibernateHttpHelper.GetCurrentSession("ViverMais");
        //    using (sessao.BeginTransaction())
        //    {
        //        try
        //        {
        //            #region Salva Pacto
        //                //Session.BeginTransaction();
        //                Pacto pacto = (Pacto)(object)pactoParameter;
        //                pacto = (Pacto)sessao.SaveOrUpdateCopy(pacto);
        //                //Session.Transaction.Commit();
        //            #endregion

        //            #region Salva PactoAgregado
        //                //Session.BeginTransaction();
        //                PactoAgregadoProcedCBO pactoAgregadoProcedCBO = (PactoAgregadoProcedCBO)(object)pactoAgregadoProcedCBOParameter;
        //                if (pactoAgregadoProcedCBO != null)
        //                {
        //                    pactoAgregadoProcedCBO.Pacto = pacto;
        //                    //pactoAgregadoProcedCBO.ValorMensal = 
        //                    sessao.Save(pactoAgregadoProcedCBO);
        //                }
        //                //Session.Transaction.Commit();
        //            #endregion

        //            #region Salva PactoReferencia Saldo
        //                //Session.BeginTransaction();
        //                IList<PactoReferenciaSaldo> pactosReferenciaSaldo = (IList<PactoReferenciaSaldo>)(object)pactosReferenciaSaldoParameter;

        //                if (pactosReferenciaSaldo.Count != 0)
        //                {
        //                    for (int i = 0; i < pactosReferenciaSaldo.Count; i++)
        //                    {
        //                        pactosReferenciaSaldo[i].PactoAgregadoProcedCBO = pactoAgregadoProcedCBO;
        //                        pactosReferenciaSaldo[i].ValorMensal = pactosReferenciaSaldo[i].PactoAgregadoProcedCBO.ValorPactuado / 12;
        //                        sessao.Save(pactosReferenciaSaldo[i]);
        //                        //pactosReferenciaSaldo[i] = (PactoReferenciaSaldo)Session.SaveOrUpdateCopy(pactosReferenciaSaldo[i]);
        //                    }
        //                }
        //                sessao.Transaction.Commit();
        //            #endregion
        //        }
        //        catch (Exception e)
        //        {
        //            sessao.Transaction.Rollback();
        //            throw e;
        //        }
        //    }
        //}

        #endregion
    }
}
