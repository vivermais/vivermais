using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public class TipoDocumentoBLL
    {
        public static void CompletarControles(List<ControleDocumento> controles)
        {
            TipoDocumentoDAO dao = new TipoDocumentoDAO();
            foreach (ControleDocumento item in controles)
            {
                dao.Completar(item.TipoDocumento);
            }            
        }
    }
}
