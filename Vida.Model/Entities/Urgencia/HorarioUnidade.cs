using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    
    public class HorarioUnidade
    {
        string codigounidade;

        public virtual string CodigoUnidade
        {
            get { return codigounidade; }
            set { codigounidade = value; }
        }
        
        string horario;

        public virtual string Horario
        {
            get { return horario; }
            set { horario = value; }
        }

        
        public HorarioUnidade()
        {

        }

        public override string ToString()
        {
            return this.Horario + "h";
        }

    }
}
