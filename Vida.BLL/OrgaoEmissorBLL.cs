using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public class OrgaoEmissorBLL
    {
        public static List<OrgaoEmissor> ListarTodos()
        {
            OrgaoEmissorDAO dao = new OrgaoEmissorDAO();
            List<OrgaoEmissor> retorno = dao.ListarTodos();            
            return retorno;
        }
    }
}
