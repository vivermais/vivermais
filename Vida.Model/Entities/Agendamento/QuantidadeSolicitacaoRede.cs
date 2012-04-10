﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class QuantidadeSolicitacaoRede
    {

        private int idQuantidadeSolicitacaoRede;

        public virtual int IdQuantidadeSolicitacaoRede
        {
            get { return idQuantidadeSolicitacaoRede; }
            set { idQuantidadeSolicitacaoRede = value; }
        }


        private int qtdSolicitacoes;

        public virtual int QtdSolicitacoes
        {
            get { return qtdSolicitacoes; }
            set { qtdSolicitacoes = value; }
        }

        private string competencia;

        public virtual string Competencia
        {
            get { return competencia; }
            set { competencia = value; }
        }

        private Procedimento procedimento;

        public virtual Procedimento Procedimento
        {
            get { return procedimento; }
            set { procedimento = value; }
        }

        private CBO cBO;

        public virtual CBO CBO
        {
            get { return cBO; }
            set { cBO = value; }
        }

        private SubGrupo subGrupo;

        public virtual SubGrupo SubGrupo
        {
            get { return subGrupo; }
            set { subGrupo = value; }
        }

        private EstabelecimentoSaude estabelecimento;

        public virtual EstabelecimentoSaude Estabelecimento
        {
            get { return estabelecimento; }
            set { estabelecimento = value; }
        }


        public QuantidadeSolicitacaoRede() { }
    }
}
