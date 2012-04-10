using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class ServicoSenhador
    {
        public ServicoSenhador() 
        {
        }

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

        //private TipoServicoSenhador tipoServico;

        //public virtual TipoServicoSenhador TipoServico
        //{
        //    get { return tipoServico; }
        //    set { tipoServico = value; }
        //}

        //private IList<EstabelecimentoSaude> estabelecimentos;

        //public virtual IList<EstabelecimentoSaude> Estabelecimentos
        //{
        //    get { return estabelecimentos; }
        //    set { estabelecimentos = value; }
        //}
    }
}
