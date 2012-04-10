using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.Model
{
    [Serializable]
    public class PacienteVacina
    {
        int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        Paciente pacienteVida;

        /// <summary>
        /// Representa a ligação com o pacient ecadastrado no cartão SUS.
        /// Caso seja uma pessoa sem identificação esta propriedade deve estar null enquanto
        /// não tiver registro de cartão SUS
        /// </summary>
        public virtual Paciente PacienteVida
        {
            get { return pacienteVida; }
            set { pacienteVida = value; }
        }

        public PacienteVacina()
        {
        }
    }
}
