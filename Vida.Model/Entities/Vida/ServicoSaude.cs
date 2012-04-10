﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class ServicoSaude
    {
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

        IList<EstabelecimentoSaude> unidades;

        public virtual IList<EstabelecimentoSaude> Unidades
        {
            get { return unidades; }
            set { unidades = value; }
        }

        public ServicoSaude()
        {
            unidades = new List<EstabelecimentoSaude>();
        }
    }
}