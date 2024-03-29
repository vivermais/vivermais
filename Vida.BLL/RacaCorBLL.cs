﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public static class RacaCorBLL
    {
        public static RacaCor PesquisarPorCodigo(string codigo)
        {
            RacaCorDAO dao = new RacaCorDAO();
            RacaCor racacor = new RacaCor();
            racacor = dao.Pesquisar(codigo);            
            return racacor;
        }

        public static List<RacaCor> ListarTodos()
        {
            RacaCorDAO dao = new RacaCorDAO();
            List<RacaCor> retorno = dao.ListarTodos();            
            return retorno;
        }
    }
}
