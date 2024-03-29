﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Modulo
    {
        public static int FARMACIA = 22;
        public static int ESTABELECIMENTO_SAUDE = 30;
        public static int OUVIDORIA = 26;
        public static int ENVIO_BPA = 27;
        public static int CARTAO_SUS = 1;
        public static int URGENCIA = 23;
        public static int AGENDAMENTO = 24;
        public static int NUTRINET = 25;
        public static int SEGURANCA = 28;
        public static int VACINA = 29;
        public static int PROFISSIONAL = 31;
        public static int LABORATORIO = 32;
        public static int GUIAPROCEDIMENTOS = 33;
        public static int ATENDIMENTO = 34;
        public static int SENHADOR = 35;

        int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        string nome;

        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public Modulo()
        {

        }

        public override string ToString()
        {
            return this.Nome;
        }
    }
}
