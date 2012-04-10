using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Farmacia
    {
        public static readonly int ALMOXARIFADO = 360;
        //public enum QualFarmacia { Almoxarifado = 22 };

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

        string endereco;
        public virtual string Endereco
        {
            get { return endereco; }
            set { endereco = value; }
        }

        string fone;
        public virtual string Fone
        {
            get { return fone; }
            set { fone = value; }
        }

        string responsavel;
        public virtual string Responsavel
        {
            get { return responsavel; }
            set { responsavel = value; }
        }

        string codigoUnidade;

        public virtual string CodigoUnidade
        {
            get { return codigoUnidade; }
            set { codigoUnidade = value; }
        }

        //Unidade unidade;
        //public virtual Unidade Unidade
        //{
        //    get { return unidade; }
        //    set { unidade = value; }
        //}

        IList<ElencoMedicamento> elencos;

        public virtual IList<ElencoMedicamento> Elencos
        {
            get { return elencos; }
            set { elencos = value; }
        }

        IList<int> codigosUsuarios;

        public virtual IList<int> CodigosUsuarios
        {
            get { return codigosUsuarios; }
            set { codigosUsuarios = value; }
        }

        public Farmacia()
        {
            elencos = new List<ElencoMedicamento>();
        }

        public override string ToString()
        {
            return this.nome;
        }
    }
}
