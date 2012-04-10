using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ProteseDeficiencia: AModel
    {
        public static int NAO_UTILIZA = 5;
        
        public ProteseDeficiencia(int codigo, string nome)
        {
            this.codigo = codigo;
            this.nome = nome;
        }

        public ProteseDeficiencia()
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
