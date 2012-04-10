using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public abstract class SenhaServico : SenhaSenhador
    {
        private string sigla;
        public virtual string Sigla
        {
            get { return sigla; }
            set { sigla = value; }
        }

        private int numero;
        public virtual int Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        private ServicoSenhador servico;
        public virtual ServicoSenhador Servico
        {
            get { return servico; }
            set { servico = value; }
        }

        public override string Impressao()
        {
            throw new NotImplementedException();
        }
    }
}
