using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ProntuarioExameEletivo
    {
        long codigo;
        public virtual long Codigo
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

        private ExameEletivo exame;
        public virtual ExameEletivo Exame
        {
            get { return exame; }
            set { exame = value; }
        }

        private DateTime data;
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        private string profissional;
        public virtual string Profissional
        {
            get { return profissional; }
            set { profissional = value; }
        }

        private string cboprofissional;
        public virtual string CBOProfissional
        {
            get { return cboprofissional; }
            set { cboprofissional = value; }
        }

        public virtual string NomeExame
        {
            get { return Exame.Descricao; }
        }

        public ProntuarioExameEletivo()
        {
        }
    }
}
