using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public class ControlePacienteBLL
    {
        public static ControlePaciente PesquisarPorPaciente(Paciente paciente)
        {
            ControlePacienteDAO dao = new ControlePacienteDAO();
            ControlePaciente controle = dao.PesquisarPorPaciente(paciente);            
            return controle;
        }

        public static void Inserir(ControlePaciente controlePaciente)
        {
            ControlePacienteDAO dao = new ControlePacienteDAO();
            dao.Inserir(controlePaciente);
        }
    }
}
