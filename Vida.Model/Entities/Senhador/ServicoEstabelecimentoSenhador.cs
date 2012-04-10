using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class ServicoEstabelecimentoSenhador
    {
        public ServicoEstabelecimentoSenhador()
        {
        }

        private long codigo;

        public virtual long Codigo
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
        private ServicoSenhador servico;

        public virtual ServicoSenhador Servico
        {
            get { return servico; }
            set { servico = value; }
        }
        private TipoServicoSenhador tiposervico;

        public virtual TipoServicoSenhador TipoServico
        {
            get { return tiposervico; }
            set { tiposervico = value; }
        }
    }
}
