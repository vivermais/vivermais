using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ConfiguracaoPA
    {
        public static readonly char EM_IMPLANTACAO = 'I';
        public static readonly char EM_PRODUCAO = 'P';

        private string codigo;
        public virtual string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private EstabelecimentoSaude estabelecimento;
        public virtual EstabelecimentoSaude Estabelecimento
        {
            get { return estabelecimento; }
            set { estabelecimento = value; }
        }

        private char faseacolhimento;
        public virtual char FaseAcolhimento
        {
            get { return faseacolhimento; }
            set { faseacolhimento = value; }
        }

        public ConfiguracaoPA()
        {
        }
    }
}
