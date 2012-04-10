using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model.Entities.ViverMais
{
    [Serializable]
    public class BpaConsolidado: BPA
    {
        public BpaConsolidado()
        {
        }

        private int idade;
        /// <summary>
        /// Esta idade deve ser utilizada somente pelo módulo urgência e emergência,
        /// pois os pacientes atendidos nele não necessariamente possuem
        /// vínculo com o sus
        /// </summary>
        public int Idade
        {
            set 
            {
                if (this.Paciente != null)
                    throw new Exception("Não é possível atribuir a idade sem a informação do paciente.");
                idade = value;
            }
        }

        public override int IdadePaciente()
        {
            if (this.paciente != null)
                return base.IdadePaciente();

            return this.idade;
        }
    }
}
