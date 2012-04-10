﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class SolicitanteExameLaboratorio
    {

        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        private string nome;

        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        private int codigoConselho;

        public virtual int CodigoConselho
        {
            get { return codigoConselho; }
            set { codigoConselho = value; }
        }
        private int numeroConselho;

        public virtual int NumeroConselho
        {
            get { return numeroConselho; }
            set { numeroConselho = value; }
        }
    }
}