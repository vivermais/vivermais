using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;


namespace ViverMais.BLL
{
    public class ControleDocumentoBLL
    {
        public static List<ControleDocumento> PesquisarPorPaciente(Paciente paciente)
        {
            ControleDocumentoDAO dao = new ControleDocumentoDAO();
            List<ControleDocumento> retorno = dao.PesquisarPorPaciente(paciente);            
            return retorno;
        }
    }
}
