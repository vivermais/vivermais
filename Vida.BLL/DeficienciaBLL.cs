using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public class DeficienciaBLL
    {
        public static List<Deficiencia> ListarTodos()
        {
            DeficienciaDAO dao = new DeficienciaDAO();
            return dao.ListarTodos();
        }

        public static Deficiencia Pesquisar(int codigo)
        {
            DeficienciaDAO dao = new DeficienciaDAO();
            Deficiencia objeto = dao.Pesquisar(codigo);
            return objeto;
        }
    }
}
