using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]

    public class NewsLetter
    {
        string nome;
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }

        }

        string email;
        public virtual string Email
        {
            get { return email; }
            set { email = value; }
        }

        int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        DateTime dataCriacao;

        public virtual DateTime DataCriacao
        {
            get { return dataCriacao; }
            set { dataCriacao = value; }
        }
    }
}
