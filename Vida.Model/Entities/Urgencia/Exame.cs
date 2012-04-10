using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Exame
    {
        public enum EnumDescricaoStatus { Ativo = 'A', Inativo = 'I' };

        int codigo;
        public virtual int Codigo
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

        char status;
        public virtual char Status
        {
            get { return status; }
            set { status = value; }
        }

        public Exame()
        {

        }

        public override string ToString()
        {
            return this.descricao;
        }

        public virtual string DescricaoStatus
        {
            get { return this.status == 'A' ? "Ativo" : "Inativo"; }
        }

    }
}