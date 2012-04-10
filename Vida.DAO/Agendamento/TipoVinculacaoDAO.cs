using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using NHibernate;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.Collections;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;


namespace ViverMais.DAO.Agendamento
{
    public class TipoVinculacaoDAO : ViverMaisServiceFacadeDAO, ITipoVinculacao
    {
        #region ITipoVinculacao Members

        public TipoVinculacaoDAO()
        {

        }

        IList<T> ITipoVinculacao.BuscaPorVinculacao<T>(string id_vinculacao)
        {
            string hql = "from ViverMais.Model.TipoVinculacao tv";
            hql += " where tv.Vinculacao.Codigo='"+ id_vinculacao+ "'";
            //hql += " and tv";
            return Session.CreateQuery(hql).List<T>();
        }

        T ITipoVinculacao.BuscaPorVinculacao<T>(string id_vinculacao, string id_tipoVinculacao)
        {
            string hql = "from ViverMais.Model.TipoVinculacao tv";
            hql += " where tv.Vinculacao.Codigo='" + id_vinculacao + "'";
            hql += " and tv.Codigo='"+id_tipoVinculacao+"'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        #endregion

        #region IServiceFacade Members

        public T BuscarPorCodigo<T>(object codigo) where T : new()
        {
            throw new NotImplementedException();
        }

        public void Atualizar(object obj)
        {
            throw new NotImplementedException();
        }

        public void Inserir(object obj)
        {
            throw new NotImplementedException();
        }

        public void Salvar(object obj)
        {
            throw new NotImplementedException();
        }

        public void Deletar(object obj)
        {
            throw new NotImplementedException();
        }

        public IList<T> ListarTodos<T>()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
