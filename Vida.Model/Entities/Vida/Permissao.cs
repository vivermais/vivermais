using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Permissao
    {
        Perfil perfil;

        public virtual Perfil Perfil
        {
            get { return perfil; }
            set { perfil = value; }
        }

        Operacao operacao;

        public virtual Operacao Operacao
        {
            get { return operacao; }
            set { operacao = value; }
        }

        public Permissao(Perfil p, Operacao o) 
        {
            this.Perfil   = p;
            this.Operacao = o;
        }

        public Permissao()
        {

        }

        public override bool Equals(object obj)
        {
            return this.Operacao.Codigo == ((Permissao)obj).Operacao.Codigo && this.Perfil.Codigo == ((Permissao)obj).Perfil.Codigo;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 17;
        }
    }
}
