﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class RemanejamentoMedicamento
    {
        public static char ABERTO = 'A';
        public static char FECHADO = 'F';

        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private Movimento movimento;

        public virtual Movimento Movimento
        {
            get { return movimento; }
            set { movimento = value; }
        }

        private char status;

        public virtual char Status
        {
            get { return status; }
            set { status = value; }
        }

        public virtual string DescricaoStatus
        {
            get
            {
                return status == RemanejamentoMedicamento.ABERTO ? "Aberto" : "Finalizado";
            }
        }

        public virtual string FarmaciaOrigem
        {
            get { return Movimento.Farmacia.Nome; }
        }

        private DateTime dataabertura;

        public virtual DateTime DataAbertura
        {
            get { return dataabertura; }
            set { dataabertura = value; }
        }

        private DateTime ? datafechamento;

        public virtual DateTime ? DataFechamento
        {
            get { return datafechamento; }
            set { datafechamento = value; }
        }

        public RemanejamentoMedicamento()
        {
        }
    }
}
