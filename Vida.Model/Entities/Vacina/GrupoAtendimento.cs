﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class GrupoAtendimento
    {
        //VALORES ASSOCIADOS AO BANCO DE DADOS
        public static int INDIGENAS = 1;
        public static int ASSENTADOS = 2;
        public static int ACAMPADOS = 3;
        public static int MILITARES = 4;
        public static int QUILOMBOLA = 5;
        public static int POPULACAO_PRIVADA_LIBERDADE = 6;
        public static int POPULACAO_GERAL = 7;

        public GrupoAtendimento()
        {
        }

        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string descricao;

        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
    }
}
