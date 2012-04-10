using System;
using NHibernate;
using ViverMais.Model;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;

namespace ViverMais.DAO.AtendimentoMedico.Urgencia
{
    public class PacienteUrgenceDAO: UrgenciaServiceFacadeDAO, IPacienteUrgence
    {
        P IPacienteUrgence.BuscarPorInicializacaoUnica<P>(string co_pacienteViverMais)
        {
            string hql = string.Empty;
            //hql = "FROM ViverMais.Model.PacienteUrgence AS p WHERE p.CodigoViverMais = '" + co_pacienteViverMais + "' AND p.Descricao IS NULL";
            hql = "FROM ViverMais.Model.PacienteUrgence AS p WHERE p.CodigoViverMais = '" + co_pacienteViverMais + "' AND p.Simplificado = 'N'";
            return Session.CreateQuery(hql).UniqueResult<P>();
        }

        //IList<T> IPacienteUrgence.BuscarPorCodigoViverMais<T>(string co_pacienteViverMais)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.PacienteUrgence AS p WHERE p.CodigoViverMais = '" + co_pacienteViverMais + "'";
        //    return Session.CreateQuery(hql).List<T>();
        //}

        IList<T> IPacienteUrgence.BuscarPorDescricao<T>(string descricao)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.PacienteUrgence AS p WHERE ";
            hql += " TRANSLATE(UPPER(p.Descricao),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";
            hql += " LIKE ";
            hql += " TRANSLATE(UPPER('%" + descricao + "%'),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";

            //hql = "FROM ViverMais.Model.PacienteUrgence AS p WHERE p.Descricao LIKE '%" +descricao + "%'";
            return Session.CreateQuery(hql).List<T>();
        }

        //P IPacienteUrgence.BuscarRegistroAbertoPacienteSUS<P>(P _prontuario, string codigo_pacienteViverMais)
        //{
        //    Prontuario prontuario = (Prontuario)(object)_prontuario;

        //    return Session.CreateQuery(@"FROM ViverMais.Model.Prontuario p WHERE p.Codigo <> " + prontuario.Codigo +
        //        " AND p.Paciente.CodigoViverMais IS NOT NULL AND p.Paciente.CodigoViverMais = '" + codigo_pacienteViverMais + "' AND p.CodigoUnidade='" + prontuario.CodigoUnidade + "'" +
        //        " AND (p.Situacao.Codigo = " + SituacaoAtendimento.ATENDIMENTO_INICIAL + " OR p.Situacao.Codigo = " + SituacaoAtendimento.AGUARDANDO_ATENDIMENTO +
        //        " OR p.Situacao.Codigo = " + SituacaoAtendimento.EM_OBSERVACAO_UNIDADE + " OR p.Situacao.Codigo = " + SituacaoAtendimento.AGUARDANDO_REGULACAO_ENFERMARIA +
        //        " OR p.Situacao.Codigo = " + SituacaoAtendimento.AGUARDANDO_REGULACAO_UTI + ")").UniqueResult<P>();
        //}

        //IList<T> IPacienteUrgence.BuscarPacientesSemIdentificacao<T>(string co_unidade)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.PacienteUrgence AS p WHERE ";
        //    hql += " p.CodigoUnidade='" + co_unidade + "'";
        //    hql += " AND p.Paciente";
        //    return Session.CreateQuery(hql).List<T>();
        //}
    }
}
