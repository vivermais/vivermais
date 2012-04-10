using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public abstract class SenhaSenhador
    {
        private long codigo;

        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string senha;

        public virtual string Senha
        {
            get { return senha; }
            set { senha = value; }
        }

        private DateTime geradaEm;

        public virtual DateTime GeradaEm
        { 
            get { return geradaEm; }
            set { geradaEm = value; }
        }

        private TipoSenhaSenhador tipoSenha;

        public virtual TipoSenhaSenhador TipoSenha
        {
            get { return tipoSenha; }
            set { tipoSenha = value; }
        }

        private EstabelecimentoSaude estabelecimento;
        public virtual EstabelecimentoSaude Estabelecimento
        {
            get { return estabelecimento; }
            set { estabelecimento = value; }
        }

        /// <summary>
        /// Retorna a senha no formato para impressão
        /// </summary>
        /// <returns></returns>
        public abstract string Impressao();
    }
}
