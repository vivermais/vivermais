﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class HistoricoItemMovimentoVacina
    {
        public static int LIMITE_ALTERACAO = 2;

        private long codigo;
        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private ItemMovimentoVacina item;

        public virtual ItemMovimentoVacina Item
        {
            get { return item; }
            set { item = value; }
        }
        private int quantidadeanterior;

        public virtual int QuantidadeAnterior
        {
            get { return quantidadeanterior; }
            set { quantidadeanterior = value; }
        }
        private int quantidadealterada;

        public virtual int QuantidadeAlterada
        {
            get { return quantidadealterada; }
            set { quantidadealterada = value; }
        }
        private string motivo;

        public virtual string Motivo
        {
            get { return motivo; }
            set { motivo = value; }
        }
        private DateTime data;

        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }
        private Usuario usuario;

        public virtual Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public HistoricoItemMovimentoVacina()
        {
        }
    }
}
