using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public class ProteseDeficienciaBLL
    {
        public static List<ProteseDeficiencia> ListarTodos()
        {
            ProteseDeficienciaDAO dao = new ProteseDeficienciaDAO();
            return dao.ListarTodos();
        }

        /// <summary>
        /// Retorna a lista contendo o tipo 'OUTROS' e 'NAO UTILIZA' nas últimas posições da lista
        /// </summary>
        /// <returns></returns>
        public static List<ProteseDeficiencia> ListarTodosOrdenados()
        {
            List<ProteseDeficiencia> ordenado = new List<ProteseDeficiencia>();
            ProteseDeficienciaDAO dao = new ProteseDeficienciaDAO();
            List<ProteseDeficiencia> lista = dao.ListarTodos();
            
            ordenado = lista.Where(p => p.Codigo != ProteseDeficiencia.NAO_UTILIZA).OrderBy(p => p.Nome).ToList();
            ordenado.Add(lista.Where(p => p.Codigo == ProteseDeficiencia.NAO_UTILIZA).First());

            return ordenado;
        }

        public static ProteseDeficiencia Pesquisar(int codigo)
        {
            ProteseDeficienciaDAO dao = new ProteseDeficienciaDAO();
            ProteseDeficiencia objeto = dao.Pesquisar(codigo);
            return objeto;
        }
    }
}
