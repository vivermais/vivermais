using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public class DeficienciaPacienteBLL
    {
        public static DeficienciaPaciente BuscarPorPaciente(string co_usuario)
        {
            DeficienciaPacienteDAO dao = new DeficienciaPacienteDAO();
            return dao.PesquisarPorPaciente(co_usuario);
        }
    }
}
