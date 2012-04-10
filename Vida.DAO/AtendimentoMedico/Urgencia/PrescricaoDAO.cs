using System;
using System.Data;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using System.Collections;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;

namespace ViverMais.DAO.AtendimentoMedico.Urgencia
{
    public class PrescricaoDAO : UrgenciaServiceFacadeDAO, IPrescricao
    {
        #region IPrescricao

        IList<T> IPrescricao.ListarMedicamentos<T>(string[] codigosmedicamentos)
        {
            ISession session = NHibernateHttpHelper.GetCurrentSession("ViverMais");

            using (session.BeginTransaction())
            {
                string busca = string.Empty;
                busca += "(";

                foreach (string codigo in codigosmedicamentos)
                {
                    busca += "'" + codigo + "'" + ",";
                }

                busca = busca.Remove(busca.Length - 1, 1);
                busca += ")";

                return session.CreateQuery("FROM ViverMais.Model.Medicamento AS m WHERE m.Codigo IN " + busca).List<T>();
            }
        }

        //IList<T> IPrescricao.BuscarKitsPA<T>(long co_prescricao)
        //{
        //    string hql = string.Empty;
        //    hql = "from ViverMais.Model.PrescricaoKitPA kit where kit.Prescricao.Codigo = " + co_prescricao;
        //    hql += " order by kit.KitPA.Nome";
        //    return Session.CreateQuery(hql).List<T>();
        //}

        IList<T> IPrescricao.BuscarMedicamentos<T>(long co_prescricao)
        {
            string hql = string.Empty;
            hql = "from ViverMais.Model.PrescricaoMedicamento pm where pm.Prescricao.Codigo = " + co_prescricao;
            IList<PrescricaoMedicamento> lista = (IList<PrescricaoMedicamento>)(object)Session.CreateQuery(hql).List<T>();

            IMedicamento iMedicamento = Factory.GetInstance<IMedicamento>();
            foreach (PrescricaoMedicamento medicamento in lista)
                medicamento.ObjetoMedicamento = iMedicamento.BuscarPorCodigo<Medicamento>(medicamento.Medicamento);

            return (IList<T>)lista;
        }

        T IPrescricao.BuscarMedicamento<T>(long co_prescricao, int co_medicamento)
        {
            string hql = string.Empty;
            hql = "from ViverMais.Model.PrescricaoMedicamento pm where pm.Prescricao.Codigo = " + co_prescricao;
            hql += " AND pm.Medicamento=" + co_medicamento;
            PrescricaoMedicamento prescricao = (PrescricaoMedicamento)(object)Session.CreateQuery(hql).UniqueResult<T>();

            if (prescricao != null)
            {
                IMedicamento iMedicamento = Factory.GetInstance<IMedicamento>();
                prescricao.ObjetoMedicamento = iMedicamento.BuscarPorCodigo<Medicamento>(prescricao.Medicamento);
            }

            return (T)(object)prescricao;
        }

        T IPrescricao.BuscarProcedimentoNaoFaturavel<T>(long co_prescricao, int co_procedimento)
        {
            string hql = string.Empty;
            hql = "from ViverMais.Model.PrescricaoProcedimentoNaoFaturavel pm where pm.Prescricao.Codigo = " + co_prescricao;
            hql += " AND pm.Procedimento.Codigo=" + co_procedimento;

            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        IList<T> IPrescricao.BuscarProcedimentos<T>(long co_prescricao)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.PrescricaoProcedimento AS pp WHERE pp.Prescricao.Codigo = " + co_prescricao;
            //return Session.CreateQuery(hql).List<T>();

            IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();
            ICid iCid = Factory.GetInstance<ICid>();

            IList<PrescricaoProcedimento> procedimentos = (IList<PrescricaoProcedimento>)(object)Session.CreateQuery(hql).List<T>();
            foreach (PrescricaoProcedimento procedimento in procedimentos)
            {
                procedimento.Procedimento = iProcedimento.BuscarPorCodigo<ViverMais.Model.Procedimento>(procedimento.CodigoProcedimento);

                if (!string.IsNullOrEmpty(procedimento.CodigoCid))
                    procedimento.Cid = iCid.BuscarPorCodigo<Cid>(procedimento.CodigoCid);
            }

            return (List<T>)procedimentos;
        }

        T IPrescricao.BuscarProcedimento<T>(long co_prescricao, string co_procedimento)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.PrescricaoProcedimento AS pp WHERE pp.Prescricao.Codigo = " + co_prescricao;
            hql += " AND pp.CodigoProcedimento='" + co_procedimento + "'";

            IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();
            ICid iCid = Factory.GetInstance<ICid>();

            PrescricaoProcedimento procedimento = (PrescricaoProcedimento)(object)Session.CreateQuery(hql).UniqueResult<T>();

            if (procedimento != null)
            {
                procedimento.Procedimento = iProcedimento.BuscarPorCodigo<ViverMais.Model.Procedimento>(procedimento.CodigoProcedimento);

                if (!string.IsNullOrEmpty(procedimento.CodigoCid))
                    procedimento.Cid = iCid.BuscarPorCodigo<Cid>(procedimento.CodigoCid);
            }

            return (T)(object)procedimento;
        }

        IList<T> IPrescricao.BuscarProcedimentosNaoFaturaveis<T>(long co_prescricao)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.PrescricaoProcedimentoNaoFaturavel AS ppnf WHERE ppnf.Prescricao.Codigo = " + co_prescricao;
            return Session.CreateQuery(hql).List<T>();
        }

        //IList<T> IPrescricao.BuscarPrescricaoPorProntuario<T>(long co_prontuario, char status)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.Prescricao AS p WHERE p.Prontuario.Codigo IS NOT NULL AND p.Prontuario.Codigo = " + co_prontuario + "";
        //    hql += " AND p.Status = '" + status + "'";
        //    return Session.CreateQuery(hql).List<T>();
        //}

        IList<T> IPrescricao.BuscarPorProntuario<T>(long co_prontuario)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Prescricao AS p WHERE p.Prontuario.Codigo = " + co_prontuario + " ORDER BY p.Data";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IPrescricao.BuscarPorProntuario<T>(long co_prontuario, char status)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Prescricao AS p WHERE p.Prontuario.Codigo = " + co_prontuario;
            hql += " AND p.Status='" + status + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        ///// <summary>
        ///// Retorna a hora válida para a prescrição
        ///// </summary>
        ///// <param name="co_unidade">código da unidade</param>
        ///// <returns>data válida</returns>
        //DateTime IPrescricao.RetornaDataValidaPrescricao(string co_unidade)
        //{
        //    ViverMais.Model.HorarioUnidade horariounidade = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.HorarioUnidade>(co_unidade);
        //    DateTime data_valida;

        //    char[] separador = { ':' };
        //    int hora_unidade = int.Parse(horariounidade.Horario.Split(separador)[0]);
        //    int minuto_unidade = int.Parse(horariounidade.Horario.Split(separador)[1]);

        //    int hora_atual = DateTime.Now.Hour;

        //    if (hora_atual >= hora_unidade)
        //        data_valida = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hora_unidade, minuto_unidade, 0).AddDays(1);
        //    else
        //        data_valida = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hora_unidade, minuto_unidade, 0);

        //    //return DateTime.Now.AddDays(1);
        //    return data_valida;
        //}

        //void IPrescricao.AtualizarStatusPrescricaoAgendada(long co_prontuario)
        //{
        //    using (Session.BeginTransaction())
        //    {
        //        try
        //        {
        //            IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
        //            Prescricao prescricao = iPrescricao.BuscarPrescricaoPorProntuario<Prescricao>(co_prontuario, Convert.ToChar(Prescricao.StatusPrescricao.Agendada)).FirstOrDefault();

        //            if (prescricao != null)
        //            {
        //                if (DateTime.Compare(DateTime.Now, prescricao.UltimaDataValida.AddDays(-1)) >= 0
        //                    && DateTime.Compare(DateTime.Now, prescricao.UltimaDataValida) < 0)
        //                {
        //                    prescricao.Status = Convert.ToChar(Prescricao.StatusPrescricao.Valida);
        //                    Prescricao valida = iPrescricao.BuscarPrescricaoPorProntuario<Prescricao>(co_prontuario, Convert.ToChar(Prescricao.StatusPrescricao.Valida)).FirstOrDefault();
        //                    valida.Status = Convert.ToChar(Prescricao.StatusPrescricao.Suspensa);
        //                    Session.Update(prescricao);
        //                    Session.Update(valida);
        //                }
        //                else
        //                {
        //                    if (DateTime.Compare(DateTime.Now, prescricao.UltimaDataValida) >= 0)
        //                    {
        //                        prescricao.Status = Convert.ToChar(Prescricao.StatusPrescricao.Invalida);
        //                        prescricao.UltimaDataValida = iPrescricao.RetornaDataValidaPrescricao(prescricao.Prontuario.CodigoUnidade);
        //                        Prescricao valida = iPrescricao.BuscarPrescricaoPorProntuario<Prescricao>(co_prontuario, Convert.ToChar(Prescricao.StatusPrescricao.Valida)).FirstOrDefault();
        //                        valida.Status = Convert.ToChar(Prescricao.StatusPrescricao.Suspensa);
        //                        Session.Update(prescricao);
        //                        Session.Update(valida);
        //                    }
        //                }

        //                Session.Transaction.Commit();
        //            }
        //        }
        //        catch (Exception f)
        //        {
        //            Session.Transaction.Rollback();
        //            throw f;
        //        }
        //    }
        //}

        void IPrescricao.AtualizarStatusPrescricoesProntuario(long co_prontuario)
        {
            //Factory.GetInstance<IPrescricao>().AtualizarStatusPrescricaoAgendada(co_prontuario);
            IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();

            using (Session.BeginTransaction())
            {
                try
                {
                    char[] status = { Convert.ToChar(Prescricao.StatusPrescricao.Valida), Convert.ToChar(Prescricao.StatusPrescricao.Invalida) };

                    List<long> prescricoesatualizadas = new List<long>();
                    Prescricao prescricaoagendada = iPrescricao.BuscarPorProntuario<Prescricao>(co_prontuario, Convert.ToChar(Prescricao.StatusPrescricao.Agendada)).FirstOrDefault();

                    if (prescricaoagendada != null)
                    {
                        if (DateTime.Compare(DateTime.Now, prescricaoagendada.UltimaDataValida.AddDays(-1)) >= 0
                            && DateTime.Compare(DateTime.Now, prescricaoagendada.UltimaDataValida) < 0)
                        {
                            prescricaoagendada.Status = Convert.ToChar(Prescricao.StatusPrescricao.Valida);
                            Prescricao valida = iPrescricao.BuscarPorProntuario<Prescricao>(co_prontuario, Convert.ToChar(Prescricao.StatusPrescricao.Valida)).FirstOrDefault();
                            valida.Status = Convert.ToChar(Prescricao.StatusPrescricao.Suspensa);
                            Session.Update(prescricaoagendada);
                            Session.Update(valida);
                            prescricoesatualizadas.Add(prescricaoagendada.Codigo);
                            prescricoesatualizadas.Add(valida.Codigo);
                        }
                        else
                        {
                            if (DateTime.Compare(DateTime.Now, prescricaoagendada.UltimaDataValida) >= 0)
                            {
                                prescricaoagendada.Status = Convert.ToChar(Prescricao.StatusPrescricao.Invalida);
                                DateTime horaatual = DateTime.Now;
                                prescricaoagendada.UltimaDataValida = new DateTime(horaatual.Year, horaatual.Month, horaatual.Day, prescricaoagendada.UltimaDataValida.Hour, prescricaoagendada.UltimaDataValida.Minute, prescricaoagendada.UltimaDataValida.Second).AddDays(1);

                                Prescricao valida = iPrescricao.BuscarPorProntuario<Prescricao>(co_prontuario, Convert.ToChar(Prescricao.StatusPrescricao.Valida)).FirstOrDefault();
                                valida.Status = Convert.ToChar(Prescricao.StatusPrescricao.Suspensa);
                                Session.Update(prescricaoagendada);
                                Session.Update(valida);
                                prescricoesatualizadas.Add(prescricaoagendada.Codigo);
                                prescricoesatualizadas.Add(valida.Codigo);
                            }
                        }
                    }

                    foreach (char s in status)
                    {
                        Prescricao prescricaoatual = Factory.GetInstance<IPrescricao>().BuscarPorProntuario<Prescricao>(co_prontuario, s).FirstOrDefault();

                        if (prescricaoatual != null && !prescricoesatualizadas.Contains(prescricaoatual.Codigo))
                        {
                            if (DateTime.Compare(prescricaoatual.UltimaDataValida, DateTime.Now) <= 0)
                            {
                                if (prescricaoatual.Status == Convert.ToChar(Prescricao.StatusPrescricao.Valida))
                                    prescricaoatual.Status = Convert.ToChar(Prescricao.StatusPrescricao.Invalida);

                                DateTime horaatual = DateTime.Now;
                                prescricaoatual.UltimaDataValida = new DateTime(horaatual.Year, horaatual.Month, horaatual.Day, prescricaoatual.UltimaDataValida.Hour, prescricaoatual.UltimaDataValida.Minute, prescricaoatual.UltimaDataValida.Second).AddDays(1);
                                Session.Update(prescricaoatual);
                            }
                        }
                    }

                    //IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();

                    //foreach (char s in status)
                    //{
                    //    Prescricao prescricao = iPrescricao.BuscarPrescricaoPorProntuarioStatus<Prescricao>(co_prontuario, s).FirstOrDefault();

                    //    if (prescricao != null)
                    //    {
                    //        if (DateTime.Compare(prescricao.UltimaDataValida, DateTime.Now) <= 0)
                    //        {
                    //            if (prescricao.Status == Convert.ToChar(Prescricao.StatusPrescricao.Valida))
                    //                prescricao.Status = Convert.ToChar(Prescricao.StatusPrescricao.Invalida);

                    //            prescricao.UltimaDataValida = prescricao.UltimaDataValida.AddDays(1);
                    //                //iPrescricao.RetornaDataValidaPrescricao(prescricao.Prontuario.CodigoUnidade);
                    //            Session.Update(prescricao);
                    //        }
                    //    }
                    //}

                    Session.Transaction.Commit();
                }
                catch (Exception f)
                {
                    Session.Transaction.Rollback();
                    throw f;
                }
            }
        }

        T IPrescricao.RetornaPrescricaoVigente<T>(long co_prontuario)
        {
            IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            Prescricao prescricao = iPrescricao.BuscarPorProntuario<Prescricao>(co_prontuario).Where(p => p.Status == Convert.ToChar(Prescricao.StatusPrescricao.Valida)).FirstOrDefault();

            if (prescricao != null)
                return (T)(object)prescricao;

            prescricao = iPrescricao.BuscarPorProntuario<Prescricao>(co_prontuario).Where(p => p.Status == Convert.ToChar(Prescricao.StatusPrescricao.Invalida)).FirstOrDefault();
            return (T)(object)prescricao;
        }

        /// <summary>
        /// Realiza a atualização dos itens aprazados para determinada prescrição
        /// </summary>
        /// <typeparam name="T">Prescrição</typeparam>
        /// <param name="_prescricao"></param>
        /// <param name="verificar_medicamentos">verifica a atualização para os medicamentos da prescrição</param>
        /// <param name="verificar_procedimentos">verifica a atualização para os procedimentos da prescrição</param>
        /// <param name="verificar_procedimentosnaofaturaveis">verifica a atualização para os procedimentos não-faturáveis da prescrição</param>
        void IPrescricao.AtualizarStatusItensAprazadosPrescricao<T>(T _prescricao, bool verificar_medicamentos, bool verificar_procedimentos, bool verificar_procedimentosnaofaturaveis)
        {
            using (Session.BeginTransaction())
            {
                double horas;
                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
                IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();

                try
                {
                    if (_prescricao != null)
                    {
                        Prescricao prescricao = (Prescricao)(object)_prescricao;

                        //Teste de bloqueio e desbloqueio para medicamento
                        if (verificar_medicamentos)
                        {
                            IList<AprazamentoMedicamento> aprazamentomedicamentos = iAprazamento.BuscarAprazamentoMedicamento<AprazamentoMedicamento>(prescricao.Codigo, Convert.ToChar(AprazamentoMedicamento.StatusItem.Bloqueado));
                            foreach (AprazamentoMedicamento aprazamento in aprazamentomedicamentos)
                            //{
                                aprazamento.Status = Convert.ToChar(AprazamentoMedicamento.StatusItem.Ativo);
                                //Session.SaveOrUpdateCopy(aprazamento);
                            //}

                            aprazamentomedicamentos = aprazamentomedicamentos.Concat(iAprazamento.BuscarAprazamentoMedicamento<AprazamentoMedicamento>(prescricao.Codigo, Convert.ToChar(AprazamentoMedicamento.StatusItem.Ativo))).ToList();

                            if (aprazamentomedicamentos.Count() > 0)
                            {
                                IList<int> lcodigomedicamentos = aprazamentomedicamentos.GroupBy(pt => pt.CodigoMedicamento).Select(pt => pt.Key).ToList();

                                foreach (int co_medicamento in lcodigomedicamentos)
                                {
                                    AprazamentoMedicamento ap = aprazamentomedicamentos.Where(pt => pt.CodigoMedicamento == co_medicamento).OrderBy(pt => pt.Horario).First();

                                    horas = DateTime.Now.Subtract(ap.Horario).TotalHours;

                                    if (horas >= 2) //Bloquear todos os aprazamentos deste medicamento
                                    {
                                        IList<AprazamentoMedicamento> ltemp = aprazamentomedicamentos.Where(pt => pt.CodigoMedicamento == co_medicamento).ToList();
                                        foreach (AprazamentoMedicamento temp in ltemp)
                                        {
                                            temp.Status = Convert.ToChar(AprazamentoMedicamento.StatusItem.Bloqueado);
                                            Session.SaveOrUpdateCopy(temp);
                                        }
                                    }
                                    else 
                                    {
                                        IList<AprazamentoMedicamento> ltemp = aprazamentomedicamentos.Where(pt => pt.CodigoMedicamento == co_medicamento && pt.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Ativo)).ToList();
                                        foreach (AprazamentoMedicamento temp in ltemp)
                                            Session.SaveOrUpdateCopy(temp);
                                    }
                                }
                            }
                        }

                        //Teste de bloqueio e desbloqueio para procedimento não faturável
                        if (verificar_procedimentosnaofaturaveis)
                        {
                            IList<AprazamentoProcedimentoNaoFaturavel> aprazamentosnaofaturaveis = iAprazamento.BuscarAprazamentoProcedimentoNaoFaturavel<AprazamentoProcedimentoNaoFaturavel>(prescricao.Codigo, Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Bloqueado));
                            foreach (AprazamentoProcedimentoNaoFaturavel aprazamento in aprazamentosnaofaturaveis)
                            //{
                                aprazamento.Status = Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Ativo);
                                //Session.SaveOrUpdateCopy(aprazamento);
                            //}

                            aprazamentosnaofaturaveis = aprazamentosnaofaturaveis.Concat(iAprazamento.BuscarAprazamentoProcedimentoNaoFaturavel<AprazamentoProcedimentoNaoFaturavel>(prescricao.Codigo, Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Ativo))).ToList();

                            if (aprazamentosnaofaturaveis.Count() > 0)
                            {
                                IList<int> lcodigoaprazadosnaofaturaveis = aprazamentosnaofaturaveis.GroupBy(pt => pt.ProcedimentoNaoFaturavel.Codigo).Select(pt => pt.Key).ToList();

                                foreach (int co_procedimento in lcodigoaprazadosnaofaturaveis)
                                {
                                    AprazamentoProcedimentoNaoFaturavel ap = aprazamentosnaofaturaveis.Where(pt => pt.ProcedimentoNaoFaturavel.Codigo == co_procedimento).OrderBy(pt => pt.Horario).First();

                                    horas = DateTime.Now.Subtract(ap.Horario).TotalHours;
                                    if (horas >= 2) //Bloquear todos os aprazamentos deste medicamento
                                    //if ((DateTime.Now - ap.Horario).Hours >= 2) //Bloquear todos os aprazamentos deste medicamento
                                    {
                                        IList<AprazamentoProcedimentoNaoFaturavel> ltemp = aprazamentosnaofaturaveis.Where(pt => pt.ProcedimentoNaoFaturavel.Codigo == co_procedimento).ToList();
                                        foreach (AprazamentoProcedimentoNaoFaturavel temp in ltemp)
                                        {
                                            temp.Status = Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Bloqueado);
                                            Session.SaveOrUpdateCopy(temp);
                                        }
                                    }
                                    else
                                    {
                                        IList<AprazamentoProcedimentoNaoFaturavel> ltemp = aprazamentosnaofaturaveis.Where(pt => pt.ProcedimentoNaoFaturavel.Codigo == co_procedimento && pt.Status == Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Ativo)).ToList();
                                        foreach (AprazamentoProcedimentoNaoFaturavel temp in ltemp)
                                            Session.SaveOrUpdateCopy(temp);
                                    }
                                }
                            }
                        }

                        //Teste de bloqueio e desbloqueio para procedimento(SIGTAP)
                        if (verificar_procedimentos)
                        {
                            IList<AprazamentoProcedimento> aprazamentoprocedimentos = iAprazamento.BuscarAprazamentoProcedimento<AprazamentoProcedimento>(prescricao.Codigo, Convert.ToChar(AprazamentoProcedimento.StatusItem.Bloqueado));
                            foreach (AprazamentoProcedimento aprazamento in aprazamentoprocedimentos)
                            //{
                                aprazamento.Status = Convert.ToChar(AprazamentoProcedimento.StatusItem.Ativo);
                                //Session.SaveOrUpdateCopy(aprazamento);
                            //}

                            aprazamentoprocedimentos = aprazamentoprocedimentos.Concat(iAprazamento.BuscarAprazamentoProcedimento<AprazamentoProcedimento>(prescricao.Codigo, Convert.ToChar(AprazamentoProcedimento.StatusItem.Ativo))).ToList();

                            if (aprazamentoprocedimentos.Count() > 0)
                            {
                                IList<string> lcodigoaprazadosprocedimentos = aprazamentoprocedimentos.GroupBy(pt => pt.CodigoProcedimento).Select(pt => pt.Key).ToList();

                                foreach (string co_procedimento in lcodigoaprazadosprocedimentos)
                                {
                                    AprazamentoProcedimento ap = aprazamentoprocedimentos.Where(pt => pt.CodigoProcedimento == co_procedimento).OrderBy(pt => pt.Horario).First();

                                    horas = DateTime.Now.Subtract(ap.Horario).TotalHours;
                                    if (horas >= 2)
                                    //if ((DateTime.Now - ap.Horario).Hours >= 2) //Bloquear todos os aprazamentos deste medicamento
                                    {
                                        IList<AprazamentoProcedimento> ltemp = aprazamentoprocedimentos.Where(pt => pt.CodigoProcedimento == co_procedimento).ToList();
                                        foreach (AprazamentoProcedimento temp in ltemp)
                                        {
                                            temp.Status = Convert.ToChar(AprazamentoProcedimento.StatusItem.Bloqueado);
                                            Session.SaveOrUpdateCopy(temp);
                                        }
                                    }
                                    else
                                    {
                                        IList<AprazamentoProcedimento> ltemp = aprazamentoprocedimentos.Where(pt => pt.CodigoProcedimento == co_procedimento && pt.Status == Convert.ToChar(AprazamentoProcedimento.StatusItem.Ativo)).ToList();
                                        foreach (AprazamentoProcedimento temp in ltemp)
                                            Session.SaveOrUpdateCopy(temp);
                                    }
                                }
                            }
                        }

                        Session.Transaction.Commit();
                    }
                }
                catch (Exception f)
                {
                    Session.Transaction.Rollback();
                    throw f;
                }
            }
        }

        IList<T> IPrescricao.ListarProcedimentosFPOUnidade<T>(bool faturavel, string co_unidade, int competencia)
        {
            string hql = string.Empty;
            IList<T> procedimentos;

            if (!faturavel)
            {
                hql = "FROM ViverMais.Model.PrescricaoProcedimento AS pp WHERE pp.Prescricao.Prontuario.CodigoUnidade = '" + co_unidade + "'";
                procedimentos = Session.CreateQuery(hql).List<T>();
                procedimentos = (IList<T>)procedimentos.Cast<PrescricaoProcedimento>().Where(p => int.Parse((p.Data.Year.ToString() + (p.Data.Month < 10 ? "0" + p.Data.Month.ToString() : p.Data.Month.ToString()))) == competencia).ToList();
            }
            else
            {
                hql = "FROM ViverMais.Model.PrescricaoProcedimentoNaoFaturavel AS pp WHERE pp.Prescricao.Prontuario.CodigoUnidade = '" + co_unidade + "'";
                procedimentos = Session.CreateQuery(hql).List<T>();
                procedimentos = (IList<T>)procedimentos.Cast<PrescricaoProcedimentoNaoFaturavel>().Where(p => int.Parse((p.Data.Year.ToString() + (p.Data.Month < 10 ? "0" + p.Data.Month.ToString() : p.Data.Month.ToString()))) == competencia).ToList();
            }


            //lp = lp.Where(p => int.Parse((p.Data.Year.ToString() + (p.Data.Month < 10 ? "0" + p.Data.Month.ToString() : p.Data.Month.ToString()))) == competencia).ToList();

            return procedimentos;
        }

        void IPrescricao.ExcluirPrescricao<T>(T prescricao)
        {
            using (Session.BeginTransaction())
            {
                try
                {
                    Prescricao prescricaoagendada = (Prescricao)(object)prescricao;

                    ControlePrescricaoUrgence controle = Factory.GetInstance<IControlePrescricaoUrgence>().BuscarControlePorPrescricao<ControlePrescricaoUrgence>(prescricaoagendada.Codigo);
                    Session.Delete(controle);

                    IList<PrescricaoMedicamento> medicamentos = Factory.GetInstance<IPrescricao>().BuscarMedicamentos<PrescricaoMedicamento>(prescricaoagendada.Codigo);
                    foreach (PrescricaoMedicamento medicamento in medicamentos)
                        Session.Delete(medicamento);

                    IList<PrescricaoProcedimento> procedimentos = Factory.GetInstance<IPrescricao>().BuscarProcedimentos<PrescricaoProcedimento>(prescricaoagendada.Codigo);
                    foreach (PrescricaoProcedimento procedimento in procedimentos)
                        Session.Delete(procedimento);

                    IList<PrescricaoProcedimentoNaoFaturavel> procedimentosnaofaturaveis = Factory.GetInstance<IPrescricao>().BuscarProcedimentosNaoFaturaveis<PrescricaoProcedimentoNaoFaturavel>(prescricaoagendada.Codigo);
                    foreach (PrescricaoProcedimentoNaoFaturavel procedimento in procedimentosnaofaturaveis)
                        Session.Delete(procedimento);

                    Session.Delete(prescricaoagendada);

                    Session.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    Session.Transaction.Rollback();
                    throw ex;
                }
            }
        }

        IList<T> IPrescricao.BuscarCidsProcedimentoPorCodigo<T>(string co_procedimento, string codigocid)
        {
            ICid iCid = Factory.GetInstance<ICid>();

            IList<Cid> cids = iCid.BuscarPorCodigo<Cid>(codigocid, co_procedimento);

            if (cids.Count() > 0)
                return (IList<T>)(object)cids;

            Cid cid = iCid.BuscarPorCodigo<Cid>(codigocid);
            if (cid != null)
                cids.Add(cid);

            return (IList<T>)(object)cids;
        }

        IList<T> IPrescricao.BuscarCidsProcedimentoPorGrupo<T>(string co_procedimento, string grupocid)
        {
            ICid iCid = Factory.GetInstance<ICid>();

            IList<Cid> cids = iCid.BuscarPorGrupo<Cid>(grupocid, co_procedimento);

            if (cids.Count() > 0)
                return (IList<T>)(object)cids;

            return (IList<T>)(object)iCid.BuscarPorGrupo<Cid>(grupocid);
        }

        bool IPrescricao.VerificaPossibilidadeExcluirItemPrescricao(long co_prescricao)
        {
            object resultado = null;
            int qtdtotal = 0;
            string hql = string.Empty;

            hql = "SELECT COUNT(*) FROM ViverMais.Model.PrescricaoMedicamento pm WHERE pm.Prescricao.Codigo = " + co_prescricao;
            resultado = Session.CreateQuery(hql).UniqueResult();
            if (resultado != null)
                qtdtotal += int.Parse(resultado.ToString());

            hql = "SELECT COUNT(*) FROM ViverMais.Model.PrescricaoProcedimento AS pp WHERE pp.Prescricao.Codigo = " + co_prescricao;
            resultado = Session.CreateQuery(hql).UniqueResult();
            if (resultado != null)
                qtdtotal += int.Parse(resultado.ToString());

            hql = "SELECT COUNT(*) FROM ViverMais.Model.PrescricaoProcedimentoNaoFaturavel AS ppnf WHERE ppnf.Prescricao.Codigo = " + co_prescricao;
            resultado = Session.CreateQuery(hql).UniqueResult();
            if (resultado != null)
                qtdtotal += int.Parse(resultado.ToString());

            return (qtdtotal - 1 > 0) ? true : false;
        }

        bool IPrescricao.VerificaPossibilidadeExcluirProcedimentoPrescricao(long co_prescricao, string co_procedimento)
        {
            string hql = string.Empty;
            hql = "SELECT COUNT(*) FROM AprazamentoProcedimento AS am WHERE am.Prescricao.Codigo = " + co_prescricao;
            hql += " AND am.CodigoProcedimento = '" + co_procedimento + "'";
            hql += " AND am.Status = '" + Convert.ToChar(AprazamentoProcedimento.StatusItem.Finalizado) + "'";

            int qtd = 0;
            object resultado = Session.CreateQuery(hql).UniqueResult();
            if (resultado != null)
                qtd += int.Parse(resultado.ToString());

            return qtd > 0 ? false : true;
        }

        void IPrescricao.ExcluirProcedimentoPrescricao<T>(T _prescricaoprocedimento)
        {
            using (Session.BeginTransaction())
            {
                try
                {
                    PrescricaoProcedimento prescricaoprocedimento = (PrescricaoProcedimento)(object)_prescricaoprocedimento;

                    string hql = string.Empty;
                    hql = "FROM AprazamentoProcedimento AS am WHERE am.Prescricao.Codigo = " + prescricaoprocedimento.Prescricao.Codigo;
                    hql += " AND am.CodigoProcedimento = '" + prescricaoprocedimento.Procedimento.Codigo + "'";

                    IList<AprazamentoProcedimento> aprazamentos = (IList<AprazamentoProcedimento>)(object)Session.CreateQuery(hql).List<AprazamentoProcedimento>();

                    foreach (AprazamentoProcedimento aprazamento in aprazamentos)
                        Session.Delete(aprazamento);

                    Session.Delete(prescricaoprocedimento);
                    Session.Transaction.Commit();
                }
                catch (Exception f)
                {
                    Session.Transaction.Rollback();
                    throw f;
                }
            }
        }

        bool IPrescricao.VerificaPossibilidadeExcluirProcedimentoNaoFaturavelPrescricao(long co_prescricao, int co_procedimento)
        {
            string hql = string.Empty;
            hql = "SELECT COUNT(*) FROM AprazamentoProcedimentoNaoFaturavel AS ap WHERE ap.Prescricao.Codigo = " + co_prescricao;
            hql += " AND ap.ProcedimentoNaoFaturavel.Codigo = " + co_procedimento;
            hql += " AND ap.Status = '" + Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Finalizado) + "'";

            int qtd = 0;
            object resultado = Session.CreateQuery(hql).UniqueResult();
            if (resultado != null)
                qtd += int.Parse(resultado.ToString());

            return qtd > 0 ? false : true;
        }

        void IPrescricao.ExcluirProcedimentoNaoFaturavelPrescricao<T>(T _prescricaoprocedimento)
        {
            using (Session.BeginTransaction())
            {
                PrescricaoProcedimentoNaoFaturavel prescricaoprocedimento = (PrescricaoProcedimentoNaoFaturavel)(object)_prescricaoprocedimento;
                try
                {
                    string hql = string.Empty;
                    hql = "FROM AprazamentoProcedimentoNaoFaturavel AS ap WHERE ap.Prescricao.Codigo = " + prescricaoprocedimento.Prescricao.Codigo;
                    hql += " AND ap.ProcedimentoNaoFaturavel.Codigo = " + prescricaoprocedimento.Procedimento.Codigo;
                    IList<AprazamentoProcedimentoNaoFaturavel> aprazamentos = (IList<AprazamentoProcedimentoNaoFaturavel>)(object)Session.CreateQuery(hql).List<AprazamentoProcedimentoNaoFaturavel>();

                    foreach (AprazamentoProcedimentoNaoFaturavel aprazamento in aprazamentos)
                        Session.Delete(aprazamento);

                    Session.Delete(prescricaoprocedimento);
                    Session.Transaction.Commit();
                }
                catch (Exception f)
                {
                    Session.Transaction.Rollback();
                    throw f;
                }
            }
        }

        bool IPrescricao.VerificaPossibilidadeExcluirMedicamentoPrescricao(long co_prescricao, int co_medicamento)
        {
            string hql = string.Empty;
            hql = "SELECT COUNT(*) FROM AprazamentoMedicamento AS am WHERE am.Prescricao.Codigo = " + co_prescricao;
            hql += " AND am.CodigoMedicamento = " + co_medicamento.ToString();
            hql += " AND (am.Status = '" + Convert.ToChar(AprazamentoMedicamento.StatusItem.Finalizado)
                + "' OR am.Status = '" + Convert.ToChar(AprazamentoMedicamento.StatusItem.Recusado) + "')";

            int qtd = 0;
            object resultado = Session.CreateQuery(hql).UniqueResult();
            if (resultado != null)
                qtd += int.Parse(resultado.ToString());

            return qtd > 0 ? false : true;
        }

        void IPrescricao.ExcluirMedicamentoPrescricao<T>(T _prescricaomedicamento)
        {
            using (Session.BeginTransaction())
            {
                PrescricaoMedicamento prescricaomedicamento = (PrescricaoMedicamento)(object)_prescricaomedicamento;
                try
                {
                    string hql = string.Empty;
                    hql = "FROM AprazamentoMedicamento AS am WHERE am.Prescricao.Codigo = " + prescricaomedicamento.Prescricao.Codigo;
                    hql += " AND am.CodigoMedicamento = " + prescricaomedicamento.ObjetoMedicamento.Codigo;
                    IList<AprazamentoMedicamento> aprazamentos = (IList<AprazamentoMedicamento>)(object)Session.CreateQuery(hql).List<AprazamentoMedicamento>();

                    foreach (AprazamentoMedicamento aprazamento in aprazamentos)
                        Session.Delete(aprazamento);

                    Session.Delete(prescricaomedicamento);
                    Session.Transaction.Commit();
                }
                catch (Exception f)
                {
                    Session.Transaction.Rollback();
                    throw f;
                }
            }
        }

        #endregion

        public PrescricaoDAO()
        {
        }
    }
}
