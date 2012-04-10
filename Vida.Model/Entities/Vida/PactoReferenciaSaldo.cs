using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class PactoReferenciaSaldo
    {
        public PactoReferenciaSaldo()
        {
        }

        public enum StatusTranferiuPactoMesSegunte {TRANSFERIU=1, NAOTRANSFERIU = 2 }

        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private PactoAgregadoProcedCBO pactoAgregadoProcedCBO;

        public virtual PactoAgregadoProcedCBO PactoAgregadoProcedCBO
        {
            get { return pactoAgregadoProcedCBO; }
            set { pactoAgregadoProcedCBO = value; }
        }

        private int mes;

        public virtual int Mes
        {
            get { return mes; }
            set { mes = value; }
        }



        //private float valorMensal;

        //public virtual float ValorMensal
        //{
        //    get { return valorMensal; }
        //    set { valorMensal = value; }
        //}

        private Decimal valorRestante;

        public virtual Decimal ValorRestante
        {
            get { return valorRestante; }
            set { valorRestante = value; }
        }

        private int tranferiuPactoMesSeguinte;

        public virtual int TranferiuPactoMesSeguinte
        {
            get { return tranferiuPactoMesSeguinte; }
            set { tranferiuPactoMesSeguinte = value; }
        }
    }
}
