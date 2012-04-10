using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public class BairroBLL
    {
        public static List<Bairro> PesquisarPorMunicipio(Municipio municipio)
        {
            DistritoDAO distritoDAO = new DistritoDAO();
            BairroDAO dao = new BairroDAO();
            List<Distrito> distritos = distritoDAO.PesquisarPorMunicipio(municipio);
            List<Bairro> bairros = new List<Bairro>();
            foreach (Distrito distrito in distritos)
            {
                bairros.AddRange(dao.PesquisarPorDistrito(distrito));
            }            
            return bairros; 
        }

        public static Bairro Pesquisar(string codigo)
        {
            BairroDAO dao = new BairroDAO();
            Bairro bairro = dao.Pesquisar(codigo);
            return bairro;
        }

        public static Bairro PesquisarPorNomeNoDistrito(string nome, string nomeDistrito)
        {
            Distrito distrito = null;
            Bairro bairro = null;
            DistritoDAO distritoDAO = new DistritoDAO();
            BairroDAO dao = new BairroDAO();
            distrito = distritoDAO.PesquisarPorNome(nomeDistrito);
            if (distrito != null)
            {
                List<Bairro> bairros = dao.PesquisarPorDistrito(distrito);
                bairro = bairros.Find(x => x.Nome == nome);
            }            
            return bairro;
        }

        public static void CadastrarComDistrito(Bairro bairroViverMais)
        {
            
            DistritoDAO distritoDAO = new DistritoDAO();
            BairroDAO bairroDAO = new BairroDAO();            
            distritoDAO.Inserir(bairroViverMais.Distrito);
            bairroDAO.Inserir(bairroViverMais);            
        }

        public static void Cadastrar(Bairro bairroViverMais)
        {            
            BairroDAO bairroDAO = new BairroDAO();            
            bairroDAO.Inserir(bairroViverMais);            
        }

        public static void Atualizar(Bairro bairroViverMais)
        {
            BairroDAO bairroDAO = new BairroDAO();
            bairroDAO.Inserir(bairroViverMais);
        }
    }
}
