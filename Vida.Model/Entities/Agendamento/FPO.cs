﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class FPO
    {
        private int competencia;
        
        public virtual int Competencia
        {
            get { return competencia; }
            set { competencia = value; }
        }


        private int qtd_total;

        public virtual int QTD_Total
        {
            get { return qtd_total; }
            set { qtd_total = value; }
        }

        private float valorTotal;

        public virtual float ValorTotal
        {
            get { return valorTotal; }
            set { valorTotal = value; }
        }

        private char nivelApuracao;

        public virtual char NivelApuracao
        {
            get { return nivelApuracao; }
            set { nivelApuracao = value; }
        }

        private EstabelecimentoSaude estabelecimento;

        public virtual EstabelecimentoSaude Estabelecimento
        {
            get { return estabelecimento; }
            set { estabelecimento = value; }
        }

        //public virtual string ID_EstabelecimentoSaude
        //{
        //    get { return iD_EstabelecimentoSaude; }
        //    set { iD_EstabelecimentoSaude = value; }
        //}

        

        //public virtual EstabelecimentoSaude EstabelecimentoSaude
        //{
        //    get { return estabelecimentoSaude; }
        //    set { estabelecimentoSaude = value; }
        //}


        private Procedimento procedimento;

        public virtual Procedimento Procedimento
        {
            get { return procedimento; }
            set { procedimento = value; }
        }

        //public virtual string ID_Procedimento
        //{
        //    get { return iD_Procedimento; }
        //    set { iD_Procedimento = value; }
        //}
        

        public override bool Equals(object obj)
        {
            return this.Estabelecimento.CNES == ((FPO)obj).Estabelecimento.CNES &&
                   this.Procedimento.Codigo == ((FPO)obj).Procedimento.Codigo &&
                   this.Competencia == ((FPO)obj).Competencia;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 47;
        }

        public FPO()
        {
        }
    }
}
