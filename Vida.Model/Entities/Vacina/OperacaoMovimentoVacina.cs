﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class OperacaoMovimentoVacina
    {
        //VALORES ASSOCIADOS AO BANCO DE DADOS
        public static int ENTRADA = 1;
        public static int SAIDA = 2;

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

        public OperacaoMovimentoVacina()
        {
        }

        public static int RetornaSituacao(int co_tipomovimento)
        {
            if (co_tipomovimento == TipoMovimentoVacina.PERDA || co_tipomovimento == TipoMovimentoVacina.REMANEJAMENTO)
                return OperacaoMovimentoVacina.SAIDA;
            else if (co_tipomovimento == TipoMovimentoVacina.DEVOLUCAO)
                return OperacaoMovimentoVacina.ENTRADA;

            return -1;
        }
    }
}
