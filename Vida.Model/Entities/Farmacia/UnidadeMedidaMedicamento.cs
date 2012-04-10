using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class UnidadeMedidaMedicamento
    {
        public static readonly int INDEFINIDA = 22;
        public static readonly int UNIDADE = 17;

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

        string sigla;

        public virtual string Sigla
        {
            get { return sigla; }
            set { sigla = value; }
        }

        //string und;
        //public virtual string Und
        //{
        //    get { return und; }
        //    set { und = value; }
        //}

        private bool ativo;

        public virtual bool Ativo
        {
            get { return ativo; }
            set { ativo = value; }
        }

        public UnidadeMedidaMedicamento()
        {
           
        }
    }
}
