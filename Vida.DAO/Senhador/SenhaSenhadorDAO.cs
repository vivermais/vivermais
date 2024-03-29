﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Senhador;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using System.Text.RegularExpressions;
using NHibernate;
using ViverMais.DAO.Helpers;

namespace ViverMais.DAO.Senhador
{
    public class SenhaSenhadorDAO : SenhadorDAO, ISenhaSenhador
    {
        /// <summary>
        /// Gera uma senha, de atendimento ao paciente, para um serviço específico de determinado estabelecimento de saúde
        /// </summary>
        /// <param name="co_servico">código do serviço</param>
        /// <param name="co_estabelecimento">código do estabelecimento</param>
        /// <param name="co_tiposenha">código do tipo de senha</param>
        /// <param name="co_paciente">código do paciente</param>
        /// <returns>Senha Impressa</returns>
        string ISenhaSenhador.GerarSenhaAtendimentoPaciente(int co_servico, string co_estabelecimento, int co_tiposenha, string co_paciente)
        {
            SenhaPaciente senha = this.GerarSenhaAtendimentoPaciente(co_servico, co_estabelecimento, co_tiposenha, co_paciente);
            return senha.Impressao();
        }

        /// <summary>
        /// Gera uma senha, de atendimento ao paciente, para um serviço específico de determinado estabelecimento de saúde
        /// </summary>
        /// <typeparam name="T">tipo de retorno do objeto senha</typeparam>
        /// <param name="co_servico">código do serviço</param>
        /// <param name="co_estabelecimento">código do estabelecimento</param>
        /// <param name="co_tiposenha">código do tipo de senha</param>
        /// <param name="co_paciente">código do paciente</param>
        /// <returns>Objeto Senha</returns>
        T ISenhaSenhador.GerarSenhaAtendimentoPaciente<T>(int co_servico, string co_estabelecimento, int co_tiposenha, string co_paciente)
        {
            SenhaPaciente senha = this.GerarSenhaAtendimentoPaciente(co_servico, co_estabelecimento, co_tiposenha, co_paciente);
            return (T)(object)senha;
        }

        private SenhaPaciente GerarSenhaAtendimentoPaciente(int co_servico, string co_estabelecimento, int co_tiposenha, string co_paciente)
        {
            IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            IServicoSenhador iServico = Factory.GetInstance<IServicoSenhador>();
            IPaciente iPaciente = Factory.GetInstance<IPaciente>();

            SenhaPaciente senha = new SenhaPaciente();
            senha.Estabelecimento = iEstabelecimento.BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(co_estabelecimento);
            senha.TipoSenha = iViverMais.BuscarPorCodigo<TipoSenhaSenhador>(co_tiposenha);
            senha.Servico = iServico.BuscarPorCodigo<ServicoSenhador>(co_servico);
            senha.Paciente = iPaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(co_paciente);

            //if (senha.Servico.TipoServico.Codigo != TipoServicoSenhador.NAO_AGENDADO)
            //    throw new Exception("Este serviço é incompatível com geração de senhas para o atendimento do paciente.");

            ISession iSession = this.Session;

            using (iSession.BeginTransaction())
            {
                try
                {
                    //string hql = "FROM ViverMais.Model.SenhaPaciente senha WHERE senha.Servico.Codigo=" + co_servico.ToString()
                    //             + " AND senha.Estabelecimento.CNES=:CNES AND TO_CHAR(senha.GeradaEm,'dd/mm/yyyy')=TO_CHAR(sysdate,'dd/mm/yyyy')"
                    //             + " ORDER BY senha.GeradaEm DESC";

                    //IQuery iQuery = iSession.CreateQuery(hql);
                    //iQuery.SetMaxResults(1);
                    //iQuery.SetString("CNES", senha.Estabelecimento.CNES);

                    //SenhaPaciente ultimaSenhaDia = iQuery.UniqueResult<SenhaPaciente>();

                    string sql = @"SELECT * FROM (
                                    SELECT servico.sigla, servico.numero FROM senhr_senha senha INNER JOIN senhr_senhaservico servico
                                    ON servico.co_senha = senha.co_senha INNER JOIN senhr_senhapaciente paciente ON paciente.co_senha = senha.co_senha
                                    WHERE servico.co_servico =:co_servico AND senha.co_estabelecimento =:co_estabelecimento
                                    AND TO_CHAR(senha.geradaEm,'dd/mm/yyyy')=TO_CHAR(sysdate,'dd/mm/yyyy')
                                    AND paciente.co_senha NOT IN (SELECT profissional.co_senha FROM senhr_senhaprofissional profissional
                                    WHERE senha.co_senha=profissional.co_senha) 
                                    ORDER BY senha.geradaEm DESC
                                 ) WHERE ROWNUM = 1";

                    ISQLQuery iSQLQuery = iSession.CreateSQLQuery(sql);
                    iSQLQuery.SetString("co_estabelecimento", senha.Estabelecimento.CNES);
                    iSQLQuery.SetString("co_servico", co_servico.ToString());

                    object[] ultimaSenhaDia = iSQLQuery.UniqueResult<object[]>();

                    if (ultimaSenhaDia != null)
                    {
                        senha.Sigla = ultimaSenhaDia[0].ToString();
                        senha.Numero = int.Parse(ultimaSenhaDia[1].ToString()) + 1;

                        //senha.Senha = ultimaSenhaDia.Senha + 1;
                        //senha.Sigla = ultimaSenhaDia.Sigla;
                    }
                    else
                    {
                        string siglaSenha = null;
                        bool siglaRepetida = true;

                        while (siglaRepetida)
                        {
                            siglaSenha = HelperRandomGenerator.GerarPalavra(2);

                            string hql = "SELECT COUNT(senha.Codigo) FROM ViverMais.Model.SenhaServico senha WHERE senha.Sigla=:SIGLA"
                                + " AND senha.Estabelecimento.CNES=:CNES AND TO_CHAR(senha.GeradaEm,'dd/mm/yyyy')=TO_CHAR(sysdate,'dd/mm/yyyy')"
                                + " AND ROWNUM=1";

                            IQuery iQuery = iSession.CreateQuery(hql);
                            iQuery.SetString("CNES", senha.Estabelecimento.CNES);
                            iQuery.SetString("SIGLA", siglaSenha);

                            siglaRepetida = int.Parse(iQuery.UniqueResult().ToString()) > 0;
                        }

                        senha.Sigla = siglaSenha;
                        senha.Numero = 1;
                    }

                    senha.GeradaEm = DateTime.Now;
                    senha.Senha = senha.Sigla + senha.Numero.ToString("0000");

                    iSession.Save(senha);
                    iSession.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    iSession.Transaction.Rollback();

                    throw ex;
                }
            }

            return senha;
        }

        /// <summary>
        /// Gera uma senha, de atendimento ao paciente, para um serviço e profissional específico de determinado estabelecimento de saúde
        /// </summary>
        /// <param name="co_servico">código do serviço</param>
        /// <param name="co_estabelecimento">código do estabelecimento</param>
        /// <param name="co_tiposenha">código do tipo de senha</param>
        /// <param name="co_paciente">código do paciente</param>
        /// <param name="co_profissional">código do profissional</param>
        /// <returns>Senha Impressa</returns>
        string ISenhaSenhador.GerarSenhaAtendimentoPaciente(int co_servico, string co_estabelecimento, int co_tiposenha, string co_paciente, string co_profissional)
        {
            SenhaProfissional senha = this.GerarSenhaAtendimentoPaciente(co_servico, co_estabelecimento, co_tiposenha, co_paciente, co_profissional);
            return senha.Impressao();
        }

        /// <summary>
        /// Gera uma senha, de atendimento ao paciente, para um serviço e profissional específico de determinado estabelecimento de saúde
        /// </summary>
        /// <typeparam name="T">tipo de retorno do objeto senha</typeparam>
        /// <param name="co_servico">código do serviço</param>
        /// <param name="co_estabelecimento">código do estabelecimento</param>
        /// <param name="co_tiposenha">código do tipo de senha</param>
        /// <param name="co_profissional">código do profissional</param>
        /// <returns>Objeto Senha</returns>
        T ISenhaSenhador.GerarSenhaAtendimentoPaciente<T>(int co_servico, string co_estabelecimento, int co_tiposenha, string co_paciente, string co_profissional)
        {
            SenhaProfissional senha = this.GerarSenhaAtendimentoPaciente(co_servico, co_estabelecimento, co_tiposenha, co_paciente, co_profissional);
            return (T)(object)senha;
        }

        private SenhaProfissional GerarSenhaAtendimentoPaciente(int co_servico, string co_estabelecimento, int co_tiposenha, string co_paciente, string co_profissional)
        {
            IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            IServicoSenhador iServico = Factory.GetInstance<IServicoSenhador>();
            IProfissional iProfissional = Factory.GetInstance<IProfissional>();
            IPaciente iPaciente = Factory.GetInstance<IPaciente>();

            SenhaProfissional senha = new SenhaProfissional();
            senha.Estabelecimento = iEstabelecimento.BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(co_estabelecimento);
            senha.TipoSenha = iViverMais.BuscarPorCodigo<TipoSenhaSenhador>(co_tiposenha);
            senha.Servico = iServico.BuscarPorCodigo<ServicoSenhador>(co_servico);
            senha.Profissional = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(co_profissional);
            senha.Paciente = iPaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(co_paciente);

            //if (senha.Servico.TipoServico.Codigo != TipoServicoSenhador.AGENDADO)
            //    throw new Exception("Este serviço é incompatível com geração de senhas para o atendimento do paciente com um profissional específico.");

            ISession iSession = this.Session;

            using (iSession.BeginTransaction())
            {
                try
                {
                    string sql = @"SELECT * FROM (
                                    SELECT servico.sigla, servico.numero FROM senhr_senha senha INNER JOIN senhr_senhaservico servico
                                    ON servico.co_senha = senha.co_senha INNER JOIN senhr_senhaprofissional profissional ON profissional.co_senha = senha.co_senha
                                    WHERE servico.co_servico =:co_servico AND senha.co_estabelecimento =:co_estabelecimento
                                    AND profissional.co_profissional =:co_profissional
                                    AND TO_CHAR(senha.geradaEm,'dd/mm/yyyy')=TO_CHAR(sysdate,'dd/mm/yyyy') ORDER BY senha.geradaEm DESC
                                 ) WHERE ROWNUM = 1";

                    ISQLQuery iSQLQuery = iSession.CreateSQLQuery(sql);
                    iSQLQuery.SetString("co_estabelecimento", senha.Estabelecimento.CNES);
                    iSQLQuery.SetString("co_servico", co_servico.ToString());
                    iSQLQuery.SetString("co_profissional", senha.Profissional.CPF);

                    object[] ultimaSenhaDia = iSQLQuery.UniqueResult<object[]>();
                    //string hql = "FROM ViverMais.Model.SenhaProfissional senha WHERE senha.Servico.Codigo=" + co_servico.ToString()
                    //                + " AND senha.Estabelecimento.CNES=:CNES AND TO_CHAR(senha.GeradaEm,'dd/mm/yyyy')=TO_CHAR(sysdate,'dd/mm/yyyy')"
                    //                + " AND senha.Profissional.CPF=:CPF"
                    //                + " ORDER BY senha.GeradaEm DESC";

                    //IQuery iQuery = iSession.CreateQuery(hql);
                    //iQuery.SetMaxResults(1);
                    //iQuery.SetString("CNES", senha.Estabelecimento.CNES);
                    //iQuery.SetString("CPF", senha.Profissional.CPF);

                    //SenhaProfissional ultimaSenhaDia = iQuery.UniqueResult<SenhaProfissional>();

                    if (ultimaSenhaDia != null)
                    {
                        senha.Sigla = ultimaSenhaDia[0].ToString();
                        senha.Numero = int.Parse(ultimaSenhaDia[1].ToString()) + 1;

                        //senha.Senha = ultimaSenhaDia.Senha + 1;
                        //senha.Sigla = ultimaSenhaDia.Sigla;
                    }
                    else
                    {
                        string siglaSenha = null;
                        bool siglaRepetida = true;

                        while (siglaRepetida)
                        {
                            siglaSenha = HelperRandomGenerator.GerarPalavra(2);

                            string hql = "SELECT COUNT(senha.Codigo) FROM ViverMais.Model.SenhaServico senha WHERE senha.Sigla=:SIGLA"
                                + " AND senha.Estabelecimento.CNES=:CNES AND TO_CHAR(senha.GeradaEm,'dd/mm/yyyy')=TO_CHAR(sysdate,'dd/mm/yyyy')"
                                + " AND ROWNUM=1";

                            IQuery iQuery = iSession.CreateQuery(hql);
                            iQuery.SetString("CNES", senha.Estabelecimento.CNES);
                            iQuery.SetString("SIGLA", siglaSenha);

                            siglaRepetida = int.Parse(iQuery.UniqueResult().ToString()) > 0;
                        }

                        senha.Sigla = siglaSenha;
                        senha.Numero = 1;
                    }

                    senha.GeradaEm = DateTime.Now;
                    senha.Senha = senha.Sigla + senha.Numero.ToString("0000");

                    iSession.Save(senha);
                    iSession.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    iSession.Transaction.Rollback();

                    throw ex;
                }
            }

            return senha;
        }
    }
}
