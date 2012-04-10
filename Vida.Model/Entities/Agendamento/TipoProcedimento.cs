﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class TipoProcedimento
    {
        //private Procedimento procedimento;
        //public virtual Procedimento Procedimento
        //{
        //    get { return procedimento; }
        //    set { procedimento = value; }
        //}
        public enum TiposDeProcedimento {REGULADO = 1, AUTORIZADO = 2, AGENDADO = 3, ATENDIMENTOBASICO = 4 }


        private string procedimento;
        public virtual string Procedimento
        {
            get { return procedimento; }
            set { procedimento = value; }
        }

        private char tipo;
        public virtual char Tipo 
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private IList<Preparo> preparos;

        public virtual IList<Preparo> Preparos
        {
            get { return preparos; }
            set { preparos = value; }
        }

        public TipoProcedimento()
        {
            this.Preparos = new List<Preparo>();
        }

        //public override bool Equals(object obj)
        //{
        //    if (this.Procedimento.Codigo == ((Procedimento)obj).Codigo)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        //public override int GetHashCode()
        //{
        //    return 30* base.GetHashCode();
        //}
    }
}
