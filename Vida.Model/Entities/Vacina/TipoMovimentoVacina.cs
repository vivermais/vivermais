using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class TipoMovimentoVacina
    {
        //VALORES ASSOCIADOS AO BANCO DE DADOS
        public static int DEVOLUCAO = 1;
        public static int DOACAO = 2;
        public static int EMPRESTIMO = 3;
        public static int PERDA = 4;
        public static int REMANEJAMENTO = 5;
        public static int ACERTO_BALANCO = 7;

        int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        string nome;
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public TipoMovimentoVacina()
        {
        }
    }
}
