using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;

namespace ViverMais.DAO.Vacina.Misc
{
    public class FabricanteVacinaDAO : VacinaServiceFacadeDAO, IFabricanteVacina
    {
        IList<T> IFabricanteVacina.BuscaPorNome<T>(string nome)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.FabricanteVacina AS f WHERE f.Nome = '" + nome + "%' ";
            return Session.CreateQuery(hql).List<T>();
        }

        /// <summary>
        /// Lista todos os fabricantes ativos
        /// </summary>
        /// <typeparam name="T">FabricanteVacina</typeparam>
        /// <returns></returns>
        IList<T> IFabricanteVacina.ListarFabricantesAtivos<T>()
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.FabricanteVacina AS f WHERE f.Situacao = 'A'";
            return Session.CreateQuery(hql).List<T>();
        }

        /// <summary>
        /// Lista todos os fabricantes por vacina
        /// </summary>
        /// <typeparam name="T">FabricanteVacina</typeparam>
        /// <param name="nome"></param>
        /// <returns></returns>
        IList<T> IFabricanteVacina.ListarFabricantesPorVacina<T>(int co_vacina)
        {
            string hql = string.Empty;
            hql = "SELECT f FROM ViverMais.Model.FabricanteVacina AS f, ViverMais.Model.ItemVacina iv WHERE f.Situacao = 'A' "+
                   " AND f.Codigo = iv.FabricanteVacina.Codigo AND iv.Vacina.Codigo = " + co_vacina;
            return Session.CreateQuery(hql).List<T>();
        }

        bool IFabricanteVacina.ValidarCadastro<T>(int co_fabricante, string cnpj)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.FabricanteVacina fv WHERE fv.Codigo <> " + co_fabricante;
            hql += " AND fv.CNPJ='" + cnpj + "'";
            return Session.CreateQuery(hql).List<T>().Count() > 0 ? false : true;
        }
    }
}
