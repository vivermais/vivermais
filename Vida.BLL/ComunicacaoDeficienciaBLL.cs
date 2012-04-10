using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public class ComunicacaoDeficienciaBLL
    {
        public static List<ComunicacaoDeficiencia> ListarTodos()
        {
            ComunicacaoDeficienciaDAO dao = new ComunicacaoDeficienciaDAO();
            return dao.ListarTodos();
        }

        /// <summary>
        /// Retorna a lista contendo o tipo 'OUTROS' e 'NAO UTILIZA' nas últimas posições da lista
        /// </summary>
        /// <returns></returns>
        public static List<ComunicacaoDeficiencia> ListarTodosOrdenados()
        {
            List<ComunicacaoDeficiencia> ordenado = new List<ComunicacaoDeficiencia>();
            ComunicacaoDeficienciaDAO dao = new ComunicacaoDeficienciaDAO();
            List<ComunicacaoDeficiencia> lista = dao.ListarTodos();

            ordenado = lista.Where(p => p.Codigo != ComunicacaoDeficiencia.NAO_UTILIZA && p.Codigo != ComunicacaoDeficiencia.OUTROS).OrderBy(p => p.Nome).ToList();
            ordenado.Add(lista.Where(p => p.Codigo == ComunicacaoDeficiencia.OUTROS).First());
            ordenado.Add(lista.Where(p => p.Codigo == ComunicacaoDeficiencia.NAO_UTILIZA).First());

            return ordenado;
        }

        public static ComunicacaoDeficiencia Pesquisar(int codigo)
        {
            ComunicacaoDeficienciaDAO dao = new ComunicacaoDeficienciaDAO();
            ComunicacaoDeficiencia objeto = dao.Pesquisar(codigo);
            return objeto;
        }
    }
}
