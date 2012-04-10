using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public class MotivoCadastroBLL
    {
        public static List<MotivoCadastro> ListarTodos()
        {
            MotivoCadastroDAO dao = new MotivoCadastroDAO();            
            List<MotivoCadastro> retorno = dao.ListarTodos();            
            return retorno;
        }
    }
}
