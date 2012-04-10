using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ProcedimentoNaoFaturavel
    {
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

        //private long codigoprocedimento;
        //public virtual long CodigoProcedimento
        //{
        //    get { return codigoprocedimento; }
        //    set { codigoprocedimento = value; }
        //}

        private char status;

        public virtual char Status
        {
            get { return status; }
            set { status = value; }
        }

        public virtual string DescricaoStatus 
        {
            get { return Status == 'A' ? "Ativo" : "Inativo"; }
        }

        public ProcedimentoNaoFaturavel()
        {
        }
    }
}
