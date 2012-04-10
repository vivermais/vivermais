﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class RemanejamentoVacina
    {
        public static char ABERTO = 'A';
        public static char FECHADO = 'F';

        private MovimentoVacina movimento;

        public virtual MovimentoVacina Movimento
        {
            get { return movimento; }
            set { movimento = value; }
        }

        private long codigo;

        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        private DateTime data;

        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }
        private char status;

        public virtual char Status
        {
            get { return status; }
            set { status = value; }
        }

        public virtual string SalaOrigem
        {
            get { return this.Movimento.Sala.Nome; }
        }

        public RemanejamentoVacina()
        {
        }
    }
}
