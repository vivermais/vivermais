using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.Model;
using ViverMais.BLL;

namespace ViverMais.DAO.AtendimentoMedico.Urgencia
{
    public class ExameDAO : UrgenciaServiceFacadeDAO, IExame
    {
        #region

        IList<T> IExame.ListarPorEstabelecimentoSaude<T>(string co_estabelecimento)
        {
            string hql = string.Empty;
            hql = "from ViverMais.Model.ProntuarioExame pe where pe.Prontuario.CodigoUnidade = '" + co_estabelecimento + "'";
            hql += " ORDER BY pe.Data";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IExame.ListarPorEstabelecimentoSaudePendentes<T>(string co_estabelecimento)
        {
            string hql = string.Empty;
            hql = "from ViverMais.Model.ProntuarioExame pe where pe.Prontuario.CodigoUnidade = '" + co_estabelecimento + "'";
            hql += " AND pe.Resultado IS NULL";
            hql += " ORDER BY pe.Data";

            IList<ProntuarioExame> prontuarios = (IList<ProntuarioExame>)(object)Session.CreateQuery(hql).List<T>();
            foreach (ProntuarioExame exame in prontuarios)
            {
                if (!string.IsNullOrEmpty(exame.Prontuario.Paciente.CodigoViverMais))
                    exame.Prontuario.Paciente.Nome = PacienteBLL.Pesquisar(exame.Prontuario.Paciente.CodigoViverMais).Nome;
            }

            return (IList<T>)(object)prontuarios;
            //return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IExame.BuscarExamesPorCartaoSus<T>(string co_cartaosus)
        {
            ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().PesquisarPacientePorCNS<ViverMais.Model.Paciente>(co_cartaosus);
              
            if (paciente == null)
                return new List<T>();

            string hql = string.Empty;
            hql = "from ViverMais.Model.ProntuarioExame pe where ";
            hql += "pe.Prontuario.Paciente.CodigoViverMais = '" + paciente.Codigo + "' ORDER BY pe.Data";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IExame.BuscarExamesPendentesPorCartaoSus<T>(string co_cartaosus, string co_unidade)
        {
            ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().PesquisarPacientePorCNS<ViverMais.Model.Paciente>(co_cartaosus);

            if (paciente == null)
                return new List<T>();

            string hql = string.Empty;
            hql = "from ViverMais.Model.ProntuarioExame pe where ";
            hql += "pe.Prontuario.Paciente.CodigoViverMais = '" + paciente.Codigo + "'";
            hql += " AND pe.Prontuario.CodigoUnidade='" + co_unidade + "' AND pe.Resultado IS NULL";
            hql += " ORDER BY pe.Data";

            IList<ProntuarioExame> prontuarios = (IList<ProntuarioExame>)(object)Session.CreateQuery(hql).List<T>();
            foreach (ProntuarioExame exame in prontuarios)
            {
                if (!string.IsNullOrEmpty(exame.Prontuario.Paciente.CodigoViverMais))
                    exame.Prontuario.Paciente.Nome = PacienteBLL.Pesquisar(exame.Prontuario.Paciente.CodigoViverMais).Nome;
            }

            return (IList<T>)(object)prontuarios;

            //return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IExame.BuscarExamesPorProntuario<T>(long co_prontuario)
        {
            string hql = string.Empty;
            hql = "from ViverMais.Model.ProntuarioExame pe where pe.Prontuario.Codigo = " + co_prontuario + " ORDER BY pe.Data";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IExame.BuscarExamesPendentesPorProntuario<T>(long co_prontuario, string co_unidade)
        {
            string hql = string.Empty;
            hql = "from ViverMais.Model.ProntuarioExame pe where pe.Prontuario.Codigo = " + co_prontuario;
            hql += " AND pe.Prontuario.CodigoUnidade='" + co_unidade + "'";
            hql += " AND pe.Resultado IS NULL";
            hql += " ORDER BY pe.Data";

            IList<ProntuarioExame> prontuarios = (IList<ProntuarioExame>)(object)Session.CreateQuery(hql).List<T>();
            foreach (ProntuarioExame exame in prontuarios)
            {
                if (!string.IsNullOrEmpty(exame.Prontuario.Paciente.CodigoViverMais))
                    exame.Prontuario.Paciente.Nome = PacienteBLL.Pesquisar(exame.Prontuario.Paciente.CodigoViverMais).Nome;
            }

            return (IList<T>)(object)prontuarios;

            //return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IExame.BuscarExamesEletivosPorProntuario<T>(long co_prontuario)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ProntuarioExameEletivo AS pe WHERE pe.Prontuario.Codigo = " + co_prontuario;
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IExame.BuscarControleExamesEletivosPorExames<T, E>(E exames)
        {
            string hql = string.Empty;
            IList<ProntuarioExameEletivo> eletivos = (IList<ProntuarioExameEletivo>)(object)exames;
            string subquery = string.Empty;

            if (eletivos.Count() > 0)
            {
                subquery += "(";

                foreach (ProntuarioExameEletivo p in eletivos)
                    subquery += p.Codigo + ",";

                subquery = subquery.Remove(subquery.Length - 1, 1);
                subquery += ")";

                hql = "FROM ViverMais.Model.ControleExameEletivoUrgence AS c WHERE c.ProntuarioExame.Codigo IN " + subquery;
                return Session.CreateQuery(hql).List<T>();
            }
            else
                return (IList<T>)new List<ControleExameEletivoUrgence>();
        }

        IList<T> IExame.ListarPorEstabelecimentoDisponivelEntrega<T>(string co_estabelecimento)
        {
            string hql = string.Empty;
            hql = "from ViverMais.Model.ProntuarioExame pe where pe.Prontuario.CodigoUnidade = '" + co_estabelecimento + "'";
            hql += " AND pe.DataResultado IS NOT NULL AND pe.DataConfirmacaoBaixa IS NULL";
            hql += " ORDER BY pe.Data";
            return Session.CreateQuery(hql).List<T>();
        }

        //IList<T> IExame.BuscarControlesEletivosDoAtendimentoMedicamento<T>(long co_prontuario)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.ControleExameEletivoUrgence AS c " +
        //          " WHERE c.AtendimentoMedico.Codigo = " + co_prontuario;
        //    return Session.CreateQuery(hql).List<T>();
        //}

        IList<T> IExame.BuscarControlesEletivosDaEvolucaoMedica<T>(long co_prontuario)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ControleExameEletivoUrgence AS c " +
                  " WHERE c.EvolucaoMedica.Prontuario.Codigo = " + co_prontuario;
            return Session.CreateQuery(hql).List<T>();
        }

        #endregion

        public ExameDAO()
        {
        }


        #region IServiceFacade Members

        //T ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.BuscarPorCodigo<T>(object codigo)
        //{
        //    throw new NotImplementedException();
        //}

        //void ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.Atualizar(object obj)
        //{
        //    throw new NotImplementedException();
        //}

        //void ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.Inserir(object obj)
        //{
        //    throw new NotImplementedException();
        //}

        //void ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.Salvar(object obj)
        //{
        //    throw new NotImplementedException();
        //}

        //void ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.Deletar(object obj)
        //{
        //    throw new NotImplementedException();
        //}

        //IList<T> ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.ListarTodos<T>()
        //{
        //    throw new NotImplementedException();
        //}

        //IList<T> ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.ListarTodos<T>(string orderField, bool asc)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion
    }
}
