using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ClassificacaoRisco
    {
        public static int VERMELHO = 1;
        public static int VERDE = 2;
        public static int AMARELO = 3;
        public static int AZUL = 4;

        int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        string descricao;

        public virtual string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        string cor;

        public virtual string Cor
        {
            get { return cor; }
            set { cor = value; }
        }

        string imagem;

        public virtual string Imagem 
        {
            get { return imagem; }
            set { imagem = value; }
        }

        private int ordem;

        public virtual int Ordem
        {
            get { return ordem; }
            set { ordem = value; }
        }

        private string corgrafico;

        public virtual string CorGrafico
        {
            get { return corgrafico; }
            set { corgrafico = value; }
        }

        public ClassificacaoRisco() 
        {
        }
    }
}
