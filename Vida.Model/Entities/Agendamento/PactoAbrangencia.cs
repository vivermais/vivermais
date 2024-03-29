﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class PactoAbrangencia
    {        
        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private GrupoAbrangencia grupo;

        public virtual GrupoAbrangencia Grupo
        {
          get { return grupo; }
          set { grupo = value; }
        }

        private IList<PactoAbrangenciaAgregado> pactoAbrangenciaAgregado;

        public virtual IList<PactoAbrangenciaAgregado> PactoAbrangenciaAgregado
        {
            get { return pactoAbrangenciaAgregado; }
            set { pactoAbrangenciaAgregado = value; }
        }

        //private  IList<PactoAbrangenciaProcedimentoCBO> pactoAbrangenciaProcedimentoCBO;

        //public virtual IList<PactoAbrangenciaProcedimentoCBO> PactoAbrangenciaProcedimentoCBO
        //{
        //  get { return pactoAbrangenciaProcedimentoCBO; }
        //  set { pactoAbrangenciaProcedimentoCBO = value; }
        //}

        //public IList<PactoProcedimento> PactoProcedimento
        //{
        //    get { return pactoProcedimento; }
        //    set { pactoProcedimento = value; }
        //}
        public PactoAbrangencia()
        {
            this.PactoAbrangenciaAgregado= new List<PactoAbrangenciaAgregado>();
        }
    }
}
