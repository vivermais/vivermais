using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class PacienteUrgence
    {
        private long codigo;

        public virtual long Codigo
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

        private string descricao;
        public virtual string Descricao 
        {
            get { return descricao; }
            set { descricao = value; }
        }

        private string codigoViverMais;
        public virtual string CodigoViverMais
        {
            get { return codigoViverMais; }
            set { codigoViverMais = value; }
        }

        private Paciente pacienteViverMais;
        public virtual Paciente PacienteViverMais
        {
            get { return pacienteViverMais; }
            set { pacienteViverMais = value; }
        }

        private char sexo;
        public virtual char Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }

        private bool simplificado;
        public virtual bool Simplificado
        {
            get { return simplificado; }
            set { simplificado = value; }
        }

        public PacienteUrgence() 
        {
        }
    }
}
