using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Estoque
    {
        LoteMedicamento lotemedicamento;
        public virtual LoteMedicamento LoteMedicamento
        {
            get { return lotemedicamento; }
            set { lotemedicamento = value; }
        }

        Farmacia farmacia;
        public virtual Farmacia Farmacia
        {
            get { return farmacia; }
            set { farmacia = value; }
        }

        int quantidadeestoque;
        public virtual int QuantidadeEstoque
        {
            get { return quantidadeestoque; }
            set { quantidadeestoque = value; }
        }

        public Estoque()
        {

        }

        public virtual Medicamento Medicamento
        {
            get { return this.LoteMedicamento.Medicamento; }
        }

        public virtual string NomeMedicamento
        {
            get
            {
                return this.LoteMedicamento.Medicamento.Nome;
            }
        }

        public virtual string NomeLote
        {            
            get             
            {               
                return this.LoteMedicamento.Lote;
            }  
        }       
        
        public virtual string NomeFabricante
        {
            get
            {
                return this.LoteMedicamento.Fabricante.Nome;
            }
        }

        public virtual string DataValidadeLote
        {
            get 
            {
                return this.LoteMedicamento.Validade.ToString("dd/MM/yyyy");
            }
        }

        public virtual string Und
        {
            get
            {
                return this.LoteMedicamento.Medicamento.UnidadeMedida.Sigla;
            }
        }

        public override bool Equals(object obj)
        {
            return this.LoteMedicamento.Codigo == ((Estoque)obj).LoteMedicamento.Codigo &&
                   this.Farmacia.Codigo == ((Estoque)obj).Farmacia.Codigo;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 47;
        }

        public virtual string NomeDropdownDispensacao
        {
            get { return LoteMedicamento.Medicamento.Nome + " - Lote: " + LoteMedicamento.Lote; }
        }

        public virtual int CodigoDropdownDispensacao
        {
            get { return LoteMedicamento.Codigo; }
        }
    }
}
