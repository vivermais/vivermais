﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class SubTipoVinculacao
    {
        string codigo;

        public virtual string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        
        Vinculacao vinculacao;

        public virtual Vinculacao Vinculacao
        {
            get { return vinculacao; }
            set { vinculacao = value; }
        }

        TipoVinculacao tipoVinculacao;

        public virtual TipoVinculacao TipoVinculacao
        {
            get { return tipoVinculacao; }
            set { tipoVinculacao = value; }
        }

        string descricaoSubVinculo;

        public virtual string DescricaoSubVinculo
        {
            get { return descricaoSubVinculo; }
            set { descricaoSubVinculo = value; }
        }

        public SubTipoVinculacao()
        {

        }
    }
}
