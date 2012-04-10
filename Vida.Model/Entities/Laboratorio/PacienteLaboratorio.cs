using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class PacienteLaboratorio
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
        private string rg;

        public virtual string Rg
        {
            get { return rg; }
            set { rg = value; }
        }
        private string cartaoSus;

        public virtual string CartaoSus
        {
            get { return cartaoSus; }
            set { cartaoSus = value; }
        }

        private DateTime dataNascimento;

        public virtual DateTime DataNascimento
        {
            get { return dataNascimento; }
            set { dataNascimento = value; }
        }

        public virtual int Idade
        {
            get {
                int idade = DateTime.Now.Year - dataNascimento.Year;
                if (DateTime.Now.Month < dataNascimento.Month ||
                (DateTime.Now.Month == dataNascimento.Month &&
                DateTime.Now.Day < dataNascimento.Day))
                    idade--;
                return idade;
            }
        }
    }
}
