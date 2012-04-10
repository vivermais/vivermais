using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;
using Oracle.DataAccess.Client;

namespace ViverMais.BLL
{
    public class CartaoBaseBLL
    {
        public static CartaoBase PesquisarPrimeiroNaoAtribuido()
        {
            CartaoBaseDAO dao = new CartaoBaseDAO();
            OracleTransaction trans = null;
            CartaoBase retorno = dao.PesquisarPrimeiroNaoAtribuido(ref trans);
            trans.Commit();
            return retorno;
        }
    }
}
