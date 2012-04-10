﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class GrupoAbrangenciaMunicipio
    {
        public GrupoAbrangenciaMunicipio() { }
        
        public GrupoAbrangenciaMunicipio(GrupoAbrangencia g, Municipio m) 
        {
            this.Municipio = m;
            this.Grupo = g;
        }

        private Municipio municipio;

        public virtual Municipio Municipio
        {
            get { return municipio; }
            set { municipio = value; }
        }

        private GrupoAbrangencia grupo;

        public virtual GrupoAbrangencia Grupo
        {
            get { return grupo; }
            set { grupo = value; }
        }

        //private float valorPacto;

        //public virtual float ValorPacto
        //{
        //    get { return valorPacto; }
        //    set { valorPacto = value; }
        //}

        //private float saldoPacto;

        //public virtual float SaldoPacto
        //{
        //    get { return saldoPacto; }
        //    set { saldoPacto = value; }
        //}

        public override int GetHashCode()
        {
            return base.GetHashCode() * 41;
        }

        public override bool Equals(object obj)
        {
            return this.Municipio.Equals(obj) && this.Grupo.Equals(obj);
        }
    }
}
