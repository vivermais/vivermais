﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class UnidadeDistritoRM
    {
        private int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private EstabelecimentoSaude unidade;
        public virtual EstabelecimentoSaude Unidade
        {
            get { return unidade; }
            set { unidade = value; }
        }

        private Distrito distrito;
        public virtual Distrito Distrito
        {
            get { return distrito; }
            set { distrito = value; }
        }

        public UnidadeDistritoRM()
        {
        }

        public virtual string CNESUnidade
        {
            get { return this.Unidade.CNES; }
        }

        public virtual string NomeUnidade
        {
            get { return this.Unidade.NomeFantasia; }
        }

        public virtual string NomeDistrito
        {
            get { return this.Distrito.Nome; }
        }
    }
}