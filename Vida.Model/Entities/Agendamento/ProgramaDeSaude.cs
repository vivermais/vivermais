using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ProgramaDeSaude
    {
        public ProgramaDeSaude()
        {

        }

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

        private bool multiDisciplinar;
        public virtual bool MultiDisciplinar
        {
            get { return multiDisciplinar; }
            set { multiDisciplinar = value; }
        }

        private bool ativo;
        public virtual bool Ativo
        {
            get { return ativo; }
            set { ativo = value; }
        }

        public virtual string MultiDisciplinarToString
        {
            get 
            {
                if (this.MultiDisciplinar)
                    return "SIM";
                else
                    return "NÃO";
            }
        }

        public override string ToString()
        {
            return this.Nome;
        }
    }
}
