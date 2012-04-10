using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.Model
{
    public class ItensRequisicao
    {
        int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        RequisicaoMedicamento requisicao;
        public virtual RequisicaoMedicamento Requisicao
        {
            get { return requisicao; }
            set { requisicao = value; }
        }

        Medicamento medicamento;
        public virtual Medicamento Medicamento
        {
            get { return medicamento; }
            set { medicamento = value; }
        }

        LoteMedicamento lotemedicamento;
        public virtual LoteMedicamento LoteMedicamento
        {
            get { return lotemedicamento; }
            set { lotemedicamento = value; }
        }

        GrupoMedicamento grupo;

        public virtual GrupoMedicamento Grupo
        {
            get { return grupo; }
            set { grupo = value; }
        }

        int qtdpedida;
        public virtual int QtdPedida
        {
            get { return qtdpedida; }
            set { qtdpedida = value; }
        }

        int qtdfornecida;
        public virtual int QtdFornecida
        {
            get { return qtdfornecida; }
            set { qtdfornecida = value; }
        }

        int qtdatendida;
        public virtual int QtdAtendida
        {
            get { return qtdatendida; }
            set { qtdatendida = value; }
        }

        int cod_atendimento;
        public virtual int Cod_Atendimento
        {
            get { return cod_atendimento; }
            set { cod_atendimento = value; }
        }

        ElencoMedicamento elenco;
        public virtual ElencoMedicamento Elenco
        {
            get { return elenco; }
            set { elenco = value; }
        }

        int saldoatual;
        public virtual int SaldoAtual
        {
            get { return qtdatendida; }
            set { qtdatendida = value; }
        }

        int saldoanterior;
        public virtual int SaldoAnterior
        {
            get { return saldoanterior; }
            set { saldoanterior = value; }
        }

        string solicitante;
        public virtual string Solicitante
        {
            get { return solicitante; }
            set { solicitante = value; }
        }

        int qtdpedida_distrito;
        public virtual int QtdPedida_Distrito
        {
            get { return qtdpedida_distrito; }
            set { qtdpedida_distrito = value; }
        }


        public ItensRequisicao()
        {

        }

        public virtual string NomeMedicamento
        {
            get
            {
                return this.Medicamento.Nome;
            }
        }

        public virtual int CodigoRequisicao
        {
            get { return Requisicao.Codigo; }
        }

        public virtual int CodMedicamento
        {
            get { return Medicamento.Codigo; }
        }

        public virtual string Lote
        {
            get { return LoteMedicamento.Lote; }
        }

        public virtual int CodGrupo
        {
            get { return Elenco.Codigo; }
        }

        public override bool Equals(object obj)
        {
            return this.Requisicao.Codigo == ((ItensRequisicao)obj).Requisicao.Codigo &&
                   this.LoteMedicamento.Codigo == ((ItensRequisicao)obj).LoteMedicamento.Codigo &&
                   this.Medicamento.Codigo == ((ItensRequisicao)obj).Medicamento.Codigo &&
                   this.Requisicao.Codigo == ((ItensRequisicao)obj).Requisicao.Codigo;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 47;
        }
    }
}
