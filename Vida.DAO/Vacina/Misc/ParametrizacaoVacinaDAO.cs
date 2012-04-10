using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.DAO.Vacina.Misc
{
    public class ParametrizacaoVacinaDAO : VacinaServiceFacadeDAO, IParametrizacaoVacina
    {
        IList<T> IParametrizacaoVacina.BuscarPorVacina<T>(int co_vacina)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ParametrizacaoVacina AS p WHERE p.ItemDoseVacina.Vacina.Codigo = " + co_vacina;
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IParametrizacaoVacina.BuscarPorDoseVacina<T>(int co_dosevacina, int co_vacina)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ParametrizacaoVacina AS p WHERE p.ItemDoseVacina.DoseVacina.Codigo = " + co_dosevacina
                  + " AND p.ItemDoseVacina.Vacina.Codigo = " + co_vacina;
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IParametrizacaoVacina.BuscarPorDoseVacina<T>(int co_item)
        {
            string hql = string.Empty;
            hql = @"FROM ViverMais.Model.ParametrizacaoVacina AS p WHERE " +
                  " p.ItemDoseVacina.Codigo = " + co_item;
            return Session.CreateQuery(hql).List<T>();
        }

        /// <summary>
        /// Somente vacinas que possuem o evento após possuem data de próxima dose
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="co_dosevacina"></param>
        /// <param name="co_vacina"></param>
        /// <returns></returns>
        IList<T> IParametrizacaoVacina.BuscaProximaDose<T>(int co_dosevacina, int co_vacina)
        {
            string hql = "FROM ViverMais.Model.ParametrizacaoVacina AS p WHERE p.ItemDoseVacina.DoseVacina.Codigo = " + co_dosevacina
                   + " AND p.ItemDoseVacina.Vacina.Codigo = " + co_vacina
                   + " and p.Tipo = '" + ParametrizacaoVacina.POR_EVENTO + "'";

            return Session.CreateQuery(hql).List<T>();
        }

        /// <summary>
        /// Verifica se a vacina pertence a uma categoria de cartao de vacina
        /// Exemplo: Verifica se a vacina "A" pertence ao cartao da criança
        /// </summary>
        /// <param name="co_vacina"></param>
        /// <param name="co_paciente"></param>
        /// <returns></returns>
        bool IParametrizacaoVacina.VerificarRequisitos(int co_vacina, int co_dose, int valor)
        {
            //Obtém todos os parâmetros de uma Vacina
            IList<object[]> parametros = Session.CreateQuery("SELECT p.FaixaEtariaInicial, p.FaixaEtariaFinal, p.UnidadeTempoInicial, p.UnidadeTempoFinal FROM ViverMais.Model.ParametrizacaoVacina AS p WHERE p.Tipo='" + ParametrizacaoVacina.POR_FAIXA_ETARIA + "' AND p.ItemDoseVacina.Vacina.Codigo = " + co_vacina + " AND p.ItemDoseVacina.DoseVacina.Codigo =" + co_dose).List<object[]>();

            if (valor == CartaoVacina.CRIANCA)
            {
                var consulta = from parametro in parametros
                               where
                               ParametrizacaoVacina.ConverteUnidadeTempo(float.Parse(parametro[0].ToString()), int.Parse(parametro[2].ToString()), (int)ParametrizacaoVacina.UNIDADE_TEMPO.ANOS) <= 10
                               select parametro;

                if (consulta.ToList().Count() > 0)
                    return true;
            }
            else
            {
                if (valor == CartaoVacina.ADOLESCENTE)
                {
                    var consulta = from parametro in parametros
                                   where
                                   ParametrizacaoVacina.ConverteUnidadeTempo(float.Parse(parametro[0].ToString()), int.Parse(parametro[2].ToString()), (int)ParametrizacaoVacina.UNIDADE_TEMPO.ANOS) <= 19
                                   && ParametrizacaoVacina.ConverteUnidadeTempo(float.Parse(parametro[1].ToString()), int.Parse(parametro[3].ToString()), (int)ParametrizacaoVacina.UNIDADE_TEMPO.ANOS) >= 11
                                   select parametro;

                    if (consulta.ToList().Count() > 0)
                        return true;
                }
                else
                {
                    if (valor == CartaoVacina.ADULTOIDOSO)
                    {
                        var consulta = from parametro in parametros
                                       where
                                       ParametrizacaoVacina.ConverteUnidadeTempo(float.Parse(parametro[1].ToString()), int.Parse(parametro[3].ToString()), (int)ParametrizacaoVacina.UNIDADE_TEMPO.ANOS) >= 20
                                       select parametro;

                        if (consulta.ToList().Count() > 0)
                            return true;
                    }
                }
            }

            return false;
        }
    }
}
