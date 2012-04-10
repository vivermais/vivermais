﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.Model;

namespace ViverMais.DAO.AtendimentoMedico.Urgencia
{
    public class ProcedimentoNaoFaturavelDAO : UrgenciaServiceFacadeDAO, IProcedimentoNaoFaturavel
    {
        bool IProcedimentoNaoFaturavel.ValidarCadastroProcedimento(long codigo, int id)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ProcedimentoNaoFaturavel AS p WHERE p.CodigoProcedimento = " + codigo + " AND p.Codigo <> " + id;
            return Session.CreateQuery(hql).List<ProcedimentoNaoFaturavel>().Count > 0 ? false : true;
        }

        IList<T> IProcedimentoNaoFaturavel.BuscarPorNome<T>(string nome)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ProcedimentoNaoFaturavel AS p WHERE ";
            hql += " TRANSLATE(UPPER(p.Nome),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";
            hql += " LIKE ";
            hql += " TRANSLATE(UPPER('" + nome + "%'),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";
            hql += " ORDER BY p.Nome";
            //hql = "FROM ViverMais.Model.ProcedimentoNaoFaturavel AS p WHERE p.Nome LIKE '" + nome + "%'";
            //hql += " ORDER BY p.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        public ProcedimentoNaoFaturavelDAO()
        {
        }
    }
}