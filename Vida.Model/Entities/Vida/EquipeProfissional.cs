﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class EquipeProfissional
    {
        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private Equipe equipe;

        public virtual Equipe Equipe 
        {
            get { return equipe; }
            set { equipe = value; }
        }

        private Profissional profissional;

        public virtual Profissional Profissional 
        {
            get { return profissional; }
            set { profissional = value; }
        }

        private EstabelecimentoSaude estabelecimentosaude;

        public virtual EstabelecimentoSaude EstabelecimentoSaude 
        {
            get { return estabelecimentosaude; }
            set { estabelecimentosaude = value; }
        }

        private CBO cbo;

        public virtual CBO CBO 
        {
            get { return cbo; }
            set { cbo = value; }
        }

        private char vinculosus;

        public virtual char VinculoSUS 
        {
            get { return vinculosus; }
            set { vinculosus = value; }
        }

        private string identificacaovinculo;

        public virtual string IdentificacaoVinculo 
        {
            get { return identificacaovinculo; }
            set { identificacaovinculo = value; }
        }

        private char equipeminima;

        public virtual char EquipeMinima 
        {
            get { return equipeminima; }
            set { equipeminima = value; }
        }

        private DateTime dataentrada;

        public virtual DateTime DataEntrada 
        {
            get { return dataentrada; }
            set { dataentrada = value; }
        }

        private DateTime datadesligamento;

        public virtual DateTime DataDesligamento
        {
            get { return datadesligamento; }
            set { datadesligamento = value; }
        }

        //public override bool Equals(object obj)
        //{
        //    return this.profissional.Equals(obj); //&&
        //           //this.Equipe.Equals(obj);
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode() * 37;
        //}

        public EquipeProfissional() 
        {
        }
    }
}
