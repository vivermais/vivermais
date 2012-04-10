using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public class DistritoBLL
    {
        public static List<Distrito> PesquisarPorMunicipio(Municipio municipio)
        {
            DistritoDAO dao = new DistritoDAO();
            List<Distrito> retorno = dao.PesquisarPorMunicipio(municipio);            
            return retorno;
        }

        public static void Cadastrar(Distrito distritoViverMais)
        {
            DistritoDAO dao = new DistritoDAO();            
            dao.Inserir(distritoViverMais);                   
        }
    }
}
