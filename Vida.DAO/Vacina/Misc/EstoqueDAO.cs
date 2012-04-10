﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.Model;
using NHibernate.Criterion;
using NHibernate;

namespace ViverMais.DAO.Vacina.Misc
{
    public class EstoqueDAO : VacinaServiceFacadeDAO, IEstoque
    {
        T IEstoque.BuscarItemEstoque<T>(object co_lote, object co_sala)
        {
            string hql = "FROM ViverMais.Model.EstoqueVacina e Where"
                + " e.Lote.Codigo = " + co_lote
                + " and e.Sala.Codigo = " + co_sala;

            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        IList<T> IEstoque.BuscarItensEstoque<T>(int co_sala, int co_vacina, int co_fabricante, int qtdaplicacao)
        {
            string hql = "from ViverMais.Model.EstoqueVacina estoque where estoque.Sala.Codigo=" + co_sala;
            
            if (co_vacina != -1)
                hql += " and estoque.Lote.ItemVacina.Vacina.Codigo=" + co_vacina;

            if(co_fabricante != -1)
                hql += " and estoque.Lote.ItemVacina.FabricanteVacina.Codigo=" + co_fabricante;

            if (qtdaplicacao != -1)
                hql += " and estoque.Lote.ItemVacina.Aplicacao=" + qtdaplicacao;

            //return (IList<T>)(object)Session.CreateQuery(hql).List<EstoqueVacina>().OrderBy(p => p.NomeVacina).ThenBy(p => p.NomeFabricanteVacina).ThenBy(p => p.QtdAplicacaoVacina).ThenBy(p => p.IdentificacaoLote).ToList();
            var result = (from estoque in Session.CreateQuery(hql).List<EstoqueVacina>()
                          orderby estoque.Lote.ItemVacina.Vacina.Nome,
                              estoque.Lote.ItemVacina.FabricanteVacina.Nome,
                              estoque.Lote.ItemVacina.Aplicacao, estoque.Lote.Identificacao
                          select estoque);

            return (IList<T>)(object)result.ToList();
        }
    }
}