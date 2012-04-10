using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public static class EstabelecimentoSaudeBLL
    {
        public static List<EstabelecimentoSaude> ListarTodos()
        {
            EstabelecimentoSaudeDAO dao = new EstabelecimentoSaudeDAO();
            List<EstabelecimentoSaude> lista = dao.ListarTodos();
            lista = lista.OrderBy(x => x.NomeFantasia).ToList();
            return lista;
        }
    }
}
