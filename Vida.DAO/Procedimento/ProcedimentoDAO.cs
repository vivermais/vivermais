﻿using System;
using System.Collections.Generic;
using System.Collections;
using NHibernate;
using NHibernate.Criterion;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;

namespace ViverMais.DAO.Procedimento
{
    public class ProcedimentoDAO: ViverMaisServiceFacadeDAO, IProcedimento
    {
        #region IProcedimento
            IList<T> IProcedimento.BuscarPorGrupo<T>(string co_grupo) 
            {
                string hql = string.Empty;
                hql = "FROM ViverMais.Model.Procedimento AS p WHERE p.Codigo LIKE '" + co_grupo.ToUpper() + "%'";
                return Session.CreateQuery(hql).List<T>();
            }

            IList<T> IProcedimento.BuscarPorSubGrupo<T>(string co_subgrupo)
            {
                string hql = string.Empty;
                hql = "FROM ViverMais.Model.Procedimento AS p WHERE p.Codigo LIKE '" + co_subgrupo.ToUpper() + "%'";
                return Session.CreateQuery(hql).List<T>();
            }

            IList<T> IProcedimento.BuscarPorForma<T>(string co_forma)
            {
                string hql = string.Empty;
                hql = "FROM ViverMais.Model.Procedimento AS p WHERE p.Codigo LIKE '" + co_forma + "%'";
                return Session.CreateQuery(hql).List<T>();
            }

            IList<T> IProcedimento.BuscarPorOcupacao<T>(string co_ocupacao) 
            {
                string hql = string.Empty;
                hql = "FROM ViverMais.Model.ProcedimentoOcupacao AS po WHERE po.CBO.Codigo = '" + co_ocupacao + "'";
                return Session.CreateQuery(hql).List<T>();
            }
            
            IList<T> IProcedimento.BuscarPorNome<T>(string nome)
            {
                string hql = string.Empty;
                hql = "FROM ViverMais.Model.Procedimento AS p WHERE ";
                hql += " TRANSLATE(UPPER(p.Nome),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";
                hql += " LIKE ";
                hql += " TRANSLATE(UPPER('" + nome + "%'),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";
                //hql = "FROM ViverMais.Model.Procedimento AS p WHERE p.Nome LIKE  '" + nome.ToUpper() + "%'";
                hql += " ORDER BY p.Nome";
                return Session.CreateQuery(hql).List<T>();
            }

            IList<T> IProcedimento.BuscarPorCid<T>(string co_cid)
            {
                string hql = string.Empty;
                hql = "FROM ViverMais.Model.ProcedimentoCid AS pc WHERE pc.Cid.Codigo = '" + co_cid + "'";
                return Session.CreateQuery(hql).List<T>();
            }
            T IProcedimento.BuscarProcedimentoAPAC<T>(string co_procedimento)
            {
                string hql = string.Empty;
                hql = "Select pr.Procedimento FROM ViverMais.Model.ProcedimentoRegistro as pr where pr.Procedimento.Codigo = '"+co_procedimento +"'";
                hql += " and (pr.Registro.Codigo = 06 or pr.Registro.Codigo = 07)";
                return Session.CreateQuery(hql).UniqueResult<T>();
            }

            bool IProcedimento.CBOExecutaProcedimento(string co_procedimento, string cbo)
            {
                string hql = string.Empty;
                hql = "SELECT po.Procedimento FROM ViverMais.Model.ProcedimentoOcupacao po WHERE po.Procedimento.Codigo='" + co_procedimento + "' AND po.CBO.Codigo='" + cbo + "'";
                return Session.CreateQuery(hql).List<ViverMais.Model.Procedimento>().Count() > 0 ? true : false;
            }

        #endregion

        public ProcedimentoDAO()
        {
        }
    }
}
