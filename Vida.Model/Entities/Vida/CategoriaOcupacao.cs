﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class CategoriaOcupacao
    {
        public static string MEDICO = "1";
        public static string ENFERMEIRO = "25";

        private string codigo;

        public virtual string Codigo 
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

        //private OrgaoEmissor orgaoEmissor;

        //public virtual OrgaoEmissor OrgaoEmissor
        //{
        //    get { return orgaoEmissor; }
        //    set { orgaoEmissor = value; }
        //}

        public CategoriaOcupacao() 
        {
        }
    }
}
