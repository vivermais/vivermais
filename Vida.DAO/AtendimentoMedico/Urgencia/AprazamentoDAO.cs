using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.BLL;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using System.Collections;
using System.Data;

namespace ViverMais.DAO.AtendimentoMedico.Urgencia
{
    public class AprazamentoDAO : UrgenciaServiceFacadeDAO, IAprazamento
    {
        #region

        T IAprazamento.BuscarAprazamentoMedicamento<T>(long co_prescricao, int co_medicamento, DateTime horario)
        {
            string hql = string.Empty;
            hql = "from ViverMais.Model.AprazamentoMedicamento am where am.CodigoMedicamento  = " + co_medicamento;
            hql = hql + " and am.Prescricao.Codigo  = " + co_prescricao;
            hql = hql + " and TO_CHAR(am.Horario,'DD/MM/YYYY HH24:MI')  = '" + horario.ToString("dd/MM/yyyy HH:mm") + "'";

            IMedicamento iMedicamento = Factory.GetInstance<IMedicamento>();
            IProfissional iProfissional = Factory.GetInstance<IProfissional>();

            AprazamentoMedicamento aprazamento = (AprazamentoMedicamento)(object)Session.CreateQuery(hql).UniqueResult<T>();

            if (aprazamento != null)
            {
                aprazamento.Medicamento = iMedicamento.BuscarPorCodigo<Medicamento>(aprazamento.CodigoMedicamento);
                aprazamento.Profissional = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissional);

                if (!string.IsNullOrEmpty(aprazamento.CodigoProfissionalConfirmacao))
                    aprazamento.ProfissionalConfirmacao = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissionalConfirmacao);
            }

            return (T)(object)aprazamento;
        }

        T IAprazamento.BuscarAprazamentoProcedimento<T>(long co_prescricao, string co_procedimento, DateTime horario)
        {
            string hql = string.Empty;
            hql = "from ViverMais.Model.AprazamentoProcedimento ap where ap.CodigoProcedimento  = '" + co_procedimento + "'";
            hql = hql + " and ap.Prescricao.Codigo  = " + co_prescricao;
            hql = hql + " and to_char(ap.Horario,'DD/MM/YYYY HH24:MI')  = '" + horario.ToString("dd/MM/yyyy HH:mm") + "'";

            AprazamentoProcedimento aprazamento = (AprazamentoProcedimento)(object)Session.CreateQuery(hql).UniqueResult<T>();
            IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();
            IProfissional iProfissional = Factory.GetInstance<IProfissional>();

            if (aprazamento != null)
            {
                aprazamento.Procedimento = iProcedimento.BuscarPorCodigo<ViverMais.Model.Procedimento>(aprazamento.CodigoProcedimento);
                aprazamento.Profissional = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissional);

                if (!string.IsNullOrEmpty(aprazamento.CodigoProfissionalConfirmacao))
                    aprazamento.ProfissionalConfirmacao = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissionalConfirmacao);
            }

            return (T)(object)aprazamento;
        }

        T IAprazamento.BuscarAprazamentoProcedimentoNaoFaturavel<T>(long co_prescricao, int co_procedimentonaofaturavel, DateTime horario)
        {
            string hql = string.Empty;
            hql = "from ViverMais.Model.AprazamentoProcedimentoNaoFaturavel ap where ap.ProcedimentoNaoFaturavel.Codigo  = " + co_procedimentonaofaturavel;
            hql = hql + " and ap.Prescricao.Codigo  = " + co_prescricao;
            hql = hql + " and to_char(ap.Horario,'DD/MM/YYYY HH24:MI')  = '" + horario.ToString("dd/MM/yyyy HH:mm") + "'";
            AprazamentoProcedimentoNaoFaturavel aprazamento = (AprazamentoProcedimentoNaoFaturavel)(object)Session.CreateQuery(hql).UniqueResult<T>();

            IProfissional iProfissional = Factory.GetInstance<IProfissional>();

            if (aprazamento != null)
            {
                aprazamento.Profissional = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissional);

                if (!string.IsNullOrEmpty(aprazamento.CodigoProfissionalConfirmacao))
                    aprazamento.ProfissionalConfirmacao = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissionalConfirmacao);
            }

            return (T)(object)aprazamento;
        }

        IList<T> IAprazamento.BuscarAprazamentoMedicamento<T>(long co_prescricao)
        {
            string hql = string.Empty;
            hql = "FROM AprazamentoMedicamento AS am WHERE am.Prescricao.Codigo = " + co_prescricao + " ORDER BY am.Horario";
            IList<AprazamentoMedicamento> aprazamentos = (IList<AprazamentoMedicamento>)(object)Session.CreateQuery(hql).List<T>();
            IMedicamento iMedicamento = Factory.GetInstance<IMedicamento>();
            IProfissional iProfissional = Factory.GetInstance<IProfissional>();

            foreach (AprazamentoMedicamento aprazamento in aprazamentos)
            {
                aprazamento.Medicamento = iMedicamento.BuscarPorCodigo<Medicamento>(aprazamento.CodigoMedicamento);
                aprazamento.Profissional = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissional);

                if (!string.IsNullOrEmpty(aprazamento.CodigoProfissionalConfirmacao))
                    aprazamento.ProfissionalConfirmacao = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissionalConfirmacao);
            }

            return (IList<T>)(object)aprazamentos;
        }

        IList<T> IAprazamento.BuscarAprazamentoProcedimentoNaoFaturavel<T>(long co_prescricao)
        {
            string hql = string.Empty;
            hql = "FROM AprazamentoProcedimentoNaoFaturavel AS am WHERE am.Prescricao.Codigo = " + co_prescricao + " ORDER BY am.Horario";
            IList<AprazamentoProcedimentoNaoFaturavel> aprazamentos = (IList<AprazamentoProcedimentoNaoFaturavel>)(object)Session.CreateQuery(hql).List<T>();
            IProfissional iProfissional = Factory.GetInstance<IProfissional>();

            foreach (AprazamentoProcedimentoNaoFaturavel aprazamento in aprazamentos)
            {
                aprazamento.Profissional = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissional);

                if (!string.IsNullOrEmpty(aprazamento.CodigoProfissionalConfirmacao))
                    aprazamento.ProfissionalConfirmacao = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissionalConfirmacao);
            }

            return (IList<T>)(object)aprazamentos;
        }

        IList<T> IAprazamento.BuscarAprazamentoProcedimento<T>(long co_prescricao)
        {
            string hql = string.Empty;
            hql = "FROM AprazamentoProcedimento AS am WHERE am.Prescricao.Codigo = " + co_prescricao + " ORDER BY am.Horario";
            IList<AprazamentoProcedimento> aprazamentos = (IList<AprazamentoProcedimento>)(object)Session.CreateQuery(hql).List<T>();
            IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();
            IProfissional iProfissional = Factory.GetInstance<IProfissional>();

            foreach (AprazamentoProcedimento aprazamento in aprazamentos)
            {
                aprazamento.Procedimento = iProcedimento.BuscarPorCodigo<ViverMais.Model.Procedimento>(aprazamento.CodigoProcedimento);
                aprazamento.Profissional = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissional);

                if (!string.IsNullOrEmpty(aprazamento.CodigoProfissionalConfirmacao))
                    aprazamento.ProfissionalConfirmacao = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissionalConfirmacao);
            }

            return (IList<T>)(object)aprazamentos;
        }

        IList<T> IAprazamento.BuscarAprazamentosBPAI<T>(string co_unidade, int competencia, DateTime datainicio, DateTime datalimite)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.AprazamentoProcedimento ap WHERE ap.Prescricao.Prontuario.CodigoUnidade='" + co_unidade + "'";
            hql += " AND ap.Status = '" + Convert.ToChar(AprazamentoProcedimento.StatusItem.Finalizado) + "'";
            hql += " AND TO_CHAR(ap.HorarioConfirmacao,'YYYYMM') = '" + competencia + "'";
            hql += " AND ap.Prescricao.Prontuario.Paciente.CodigoViverMais IS NOT NULL";
            hql += " AND LTRIM(RTRIM(ap.Prescricao.Prontuario.Paciente.CodigoViverMais)) <> ' '";
            hql += " AND ap.CodigoCid IS NOT NULL AND LTRIM(RTRIM(ap.CodigoCid)) <> ' '";
            hql += " AND ap.HorarioConfirmacao BETWEEN TO_DATE('" + datainicio.ToString("dd/MM/yyyy HH:mm") + "','DD/MM/YYYY HH24:MI')";
            hql += " AND TO_DATE('" + datalimite.ToString("dd/MM/yyyy HH:mm") + "','DD/MM/YYYY HH24:MI')";

            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IAprazamento.BuscarAprazamentosBPAC<T>(string co_unidade, int competencia, DateTime datainicio, DateTime datalimite)
        {
            IProntuario iProntuario = Factory.GetInstance<IProntuario>();

            string hql = string.Empty;
            hql = "FROM ViverMais.Model.AprazamentoProcedimento ap WHERE ap.Prescricao.Prontuario.CodigoUnidade='" + co_unidade + "'";
            hql += " AND ap.Status = '" + Convert.ToChar(AprazamentoProcedimento.StatusItem.Finalizado) + "'";
            hql += " AND TO_CHAR(ap.HorarioConfirmacao,'YYYYMM') = '" + competencia + "'";
            hql += " AND ap.HorarioConfirmacao BETWEEN TO_DATE('" + datainicio.ToString("dd/MM/yyyy HH:mm") + "','DD/MM/YYYY HH24:MI')";
            hql += " AND TO_DATE('" + datalimite.ToString("dd/MM/yyyy HH:mm") + "','DD/MM/YYYY HH24:MI')";

            IList<AprazamentoProcedimento> aprazamentos = (IList<AprazamentoProcedimento>)(object)Session.CreateQuery(hql).List<T>();

            foreach (AprazamentoProcedimento aprazamento in aprazamentos)
            {
                string co_usuario = aprazamento.Prescricao.Prontuario.Paciente.CodigoViverMais;
                if (!string.IsNullOrEmpty(co_usuario))
                    aprazamento.Prescricao.Prontuario.Idade = PacienteBLL.Pesquisar(co_usuario).Idade;
                        //iProntuario.CalculaIdade(DateTime.Now, PacienteBLL.Pesquisar(co_usuario).DataNascimento);
            }

            return (IList<T>)(object)aprazamentos;
        }

        bool IAprazamento.AprazamentoMedicamentoAnteriorNaoExecutado(long co_prescricao, int co_medicamento, DateTime horario)
        {
            string hql = string.Empty;
            hql = "SELECT COUNT (a.Codigo) FROM ViverMais.Model.AprazamentoMedicamento a";
            hql += " WHERE a.CodigoMedicamento=" + co_medicamento + " AND";
            hql += " a.Prescricao.Codigo=" + co_prescricao + " AND";
            hql += " (a.Status='" + Convert.ToChar(AprazamentoMedicamento.StatusItem.Bloqueado) + "' OR a.Status='" + Convert.ToChar(AprazamentoMedicamento.StatusItem.Ativo) + "')";
            hql += " AND a.Horario < TO_DATE('" + horario.ToString("dd/MM/yyyy HH:mm") + "','DD/MM/YYYY HH24:MI')";

            object o = Session.CreateQuery(hql).UniqueResult();

            if (o != null && int.Parse(o.ToString()) > 0)
                return true;

            return false;
        }

        bool IAprazamento.AprazamentoProcedimentoAnteriorNaoExecutado(long co_prescricao, string co_procedimento, DateTime horario)
        {
            string hql = string.Empty;
            hql = "SELECT COUNT (a.Codigo) FROM ViverMais.Model.AprazamentoProcedimento a";
            hql += " WHERE a.CodigoProcedimento='" + co_procedimento + "' AND";
            hql += " a.Prescricao.Codigo=" + co_prescricao + " AND";
            hql += " (a.Status='" + Convert.ToChar(AprazamentoProcedimento.StatusItem.Bloqueado) + "' OR a.Status='" + Convert.ToChar(AprazamentoProcedimento.StatusItem.Ativo) + "')";
            hql += " AND a.Horario < TO_DATE('" + horario.ToString("dd/MM/yyyy HH:mm") + "','DD/MM/YYYY HH24:MI')";

            object o = Session.CreateQuery(hql).UniqueResult();

            if (o != null && int.Parse(o.ToString()) > 0)
                return true;

            return false;
        }

        bool IAprazamento.AprazamentoProcedimentoNaoFaturavelAnteriorNaoExecutado(long co_prescricao, int co_procedimento, DateTime horario)
        {
            string hql = string.Empty;
            hql = "SELECT COUNT (a.Codigo) FROM ViverMais.Model.AprazamentoProcedimentoNaoFaturavel a";
            hql += " WHERE a.ProcedimentoNaoFaturavel.Codigo=" + co_procedimento + " AND";
            hql += " a.Prescricao.Codigo=" + co_prescricao + " AND";
            hql += " (a.Status='" + Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Bloqueado) + "' OR a.Status='" + Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Ativo) + "')";
            hql += " AND a.Horario < TO_DATE('" + horario.ToString("dd/MM/yyyy HH:mm") + "','DD/MM/YYYY HH24:MI')";

            object o = Session.CreateQuery(hql).UniqueResult();

            if (o != null && int.Parse(o.ToString()) > 0)
                return true;

            return false;
        }

        void IAprazamento.AprazarMedicamento<T>(T _aprazamento, bool aprazarautomatico, int co_usuario)
        {
            AprazamentoMedicamento aprazamento = (AprazamentoMedicamento)(object)_aprazamento;
            IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();
            IList<AprazamentoMedicamento> aprazamentos = iAprazamento.BuscarAprazamentoMedicamento<AprazamentoMedicamento>(aprazamento.Prescricao.Codigo, aprazamento.CodigoMedicamento);

            using (Session.BeginTransaction())
            {
                try
                {
                    Session.Save(aprazamento);
                    Session.Save(new LogUrgencia(DateTime.Now, co_usuario, 22, "id_prescricao=" + aprazamento.Prescricao.Codigo + " id_medicamento = " + aprazamento.Medicamento.Codigo + " id profissional: " + aprazamento.CodigoProfissional));

                    if (aprazarautomatico)
                    {
                        PrescricaoMedicamento prescricao = iPrescricao.BuscarMedicamento<PrescricaoMedicamento>(aprazamento.Prescricao.Codigo, aprazamento.CodigoMedicamento);
                        int intervalo = prescricao.Intervalo;

                        DateTime proximadata = aprazamento.Horario;
                        proximadata = proximadata.AddMinutes(intervalo);

                        while (proximadata.CompareTo(aprazamento.Prescricao.UltimaDataValida) <= 0)
                        {
                            AprazamentoMedicamento novoaprazamento = new AprazamentoMedicamento();

                            novoaprazamento.CodigoMedicamento = aprazamento.CodigoMedicamento;
                            novoaprazamento.Horario = proximadata;
                            novoaprazamento.Status = Convert.ToChar(AprazamentoMedicamento.StatusItem.Ativo);
                            novoaprazamento.CodigoProfissional = aprazamento.CodigoProfissional;
                            novoaprazamento.CBOProfissional = aprazamento.CBOProfissional;
                            novoaprazamento.HorarioValidoPrescricao = aprazamento.Prescricao.UltimaDataValida;
                            novoaprazamento.Prescricao = aprazamento.Prescricao;

                            if (aprazamentos.Where(p=>p.Horario.CompareTo(novoaprazamento.Horario) == 0).FirstOrDefault() == null)
                            {
                                Session.Save(novoaprazamento);
                                Session.Save(new LogUrgencia(DateTime.Now, co_usuario, 22, "id_prescricao=" + novoaprazamento.Prescricao.Codigo + " id_medicamento = " + novoaprazamento.CodigoMedicamento + " id profissional: " + novoaprazamento.CodigoProfissional));
                            }

                            proximadata = proximadata.AddMinutes(intervalo);
                        }
                    }

                    Session.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    Session.Transaction.Rollback();
                    throw ex;
                }
            }
        }

        void IAprazamento.AprazarProcedimento<T>(T _aprazamento, bool aprazarautomatico, int co_usuario)
        {
            AprazamentoProcedimento aprazamento = (AprazamentoProcedimento)(object)_aprazamento;
            IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();

            IList<AprazamentoProcedimento> aprazamentos = iAprazamento.BuscarAprazamentoProcedimento<AprazamentoProcedimento>(aprazamento.Prescricao.Codigo, aprazamento.Procedimento.Codigo);

            using (Session.BeginTransaction())
            {
                try
                {
                    Session.Save(aprazamento);
                    Session.Save(new LogUrgencia(DateTime.Now, co_usuario, 33, "id_prescricao=" + aprazamento.Prescricao.Codigo + " co_procedimento = " + aprazamento.Procedimento.Codigo + " id profissional: " + aprazamento.CodigoProfissional));

                    if (aprazarautomatico)
                    {
                        PrescricaoProcedimento prescricao = iPrescricao.BuscarProcedimento<PrescricaoProcedimento>(aprazamento.Prescricao.Codigo, aprazamento.CodigoProcedimento);
                        int intervalo = prescricao.Intervalo;

                        DateTime proximadata = aprazamento.Horario;
                        proximadata = proximadata.AddMinutes(intervalo);

                        while (proximadata.CompareTo(aprazamento.Prescricao.UltimaDataValida) <= 0)
                        {
                            AprazamentoProcedimento novoaprazamento = new AprazamentoProcedimento();

                            novoaprazamento.CodigoProcedimento = aprazamento.CodigoProcedimento;
                            novoaprazamento.Horario = proximadata;
                            novoaprazamento.Status = Convert.ToChar(AprazamentoProcedimento.StatusItem.Ativo);
                            novoaprazamento.CodigoProfissional = aprazamento.CodigoProfissional;
                            novoaprazamento.CBOProfissional = aprazamento.CBOProfissional;
                            novoaprazamento.HorarioValidoPrescricao = aprazamento.Prescricao.UltimaDataValida;
                            novoaprazamento.Prescricao = aprazamento.Prescricao;
                            novoaprazamento.CodigoCid = prescricao.CodigoCid;

                            if (aprazamentos.Where(p =>p.Horario.CompareTo(novoaprazamento.Horario) == 0).FirstOrDefault() == null)
                            {
                                Session.Save(novoaprazamento);
                                Session.Save(new LogUrgencia(DateTime.Now, co_usuario, 33, "id_prescricao=" + novoaprazamento.Prescricao.Codigo + " co_procedimento = " + novoaprazamento.CodigoProcedimento + " id profissional: " + novoaprazamento.CodigoProfissional));
                            }

                            proximadata = proximadata.AddMinutes(intervalo);
                        }
                    }

                    Session.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    Session.Transaction.Rollback();
                    throw ex;
                }
            }
        }

        void IAprazamento.AprazarProcedimentoNaoFaturavel<T>(T _aprazamento, bool aprazarautomatico, int co_usuario)
        {
            AprazamentoProcedimentoNaoFaturavel aprazamento = (AprazamentoProcedimentoNaoFaturavel)(object)_aprazamento;
            IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();

            IList<AprazamentoProcedimentoNaoFaturavel> aprazamentos = iAprazamento.BuscarAprazamentoProcedimentoNaoFaturavel<AprazamentoProcedimentoNaoFaturavel>(aprazamento.Prescricao.Codigo, aprazamento.ProcedimentoNaoFaturavel.Codigo);

            using (Session.BeginTransaction())
            {
                try
                {
                    Session.Save(aprazamento);
                    Session.Save(new LogUrgencia(DateTime.Now, co_usuario, 34, "id_prescricao=" + aprazamento.Prescricao.Codigo + " co_procedimento = " + aprazamento.ProcedimentoNaoFaturavel.Codigo + " id profissional: " + aprazamento.CodigoProfissional));

                    if (aprazarautomatico)
                    {
                        PrescricaoProcedimentoNaoFaturavel prescricao = iPrescricao.BuscarProcedimentoNaoFaturavel<PrescricaoProcedimentoNaoFaturavel>(aprazamento.Prescricao.Codigo, aprazamento.ProcedimentoNaoFaturavel.Codigo);
                        int intervalo = prescricao.Intervalo;

                        DateTime proximadata = aprazamento.Horario;
                        proximadata = proximadata.AddMinutes(intervalo);

                        while (proximadata.CompareTo(aprazamento.Prescricao.UltimaDataValida) <= 0)
                        {
                            AprazamentoProcedimentoNaoFaturavel novoaprazamento = new AprazamentoProcedimentoNaoFaturavel();

                            novoaprazamento.ProcedimentoNaoFaturavel = aprazamento.ProcedimentoNaoFaturavel;
                            novoaprazamento.Horario = proximadata;
                            novoaprazamento.Status = Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Ativo);
                            novoaprazamento.CodigoProfissional = aprazamento.CodigoProfissional;
                            novoaprazamento.CBOProfissional = aprazamento.CBOProfissional;
                            novoaprazamento.HorarioValidoPrescricao = aprazamento.Prescricao.UltimaDataValida;
                            novoaprazamento.Prescricao = aprazamento.Prescricao;

                            if (aprazamentos.Where(p => p.Horario.CompareTo(novoaprazamento.Horario) == 0).FirstOrDefault() == null)
                            {
                                Session.Save(novoaprazamento);
                                Session.Save(new LogUrgencia(DateTime.Now, co_usuario, 34, "id_prescricao=" + novoaprazamento.Prescricao.Codigo + " co_procedimento = " + novoaprazamento.ProcedimentoNaoFaturavel.Codigo + " id profissional: " + novoaprazamento.CodigoProfissional));
                            }

                            proximadata = proximadata.AddMinutes(intervalo);
                        }
                    }

                    Session.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    Session.Transaction.Rollback();
                    throw ex;
                }
            }
        }

        //Hashtable IAprazamento.RetornaTabelaAprazamento(long co_prescricao, DateTime data)
        //{
        //    IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
        //    Prescricao prescricao = iPrescricao.BuscarPorCodigo<Prescricao>(co_prescricao);
        //    IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();

        //    Hashtable hash = new Hashtable();

        //    DataTable corpo = new DataTable();
        //    corpo.Columns.Add("nome", typeof(string));
        //    corpo.Columns.Add("prescricao", typeof(string));

        //    for (int i = 0; i < 48; i++)
        //        corpo.Columns.Add(i.ToString(), typeof(string));

        //    IList<AprazamentoMedicamento> medicamentos = iAprazamento.BuscarAprazamentoMedicamento<AprazamentoMedicamento>(prescricao.Codigo, data);
        //    var _medicamentos = from _medicamento in medicamentos
        //                        select new
        //                        {
        //                            _Codigo = "MED_" + _medicamento.Medicamento.Codigo,
        //                            _Prescricao = _medicamento.Prescricao,
        //                            _Nome = _medicamento.Medicamento.Nome,
        //                            _Horario = _medicamento.Horario,
        //                            _Profissional = _medicamento.Profissional
        //                        };

        //    IList<AprazamentoProcedimento> procedimentos = iAprazamento.BuscarAprazamentoProcedimento<AprazamentoProcedimento>(prescricao.Codigo, data);
        //    var _procedimentos = from _procedimento in procedimentos
        //                         select new
        //                         {
        //                             _Codigo = "PROC_" + _procedimento.Procedimento.Codigo,
        //                             _Prescricao = _procedimento.Prescricao,
        //                             _Nome = _procedimento.Procedimento.Nome,
        //                             _Horario = _procedimento.Horario,
        //                             _Profissional = _procedimento.Profissional
        //                         };

        //    IList<AprazamentoProcedimentoNaoFaturavel> procedimentosnaofaturaveis = iAprazamento.BuscarAprazamentoProcedimentoNaoFaturavel<AprazamentoProcedimentoNaoFaturavel>(prescricao.Codigo, data);
        //    var _procedimentosnaofaturaveis = from _procedimento in procedimentosnaofaturaveis
        //                                      select new
        //                                      {
        //                                          _Codigo = "PROCNF_" + _procedimento.ProcedimentoNaoFaturavel.Codigo,
        //                                          _Prescricao = _procedimento.Prescricao,
        //                                          _Nome = _procedimento.ProcedimentoNaoFaturavel.Nome,
        //                                          _Horario = _procedimento.Horario,
        //                                          _Profissional = _procedimento.Profissional
        //                                      };

        //    var _aprazamentosconcatenados = _procedimentos.Concat(_procedimentosnaofaturaveis).Concat(_medicamentos);
        //    var _aprazamentos =
        //        from _aprazamento in
        //            _aprazamentosconcatenados
        //        orderby _aprazamento._Nome
        //        group _aprazamento by _aprazamento._Codigo into g
        //        select g;

        //    IList<ViverMais.Model.Profissional> profissionais = new List<ViverMais.Model.Profissional>();
        //    DataTable responsaveis = new DataTable();
        //    responsaveis.Columns.Add("nome", typeof(string));

        //    foreach (var aprazamento in _aprazamentos)
        //    {
        //        var listaordenada = aprazamento.OrderBy(p => p._Horario).ToList();
        //        DataRow row = corpo.NewRow();
        //        row["prescricao"] = prescricao.Codigo;
        //        row["nome"] = listaordenada.First()._Nome;

        //        foreach (var conteudo in listaordenada)
        //        {
        //            int pos = conteudo._Horario.Hour * 2;

        //            if (conteudo._Horario.Minute != 0)
        //                row[(pos + 1).ToString()] = "X";
        //            else
        //                row[pos.ToString()] = "X";

        //            profissionais.Add(conteudo._Profissional);
        //        }

        //        corpo.Rows.Add(row);
        //    }

        //    profissionais = profissionais.Distinct(new GenericComparer<ViverMais.Model.Profissional>("CPF")).ToList();
        //    foreach (ViverMais.Model.Profissional profissional in profissionais)
        //    {
        //        DataRow row = responsaveis.NewRow();
        //        row["nome"] = profissional.Nome;
        //        responsaveis.Rows.Add(row);
        //    }

        //    IProntuario iProntuario = Factory.GetInstance<IProntuario>();

        //    hash.Add("cabecalho", iProntuario.RetornaCabecalhoGeralProntuario<ViverMais.Model.Prontuario>(iProntuario.BuscarPorCodigo<Prontuario>(prescricao.Prontuario.Codigo)));
        //    hash.Add("corpo", corpo);
        //    hash.Add("responsaveis", responsaveis);

        //    return hash;
        //}

        IList<T> IAprazamento.BuscarAprazamentoMedicamento<T>(long co_prescricao, DateTime data)
        {
            string hql = string.Empty;
            hql = "from ViverMais.Model.AprazamentoMedicamento am where ";
            hql = hql + " am.Prescricao.Codigo  = " + co_prescricao;
            hql = hql + " and TO_CHAR(am.Horario,'DD/MM/YYYY')  = '" + data.ToString("dd/MM/yyyy") + "'";

            IMedicamento iMedicamento = Factory.GetInstance<IMedicamento>();
            IProfissional iProfissional = Factory.GetInstance<IProfissional>();

            IList<AprazamentoMedicamento> aprazamentos = (IList<AprazamentoMedicamento>)(object)Session.CreateQuery(hql).List<T>();

            foreach (AprazamentoMedicamento aprazamento in aprazamentos)
            {
                aprazamento.Medicamento = iMedicamento.BuscarPorCodigo<Medicamento>(aprazamento.CodigoMedicamento);
                aprazamento.Profissional = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissional);

                if (!string.IsNullOrEmpty(aprazamento.CodigoProfissionalConfirmacao))
                    aprazamento.ProfissionalConfirmacao = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissionalConfirmacao);
            }

            return (IList<T>)(object)aprazamentos;
        }

        IList<T> IAprazamento.BuscarAprazamentoProcedimento<T>(long co_prescricao, DateTime data)
        {
            string hql = string.Empty;
            hql = "from ViverMais.Model.AprazamentoProcedimento ap where ";
            hql = hql + " ap.Prescricao.Codigo  = " + co_prescricao;
            hql = hql + " and to_char(ap.Horario,'DD/MM/YYYY')  = '" + data.ToString("dd/MM/yyyy") + "'";

            IList<AprazamentoProcedimento> aprazamentos = (IList<AprazamentoProcedimento>)(object)Session.CreateQuery(hql).List<T>();
            IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();
            IProfissional iProfissional = Factory.GetInstance<IProfissional>();

            foreach (AprazamentoProcedimento aprazamento in aprazamentos)
            {
                aprazamento.Procedimento = iProcedimento.BuscarPorCodigo<ViverMais.Model.Procedimento>(aprazamento.CodigoProcedimento);
                aprazamento.Profissional = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissional);

                if (!string.IsNullOrEmpty(aprazamento.CodigoProfissionalConfirmacao))
                    aprazamento.ProfissionalConfirmacao = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissionalConfirmacao);
            }

            return (IList<T>)(object)aprazamentos;
        }

        IList<T> IAprazamento.BuscarAprazamentoProcedimentoNaoFaturavel<T>(long co_prescricao, DateTime data)
        {
            string hql = string.Empty;
            hql = "from ViverMais.Model.AprazamentoProcedimentoNaoFaturavel ap where ";
            hql = hql + " ap.Prescricao.Codigo  = " + co_prescricao;
            hql = hql + " and to_char(ap.Horario,'DD/MM/YYYY')  = '" + data.ToString("dd/MM/yyyy") + "'";
            IList<AprazamentoProcedimentoNaoFaturavel> aprazamentos = (IList<AprazamentoProcedimentoNaoFaturavel>)(object)Session.CreateQuery(hql).List<T>();

            IProfissional iProfissional = Factory.GetInstance<IProfissional>();

            foreach (AprazamentoProcedimentoNaoFaturavel aprazamento in aprazamentos)
            {
                aprazamento.Profissional = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissional);

                if (!string.IsNullOrEmpty(aprazamento.CodigoProfissionalConfirmacao))
                    aprazamento.ProfissionalConfirmacao = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissionalConfirmacao);
            }

            return (IList<T>)(object)aprazamentos;
        }

        void IAprazamento.ExcluirAprazamentosNaoExecutadosMedicamento<T>(long co_prescricao, int co_medicamento)
        {
            using (Session.BeginTransaction())
            {
                try
                {
                    string hql = string.Empty;
                    hql = "FROM AprazamentoMedicamento AS am WHERE am.Prescricao.Codigo = " + co_prescricao;
                    hql += " AND am.CodigoMedicamento = " + co_medicamento.ToString();
                    hql += " AND am.Status <> '" + Convert.ToChar(AprazamentoMedicamento.StatusItem.Finalizado)
                        + "' AND am.Status <> '" + Convert.ToChar(AprazamentoMedicamento.StatusItem.Recusado) + "'";
                    IList<AprazamentoMedicamento> aprazamentos = (IList<AprazamentoMedicamento>)(object)Session.CreateQuery(hql).List<T>();

                    foreach (AprazamentoMedicamento aprazamento in aprazamentos)
                        Session.Delete(aprazamento);

                    Session.Transaction.Commit();
                }
                catch (Exception f)
                {
                    Session.Transaction.Rollback();
                    throw f;
                }
            }
        }

        void IAprazamento.ExcluirAprazamentosNaoExecutadosProcedimentoNaoFaturavel<T>(long co_prescricao, int co_procedimento)
        {
            using (Session.BeginTransaction())
            {
                try
                {
                    string hql = string.Empty;
                    hql = "FROM AprazamentoProcedimentoNaoFaturavel AS ap WHERE ap.Prescricao.Codigo = " + co_prescricao;
                    hql += " AND ap.ProcedimentoNaoFaturavel.Codigo = " + co_procedimento;
                    hql += " AND ap.Status <> '" + Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Finalizado) + "'";
                    IList<AprazamentoProcedimentoNaoFaturavel> aprazamentos = (IList<AprazamentoProcedimentoNaoFaturavel>)(object)Session.CreateQuery(hql).List<T>();

                    foreach (AprazamentoProcedimentoNaoFaturavel aprazamento in aprazamentos)
                        Session.Delete(aprazamento);

                    Session.Transaction.Commit();
                }
                catch (Exception f)
                {
                    Session.Transaction.Rollback();
                    throw f;
                }
            }
        }

        void IAprazamento.ExcluirAprazamentosNaoExecutadosProcedimento<T>(long co_prescricao, string co_procedimento)
        {
            using (Session.BeginTransaction())
            {
                try
                {
                    string hql = string.Empty;
                    hql = "FROM AprazamentoProcedimento AS am WHERE am.Prescricao.Codigo = " + co_prescricao;
                    hql += " AND am.CodigoProcedimento = '" + co_procedimento + "'";
                    hql += " AND am.Status <> '" + Convert.ToChar(AprazamentoProcedimento.StatusItem.Finalizado) + "'";

                    IList<AprazamentoProcedimento> aprazamentos = (IList<AprazamentoProcedimento>)(object)Session.CreateQuery(hql).List<T>();

                    foreach (AprazamentoProcedimento aprazamento in aprazamentos)
                        Session.Delete(aprazamento);

                    Session.Transaction.Commit();
                }
                catch (Exception f)
                {
                    Session.Transaction.Rollback();
                    throw f;
                }
            }
        }

        IList<T> IAprazamento.BuscarAprazamentoMedicamento<T>(long co_prescricao, int co_medicamento)
        {
            string hql = string.Empty;
            hql = "FROM AprazamentoMedicamento AS am WHERE am.Prescricao.Codigo = " + co_prescricao + " AND am.CodigoMedicamento=" + co_medicamento;
            hql += " ORDER BY am.Horario";
            IList<AprazamentoMedicamento> aprazamentos = (IList<AprazamentoMedicamento>)(object)Session.CreateQuery(hql).List<T>();
            IMedicamento iMedicamento = Factory.GetInstance<IMedicamento>();
            IProfissional iProfissional = Factory.GetInstance<IProfissional>();

            foreach (AprazamentoMedicamento aprazamento in aprazamentos)
            {
                aprazamento.Medicamento = iMedicamento.BuscarPorCodigo<Medicamento>(aprazamento.CodigoMedicamento);
                aprazamento.Profissional = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissional);

                if (!string.IsNullOrEmpty(aprazamento.CodigoProfissionalConfirmacao))
                    aprazamento.ProfissionalConfirmacao = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissionalConfirmacao);
            }

            return (IList<T>)(object)aprazamentos;
        }

        IList<T> IAprazamento.BuscarAprazamentoProcedimento<T>(long co_prescricao, string co_procedimento)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.AprazamentoProcedimento ap WHERE ";
            hql += " ap.Prescricao.Codigo  = " + co_prescricao;
            hql += " AND ap.CodigoProcedimento = '" + co_procedimento + "'";
            hql += " ORDER BY ap.Horario";

            IList<AprazamentoProcedimento> aprazamentos = (IList<AprazamentoProcedimento>)(object)Session.CreateQuery(hql).List<T>();
            IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();
            IProfissional iProfissional = Factory.GetInstance<IProfissional>();

            foreach (AprazamentoProcedimento aprazamento in aprazamentos)
            {
                aprazamento.Procedimento = iProcedimento.BuscarPorCodigo<ViverMais.Model.Procedimento>(aprazamento.CodigoProcedimento);
                aprazamento.Profissional = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissional);

                if (!string.IsNullOrEmpty(aprazamento.CodigoProfissionalConfirmacao))
                    aprazamento.ProfissionalConfirmacao = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissionalConfirmacao);
            }

            return (IList<T>)(object)aprazamentos;
        }

        IList<T> IAprazamento.BuscarAprazamentoProcedimentoNaoFaturavel<T>(long co_prescricao, int co_procedimento)
        {
            string hql = string.Empty;
            hql = "FROM AprazamentoProcedimentoNaoFaturavel AS am WHERE am.Prescricao.Codigo = " + co_prescricao + " AND am.ProcedimentoNaoFaturavel.Codigo=" + co_procedimento;
            hql += " ORDER BY am.Horario";
            IList<AprazamentoProcedimentoNaoFaturavel> aprazamentos = (IList<AprazamentoProcedimentoNaoFaturavel>)(object)Session.CreateQuery(hql).List<T>();
            IProfissional iProfissional = Factory.GetInstance<IProfissional>();

            foreach (AprazamentoProcedimentoNaoFaturavel aprazamento in aprazamentos)
            {
                aprazamento.Profissional = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissional);

                if (!string.IsNullOrEmpty(aprazamento.CodigoProfissionalConfirmacao))
                    aprazamento.ProfissionalConfirmacao = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissionalConfirmacao);
            }

            return (IList<T>)(object)aprazamentos;
        }

        IList<T> IAprazamento.BuscarAprazamentoMedicamento<T>(long co_prescricao, char status)
        {
            string hql = string.Empty;
            hql = "FROM AprazamentoMedicamento AS am WHERE am.Prescricao.Codigo = " + co_prescricao + " AND am.Status='" + status + "'";
            hql += " ORDER BY am.Horario";
            IList<AprazamentoMedicamento> aprazamentos = (IList<AprazamentoMedicamento>)(object)Session.CreateQuery(hql).List<T>();
            IMedicamento iMedicamento = Factory.GetInstance<IMedicamento>();
            IProfissional iProfissional = Factory.GetInstance<IProfissional>();

            foreach (AprazamentoMedicamento aprazamento in aprazamentos)
            {
                aprazamento.Medicamento = iMedicamento.BuscarPorCodigo<Medicamento>(aprazamento.CodigoMedicamento);
                aprazamento.Profissional = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissional);

                if (!string.IsNullOrEmpty(aprazamento.CodigoProfissionalConfirmacao))
                    aprazamento.ProfissionalConfirmacao = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissionalConfirmacao);
            }

            return (IList<T>)(object)aprazamentos;
        }

        IList<T> IAprazamento.BuscarAprazamentoProcedimento<T>(long co_prescricao, char status)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.AprazamentoProcedimento ap WHERE ";
            hql += " ap.Prescricao.Codigo  = " + co_prescricao;
            hql += " AND ap.Status = '" + status + "'";
            hql += " ORDER BY ap.Horario";

            IList<AprazamentoProcedimento> aprazamentos = (IList<AprazamentoProcedimento>)(object)Session.CreateQuery(hql).List<T>();
            IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();
            IProfissional iProfissional = Factory.GetInstance<IProfissional>();

            foreach (AprazamentoProcedimento aprazamento in aprazamentos)
            {
                aprazamento.Procedimento = iProcedimento.BuscarPorCodigo<ViverMais.Model.Procedimento>(aprazamento.CodigoProcedimento);
                aprazamento.Profissional = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissional);

                if (!string.IsNullOrEmpty(aprazamento.CodigoProfissionalConfirmacao))
                    aprazamento.ProfissionalConfirmacao = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissionalConfirmacao);
            }

            return (IList<T>)(object)aprazamentos;
        }

        IList<T> IAprazamento.BuscarAprazamentoProcedimentoNaoFaturavel<T>(long co_prescricao, char status)
        {
            string hql = string.Empty;
            hql = "FROM AprazamentoProcedimentoNaoFaturavel AS am WHERE am.Prescricao.Codigo = " + co_prescricao + " AND am.Status='" + status + "'";
            hql += " ORDER BY am.Horario";
            IList<AprazamentoProcedimentoNaoFaturavel> aprazamentos = (IList<AprazamentoProcedimentoNaoFaturavel>)(object)Session.CreateQuery(hql).List<T>();
            IProfissional iProfissional = Factory.GetInstance<IProfissional>();

            foreach (AprazamentoProcedimentoNaoFaturavel aprazamento in aprazamentos)
            {
                aprazamento.Profissional = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissional);

                if (!string.IsNullOrEmpty(aprazamento.CodigoProfissionalConfirmacao))
                    aprazamento.ProfissionalConfirmacao = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissionalConfirmacao);
            }

            return (IList<T>)(object)aprazamentos;
        }

        #endregion

        public AprazamentoDAO()
        {
        }
    }
}
