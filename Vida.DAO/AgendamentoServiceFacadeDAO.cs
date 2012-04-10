using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades;
using NHibernate;
using NHibernate.Criterion;
using System.Collections;
using ViverMais.DAO.FormatoDataOracle;
namespace ViverMais.DAO
{
    public class AgendamentoServiceFacadeDAO : NHibernateDAO, IAgendamentoServiceFacade
    {
        public AgendamentoServiceFacadeDAO()
            : base("ViverMais")
        {

        }

        #region IServiceFacade Members

        T IServiceFacade.BuscarPorCodigo<T>(object codigo)
        {
            return this.FindByPrimaryKey<T>(codigo);
        }

        void IServiceFacade.Atualizar(object obj)
        {
            this.Update(obj);
        }
        
        void IServiceFacade.Deletar(object obj)
        {
            this.Delete(obj);
        }

        void IServiceFacade.Inserir(object obj)
        {
            this.Insert(obj);
        }

        void IServiceFacade.Salvar(object obj)
        {
            this.Save(obj);
        }

        void IServiceFacade.Salvar<T>(ref T obj)
        {
            this.Save<T>(ref obj);
        }

        IList<T> IServiceFacade.ListarTodos<T>()
        {
            return this.Session.CreateCriteria(typeof(T)).List<T>();
        }

        IList<T> IServiceFacade.ListarTodos<T>(string orderField, bool asc)
        {
            return this.Session.CreateCriteria(typeof(T)).AddOrder(asc ? Order.Asc(orderField) : Order.Desc(orderField)).List<T>();
        }

        #endregion

        #region IAgendamentoServiceFacade Members
        

        int IAgendamentoServiceFacade.BuscarAfastamentoEAS(string id_unidade)
        {
            string hql = "SELECT MAX(afastamento.Codigo) FROM ViverMais.Model.AfastamentoEAS afastamento";
            hql += " WHERE afastamento.ID_Unidade = '" + id_unidade + "'";
            return Session.CreateQuery(hql).UniqueResult<int>();
        }

        

        T IAgendamentoServiceFacade.VerificaEASInativo<T>(string id_unidade, DateTime data)
        {
            string hql = "from ViverMais.Model.AfastamentoEAS as afastamento";
            hql += " WHERE afastamento.ID_Unidade = '" + id_unidade + "'";
            //hql += " and afastamento.Inativo = " + inativo;
            hql += " and '" + FormatoData.ConverterData(data,FormatoData.nomeBanco.ORACLE) + "'";
            hql += " >= afastamento.Data_Inicial ";
            hql += " and '" + FormatoData.ConverterData(data, FormatoData.nomeBanco.ORACLE) + "'";
            hql += " <= afastamento.Data_Reativacao ";            
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IAgendamentoServiceFacade.VerificaProfissionalInativo<T>(string id_unidade, string id_profissional, DateTime data)
        {
            int inativo = 1;

            string hql = "from ViverMais.Model.AfastamentoProfissional as afastamento";
            hql += " WHERE afastamento.ID_Unidade = '" + id_unidade + "'";
            hql += " and afastamento.ID_Profissional = '" + id_profissional + "'";
            hql += " and afastamento.Inativo = " + inativo;
            hql += " and '" + FormatoData.ConverterData(data, FormatoData.nomeBanco.ORACLE) + "'";
            hql += " >= afastamento.Data_Inicial ";
            hql += " and '" + FormatoData.ConverterData(data, FormatoData.nomeBanco.ORACLE) + "'";
            hql += " <= afastamento.Data_Final ";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IAgendamentoServiceFacade.BuscarPercentual<T>(string unidade, string procedimento)
        {
            string hql = "from ViverMais.Model.CotasEAS cotas";
            hql += " WHERE cotas.Id_EstabelecimentoSaude ='"+unidade+"'";
            hql += " and cotas.Id_Procedimento ='" + procedimento + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
         }
        
        IList<T> IAgendamentoServiceFacade.BuscarProcedimentoPorOcupacao<T>(string cbo)
        {
            string hql = "select  po.Procedimento";
            hql += " from ViverMais.Model.ProcedimentoOcupacao po ";
            hql += " where po.CBO.Codigo = '" + cbo + "'";
            IQuery query = NHibernateHttpHelper.GetCurrentSession("ViverMais").CreateQuery(hql);
            return query.List<T>();
        }

        T IAgendamentoServiceFacade.BuscarFatura<T>(string id_unidade, int competencia, string tipo)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Fatura as fatura";
            hql += " WHERE fatura.Competencia = '" + competencia + "'";
            hql += " and fatura.ID_Unidade = '" + id_unidade + "'";
            hql += " and fatura.Tipo = '" + tipo + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        IList<T> IAgendamentoServiceFacade.ListarFaturas<T>(string id_unidade, int competencia)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Fatura as fatura";
            hql += " WHERE fatura.Competencia = '" + competencia + "'";
            hql += " and fatura.ID_Unidade = '" + id_unidade + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IAgendamentoServiceFacade.BuscarMovimento<T>(int id_fatura)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.MovimentoFatura as mov";
            hql += " WHERE mov.Fatura.Codigo = '" + id_fatura + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IAgendamentoServiceFacade.ListarBPC<T>(string id_unidade, int competencia)
        {
            string hql = string.Empty;
            hql = "Select fatura.ID_Unidade, fatura.Competencia, fatura.MovimentoFatura.ID_Profissional ";
            hql += ", fatura.MovimentoFatura.Cod_Cbo, fatura.MovimentoFatura.ID_Procedimento, fatura.MovimentoFatura.Sexo ";
            hql += ", fatura.MovimentoFatura.IBGE, fatura.MovimentoFatura.Idade, fatura.MovimentoFatura.RacaCor, sum(movimento.Qtd) as Quantidade ";
            hql += " FROM ViverMais.Model.Fatura as fatura ";
            hql += " WHERE fatura.Competencia = '" + competencia + "'";
            hql += " and fatura.ID_Unidade = '" + id_unidade + "'";
            hql += " Group by fatura.ID_Unidade, fatura.Competencia, fatura.MovimentoFatura.ID_Profissional ";
            hql += " , fatura.MovimentoFatura.Cod_Cbo, fatura.MovimentoFatura.ID_Procedimento, fatura.MovimentoFatura.Sexo ";
            hql += " , fatura.MovimentoFatura.IBGE, fatura.MovimentoFatura.Idade, fatura.MovimentoFatura.RacaCor ";
            return Session.CreateQuery(hql).List<T>();
        }
        
        
        #endregion

    }
}
