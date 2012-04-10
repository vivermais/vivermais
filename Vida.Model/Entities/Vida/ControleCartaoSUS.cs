using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model.Entities;

namespace ViverMais.Model
{
    [Serializable]
    [RelatorioAttribute(true, true)]
    public class ControleCartaoSUS:AModel
    {
        string numeroCartao;

        [RelatorioAttribute(true, true)]
        public virtual string NumeroCartao
        {
            get { return numeroCartao; }
            set { numeroCartao = value; }
        }

        int viaCartao;

        [RelatorioAttribute(true, true)]
        public virtual int ViaCartao
        {
            get { return viaCartao; }
            set { viaCartao = value; }
        }

        DateTime dataEmissao;

        [RelatorioAttribute(true, true)]
        public virtual DateTime DataEmissao
        {
            get { return dataEmissao; }
            set { dataEmissao = value; }
        }

        Usuario usuario;

        [RelatorioAttribute(false, true)]
        public virtual Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public ControleCartaoSUS()
        {

        }

        public override bool Equals(object obj)
        {
            return this.NumeroCartao.Equals(((ControleCartaoSUS)obj).NumeroCartao) && this.ViaCartao.Equals(((ControleCartaoSUS)obj).ViaCartao);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 23;
        }

        public override bool Persistido()
        {
            throw new NotImplementedException();
        }
    }
}
