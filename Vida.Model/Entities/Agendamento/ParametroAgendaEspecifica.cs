﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ParametroAgendaEspecifica
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


        private string id_unidade;
        public virtual string ID_Unidade
        {
            get { return id_unidade; }
            set { id_unidade = value; }
        }

        private int iD_Programa;
        public virtual int ID_Programa
        {
            get { return iD_Programa; }
            set { iD_Programa = value; }
        }

        public ParametroAgendaEspecifica() 
        {
        }
    }
}
