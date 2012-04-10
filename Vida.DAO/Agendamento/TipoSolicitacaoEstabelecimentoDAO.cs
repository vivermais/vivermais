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
    public class TipoSolicitacaoEstabelecimentoDAO : AgendamentoServiceFacadeDAO, ITipoSolicitacaoEstabelecimento
    {
        #region ITipoSolicitacaoEstabelecimento Members

        public TipoSolicitacaoEstabelecimentoDAO()
        {

        }

        T ITipoSolicitacaoEstabelecimento.BuscarTipoSolicitacaoPorEstabelecimento<T>(string codigo_Estabelecimento, string tipo)
        {
            //IEstabelecimentoSaude iEstabelecimentoSaude = Factory.GetInstance<IEstabelecimentoSaude>();
            //ViverMais.Model.EstabelecimentoSaude estabelecimentosDeSaude = iEstabelecimentoSaude.BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(cnes);
            //ViverMais.Model.EstabelecimentoSaude estabelecimentosDeSaude = new ViverMais.Model.EstabelecimentoSaude();

                //var estabelecimento = from p in estabelecimentosDeSaude.CNES
                //                      where p.Equals(cnes)
                //                      select p;
                string hql = String.Empty;
                hql += "from ViverMais.Model.ParametroTipoSolicitacaoEstabelecimento parametro";
                hql += " where parametro.CodigoEstabelecimento='" + codigo_Estabelecimento+"'";
                hql += " and parametro.TipoSolicitacao='" + tipo+"'";
                return Session.CreateQuery(hql).UniqueResult<T>();
            //}            
        }

        IList<T> ITipoSolicitacaoEstabelecimento.BuscaPorEstabelecimento<T>(string cnes)
        {
            string hql = String.Empty;
            hql += "from ViverMais.Model.ParametroTipoSolicitacaoEstabelecimento parametro";
            hql += " where parametro.CodigoEstabelecimento='" + cnes + "'";
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
