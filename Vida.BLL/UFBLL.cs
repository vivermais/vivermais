using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;
using Oracle.DataAccess.Client;

namespace ViverMais.BLL
{
    public static class UFBLL
    {
        public static UF Cadastrar(string nome, string sigla)
        {            
            UF uf = new UF();
            uf.Nome = nome;
            uf.Sigla = sigla;
            UFDAO dao = new UFDAO();            
            dao.Inserir(uf);            
            return uf;
        }

        public static UF PesquisarPorID(int id)
        {
            UFDAO dao = new UFDAO();
            UF retorno = dao.Pesquisar(id);            
            return retorno;
        }

        public static UF PesquisarPorID(string codigo)
        {
            UFDAO dao = new UFDAO();
            UF retorno = dao.Pesquisar(codigo);            
            return retorno;
        }

        
        public static void Completar(UF estado)
        {
            UFDAO dao = new UFDAO();
            dao.Completar(estado);            
        }

        public static List<UF> ListarTodos()
        {
            UFDAO dao = new UFDAO();
            List<UF> retorno = dao.ListarTodos();            
            return retorno;                
        }

        public static UF PesquisarPorSigla(string sigla)
        {
            UFDAO dao = new UFDAO();
            UF retorno = dao.PesquisarPorSigla(sigla);            
            return retorno;
        }
    }
}
