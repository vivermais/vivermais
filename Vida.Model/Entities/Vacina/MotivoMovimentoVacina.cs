using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class MotivoMovimentoVacina
    {
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

        private TipoMovimentoVacina tipomovimento;

        public virtual TipoMovimentoVacina TipoMovimento
        {
            get { return tipomovimento; }
            set { tipomovimento = value; }
        }

        public MotivoMovimentoVacina()
        {
        }
    }
}
