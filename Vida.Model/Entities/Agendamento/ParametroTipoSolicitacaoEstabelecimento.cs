using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ParametroTipoSolicitacaoEstabelecimento
    {
        public ParametroTipoSolicitacaoEstabelecimento()
        {

        }
        //private string codigo;

        //public virtual string Codigo
        //{
        //    get { return codigo; }
        //    set { codigo = value; }
        //}

        private string codigoEstabelecimento;

        public virtual string CodigoEstabelecimento
        {
            get { return codigoEstabelecimento; }
            set { codigoEstabelecimento = value; }
        }

        //private EstabelecimentoSaude estabelecimentoSaude;

        //public virtual EstabelecimentoSaude EstabelecimentoSaude
        //{
        //    get { return estabelecimentoSaude; }
        //    set { estabelecimentoSaude = value; }
        //}
        private string tipoSolicitacao;

        public virtual string TipoSolicitacao
        {
            get { return tipoSolicitacao; }
            set { tipoSolicitacao = value; }
        }

        //private bool selecionado;

        //public virtual bool Selecionado
        //{
        //    get { return selecionado; }
        //    set { selecionado = value; }
        //}


        //private IList<string> tipoSolicitacao;

        //public virtual IList<string> TipoSolicitacao
        //{
        //    get { return tipoSolicitacao; }
        //    set { tipoSolicitacao = value; }
        //}


        public override bool Equals(object obj)
        {
            if (this.codigoEstabelecimento == ((EstabelecimentoSaude)obj).CNES)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return 30 * base.GetHashCode();
        }
    }
}
