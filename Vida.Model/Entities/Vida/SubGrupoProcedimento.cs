using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class SubGrupoProcedimento
    {
        private string codigo;

        public virtual string Codigo 
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

        private GrupoProcedimento grupoprocedimento;

        public virtual GrupoProcedimento GrupoProcedimento 
        {
            get { return grupoprocedimento; }
            set { grupoprocedimento = value; }
        }

        private string datacompetencia;

        public virtual string DataCompetencia 
        {
            get { return datacompetencia; }
            set { datacompetencia = value; }
        }

        public SubGrupoProcedimento() 
        {
        }
    }
}
