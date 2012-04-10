using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;

namespace ViverMais.DAO.Profissional
{
    public class ProfissionalSolicitanteDAO : ViverMaisServiceFacadeDAO, IProfissionalSolicitante
    {
        public ProfissionalSolicitanteDAO()
        {
        }

        #region IProfissionalSolicitante Members

        /// <summary>
        /// O codigo serve para testar a alteracao. Passa-se o codigo para verificar se não está atualizando
        /// um registro e este ficará idêntico a um já existente
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="categoria"></param>
        /// <param name="numeroconselho"></param>
        /// <param name="nome"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        IList<T> IProfissionalSolicitante.BuscaProfissionalPorNumeroConselhoECategoria<T>(string categoria, string numeroconselho, string nome, int codigo)
        {
            string hql = "from ViverMais.Model.ProfissionalSolicitante prof";
            hql += " where prof.OrgaoEmissorRegistro.CategoriaOcupacao.Codigo = '" + categoria + "'";
            hql += " and prof.Status = 'ATIVO'";
            if(numeroconselho != "")
                hql += " and prof.NumeroRegistro = '" + numeroconselho + "'";
            if(nome != "")
                hql += " and prof.Nome like '" + nome.ToUpper() + "%'";
            if (codigo != 0)
                hql += " and prof.Codigo <> " + codigo.ToString();
            return Session.CreateQuery(hql).List<T>();
        }

        T IProfissionalSolicitante.BuscaUltimoRegistroIncluido<T>()
        {
            string hql = "SELECT MAX(prof) from ViverMais.Model.ProfissionalSolicitante prof";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        #endregion
    }
}
