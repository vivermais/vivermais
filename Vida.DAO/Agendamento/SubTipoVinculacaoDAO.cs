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
    public class SubTipoVinculacaoDAO : ViverMaisServiceFacadeDAO, ISubTipoVinculacao
    {
        #region ISubTipoVinculacao Members

        public SubTipoVinculacaoDAO()
        {

        }

        IList<T> ISubTipoVinculacao.BuscaPorTipoVinculacao<T>(string id_vinculacao, string id_tipoVinculacao)
        {
            string hql = "from ViverMais.Model.SubTipoVinculacao stv";
            hql += " where stv.Vinculacao.Codigo='" + id_vinculacao + "'";
            hql += " and stv.TipoVinculacao.Codigo='" + id_tipoVinculacao+ "'";
            return Session.CreateQuery(hql).List<T>();
        }

        T ISubTipoVinculacao.BuscaPorTipoVinculacao<T>(string id_vinculacao, string id_tipoVinculacao, string id_SubTipoVinculacao)
        {
            string hql = "from ViverMais.Model.SubTipoVinculacao stv";
            hql += " where stv.Vinculacao.Codigo='" + id_vinculacao + "'";
            hql += " and stv.TipoVinculacao.Codigo='" + id_tipoVinculacao + "'";
            hql += " and stv.Codigo='"+ id_SubTipoVinculacao+"'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        IList<T> ISubTipoVinculacao.BuscaPorTipoVinculacao<T>(string id_SubTipoVinculacao)
        {
            string hql = "from ViverMais.Model.SubTipoVinculacao stv";
            hql += " where stv.Codigo='" + id_SubTipoVinculacao + "'";
            return Session.CreateQuery(hql).List<T>();
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
