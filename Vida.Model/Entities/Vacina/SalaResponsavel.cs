﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class SalaResponsavel
    {
        private SalaVacina salavacina;
        public virtual SalaVacina SalaVacina
        {
            get { return salavacina; }
            set { salavacina = value; }
        }

        private Usuario responsavel;
        public virtual Usuario Responsavel
        {
            get { return responsavel; }
            set { responsavel = value; }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 19;
        }

        public override bool Equals(object obj)
        {
            return this.Responsavel.Equals(obj) && this.SalaVacina.Equals(obj);
        }

        public SalaResponsavel()
        {
        }

        public SalaResponsavel(Usuario u, SalaVacina s)
        {
            this.Responsavel = u;
            this.SalaVacina  = s;
        }
    }
}