﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ParametroAgenda
    {
        public enum TipoDeAgenda { DISTRITAL = 1, ESPECIFICA = 2, LOCAL = 3, REDE = 4, RESERVA_TECNICA = 5 }

        public const string CONFIGURACAO_UNIDADE = "U";
        public const string CONFIGURACAO_PROCEDIMENTO = "P";

        private string tipoConfiguracao;

        public virtual string TipoConfiguracao
        {
            get { return tipoConfiguracao; }
            set { tipoConfiguracao = value; }
        }

        private int codigo;
        public virtual int Codigo 
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string cnes;
        public virtual string Cnes
        {
            get { return cnes; }
            set { cnes = value; }
        }

        private int tipoAgenda;
        public virtual int TipoAgenda
        {
            get { return tipoAgenda; }
            set { tipoAgenda = value; }
        }

        private int percentual;
        public virtual int Percentual
        {
            get { return percentual; }
            set { percentual = value; }
        }

        private Procedimento procedimento;

        public virtual Procedimento Procedimento
        {
            get { return procedimento; }
            set { procedimento = value; }
        }

        private CBO cbo;

        public virtual CBO Cbo
        {
            get { return cbo; }
            set { cbo = value; }
        }

        private SubGrupo subGrupo;
        public virtual SubGrupo SubGrupo
        {
            get { return subGrupo; }
            set { subGrupo = value; }
        }

        //private string iD_Procedimento;
        //public virtual string ID_Procedimento
        //{
        //    get { return iD_Procedimento; }
        //    set { iD_Procedimento = value; }
        //}

        //private int iD_Programa;
        //public virtual int ID_Programa
        //{
        //    get { return iD_Programa; }
        //    set { iD_Programa = value; }
        //}
        private ParametroAgendaUnidade parametroAgendaUnidade;
        public virtual ParametroAgendaUnidade ParametroAgendaUnidade
        {
            get { return parametroAgendaUnidade; }
            set { parametroAgendaUnidade = value; }
        }

        public ParametroAgenda() { }
    }
}
