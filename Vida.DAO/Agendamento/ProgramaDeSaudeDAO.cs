using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;

namespace ViverMais.DAO.Agendamento
{
    public class ProgramaDeSaudeDAO : AgendamentoServiceFacadeDAO, IProgramaDeSaude
    {
        public ProgramaDeSaudeDAO() { }

        IList<T> IProgramaDeSaude.ListarPacientesPorPrograma<T>(int co_programa, bool ativo)
        {
            string hql = "Select prog.Paciente from ViverMais.Model.ProgramaDeSaudePaciente prog where prog.ProgramaDeSaude.Codigo = " + co_programa;
            hql += " and prog.Ativo = " + (ativo ? 1 : 0).ToString();
            return Session.CreateQuery(hql).List<T>();
        }

        T IProgramaDeSaude.BuscarProgramaDeSaudePaciente<T>(int co_prograSaude, string co_paciente)
        {
            string hql = "from ViverMais.Model.ProgramDeSaudePaciente prog where prog.ProgradaDeSaude.Codigo = " + co_prograSaude;
            hql += " and prog.Paciente.Codigo = '" + co_paciente + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IProgramaDeSaude.BuscarPorNome<T>(string nomePrograma)
        {
            return Session.CreateQuery("from ViverMais.Model.ProgramaDeSaude programa where programa.Nome = '" + nomePrograma + "'").UniqueResult<T>();
        }
    }
}
