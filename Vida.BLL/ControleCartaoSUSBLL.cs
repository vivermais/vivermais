using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public class ControleCartaoSUSBLL
    {
        public static List<ControleCartaoSUS> ListarPorCartao(CartaoSUS cartao)
        {            
            ControleCartaoSUSDAO dao = new ControleCartaoSUSDAO();            
            List<ControleCartaoSUS> retorno = dao.PesquisarPorNumeroCartao(cartao.Numero);            
            return retorno;
        }
    }
}
