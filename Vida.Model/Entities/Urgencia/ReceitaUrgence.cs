using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.Model
{
    public class ReceitaUrgence
    {
        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private Prontuario prontuario;

        public virtual Prontuario Prontuario
        {
            get { return prontuario; }
            set { prontuario = value; }
        }

        private string codigoprofissional;

        public virtual string CodigoProfissional
        {
            get { return codigoprofissional; }
            set { codigoprofissional = value; }
        }

        private DateTime data;

        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        private string conteudo;

        public virtual string Conteudo
        {
            get { return conteudo; }
            set { conteudo = value; }
        }

        public ReceitaUrgence()
        {
        }

        public ReceitaUrgence(Prontuario prontuario, DateTime data, string codigoprofissional, string conteudo) 
        {
            this.Prontuario = prontuario;
            this.Data = data;
            this.CodigoProfissional = codigoprofissional;
            this.Conteudo = conteudo;
        }
    }
}
