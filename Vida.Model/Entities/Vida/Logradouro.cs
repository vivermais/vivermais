using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Logradouro:AModel
    {
        long cep;

        public virtual long CEP
        {
            get { return cep; }
            set { cep = value; }
        }

        string tipo;

        public virtual string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        string preposicao;

        public virtual string Preposicao
        {
            get { return preposicao; }
            set { preposicao = value; }
        }

        string titulo;

        public virtual string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }

        string nomeLogradouro;

        public virtual string NomeLogradouro
        {
            get { return nomeLogradouro; }
            set { nomeLogradouro = value; }
        }

        string informacaoAdicional;

        public virtual string InformacaoAdicional
        {
            get { return informacaoAdicional; }
            set { informacaoAdicional = value; }
        }

        string bairro;

        public virtual string Bairro
        {
            get { return bairro; }
            set { bairro = value; }
        }

        string cidade;

        public virtual string Cidade
        {
            get { return cidade; }
            set { cidade = value; }
        }

        public Logradouro()
        {

        }

        public override bool Persistido()
        {
            throw new NotImplementedException();
        }
    }
}
