using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc;

namespace ViverMais.DAO.AtendimentoMedico.Misc
{
    public class ControlePrescricaoUrgenceDAO : UrgenciaServiceFacadeDAO, IControlePrescricaoUrgence
    {
        T IControlePrescricaoUrgence.BuscarControlePorPrescricao<T>(long co_prescricao)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ControlePrescricaoUrgence AS cp WHERE cp.Prescricao.Codigo = " + co_prescricao;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IControlePrescricaoUrgence.BuscarPorPrimeiraConsulta<T>(long co_prontuario)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ControlePrescricaoUrgence AS cp WHERE cp.AtendimentoMedico IS NOT NULL ";
            hql += " AND cp.AtendimentoMedico.Codigo=" + co_prontuario;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IControlePrescricaoUrgence.BuscarPorEvolucaoMedica<T>(long co_evolucaomedica)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ControlePrescricaoUrgence AS cp WHERE cp.EvolucaoMedica IS NOT NULL ";
            hql += " AND cp.EvolucaoMedica.Codigo=" + co_evolucaomedica;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }
    }
}
