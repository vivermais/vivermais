using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;

namespace ViverMais.DAO.Procedimento.Misc
{
    public class CidDAO : ViverMaisServiceFacadeDAO, ICid
    {
        T ICid.BuscarPorCodigo<T>(string co_cid)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Cid AS c WHERE c.Codigo = '" + co_cid + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        bool ICid.ExisteVinculoProcedimentoCid(string co_procedimento, string co_cid)
        {
            string hql = "From ViverMais.Model.ProcedimentoCid AS pc WHERE pc.Procedimento.Codigo = '" + co_procedimento + "'";
            hql += " and pc.Cid.Codigo = '" + co_cid + "'";
            return Session.CreateQuery(hql).List().Count > 0 ? true : false;
        }
        
        IList<T> ICid.BuscarPorProcedimento<T>(string co_procedimento)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ProcedimentoCid AS pc WHERE pc.Procedimento.Codigo = '" + co_procedimento + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ICid.BuscarPorProcedimento<T>(string co_procedimento, string sexo)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ProcedimentoCid AS pc WHERE pc.Procedimento.Codigo = '" + co_procedimento + "'";
            hql += " and (pc.Cid.SexoAplicado = '" + sexo + "' or pc.Cid.SexoAplicado = 'I')";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<string> ICid.ListarGrupos()
        {
            char inicio = 'A';
            char termino = 'Z';
            List<string> grupos = new List<string>();

            for (int i = (int)inicio; i <= (int)termino; i++)
                grupos.Add(Convert.ToChar(i).ToString());

            return grupos;
            //for(char letra='A'
            //string hql = string.Empty;

            //hql = "SELECT SUBSTR(c.Codigo,1,1) FROM ViverMais.Model.Cid c GROUP BY SUBSTR(c.Codigo,1,1) ORDER BY SUBSTR(c.Codigo,1,1)";
            //return Session.CreateQuery(hql).List<string>();
        }

        IList<T> ICid.BuscarPorGrupo<T>(string codigo)
        {
            string hql = "FROM ViverMais.Model.Cid c WHERE c.Codigo LIKE '" + codigo + "%' ";
            hql += " ORDER BY c.Nome ";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ICid.BuscarPorInicioNome<T>(string nome)
        {
            string hql = "FROM ViverMais.Model.Cid c WHERE ";
            hql += " TRANSLATE(UPPER(c.Nome),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";
            hql += " LIKE TRANSLATE(UPPER('" + nome + "%'),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";
            hql += " ORDER BY c.Nome ";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ICid.BuscarPorGrupo<T>(string codigo, string co_procedimento)
        {
            string hql = string.Empty;
            hql = "SELECT DISTINCT pc.Cid FROM ViverMais.Model.ProcedimentoCid AS pc WHERE pc.Procedimento.Codigo = '" + co_procedimento + "'";
            hql += " AND pc.Cid.Codigo LIKE '" + codigo + "%' AND pc.DataCompetencia=(SELECT MAX(pc2.DataCompetencia) FROM ViverMais.Model.ProcedimentoCid pc2";
            hql += " WHERE pc2.Procedimento.Codigo = '" + co_procedimento + "' AND pc2.Cid.Codigo LIKE '" + codigo + "%')";
            hql += " ORDER BY pc.Cid.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ICid.BuscarPorInicioNome<T>(string co_procedimento, string nome)
        {
            string hql = string.Empty;
            hql = "SELECT DISTINCT pc.Cid FROM ViverMais.Model.ProcedimentoCid AS pc WHERE pc.Procedimento.Codigo = '" + co_procedimento + "'";
            hql += " AND TRANSLATE(UPPER(pc.Cid.Nome),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";
            hql += " LIKE TRANSLATE(UPPER('" + nome + "%'),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";
            hql += " AND pc.DataCompetencia=(SELECT MAX(pc2.DataCompetencia) FROM ViverMais.Model.ProcedimentoCid pc2";
            hql += " WHERE pc2.Procedimento.Codigo = '" + co_procedimento + "' ";
            hql += " AND TRANSLATE(UPPER(pc2.Cid.Nome),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";
            hql += " LIKE TRANSLATE(UPPER('" + nome + "%'),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc'))";
            hql += " ORDER BY pc.Cid.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ICid.BuscarPorCodigo<T>(string codigo, string co_procedimento)
        {
            string hql = string.Empty;
            hql = "SELECT DISTINCT pc.Cid FROM ViverMais.Model.ProcedimentoCid AS pc WHERE pc.Procedimento.Codigo = '" + co_procedimento + "'";
            hql += " AND pc.Cid.Codigo='" + codigo + "' AND pc.DataCompetencia=(SELECT MAX(pc2.DataCompetencia) FROM ViverMais.Model.ProcedimentoCid pc2";
            hql += " WHERE pc2.Procedimento.Codigo = '" + co_procedimento + "' AND pc2.Cid.Codigo = '" + codigo + "')";
            hql += " ORDER BY pc.Cid.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        bool ICid.CidPermitidoParaSexo(string co_cid, string sexo)
        {
            string hql = " from ViverMais.Model.Cid cid where cid.Codigo = '" + co_cid + "'";
            hql += " and (cid.SexoAplicado = '" + sexo.ToUpper() + "' or cid.SexoAplicado = 'I')";
            return Session.CreateQuery(hql).List().Count > 0 ? true : false;
        }

        //T ICid.BuscarCidPrincipalPorProcedimento<T>(string co_procedimento)
        //{
        //    string hql = "from ViverMais.Model.ProcedimentoCid  as pc WHERE c.Codigo LIKE '" + codigo + "%' ";
        //    hql += " ORDER BY c.Nome ";
        //    return Session.CreateQuery(hql).List<T>();
        //}
    }
}
