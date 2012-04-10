using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Biometria
    {
        string codigoPaciente;

        public virtual string CodigoPaciente
        {
            get { return codigoPaciente; }
            set { codigoPaciente = value; }
        }

        public Biometria()
        {

        }
    }
}
