﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public static class MotivocadastroPacienteBLL
    {
        public static MotivoCadastroPaciente PesquisarPorPaciente(Paciente paciente, string cnesUnidade)
        {
            MotivoCadastroPacienteDAO dao = new MotivoCadastroPacienteDAO();
            List<MotivoCadastroPaciente> motivos = dao.PesquisarPorUsuario(paciente);            
            if (motivos.Count != 0)
                return motivos[0];            
            return null;
        }

        public static void Cadastrar(MotivoCadastroPaciente motivo)
        {
            MotivoCadastroPacienteDAO dao = new MotivoCadastroPacienteDAO();
            dao.Cadastrar(motivo);            
        }

        public static void Atualizar(MotivoCadastroPaciente motivo)
        {
            MotivoCadastroPacienteDAO dao = new MotivoCadastroPacienteDAO();
            dao.Atualizar(motivo);            
        }
    }
}
