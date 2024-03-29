﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;
using Oracle.DataAccess.Client;


namespace ViverMais.BLL
{
    public class PacienteBLL
    {
        public static Paciente Pesquisar(string codigo)
        {
            PacienteDAO dao = new PacienteDAO();
            DeficienciaPacienteDAO decificienciaDAO = new DeficienciaPacienteDAO();
            Paciente retorno =  dao.Pesquisar(codigo);

            //Deficiencia paciente
            retorno.Deficiencia = decificienciaDAO.PesquisarPorPaciente(retorno.Codigo);
            return retorno;
        }

        public static Paciente PesquisarCompleto(string codigo)
        {
            PacienteDAO daoPaciente = new PacienteDAO();
            DeficienciaPacienteDAO decificienciaDAO = new DeficienciaPacienteDAO();
            Paciente paciente = daoPaciente.Pesquisar(codigo);
            if (paciente.MunicipioNascimento != null && paciente.MunicipioNascimento.Codigo != string.Empty)
            {
                MunicipioBLL.CompletarMunicipio(paciente.MunicipioNascimento);
                UFBLL.Completar(paciente.MunicipioNascimento.UF);
            }
            if (paciente.MunicipioResidencia != null && paciente.MunicipioResidencia.Codigo != string.Empty)
            {
                MunicipioBLL.CompletarMunicipio(paciente.MunicipioResidencia);
                UFBLL.Completar(paciente.MunicipioResidencia.UF);
            }

            //Deficiencia paciente
            paciente.Deficiencia = decificienciaDAO.PesquisarPorPaciente(paciente.Codigo);
            return paciente;
        }

        // Método Utilizado para apenas o Formulário de Paciente, pois necessita de Atualização de Deficiência
        public static void Atualizar(Paciente paciente)
        {
            OracleTransaction trans = null;
            PacienteDAO dao = new PacienteDAO();
            DeficienciaPacienteDAO deficienteDAO = new DeficienciaPacienteDAO();

            try
            {
                dao.Inserir(paciente, ref trans);

                //Colocar aqui o cadastro de deficiência
                DeficienciaPaciente deficienciaAntigo = deficienteDAO.PesquisarPorPaciente(paciente.Codigo, ref trans);

                if (deficienciaAntigo != null)
                    deficienteDAO.Atualizar(paciente.Deficiencia, deficienciaAntigo, paciente.Codigo, ref trans);
                else
                    deficienteDAO.Cadastrar(paciente.Deficiencia, paciente.Codigo, ref trans);
                //dao.Inserir(paciente);
                
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
        }

        // Método utilizado para Salvar alguns campos do Paciente e que não necessitem atualizar a Deficiência do Paciente
        // Utilizar esse método quando estiver em outro módulo do Sistema ViverMais. Ex: Regulação / Laboratório / Urgência
        public static void AtualizarDadosPaciente(Paciente paciente)
        {
            OracleTransaction trans = null;
            PacienteDAO dao = new PacienteDAO();
            DeficienciaPacienteDAO deficienteDAO = new DeficienciaPacienteDAO();

            try
            {
                dao.Inserir(paciente, ref trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
        }

        // Método utilizado para Salvar alguns campos do Paciente e que não necessitem atualizar a Deficiência do Paciente
        // Utilizar esse método quando estiver em outro módulo do Sistema ViverMais. Ex: Regulação / Laboratório / Urgência
        public static void AtualizarDadosPacienteSemLog(Paciente paciente)
        {
            OracleTransaction trans = null;
            PacienteDAO dao = new PacienteDAO();
            DeficienciaPacienteDAO deficienteDAO = new DeficienciaPacienteDAO();

            try
            {
                dao.InserirSemLog(paciente, ref trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
        }

        public static List<Paciente> Pesquisar(string nome, string nomeMae, DateTime dataNascimento)
        {
            PacienteDAO dao = new PacienteDAO();
            List<Paciente> retorno = dao.Pesquisar(nome, nomeMae, dataNascimento);
            return retorno;
        }

        public static Paciente PesquisarPorCPF(string numeroCPF)
        {
            PacienteDAO pacienteDAO = new PacienteDAO();
            DeficienciaPacienteDAO decificienciaDAO = new DeficienciaPacienteDAO();
            Paciente paciente = pacienteDAO.PesquisarPorCPF(numeroCPF);

            if (paciente != null) //Deficiencia paciente
                paciente.Deficiencia = decificienciaDAO.PesquisarPorPaciente(paciente.Codigo);

            return paciente;
        }

        public static void Cadastrar(Paciente paciente)
        {
            //Gerar a exceção caso não haja cartao para ser atribuido

            OracleTransaction trans = null;
            try
            {
                DeficienciaPacienteDAO deficienciaDAO = new DeficienciaPacienteDAO();
                CartaoBaseDAO daoCartaoBase = new CartaoBaseDAO();
                CartaoBase cartaoBase = daoCartaoBase.PesquisarPrimeiroNaoAtribuido(ref trans);
                //Coloquei aqui para tornar a operação de tornar o cartão já
                //atribuído mais rápuida. Diminuindo assim problemas de concorrência.
                cartaoBase.Atribuido = 1;
                daoCartaoBase.Inserir(cartaoBase, ref trans);
                
                if (cartaoBase == null)
                    throw new Exception("Não existem números de cartão disponíveis. Favor entrar em contato de imediato com o NGI.");
                PacienteDAO daoPaciente = new PacienteDAO();
                try
                {
                    daoPaciente.Inserir(paciente, ref trans);
                }
                //Realizado na refatoração para retirar o hibernate.
                catch (OracleException ex)
                {
                    //Erro de Unique Constraint
                    if (ex.Message.Contains("ORA-00001"))
                    {
                        cartaoBase = daoCartaoBase.PesquisarPrimeiroNaoAtribuido(ref trans);
                        //Coloquei aqui para tornar a operação de tornar o cartão já
                        //atribuído mais rápuida. Diminuindo assim problemas de concorrência.
                        cartaoBase.Atribuido = 1;
                        daoCartaoBase.Inserir(cartaoBase, ref trans);
                        if (cartaoBase == null)
                            throw new Exception("Não existem números de cartão disponíveis. Favor entrar em contato de imediato com o NGI.");
                        daoPaciente.Inserir(paciente, ref trans);
                    }
                }

                CartaoSUS cartao = new CartaoSUS();
                cartao.Paciente = paciente;
                cartao.DataAtribuicao = DateTime.Today;
                cartao.Numero = cartaoBase.Numero;
                cartao.Tipo = 'P';
                cartao.Excluido = '0';

                CartaoSUSDAO daoCartaoSUS = new CartaoSUSDAO();
                daoCartaoSUS.Inserir(cartao, ref trans);
                //cartaoBase.Atribuido = 1;
                //daoCartaoBase.Inserir(cartaoBase,ref trans);

                //Colocar aqui o cadastro de deficiência
                deficienciaDAO.Cadastrar(paciente.Deficiencia, paciente.Codigo, ref trans);
                trans.Commit();
            }
            catch (System.IndexOutOfRangeException)
            {
                throw new Exception("Não existem números de cartão disponíveis. Favor entrar em contato de imediato com o NGI.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                trans.Rollback();
                throw ex;
            }            
        }

    }
}
