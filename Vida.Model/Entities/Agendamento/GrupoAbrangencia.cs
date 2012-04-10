﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class GrupoAbrangencia
    {
        public GrupoAbrangencia()
        {

        }

        private string codigo;

        public virtual string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string nomeGrupo;

        public virtual string NomeGrupo
        {
            get { return nomeGrupo; }
            set { nomeGrupo = value; }
        }

        private bool ativo;

        public virtual bool Ativo
        {
            get { return ativo; }
            set { ativo = value; }
        }

        private IList<Municipio> municipios;

        public virtual IList<Municipio> Municipios
        {
            get { return municipios; }
            set { municipios = value; }
        }

        public override string ToString()
        {
            return this.NomeGrupo;
        }



    }
}