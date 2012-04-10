using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.Model
{
    public class Evolucao
    {
        int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        Prontuario prontuario;
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

        Profissional profissional;
        public virtual Profissional Profissional
        {
            get { return profissional; }
            set { profissional = value; }
        }

        public virtual string NomeProfissionalToString 
        {
            get 
            {
                if (Profissional != null)
                    return Profissional.Nome;
                return "";
            }
        }

        string observacao;
        public virtual string Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }

        string aprazamento;
        public virtual string Aprazamento 
        {
            get 
            {
                if (string.IsNullOrEmpty(aprazamento))
                    return " - ";
                return aprazamento;
            }
            set { aprazamento = value; }
        }

        DateTime data;
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        public Evolucao()
        {     
            //this.profissional = new Profissional();
            this.data = new DateTime();

        }
    }
}
