using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;


namespace ViverMais.BLL
{
    public class EstadoCivilBLL
    {
        public static EstadoCivil PesquisarPorCodigo(string codigo)
        {
            EstadoCivilDAO dao = new EstadoCivilDAO();
            EstadoCivil retorno = dao.Pesquisar(codigo);            
            return retorno;
        }
    }
}
