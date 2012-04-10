using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class ParametroAgendaUnidade
    {
        private int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private bool solicitaOutrasUnidades;
        public virtual bool SolicitaOutrasUnidades
        {
            get { return solicitaOutrasUnidades; }
            set { solicitaOutrasUnidades = value; }
        }

        private EstabelecimentoSaude estabelecimento;
        public virtual EstabelecimentoSaude Estabelecimento
        {
            get { return estabelecimento; }
            set { estabelecimento = value; }
        }

        public ParametroAgendaUnidade() { }
    }
}
