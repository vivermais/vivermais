using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model.Entities;

namespace ViverMais.Model
{
    [Serializable]
    [RelatorioAttribute(true, true)]
    public class Perfil
    {
        int codigo;

        [RelatorioAttribute(true, false)]
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        string nome;

        [RelatorioAttribute(false, true)]
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        private Modulo modulo;

        [RelatorioAttribute(false, true)]
        public virtual Modulo Modulo 
        {
            get { return modulo; }
            set { modulo = value; }
        }

        private bool perfilProfissionalSaude;

        public virtual bool PerfilProfissionalSaude
        {
            get { return perfilProfissionalSaude; }
            set { perfilProfissionalSaude = value; }
        }
        
        //public virtual string ModuloToString 
        //{
        //    get { return Modulo.Nome; }
        //}

        private IList<Operacao> operacoes;

        public virtual IList<Operacao> Operacoes 
        {
            get { return operacoes; }
            set { operacoes = value; }
        }

        public override bool Equals(object obj)
        {
            return this.Codigo == (((Perfil)obj)).Codigo;
        }

        public Perfil()
        {

        }
    }
}
