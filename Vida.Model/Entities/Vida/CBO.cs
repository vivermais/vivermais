using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class CBO
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

        private char ocupacaosaude;

        public virtual char OcupacaoSaude
        {
            get { return ocupacaosaude; }
            set { ocupacaosaude = value; }
        }

        private CategoriaOcupacao categoriaocupacao;

        public virtual CategoriaOcupacao CategoriaOcupacao
        {
            get { return categoriaocupacao; }
            set { categoriaocupacao = value; }
        }

        private GrupoCBO grupoCBO;

        public virtual GrupoCBO GrupoCBO
        {
            get { return grupoCBO; }
            set { grupoCBO = value; }
        }

        public override string ToString()
        {
            return this.Nome;
        }
        //public override bool Equals(object obj)
        //{
        //    CBO cbo = (CBO)obj;
        //    if (cbo.Codigo == this.Codigo)
        //        return true;
        //    else
        //        return false;
        //}

        public CBO()
        {
        }

    }
}
