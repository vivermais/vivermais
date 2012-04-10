using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ParametroAgendaDistrital
    {
        private int codigo;
        public virtual int Codigo 
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private ParametroAgenda parametroagenda;
        public virtual ParametroAgenda ParametroAgenda
        {
            get { return parametroagenda; }
            set { parametroagenda = value; }
        }


        private int id_distrito;
        public virtual int ID_Distrito
        {
            get { return id_distrito; }
            set { id_distrito = value; }
        }

        public ParametroAgendaDistrital() 
        {
        }
    }
}
