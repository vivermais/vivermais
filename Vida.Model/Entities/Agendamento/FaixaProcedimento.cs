using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class FaixaProcedimento
    {
        public FaixaProcedimento()
        { 

        }

        private Faixa faixa;

        public virtual Faixa Faixa
        {
            get { return faixa; }
            set { faixa = value; }
        }

        private string id_Procedimento;

        public virtual string Id_Procedimento
        {
            get { return id_Procedimento; }
            set { id_Procedimento = value; }
        }

        private DateTime validadeInicial;

        public virtual DateTime ValidadeInicial
        {
            get { return validadeInicial; }
            set { validadeInicial = value; }
        }

        private DateTime validadeFinal;

        public virtual DateTime ValidadeFinal
        {
            get { return validadeFinal; }
            set { validadeFinal = value; }
        }

        private string id_Unidade;

        public virtual string Id_Unidade
        {
            get { return id_Unidade; }
            set { id_Unidade = value; }
        }

        public override bool Equals(object obj)
        {
            return this.Faixa.Equals(obj) &&
                   this.Id_Procedimento.Equals(obj) &&
                   this.Id_Unidade.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 37;
        }
    }
}
