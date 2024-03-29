﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class DescritivoProcedimentosRealizados
    {
        public DescritivoProcedimentosRealizados() { }

        public DescritivoProcedimentosRealizados(Procedimento procedimento, CBO cbo, Cid cidPrincipal, Cid cidSecundario, int qtd)
        {
            this.Procedimento = procedimento;
            this.Cbo = cbo;
            this.CidPrincipal = cidPrincipal;
            this.CidSecundario = cidSecundario;
            this.Quantidade = qtd;
        }

        private Procedimento procedimento;
        public Procedimento Procedimento
        {
            get { return procedimento; }
            set { procedimento = value; }
        }

        private CBO cbo;
        public CBO Cbo
        {
            get { return cbo; }
            set { cbo = value; }
        }

        private int quantidade;
        public int Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }

        private Cid cidPrincipal;
        public Cid CidPrincipal
        {
            get { return cidPrincipal; }
            set { cidPrincipal = value; }
        }

        private Cid cidSecundario;
        public Cid CidSecundario
        {
            get { return cidSecundario; }
            set { cidSecundario = value; }
        }
    }
}
