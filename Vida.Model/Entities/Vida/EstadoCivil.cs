using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class EstadoCivil:AModel
    {
        public EstadoCivil()
        {

        }

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

        DateTime dataAtivacaoDesativacao;

        public virtual DateTime DataAtivacaoDesativacao
        {
            get { return dataAtivacaoDesativacao; }
            set { dataAtivacaoDesativacao = value; }
        }

        char situacaoAtivacao;

        public virtual char SituacaoAtivacao
        {
            get { return situacaoAtivacao; }
            set { situacaoAtivacao = value; }
        }

        public override bool Persistido()
        {
            return this.codigo != string.Empty;
        }
    }
}
