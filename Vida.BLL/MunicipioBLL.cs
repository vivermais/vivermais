using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;


namespace ViverMais.BLL
{
    public class MunicipioBLL
    {
        public static Municipio PesquisarPorCodigo(string codigo)
        {
            MunicipioDAO dao = new MunicipioDAO();
            Municipio retorno = dao.Pesquisar(codigo);            
            return retorno;
        }

        public static void CompletarMunicipio(Municipio municipio)
        {
            MunicipioDAO dao = new MunicipioDAO();
            dao.Completar(municipio);
        }

        public static List<Municipio> PesquisarPorEstado(string sigla)
        {
            MunicipioDAO dao = new MunicipioDAO();
            List<Municipio> municipios = dao.PesquisarPorEstado(sigla);
            return municipios;
        }

        public static Municipio PesquisarPorNome(string nome)
        {
            MunicipioDAO dao = new MunicipioDAO();
            Municipio municipio = new Municipio();
            municipio = dao.PesquisarPorNome(nome);
           return municipio;
        }
    }
}
