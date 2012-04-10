﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ViverMais.Model
{
    [Serializable]    
    public class RacaCor:AModel
    {
        public static readonly string INDIGENA = "5";

        string codigo;

        public virtual string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        string descricao;

        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        public RacaCor()
        {

        }

        public override bool Persistido()
        {
            return this.codigo != string.Empty;
        }
    }
}