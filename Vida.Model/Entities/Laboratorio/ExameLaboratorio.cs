using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ExameLaboratorio
    {
        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        private string nomeSus;

        public virtual string NomeSus
        {
            get { return nomeSus; }
            set { nomeSus = value; }
        }

        private string nomeUsual;

        public virtual string NomeUsual
        {
            get { return nomeUsual; }
            set { nomeUsual = value; }
        }
        private string sg;

        public virtual string Sg
        {
            get { return sg; }
            set { sg = value; }
        }

        public virtual string SgNomeUsual
        {
            get
            {
                return Sg + " - " + nomeUsual;
            }
        }
    }
}
