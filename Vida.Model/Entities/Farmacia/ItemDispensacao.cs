﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ItemDispensacao
    {
        private int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }


        private Dispensacao dispensacao;
        public virtual Dispensacao Dispensacao
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
            return this.Dispensacao == ((ItemDispensacao)obj).Dispensacao &&
                   this.LoteMedicamento == ((ItemDispensacao)obj).LoteMedicamento;
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

        public ItemDispensacao() 
        {
        }

        public virtual int QtdPrescrita
        {
            get { return Dispensacao.Receita.LocalizarPrescricao(this).QtdPrescrita; }
        }

        public virtual bool isQuantidadeSolicitadaMaior(int valor)
        {
            return QtdDispensada > valor;
        }        
    }
}
