using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Deficiencia: AModel
    {
        public static int FISICA = 1;
        public static int INTELECTUAL = 3;
        public static int VISUAL = 6;
        public static int MULTIPLA = 4;
        public static int AUDITIVA = 2;

        public Deficiencia(int codigo, string nome)
        {
            this.codigo = codigo;
            this.nome = nome;
        }

        public Deficiencia()
        {
        }

        private int codigo;
        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string nome;
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public override bool Persistido()
        {
            throw new NotImplementedException();
        }
    }
}
