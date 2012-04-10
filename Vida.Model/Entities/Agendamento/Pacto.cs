using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Pacto
    {
        public enum StatusSaldoAcumulativo {SIM = 1, NAO = 2 }

        public static int SaldoAcumulativo = Convert.ToInt32(Pacto.StatusSaldoAcumulativo.SIM);
        public static int PercentualUrgenciaEmergencia = 15;

        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private Municipio municipio;

        public virtual Municipio Municipio
        {
            get { return municipio; }
            set { municipio = value; }
        }

        //private int valorRestante;

        //public virtual int ValorRestante
        //{
        //    get { return valorRestante; }
        //    set { valorRestante = value; }
        //}

        //private int valorInicial;

        //public virtual int ValorInicial
        //{
        //    get { return valorInicial; }
        //    set { valorInicial = value; }
        //}

        private IList<PactoAgregadoProcedCBO> pactosAgregados;

        public virtual IList<PactoAgregadoProcedCBO> PactosAgregados
        {
            get { return pactosAgregados; }
            set { pactosAgregados = value; }
        }

        //public virtual IList<PactoAgregadoProcedCBO> PactoAgregadoProcedCBO
        //{
        //    get { return pactoAgregadoProcedCBO; }
        //    set { pactoAgregadoProcedCBO = value; }
        //}
        

        //private IList<PactoProcedimentoCBO> pactosProcedimentoCBO;

        //public virtual IList<PactoProcedimentoCBO> PactosProcedimentoCBO
        //{
        //    get { return pactosProcedimentoCBO; }
        //    set { pactosProcedimentoCBO = value; }
        //}

        //public IList<PactoProcedimento> PactoProcedimento
        //{
        //    get { return pactoProcedimento; }
        //    set { pactoProcedimento = value; }
        //}

        public Pacto()
        {
            this.PactosAgregados = new List<PactoAgregadoProcedCBO>();
        }
    }
}
