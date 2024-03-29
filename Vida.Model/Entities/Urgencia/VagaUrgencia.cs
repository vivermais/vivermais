﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class VagaUrgencia
    {
        public static char FEMININA = 'F';
        public static char MASCULINA = 'M';
        public static char INFANTIL = 'I';

        private char tipovaga;

        public virtual char TipoVaga 
        {
            get { return tipovaga; }
            set { tipovaga = value; }
        }

        private Prontuario prontuario;

        public virtual Prontuario Prontuario 
        {
            get { return prontuario; }
            set { prontuario = value; }
        }

        private string codigounidade;

        public virtual string CodigoUnidade
        {
            get { return codigounidade; }
            set { codigounidade = value; }
        }

        private EstabelecimentoSaude unidade;

        public virtual EstabelecimentoSaude Unidade
        {
            get { return unidade; }
            set { unidade = value; }
        }

        private long codigo;

        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public VagaUrgencia()
        {
        }

        public VagaUrgencia(string codigounidade, char tipovaga)
        {
            this.codigounidade = codigounidade;
            this.tipovaga = tipovaga;
        }
    }
}
