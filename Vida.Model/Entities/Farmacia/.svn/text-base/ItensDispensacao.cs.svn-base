using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.Model
{
    public class ItensDispensacao
    {
        private ReceitaDispensacao dispensacao;
        public virtual ReceitaDispensacao Dispensacao 
        {
            get { return dispensacao; }
            set { dispensacao = value; }
        }

        private LoteMedicamento loteMedicamento;
        public virtual LoteMedicamento LoteMedicamento 
        {
            get { return loteMedicamento; }
            set { loteMedicamento = value; }
        }

        private Farmacia farmacia;
        public virtual Farmacia Farmacia
        {
            get { return farmacia; }
            set { farmacia = value; }
        }

        private DateTime dataAtendimento;
        public virtual DateTime DataAtendimento 
        {
            get { return dataAtendimento; }
            set { dataAtendimento = value; }
        }

        private int qtdPrescrita;
        public virtual int QtdPrescrita 
        {
            get { return qtdPrescrita; }
            set { qtdPrescrita = value; }
        }

        private int qtdDispensada;
        public virtual int QtdDispensada 
        {
            get { return qtdDispensada; }
            set { qtdDispensada = value; }
        }

        private int qtdDias;
        public virtual int QtdDias 
        {
            get { return qtdDias; }
            set { qtdDias = value; }
        }

        private string observacao;
        public virtual string Observacao 
        {
            get { return observacao; }
            set { observacao = value; }
        }

        public override bool Equals(object obj)
        {
            return this.Dispensacao.Codigo == ((ItensDispensacao)obj).Dispensacao.Codigo &&
                   this.LoteMedicamento.Codigo == ((ItensDispensacao)obj).LoteMedicamento.Codigo &&
                   this.DataAtendimento == ((ItensDispensacao)obj).DataAtendimento;
        }

        public virtual string Medicamento
        {
            get { return LoteMedicamento.Medicamento.Nome; }
            set { LoteMedicamento.Medicamento.Nome = value; }
        }

        string codigoUsuario;

        public virtual string CodigoUsuario
        {
            get { return codigoUsuario; }
            set { codigoUsuario = value; }
        }

        public virtual string NomeLoteMedicamento 
        {
            get { return LoteMedicamento.Lote; }
        }

        public virtual string CodigoLoteMedicamento
        {
            get { return LoteMedicamento.Codigo.ToString(); }
        }

        public virtual string Fabricante 
        {
            get { return LoteMedicamento.Fabricante.Nome; }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 47;
        }

        public ItensDispensacao() 
        {
        }
    }
}
