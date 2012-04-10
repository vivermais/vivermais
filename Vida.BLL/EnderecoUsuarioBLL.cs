﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;
using Oracle.DataAccess.Client;


namespace ViverMais.BLL
{
    public class EnderecoUsuarioBLL
    {
        public static EnderecoUsuario PesquisarPorUsuario(string codigoUsuario)
        {
            EnderecoUsuarioDAO dao = new EnderecoUsuarioDAO();
            EnderecoUsuario retorno = dao.PesquisarPorUsuario(codigoUsuario);
            return retorno;
        }

        public static EnderecoUsuario Pesquisar(string codigoUsuario, string codigoEndereco)
        {
            EnderecoUsuarioDAO dao = new EnderecoUsuarioDAO();
            EnderecoUsuario retorno = dao.Pesquisar(codigoUsuario, codigoEndereco);
            return retorno;
        }

        public static void Cadastrar(EnderecoUsuario enderecoUsuario)
        {
            OracleTransaction transaction = null;
            ControleEnderecoDAO daoControle = new ControleEnderecoDAO();
            EnderecoDAO daoEndereco = new EnderecoDAO();
            EnderecoUsuarioDAO daoEnderecoUsuario = new EnderecoUsuarioDAO();
            try
            {
                daoControle.Inserir(enderecoUsuario.Endereco.ControleEndereco, ref transaction);
                daoEndereco.Cadastrar(enderecoUsuario.Endereco,ref transaction);
                daoEnderecoUsuario.Cadastrar(enderecoUsuario, ref transaction);
                transaction.Commit();
            }
            catch (Exception)
            {                
                transaction.Rollback();
                throw;
            }
            

        }

        /// <summary>
        /// Realiza a mudança de endereço para
        /// um usuário, desativando o antigo e cadastrando um novo registro
        /// </summary>
        /// <param name="antigoEndereco"></param>
        /// <param name="novoEndereco"></param>
        public static void RealizarMudanca(EnderecoUsuario antigoEndereco, Endereco enderecoAtualizacao)
        {
            OracleTransaction transaction = null;
            EnderecoUsuarioDAO daoEnderecoUsuario = new EnderecoUsuarioDAO();
            ControleEnderecoDAO daoControle = new ControleEnderecoDAO();
            EnderecoDAO daoEndereco = new EnderecoDAO();

            antigoEndereco.Excluido = '1';

            EnderecoUsuario novoEndereco = new EnderecoUsuario();
            novoEndereco.Endereco = enderecoAtualizacao;
            novoEndereco.CodigoPaciente = antigoEndereco.CodigoPaciente;
            novoEndereco.Excluido = '0';
            novoEndereco.Operacao = DateTime.Now;
            novoEndereco.TipoEndereco = new TipoEndereco();
            novoEndereco.TipoEndereco.Codigo = "01";
            novoEndereco.Endereco.ControleEndereco = new ControleEndereco();

            try
            {
                daoEnderecoUsuario.Atualizar(antigoEndereco, ref transaction);
                daoControle.Inserir(novoEndereco.Endereco.ControleEndereco, ref transaction);
                daoEndereco.Cadastrar(novoEndereco.Endereco, ref transaction);
                daoEnderecoUsuario.Cadastrar(novoEndereco, ref transaction);

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }

        }
    }
}