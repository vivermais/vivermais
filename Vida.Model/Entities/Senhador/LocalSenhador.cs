using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class LocalSenhador
    {
        public LocalSenhador() 
        {
        }

        private long codigo;

        public virtual long Codigo
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

        private EstabelecimentoSaude estabelecimento;
        public virtual EstabelecimentoSaude Estabelecimento
        {
            get { return estabelecimento; }
            set { estabelecimento = value; }
        }

        private IList<ServicoEstabelecimentoSenhador> servicoslocal;
        public virtual IList<ServicoEstabelecimentoSenhador> ServicosLocal
        {
            get { return servicoslocal; }
            set { servicoslocal = value; }
        }

        //private ServicoSenhador servico;

        //public virtual ServicoSenhador Servico
        //{
        //    get { return servico; }
        //    set { servico = value; }
        //}
    }
}
