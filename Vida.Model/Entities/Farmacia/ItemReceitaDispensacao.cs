using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ItemReceitaDispensacao
    {
        private int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private ReceitaDispensacao receita;
        public virtual ReceitaDispensacao Receita 
        {
            get { return receita; }
            set { receita = value; }
        }

        private Medicamento medicamento;
        public virtual Medicamento Medicamento 
        {
            get { return medicamento; }
            set { medicamento = value; }
        }

        private int qtdPrescrita;
        public virtual int QtdPrescrita 
        {
            get { return qtdPrescrita; }
            set { qtdPrescrita = value; }
        }     

 
        public virtual string NomeMedicamento
        {
            get { return Medicamento.Nome; }
        }       

        public virtual string CodMedicamento
        {
            get { return Medicamento.CodMedicamento; }
        }
        

        public override int GetHashCode()
        {
            return base.GetHashCode() * 47;
        }

        public ItemReceitaDispensacao() 
        {
        }
    }
}
