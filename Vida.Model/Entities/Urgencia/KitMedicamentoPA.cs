using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class KitMedicamentoPA
    {
        private long codigo;
        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private KitPA kitpa;

        public virtual KitPA KitPA
        {
            get { return kitpa; }
            set { kitpa = value; }
        }

        private int codigomedicamento;

        public virtual int CodigoMedicamento
        {
            get { return codigomedicamento; }
            set { codigomedicamento = value; }
        }

        private bool medicamentoprincipal;

        public virtual bool MedicamentoPrincipal
        {
            get { return medicamentoprincipal; }
            set { medicamentoprincipal = value; }
        }

        public virtual string DescricaoMedicamentoPrincipal 
        {
            get { return MedicamentoPrincipal ? "Sim" : "Não"; }
        }

        private Medicamento medicamento;

        public virtual Medicamento Medicamento 
        {
            get { return medicamento; }
            set { medicamento = value; }
        }

        private int quantidade;

        public virtual int Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }

        public override bool Equals(object obj)
        {
            return this.KitPA.Codigo == ((KitMedicamentoPA)obj).KitPA.Codigo &&
                   this.CodigoMedicamento == ((KitMedicamentoPA)obj).CodigoMedicamento;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 93;
        }

        public KitMedicamentoPA()
        {
        }

        public virtual string NomeMedicamento 
        {
            get { return Medicamento.Nome; }
        }
    }
}
