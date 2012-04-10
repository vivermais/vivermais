
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public static class EtniaBLL
    {
        public static void CompletarObjeto(Etnia objeto)
        {
            EtniaDAO dao = new EtniaDAO();
            dao.Completar(objeto);
        }

        public static List<Etnia> ListarTodos()
        {
            EtniaDAO dao = new EtniaDAO();
            return dao.ListarTodos();
        }

        public static void Cadastrar(Etnia etnia)
        {
            EtniaDAO dao = new EtniaDAO();
            dao.Cadastrar(etnia);
        }
    }
}
