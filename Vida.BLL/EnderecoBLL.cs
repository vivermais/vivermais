﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public class EnderecoBLL
    {
        public static void CompletarEndereco(Endereco endereco)
        {
            EnderecoDAO dao = new EnderecoDAO();
            dao.Completar(endereco);            
        }

        public static Endereco PesquisarPorPaciente(Paciente paciente)
        {
            EnderecoUsuarioDAO dao = new EnderecoUsuarioDAO();
            EnderecoUsuario endUsuario = dao.PesquisarPorPaciente(paciente);
            if (endUsuario == null)
                return null;
            CompletarEndereco(endUsuario.Endereco);
            return endUsuario.Endereco;            
        }

        public static Endereco PesquisarCompletoPorPaciente(Paciente paciente)
        {
            Endereco retorno = null;
            EnderecoUsuarioDAO dao = new EnderecoUsuarioDAO();
            MunicipioDAO municipioDAO = new MunicipioDAO();
            TipoLogradouroDAO tipoLogradouroDAO = new TipoLogradouroDAO();
            EnderecoUsuario endUsuario = dao.PesquisarPorPaciente(paciente);
            if (endUsuario == null)
                return null;
            retorno = endUsuario.Endereco;
            CompletarEndereco(retorno);
            municipioDAO.Completar(retorno.Municipio);
            tipoLogradouroDAO.Completar(retorno.TipoLogradouro);
            return retorno;
        }

        public static void Atualizar(Endereco endereco)
        {
            EnderecoDAO dao = new EnderecoDAO();
            dao.Inserir(endereco);            
        }
    }
}