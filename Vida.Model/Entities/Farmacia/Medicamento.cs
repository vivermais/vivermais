using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Medicamento
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

        string codmedicamento;
        public virtual string CodMedicamento
        {
            get { return codmedicamento; }
            set { codmedicamento = value; }
        }

        bool ind_antibio;
        public virtual bool Ind_Antibio
        {
            get { return ind_antibio; }
            set { ind_antibio = value; }
        }

        UnidadeMedidaMedicamento unidademedida;
        public virtual UnidadeMedidaMedicamento UnidadeMedida
        {
            get { return unidademedida; }
            set { unidademedida = value; }
        }

        bool pertencearede;
        public virtual bool PertenceARede
        {
            get { return pertencearede; }
            set { pertencearede = value; }
        }

        bool emedicamento;
        public virtual bool EMedicamento
        {
            get { return emedicamento; }
            set { emedicamento = value; }
        }

        public virtual string PertenceARedeToString
        {
            get { return pertencearede?"Sim":"Não"; }
            
        }

        public virtual string UnidadeMedidaToString
        {
            get { return UnidadeMedida.Nome; }
        }

        public override string ToString()
        {
            return this.Nome;
        }

        public override bool Equals(object obj)
        {
            if (this.codigo == ((ViverMais.Model.Medicamento)obj).codigo)
                return true;
            else
                return false;
        }

        public Medicamento()
        {
            
        }
    }
}
