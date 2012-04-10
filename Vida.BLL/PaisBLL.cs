using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;


namespace ViverMais.BLL
{
    public class PaisBLL
    {
        public static void CompletarObjeto(Pais pais)
        {
            PaisDAO dao = new PaisDAO();
            dao.Completar(pais);            
        }

        public static List<Pais> ListarTodos()
        {
            PaisDAO dao = new PaisDAO();
            List<Pais> retorno = dao.ListarTodos();            
            return retorno;
        }
    }
}
