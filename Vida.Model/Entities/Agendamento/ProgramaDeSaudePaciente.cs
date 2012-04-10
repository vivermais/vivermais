using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class ProgramaDeSaudePaciente
    {
        public ProgramaDeSaudePaciente() { }
        public ProgramaDeSaudePaciente(ProgramaDeSaude programa, Paciente paciente)
        {
            this.ProgramaDeSaude = programa;
            this.Paciente = paciente;
            this.Ativo = true;
        }

        private ProgramaDeSaude programaDeSaude;

        public virtual ProgramaDeSaude ProgramaDeSaude
        {
            get { return programaDeSaude; }
            set { programaDeSaude = value; }
        }

        private Paciente paciente;

        public virtual Paciente Paciente
        {
            get { return paciente; }
            set { paciente = value; }
        }

        private bool ativo;

        public virtual bool Ativo
        {
            get { return ativo; }
            set { ativo = value; }
        }

        public override bool Equals(object obj)
        {
            return this.Paciente.Codigo == ((ViverMais.Model.Paciente)obj).Codigo &&
                this.ProgramaDeSaude.Codigo == ((ViverMais.Model.ProgramaDeSaude)obj).Codigo;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 23;
        }
    }
}
