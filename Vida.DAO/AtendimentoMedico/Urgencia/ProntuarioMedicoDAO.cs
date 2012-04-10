using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Vida.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;

namespace Vida.DAO.AtendimentoMedico.Urgencia
{
    public class ProntuarioMedicoDAO : UrgenciaServiceFacadeDAO, IProntuarioMedico
    {
        #region IProntuarioMedico

        IList<T> IProntuarioMedico.buscarPorProntuario<T>(int co_prontuario) 
            {
                //prontuario.Situacao.Codigo = 1  => Paciente aguardando avaliacao inicial
                string hql = string.Empty;
                       hql = "from Vida.Model.ProntuarioMedico prontuariomedico " +
                             " where prontuariomedico.Prontuario.Codigo = " + co_prontuario +
                             " order by prontuariomedico.Data";
                return Session.CreateQuery(hql).List<T>();
            }

        void IProntuarioMedico.SalvarProntuarioMedico<T,V>(T ProntuarioMedico,bool OcupaVaga,bool DesocupaVaga,V VagaUrgencia)
        {
            Vida.Model.ProntuarioMedico prontmedico = (Vida.Model.ProntuarioMedico)(object)ProntuarioMedico;
            Vida.Model.VagaUrgencia vagaurgencia = (Vida.Model.VagaUrgencia)(object)VagaUrgencia;

            using (Session.BeginTransaction()) 
            {
                try 
                {
                    Session.SaveOrUpdate(prontmedico.Prontuario);

                    if (OcupaVaga)
                    {
                        vagaurgencia.Prontuario = prontmedico.Prontuario;
                        Session.Update(vagaurgencia);
                    }
                    else 
                    {
                        if (DesocupaVaga) 
                        {
                            vagaurgencia.Prontuario = null;
                            Session.Update(vagaurgencia);
                        }
                    }

                    Vida.Model.ProntuarioMedico ptransiente = (Vida.Model.ProntuarioMedico)Session.Merge(prontmedico);
                    Session.SaveOrUpdate(ptransiente);

                    IList<Vida.Model.ProntuarioProcedimento> lpptransiente = prontmedico.ProntuarioProcedimento;

                    if (lpptransiente != null && lpptransiente.Count() > 0)
                    {
                        foreach (Vida.Model.ProntuarioProcedimento pp in lpptransiente)
                        {
                            pp.ProntuarioMedico = ptransiente;
                            
                            if (pp.Excluir)
                                Session.Delete(pp);
                            else
                            {
                                if (pp.Atualizar)
                                    Session.Update(pp);
                                else
                                    Session.Save(pp);
                            }
                        }
                    }

                    Session.Transaction.Commit();
                    Session.Flush();
                }catch(Exception f)
                {
                    Session.Transaction.Rollback();
                    throw f;
                }
            }
        }

        #endregion
        public ProntuarioMedicoDAO()
        {

        }
    }
}
