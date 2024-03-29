﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Movimentacao;
using ViverMais.Model;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using System.Collections;

namespace ViverMais.DAO.Vacina.Misc
{
    public class CartaoVacinaDAO : VacinaServiceFacadeDAO, ICartaoVacina
    {
        #region ICartaoVacina Members

        IList<T> ICartaoVacina.BuscarPorPaciente<T>(string co_paciente)
        {
            string hql = "FROM ViverMais.Model.CartaoVacina as cartao "
                //+ "WHERE (cartao.ItemDispensacao.Codigo IS NOT NULL AND cartao.ItemDispensacao.Dispensacao.Paciente.Codigo = '" + co_paciente + "')"
                //+ " UNION SELECT cartao FROM ViverMais.Model.CartaoVacina as cartao "
                //+ " WHERE cartao.Paciente.Codigo = '" + co_paciente + "'"
            + "WHERE cartao.Paciente.Codigo = '" + co_paciente + "'";
            //+ " ORDER BY cartao.Vacina.Nome, cartao.DoseVacina.NumeracaoDose, cartao.DataAplicacao";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ICartaoVacina.BuscarPorPacienteLoteVacina<T>(int co_paciente, int co_lote, int co_vacina)
        {
            string hql = "From ViverMais.Model.CartaoVacina as cartao "
                + " where cartao.Paciente.Codigo = " + co_paciente
                + " and cartao.Vacina.Codigo = " + co_vacina
                + " and cartao.LoteVacina.Codigo = " + co_lote;
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ICartaoVacina.BuscarPorDoseVacina<T>(int co_vacina, int co_dose, string co_paciente)
        {
            string hql = "From ViverMais.Model.CartaoVacina as cartao where cartao.Vacina.Codigo = " + co_vacina;
            hql += " and cartao.DoseVacina.Codigo = " + co_dose;
            hql += " and cartao.Paciente.Codigo='" + co_paciente + "'";
            //string hql = "From ViverMais.Model.CartaoVacina as cartao where cartao.Paciente.Codigo = '" + co_paciente + "' AND";
            //hql += " ((cartao.Vacina.Codigo=" + co_vacina + " AND cartao.DoseVacina.Codigo=" + co_dose + ")";
            //hql += " OR (cartao.ItemDispensacao.Lote.ItemVacina.Vacina.Codigo=" + co_vacina + " AND cartao.ItemDispensacao.Dose.Codigo = " + co_dose + "))";
            return Session.CreateQuery(hql).List<T>();
        }

        Hashtable ICartaoVacina.RetornaCartoesPaciente(string co_paciente)
        {
            Hashtable hash = new Hashtable();
            IParametrizacaoVacina iparametrizacao = Factory.GetInstance<IParametrizacaoVacina>();

            //carrega as vacinas q o paciente ja tomou
            IList<CartaoVacina> cartaohistorico = Factory.GetInstance<ICartaoVacina>().BuscarPorPaciente<CartaoVacina>(co_paciente);

            //carrega todas as vacinas do calendario
            IList<CartaoVacina> cartoescalendario = this.RetornarCartoesCalendario();

            foreach (CartaoVacina cartao in cartaohistorico)
            {
                IList<ParametrizacaoVacina> parametrizacoes = iparametrizacao.BuscaProximaDose<ParametrizacaoVacina>(cartao.DoseVacina.Codigo, cartao.Vacina.Codigo);

                cartao.ProximaDose = CartaoVacina.RetornaProximaDose(parametrizacoes, cartao.DataAplicacao);
                cartoescalendario = CartaoVacina.SubstituirCartao(cartoescalendario, cartao);
            }

            var cartoesVacina = from cartao in cartoescalendario
                         orderby cartao.Vacina.Nome, cartao.DoseVacina.NumeracaoDose, cartao.DataAplicacao
                         select cartao;

            var cartaocrianca = from cartao in cartoesVacina
                                where iparametrizacao.VerificarRequisitos(cartao.Vacina.Codigo, cartao.DoseVacina.Codigo, CartaoVacina.CRIANCA) == true
                                select cartao;

            var cartaoadulto = from cartao in cartoesVacina
                               where iparametrizacao.VerificarRequisitos(cartao.Vacina.Codigo, cartao.DoseVacina.Codigo, CartaoVacina.ADULTOIDOSO) == true
                               select cartao;
           
            var cartaoadolescente = from cartao in cartoesVacina
                                    where iparametrizacao.VerificarRequisitos(cartao.Vacina.Codigo, cartao.DoseVacina.Codigo, CartaoVacina.ADOLESCENTE) == true
                                    select cartao;

            hash.Add("cartaohistorico", cartaohistorico);
            hash.Add("cartaocrianca", cartaocrianca.ToList());
            hash.Add("cartaoadulto", cartaoadulto.ToList());
            hash.Add("cartaoadolescente", cartaoadolescente.ToList());

            return hash;
        }

        bool ICartaoVacina.ValidarAtualizacaoCartaoVacina(string co_usuario, DateTime dataaplicacao, int co_vacina, int co_dose)
        {
            string hql = string.Empty;
            hql = @"SELECT COUNT(cv.Codigo) FROM ViverMais.Model.CartaoVacina AS cv WHERE cv.Paciente.Codigo='" + co_usuario +
                "' AND TO_CHAR(cv.DataAplicacao,'DD/MM/YYYY')='" + dataaplicacao.ToString("dd/MM/yyyy") + "' AND cv.Vacina.Codigo=" + co_vacina +
                " AND cv.DoseVacina.Codigo=" + co_dose
                ;
            return int.Parse(Session.CreateQuery(hql).UniqueResult().ToString()) > 0 ? false : true;
        }

        T ICartaoVacina.BuscarPorItemDispensacao<T>(long co_item)
        {
            string hql = string.Empty;
            hql = @"FROM ViverMais.Model.CartaoVacina c WHERE c.ItemDispensacao.Codigo = " + co_item;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        private IList<CartaoVacina> RetornarCartoesCalendario()
        {
            IVacina iVacina = Factory.GetInstance<IVacina>();
            IList<CartaoVacina> cartoes = new List<CartaoVacina>();
            IList<ViverMais.Model.Vacina> vacinas = iVacina.BuscarVacinasDoCalendario<ViverMais.Model.Vacina>();

            foreach (ViverMais.Model.Vacina vacina in vacinas)
            {
                IList<DoseVacina> doses = iVacina.BuscarDoses<DoseVacina>(vacina.Codigo).OrderBy(s => s.NumeracaoDose).ToList();
                foreach (DoseVacina dose in doses)
                {
                    CartaoVacina cv = new CartaoVacina();
                    cv.Vacina = vacina;
                    cv.DoseVacina = dose;
                    cartoes.Add(cv);
                }
            }

            return cartoes;
        }

        #endregion
    }
}
