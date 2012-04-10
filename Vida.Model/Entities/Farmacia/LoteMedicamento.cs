using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class LoteMedicamento
    {
        int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        Medicamento medicamento;
        public virtual Medicamento Medicamento
        {
            get { return medicamento; }
            set { medicamento = value; }
        }

        FabricanteMedicamento fabricante;
        public virtual FabricanteMedicamento Fabricante
        {
            get { return fabricante; }
            set { fabricante = value; }
        }

        string lote;
        public virtual string Lote
        {
            get { return lote; }
            set { lote = value; }
        }

        DateTime validade;
        public virtual DateTime Validade
        {
            get { return validade; }
            set { validade = value; }
        }

        public virtual string NomeMedicamento
        {
            get
            {
                return this.Medicamento.Nome;
            }
        }

        public virtual string NomeFabricante
        {
            get
            {
                return this.Fabricante.Nome;
            }
        }

        public override string ToString()
        {
            return this.Lote;
        }

        public LoteMedicamento()
        {
        }

        public LoteMedicamento(Medicamento medicamento, FabricanteMedicamento fabricante, string lote, DateTime validade) 
        {
            this.Medicamento = medicamento;
            this.Fabricante  = fabricante;
            Lote = lote;
            Validade = validade;
        }
    }
}
