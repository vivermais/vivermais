using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public class LogradouroBLL
    {
        public static void Cadastrar(Logradouro objeto)
        {
            if (LocalizarPorCep(objeto.CEP) == null)
            {
                LogradouroDAO dao = new LogradouroDAO();
                dao.Cadastrar(objeto);                
            }
        }

        public static Logradouro LocalizarPorCep(long cep)
        {
            LogradouroDAO dao = new LogradouroDAO();
            Logradouro retorno = dao.PesquisarPorCep(cep);            
            return retorno;
        }
    }
}
