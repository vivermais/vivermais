using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ProcedimentoAgregado
    {
        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        
        private Procedimento procedimento;

        public virtual Procedimento Procedimento 
        {
            get { return procedimento; }
            set { procedimento = value; }
        }

        private Agregado agregado;

        public virtual Agregado Agregado
        {
            get { return agregado; }
            set { agregado = value; }
        }

        private int valor;

        public virtual int Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        public ProcedimentoAgregado() 
        {
            
        }

        //public override bool Equals(object obj)
        //{
        //    //if (this.Procedimento.Codigo == ((Procedimento)obj).Codigo)
        //    //{
        //    //    return true;
        //    //}
        //    //else
        //    //{
        //    //    return false;
        //    //}
        //}

        //public override int GetHashCode()
        //{
        //    return 30 * base.GetHashCode();
        //}
    }
}
