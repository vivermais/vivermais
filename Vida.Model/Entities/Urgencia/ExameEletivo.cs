using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ExameEletivo
    {
        public enum DescricaoStatus { Ativo = 'A', Inativo = 'I' };

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

        private char status;
        public virtual char Status
        {
            get { return status; }
            set { status = value; }
        }

        public virtual string StatusToString
        {
            get { return Status == 'A' ? "Ativo" : "Inativo"; }
        }

        public ExameEletivo()
        {
        }
    }
}
