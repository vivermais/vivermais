using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public class CartaoSUSBLL
    {
        public static List<CartaoSUS> ListarPorPaciente(Paciente paciente)
        {
            CartaoSUSDAO dao = new CartaoSUSDAO();
            List<CartaoSUS> retorno = dao.PesquisarPorPaciente(paciente);            
            return retorno;
        }
    }
}
