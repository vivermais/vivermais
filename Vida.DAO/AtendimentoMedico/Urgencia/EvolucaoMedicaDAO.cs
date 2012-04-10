using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;

namespace ViverMais.DAO.AtendimentoMedico.Urgencia
{
    public class EvolucaoMedicaDAO : UrgenciaServiceFacadeDAO, IEvolucaoMedica
    {
        #region
            IList<T> IEvolucaoMedica.BuscarPorProntuario<T>(long co_prontuario) 
            {
                string hql = string.Empty;
                       hql = "FROM ViverMais.Model.EvolucaoMedica evolucao WHERE evolucao.Prontuario.Codigo = " + co_prontuario;
                       hql+= " AND evolucao.PrimeiraConsulta = 'N' ORDER BY evolucao.Data desc";
                return Session.CreateQuery(hql).List<T>();
            }

            T IEvolucaoMedica.BuscarConsultaMedica<T>(long co_prontuario)
            {
                string hql = string.Empty;
                hql = "FROM ViverMais.Model.EvolucaoMedica evolucao WHERE evolucao.Prontuario.Codigo = " + co_prontuario;
                hql += " AND evolucao.PrimeiraConsulta = 'Y'";
                return Session.CreateQuery(hql).UniqueResult<T>();
            }
            
        #endregion

        public EvolucaoMedicaDAO()
        {

        }
    

#region IServiceFacade Members

//T  ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.BuscarPorCodigo<T>(object codigo)
//{
//    throw new NotImplementedException();
//}

//void  ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.Atualizar(object obj)
//{
//    throw new NotImplementedException();
//}

//void  ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.Inserir(object obj)
//{
//    throw new NotImplementedException();
//}

//void  ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.Salvar(object obj)
//{
//    throw new NotImplementedException();
//}

//void  ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.Deletar(object obj)
//{
//    throw new NotImplementedException();
//}

//IList<T>  ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.ListarTodos<T>()
//{
//    throw new NotImplementedException();
//}

//IList<T>  ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.ListarTodos<T>(string orderField, bool asc)
//{
//    throw new NotImplementedException();
//}

#endregion
}
}
