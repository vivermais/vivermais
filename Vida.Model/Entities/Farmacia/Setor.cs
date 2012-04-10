using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Setor
    {
        int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        
        string nome;

        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        //string codigoUnidade;

        //public virtual string CodigoUnidade
        //{
        //    get { return codigoUnidade; }
        //    set { codigoUnidade = value; }
        //}

        //private Farmacia farmacia;
        //public virtual Farmacia Farmacia
        //{
        //    get { return farmacia; }
        //    set { farmacia = value; }
        //}

        //Unidade unidade;

        //public virtual Unidade Unidade
        //{
        //    get { return unidade; }
        //    set { unidade = value; }
        //}

        //public virtual string NomeUnidade
        //{
        //    get
        //    {
        //        return this.Unidade.Nome;
        //    }
        //}

        public Setor()
        {

        }
    }
}
