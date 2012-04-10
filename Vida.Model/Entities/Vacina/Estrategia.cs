﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Estrategia
    {
        //VALORES ASSOCIADOS AO BANCO DE DADOS
        public static int ROTINA = 1;
        public static int ESPECIAL = 2;
        public static int BLOQUEIO = 3;
        public static int INTENSIFICACAO = 4;
        public static int CAMPANHA_INDISCRIMINADA = 5;
        public static int CAMPANHA_SELETIVA = 6;
        public static int SOROTERAPIA = 7;

        public Estrategia()
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

        private IList<Vacina> vacinas;

        public virtual IList<Vacina> Vacinas
        {
            get { return vacinas; }
            set { vacinas = value; }
        }
    }
}
