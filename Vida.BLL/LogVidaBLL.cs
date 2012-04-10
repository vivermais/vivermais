using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public static class LogViverMaisBLL
    {
        public static int QuantidadeCartoesPorUsuario(Usuario usuario, DateTime dataInicio, DateTime dataFim)
        {
            LogViverMaisDAO dao = new LogViverMaisDAO();
            int quantidade = dao.QuantidadeDeCartoesPorUsuario(usuario.Codigo, dataInicio, dataFim);
            return quantidade;
        }
    }
}
