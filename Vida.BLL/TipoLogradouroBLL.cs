using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public class TipoLogradouroBLL
    {
        public static List<TipoLogradouro> ListarTodos()
        {
            TipoLogradouroDAO dao = new TipoLogradouroDAO();            
            return dao.ListarTodos();
        }
    }
}
