using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public class OrigemDeficienciaBLL
    {
        public static List<OrigemDeficiencia> ListarTodos()
        {
            OrigemDeficienciaDAO dao = new OrigemDeficienciaDAO();
            return dao.ListarTodos();
        }

        public static OrigemDeficiencia Pesquisar(int codigo)
        {
            OrigemDeficienciaDAO dao = new OrigemDeficienciaDAO();
            OrigemDeficiencia objeto = dao.Pesquisar(codigo);
            return objeto;
        }
    }
}
