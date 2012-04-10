using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public class LocomocaoDeficienciaBLL
    {
        public static List<LocomocaoDeficiencia> ListarTodos()
        {
            LocomocaoDeficienciaDAO dao = new LocomocaoDeficienciaDAO();
            return dao.ListarTodos();
        }

        /// <summary>
        /// Retorna a lista contendo o tipo 'OUTROS' e 'NAO UTILIZA' nas últimas posições da lista
        /// </summary>
        /// <returns></returns>
        public static List<LocomocaoDeficiencia> ListarTodosOrdenados()
        {
            List<LocomocaoDeficiencia> ordenado = new List<LocomocaoDeficiencia>();
            LocomocaoDeficienciaDAO dao = new LocomocaoDeficienciaDAO();
            List<LocomocaoDeficiencia> lista = dao.ListarTodos();

            ordenado = lista.Where(p => p.Codigo != LocomocaoDeficiencia.NAO_UTILIZA && p.Codigo != LocomocaoDeficiencia.OUTROS).OrderBy(p => p.Nome).ToList();
            ordenado.Add(lista.Where(p => p.Codigo == LocomocaoDeficiencia.OUTROS).First());
            ordenado.Add(lista.Where(p => p.Codigo == LocomocaoDeficiencia.NAO_UTILIZA).First());

            return ordenado;
        }

        public static LocomocaoDeficiencia Pesquisar(int codigo)
        {
            LocomocaoDeficienciaDAO dao = new LocomocaoDeficienciaDAO();
            LocomocaoDeficiencia objeto = dao.Pesquisar(codigo);
            return objeto;
        }
    }
}
