﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ProgramaDeSaudeUnidade
    {
        public ProgramaDeSaudeUnidade()
        {
        }

        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private ProgramaDeSaude programaDeSaude;

        public virtual ProgramaDeSaude ProgramaDeSaude
        {
            get { return programaDeSaude; }
            set { programaDeSaude = value; }
        }

        private EstabelecimentoSaude estabelecimento;

        public virtual EstabelecimentoSaude Estabelecimento
        {
            get { return estabelecimento; }
            set { estabelecimento = value; }
        }

        private bool ativo;
        public virtual bool Ativo
        {
            get { return ativo; }
            set { ativo = value; }
        }

        //private bool unidadeSolicitante;
        //public virtual bool UnidadeSolicitante
        //{
        //    get { return unidadeSolicitante; }
        //    set { unidadeSolicitante = value; }
        //}
        private bool tipoExecutante;
        public virtual bool TipoExecutante
        {
            get { return tipoExecutante; }
            set { tipoExecutante = value; }
        }

        private bool tipoSolicitante;
        public virtual bool TipoSolicitante
        {
            get { return tipoSolicitante; }
            set { tipoSolicitante = value; }
        }

        //private char tipoUnidade;
        //public virtual char TipoUnidade
        //{
        //    get { return tipoUnidade; }
        //    set { tipoUnidade = value; }
        //}

        public virtual string TipoSolicitanteToString
        {
            get
            {
                if (this.TipoSolicitante)
                    return "SIM";
                else if (!this.TipoSolicitante)
                    return "NÃO";
                else
                    return string.Empty;
            }
        }

        public virtual string TipoExecutanteToString
        {
            get
            {
                if (this.TipoExecutante)
                    return "SIM";
                else if (!this.TipoExecutante)
                    return "NÃO";
                else
                    return string.Empty;
            }
        }

        //public virtual string AtivoToString
        //{
        //    get
        //    {
        //        if (this.Ativo)
        //            return "SIM";
        //        else
        //            return "NÃO";
        //    }
        //}
    }
}
