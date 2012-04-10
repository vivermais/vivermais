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
    public class ProfissionalDAO : ViverMaisServiceFacadeDAO, IProfissional
    {
        public ProfissionalDAO()
        {
        }

        #region IProfissional Members
        IList<E> IProfissional.BuscarPorEquipe<A, C, E>(A area_equipe, C codigo_equipe) 
        {
            Area area     = (Area)((object)area_equipe);
            string codigo = (string)((object)codigo_equipe);

            string hql = string.Empty;
            hql = "FROM ViverMais.Model.EquipeProfissional AS ep WHERE ep.Equipe.Area.Codigo = '" + area.Codigo + "' AND ep.Equipe.Area.Municipio.Codigo = '" + area.Municipio.Codigo + "' AND ep.Equipe.Codigo = '" + codigo + "'";
            return Session.CreateQuery(hql).List<E>();
        }

        T IProfissional.BuscarProfissionalPorCNS<T>(string cartaoSUS)
        {
            string hql = "from ViverMais.Model.Profissional profissional";
            hql += " where profissional.CartaoSUS = '"+ cartaoSUS+"'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IProfissional.BuscaProfissionalPorVinculoCBO<T>(int numeroconselho,string cbo, string cnes)
        {            
            string hql = "from ViverMais.Model.VinculoProfissional vinculo";
            //hql += " where vinculo.CBO.CategoriaOcupacao.Codigo = '"+categoria+"'";
            hql += " and vinculo.RegistroConselho = '"+numeroconselho+"'";
            hql += " and vinculo.CBO.Codigo = '"+cbo+"'";
            hql += " and vinculo.EstabelecimentoSaude.CNES = '" + cnes + "'";
            hql += " and vinculo.Status = 'A'"; //Vinculo Ativo
            IQuery query = Session.CreateQuery(hql);
            return query.UniqueResult<T>();
        }
        
        IList<T> IProfissional.BuscaProfissionalPorNumeroConselhoECategoria<T>(int categoria, string numeroconselho, string nome)
        {
            string hql = "Select v.Profissional From ViverMais.Model.VinculoProfissional v";
            hql += " where v.OrgaoEmissorRegistroConselho.CategoriaOcupacao.Codigo = '" + categoria + "'";
            if(numeroconselho != "")
                hql += " and v.RegistroConselho = '" + numeroconselho + "'";
            if(nome != "")
                hql += " and v.Profissional.Nome like '"+nome.ToUpper()+"%'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IProfissional.BuscaVinculoPorNumeroConselhoECategoria<T>(int categoria, string numeroconselho, string nome)
        {
            string hql = "Select v From ViverMais.Model.VinculoProfissional v";
            hql += " where v.OrgaoEmissorRegistroConselho.CategoriaOcupacao.Codigo = '" + categoria + "'";
            if (numeroconselho != "")
                hql += " and v.RegistroConselho = '" + numeroconselho + "'";
            if (nome != "")
                hql += " and v.Profissional.Nome like '" + nome.ToUpper() + "%'";
            return Session.CreateQuery(hql).List<T>();
        }

        #endregion
    }
}
