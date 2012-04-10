using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Misc;
using NHibernate;
using NHibernate.Criterion;
using ViverMais.Model;

namespace ViverMais.DAO.Misc
{
    public class ServicoSaudeDAO : ViverMaisServiceFacadeDAO, IServicoSaude
    {
        IList<B> IServicoSaude.BuscarBairrosServicos<B,U>(IList<U> unidades)
        {
            string hql = string.Empty;
            IList<ViverMais.Model.EstabelecimentoSaude> lista = (IList<ViverMais.Model.EstabelecimentoSaude>)(object)unidades;

            if (lista != null && lista.Count() > 0)
            {
                IList<Distrito> distritos = new List<Distrito>();

                string subconsulta = string.Empty;
                subconsulta += "(";

                foreach (ViverMais.Model.EstabelecimentoSaude estabelecimento in lista)
                {
                    if (!distritos.Contains(estabelecimento.Bairro.Distrito) && estabelecimento.Bairro.Distrito != null)
                        distritos.Add(estabelecimento.Bairro.Distrito);

                    subconsulta += estabelecimento.Bairro.Codigo + ",";
                }

                subconsulta = subconsulta.Remove(subconsulta.Length - 1, 1);
                subconsulta += ")";

                string subconsulta2 = string.Empty;
                subconsulta2 += "(";

                foreach (Distrito d in distritos)
                    subconsulta2 += d.Codigo + ",";

                subconsulta2 = subconsulta2.Remove(subconsulta2.Length - 1, 1);
                subconsulta2 += ")";

                hql = "FROM ViverMais.Model.Bairro AS b WHERE b.Distrito.Codigo IN " + subconsulta2;
                hql += " AND b.Codigo IN " + subconsulta;
                hql += " ORDER BY b.Nome";

                return Session.CreateQuery(hql).List<B>().Distinct().ToList();
            }
            else
                return new List<B>();
        }

        IList<E> IServicoSaude.BuscarEstabelecimentos<E>(int servico, string bairro)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.EstabelecimentoSaude e WHERE e IN ";
            hql += " (SELECT ELEMENTS (servico.Unidades) FROM ViverMais.Model.ServicoSaude servico WHERE servico.Codigo = " + servico + ")";
            hql += " AND e.Bairro.Distrito IN (SELECT b.Distrito FROM ViverMais.Model.Bairro b WHERE b.Codigo = " + bairro + ")";
            return Session.CreateQuery(hql).List<E>();
        }
    }
}
