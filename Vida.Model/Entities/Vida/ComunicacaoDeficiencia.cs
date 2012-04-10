using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ComunicacaoDeficiencia: AModel
    {
        public static int NAO_UTILIZA = 4;
        public static int OUTROS = 3;

        public ComunicacaoDeficiencia(int codigo, string nome)
        {
            this.codigo = codigo;
            this.nome = nome;
        }

        public ComunicacaoDeficiencia()
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
