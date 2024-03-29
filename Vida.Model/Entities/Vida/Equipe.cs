﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable] 
    public class Equipe
    {
        private int codigo;

        public virtual int Codigo 
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private Area area;

        public virtual Area Area 
        {
            get { return area; }
            set { area = value; }
        }

        private EstabelecimentoSaude estabelecimentosaude;

        public virtual EstabelecimentoSaude EstabelecimentoSaude 
        {
            get { return estabelecimentosaude; }
            set { estabelecimentosaude = value; }
        }

        private TipoEquipe tipoequipe;

        public virtual TipoEquipe TipoEquipe 
        {
            get { return tipoequipe; }
            set { tipoequipe = value; }
        }

        private string nomereferencia;

        public virtual string NomeReferencia 
        {
            get { return nomereferencia; }
            set { nomereferencia = value; }
        }

        private char atendepopquilombolas;

        public virtual char AtendePopQuilombolas 
        {
            get { return atendepopquilombolas; }
            set { atendepopquilombolas = value; }
        }

        private char atendepopassentados;

        public virtual char AtendePopAssentados
        {
            get { return atendepopassentados; }
            set { atendepopassentados = value; }
        }

        private char atendepopgeral;

        public virtual char AtendePopGeral
        {
            get { return atendepopgeral; }
            set { atendepopgeral = value; }
        }

        private char atendepopescola;

        public virtual char AtendePopEscola
        {
            get { return atendepopescola; }
            set { atendepopescola = value; }
        }

        private char atendepoppronasci;

        public virtual char AtendePopPronasci
        {
            get { return atendepoppronasci; }
            set { atendepoppronasci = value; }
        }

        private char atendepopindigena;

        public virtual char AtendePopIndigena
        {
            get { return atendepopindigena; }
            set { atendepopindigena = value; }
        }

        private char statusequipe;

        public virtual char StatusEquipe 
        {
            get { return statusequipe; }
            set { statusequipe = value; }
        }

        private DateTime ? datadesativacao;
        public virtual DateTime ? DataDesativacao
        {
            get { return datadesativacao; }
            set { datadesativacao = value; }
        }

        public Equipe() 
        {
        }

        public override bool Equals(object obj)
        {
            return this.Area.Equals(obj) &&
                   this.Codigo.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 71;
        }
    }
}
