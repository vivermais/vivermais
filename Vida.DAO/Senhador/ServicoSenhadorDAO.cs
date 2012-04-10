using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Senhador;
using ViverMais.Model;
using System.Text.RegularExpressions;
using NHibernate;

namespace ViverMais.DAO.Senhador
{
    public class ServicoSenhadorDAO : SenhadorDAO, IServicoSenhador
    {
        public ServicoSenhadorDAO()
        {

        }

        T IServicoSenhador.BuscarPorNome<T>(string nome)
        {
            String hql = "from ViverMais.Model.ServicoSenhador servico where servico.Nome='" + nome + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();

        }

        bool IServicoSenhador.VerificarServicoEstabecimento(int co_servico, string co_unidade)
        {
            string hql = @"FROM ViverMais.Model.ServicoEstabelecimentoSenhador servico 
                WHERE servico.Estabelecimento.CNES=:co_unidade AND servico.Servico.Codigo=:co_servico";

            IQuery iQuery = this.Session.CreateQuery(hql);
            iQuery.SetString("co_unidade", co_unidade);
            iQuery.SetInt32("co_servico", co_servico);

            return iQuery.UniqueResult<ServicoEstabelecimentoSenhador>() != null;
        }

        IList<T> IServicoSenhador.BuscarServicoPorEstabelecimento<T>(string co_unidade)
        {
            string hql = "FROM ViverMais.Model.ServicoEstabelecimentoSenhador servico WHERE servico.Estabelecimento.CNES=:co_unidade ORDER BY servico.Servico.Nome";

            IQuery iQuery = this.Session.CreateQuery(hql);
            iQuery.SetString("co_unidade", co_unidade);

            return iQuery.List<T>();
        }
    }
}
